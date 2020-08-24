using System;

namespace RedLine.Models.WMI
{
	// Token: 0x02000022 RID: 34
	public class WmiGraphicCardQuery : WmiQueryBase
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00003EAF File Offset: 0x000020AF
		public WmiGraphicCardQuery() : base("Win32_VideoController", null, new string[]
		{
			"Name",
			"AdapterRAM",
			"Caption",
			"Description"
		})
		{
		}
	}
}
