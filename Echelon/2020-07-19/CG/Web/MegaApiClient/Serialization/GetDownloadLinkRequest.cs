using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000107 RID: 263
	internal class GetDownloadLinkRequest : RequestBase
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x0003CEEC File Offset: 0x0003B0EC
		public GetDownloadLinkRequest(INode node) : base("l")
		{
			this.Id = node.Id;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0003CF08 File Offset: 0x0003B108
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x0003CF10 File Offset: 0x0003B110
		[JsonProperty("n")]
		public string Id { get; private set; }
	}
}
