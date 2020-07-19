using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Ionic.BZip2
{
	// Token: 0x020000BB RID: 187
	[ComVisible(true)]
	public class BZip2OutputStream : Stream
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x0002A944 File Offset: 0x00028B44
		public BZip2OutputStream(Stream output) : this(output, BZip2.MaxBlockSize, false)
		{
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0002A954 File Offset: 0x00028B54
		public BZip2OutputStream(Stream output, int blockSize) : this(output, blockSize, false)
		{
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0002A960 File Offset: 0x00028B60
		public BZip2OutputStream(Stream output, bool leaveOpen) : this(output, BZip2.MaxBlockSize, leaveOpen)
		{
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0002A970 File Offset: 0x00028B70
		public BZip2OutputStream(Stream output, int blockSize, bool leaveOpen)
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
			this.compressor = new BZip2Compressor(this.bw, blockSize);
			this.leaveOpen = leaveOpen;
			this.combinedCRC = 0u;
			this.EmitHeader();
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0002AA3C File Offset: 0x00028C3C
		public override void Close()
		{
			if (this.output != null)
			{
				Stream stream = this.output;
				this.Finish();
				if (!this.leaveOpen)
				{
					stream.Close();
				}
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0002AA78 File Offset: 0x00028C78
		public override void Flush()
		{
			if (this.output != null)
			{
				this.bw.Flush();
				this.output.Flush();
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0002AA9C File Offset: 0x00028C9C
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

		// Token: 0x06000618 RID: 1560 RVA: 0x0002AAE0 File Offset: 0x00028CE0
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

		// Token: 0x06000619 RID: 1561 RVA: 0x0002AB60 File Offset: 0x00028D60
		private void Finish()
		{
			try
			{
				int totalBytesWrittenOut = this.bw.TotalBytesWrittenOut;
				this.compressor.CompressAndWrite();
				this.combinedCRC = (this.combinedCRC << 1 | this.combinedCRC >> 31);
				this.combinedCRC ^= this.compressor.Crc32;
				this.EmitTrailer();
			}
			finally
			{
				this.output = null;
				this.compressor = null;
				this.bw = null;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0002ABE8 File Offset: 0x00028DE8
		public int BlockSize
		{
			get
			{
				return this.blockSize100k;
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0002ABF0 File Offset: 0x00028DF0
		public override void Write(byte[] buffer, int offset, int count)
		{
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
			if (this.output == null)
			{
				throw new IOException("the stream is not open");
			}
			if (count == 0)
			{
				return;
			}
			int num = 0;
			int num2 = count;
			do
			{
				int num3 = this.compressor.Fill(buffer, offset, num2);
				if (num3 != num2)
				{
					int totalBytesWrittenOut = this.bw.TotalBytesWrittenOut;
					this.compressor.CompressAndWrite();
					this.combinedCRC = (this.combinedCRC << 1 | this.combinedCRC >> 31);
					this.combinedCRC ^= this.compressor.Crc32;
					offset += num3;
				}
				num2 -= num3;
				num += num3;
			}
			while (num2 > 0);
			this.totalBytesWrittenIn += num;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0002AD08 File Offset: 0x00028F08
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0002AD0C File Offset: 0x00028F0C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0002AD10 File Offset: 0x00028F10
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

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0002AD34 File Offset: 0x00028F34
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0002AD3C File Offset: 0x00028F3C
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x0002AD48 File Offset: 0x00028F48
		public override long Position
		{
			get
			{
				return (long)this.totalBytesWrittenIn;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0002AD50 File Offset: 0x00028F50
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0002AD58 File Offset: 0x00028F58
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0002AD60 File Offset: 0x00028F60
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0002AD68 File Offset: 0x00028F68
		[Conditional("Trace")]
		private void TraceOutput(BZip2OutputStream.TraceBits bits, string format, params object[] varParams)
		{
			if ((bits & this.desiredTrace) != BZip2OutputStream.TraceBits.None)
			{
				int hashCode = Thread.CurrentThread.GetHashCode();
				Console.ForegroundColor = hashCode % 8 + ConsoleColor.Green;
				Console.Write("{0:000} PBOS ", hashCode);
				Console.WriteLine(format, varParams);
				Console.ResetColor();
			}
		}

		// Token: 0x04000318 RID: 792
		private int totalBytesWrittenIn;

		// Token: 0x04000319 RID: 793
		private bool leaveOpen;

		// Token: 0x0400031A RID: 794
		private BZip2Compressor compressor;

		// Token: 0x0400031B RID: 795
		private uint combinedCRC;

		// Token: 0x0400031C RID: 796
		private Stream output;

		// Token: 0x0400031D RID: 797
		private BitWriter bw;

		// Token: 0x0400031E RID: 798
		private int blockSize100k;

		// Token: 0x0400031F RID: 799
		private BZip2OutputStream.TraceBits desiredTrace = BZip2OutputStream.TraceBits.Crc | BZip2OutputStream.TraceBits.Write;

		// Token: 0x0200027A RID: 634
		[Flags]
		private enum TraceBits : uint
		{
			// Token: 0x04000B16 RID: 2838
			None = 0u,
			// Token: 0x04000B17 RID: 2839
			Crc = 1u,
			// Token: 0x04000B18 RID: 2840
			Write = 2u,
			// Token: 0x04000B19 RID: 2841
			All = 4294967295u
		}
	}
}
