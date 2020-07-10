using System;
using System.Net;
using System.Net.Sockets;

namespace MufMaOSvGyvz
{
	// Token: 0x02000015 RID: 21
	public static class MAC
	{
		// Token: 0x0600005D RID: 93
		public static void GetMAC(string string_0, string string_1, string string_2)
		{
			UdpClient udpClient = new UdpClient();
			byte[] array = new byte[102];
			for (int i = 0; i <= 5; i++)
			{
				array[i] = byte.MaxValue;
			}
			string[] array2;
			if (string_0.Contains("-"))
			{
				array2 = string_0.Split(new char[]
				{
					'-'
				});
			}
			else
			{
				array2 = string_0.Split(new char[]
				{
					':'
				});
			}
			if (array2.Length != 6)
			{
				throw new ArgumentException("Incorrect MAC address supplied!");
			}
			int num = 6;
			for (int i = 0; i < 16; i++)
			{
				for (int j = 0; j < 6; j++)
				{
					array[num + i * 6 + j] = (byte)Convert.ToInt32(array2[j], 16);
				}
			}
			IPAddress ipaddress_ = IPAddress.Parse(string_1);
			IPAddress ipaddress_2 = IPAddress.Parse(string_2);
			IPAddress broadcastAddress = ipaddress_.GetBroadcastAddress(ipaddress_2);
			udpClient.Send(array, array.Length, broadcastAddress.ToString(), 3);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00007E48 File Offset: 0x00006048
		public static IPAddress GetBroadcastAddress(this IPAddress ipaddress_0, IPAddress ipaddress_1)
		{
			byte[] addressBytes = ipaddress_0.GetAddressBytes();
			byte[] addressBytes2 = ipaddress_1.GetAddressBytes();
			if (addressBytes.Length != addressBytes2.Length)
			{
				throw new ArgumentException("Lengths of IP address and subnet mask do not match.");
			}
			byte[] array = new byte[addressBytes.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (addressBytes[i] | (addressBytes2[i] ^ byte.MaxValue));
			}
			return new IPAddress(array);
		}
	}
}
