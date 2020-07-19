using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x0200010C RID: 268
	internal class LoginResponse
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0003D1DC File Offset: 0x0003B3DC
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x0003D1E4 File Offset: 0x0003B3E4
		[JsonProperty("csid")]
		public string SessionId { get; private set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0003D1F0 File Offset: 0x0003B3F0
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x0003D1F8 File Offset: 0x0003B3F8
		[JsonProperty("tsid")]
		public string TemporarySessionId { get; private set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0003D204 File Offset: 0x0003B404
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x0003D20C File Offset: 0x0003B40C
		[JsonProperty("privk")]
		public string PrivateKey { get; private set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0003D218 File Offset: 0x0003B418
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x0003D220 File Offset: 0x0003B420
		[JsonProperty("k")]
		public string MasterKey { get; private set; }
	}
}
