using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000116 RID: 278
	[JsonConverter(typeof(ShareDataConverter))]
	internal class ShareData
	{
		// Token: 0x0600097E RID: 2430 RVA: 0x0003D474 File Offset: 0x0003B674
		public ShareData(string nodeId)
		{
			this.NodeId = nodeId;
			this.items = new List<ShareData.ShareDataItem>();
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0003D490 File Offset: 0x0003B690
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x0003D498 File Offset: 0x0003B698
		public string NodeId { get; private set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0003D4A4 File Offset: 0x0003B6A4
		public IEnumerable<ShareData.ShareDataItem> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0003D4AC File Offset: 0x0003B6AC
		public void AddItem(string nodeId, byte[] data, byte[] key)
		{
			ShareData.ShareDataItem item = new ShareData.ShareDataItem
			{
				NodeId = nodeId,
				Data = data,
				Key = key
			};
			this.items.Add(item);
		}

		// Token: 0x04000569 RID: 1385
		private IList<ShareData.ShareDataItem> items;

		// Token: 0x020002A6 RID: 678
		public class ShareDataItem
		{
			// Token: 0x170004C7 RID: 1223
			// (get) Token: 0x06001761 RID: 5985 RVA: 0x00077FDC File Offset: 0x000761DC
			// (set) Token: 0x06001762 RID: 5986 RVA: 0x00077FE4 File Offset: 0x000761E4
			public string NodeId { get; set; }

			// Token: 0x170004C8 RID: 1224
			// (get) Token: 0x06001763 RID: 5987 RVA: 0x00077FF0 File Offset: 0x000761F0
			// (set) Token: 0x06001764 RID: 5988 RVA: 0x00077FF8 File Offset: 0x000761F8
			public byte[] Data { get; set; }

			// Token: 0x170004C9 RID: 1225
			// (get) Token: 0x06001765 RID: 5989 RVA: 0x00078004 File Offset: 0x00076204
			// (set) Token: 0x06001766 RID: 5990 RVA: 0x0007800C File Offset: 0x0007620C
			public byte[] Key { get; set; }
		}
	}
}
