using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x020000FE RID: 254
	internal class AccountInformationRequest : RequestBase
	{
		// Token: 0x060008FF RID: 2303 RVA: 0x0003C858 File Offset: 0x0003AA58
		public AccountInformationRequest() : base("uq")
		{
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0003C868 File Offset: 0x0003AA68
		[JsonProperty("strg")]
		public int Storage
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0003C86C File Offset: 0x0003AA6C
		[JsonProperty("xfer")]
		public int Transfer
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0003C870 File Offset: 0x0003AA70
		[JsonProperty("pro")]
		public int AccountType
		{
			get
			{
				return 0;
			}
		}
	}
}
