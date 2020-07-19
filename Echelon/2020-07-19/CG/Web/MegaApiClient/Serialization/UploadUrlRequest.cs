using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x0200011A RID: 282
	internal class UploadUrlRequest : RequestBase
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x0003D814 File Offset: 0x0003BA14
		public UploadUrlRequest(long fileSize) : base("u")
		{
			this.Size = fileSize;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0003D828 File Offset: 0x0003BA28
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x0003D830 File Offset: 0x0003BA30
		[JsonProperty("s")]
		public long Size { get; private set; }
	}
}
