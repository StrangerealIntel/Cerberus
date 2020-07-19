using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000113 RID: 275
	internal abstract class RequestBase
	{
		// Token: 0x06000977 RID: 2423 RVA: 0x0003D420 File Offset: 0x0003B620
		protected RequestBase(string action)
		{
			this.Action = action;
			this.QueryArguments = new Dictionary<string, string>();
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x0003D43C File Offset: 0x0003B63C
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x0003D444 File Offset: 0x0003B644
		[JsonProperty("a")]
		public string Action { get; private set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0003D450 File Offset: 0x0003B650
		[JsonIgnore]
		public Dictionary<string, string> QueryArguments { get; }
	}
}
