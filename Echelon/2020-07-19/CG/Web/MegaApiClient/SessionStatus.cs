using System;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000F0 RID: 240
	[Flags]
	public enum SessionStatus
	{
		// Token: 0x040004EC RID: 1260
		Undefined = 0,
		// Token: 0x040004ED RID: 1261
		Current = 1,
		// Token: 0x040004EE RID: 1262
		Active = 2,
		// Token: 0x040004EF RID: 1263
		Expired = 4
	}
}
