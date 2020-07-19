using System;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000108 RID: 264
	internal class GetNodesRequest : RequestBase
	{
		// Token: 0x0600093C RID: 2364 RVA: 0x0003CF1C File Offset: 0x0003B11C
		public GetNodesRequest(string shareId = null) : base("f")
		{
			this.c = 1;
			if (shareId != null)
			{
				base.QueryArguments["n"] = shareId;
				this.r = 1;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0003CF60 File Offset: 0x0003B160
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x0003CF68 File Offset: 0x0003B168
		public int c { get; private set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0003CF74 File Offset: 0x0003B174
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x0003CF7C File Offset: 0x0003B17C
		public int r { get; private set; }
	}
}
