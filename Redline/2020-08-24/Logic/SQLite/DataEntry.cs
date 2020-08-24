using System;

namespace RedLine.Logic.SQLite
{
	// Token: 0x0200004B RID: 75
	public struct DataEntry
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00006A78 File Offset: 0x00004C78
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00006A80 File Offset: 0x00004C80
		public long ID { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00006A89 File Offset: 0x00004C89
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00006A91 File Offset: 0x00004C91
		public string Type { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00006A9A File Offset: 0x00004C9A
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00006AA2 File Offset: 0x00004CA2
		public string Name { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00006AAB File Offset: 0x00004CAB
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00006AB3 File Offset: 0x00004CB3
		public string AstableName { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00006ABC File Offset: 0x00004CBC
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00006AC4 File Offset: 0x00004CC4
		public long RootNum { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00006ACD File Offset: 0x00004CCD
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00006AD5 File Offset: 0x00004CD5
		public string SqlStatement { get; set; }
	}
}
