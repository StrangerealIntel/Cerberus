using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Ionic.Zlib
{
	// Token: 0x020000C3 RID: 195
	[ComVisible(true)]
	public class GZipStream : Stream
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0002E3C0 File Offset: 0x0002C5C0
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x0002E3C8 File Offset: 0x0002C5C8
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._Comment = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0002E3E8 File Offset: 0x0002C5E8
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x0002E3F0 File Offset: 0x0002C5F0
		public string FileName
		{
			get
			{
				return this._FileName;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._FileName = value;
				if (this._FileName == null)
				{
					return;
				}
				if (this._FileName.IndexOf("/") != -1)
				{
					this._FileName = this._FileName.Replace("/", "\\");
				}
				if (this._FileName.EndsWith("\\"))
				{
					throw new Exception("Illegal filename");
				}
				if (this._FileName.IndexOf("\\") != -1)
				{
					this._FileName = Path.GetFileName(this._FileName);
				}
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0002E4A4 File Offset: 0x0002C6A4
		public int Crc32
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0002E4AC File Offset: 0x0002C6AC
		public GZipStream(Stream stream, CompressionMode mode) : this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0002E4B8 File Offset: 0x0002C6B8
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level) : this(stream, mode, level, false)
		{
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0002E4C4 File Offset: 0x0002C6C4
		public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen) : this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0002E4D0 File Offset: 0x0002C6D0
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.GZIP, leaveOpen);
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0002E4F0 File Offset: 0x0002C6F0
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x0002E500 File Offset: 0x0002C700
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
					throw new ObjectDisposedException("GZipStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0002E524 File Offset: 0x0002C724
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x0002E534 File Offset: 0x0002C734
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
					throw new ObjectDisposedException("GZipStream");
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

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0002E5B0 File Offset: 0x0002C7B0
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0002E5C4 File Offset: 0x0002C7C4
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0002E5D8 File Offset: 0x0002C7D8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
						this._Crc32 = this._baseStream.Crc32;
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0002E644 File Offset: 0x0002C844
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0002E66C File Offset: 0x0002C86C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0002E670 File Offset: 0x0002C870
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0002E698 File Offset: 0x0002C898
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0002E6BC File Offset: 0x0002C8BC
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0002E6C4 File Offset: 0x0002C8C4
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x0002E730 File Offset: 0x0002C930
		public override long Position
		{
			get
			{
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return this._baseStream._z.TotalBytesOut + (long)this._headerByteCount;
				}
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return this._baseStream._z.TotalBytesIn + (long)this._baseStream._gzipHeaderByteCount;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0002E738 File Offset: 0x0002C938
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			int result = this._baseStream.Read(buffer, offset, count);
			if (!this._firstReadDone)
			{
				this._firstReadDone = true;
				this.FileName = this._baseStream._GzipFileName;
				this.Comment = this._baseStream._GzipComment;
			}
			return result;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0002E7A4 File Offset: 0x0002C9A4
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0002E7AC File Offset: 0x0002C9AC
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0002E7B4 File Offset: 0x0002C9B4
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				if (!this._baseStream._wantCompress)
				{
					throw new InvalidOperationException();
				}
				this._headerByteCount = this.EmitHeader();
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0002E824 File Offset: 0x0002CA24
		private int EmitHeader()
		{
			byte[] array = (this.Comment == null) ? null : GZipStream.iso8859dash1.GetBytes(this.Comment);
			byte[] array2 = (this.FileName == null) ? null : GZipStream.iso8859dash1.GetBytes(this.FileName);
			int num = (this.Comment == null) ? 0 : (array.Length + 1);
			int num2 = (this.FileName == null) ? 0 : (array2.Length + 1);
			int num3 = 10 + num + num2;
			byte[] array3 = new byte[num3];
			int num4 = 0;
			array3[num4++] = 31;
			array3[num4++] = 139;
			array3[num4++] = 8;
			byte b = 0;
			if (this.Comment != null)
			{
				b ^= 16;
			}
			if (this.FileName != null)
			{
				b ^= 8;
			}
			array3[num4++] = b;
			if (this.LastModified == null)
			{
				this.LastModified = new DateTime?(DateTime.Now);
			}
			int value = (int)(this.LastModified.Value - GZipStream._unixEpoch).TotalSeconds;
			Array.Copy(BitConverter.GetBytes(value), 0, array3, num4, 4);
			num4 += 4;
			array3[num4++] = 0;
			array3[num4++] = byte.MaxValue;
			if (num2 != 0)
			{
				Array.Copy(array2, 0, array3, num4, num2 - 1);
				num4 += num2 - 1;
				array3[num4++] = 0;
			}
			if (num != 0)
			{
				Array.Copy(array, 0, array3, num4, num - 1);
				num4 += num - 1;
				array3[num4++] = 0;
			}
			this._baseStream._stream.Write(array3, 0, array3.Length);
			return array3.Length;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0002E9F4 File Offset: 0x0002CBF4
		public static byte[] CompressString(string s)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0002EA44 File Offset: 0x0002CC44
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream compressor = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, compressor);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0002EA94 File Offset: 0x0002CC94
		public static string UncompressString(byte[] compressed)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new GZipStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressString(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0002EADC File Offset: 0x0002CCDC
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream decompressor = new GZipStream(memoryStream, CompressionMode.Decompress);
				result = ZlibBaseStream.UncompressBuffer(compressed, decompressor);
			}
			return result;
		}

		// Token: 0x04000395 RID: 917
		public DateTime? LastModified;

		// Token: 0x04000396 RID: 918
		private int _headerByteCount;

		// Token: 0x04000397 RID: 919
		internal ZlibBaseStream _baseStream;

		// Token: 0x04000398 RID: 920
		private bool _disposed;

		// Token: 0x04000399 RID: 921
		private bool _firstReadDone;

		// Token: 0x0400039A RID: 922
		private string _FileName;

		// Token: 0x0400039B RID: 923
		private string _Comment;

		// Token: 0x0400039C RID: 924
		private int _Crc32;

		// Token: 0x0400039D RID: 925
		internal static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x0400039E RID: 926
		internal static readonly Encoding iso8859dash1 = Encoding.GetEncoding("iso-8859-1");
	}
}
