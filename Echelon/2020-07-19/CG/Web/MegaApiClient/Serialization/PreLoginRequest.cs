using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000110 RID: 272
	internal class PreLoginRequest : RequestBase
	{
		// Token: 0x0600096A RID: 2410 RVA: 0x0003D370 File Offset: 0x0003B570
		public PreLoginRequest(string userHandle) : base("us0")
		{
			this.UserHandle = userHandle;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0003D384 File Offset: 0x0003B584
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x0003D38C File Offset: 0x0003B58C
		[JsonProperty("user")]
		public string UserHandle { get; private set; }
	}
}
