using System;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000E9 RID: 233
	public interface IStorageMetrics
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600080B RID: 2059
		string NodeId { get; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600080C RID: 2060
		long BytesUsed { get; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600080D RID: 2061
		long FilesCount { get; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600080E RID: 2062
		long FoldersCount { get; }
	}
}
