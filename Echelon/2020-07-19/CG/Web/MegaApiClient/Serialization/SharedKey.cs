using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000118 RID: 280
	[DebuggerDisplay("Id: {Id} / Key: {Key}")]
	internal class SharedKey
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x0003D628 File Offset: 0x0003B828
		public SharedKey(string id, string key)
		{
			this.Id = id;
			this.Key = key;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0003D640 File Offset: 0x0003B840
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x0003D648 File Offset: 0x0003B848
		[JsonProperty("h")]
		public string Id { get; private set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0003D654 File Offset: 0x0003B854
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x0003D65C File Offset: 0x0003B85C
		[JsonProperty("k")]
		public string Key { get; private set; }
	}
}
