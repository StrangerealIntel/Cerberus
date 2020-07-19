using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000114 RID: 276
	internal class SessionHistoryRequest : RequestBase
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x0003D458 File Offset: 0x0003B658
		public SessionHistoryRequest() : base("usl")
		{
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0003D468 File Offset: 0x0003B668
		[JsonProperty("x")]
		public int LoadSessionIds
		{
			get
			{
				return 1;
			}
		}
	}
}
