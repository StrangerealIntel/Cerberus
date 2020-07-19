using System;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000EC RID: 236
	public interface INode : INodeInfo, IEquatable<INodeInfo>
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000832 RID: 2098
		string ParentId { get; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000833 RID: 2099
		DateTime CreationDate { get; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000834 RID: 2100
		string Owner { get; }
	}
}
