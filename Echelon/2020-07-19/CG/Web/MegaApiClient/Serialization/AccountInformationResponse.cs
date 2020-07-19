using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x020000FF RID: 255
	internal class AccountInformationResponse : IAccountInformation
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0003C874 File Offset: 0x0003AA74
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x0003C87C File Offset: 0x0003AA7C
		[JsonProperty("mstrg")]
		public long TotalQuota { get; private set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0003C888 File Offset: 0x0003AA88
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x0003C890 File Offset: 0x0003AA90
		[JsonProperty("cstrg")]
		public long UsedQuota { get; private set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0003C89C File Offset: 0x0003AA9C
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x0003C8A4 File Offset: 0x0003AAA4
		[JsonProperty("cstrgn")]
		private Dictionary<string, long[]> MetricsSerialized { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0003C8B0 File Offset: 0x0003AAB0
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0003C8B8 File Offset: 0x0003AAB8
		public IEnumerable<IStorageMetrics> Metrics { get; private set; }

		// Token: 0x0600090B RID: 2315 RVA: 0x0003C8C4 File Offset: 0x0003AAC4
		[OnDeserialized]
		public void OnDeserialized(StreamingContext context)
		{
			this.Metrics = from x in this.MetricsSerialized
			select new AccountInformationResponse.StorageMetrics(x.Key, x.Value);
		}

		// Token: 0x020002A1 RID: 673
		private class StorageMetrics : IStorageMetrics
		{
			// Token: 0x06001748 RID: 5960 RVA: 0x00077E48 File Offset: 0x00076048
			public StorageMetrics(string nodeId, long[] metrics)
			{
				this.NodeId = nodeId;
				this.BytesUsed = metrics[0];
				this.FilesCount = metrics[1];
				this.FoldersCount = metrics[2];
			}

			// Token: 0x170004BF RID: 1215
			// (get) Token: 0x06001749 RID: 5961 RVA: 0x00077E74 File Offset: 0x00076074
			public string NodeId { get; }

			// Token: 0x170004C0 RID: 1216
			// (get) Token: 0x0600174A RID: 5962 RVA: 0x00077E7C File Offset: 0x0007607C
			public long BytesUsed { get; }

			// Token: 0x170004C1 RID: 1217
			// (get) Token: 0x0600174B RID: 5963 RVA: 0x00077E84 File Offset: 0x00076084
			public long FilesCount { get; }

			// Token: 0x170004C2 RID: 1218
			// (get) Token: 0x0600174C RID: 5964 RVA: 0x00077E8C File Offset: 0x0007608C
			public long FoldersCount { get; }
		}
	}
}
