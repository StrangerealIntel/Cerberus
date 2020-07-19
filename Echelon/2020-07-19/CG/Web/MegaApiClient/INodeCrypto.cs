using System;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000ED RID: 237
	internal interface INodeCrypto
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000835 RID: 2101
		byte[] Key { get; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000836 RID: 2102
		byte[] SharedKey { get; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000837 RID: 2103
		byte[] Iv { get; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000838 RID: 2104
		byte[] MetaMac { get; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000839 RID: 2105
		byte[] FullKey { get; }
	}
}
