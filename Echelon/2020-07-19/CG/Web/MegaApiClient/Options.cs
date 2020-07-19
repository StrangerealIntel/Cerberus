using System;
using System.Linq;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000F7 RID: 247
	public class Options
	{
		// Token: 0x060008B9 RID: 2233 RVA: 0x0003BC58 File Offset: 0x00039E58
		public Options(string applicationKey = "axhQiYyQ", bool synchronizeApiRequests = true, Options.ComputeApiRequestRetryWaitDelayDelegate computeApiRequestRetryWaitDelay = null, int bufferSize = 65536, int chunksPackSize = 1048576)
		{
			this.ApplicationKey = applicationKey;
			this.SynchronizeApiRequests = synchronizeApiRequests;
			this.ComputeApiRequestRetryWaitDelay = (computeApiRequestRetryWaitDelay ?? new Options.ComputeApiRequestRetryWaitDelayDelegate(this.ComputeDefaultApiRequestRetryWaitDelay));
			this.BufferSize = bufferSize;
			this.ChunksPackSize = chunksPackSize;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0003BC98 File Offset: 0x00039E98
		public string ApplicationKey { get; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0003BCA0 File Offset: 0x00039EA0
		public bool SynchronizeApiRequests { get; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0003BCA8 File Offset: 0x00039EA8
		public Options.ComputeApiRequestRetryWaitDelayDelegate ComputeApiRequestRetryWaitDelay { get; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0003BCB0 File Offset: 0x00039EB0
		public int BufferSize { get; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0003BCB8 File Offset: 0x00039EB8
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x0003BCC0 File Offset: 0x00039EC0
		public int ChunksPackSize { get; internal set; }

		// Token: 0x060008C0 RID: 2240 RVA: 0x0003BCCC File Offset: 0x00039ECC
		private bool ComputeDefaultApiRequestRetryWaitDelay(int attempt, out TimeSpan delay)
		{
			if (attempt > 17)
			{
				delay = default(TimeSpan);
				return false;
			}
			int num = Enumerable.Range(0, attempt).Aggregate(0, (int current, int item) => (int)((current == 0) ? 100f : ((float)current * 1.5f)));
			delay = TimeSpan.FromMilliseconds((double)num);
			return true;
		}

		// Token: 0x04000511 RID: 1297
		public const string DefaultApplicationKey = "axhQiYyQ";

		// Token: 0x04000512 RID: 1298
		public const bool DefaultSynchronizeApiRequests = true;

		// Token: 0x04000513 RID: 1299
		public const int DefaultApiRequestAttempts = 17;

		// Token: 0x04000514 RID: 1300
		public const int DefaultApiRequestDelay = 100;

		// Token: 0x04000515 RID: 1301
		public const float DefaultApiRequestDelayFactor = 1.5f;

		// Token: 0x04000516 RID: 1302
		public const int DefaultBufferSize = 65536;

		// Token: 0x04000517 RID: 1303
		public const int DefaultChunksPackSize = 1048576;

		// Token: 0x0200029D RID: 669
		// (Invoke) Token: 0x0600173A RID: 5946
		public delegate bool ComputeApiRequestRetryWaitDelayDelegate(int attempt, out TimeSpan delay);
	}
}
