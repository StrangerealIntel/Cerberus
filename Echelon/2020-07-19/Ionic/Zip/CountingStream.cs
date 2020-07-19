using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x0200009F RID: 159
	[ComVisible(true)]
	public class CountingStream : Stream
	{
		// Token: 0x0600034F RID: 847 RVA: 0x00018960 File Offset: 0x00016B60
		public CountingStream(Stream stream)
		{
			this._s = stream;
			try
			{
				this._initialOffset = this._s.Position;
			}
			catch
			{
				this._initialOffset = 0L;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000189B0 File Offset: 0x00016BB0
		public Stream WrappedStream
		{
			get
			{
				return this._s;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000351 RID: 849 RVA: 0x000189B8 File Offset: 0x00016BB8
		public long BytesWritten
		{
			get
			{
				return this._bytesWritten;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000352 RID: 850 RVA: 0x000189C0 File Offset: 0x00016BC0
		public long BytesRead
		{
			get
			{
				return this._bytesRead;
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000189C8 File Offset: 0x00016BC8
		public void Adjust(long delta)
		{
			this._bytesWritten -= delta;
			if (this._bytesWritten < 0L)
			{
				throw new InvalidOperationException();
			}
			if (this._s is CountingStream)
			{
				((CountingStream)this._s).Adjust(delta);
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00018A1C File Offset: 0x00016C1C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this._s.Read(buffer, offset, count);
			this._bytesRead += (long)num;
			return num;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00018A4C File Offset: 0x00016C4C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			this._s.Write(buffer, offset, count);
			this._bytesWritten += (long)count;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00018A74 File Offset: 0x00016C74
		public override bool CanRead
		{
			get
			{
				return this._s.CanRead;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00018A84 File Offset: 0x00016C84
		public override bool CanSeek
		{
			get
			{
				return this._s.CanSeek;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00018A94 File Offset: 0x00016C94
		public override bool CanWrite
		{
			get
			{
				return this._s.CanWrite;
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00018AA4 File Offset: 0x00016CA4
		public override void Flush()
		{
			this._s.Flush();
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00018AB4 File Offset: 0x00016CB4
		public override long Length
		{
			get
			{
				return this._s.Length;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00018AC4 File Offset: 0x00016CC4
		public long ComputedPosition
		{
			get
			{
				return this._initialOffset + this._bytesWritten;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00018AD4 File Offset: 0x00016CD4
		// (set) Token: 0x0600035D RID: 861 RVA: 0x00018AE4 File Offset: 0x00016CE4
		public override long Position
		{
			get
			{
				return this._s.Position;
			}
			set
			{
				this._s.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00018AF4 File Offset: 0x00016CF4
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._s.Seek(offset, origin);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00018B04 File Offset: 0x00016D04
		public override void SetLength(long value)
		{
			this._s.SetLength(value);
		}

		// Token: 0x04000194 RID: 404
		private Stream _s;

		// Token: 0x04000195 RID: 405
		private long _bytesWritten;

		// Token: 0x04000196 RID: 406
		private long _bytesRead;

		// Token: 0x04000197 RID: 407
		private long _initialOffset;
	}
}
