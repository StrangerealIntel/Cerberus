using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Ionic.Crc;

namespace Ionic.Zlib
{
	// Token: 0x020000CA RID: 202
	[ComVisible(true)]
	public class ParallelDeflateOutputStream : Stream
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x000322DC File Offset: 0x000304DC
		public ParallelDeflateOutputStream(Stream stream) : this(stream, CompressionLevel.Default, CompressionStrategy.Default, false)
		{
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x000322E8 File Offset: 0x000304E8
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level) : this(stream, level, CompressionStrategy.Default, false)
		{
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000322F4 File Offset: 0x000304F4
		public ParallelDeflateOutputStream(Stream stream, bool leaveOpen) : this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen)
		{
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00032300 File Offset: 0x00030500
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level, bool leaveOpen) : this(stream, CompressionLevel.Default, CompressionStrategy.Default, leaveOpen)
		{
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0003230C File Offset: 0x0003050C
		public ParallelDeflateOutputStream(Stream stream, CompressionLevel level, CompressionStrategy strategy, bool leaveOpen)
		{
			this._outStream = stream;
			this._compressLevel = level;
			this.Strategy = strategy;
			this._leaveOpen = leaveOpen;
			this.MaxBufferPairs = 16;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00032380 File Offset: 0x00030580
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x00032388 File Offset: 0x00030588
		public CompressionStrategy Strategy { get; private set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00032394 File Offset: 0x00030594
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x0003239C File Offset: 0x0003059C
		public int MaxBufferPairs
		{
			get
			{
				return this._maxBufferPairs;
			}
			set
			{
				if (value < 4)
				{
					throw new ArgumentException("MaxBufferPairs", "Value must be 4 or greater.");
				}
				this._maxBufferPairs = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000323BC File Offset: 0x000305BC
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x000323C4 File Offset: 0x000305C4
		public int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
			set
			{
				if (value < 1024)
				{
					throw new ArgumentOutOfRangeException("BufferSize", "BufferSize must be greater than 1024 bytes");
				}
				this._bufferSize = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x000323E8 File Offset: 0x000305E8
		public int Crc32
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x000323F0 File Offset: 0x000305F0
		public long BytesProcessed
		{
			get
			{
				return this._totalBytesProcessed;
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x000323F8 File Offset: 0x000305F8
		private void _InitializePoolOfWorkItems()
		{
			this._toWrite = new Queue<int>();
			this._toFill = new Queue<int>();
			this._pool = new List<WorkItem>();
			int num = ParallelDeflateOutputStream.BufferPairsPerCore * Environment.ProcessorCount;
			num = Math.Min(num, this._maxBufferPairs);
			for (int i = 0; i < num; i++)
			{
				this._pool.Add(new WorkItem(this._bufferSize, this._compressLevel, this.Strategy, i));
				this._toFill.Enqueue(i);
			}
			this._newlyCompressedBlob = new AutoResetEvent(false);
			this._runningCrc = new CRC32();
			this._currentlyFilling = -1;
			this._lastFilled = -1;
			this._lastWritten = -1;
			this._latestCompressed = -1;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000324B4 File Offset: 0x000306B4
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool mustWait = false;
			if (this._isClosed)
			{
				throw new InvalidOperationException();
			}
			if (this._pendingException != null)
			{
				this._handlingException = true;
				Exception pendingException = this._pendingException;
				this._pendingException = null;
				throw pendingException;
			}
			if (count == 0)
			{
				return;
			}
			if (!this._firstWriteDone)
			{
				this._InitializePoolOfWorkItems();
				this._firstWriteDone = true;
			}
			for (;;)
			{
				this.EmitPendingBuffers(false, mustWait);
				mustWait = false;
				int num;
				if (this._currentlyFilling >= 0)
				{
					num = this._currentlyFilling;
					goto IL_AF;
				}
				if (this._toFill.Count != 0)
				{
					num = this._toFill.Dequeue();
					this._lastFilled++;
					goto IL_AF;
				}
				mustWait = true;
				IL_170:
				if (count <= 0)
				{
					return;
				}
				continue;
				IL_AF:
				WorkItem workItem = this._pool[num];
				int num2 = (workItem.buffer.Length - workItem.inputBytesAvailable > count) ? count : (workItem.buffer.Length - workItem.inputBytesAvailable);
				workItem.ordinal = this._lastFilled;
				Buffer.BlockCopy(buffer, offset, workItem.buffer, workItem.inputBytesAvailable, num2);
				count -= num2;
				offset += num2;
				workItem.inputBytesAvailable += num2;
				if (workItem.inputBytesAvailable == workItem.buffer.Length)
				{
					if (!ThreadPool.QueueUserWorkItem(new WaitCallback(this._DeflateOne), workItem))
					{
						break;
					}
					this._currentlyFilling = -1;
				}
				else
				{
					this._currentlyFilling = num;
				}
				goto IL_170;
			}
			throw new Exception("Cannot enqueue workitem");
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0003263C File Offset: 0x0003083C
		private void _FlushFinish()
		{
			byte[] array = new byte[128];
			ZlibCodec zlibCodec = new ZlibCodec();
			int num = zlibCodec.InitializeDeflate(this._compressLevel, false);
			zlibCodec.InputBuffer = null;
			zlibCodec.NextIn = 0;
			zlibCodec.AvailableBytesIn = 0;
			zlibCodec.OutputBuffer = array;
			zlibCodec.NextOut = 0;
			zlibCodec.AvailableBytesOut = array.Length;
			num = zlibCodec.Deflate(FlushType.Finish);
			if (num != 1 && num != 0)
			{
				throw new Exception("deflating: " + zlibCodec.Message);
			}
			if (array.Length - zlibCodec.AvailableBytesOut > 0)
			{
				this._outStream.Write(array, 0, array.Length - zlibCodec.AvailableBytesOut);
			}
			zlibCodec.EndDeflate();
			this._Crc32 = this._runningCrc.Crc32Result;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00032704 File Offset: 0x00030904
		private void _Flush(bool lastInput)
		{
			if (this._isClosed)
			{
				throw new InvalidOperationException();
			}
			if (this.emitting)
			{
				return;
			}
			if (this._currentlyFilling >= 0)
			{
				WorkItem wi = this._pool[this._currentlyFilling];
				this._DeflateOne(wi);
				this._currentlyFilling = -1;
			}
			if (lastInput)
			{
				this.EmitPendingBuffers(true, false);
				this._FlushFinish();
				return;
			}
			this.EmitPendingBuffers(false, false);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0003277C File Offset: 0x0003097C
		public override void Flush()
		{
			if (this._pendingException != null)
			{
				this._handlingException = true;
				Exception pendingException = this._pendingException;
				this._pendingException = null;
				throw pendingException;
			}
			if (this._handlingException)
			{
				return;
			}
			this._Flush(false);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000327C8 File Offset: 0x000309C8
		public override void Close()
		{
			if (this._pendingException != null)
			{
				this._handlingException = true;
				Exception pendingException = this._pendingException;
				this._pendingException = null;
				throw pendingException;
			}
			if (this._handlingException)
			{
				return;
			}
			if (this._isClosed)
			{
				return;
			}
			this._Flush(true);
			if (!this._leaveOpen)
			{
				this._outStream.Close();
			}
			this._isClosed = true;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00032840 File Offset: 0x00030A40
		public new void Dispose()
		{
			this.Close();
			this._pool = null;
			this.Dispose(true);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00032858 File Offset: 0x00030A58
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00032864 File Offset: 0x00030A64
		public void Reset(Stream stream)
		{
			if (!this._firstWriteDone)
			{
				return;
			}
			this._toWrite.Clear();
			this._toFill.Clear();
			foreach (WorkItem workItem in this._pool)
			{
				this._toFill.Enqueue(workItem.index);
				workItem.ordinal = -1;
			}
			this._firstWriteDone = false;
			this._totalBytesProcessed = 0L;
			this._runningCrc = new CRC32();
			this._isClosed = false;
			this._currentlyFilling = -1;
			this._lastFilled = -1;
			this._lastWritten = -1;
			this._latestCompressed = -1;
			this._outStream = stream;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00032934 File Offset: 0x00030B34
		private void EmitPendingBuffers(bool doAll, bool mustWait)
		{
			if (this.emitting)
			{
				return;
			}
			this.emitting = true;
			if (doAll || mustWait)
			{
				this._newlyCompressedBlob.WaitOne();
			}
			do
			{
				int num = -1;
				int num2 = doAll ? 200 : (mustWait ? -1 : 0);
				int num3 = -1;
				do
				{
					if (Monitor.TryEnter(this._toWrite, num2))
					{
						num3 = -1;
						try
						{
							if (this._toWrite.Count > 0)
							{
								num3 = this._toWrite.Dequeue();
							}
						}
						finally
						{
							Monitor.Exit(this._toWrite);
						}
						if (num3 >= 0)
						{
							WorkItem workItem = this._pool[num3];
							if (workItem.ordinal != this._lastWritten + 1)
							{
								lock (this._toWrite)
								{
									this._toWrite.Enqueue(num3);
								}
								if (num == num3)
								{
									this._newlyCompressedBlob.WaitOne();
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
								this._outStream.Write(workItem.compressed, 0, workItem.compressedBytesAvailable);
								this._runningCrc.Combine(workItem.crc, workItem.inputBytesAvailable);
								this._totalBytesProcessed += (long)workItem.inputBytesAvailable;
								workItem.inputBytesAvailable = 0;
								this._lastWritten = workItem.ordinal;
								this._toFill.Enqueue(workItem.index);
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
			while (doAll && this._lastWritten != this._latestCompressed);
			this.emitting = false;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00032B00 File Offset: 0x00030D00
		private void _DeflateOne(object wi)
		{
			WorkItem workItem = (WorkItem)wi;
			try
			{
				int index = workItem.index;
				CRC32 crc = new CRC32();
				crc.SlurpBlock(workItem.buffer, 0, workItem.inputBytesAvailable);
				this.DeflateOneSegment(workItem);
				workItem.crc = crc.Crc32Result;
				lock (this._latestLock)
				{
					if (workItem.ordinal > this._latestCompressed)
					{
						this._latestCompressed = workItem.ordinal;
					}
				}
				lock (this._toWrite)
				{
					this._toWrite.Enqueue(workItem.index);
				}
				this._newlyCompressedBlob.Set();
			}
			catch (Exception pendingException)
			{
				lock (this._eLock)
				{
					if (this._pendingException != null)
					{
						this._pendingException = pendingException;
					}
				}
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00032C28 File Offset: 0x00030E28
		private bool DeflateOneSegment(WorkItem workitem)
		{
			ZlibCodec compressor = workitem.compressor;
			compressor.ResetDeflate();
			compressor.NextIn = 0;
			compressor.AvailableBytesIn = workitem.inputBytesAvailable;
			compressor.NextOut = 0;
			compressor.AvailableBytesOut = workitem.compressed.Length;
			do
			{
				compressor.Deflate(FlushType.None);
			}
			while (compressor.AvailableBytesIn > 0 || compressor.AvailableBytesOut == 0);
			compressor.Deflate(FlushType.Sync);
			workitem.compressedBytesAvailable = (int)compressor.TotalBytesOut;
			return true;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00032CA0 File Offset: 0x00030EA0
		[Conditional("Trace")]
		private void TraceOutput(ParallelDeflateOutputStream.TraceBits bits, string format, params object[] varParams)
		{
			if ((bits & this._DesiredTrace) != ParallelDeflateOutputStream.TraceBits.None)
			{
				lock (this._outputLock)
				{
					int hashCode = Thread.CurrentThread.GetHashCode();
					Console.ForegroundColor = hashCode % 8 + ConsoleColor.DarkGray;
					Console.Write("{0:000} PDOS ", hashCode);
					Console.WriteLine(format, varParams);
					Console.ResetColor();
				}
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00032D14 File Offset: 0x00030F14
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00032D18 File Offset: 0x00030F18
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00032D1C File Offset: 0x00030F1C
		public override bool CanWrite
		{
			get
			{
				return this._outStream.CanWrite;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00032D2C File Offset: 0x00030F2C
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00032D34 File Offset: 0x00030F34
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x00032D44 File Offset: 0x00030F44
		public override long Position
		{
			get
			{
				return this._outStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00032D4C File Offset: 0x00030F4C
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00032D54 File Offset: 0x00030F54
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00032D5C File Offset: 0x00030F5C
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040003FB RID: 1019
		private static readonly int IO_BUFFER_SIZE_DEFAULT = 65536;

		// Token: 0x040003FC RID: 1020
		private static readonly int BufferPairsPerCore = 4;

		// Token: 0x040003FD RID: 1021
		private List<WorkItem> _pool;

		// Token: 0x040003FE RID: 1022
		private bool _leaveOpen;

		// Token: 0x040003FF RID: 1023
		private bool emitting;

		// Token: 0x04000400 RID: 1024
		private Stream _outStream;

		// Token: 0x04000401 RID: 1025
		private int _maxBufferPairs;

		// Token: 0x04000402 RID: 1026
		private int _bufferSize = ParallelDeflateOutputStream.IO_BUFFER_SIZE_DEFAULT;

		// Token: 0x04000403 RID: 1027
		private AutoResetEvent _newlyCompressedBlob;

		// Token: 0x04000404 RID: 1028
		private object _outputLock = new object();

		// Token: 0x04000405 RID: 1029
		private bool _isClosed;

		// Token: 0x04000406 RID: 1030
		private bool _firstWriteDone;

		// Token: 0x04000407 RID: 1031
		private int _currentlyFilling;

		// Token: 0x04000408 RID: 1032
		private int _lastFilled;

		// Token: 0x04000409 RID: 1033
		private int _lastWritten;

		// Token: 0x0400040A RID: 1034
		private int _latestCompressed;

		// Token: 0x0400040B RID: 1035
		private int _Crc32;

		// Token: 0x0400040C RID: 1036
		private CRC32 _runningCrc;

		// Token: 0x0400040D RID: 1037
		private object _latestLock = new object();

		// Token: 0x0400040E RID: 1038
		private Queue<int> _toWrite;

		// Token: 0x0400040F RID: 1039
		private Queue<int> _toFill;

		// Token: 0x04000410 RID: 1040
		private long _totalBytesProcessed;

		// Token: 0x04000411 RID: 1041
		private CompressionLevel _compressLevel;

		// Token: 0x04000412 RID: 1042
		private volatile Exception _pendingException;

		// Token: 0x04000413 RID: 1043
		private bool _handlingException;

		// Token: 0x04000414 RID: 1044
		private object _eLock = new object();

		// Token: 0x04000415 RID: 1045
		private ParallelDeflateOutputStream.TraceBits _DesiredTrace = ParallelDeflateOutputStream.TraceBits.EmitLock | ParallelDeflateOutputStream.TraceBits.EmitEnter | ParallelDeflateOutputStream.TraceBits.EmitBegin | ParallelDeflateOutputStream.TraceBits.EmitDone | ParallelDeflateOutputStream.TraceBits.EmitSkip | ParallelDeflateOutputStream.TraceBits.Session | ParallelDeflateOutputStream.TraceBits.Compress | ParallelDeflateOutputStream.TraceBits.WriteEnter | ParallelDeflateOutputStream.TraceBits.WriteTake;

		// Token: 0x02000280 RID: 640
		[Flags]
		private enum TraceBits : uint
		{
			// Token: 0x04000B40 RID: 2880
			None = 0u,
			// Token: 0x04000B41 RID: 2881
			NotUsed1 = 1u,
			// Token: 0x04000B42 RID: 2882
			EmitLock = 2u,
			// Token: 0x04000B43 RID: 2883
			EmitEnter = 4u,
			// Token: 0x04000B44 RID: 2884
			EmitBegin = 8u,
			// Token: 0x04000B45 RID: 2885
			EmitDone = 16u,
			// Token: 0x04000B46 RID: 2886
			EmitSkip = 32u,
			// Token: 0x04000B47 RID: 2887
			EmitAll = 58u,
			// Token: 0x04000B48 RID: 2888
			Flush = 64u,
			// Token: 0x04000B49 RID: 2889
			Lifecycle = 128u,
			// Token: 0x04000B4A RID: 2890
			Session = 256u,
			// Token: 0x04000B4B RID: 2891
			Synch = 512u,
			// Token: 0x04000B4C RID: 2892
			Instance = 1024u,
			// Token: 0x04000B4D RID: 2893
			Compress = 2048u,
			// Token: 0x04000B4E RID: 2894
			Write = 4096u,
			// Token: 0x04000B4F RID: 2895
			WriteEnter = 8192u,
			// Token: 0x04000B50 RID: 2896
			WriteTake = 16384u,
			// Token: 0x04000B51 RID: 2897
			All = 4294967295u
		}
	}
}
