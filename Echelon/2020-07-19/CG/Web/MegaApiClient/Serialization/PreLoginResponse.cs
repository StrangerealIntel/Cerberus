using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000111 RID: 273
	internal class PreLoginResponse
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0003D398 File Offset: 0x0003B598
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x0003D3A0 File Offset: 0x0003B5A0
		[JsonProperty("s")]
		public string Salt { get; private set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0003D3AC File Offset: 0x0003B5AC
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x0003D3B4 File Offset: 0x0003B5B4
		[JsonProperty("v")]
		public int Version { get; private set; }
	}
}
