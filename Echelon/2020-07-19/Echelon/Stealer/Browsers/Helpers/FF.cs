using System;

namespace Echelon.Stealer.Browsers.Helpers
{
	// Token: 0x02000028 RID: 40
	public struct FF
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00007520 File Offset: 0x00005720
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00007528 File Offset: 0x00005728
		public long ID { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00007534 File Offset: 0x00005734
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000753C File Offset: 0x0000573C
		public string Type { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00007548 File Offset: 0x00005748
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00007550 File Offset: 0x00005750
		public string Name { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000755C File Offset: 0x0000575C
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00007564 File Offset: 0x00005764
		public string AstableName { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00007570 File Offset: 0x00005770
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00007578 File Offset: 0x00005778
		public long RootNum { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00007584 File Offset: 0x00005784
		// (set) Token: 0x0600008F RID: 143 RVA: 0x0000758C File Offset: 0x0000578C
		public string SqlStatement { get; set; }
	}
}
