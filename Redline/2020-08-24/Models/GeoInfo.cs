using System;

namespace RedLine.Models
{
	// Token: 0x0200000E RID: 14
	public class GeoInfo
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000033B7 File Offset: 0x000015B7
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000033BF File Offset: 0x000015BF
		public string IP { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000033C8 File Offset: 0x000015C8
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000033D0 File Offset: 0x000015D0
		public string Location { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000033D9 File Offset: 0x000015D9
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000033E1 File Offset: 0x000015E1
		public string Country { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000033EA File Offset: 0x000015EA
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000033F2 File Offset: 0x000015F2
		public string PostalCode { get; set; }
	}
}
