using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000112 RID: 274
	internal class RenameRequest : RequestBase
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x0003D3C8 File Offset: 0x0003B5C8
		public RenameRequest(INode node, string attributes) : base("a")
		{
			this.Id = node.Id;
			this.SerializedAttributes = attributes;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0003D3F8 File Offset: 0x0003B5F8
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x0003D400 File Offset: 0x0003B600
		[JsonProperty("n")]
		public string Id { get; private set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0003D40C File Offset: 0x0003B60C
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x0003D414 File Offset: 0x0003B614
		[JsonProperty("attr")]
		public string SerializedAttributes { get; set; }
	}
}
