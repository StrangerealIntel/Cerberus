using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000119 RID: 281
	internal class ShareNodeRequest : RequestBase
	{
		// Token: 0x0600098C RID: 2444 RVA: 0x0003D668 File Offset: 0x0003B868
		public ShareNodeRequest(INode node, byte[] masterKey, IEnumerable<INode> nodes) : base("s2")
		{
			this.Id = node.Id;
			this.Options = new object[]
			{
				new
				{
					r = 0,
					u = "EXP"
				}
			};
			INodeCrypto nodeCrypto = (INodeCrypto)node;
			byte[] array = nodeCrypto.SharedKey;
			if (array == null)
			{
				array = Crypto.CreateAesKey();
			}
			this.SharedKey = Crypto.EncryptKey(array, masterKey).ToBase64();
			if (nodeCrypto.SharedKey == null)
			{
				this.Share = new ShareData(node.Id);
				this.Share.AddItem(node.Id, nodeCrypto.FullKey, array);
				foreach (INode node2 in this.GetRecursiveChildren(nodes.ToArray<INode>(), node))
				{
					this.Share.AddItem(node2.Id, ((INodeCrypto)node2).FullKey, array);
				}
			}
			byte[] data = (node.Id + node.Id).ToBytes();
			this.HandleAuth = Crypto.EncryptKey(data, masterKey).ToBase64();
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0003D798 File Offset: 0x0003B998
		private IEnumerable<INode> GetRecursiveChildren(INode[] nodes, INode parent)
		{
			using (IEnumerator<INode> enumerator = (from x in nodes
			where x.Type == NodeType.Directory || x.Type == NodeType.File
			select x).GetEnumerator())
			{
				IL_F9:
				while (enumerator.MoveNext())
				{
					INode node = enumerator.Current;
					ShareNodeRequest.<>c__DisplayClass1_0 CS$<>8__locals1 = new ShareNodeRequest.<>c__DisplayClass1_0();
					CS$<>8__locals1.parentId = node.Id;
					for (;;)
					{
						ShareNodeRequest.<>c__DisplayClass1_0 CS$<>8__locals2 = CS$<>8__locals1;
						INode node2 = nodes.FirstOrDefault((INode x) => x.Id == CS$<>8__locals1.parentId);
						CS$<>8__locals2.parentId = ((node2 != null) ? node2.ParentId : null);
						if (CS$<>8__locals1.parentId == parent.Id)
						{
							break;
						}
						if (CS$<>8__locals1.parentId == null)
						{
							goto IL_F9;
						}
					}
					yield return node;
				}
			}
			IEnumerator<INode> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0003D7B0 File Offset: 0x0003B9B0
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x0003D7B8 File Offset: 0x0003B9B8
		[JsonProperty("n")]
		public string Id { get; private set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0003D7C4 File Offset: 0x0003B9C4
		// (set) Token: 0x06000991 RID: 2449 RVA: 0x0003D7CC File Offset: 0x0003B9CC
		[JsonProperty("ha")]
		public string HandleAuth { get; private set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0003D7D8 File Offset: 0x0003B9D8
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x0003D7E0 File Offset: 0x0003B9E0
		[JsonProperty("s")]
		public object[] Options { get; private set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0003D7EC File Offset: 0x0003B9EC
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x0003D7F4 File Offset: 0x0003B9F4
		[JsonProperty("cr")]
		public ShareData Share { get; private set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0003D800 File Offset: 0x0003BA00
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x0003D808 File Offset: 0x0003BA08
		[JsonProperty("ok")]
		public string SharedKey { get; private set; }
	}
}
