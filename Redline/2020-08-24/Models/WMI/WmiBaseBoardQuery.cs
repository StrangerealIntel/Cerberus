using System;

namespace RedLine.Models.WMI
{
	// Token: 0x0200001C RID: 28
	public class WmiBaseBoardQuery : WmiQueryBase
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00003D86 File Offset: 0x00001F86
		public WmiBaseBoardQuery() : base("Win32_BaseBoard", null, new string[]
		{
			"Manufacturer",
			"Product",
			"SerialNumber"
		})
		{
		}
	}
}
