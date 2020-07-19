using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000103 RID: 259
	internal class DeleteRequest : RequestBase
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x0003CDE8 File Offset: 0x0003AFE8
		public DeleteRequest(INode node) : base("d")
		{
			this.Node = node.Id;
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0003CE04 File Offset: 0x0003B004
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0003CE0C File Offset: 0x0003B00C
		[JsonProperty("n")]
		public string Node { get; private set; }
	}
}
