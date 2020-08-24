using System;

namespace RedLine.Logic.SQLite
{
	// Token: 0x0200004E RID: 78
	public struct TypeSizes
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00007DEA File Offset: 0x00005FEA
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00007DF2 File Offset: 0x00005FF2
		public long Size { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00007DFB File Offset: 0x00005FFB
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00007E03 File Offset: 0x00006003
		public long Type { get; set; }
	}
}
