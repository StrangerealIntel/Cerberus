using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000104 RID: 260
	internal class DownloadUrlRequest : RequestBase
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x0003CE18 File Offset: 0x0003B018
		public DownloadUrlRequest(INode node) : base("g")
		{
			this.Id = node.Id;
			PublicNode publicNode = node as PublicNode;
			if (publicNode != null)
			{
				base.QueryArguments["n"] = publicNode.ShareId;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0003CE64 File Offset: 0x0003B064
		public int g
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0003CE68 File Offset: 0x0003B068
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x0003CE70 File Offset: 0x0003B070
		[JsonProperty("n")]
		public string Id { get; private set; }
	}
}
