using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ionic.Crc
{
	// Token: 0x020000DB RID: 219
	[ComVisible(true)]
	public class CrcCalculatorStream : Stream, IDisposable
	{
		// Token: 0x06000763 RID: 1891 RVA: 0x000353C0 File Offset: 0x000335C0
		public CrcCalculatorStream(Stream stream) : this(true, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000353D0 File Offset: 0x000335D0
		public CrcCalculatorStream(Stream stream, bool leaveOpen) : this(leaveOpen, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000353E0 File Offset: 0x000335E0
		public CrcCalculatorStream(Stream stream, long length) : this(true, length, stream, null)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00035400 File Offset: 0x00033600
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen) : this(leaveOpen, length, stream, null)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00035420 File Offset: 0x00033620
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen, CRC32 crc32) : this(leaveOpen, length, stream, crc32)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00035440 File Offset: 0x00033640
		private CrcCalculatorStream(bool leaveOpen, long length, Stream stream, CRC32 crc32)
		{
			this._innerStream = stream;
			this._Crc32 = (crc32 ?? new CRC32());
			this._lengthLimit = length;
			this._leaveOpen = leaveOpen;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0003547C File Offset: 0x0003367C
		public long TotalBytesSlurped
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0003548C File Offset: 0x0003368C
		public int Crc
		{
			get
			{
				return this._Crc32.Crc32Result;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0003549C File Offset: 0x0003369C
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x000354A4 File Offset: 0x000336A4
		public bool LeaveOpen
		{
			get
			{
				return this._leaveOpen;
			}
			set
			{
				this._leaveOpen = value;
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000354B0 File Offset: 0x000336B0
		public override int Read(byte[] buffer, int offset, int count)
		{
			int count2 = count;
			if (this._lengthLimit != CrcCalculatorStream.UnsetLengthLimit)
			{
				if (this._Crc32.TotalBytesRead >= this._lengthLimit)
				{
					return 0;
				}
				long num = this._lengthLimit - this._Crc32.TotalBytesRead;
				if (num < (long)count)
				{
					count2 = (int)num;
				}
			}
			int num2 = this._innerStream.Read(buffer, offset, count2);
			if (num2 > 0)
			{
				this._Crc32.SlurpBlock(buffer, offset, num2);
			}
			return num2;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00035530 File Offset: 0x00033730
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count > 0)
			{
				this._Crc32.SlurpBlock(buffer, offset, count);
			}
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x00035558 File Offset: 0x00033758
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00035568 File Offset: 0x00033768
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0003556C File Offset: 0x0003376C
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0003557C File Offset: 0x0003377C
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0003558C File Offset: 0x0003378C
		public override long Length
		{
			get
			{
				if (this._lengthLimit == CrcCalculatorStream.UnsetLengthLimit)
				{
					return this._innerStream.Length;
				}
				return this._lengthLimit;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x000355B0 File Offset: 0x000337B0
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x000355C0 File Offset: 0x000337C0
		public override long Position
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000355C8 File Offset: 0x000337C8
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000355D0 File Offset: 0x000337D0
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000355D8 File Offset: 0x000337D8
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000355E0 File Offset: 0x000337E0
		public override void Close()
		{
			base.Close();
			if (!this._leaveOpen)
			{
				this._innerStream.Close();
			}
		}

		// Token: 0x0400048D RID: 1165
		private static readonly long UnsetLengthLimit = -99L;

		// Token: 0x0400048E RID: 1166
		internal Stream _innerStream;

		// Token: 0x0400048F RID: 1167
		private CRC32 _Crc32;

		// Token: 0x04000490 RID: 1168
		private long _lengthLimit = -99L;

		// Token: 0x04000491 RID: 1169
		private bool _leaveOpen;
	}
}
