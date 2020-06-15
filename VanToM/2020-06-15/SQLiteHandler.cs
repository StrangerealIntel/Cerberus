using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Stub
{
	// Token: 0x0200001D RID: 29
	public class SQLiteHandler
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00004F48 File Offset: 0x00003148
		private ushort ToBigEndian16Bit(ushort value)
		{
			return checked((ushort)((int)(value & 255) << 8 | (value & 65280) >> 8));
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004F74 File Offset: 0x00003174
		private uint ToBigEndian32Bit(uint value)
		{
			return checked((uint)(unchecked(((ulong)value & 255UL) << 24 | ((ulong)value & 65280UL) << 8 | ((ulong)value & 16711680UL) >> 8 | ((ulong)value & 18446744073692774400UL) >> 24)));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004FBC File Offset: 0x000031BC
		private ulong ToBigEndian64Bit(ulong value)
		{
			return checked((ulong)(((long)value & 255L) << 56 | ((long)value & 65280L) << 40 | ((long)value & 16711680L) << 24 | ((long)value & (long)(unchecked((ulong)-16777216))) << 8 | ((long)value & 1095216660480L) >> 8 | ((long)value & 280375465082880L) >> 24 | ((long)value & 71776119061217280L) >> 40 | ((long)value & -72057594037927936L) >> 56));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005044 File Offset: 0x00003244
		private int GVL(int startIndex)
		{
			bool flag = startIndex > this.db_bytes.Length;
			checked
			{
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					int num = startIndex + 8;
					int num2 = startIndex;
					for (;;)
					{
						int num3 = num2;
						int num4 = num;
						if (num3 > num4)
						{
							goto Block_4;
						}
						flag = (num2 > this.db_bytes.Length - 1);
						if (flag)
						{
							break;
						}
						flag = ((this.db_bytes[num2] & 128) != 128);
						if (flag)
						{
							goto Block_3;
						}
						num2++;
					}
					return 0;
					Block_3:
					return num2;
					Block_4:
					result = startIndex + 8;
				}
				return result;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000050D4 File Offset: 0x000032D4
		private long CVL(int startIndex, int endIndex)
		{
			checked
			{
				endIndex++;
				byte[] array = new byte[8];
				int num = endIndex - startIndex;
				bool flag = false;
				bool flag2 = num == 0 | num > 9;
				long result;
				if (flag2)
				{
					result = 0L;
				}
				else
				{
					flag2 = (num == 1);
					if (flag2)
					{
						array[0] = (this.db_bytes[startIndex] & 127);
						result = BitConverter.ToInt64(array, 0);
					}
					else
					{
						flag2 = (num == 9);
						if (flag2)
						{
							flag = true;
						}
						int num2 = 1;
						int num3 = 7;
						int num4 = 0;
						flag2 = flag;
						if (flag2)
						{
							array[0] = this.db_bytes[endIndex - 1];
							endIndex--;
							num4 = 1;
						}
						for (int i = endIndex - 1; i >= startIndex; i += -1)
						{
							flag2 = (i - 1 >= startIndex);
							if (flag2)
							{
								array[num4] = (byte)(unchecked(((int)((byte)((uint)this.db_bytes[i] >> (checked(num2 - 1) & 7))) & 255 >> num2) | (int)((byte)(this.db_bytes[checked(i - 1)] << (num3 & 7)))));
								num2++;
								num4++;
								num3--;
							}
							else
							{
								flag2 = !flag;
								if (flag2)
								{
									array[num4] = (byte)((int)(unchecked((byte)((uint)this.db_bytes[i] >> (checked(num2 - 1) & 7)))) & 255 >> num2);
								}
							}
						}
						result = BitConverter.ToInt64(array, 0);
					}
				}
				return result;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000522C File Offset: 0x0000342C
		private bool IsOdd(long value)
		{
			return (value & 1L) == 1L;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000524C File Offset: 0x0000344C
		private ulong ConvertToInteger(int startIndex, int Size)
		{
			bool flag = Size > 8 | Size == 0;
			checked
			{
				ulong result;
				if (flag)
				{
					result = 0UL;
				}
				else
				{
					ulong num = 0UL;
					int num2 = 0;
					int num3 = Size - 1;
					int num4 = num2;
					for (;;)
					{
						int num5 = num4;
						int num6 = num3;
						if (num5 > num6)
						{
							break;
						}
						num = (num << 8 | unchecked((ulong)this.db_bytes[checked(startIndex + num4)]));
						num4++;
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000052AC File Offset: 0x000034AC
		private void ReadMasterTable(ulong Offset)
		{
			checked
			{
				bool flag = this.db_bytes[(int)Offset] == 13;
				if (flag)
				{
					ushort num = this.ToBigEndian16Bit(BitConverter.ToUInt16(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)))) - 1;
					int num2 = 0;
					flag = (this.master_table_entries != null);
					if (flag)
					{
						num2 = this.master_table_entries.Length - 1;
						this.master_table_entries = (SQLiteHandler.sqlite_master_entry[])Utils.CopyArray((Array)this.master_table_entries, new SQLiteHandler.sqlite_master_entry[this.master_table_entries.Length - 1 + (int)num + 1]);
					}
					else
					{
						this.master_table_entries = new SQLiteHandler.sqlite_master_entry[(int)(num + 1)];
					}
					int num3 = 0;
					int num4 = (int)num;
					int num5 = num3;
					for (;;)
					{
						int num6 = num5;
						int num7 = num4;
						if (num6 > num7)
						{
							break;
						}
						ulong num8 = unchecked((ulong)this.ToBigEndian16Bit(BitConverter.ToUInt16(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8m), new decimal(checked(num5 * 2)))))));
						flag = (decimal.Compare(new decimal(Offset), 100m) != 0);
						if (flag)
						{
							num8 += Offset;
						}
						int num9 = this.GVL((int)num8);
						long num10 = this.CVL((int)num8, num9);
						int num11 = this.GVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), decimal.Subtract(new decimal(num9), new decimal(num8))), 1m)));
						this.master_table_entries[num2 + num5].row_id = this.CVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), decimal.Subtract(new decimal(num9), new decimal(num8))), 1m)), num11);
						num8 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num8), decimal.Subtract(new decimal(num11), new decimal(num8))), 1m));
						num9 = this.GVL((int)num8);
						num11 = num9;
						long value = this.CVL((int)num8, num9);
						long[] array = new long[5];
						int num12 = 0;
						bool flag2;
						int num13;
						do
						{
							num9 = num11 + 1;
							num11 = this.GVL(num9);
							array[num12] = this.CVL(num9, num11);
							flag = (array[num12] > 9L);
							if (flag)
							{
								flag2 = this.IsOdd(array[num12]);
								if (flag2)
								{
									array[num12] = (long)Math.Round((double)(array[num12] - 13L) / 2.0);
								}
								else
								{
									array[num12] = (long)Math.Round((double)(array[num12] - 12L) / 2.0);
								}
							}
							else
							{
								array[num12] = (long)(unchecked((ulong)this.SQLDataTypeSize[checked((int)array[num12])]));
							}
							num12++;
							num13 = num12;
							num7 = 4;
						}
						while (num13 <= num7);
						flag2 = (decimal.Compare(new decimal(this.encoding), 1m) == 0);
						if (flag2)
						{
							this.master_table_entries[num2 + num5].item_type = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num8), new decimal(value))), (int)array[0]);
						}
						else
						{
							flag2 = (decimal.Compare(new decimal(this.encoding), 2m) == 0);
							if (flag2)
							{
								this.master_table_entries[num2 + num5].item_type = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num8), new decimal(value))), (int)array[0]);
							}
							else
							{
								flag2 = (decimal.Compare(new decimal(this.encoding), 3m) == 0);
								if (flag2)
								{
									this.master_table_entries[num2 + num5].item_type = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num8), new decimal(value))), (int)array[0]);
								}
							}
						}
						flag2 = (decimal.Compare(new decimal(this.encoding), 1m) == 0);
						if (flag2)
						{
							this.master_table_entries[num2 + num5].item_name = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), new decimal(value)), new decimal(array[0]))), (int)array[1]);
						}
						else
						{
							flag2 = (decimal.Compare(new decimal(this.encoding), 2m) == 0);
							if (flag2)
							{
								this.master_table_entries[num2 + num5].item_name = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), new decimal(value)), new decimal(array[0]))), (int)array[1]);
							}
							else
							{
								flag2 = (decimal.Compare(new decimal(this.encoding), 3m) == 0);
								if (flag2)
								{
									this.master_table_entries[num2 + num5].item_name = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), new decimal(value)), new decimal(array[0]))), (int)array[1]);
								}
							}
						}
						this.master_table_entries[num2 + num5].root_num = (long)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num8), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2]))), (int)array[3]);
						flag2 = (decimal.Compare(new decimal(this.encoding), 1m) == 0);
						if (flag2)
						{
							this.master_table_entries[num2 + num5].sql_statement = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num8), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
						}
						else
						{
							flag2 = (decimal.Compare(new decimal(this.encoding), 2m) == 0);
							if (flag2)
							{
								this.master_table_entries[num2 + num5].sql_statement = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num8), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
							}
							else
							{
								flag2 = (decimal.Compare(new decimal(this.encoding), 3m) == 0);
								if (flag2)
								{
									this.master_table_entries[num2 + num5].sql_statement = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num8), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
								}
							}
						}
						num5++;
					}
				}
				else
				{
					bool flag2 = this.db_bytes[(int)Offset] == 5;
					if (flag2)
					{
						ushort num14 = this.ToBigEndian16Bit(BitConverter.ToUInt16(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)))) - 1;
						int num15 = 0;
						int num16 = (int)num14;
						int num17 = num15;
						for (;;)
						{
							int num18 = num17;
							int num7 = num16;
							if (num18 > num7)
							{
								break;
							}
							ushort num19 = this.ToBigEndian16Bit(BitConverter.ToUInt16(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12m), new decimal(num17 * 2)))));
							flag2 = (decimal.Compare(new decimal(Offset), 100m) == 0);
							if (flag2)
							{
								this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)num19, 4)), 1m), new decimal((int)this.page_size))));
							}
							else
							{
								this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)(Offset + unchecked((ulong)num19)), 4)), 1m), new decimal((int)this.page_size))));
							}
							num17++;
						}
						this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 8m)), 4)), 1m), new decimal((int)this.page_size))));
					}
				}
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005C24 File Offset: 0x00003E24
		private bool ReadTableFromOffset(ulong Offset)
		{
			checked
			{
				bool flag = this.db_bytes[(int)Offset] == 13;
				if (flag)
				{
					ushort num = this.ToBigEndian16Bit(BitConverter.ToUInt16(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)))) - 1;
					int num2 = 0;
					flag = (this.table_entries != null);
					if (flag)
					{
						num2 = this.table_entries.Length - 1;
						this.table_entries = (SQLiteHandler.table_entry[])Utils.CopyArray((Array)this.table_entries, new SQLiteHandler.table_entry[this.table_entries.Length - 1 + (int)num + 1]);
					}
					else
					{
						this.table_entries = new SQLiteHandler.table_entry[(int)(num + 1)];
					}
					int num3 = 0;
					int num4 = (int)num;
					int num5 = num3;
					for (;;)
					{
						int num6 = num5;
						int num7 = num4;
						if (num6 > num7)
						{
							break;
						}
						ulong num8 = unchecked((ulong)this.ToBigEndian16Bit(BitConverter.ToUInt16(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8m), new decimal(checked(num5 * 2)))))));
						flag = (decimal.Compare(new decimal(Offset), 100m) != 0);
						if (flag)
						{
							num8 += Offset;
						}
						int num9 = this.GVL((int)num8);
						long num10 = this.CVL((int)num8, num9);
						int num11 = this.GVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), decimal.Subtract(new decimal(num9), new decimal(num8))), 1m)));
						this.table_entries[num2 + num5].row_id = this.CVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), decimal.Subtract(new decimal(num9), new decimal(num8))), 1m)), num11);
						num8 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num8), decimal.Subtract(new decimal(num11), new decimal(num8))), 1m));
						num9 = this.GVL((int)num8);
						num11 = num9;
						long num12 = this.CVL((int)num8, num9);
						long num13 = Convert.ToInt64(decimal.Add(decimal.Subtract(new decimal(num8), new decimal(num9)), 1m));
						int num14 = 0;
						SQLiteHandler.record_header_field[] array;
						while (num13 < num12)
						{
							array = (SQLiteHandler.record_header_field[])Utils.CopyArray((Array)array, new SQLiteHandler.record_header_field[num14 + 1]);
							num9 = num11 + 1;
							num11 = this.GVL(num9);
							array[num14].type = this.CVL(num9, num11);
							flag = (array[num14].type > 9L);
							if (flag)
							{
								bool flag2 = this.IsOdd(array[num14].type);
								if (flag2)
								{
									array[num14].size = (long)Math.Round((double)(array[num14].type - 13L) / 2.0);
								}
								else
								{
									array[num14].size = (long)Math.Round((double)(array[num14].type - 12L) / 2.0);
								}
							}
							else
							{
								array[num14].size = (long)(unchecked((ulong)this.SQLDataTypeSize[checked((int)array[num14].type)]));
							}
							num13 = num13 + unchecked((long)(checked(num11 - num9))) + 1L;
							num14++;
						}
						this.table_entries[num2 + num5].content = new string[array.Length - 1 + 1];
						int num15 = 0;
						int num16 = 0;
						int num17 = array.Length - 1;
						int num18 = num16;
						for (;;)
						{
							int num19 = num18;
							num7 = num17;
							if (num19 > num7)
							{
								break;
							}
							bool flag2 = array[num18].type > 9L;
							if (flag2)
							{
								flag = !this.IsOdd(array[num18].type);
								if (flag)
								{
									bool flag3 = decimal.Compare(new decimal(this.encoding), 1m) == 0;
									if (flag3)
									{
										this.table_entries[num2 + num5].content[num18] = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), new decimal(num12)), new decimal(num15))), (int)array[num18].size);
									}
									else
									{
										flag3 = (decimal.Compare(new decimal(this.encoding), 2m) == 0);
										if (flag3)
										{
											this.table_entries[num2 + num5].content[num18] = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), new decimal(num12)), new decimal(num15))), (int)array[num18].size);
										}
										else
										{
											flag3 = (decimal.Compare(new decimal(this.encoding), 3m) == 0);
											if (flag3)
											{
												this.table_entries[num2 + num5].content[num18] = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), new decimal(num12)), new decimal(num15))), (int)array[num18].size);
											}
										}
									}
								}
								else
								{
									this.table_entries[num2 + num5].content[num18] = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), new decimal(num12)), new decimal(num15))), (int)array[num18].size);
								}
							}
							else
							{
								this.table_entries[num2 + num5].content[num18] = Conversions.ToString(this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num8), new decimal(num12)), new decimal(num15))), (int)array[num18].size));
							}
							num15 = (int)(unchecked((long)num15) + array[num18].size);
							num18++;
						}
						num5++;
					}
				}
				else
				{
					bool flag3 = this.db_bytes[(int)Offset] == 5;
					if (flag3)
					{
						ushort num20 = this.ToBigEndian16Bit(BitConverter.ToUInt16(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)))) - 1;
						int num21 = 0;
						int num22 = (int)num20;
						int num23 = num21;
						for (;;)
						{
							int num24 = num23;
							int num7 = num22;
							if (num24 > num7)
							{
								break;
							}
							ushort num25 = this.ToBigEndian16Bit(BitConverter.ToUInt16(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12m), new decimal(num23 * 2)))));
							this.ReadTableFromOffset(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)(Offset + unchecked((ulong)num25)), 4)), 1m), new decimal((int)this.page_size))));
							num23++;
						}
						this.ReadTableFromOffset(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 8m)), 4)), 1m), new decimal((int)this.page_size))));
					}
				}
				return true;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000063CC File Offset: 0x000045CC
		public bool ReadTable(string TableName)
		{
			int num = -1;
			int num2 = 0;
			int num3 = this.master_table_entries.Length;
			int num4 = num2;
			checked
			{
				bool flag;
				for (;;)
				{
					int num5 = num4;
					int num6 = num3;
					if (num5 > num6)
					{
						goto IL_54;
					}
					flag = (this.master_table_entries[num4].item_name.ToLower().CompareTo(TableName.ToLower()) == 0);
					if (flag)
					{
						break;
					}
					num4++;
				}
				num = num4;
				IL_54:
				flag = (num == -1);
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					string[] array = this.master_table_entries[num].sql_statement.Substring(this.master_table_entries[num].sql_statement.IndexOf("(") + 1).Split(new char[]
					{
						','
					});
					int num7 = 0;
					int num8 = array.Length - 1;
					int num9 = num7;
					for (;;)
					{
						int num10 = num9;
						int num6 = num8;
						if (num10 > num6)
						{
							break;
						}
						array[num9] = Strings.LTrim(array[num9]);
						int num11 = array[num9].IndexOf(" ");
						flag = (num11 > 0);
						if (flag)
						{
							array[num9] = array[num9].Substring(0, num11);
						}
						flag = (array[num9].IndexOf("UNIQUE") == 0);
						if (flag)
						{
							break;
						}
						this.field_names = (string[])Utils.CopyArray((Array)this.field_names, new string[num9 + 1]);
						this.field_names[num9] = array[num9];
						num9++;
					}
					result = this.ReadTableFromOffset((ulong)((this.master_table_entries[num].root_num - 1L) * (long)(unchecked((ulong)this.page_size))));
				}
				return result;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006574 File Offset: 0x00004774
		public int GetRowCount()
		{
			return this.table_entries.Length;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006594 File Offset: 0x00004794
		public string GetValue(int row_num, int field)
		{
			bool flag = row_num >= this.table_entries.Length;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				flag = (field >= this.table_entries[row_num].content.Length);
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.table_entries[row_num].content[field];
				}
			}
			return result;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006600 File Offset: 0x00004800
		public string GetValue(int row_num, string field)
		{
			int num = -1;
			int num2 = 0;
			int num3 = this.field_names.Length;
			int num4 = num2;
			checked
			{
				bool flag;
				for (;;)
				{
					int num5 = num4;
					int num6 = num3;
					if (num5 > num6)
					{
						goto IL_4B;
					}
					flag = (this.field_names[num4].ToLower().CompareTo(field.ToLower()) == 0);
					if (flag)
					{
						break;
					}
					num4++;
				}
				num = num4;
				IL_4B:
				flag = (num == -1);
				string result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetValue(row_num, num);
				}
				return result;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000667C File Offset: 0x0000487C
		public string[] GetTableNames()
		{
			int num = 0;
			int num2 = 0;
			checked
			{
				int num3 = this.master_table_entries.Length - 1;
				int num4 = num2;
				string[] array;
				for (;;)
				{
					int num5 = num4;
					int num6 = num3;
					if (num5 > num6)
					{
						break;
					}
					bool flag = Operators.CompareString(this.master_table_entries[num4].item_type, "table", false) == 0;
					if (flag)
					{
						array = (string[])Utils.CopyArray((Array)array, new string[num + 1]);
						array[num] = this.master_table_entries[num4].item_name;
						num++;
					}
					num4++;
				}
				return array;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006714 File Offset: 0x00004914
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public SQLiteHandler(string baseName)
		{
			this.SQLDataTypeSize = new byte[]
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
			bool flag = File.Exists(baseName);
			if (flag)
			{
				FileSystem.FileOpen(1, baseName, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared, -1);
				string s = Strings.Space(checked((int)FileSystem.LOF(1)));
				FileSystem.FileGet(1, ref s, -1L, false);
				FileSystem.FileClose(new int[]
				{
					1
				});
				this.db_bytes = Encoding.Default.GetBytes(s);
				flag = (Encoding.Default.GetString(this.db_bytes, 0, 15).CompareTo("SQLite format 3") != 0);
				if (!flag)
				{
					flag = (this.db_bytes[52] != 0);
					if (!flag)
					{
						flag = ((ulong)this.ToBigEndian32Bit(checked((uint)BitConverter.ToInt32(this.db_bytes, 44))) >= 4UL);
						if (!flag)
						{
							this.page_size = checked((ushort)this.ConvertToInteger(16, 2));
							this.encoding = this.ConvertToInteger(56, 4);
							flag = (decimal.Compare(new decimal(this.encoding), 0m) == 0);
							if (flag)
							{
								this.encoding = 1UL;
							}
							this.ReadMasterTable(100UL);
						}
					}
				}
			}
		}

		// Token: 0x0400003D RID: 61
		private byte[] db_bytes;

		// Token: 0x0400003E RID: 62
		private ushort page_size;

		// Token: 0x0400003F RID: 63
		private ulong encoding;

		// Token: 0x04000040 RID: 64
		private SQLiteHandler.sqlite_master_entry[] master_table_entries;

		// Token: 0x04000041 RID: 65
		private byte[] SQLDataTypeSize;

		// Token: 0x04000042 RID: 66
		private SQLiteHandler.table_entry[] table_entries;

		// Token: 0x04000043 RID: 67
		private string[] field_names;

		// Token: 0x0200001E RID: 30
		private struct record_header_field
		{
			// Token: 0x04000044 RID: 68
			public long size;

			// Token: 0x04000045 RID: 69
			public long type;
		}

		// Token: 0x0200001F RID: 31
		private struct table_entry
		{
			// Token: 0x04000046 RID: 70
			public long row_id;

			// Token: 0x04000047 RID: 71
			public string[] content;
		}

		// Token: 0x02000020 RID: 32
		private struct sqlite_master_entry
		{
			// Token: 0x04000048 RID: 72
			public long row_id;

			// Token: 0x04000049 RID: 73
			public string item_type;

			// Token: 0x0400004A RID: 74
			public string item_name;

			// Token: 0x0400004B RID: 75
			public string astable_name;

			// Token: 0x0400004C RID: 76
			public long root_num;

			// Token: 0x0400004D RID: 77
			public string sql_statement;
		}
	}
}
