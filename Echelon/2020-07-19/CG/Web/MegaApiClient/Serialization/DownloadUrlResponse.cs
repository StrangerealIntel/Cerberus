using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000106 RID: 262
	internal class DownloadUrlResponse
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0003CEA8 File Offset: 0x0003B0A8
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x0003CEB0 File Offset: 0x0003B0B0
		[JsonProperty("g")]
		public string Url { get; private set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0003CEBC File Offset: 0x0003B0BC
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x0003CEC4 File Offset: 0x0003B0C4
		[JsonProperty("s")]
		public long Size { get; private set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0003CED0 File Offset: 0x0003B0D0
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x0003CED8 File Offset: 0x0003B0D8
		[JsonProperty("at")]
		public string SerializedAttributes { get; set; }
	}
}
