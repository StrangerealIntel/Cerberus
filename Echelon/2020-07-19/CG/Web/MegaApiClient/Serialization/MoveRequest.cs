using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x0200010E RID: 270
	internal class MoveRequest : RequestBase
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x0003D244 File Offset: 0x0003B444
		public MoveRequest(INode node, INode destinationParentNode) : base("m")
		{
			this.Id = node.Id;
			this.DestinationParentId = destinationParentNode.Id;
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0003D278 File Offset: 0x0003B478
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x0003D280 File Offset: 0x0003B480
		[JsonProperty("n")]
		public string Id { get; private set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0003D28C File Offset: 0x0003B48C
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x0003D294 File Offset: 0x0003B494
		[JsonProperty("t")]
		public string DestinationParentId { get; private set; }
	}
}
