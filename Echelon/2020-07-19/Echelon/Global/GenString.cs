using System;

namespace Echelon.Global
{
	// Token: 0x02000046 RID: 70
	internal class GenString
	{
		// Token: 0x0600019B RID: 411 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		public static string Generate()
		{
			string text = "acegikmoqsuwyBDFHJLNPRTVXZ";
			string text2 = "";
			Random random = new Random();
			int num = random.Next(0, text.Length);
			for (int i = 0; i < num; i++)
			{
				text2 += text[random.Next(10, text.Length)].ToString();
			}
			return text2;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000CA18 File Offset: 0x0000AC18
		public static int GeneNumbersTo()
		{
			return new Random().Next(11, 99);
		}
	}
}
