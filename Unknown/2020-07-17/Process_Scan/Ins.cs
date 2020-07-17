using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Process_Scan
{
	// Token: 0x02000002 RID: 2
	public class Ins
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[STAThread]
		public string Run(string param)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Process process in Process.GetProcesses())
			{
				string text = Ins.GetExecutablePathAboveVista(process.Id);
				if (string.IsNullOrEmpty(text.Trim()))
				{
					text = process.ProcessName;
				}
				stringBuilder.AppendLine(process.Id.ToString() + "->" + text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020C8 File Offset: 0x000002C8
		private static string GetExecutablePathAboveVista(int ProcessId)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			IntPtr intPtr = Ins.OpenProcess(Ins.ProcessAccessFlags.QueryLimitedInformation, false, ProcessId);
			if (intPtr != IntPtr.Zero)
			{
				try
				{
					int capacity = stringBuilder.Capacity;
					if (Ins.QueryFullProcessImageName(intPtr, 0, stringBuilder, out capacity))
					{
						return stringBuilder.ToString();
					}
				}
				finally
				{
					Ins.CloseHandle(intPtr);
				}
			}
			return string.Empty;
		}

		// Token: 0x06000003 RID: 3
		[DllImport("kernel32.dll")]
		private static extern bool QueryFullProcessImageName(IntPtr hprocess, int dwFlags, StringBuilder lpExeName, out int size);

		// Token: 0x06000004 RID: 4
		[DllImport("kernel32.dll")]
		private static extern IntPtr OpenProcess(Ins.ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);

		// Token: 0x06000005 RID: 5
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CloseHandle(IntPtr hHandle);

		// Token: 0x02000003 RID: 3
		[Flags]
		public enum ProcessAccessFlags : uint
		{
			// Token: 0x04000002 RID: 2
			All = 2035711u,
			// Token: 0x04000003 RID: 3
			Terminate = 1u,
			// Token: 0x04000004 RID: 4
			CreateThread = 2u,
			// Token: 0x04000005 RID: 5
			VirtualMemoryOperation = 8u,
			// Token: 0x04000006 RID: 6
			VirtualMemoryRead = 16u,
			// Token: 0x04000007 RID: 7
			VirtualMemoryWrite = 32u,
			// Token: 0x04000008 RID: 8
			DuplicateHandle = 64u,
			// Token: 0x04000009 RID: 9
			CreateProcess = 128u,
			// Token: 0x0400000A RID: 10
			SetQuota = 256u,
			// Token: 0x0400000B RID: 11
			SetInformation = 512u,
			// Token: 0x0400000C RID: 12
			QueryInformation = 1024u,
			// Token: 0x0400000D RID: 13
			QueryLimitedInformation = 4096u,
			// Token: 0x0400000E RID: 14
			Synchronize = 1048576u
		}
	}
}
