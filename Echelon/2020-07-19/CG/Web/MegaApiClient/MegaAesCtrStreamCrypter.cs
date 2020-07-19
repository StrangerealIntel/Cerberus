using System;
using System.IO;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000FA RID: 250
	internal class MegaAesCtrStreamCrypter : MegaAesCtrStream
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x0003C0C8 File Offset: 0x0003A2C8
		public MegaAesCtrStreamCrypter(Stream stream) : base(stream, stream.Length, MegaAesCtrStream.Mode.Crypt, Crypto.CreateAesKey(), Crypto.CreateAesKey().CopySubArray(8, 0))
		{
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0003C0F8 File Offset: 0x0003A2F8
		public byte[] FileKey
		{
			get
			{
				return this.fileKey;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0003C100 File Offset: 0x0003A300
		public byte[] Iv
		{
			get
			{
				return this.iv;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0003C108 File Offset: 0x0003A308
		public byte[] MetaMac
		{
			get
			{
				if (this.position != this.streamLength)
				{
					throw new NotSupportedException("Stream must be fully read to obtain computed FileMac");
				}
				return this.metaMac;
			}
		}
	}
}
