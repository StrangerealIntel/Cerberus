using System;
using Microsoft.Win32;

namespace RedLine.Logic.Helpers
{
	// Token: 0x0200005C RID: 92
	public static class OsDetector
	{
		// Token: 0x0600029D RID: 669 RVA: 0x0000B038 File Offset: 0x00009238
		private static string HKLM_GetString(string key, string value)
		{
			string result;
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(key);
				result = (((registryKey != null) ? registryKey.GetValue(value).ToString() : null) ?? string.Empty);
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000B088 File Offset: 0x00009288
		public static string GetWindowsVersion()
		{
			try
			{
				string str;
				try
				{
					str = (Environment.Is64BitOperatingSystem ? "x64" : "x32");
				}
				catch (Exception)
				{
					str = "x86";
				}
				string text = OsDetector.HKLM_GetString("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ProductName");
				OsDetector.HKLM_GetString("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CSDVersion");
				if (!string.IsNullOrEmpty(text))
				{
					return text + " " + str;
				}
			}
			catch (Exception)
			{
			}
			return string.Empty;
		}
	}
}
