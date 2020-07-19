using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using CG.Web.MegaApiClient.Serialization;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000F4 RID: 244
	[DebuggerDisplay("Node - Type: {Type} - Name: {Name} - Id: {Id}")]
	internal class Node : NodeInfo, INode, INodeInfo, IEquatable<INodeInfo>, INodeCrypto
	{
		// Token: 0x06000886 RID: 2182 RVA: 0x0003B6A4 File Offset: 0x000398A4
		public Node(byte[] masterKey, ref List<SharedKey> sharedKeys)
		{
			this.masterKey = masterKey;
			this.sharedKeys = sharedKeys;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0003B6BC File Offset: 0x000398BC
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x0003B6C4 File Offset: 0x000398C4
		[JsonProperty("p")]
		public string ParentId { get; private set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0003B6D0 File Offset: 0x000398D0
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0003B6D8 File Offset: 0x000398D8
		[JsonProperty("u")]
		public string Owner { get; private set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0003B6E4 File Offset: 0x000398E4
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x0003B6EC File Offset: 0x000398EC
		[JsonProperty("su")]
		public string SharingId { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0003B6F8 File Offset: 0x000398F8
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x0003B700 File Offset: 0x00039900
		[JsonProperty("sk")]
		public string SharingKey { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0003B70C File Offset: 0x0003990C
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x0003B714 File Offset: 0x00039914
		[JsonIgnore]
		public DateTime CreationDate { get; private set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0003B720 File Offset: 0x00039920
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x0003B728 File Offset: 0x00039928
		[JsonIgnore]
		public byte[] Key { get; private set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0003B734 File Offset: 0x00039934
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0003B73C File Offset: 0x0003993C
		[JsonIgnore]
		public byte[] FullKey { get; private set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0003B748 File Offset: 0x00039948
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x0003B750 File Offset: 0x00039950
		[JsonIgnore]
		public byte[] SharedKey { get; private set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0003B75C File Offset: 0x0003995C
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x0003B764 File Offset: 0x00039964
		[JsonIgnore]
		public byte[] Iv { get; private set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0003B770 File Offset: 0x00039970
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0003B778 File Offset: 0x00039978
		[JsonIgnore]
		public byte[] MetaMac { get; private set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0003B784 File Offset: 0x00039984
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0003B78C File Offset: 0x0003998C
		[JsonIgnore]
		public bool EmptyKey { get; private set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0003B798 File Offset: 0x00039998
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0003B7A0 File Offset: 0x000399A0
		[JsonProperty("ts")]
		private long SerializedCreationDate { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0003B7AC File Offset: 0x000399AC
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0003B7B4 File Offset: 0x000399B4
		[JsonProperty("a")]
		private string SerializedAttributes { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0003B7C0 File Offset: 0x000399C0
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0003B7C8 File Offset: 0x000399C8
		[JsonProperty("k")]
		private string SerializedKey { get; set; }

		// Token: 0x060008A3 RID: 2211 RVA: 0x0003B7D4 File Offset: 0x000399D4
		[OnDeserialized]
		public void OnDeserialized(StreamingContext ctx)
		{
			if (this.SharingKey != null && !this.sharedKeys.Any((SharedKey x) => x.Id == base.Id))
			{
				this.sharedKeys.Add(new SharedKey(base.Id, this.SharingKey));
			}
			this.CreationDate = this.SerializedCreationDate.ToDateTime();
			if (base.Type == NodeType.File || base.Type == NodeType.Directory)
			{
				if (string.IsNullOrEmpty(this.SerializedKey))
				{
					this.EmptyKey = true;
					return;
				}
				string text = this.SerializedKey.Split(new char[]
				{
					'/'
				})[0];
				int num = text.IndexOf(":", StringComparison.Ordinal);
				byte[] data = text.Substring(num + 1).FromBase64();
				if (this.sharedKeys != null)
				{
					string handle = text.Substring(0, num);
					SharedKey sharedKey = this.sharedKeys.FirstOrDefault((SharedKey x) => x.Id == handle);
					if (sharedKey != null)
					{
						this.masterKey = Crypto.DecryptKey(sharedKey.Key.FromBase64(), this.masterKey);
						if (base.Type == NodeType.Directory)
						{
							this.SharedKey = this.masterKey;
						}
						else
						{
							this.SharedKey = Crypto.DecryptKey(data, this.masterKey);
						}
					}
				}
				this.FullKey = Crypto.DecryptKey(data, this.masterKey);
				if (base.Type == NodeType.File)
				{
					byte[] iv;
					byte[] metaMac;
					byte[] key;
					Crypto.GetPartsFromDecryptedKey(this.FullKey, out iv, out metaMac, out key);
					this.Iv = iv;
					this.MetaMac = metaMac;
					this.Key = key;
				}
				else
				{
					this.Key = this.FullKey;
				}
				base.Attributes = Crypto.DecryptAttributes(this.SerializedAttributes.FromBase64(), this.Key);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0003B9A0 File Offset: 0x00039BA0
		public bool IsShareRoot
		{
			get
			{
				string text = this.SerializedKey.Split(new char[]
				{
					'/'
				})[0];
				int length = text.IndexOf(":", StringComparison.Ordinal);
				return text.Substring(0, length) == base.Id;
			}
		}

		// Token: 0x040004FF RID: 1279
		private byte[] masterKey;

		// Token: 0x04000500 RID: 1280
		private List<SharedKey> sharedKeys;
	}
}
