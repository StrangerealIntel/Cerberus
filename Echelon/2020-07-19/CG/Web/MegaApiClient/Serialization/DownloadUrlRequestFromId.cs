using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000105 RID: 261
	internal class DownloadUrlRequestFromId : RequestBase
	{
		// Token: 0x0600092E RID: 2350 RVA: 0x0003CE7C File Offset: 0x0003B07C
		public DownloadUrlRequestFromId(string id) : base("g")
		{
			this.Id = id;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0003CE90 File Offset: 0x0003B090
		public int g
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x0003CE94 File Offset: 0x0003B094
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x0003CE9C File Offset: 0x0003B09C
		[JsonProperty("p")]
		public string Id { get; private set; }
	}
}
