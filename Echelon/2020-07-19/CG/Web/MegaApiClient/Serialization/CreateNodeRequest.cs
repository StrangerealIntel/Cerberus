using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000102 RID: 258
	internal class CreateNodeRequest : RequestBase
	{
		// Token: 0x0600091E RID: 2334 RVA: 0x0003CCE4 File Offset: 0x0003AEE4
		private CreateNodeRequest(INode parentNode, NodeType type, string attributes, string encryptedKey, byte[] key, string completionHandle) : base("p")
		{
			this.ParentId = parentNode.Id;
			this.Nodes = new CreateNodeRequest.CreateNodeRequestData[]
			{
				new CreateNodeRequest.CreateNodeRequestData
				{
					Attributes = attributes,
					Key = encryptedKey,
					Type = type,
					CompletionHandle = completionHandle
				}
			};
			INodeCrypto nodeCrypto = parentNode as INodeCrypto;
			if (nodeCrypto == null)
			{
				throw new ArgumentException("parentNode node must implement INodeCrypto");
			}
			if (nodeCrypto.SharedKey != null)
			{
				this.Share = new ShareData(parentNode.Id);
				this.Share.AddItem(completionHandle, key, nodeCrypto.SharedKey);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0003CD88 File Offset: 0x0003AF88
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x0003CD90 File Offset: 0x0003AF90
		[JsonProperty("t")]
		public string ParentId { get; private set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0003CD9C File Offset: 0x0003AF9C
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x0003CDA4 File Offset: 0x0003AFA4
		[JsonProperty("cr")]
		public ShareData Share { get; private set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0003CDB0 File Offset: 0x0003AFB0
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x0003CDB8 File Offset: 0x0003AFB8
		[JsonProperty("n")]
		public CreateNodeRequest.CreateNodeRequestData[] Nodes { get; private set; }

		// Token: 0x06000925 RID: 2341 RVA: 0x0003CDC4 File Offset: 0x0003AFC4
		public static CreateNodeRequest CreateFileNodeRequest(INode parentNode, string attributes, string encryptedkey, byte[] fileKey, string completionHandle)
		{
			return new CreateNodeRequest(parentNode, NodeType.File, attributes, encryptedkey, fileKey, completionHandle);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0003CDD4 File Offset: 0x0003AFD4
		public static CreateNodeRequest CreateFolderNodeRequest(INode parentNode, string attributes, string encryptedkey, byte[] key)
		{
			return new CreateNodeRequest(parentNode, NodeType.Directory, attributes, encryptedkey, key, "xxxxxxxx");
		}

		// Token: 0x020002A3 RID: 675
		internal class CreateNodeRequestData
		{
			// Token: 0x170004C3 RID: 1219
			// (get) Token: 0x06001750 RID: 5968 RVA: 0x00077EC0 File Offset: 0x000760C0
			// (set) Token: 0x06001751 RID: 5969 RVA: 0x00077EC8 File Offset: 0x000760C8
			[JsonProperty("h")]
			public string CompletionHandle { get; set; }

			// Token: 0x170004C4 RID: 1220
			// (get) Token: 0x06001752 RID: 5970 RVA: 0x00077ED4 File Offset: 0x000760D4
			// (set) Token: 0x06001753 RID: 5971 RVA: 0x00077EDC File Offset: 0x000760DC
			[JsonProperty("t")]
			public NodeType Type { get; set; }

			// Token: 0x170004C5 RID: 1221
			// (get) Token: 0x06001754 RID: 5972 RVA: 0x00077EE8 File Offset: 0x000760E8
			// (set) Token: 0x06001755 RID: 5973 RVA: 0x00077EF0 File Offset: 0x000760F0
			[JsonProperty("a")]
			public string Attributes { get; set; }

			// Token: 0x170004C6 RID: 1222
			// (get) Token: 0x06001756 RID: 5974 RVA: 0x00077EFC File Offset: 0x000760FC
			// (set) Token: 0x06001757 RID: 5975 RVA: 0x00077F04 File Offset: 0x00076104
			[JsonProperty("k")]
			public string Key { get; set; }
		}
	}
}
