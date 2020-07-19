using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x0200009D RID: 157
	internal class OffsetStream : Stream, IDisposable
	{
		// Token: 0x0600032C RID: 812 RVA: 0x00017F6C File Offset: 0x0001616C
		public OffsetStream(Stream s)
		{
			this._originalPosition = s.Position;
			this._innerStream = s;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00017F88 File Offset: 0x00016188
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._innerStream.Read(buffer, offset, count);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00017F98 File Offset: 0x00016198
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00017FA0 File Offset: 0x000161A0
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00017FB0 File Offset: 0x000161B0
		public override bool CanSeek
		{
			get
			{
				return this._innerStream.CanSeek;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00017FC0 File Offset: 0x000161C0
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00017FC4 File Offset: 0x000161C4
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00017FD4 File Offset: 0x000161D4
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00017FE4 File Offset: 0x000161E4
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00017FF8 File Offset: 0x000161F8
		public override long Position
		{
			get
			{
				return this._innerStream.Position - this._originalPosition;
			}
			set
			{
				this._innerStream.Position = this._originalPosition + value;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00018010 File Offset: 0x00016210
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(this._originalPosition + offset, origin) - this._originalPosition;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00018030 File Offset: 0x00016230
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00018038 File Offset: 0x00016238
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00018040 File Offset: 0x00016240
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x0400018F RID: 399
		private long _originalPosition;

		// Token: 0x04000190 RID: 400
		private Stream _innerStream;
	}
}
