using System;
using System.IO;
using System.Linq;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000FB RID: 251
	internal class MegaAesCtrStreamDecrypter : MegaAesCtrStream
	{
		// Token: 0x060008E2 RID: 2274 RVA: 0x0003C12C File Offset: 0x0003A32C
		public MegaAesCtrStreamDecrypter(Stream stream, long streamLength, byte[] fileKey, byte[] iv, byte[] expectedMetaMac) : base(stream, streamLength, MegaAesCtrStream.Mode.Decrypt, fileKey, iv)
		{
			if (expectedMetaMac == null || expectedMetaMac.Length != 8)
			{
				throw new ArgumentException("Invalid expectedMetaMac");
			}
			this.expectedMetaMac = expectedMetaMac;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0003C160 File Offset: 0x0003A360
		protected override void OnStreamRead()
		{
			if (!this.expectedMetaMac.SequenceEqual(this.metaMac))
			{
				throw new DownloadException();
			}
		}

		// Token: 0x04000524 RID: 1316
		private readonly byte[] expectedMetaMac;
	}
}
