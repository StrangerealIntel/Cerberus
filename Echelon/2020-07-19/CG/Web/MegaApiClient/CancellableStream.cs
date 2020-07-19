using System;
using System.IO;
using System.Threading;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000F9 RID: 249
	public class CancellableStream : Stream
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x0003BE8C File Offset: 0x0003A08C
		public CancellableStream(Stream stream, CancellationToken cancellationToken)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.stream = stream;
			this.cancellationToken = cancellationToken;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0003BEB4 File Offset: 0x0003A0B4
		public override bool CanRead
		{
			get
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				return this.stream.CanRead;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0003BEE0 File Offset: 0x0003A0E0
		public override bool CanSeek
		{
			get
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				return this.stream.CanSeek;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0003BF0C File Offset: 0x0003A10C
		public override bool CanWrite
		{
			get
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				return this.stream.CanWrite;
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0003BF38 File Offset: 0x0003A138
		public override void Flush()
		{
			this.cancellationToken.ThrowIfCancellationRequested();
			this.stream.Flush();
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0003BF64 File Offset: 0x0003A164
		public override long Length
		{
			get
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				return this.stream.Length;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0003BF90 File Offset: 0x0003A190
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x0003BFBC File Offset: 0x0003A1BC
		public override long Position
		{
			get
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				return this.stream.Position;
			}
			set
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				this.stream.Position = value;
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0003BFE8 File Offset: 0x0003A1E8
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.cancellationToken.ThrowIfCancellationRequested();
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0003C018 File Offset: 0x0003A218
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.cancellationToken.ThrowIfCancellationRequested();
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0003C044 File Offset: 0x0003A244
		public override void SetLength(long value)
		{
			this.cancellationToken.ThrowIfCancellationRequested();
			this.stream.SetLength(value);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0003C070 File Offset: 0x0003A270
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.cancellationToken.ThrowIfCancellationRequested();
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0003C0A0 File Offset: 0x0003A2A0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Stream stream = this.stream;
				if (stream != null)
				{
					stream.Dispose();
				}
				this.stream = null;
			}
		}

		// Token: 0x04000522 RID: 1314
		private Stream stream;

		// Token: 0x04000523 RID: 1315
		private readonly CancellationToken cancellationToken;
	}
}
