using System;

namespace RedLine.Models.WMI
{
	// Token: 0x02000024 RID: 36
	public class WmiGraphicCard
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003F05 File Offset: 0x00002105
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00003F0D File Offset: 0x0000210D
		[WmiResult("Description")]
		public string Description { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003F16 File Offset: 0x00002116
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00003F1E File Offset: 0x0000211E
		[WmiResult("Name")]
		public string Name { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003F27 File Offset: 0x00002127
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00003F2F File Offset: 0x0000212F
		[WmiResult("Caption")]
		public string Caption { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003F38 File Offset: 0x00002138
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00003F40 File Offset: 0x00002140
		[WmiResult("AdapterRAM")]
		public uint AdapterRAM { get; private set; }

		// Token: 0x04000077 RID: 119
		internal const string ADAPTERRAM = "AdapterRAM";

		// Token: 0x04000078 RID: 120
		internal const string CAPTION = "Caption";

		// Token: 0x04000079 RID: 121
		internal const string DESCRIPTION = "Description";

		// Token: 0x0400007A RID: 122
		internal const string NAME = "Name";
	}
}
