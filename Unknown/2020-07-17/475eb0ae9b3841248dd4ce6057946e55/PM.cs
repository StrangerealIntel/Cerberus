using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography;
using System.Threading;

// Token: 0x02000002 RID: 2
internal class PM
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public static void Sleep(int time)
	{
		Thread.Sleep(time);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	public static string[] Split(string str, string sp)
	{
		if (str != null)
		{
			return str.Split(new string[]
			{
				sp
			}, StringSplitOptions.RemoveEmptyEntries);
		}
		return null;
	}

	// Token: 0x06000003 RID: 3
	public static string ReadStreamOfPayload(WebResponse hwr)
	{
		string result = "";
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		foreach (string text in hwr.Headers.AllKeys)
		{
			dictionary.Add(text, hwr.Headers.Get(text));
		}
		if (dictionary.ContainsKey("Content-Encoding") && !string.IsNullOrEmpty(dictionary["Content-Encoding"]) && dictionary["Content-Encoding"].IndexOf("gzip", StringComparison.OrdinalIgnoreCase) >= 0)
		{
			using (GZipStream gzipStream = new GZipStream(hwr.GetResponseStream(), CompressionMode.Decompress))
			{
				return new StreamReader(gzipStream).ReadToEnd();
			}
		}
		result = new StreamReader(hwr.GetResponseStream()).ReadToEnd();
		return result;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002140 File Offset: 0x00000340
	public static string GetMD5(byte[] buff)
	{
		return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(buff)).Replace("-", "").ToLower();
	}
}
