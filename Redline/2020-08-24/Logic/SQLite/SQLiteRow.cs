using System;

namespace RedLine.Logic.SQLite
{
	// Token: 0x0200004D RID: 77
	public struct SQLiteRow
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00007DC8 File Offset: 0x00005FC8
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00007DD0 File Offset: 0x00005FD0
		public long ID { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00007DD9 File Offset: 0x00005FD9
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00007DE1 File Offset: 0x00005FE1
		public string[] RowData { get; set; }
	}
}
