using System;
using System.IO;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SmartAssembly.StringsEncoding;

namespace ChromV2
{
	// Token: 0x02000075 RID: 117
	public class SQLiteHandler
	{
		// Token: 0x06000277 RID: 631 RVA: 0x00014184 File Offset: 0x00012384
		public SQLiteHandler(string baseName)
		{
			if (File.Exists(baseName))
			{
				FileSystem.FileOpen(1, baseName, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared, -1);
				string s = Microsoft.VisualBasic.Strings.Space((int)FileSystem.LOF(1));
				FileSystem.FileGet(1, ref s, -1L, false);
				FileSystem.FileClose(new int[]
				{
					1
				});
				this.db_bytes = Encoding.Default.GetBytes(s);
				if (string.Compare(Encoding.Default.GetString(this.db_bytes, 0, 15), ChromV265450.Strings.Get(107394091), StringComparison.Ordinal) != 0)
				{
					throw new Exception(ChromV265450.Strings.Get(107394102));
				}
				if (this.db_bytes[52] != 0)
				{
					throw new Exception(ChromV265450.Strings.Get(107394533));
				}
				this.page_size = (ushort)this.ConvertToInteger(16, 2);
				this.encoding = this.ConvertToInteger(56, 4);
				if (decimal.Compare(new decimal(this.encoding), 0m) == 0)
				{
					this.encoding = 1UL;
				}
				this.ReadMasterTable(100UL);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000142A4 File Offset: 0x000124A4
		private ulong ConvertToInteger(int A_1, int A_2)
		{
			if (A_2 > 8 | A_2 == 0)
			{
				return 0UL;
			}
			ulong num = 0UL;
			int num2 = A_2 - 1;
			for (int i = 0; i <= num2; i++)
			{
				num = (num << 8 | (ulong)this.db_bytes[A_1 + i]);
			}
			return num;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000142F0 File Offset: 0x000124F0
		private long CVL(int A_1, int A_2)
		{
			A_2++;
			byte[] array = new byte[8];
			int num = A_2 - A_1;
			bool flag = false;
			if (num == 0 | num > 9)
			{
				return 0L;
			}
			if (num == 1)
			{
				array[0] = (this.db_bytes[A_1] & 127);
				return BitConverter.ToInt64(array, 0);
			}
			if (num == 9)
			{
				flag = true;
			}
			int num2 = 1;
			int num3 = 7;
			int num4 = 0;
			if (flag)
			{
				array[0] = this.db_bytes[A_2 - 1];
				A_2--;
				num4 = 1;
			}
			for (int i = A_2 - 1; i >= A_1; i += -1)
			{
				if (i - 1 >= A_1)
				{
					array[num4] = (byte)(((int)((byte)(this.db_bytes[i] >> (num2 - 1 & 7))) & 255 >> num2) | (int)((byte)(this.db_bytes[i - 1] << (num3 & 7))));
					num2++;
					num4++;
					num3--;
				}
				else if (!flag)
				{
					array[num4] = (byte)((int)((byte)(this.db_bytes[i] >> (num2 - 1 & 7))) & 255 >> num2);
				}
			}
			return BitConverter.ToInt64(array, 0);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00014414 File Offset: 0x00012614
		public int GetRowCount()
		{
			return this.table_entries.Length;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00014420 File Offset: 0x00012620
		public string[] GetTableNames()
		{
			string[] array = null;
			int num = 0;
			int num2 = this.master_table_entries.Length - 1;
			for (int i = 0; i <= num2; i++)
			{
				if (this.master_table_entries[i].item_type == ChromV265450.Strings.Get(107394472))
				{
					array = (string[])Utils.CopyArray(array, new string[num + 1]);
					array[num] = this.master_table_entries[i].item_name;
					num++;
				}
			}
			return array;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000144A8 File Offset: 0x000126A8
		public string GetValue(int row_num, int field)
		{
			if (row_num >= this.table_entries.Length)
			{
				return null;
			}
			if (field >= this.table_entries[row_num].content.Length)
			{
				return null;
			}
			return this.table_entries[row_num].content[field];
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000144FC File Offset: 0x000126FC
		public string GetValue(int row_num, string field)
		{
			int num = -1;
			int num2 = this.field_names.Length - 1;
			for (int i = 0; i <= num2; i++)
			{
				if (string.Compare(this.field_names[i].ToLower(), field.ToLower(), StringComparison.Ordinal) == 0)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return null;
			}
			return this.GetValue(row_num, num);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00014564 File Offset: 0x00012764
		private int GVL(int A_1)
		{
			if (A_1 > this.db_bytes.Length)
			{
				return 0;
			}
			int num = A_1 + 8;
			for (int i = A_1; i <= num; i++)
			{
				if (i > this.db_bytes.Length - 1)
				{
					return 0;
				}
				if ((this.db_bytes[i] & 128) != 128)
				{
					return i;
				}
			}
			return A_1 + 8;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000145C8 File Offset: 0x000127C8
		private bool IsOdd(long A_1)
		{
			return (A_1 & 1L) == 1L;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000145D4 File Offset: 0x000127D4
		private void ReadMasterTable(ulong A_1)
		{
			if (this.db_bytes[(int)A_1] == 13)
			{
				ushort num = Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(A_1), 3m)), 2)), 1m));
				int num2 = 0;
				if (this.master_table_entries != null)
				{
					num2 = this.master_table_entries.Length;
					this.master_table_entries = (SQLiteHandler.sqlite_master_entry[])Utils.CopyArray(this.master_table_entries, new SQLiteHandler.sqlite_master_entry[this.master_table_entries.Length + (int)num + 1]);
				}
				else
				{
					this.master_table_entries = new SQLiteHandler.sqlite_master_entry[(int)(num + 1)];
				}
				int num3 = (int)num;
				for (int i = 0; i <= num3; i++)
				{
					ulong num4 = this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(A_1), 8m), new decimal(i * 2))), 2);
					if (decimal.Compare(new decimal(A_1), 100m) != 0)
					{
						num4 += A_1;
					}
					int num5 = this.GVL((int)num4);
					int num6 = this.GVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)));
					this.master_table_entries[num2 + i].row_id = this.CVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)), num6);
					num4 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num6), new decimal(num4))), 1m));
					num5 = this.GVL((int)num4);
					num6 = num5;
					long value = this.CVL((int)num4, num5);
					long[] array = new long[5];
					int num7 = 0;
					do
					{
						num5 = num6 + 1;
						num6 = this.GVL(num5);
						array[num7] = this.CVL(num5, num6);
						if (array[num7] > 9L)
						{
							if (this.IsOdd(array[num7]))
							{
								array[num7] = (long)Math.Round((double)(array[num7] - 13L) / 2.0);
							}
							else
							{
								array[num7] = (long)Math.Round((double)(array[num7] - 12L) / 2.0);
							}
						}
						else
						{
							array[num7] = (long)((ulong)this.SQLDataTypeSize[(int)array[num7]]);
						}
						num7++;
					}
					while (num7 <= 4);
					if (decimal.Compare(new decimal(this.encoding), 1m) == 0)
					{
						this.master_table_entries[num2 + i].item_type = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num4), new decimal(value))), (int)array[0]);
					}
					else if (decimal.Compare(new decimal(this.encoding), 2m) == 0)
					{
						this.master_table_entries[num2 + i].item_type = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num4), new decimal(value))), (int)array[0]);
					}
					else if (decimal.Compare(new decimal(this.encoding), 3m) == 0)
					{
						this.master_table_entries[num2 + i].item_type = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num4), new decimal(value))), (int)array[0]);
					}
					if (decimal.Compare(new decimal(this.encoding), 1m) == 0)
					{
						this.master_table_entries[num2 + i].item_name = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0]))), (int)array[1]);
					}
					else if (decimal.Compare(new decimal(this.encoding), 2m) == 0)
					{
						this.master_table_entries[num2 + i].item_name = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0]))), (int)array[1]);
					}
					else if (decimal.Compare(new decimal(this.encoding), 3m) == 0)
					{
						this.master_table_entries[num2 + i].item_name = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0]))), (int)array[1]);
					}
					this.master_table_entries[num2 + i].root_num = (long)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2]))), (int)array[3]);
					if (decimal.Compare(new decimal(this.encoding), 1m) == 0)
					{
						this.master_table_entries[num2 + i].sql_statement = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
					}
					else if (decimal.Compare(new decimal(this.encoding), 2m) == 0)
					{
						this.master_table_entries[num2 + i].sql_statement = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
					}
					else if (decimal.Compare(new decimal(this.encoding), 3m) == 0)
					{
						this.master_table_entries[num2 + i].sql_statement = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
					}
				}
				return;
			}
			if (this.db_bytes[(int)A_1] == 5)
			{
				int num8 = (int)Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(A_1), 3m)), 2)), 1m));
				for (int j = 0; j <= num8; j++)
				{
					ushort num9 = (ushort)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(A_1), 12m), new decimal(j * 2))), 2);
					if (decimal.Compare(new decimal(A_1), 100m) == 0)
					{
						this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)num9, 4)), 1m), new decimal((int)this.page_size))));
					}
					else
					{
						this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)(A_1 + (ulong)num9), 4)), 1m), new decimal((int)this.page_size))));
					}
				}
				this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(A_1), 8m)), 4)), 1m), new decimal((int)this.page_size))));
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00014E30 File Offset: 0x00013030
		public bool ReadTable(string TableName)
		{
			int num = -1;
			int num2 = this.master_table_entries.Length - 1;
			for (int i = 0; i <= num2; i++)
			{
				if (string.Compare(this.master_table_entries[i].item_name.ToLower(), TableName.ToLower(), StringComparison.Ordinal) == 0)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return false;
			}
			string[] array = this.master_table_entries[num].sql_statement.Substring(this.master_table_entries[num].sql_statement.IndexOf(ChromV265450.Strings.Get(107394495), StringComparison.Ordinal) + 1).Split(new char[]
			{
				','
			});
			int num3 = array.Length - 1;
			for (int j = 0; j <= num3; j++)
			{
				array[j] = array[j].TrimStart(new char[0]);
				int num4 = array[j].IndexOf(ChromV265450.Strings.Get(107394490), StringComparison.Ordinal);
				if (num4 > 0)
				{
					array[j] = array[j].Substring(0, num4);
				}
				if (array[j].IndexOf(ChromV265450.Strings.Get(107394485), StringComparison.Ordinal) == 0)
				{
					break;
				}
				this.field_names = (string[])Utils.CopyArray(this.field_names, new string[j + 1]);
				this.field_names[j] = array[j];
			}
			return this.ReadTableFromOffset((ulong)((this.master_table_entries[num].root_num - 1L) * (long)((ulong)this.page_size)));
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00014FC4 File Offset: 0x000131C4
		private bool ReadTableFromOffset(ulong A_1)
		{
			if (this.db_bytes[(int)A_1] == 13)
			{
				int num = Convert.ToInt32(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(A_1), 3m)), 2)), 1m));
				int num2 = 0;
				if (this.table_entries != null)
				{
					num2 = this.table_entries.Length;
					this.table_entries = (SQLiteHandler.table_entry[])Utils.CopyArray(this.table_entries, new SQLiteHandler.table_entry[this.table_entries.Length + num + 1]);
				}
				else
				{
					this.table_entries = new SQLiteHandler.table_entry[num + 1];
				}
				int num3 = num;
				for (int i = 0; i <= num3; i++)
				{
					SQLiteHandler.record_header_field[] array = null;
					ulong num4 = this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(A_1), 8m), new decimal(i * 2))), 2);
					if (decimal.Compare(new decimal(A_1), 100m) != 0)
					{
						num4 += A_1;
					}
					int num5 = this.GVL((int)num4);
					int num6 = this.GVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)));
					this.table_entries[num2 + i].row_id = this.CVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)), num6);
					num4 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num6), new decimal(num4))), 1m));
					num5 = this.GVL((int)num4);
					num6 = num5;
					long num7 = this.CVL((int)num4, num5);
					long num8 = Convert.ToInt64(decimal.Add(decimal.Subtract(new decimal(num4), new decimal(num5)), 1m));
					int num9 = 0;
					while (num8 < num7)
					{
						array = (SQLiteHandler.record_header_field[])Utils.CopyArray(array, new SQLiteHandler.record_header_field[num9 + 1]);
						num5 = num6 + 1;
						num6 = this.GVL(num5);
						array[num9].type = this.CVL(num5, num6);
						if (array[num9].type > 9L)
						{
							if (this.IsOdd(array[num9].type))
							{
								array[num9].size = (long)Math.Round((double)(array[num9].type - 13L) / 2.0);
							}
							else
							{
								array[num9].size = (long)Math.Round((double)(array[num9].type - 12L) / 2.0);
							}
						}
						else
						{
							array[num9].size = (long)((ulong)this.SQLDataTypeSize[(int)array[num9].type]);
						}
						num8 = num8 + (long)(num6 - num5) + 1L;
						num9++;
					}
					if (array != null)
					{
						this.table_entries[num2 + i].content = new string[array.Length - 1 + 1];
						int num10 = 0;
						int num11 = array.Length - 1;
						for (int j = 0; j <= num11; j++)
						{
							if (array[j].type > 9L)
							{
								if (!this.IsOdd(array[j].type))
								{
									if (decimal.Compare(new decimal(this.encoding), 1m) == 0)
									{
										this.table_entries[num2 + i].content[j] = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].size);
									}
									else if (decimal.Compare(new decimal(this.encoding), 2m) == 0)
									{
										this.table_entries[num2 + i].content[j] = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].size);
									}
									else if (decimal.Compare(new decimal(this.encoding), 3m) == 0)
									{
										this.table_entries[num2 + i].content[j] = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].size);
									}
								}
								else
								{
									this.table_entries[num2 + i].content[j] = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].size);
								}
							}
							else
							{
								this.table_entries[num2 + i].content[j] = Conversions.ToString(this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].size));
							}
							num10 += (int)array[j].size;
						}
					}
				}
			}
			else if (this.db_bytes[(int)A_1] == 5)
			{
				int num12 = (int)Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(A_1), 3m)), 2)), 1m));
				for (int k = 0; k <= num12; k++)
				{
					ushort num13 = (ushort)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(A_1), 12m), new decimal(k * 2))), 2);
					this.ReadTableFromOffset(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)(A_1 + (ulong)num13), 4)), 1m), new decimal((int)this.page_size))));
				}
				this.ReadTableFromOffset(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(A_1), 8m)), 4)), 1m), new decimal((int)this.page_size))));
			}
			return true;
		}

		// Token: 0x04000122 RID: 290
		private byte[] db_bytes;

		// Token: 0x04000123 RID: 291
		private ulong encoding;

		// Token: 0x04000124 RID: 292
		private string[] field_names;

		// Token: 0x04000125 RID: 293
		private SQLiteHandler.sqlite_master_entry[] master_table_entries;

		// Token: 0x04000126 RID: 294
		private ushort page_size;

		// Token: 0x04000127 RID: 295
		private byte[] SQLDataTypeSize = new byte[]
		{
			0,
			1,
			2,
			3,
			4,
			6,
			8,
			8,
			0,
			0
		};

		// Token: 0x04000128 RID: 296
		private SQLiteHandler.table_entry[] table_entries;

		// Token: 0x0200025D RID: 605
		private struct record_header_field
		{
			// Token: 0x04000A88 RID: 2696
			public long size;

			// Token: 0x04000A89 RID: 2697
			public long type;
		}

		// Token: 0x0200025E RID: 606
		private struct sqlite_master_entry
		{
			// Token: 0x04000A8A RID: 2698
			public long row_id;

			// Token: 0x04000A8B RID: 2699
			public string item_type;

			// Token: 0x04000A8C RID: 2700
			public string item_name;

			// Token: 0x04000A8D RID: 2701
			public string astable_name;

			// Token: 0x04000A8E RID: 2702
			public long root_num;

			// Token: 0x04000A8F RID: 2703
			public string sql_statement;
		}

		// Token: 0x0200025F RID: 607
		private struct table_entry
		{
			// Token: 0x04000A90 RID: 2704
			public long row_id;

			// Token: 0x04000A91 RID: 2705
			public string[] content;
		}
	}
}
