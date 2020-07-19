using System;
using System.Diagnostics;
using CG.Web.MegaApiClient.Serialization;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000F3 RID: 243
	[DebuggerDisplay("NodeInfo - Type: {Type} - Name: {Name} - Id: {Id}")]
	internal class NodeInfo : INodeInfo, IEquatable<INodeInfo>
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x0003B568 File Offset: 0x00039768
		protected NodeInfo()
		{
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0003B570 File Offset: 0x00039770
		internal NodeInfo(string id, DownloadUrlResponse downloadResponse, byte[] key)
		{
			this.Id = id;
			this.Attributes = Crypto.DecryptAttributes(downloadResponse.SerializedAttributes.FromBase64(), key);
			this.Size = downloadResponse.Size;
			this.Type = NodeType.File;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0003B5B8 File Offset: 0x000397B8
		[JsonIgnore]
		public string Name
		{
			get
			{
				Attributes attributes = this.Attributes;
				if (attributes == null)
				{
					return null;
				}
				return attributes.Name;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0003B5D0 File Offset: 0x000397D0
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x0003B5D8 File Offset: 0x000397D8
		[JsonProperty("s")]
		public long Size { get; protected set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0003B5E4 File Offset: 0x000397E4
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x0003B5EC File Offset: 0x000397EC
		[JsonProperty("t")]
		public NodeType Type { get; protected set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0003B5F8 File Offset: 0x000397F8
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x0003B600 File Offset: 0x00039800
		[JsonProperty("h")]
		public string Id { get; private set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0003B60C File Offset: 0x0003980C
		[JsonIgnore]
		public DateTime? ModificationDate
		{
			get
			{
				Attributes attributes = this.Attributes;
				if (attributes == null)
				{
					return null;
				}
				return attributes.ModificationDate;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x0003B63C File Offset: 0x0003983C
		[JsonIgnore]
		public string SerializedFingerprint
		{
			get
			{
				Attributes attributes = this.Attributes;
				if (attributes == null)
				{
					return null;
				}
				return attributes.SerializedFingerprint;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0003B654 File Offset: 0x00039854
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x0003B65C File Offset: 0x0003985C
		[JsonIgnore]
		public Attributes Attributes { get; protected set; }

		// Token: 0x06000883 RID: 2179 RVA: 0x0003B668 File Offset: 0x00039868
		public bool Equals(INodeInfo other)
		{
			return other != null && this.Id == other.Id;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0003B684 File Offset: 0x00039884
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0003B694 File Offset: 0x00039894
		public override bool Equals(object obj)
		{
			return this.Equals(obj as INodeInfo);
		}
	}
}
