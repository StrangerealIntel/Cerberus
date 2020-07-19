using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000109 RID: 265
	internal class GetNodesResponse
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x0003CF88 File Offset: 0x0003B188
		public GetNodesResponse(byte[] masterKey)
		{
			this.masterKey = masterKey;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0003CF98 File Offset: 0x0003B198
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x0003CFA0 File Offset: 0x0003B1A0
		public Node[] Nodes { get; private set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0003CFAC File Offset: 0x0003B1AC
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x0003CFB4 File Offset: 0x0003B1B4
		public Node[] UndecryptedNodes { get; private set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0003CFC0 File Offset: 0x0003B1C0
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x0003CFC8 File Offset: 0x0003B1C8
		[JsonProperty("f")]
		public JRaw NodesSerialized { get; private set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0003CFD4 File Offset: 0x0003B1D4
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x0003CFDC File Offset: 0x0003B1DC
		[JsonProperty("ok")]
		public List<SharedKey> SharedKeys
		{
			get
			{
				return this.sharedKeys;
			}
			private set
			{
				this.sharedKeys = value;
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0003CFE8 File Offset: 0x0003B1E8
		[OnDeserialized]
		public void OnDeserialized(StreamingContext ctx)
		{
			Node[] source = JsonConvert.DeserializeObject<Node[]>(this.NodesSerialized.ToString(), new JsonConverter[]
			{
				new NodeConverter(this.masterKey, ref this.sharedKeys)
			});
			this.UndecryptedNodes = (from x in source
			where x.EmptyKey
			select x).ToArray<Node>();
			this.Nodes = (from x in source
			where !x.EmptyKey
			select x).ToArray<Node>();
		}

		// Token: 0x04000551 RID: 1361
		private readonly byte[] masterKey;

		// Token: 0x04000552 RID: 1362
		private List<SharedKey> sharedKeys;
	}
}
