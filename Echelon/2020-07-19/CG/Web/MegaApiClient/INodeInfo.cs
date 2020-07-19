using System;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000EB RID: 235
	public interface INodeInfo : IEquatable<INodeInfo>
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600082C RID: 2092
		string Id { get; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600082D RID: 2093
		NodeType Type { get; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600082E RID: 2094
		string Name { get; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600082F RID: 2095
		long Size { get; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000830 RID: 2096
		DateTime? ModificationDate { get; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000831 RID: 2097
		string SerializedFingerprint { get; }
	}
}
