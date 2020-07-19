using System;
using System.IO;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000F8 RID: 248
	internal class BufferedStream : Stream
	{
		// Token: 0x060008C1 RID: 2241 RVA: 0x0003BD2C File Offset: 0x00039F2C
		public BufferedStream(Stream innerStream)
		{
			this.innerStream = innerStream;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0003BD4C File Offset: 0x00039F4C
		public byte[] Buffer
		{
			get
			{
				return this.streamBuffer;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0003BD54 File Offset: 0x00039F54
		public int BufferOffset
		{
			get
			{
				return this.streamBufferDataStartIndex;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0003BD5C File Offset: 0x00039F5C
		public int AvailableCount
		{
			get
			{
				return this.streamBufferDataCount;
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0003BD64 File Offset: 0x00039F64
		public override void Flush()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0003BD6C File Offset: 0x00039F6C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0003BD74 File Offset: 0x00039F74
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0003BD7C File Offset: 0x00039F7C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			do
			{
				int num2 = Math.Min(this.streamBufferDataCount, count);
				if (num2 != 0)
				{
					Array.Copy(this.streamBuffer, this.streamBufferDataStartIndex, buffer, offset, num2);
					offset += num2;
					count -= num2;
					this.streamBufferDataStartIndex += num2;
					this.streamBufferDataCount -= num2;
					num += num2;
				}
				if (count == 0)
				{
					break;
				}
				this.streamBufferDataStartIndex = 0;
				this.streamBufferDataCount = 0;
				this.FillBuffer();
			}
			while (this.streamBufferDataCount != 0);
			return num;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0003BE04 File Offset: 0x0003A004
		public void FillBuffer()
		{
			for (;;)
			{
				int num = this.streamBufferDataStartIndex + this.streamBufferDataCount;
				int num2 = this.streamBuffer.Length - num;
				if (num2 == 0)
				{
					break;
				}
				int num3 = this.innerStream.Read(this.streamBuffer, num, num2);
				if (num3 == 0)
				{
					break;
				}
				this.streamBufferDataCount += num3;
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0003BE60 File Offset: 0x0003A060
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0003BE68 File Offset: 0x0003A068
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0003BE6C File Offset: 0x0003A06C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0003BE70 File Offset: 0x0003A070
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0003BE74 File Offset: 0x0003A074
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0003BE7C File Offset: 0x0003A07C
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0003BE84 File Offset: 0x0003A084
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0400051D RID: 1309
		private const int BufferSize = 65536;

		// Token: 0x0400051E RID: 1310
		private Stream innerStream;

		// Token: 0x0400051F RID: 1311
		private byte[] streamBuffer = new byte[65536];

		// Token: 0x04000520 RID: 1312
		private int streamBufferDataStartIndex;

		// Token: 0x04000521 RID: 1313
		private int streamBufferDataCount;
	}
}
