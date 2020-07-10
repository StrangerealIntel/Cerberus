using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MufMaOSvGyvz
{
	// Token: 0x02000016 RID: 22
	public class ARP
	{
		// Token: 0x0600005F RID: 95 RVA: 0x0000265D File Offset: 0x0000085D
		public ARP(string string_0, string string_1)
		{
			this.MacAddress = string_0;
			this.IPAddress = string_1;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00007EAC File Offset: 0x000060AC
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000267E File Offset: 0x0000087E
		public string MacAddress
		{
			get
			{
				return this.<MacAddress>k__BackingField;
			}
			private set
			{
				this.<MacAddress>k__BackingField = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00007EC4 File Offset: 0x000060C4
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002687 File Offset: 0x00000887
		public string IPAddress
		{
			get
			{
				return this.<IPAddress>k__BackingField;
			}
			private set
			{
				this.<IPAddress>k__BackingField = value;
			}
		}

		// Token: 0x06000064 RID: 100
		public static List<ARP> ExecuteARPPing()
		{
			List<ARP> result;
			try
			{
				List<ARP> list = new List<ARP>();
				foreach (string text in ARP.ARPPing().Split(new char[]
				{
					'\n',
					'\r'
				}))
				{
					if (!string.IsNullOrEmpty(text))
					{
						IEnumerable<string> source = text.Split(new char[]
						{
							' ',
							'\t'
						});
						if (ARP.CS$<>9__CachedAnonymousMethodDelegate4 == null)
						{
							ARP.CS$<>9__CachedAnonymousMethodDelegate4 = new Func<string, bool>(ARP.<GetIPInfo>b__3);
						}
						string[] array2 = source.Where(ARP.CS$<>9__CachedAnonymousMethodDelegate4).ToArray<string>();
						if (array2.Length == 3)
						{
							list.Add(new ARP(array2[1], array2[0]));
						}
					}
				}
				result = list;
			}
			catch (Exception innerException)
			{
				throw new Exception("IPInfo: Error Parsing 'arp -a' results", innerException);
			}
			return result;
		}

		// Token: 0x06000065 RID: 101
		private static string ARPPing()
		{
			Process process = null;
			string result = string.Empty;
			try
			{
				process = Process.Start(new ProcessStartInfo("arp", "-a")
				{
					CreateNoWindow = true,
					UseShellExecute = false,
					RedirectStandardOutput = true
				});
				result = process.StandardOutput.ReadToEnd();
				process.Close();
			}
			catch (Exception innerException)
			{
				throw new Exception("IPInfo: Error Retrieving 'arp -a' Results", innerException);
			}
			finally
			{
				if (process != null)
				{
					process.Close();
				}
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002690 File Offset: 0x00000890
		private static bool <GetIPInfo>b__3(string string_0)
		{
			return !string.IsNullOrEmpty(string_0);
		}

		// Token: 0x04000065 RID: 101
		private string MIVThsKTpvf = string.Empty;

		// Token: 0x04000066 RID: 102
		private string <MacAddress>k__BackingField;

		// Token: 0x04000067 RID: 103
		private string <IPAddress>k__BackingField;

		// Token: 0x04000068 RID: 104
		private static Func<string, bool> CS$<>9__CachedAnonymousMethodDelegate4;
	}
}
