using System;
using System.Diagnostics;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000F5 RID: 245
	[DebuggerDisplay("PublicNode - Type: {Type} - Name: {Name} - Id: {Id}")]
	internal class PublicNode : INode, INodeInfo, IEquatable<INodeInfo>, INodeCrypto
	{
		// Token: 0x060008A6 RID: 2214 RVA: 0x0003BA00 File Offset: 0x00039C00
		internal PublicNode(Node node, string shareId)
		{
			this.node = node;
			this.ShareId = shareId;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0003BA18 File Offset: 0x00039C18
		public string ShareId { get; }

		// Token: 0x060008A8 RID: 2216 RVA: 0x0003BA20 File Offset: 0x00039C20
		public bool Equals(INodeInfo other)
		{
			if (this.node.Equals(other))
			{
				string shareId = this.ShareId;
				PublicNode publicNode = other as PublicNode;
				return shareId == ((publicNode != null) ? publicNode.ShareId : null);
			}
			return false;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0003BA68 File Offset: 0x00039C68
		public long Size
		{
			get
			{
				return this.node.Size;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0003BA78 File Offset: 0x00039C78
		public string Name
		{
			get
			{
				return this.node.Name;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0003BA88 File Offset: 0x00039C88
		public DateTime? ModificationDate
		{
			get
			{
				return this.node.ModificationDate;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0003BA98 File Offset: 0x00039C98
		public string SerializedFingerprint
		{
			get
			{
				return this.node.Attributes.SerializedFingerprint;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0003BAAC File Offset: 0x00039CAC
		public string Id
		{
			get
			{
				return this.node.Id;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0003BABC File Offset: 0x00039CBC
		public string ParentId
		{
			get
			{
				if (!this.node.IsShareRoot)
				{
					return this.node.ParentId;
				}
				return null;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0003BADC File Offset: 0x00039CDC
		public string Owner
		{
			get
			{
				return this.node.Owner;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0003BAEC File Offset: 0x00039CEC
		public NodeType Type
		{
			get
			{
				if (!this.node.IsShareRoot || this.node.Type != NodeType.Directory)
				{
					return this.node.Type;
				}
				return NodeType.Root;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0003BB1C File Offset: 0x00039D1C
		public DateTime CreationDate
		{
			get
			{
				return this.node.CreationDate;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0003BB2C File Offset: 0x00039D2C
		public byte[] Key
		{
			get
			{
				return this.node.Key;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0003BB3C File Offset: 0x00039D3C
		public byte[] SharedKey
		{
			get
			{
				return this.node.SharedKey;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0003BB4C File Offset: 0x00039D4C
		public byte[] Iv
		{
			get
			{
				return this.node.Iv;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0003BB5C File Offset: 0x00039D5C
		public byte[] MetaMac
		{
			get
			{
				return this.node.MetaMac;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0003BB6C File Offset: 0x00039D6C
		public byte[] FullKey
		{
			get
			{
				return this.node.FullKey;
			}
		}

		// Token: 0x0400050F RID: 1295
		private readonly Node node;
	}
}
