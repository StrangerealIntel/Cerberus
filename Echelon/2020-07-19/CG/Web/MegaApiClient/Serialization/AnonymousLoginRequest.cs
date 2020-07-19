using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000100 RID: 256
	internal class AnonymousLoginRequest : RequestBase
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x0003C910 File Offset: 0x0003AB10
		public AnonymousLoginRequest(string masterKey, string temporarySession) : base("up")
		{
			this.MasterKey = masterKey;
			this.TemporarySession = temporarySession;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0003C92C File Offset: 0x0003AB2C
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x0003C934 File Offset: 0x0003AB34
		[JsonProperty("k")]
		public string MasterKey { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0003C940 File Offset: 0x0003AB40
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x0003C948 File Offset: 0x0003AB48
		[JsonProperty("ts")]
		public string TemporarySession { get; set; }
	}
}
