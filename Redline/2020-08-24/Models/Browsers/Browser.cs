using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RedLine.Models.Browsers
{
	// Token: 0x02000031 RID: 49
	[DataContract(Name = "Browser", Namespace = "v1/Models")]
	public class Browser
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000043F2 File Offset: 0x000025F2
		// (set) Token: 0x0600011A RID: 282 RVA: 0x000043FA File Offset: 0x000025FA
		[DataMember(Name = "Name")]
		public string Name { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00004403 File Offset: 0x00002603
		// (set) Token: 0x0600011C RID: 284 RVA: 0x0000440B File Offset: 0x0000260B
		[DataMember(Name = "Profile")]
		public string Profile { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00004414 File Offset: 0x00002614
		// (set) Token: 0x0600011E RID: 286 RVA: 0x0000441C File Offset: 0x0000261C
		[DataMember(Name = "Credentials")]
		public IList<LoginPair> Credentials { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004425 File Offset: 0x00002625
		// (set) Token: 0x06000120 RID: 288 RVA: 0x0000442D File Offset: 0x0000262D
		[DataMember(Name = "Autofills")]
		public IList<Autofill> Autofills { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004436 File Offset: 0x00002636
		// (set) Token: 0x06000122 RID: 290 RVA: 0x0000443E File Offset: 0x0000263E
		[DataMember(Name = "CreditCards")]
		public IList<CreditCard> CreditCards { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004447 File Offset: 0x00002647
		// (set) Token: 0x06000124 RID: 292 RVA: 0x0000444F File Offset: 0x0000264F
		[DataMember(Name = "Cookies")]
		public IList<Cookie> Cookies { get; set; }

		// Token: 0x06000125 RID: 293 RVA: 0x00004458 File Offset: 0x00002658
		public bool IsEmpty()
		{
			bool result = true;
			IList<LoginPair> credentials = this.Credentials;
			if (credentials != null && credentials.Count > 0)
			{
				result = false;
			}
			IList<Autofill> autofills = this.Autofills;
			if (autofills != null && autofills.Count > 0)
			{
				result = false;
			}
			IList<CreditCard> creditCards = this.CreditCards;
			if (creditCards != null && creditCards.Count > 0)
			{
				result = false;
			}
			IList<Cookie> cookies = this.Cookies;
			if (cookies != null && cookies.Count > 0)
			{
				result = false;
			}
			return result;
		}
	}
}
