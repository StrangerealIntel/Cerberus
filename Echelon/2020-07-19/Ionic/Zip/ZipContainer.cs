using System;
using System.IO;
using System.Text;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x020000B5 RID: 181
	internal class ZipContainer
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x0002645C File Offset: 0x0002465C
		public ZipContainer(object o)
		{
			this._zf = (o as ZipFile);
			this._zos = (o as ZipOutputStream);
			this._zis = (o as ZipInputStream);
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00026488 File Offset: 0x00024688
		public ZipFile ZipFile
		{
			get
			{
				return this._zf;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00026490 File Offset: 0x00024690
		public ZipOutputStream ZipOutputStream
		{
			get
			{
				return this._zos;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00026498 File Offset: 0x00024698
		public string Name
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.Name;
				}
				if (this._zis != null)
				{
					throw new NotSupportedException();
				}
				return this._zos.Name;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x000264D0 File Offset: 0x000246D0
		public string Password
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf._Password;
				}
				if (this._zis != null)
				{
					return this._zis._Password;
				}
				return this._zos._password;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0002650C File Offset: 0x0002470C
		public Zip64Option Zip64
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf._zip64;
				}
				if (this._zis != null)
				{
					throw new NotSupportedException();
				}
				return this._zos._zip64;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00026544 File Offset: 0x00024744
		public int BufferSize
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.BufferSize;
				}
				if (this._zis != null)
				{
					throw new NotSupportedException();
				}
				return 0;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00026570 File Offset: 0x00024770
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x000265A4 File Offset: 0x000247A4
		public ParallelDeflateOutputStream ParallelDeflater
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.ParallelDeflater;
				}
				if (this._zis != null)
				{
					return null;
				}
				return this._zos.ParallelDeflater;
			}
			set
			{
				if (this._zf != null)
				{
					this._zf.ParallelDeflater = value;
					return;
				}
				if (this._zos != null)
				{
					this._zos.ParallelDeflater = value;
				}
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x000265D8 File Offset: 0x000247D8
		public long ParallelDeflateThreshold
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.ParallelDeflateThreshold;
				}
				return this._zos.ParallelDeflateThreshold;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x000265FC File Offset: 0x000247FC
		public int ParallelDeflateMaxBufferPairs
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.ParallelDeflateMaxBufferPairs;
				}
				return this._zos.ParallelDeflateMaxBufferPairs;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00026620 File Offset: 0x00024820
		public int CodecBufferSize
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.CodecBufferSize;
				}
				if (this._zis != null)
				{
					return this._zis.CodecBufferSize;
				}
				return this._zos.CodecBufferSize;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0002665C File Offset: 0x0002485C
		public CompressionStrategy Strategy
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.Strategy;
				}
				return this._zos.Strategy;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00026680 File Offset: 0x00024880
		public Zip64Option UseZip64WhenSaving
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.UseZip64WhenSaving;
				}
				return this._zos.EnableZip64;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x000266A4 File Offset: 0x000248A4
		public Encoding AlternateEncoding
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.AlternateEncoding;
				}
				if (this._zos != null)
				{
					return this._zos.AlternateEncoding;
				}
				return null;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x000266D8 File Offset: 0x000248D8
		public Encoding DefaultEncoding
		{
			get
			{
				if (this._zf != null)
				{
					return ZipFile.DefaultEncoding;
				}
				if (this._zos != null)
				{
					return ZipOutputStream.DefaultEncoding;
				}
				return null;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00026700 File Offset: 0x00024900
		public ZipOption AlternateEncodingUsage
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.AlternateEncodingUsage;
				}
				if (this._zos != null)
				{
					return this._zos.AlternateEncodingUsage;
				}
				return ZipOption.Default;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00026734 File Offset: 0x00024934
		public Stream ReadStream
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.ReadStream;
				}
				return this._zis.ReadStream;
			}
		}

		// Token: 0x040002C3 RID: 707
		private ZipFile _zf;

		// Token: 0x040002C4 RID: 708
		private ZipOutputStream _zos;

		// Token: 0x040002C5 RID: 709
		private ZipInputStream _zis;
	}
}
