using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x020000D9 RID: 217
	[ComVisible(true)]
	public class ZlibStream : Stream
	{
		// Token: 0x06000736 RID: 1846 RVA: 0x00034A80 File Offset: 0x00032C80
		public ZlibStream(Stream stream, CompressionMode mode) : this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00034A8C File Offset: 0x00032C8C
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level) : this(stream, mode, level, false)
		{
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00034A98 File Offset: 0x00032C98
		public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen) : this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00034AA4 File Offset: 0x00032CA4
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.ZLIB, leaveOpen);
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00034AC4 File Offset: 0x00032CC4
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x00034AD4 File Offset: 0x00032CD4
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
					throw new ObjectDisposedException("ZlibStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00034AF8 File Offset: 0x00032CF8
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x00034B08 File Offset: 0x00032D08
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
					throw new ObjectDisposedException("ZlibStream");
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

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x00034B84 File Offset: 0x00032D84
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00034B98 File Offset: 0x00032D98
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00034BAC File Offset: 0x00032DAC
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

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x00034C04 File Offset: 0x00032E04
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x00034C2C File Offset: 0x00032E2C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00034C30 File Offset: 0x00032E30
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00034C58 File Offset: 0x00032E58
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00034C7C File Offset: 0x00032E7C
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00034C84 File Offset: 0x00032E84
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x00034CDC File Offset: 0x00032EDC
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
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00034CE4 File Offset: 0x00032EE4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00034D0C File Offset: 0x00032F0C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00034D14 File Offset: 0x00032F14
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00034D1C File Offset: 0x00032F1C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00034D44 File Offset: 0x00032F44
		public static byte[] CompressString(string s)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00034D94 File Offset: 0x00032F94
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00034DE4 File Offset: 0x00032FE4
		public static string UncompressString(byte[] compressed)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new ZlibStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressString(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00034E2C File Offset: 0x0003302C
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new ZlibStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressBuffer(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x04000485 RID: 1157
		internal ZlibBaseStream _baseStream;

		// Token: 0x04000486 RID: 1158
		private bool _disposed;
	}
}
