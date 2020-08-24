using System;

namespace RedLine.Models.WMI
{
	// Token: 0x02000021 RID: 33
	public class WmiFirewallQuery : WmiQueryBase
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00003E8B File Offset: 0x0000208B
		public WmiFirewallQuery() : base("FirewallProduct", null, new string[]
		{
			"displayName",
			"pathToSignedProductExe"
		})
		{
		}
	}
}
