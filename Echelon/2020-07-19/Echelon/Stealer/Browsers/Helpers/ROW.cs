using System;

namespace Echelon.Stealer.Browsers.Helpers
{
	// Token: 0x0200002A RID: 42
	public struct ROW
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00007684 File Offset: 0x00005884
		// (set) Token: 0x0600009C RID: 156 RVA: 0x0000768C File Offset: 0x0000588C
		public long ID { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00007698 File Offset: 0x00005898
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000076A0 File Offset: 0x000058A0
		public string[] RowData { get; set; }
	}
}
