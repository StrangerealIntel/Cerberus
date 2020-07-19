using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x020000C2 RID: 194
	[ComVisible(true)]
	public class DeflateStream : Stream
	{
		// Token: 0x06000670 RID: 1648 RVA: 0x0002DF94 File Offset: 0x0002C194
		public DeflateStream(Stream stream, CompressionMode mode) : this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0002DFA0 File Offset: 0x0002C1A0
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level) : this(stream, mode, level, false)
		{
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0002DFAC File Offset: 0x0002C1AC
		public DeflateStream(Stream stream, CompressionMode mode, bool leaveOpen) : this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0002DFB8 File Offset: 0x0002C1B8
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._innerStream = stream;
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.DEFLATE, leaveOpen);
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0002DFDC File Offset: 0x0002C1DC
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x0002DFEC File Offset: 0x0002C1EC
		public virtual FlushType FlushMode
		{
			get
			{
				return this._baseStream._flushMode;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0002E010 File Offset: 0x0002C210
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x0002E020 File Offset: 0x0002C220
		public int BufferSize
		{
			get
			{
				return this._baseStream._bufferSize;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				if (this._baseStream._workingBuffer != null)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				if (value < 1024)
				{
					throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer, at least {1}.", value, 1024));
				}
				this._baseStream._bufferSize = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0002E09C File Offset: 0x0002C29C
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0002E0AC File Offset: 0x0002C2AC
		public CompressionStrategy Strategy
		{
			get
			{
				return this._baseStream.Strategy;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream.Strategy = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0002E0D0 File Offset: 0x0002C2D0
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0002E0E4 File Offset: 0x0002C2E4
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0002E0F8 File Offset: 0x0002C2F8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0002E150 File Offset: 0x0002C350
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0002E178 File Offset: 0x0002C378
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0002E17C File Offset: 0x0002C37C
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0002E1A4 File Offset: 0x0002C3A4
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0002E1C8 File Offset: 0x0002C3C8
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0002E1D0 File Offset: 0x0002C3D0
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x0002E228 File Offset: 0x0002C428
		public override long Position
		{
			get
			{
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return this._baseStream._z.TotalBytesOut;
				}
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return this._baseStream._z.TotalBytesIn;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0002E230 File Offset: 0x0002C430
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0002E258 File Offset: 0x0002C458
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0002E260 File Offset: 0x0002C460
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0002E268 File Offset: 0x0002C468
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0002E290 File Offset: 0x0002C490
		public static byte[] CompressString(string s)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0002E2E0 File Offset: 0x0002C4E0
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0002E330 File Offset: 0x0002C530
		public static string UncompressString(byte[] compressed)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new DeflateStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressString(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0002E378 File Offset: 0x0002C578
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new DeflateStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressBuffer(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x04000392 RID: 914
		internal ZlibBaseStream _baseStream;

		// Token: 0x04000393 RID: 915
		internal Stream _innerStream;

		// Token: 0x04000394 RID: 916
		private bool _disposed;
	}
}
