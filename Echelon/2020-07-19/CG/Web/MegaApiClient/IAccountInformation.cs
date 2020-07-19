using System;
using System.Collections.Generic;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000E8 RID: 232
	public interface IAccountInformation
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000808 RID: 2056
		long TotalQuota { get; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000809 RID: 2057
		long UsedQuota { get; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600080A RID: 2058
		IEnumerable<IStorageMetrics> Metrics { get; }
	}
}
