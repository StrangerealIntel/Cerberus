using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Ionic.BZip2
{
	// Token: 0x020000BD RID: 189
	[ComVisible(true)]
	public class ParallelBZip2OutputStream : Stream
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x0002AE0C File Offset: 0x0002900C
		public ParallelBZip2OutputStream(Stream output) : this(output, BZip2.MaxBlockSize, false)
		{
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0002AE1C File Offset: 0x0002901C
		public ParallelBZip2OutputStream(Stream output, int blockSize) : this(output, blockSize, false)
		{
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0002AE28 File Offset: 0x00029028
		public ParallelBZip2OutputStream(Stream output, bool leaveOpen) : this(output, BZip2.MaxBlockSize, leaveOpen)
		{
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0002AE38 File Offset: 0x00029038
		public ParallelBZip2OutputStream(Stream output, int blockSize, bool leaveOpen)
		{
			if (blockSize < BZip2.MinBlockSize || blockSize > BZip2.MaxBlockSize)
			{
				string message = string.Format("blockSize={0} is out of range; must be between {1} and {2}", blockSize, BZip2.MinBlockSize, BZip2.MaxBlockSize);
				throw new ArgumentException(message, "blockSize");
			}
			this.output = output;
			if (!this.output.CanWrite)
			{
				throw new ArgumentException("The stream is not writable.", "output");
			}
			this.bw = new BitWriter(this.output);
			this.blockSize100k = blockSize;
			this.leaveOpen = leaveOpen;
			this.combinedCRC = 0u;
			this.MaxWorkers = 16;
			this.EmitHeader();
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0002AF1C File Offset: 0x0002911C
		private void InitializePoolOfWorkItems()
		{
			this.toWrite = new Queue<int>();
			this.toFill = new Queue<int>();
			this.pool = new List<WorkItem>();
			int num = ParallelBZip2OutputStream.BufferPairsPerCore * Environment.ProcessorCount;
			num = Math.Min(num, this.MaxWorkers);
			for (int i = 0; i < num; i++)
			{
				this.pool.Add(new WorkItem(i, this.blockSize100k));
				this.toFill.Enqueue(i);
			}
			this.newlyCompressedBlob = new AutoResetEvent(false);
			this.currentlyFilling = -1;
			this.lastFilled = -1;
			this.lastWritten = -1;
			this.latestCompressed = -1;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0002AFC4 File Offset: 0x000291C4
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0002AFCC File Offset: 0x000291CC
		public int MaxWorkers
		{
			get
			{
				return this._maxWorkers;
			}
			set
			{
				if (value < 4)
				{
					throw new ArgumentException("MaxWorkers", "Value must be 4 or greater.");
				}
				this._maxWorkers = value;
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0002AFEC File Offset: 0x000291EC
		public override void Close()
		{
			if (this.pendingException != null)
			{
				this.handlingException = true;
				Exception ex = this.pendingException;
				this.pendingException = null;
				throw ex;
			}
			if (this.handlingException)
			{
				return;
			}
			if (this.output == null)
			{
				return;
			}
			Stream stream = this.output;
			try
			{
				this.FlushOutput(true);
			}
			finally
			{
				this.output = null;
				this.bw = null;
			}
			if (!this.leaveOpen)
			{
				stream.Close();
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0002B07C File Offset: 0x0002927C
		private void FlushOutput(bool lastInput)
		{
			if (this.emitting)
			{
				return;
			}
			if (this.currentlyFilling >= 0)
			{
				WorkItem wi = this.pool[this.currentlyFilling];
				this.CompressOne(wi);
				this.currentlyFilling = -1;
			}
			if (lastInput)
			{
				this.EmitPendingBuffers(true, false);
				this.EmitTrailer();
				return;
			}
			this.EmitPendingBuffers(false, false);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0002B0E4 File Offset: 0x000292E4
		public override void Flush()
		{
			if (this.output != null)
			{
				this.FlushOutput(false);
				this.bw.Flush();
				this.output.Flush();
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0002B110 File Offset: 0x00029310
		private void EmitHeader()
		{
			byte[] array = new byte[]
			{
				66,
				90,
				104,
				0
			};
			array[3] = (byte)(48 + this.blockSize100k);
			byte[] array2 = array;
			this.output.Write(array2, 0, array2.Length);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0002B154 File Offset: 0x00029354
		private void EmitTrailer()
		{
			this.bw.WriteByte(23);
			this.bw.WriteByte(114);
			this.bw.WriteByte(69);
			this.bw.WriteByte(56);
			this.bw.WriteByte(80);
			this.bw.WriteByte(144);
			this.bw.WriteInt(this.combinedCRC);
			this.bw.FinishAndPad();
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0002B1D4 File Offset: 0x000293D4
		public int BlockSize
		{
			get
			{
				return this.blockSize100k;
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0002B1DC File Offset: 0x000293DC
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool mustWait = false;
			if (this.output == null)
			{
				throw new IOException("the stream is not open");
			}
			if (this.pendingException != null)
			{
				this.handlingException = true;
				Exception ex = this.pendingException;
				this.pendingException = null;
				throw ex;
			}
			if (offset < 0)
			{
				throw new IndexOutOfRangeException(string.Format("offset ({0}) must be > 0", offset));
			}
			if (count < 0)
			{
				throw new IndexOutOfRangeException(string.Format("count ({0}) must be > 0", count));
			}
			if (offset + count > buffer.Length)
			{
				throw new IndexOutOfRangeException(string.Format("offset({0}) count({1}) bLength({2})", offset, count, buffer.Length));
			}
			if (count == 0)
			{
				return;
			}
			if (!this.firstWriteDone)
			{
				this.InitializePoolOfWorkItems();
				this.firstWriteDone = true;
			}
			int num = 0;
			int num2 = count;
			for (;;)
			{
				this.EmitPendingBuffers(false, mustWait);
				mustWait = false;
				int index;
				if (this.currentlyFilling >= 0)
				{
					index = this.currentlyFilling;
					goto IL_124;
				}
				if (this.toFill.Count != 0)
				{
					index = this.toFill.Dequeue();
					this.lastFilled++;
					goto IL_124;
				}
				mustWait = true;
				IL_1A0:
				if (num2 <= 0)
				{
					goto Block_12;
				}
				continue;
				IL_124:
				WorkItem workItem = this.pool[index];
				workItem.ordinal = this.lastFilled;
				int num3 = workItem.Compressor.Fill(buffer, offset, num2);
				if (num3 != num2)
				{
					if (!ThreadPool.QueueUserWorkItem(new WaitCallback(this.CompressOne), workItem))
					{
						break;
					}
					this.currentlyFilling = -1;
					offset += num3;
				}
				else
				{
					this.currentlyFilling = index;
				}
				num2 -= num3;
				num += num3;
				goto IL_1A0;
			}
			throw new Exception("Cannot enqueue workitem");
			Block_12:
			this.totalBytesWrittenIn += (long)num;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0002B3A4 File Offset: 0x000295A4
		private void EmitPendingBuffers(bool doAll, bool mustWait)
		{
			if (this.emitting)
			{
				return;
			}
			this.emitting = true;
			if (doAll || mustWait)
			{
				this.newlyCompressedBlob.WaitOne();
			}
			do
			{
				int num = -1;
				int num2 = doAll ? 200 : (mustWait ? -1 : 0);
				int num3 = -1;
				do
				{
					if (Monitor.TryEnter(this.toWrite, num2))
					{
						num3 = -1;
						try
						{
							if (this.toWrite.Count > 0)
							{
								num3 = this.toWrite.Dequeue();
							}
						}
						finally
						{
							Monitor.Exit(this.toWrite);
						}
						if (num3 >= 0)
						{
							WorkItem workItem = this.pool[num3];
							if (workItem.ordinal != this.lastWritten + 1)
							{
								lock (this.toWrite)
								{
									this.toWrite.Enqueue(num3);
								}
								if (num == num3)
								{
									this.newlyCompressedBlob.WaitOne();
									num = -1;
								}
								else if (num == -1)
								{
									num = num3;
								}
							}
							else
							{
								num = -1;
								BitWriter bitWriter = workItem.bw;
								bitWriter.Flush();
								MemoryStream ms = workItem.ms;
								ms.Seek(0L, SeekOrigin.Begin);
								long num4 = 0L;
								byte[] array = new byte[1024];
								int num5;
								while ((num5 = ms.Read(array, 0, array.Length)) > 0)
								{
									for (int i = 0; i < num5; i++)
									{
										this.bw.WriteByte(array[i]);
									}
									num4 += (long)num5;
								}
								if (bitWriter.NumRemainingBits > 0)
								{
									this.bw.WriteBits(bitWriter.NumRemainingBits, (uint)bitWriter.RemainingBits);
								}
								this.combinedCRC = (this.combinedCRC << 1 | this.combinedCRC >> 31);
								this.combinedCRC ^= workItem.Compressor.Crc32;
								this.totalBytesWrittenOut += num4;
								bitWriter.Reset();
								this.lastWritten = workItem.ordinal;
								workItem.ordinal = -1;
								this.toFill.Enqueue(workItem.index);
								if (num2 == -1)
								{
									num2 = 0;
								}
							}
						}
					}
					else
					{
						num3 = -1;
					}
				}
				while (num3 >= 0);
			}
			while (doAll && this.lastWritten != this.latestCompressed);
			this.emitting = false;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0002B614 File Offset: 0x00029814
		private void CompressOne(object wi)
		{
			WorkItem workItem = (WorkItem)wi;
			try
			{
				workItem.Compressor.CompressAndWrite();
				lock (this.latestLock)
				{
					if (workItem.ordinal > this.latestCompressed)
					{
						this.latestCompressed = workItem.ordinal;
					}
				}
				lock (this.toWrite)
				{
					this.toWrite.Enqueue(workItem.index);
				}
				this.newlyCompressedBlob.Set();
			}
			catch (Exception ex)
			{
				lock (this.eLock)
				{
					if (this.pendingException != null)
					{
						this.pendingException = ex;
					}
				}
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0002B710 File Offset: 0x00029910
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0002B714 File Offset: 0x00029914
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0002B718 File Offset: 0x00029918
		public override bool CanWrite
		{
			get
			{
				if (this.output == null)
				{
					throw new ObjectDisposedException("BZip2Stream");
				}
				return this.output.CanWrite;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0002B73C File Offset: 0x0002993C
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0002B744 File Offset: 0x00029944
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x0002B74C File Offset: 0x0002994C
		public override long Position
		{
			get
			{
				return this.totalBytesWrittenIn;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0002B754 File Offset: 0x00029954
		public long BytesWrittenOut
		{
			get
			{
				return this.totalBytesWrittenOut;
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0002B75C File Offset: 0x0002995C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0002B764 File Offset: 0x00029964
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0002B76C File Offset: 0x0002996C
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0002B774 File Offset: 0x00029974
		[Conditional("Trace")]
		private void TraceOutput(ParallelBZip2OutputStream.TraceBits bits, string format, params object[] varParams)
		{
			if ((bits & this.desiredTrace) != ParallelBZip2OutputStream.TraceBits.None)
			{
				lock (this.outputLock)
				{
					int hashCode = Thread.CurrentThread.GetHashCode();
					Console.ForegroundColor = hashCode % 8 + ConsoleColor.Green;
					Console.Write("{0:000} PBOS ", hashCode);
					Console.WriteLine(format, varParams);
					Console.ResetColor();
				}
			}
		}

		// Token: 0x04000325 RID: 805
		private static readonly int BufferPairsPerCore = 4;

		// Token: 0x04000326 RID: 806
		private int _maxWorkers;

		// Token: 0x04000327 RID: 807
		private bool firstWriteDone;

		// Token: 0x04000328 RID: 808
		private int lastFilled;

		// Token: 0x04000329 RID: 809
		private int lastWritten;

		// Token: 0x0400032A RID: 810
		private int latestCompressed;

		// Token: 0x0400032B RID: 811
		private int currentlyFilling;

		// Token: 0x0400032C RID: 812
		private volatile Exception pendingException;

		// Token: 0x0400032D RID: 813
		private bool handlingException;

		// Token: 0x0400032E RID: 814
		private bool emitting;

		// Token: 0x0400032F RID: 815
		private Queue<int> toWrite;

		// Token: 0x04000330 RID: 816
		private Queue<int> toFill;

		// Token: 0x04000331 RID: 817
		private List<WorkItem> pool;

		// Token: 0x04000332 RID: 818
		private object latestLock = new object();

		// Token: 0x04000333 RID: 819
		private object eLock = new object();

		// Token: 0x04000334 RID: 820
		private object outputLock = new object();

		// Token: 0x04000335 RID: 821
		private AutoResetEvent newlyCompressedBlob;

		// Token: 0x04000336 RID: 822
		private long totalBytesWrittenIn;

		// Token: 0x04000337 RID: 823
		private long totalBytesWrittenOut;

		// Token: 0x04000338 RID: 824
		private bool leaveOpen;

		// Token: 0x04000339 RID: 825
		private uint combinedCRC;

		// Token: 0x0400033A RID: 826
		private Stream output;

		// Token: 0x0400033B RID: 827
		private BitWriter bw;

		// Token: 0x0400033C RID: 828
		private int blockSize100k;

		// Token: 0x0400033D RID: 829
		private ParallelBZip2OutputStream.TraceBits desiredTrace = ParallelBZip2OutputStream.TraceBits.Crc | ParallelBZip2OutputStream.TraceBits.Write;

		// Token: 0x0200027B RID: 635
		[Flags]
		private enum TraceBits : uint
		{
			// Token: 0x04000B1B RID: 2843
			None = 0u,
			// Token: 0x04000B1C RID: 2844
			Crc = 1u,
			// Token: 0x04000B1D RID: 2845
			Write = 2u,
			// Token: 0x04000B1E RID: 2846
			All = 4294967295u
		}
	}
}
