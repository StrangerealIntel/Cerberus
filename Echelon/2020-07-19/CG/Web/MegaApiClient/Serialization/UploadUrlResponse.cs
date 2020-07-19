using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x0200011B RID: 283
	internal class UploadUrlResponse
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0003D83C File Offset: 0x0003BA3C
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x0003D844 File Offset: 0x0003BA44
		[JsonProperty("p")]
		public string Url { get; private set; }
	}
}
