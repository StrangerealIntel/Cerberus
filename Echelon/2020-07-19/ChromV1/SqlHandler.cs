using System;
using System.IO;
using System.Text;
using SmartAssembly.StringsEncoding;

namespace ChromV1
{
	// Token: 0x02000067 RID: 103
	internal sealed class SqlHandler
	{
		// Token: 0x0600023B RID: 571 RVA: 0x00011AA4 File Offset: 0x0000FCA4
		public SqlHandler(string A_1)
		{
			this._fileBytes = File.ReadAllBytes(A_1);
			this._pageSize = this.ConvertToULong(16, 2);
			this._dbEncoding = this.ConvertToULong(56, 4);
			this.ReadMasterTable(100L);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00011B08 File Offset: 0x0000FD08
		public string GetValue(int A_1, int A_2)
		{
			string result;
			try
			{
				if (A_1 >= this._tableEntries.Length)
				{
					result = null;
				}
				else
				{
					result = ((A_2 >= this._tableEntries[A_1].Content.Length) ? null : this._tableEntries[A_1].Content[A_2]);
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00011B80 File Offset: 0x0000FD80
		public int GetRowCount()
		{
			return this._tableEntries.Length;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00011B8C File Offset: 0x0000FD8C
		private bool ReadTableFromOffset(ulong A_1)
		{
			bool result;
			try
			{
				if (this._fileBytes[(int)(checked((IntPtr)A_1))] == 13)
				{
					uint num = (uint)(this.ConvertToULong((int)A_1 + 3, 2) - 1UL);
					int num2 = 0;
					if (this._tableEntries != null)
					{
						num2 = this._tableEntries.Length;
						Array.Resize<SqlHandler.TableEntry>(ref this._tableEntries, this._tableEntries.Length + (int)num + 1);
					}
					else
					{
						this._tableEntries = new SqlHandler.TableEntry[num + 1u];
					}
					for (uint num3 = 0u; num3 <= num; num3 += 1u)
					{
						ulong num4 = this.ConvertToULong((int)A_1 + 8 + (int)(num3 * 2u), 2);
						if (A_1 != 100UL)
						{
							num4 += A_1;
						}
						int num5 = this.Gvl((int)num4);
						this.Cvl((int)num4, num5);
						int num6 = this.Gvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1UL));
						this.Cvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1UL), num6);
						ulong num7 = num4 + (ulong)((long)num6 - (long)num4 + 1L);
						int num8 = this.Gvl((int)num7);
						int num9 = num8;
						long num10 = this.Cvl((int)num7, num8);
						SqlHandler.RecordHeaderField[] array = null;
						long num11 = (long)(num7 - (ulong)((long)num8) + 1UL);
						int num12 = 0;
						while (num11 < num10)
						{
							Array.Resize<SqlHandler.RecordHeaderField>(ref array, num12 + 1);
							int num13 = num9 + 1;
							num9 = this.Gvl(num13);
							array[num12].Type = this.Cvl(num13, num9);
							array[num12].Size = (long)((array[num12].Type <= 9L) ? ((ulong)this._sqlDataTypeSize[(int)(checked((IntPtr)array[num12].Type))]) : ((ulong)((!SqlHandler.IsOdd(array[num12].Type)) ? ((array[num12].Type - 12L) / 2L) : ((array[num12].Type - 13L) / 2L))));
							num11 = num11 + (long)(num9 - num13) + 1L;
							num12++;
						}
						if (array != null)
						{
							this._tableEntries[num2 + (int)num3].Content = new string[array.Length];
							int num14 = 0;
							for (int i = 0; i <= array.Length - 1; i++)
							{
								if (array[i].Type > 9L)
								{
									if (!SqlHandler.IsOdd(array[i].Type))
									{
										if (this._dbEncoding == 1UL)
										{
											this._tableEntries[num2 + (int)num3].Content[i] = Encoding.Default.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size);
										}
										else if (this._dbEncoding == 2UL)
										{
											this._tableEntries[num2 + (int)num3].Content[i] = Encoding.Unicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size);
										}
										else if (this._dbEncoding == 3UL)
										{
											this._tableEntries[num2 + (int)num3].Content[i] = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size);
										}
									}
									else
									{
										this._tableEntries[num2 + (int)num3].Content[i] = Encoding.Default.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size);
									}
								}
								else
								{
									this._tableEntries[num2 + (int)num3].Content[i] = Convert.ToString(this.ConvertToULong((int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size));
								}
								num14 += (int)array[i].Size;
							}
						}
					}
				}
				else if (this._fileBytes[(int)(checked((IntPtr)A_1))] == 5)
				{
					uint num15 = (uint)(this.ConvertToULong((int)(A_1 + 3UL), 2) - 1UL);
					for (uint num16 = 0u; num16 <= num15; num16 += 1u)
					{
						uint num17 = (uint)this.ConvertToULong((int)A_1 + 12 + (int)(num16 * 2u), 2);
						this.ReadTableFromOffset((this.ConvertToULong((int)(A_1 + (ulong)num17), 4) - 1UL) * this._pageSize);
					}
					this.ReadTableFromOffset((this.ConvertToULong((int)(A_1 + 8UL), 4) - 1UL) * this._pageSize);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0001203C File Offset: 0x0001023C
		private void ReadMasterTable(long A_1)
		{
			try
			{
				byte b = this._fileBytes[(int)(checked((IntPtr)A_1))];
				if (b != 5)
				{
					if (b == 13)
					{
						ulong num = this.ConvertToULong((int)A_1 + 3, 2) - 1UL;
						int num2 = 0;
						if (this._masterTableEntries != null)
						{
							num2 = this._masterTableEntries.Length;
							Array.Resize<SqlHandler.SqliteMasterEntry>(ref this._masterTableEntries, this._masterTableEntries.Length + (int)num + 1);
						}
						else
						{
							this._masterTableEntries = new SqlHandler.SqliteMasterEntry[num + 1UL];
						}
						for (ulong num3 = 0UL; num3 <= num; num3 += 1UL)
						{
							ulong num4 = this.ConvertToULong((int)A_1 + 8 + (int)num3 * 2, 2);
							if (A_1 != 100L)
							{
								num4 += (ulong)A_1;
							}
							int num5 = this.Gvl((int)num4);
							this.Cvl((int)num4, num5);
							int num6 = this.Gvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1UL));
							this.Cvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1UL), num6);
							ulong num7 = num4 + (ulong)((long)num6 - (long)num4 + 1L);
							int num8 = this.Gvl((int)num7);
							int num9 = num8;
							long num10 = this.Cvl((int)num7, num8);
							long[] array = new long[5];
							for (int i = 0; i <= 4; i++)
							{
								int num11 = num9 + 1;
								num9 = this.Gvl(num11);
								array[i] = this.Cvl(num11, num9);
								array[i] = (long)((array[i] <= 9L) ? ((ulong)this._sqlDataTypeSize[(int)(checked((IntPtr)array[i]))]) : ((ulong)((!SqlHandler.IsOdd(array[i])) ? ((array[i] - 12L) / 2L) : ((array[i] - 13L) / 2L))));
							}
							if (this._dbEncoding == 1UL || this._dbEncoding == 2UL)
							{
								if (this._dbEncoding == 1UL)
								{
									this._masterTableEntries[num2 + (int)num3].ItemName = Encoding.Default.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0]), (int)array[1]);
								}
								else if (this._dbEncoding == 2UL)
								{
									this._masterTableEntries[num2 + (int)num3].ItemName = Encoding.Unicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0]), (int)array[1]);
								}
								else if (this._dbEncoding == 3UL)
								{
									this._masterTableEntries[num2 + (int)num3].ItemName = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0]), (int)array[1]);
								}
							}
							this._masterTableEntries[num2 + (int)num3].RootNum = (long)this.ConvertToULong((int)(num7 + (ulong)num10 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2]), (int)array[3]);
							if (this._dbEncoding == 1UL)
							{
								this._masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.Default.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
							}
							else if (this._dbEncoding == 2UL)
							{
								this._masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.Unicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
							}
							else if (this._dbEncoding == 3UL)
							{
								this._masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
							}
						}
					}
				}
				else
				{
					uint num12 = (uint)(this.ConvertToULong((int)A_1 + 3, 2) - 1UL);
					for (int j = 0; j <= (int)num12; j++)
					{
						uint num13 = (uint)this.ConvertToULong((int)A_1 + 12 + j * 2, 2);
						if (A_1 == 100L)
						{
							this.ReadMasterTable((long)((this.ConvertToULong((int)num13, 4) - 1UL) * this._pageSize));
						}
						else
						{
							this.ReadMasterTable((long)((this.ConvertToULong((int)(A_1 + (long)((ulong)num13)), 4) - 1UL) * this._pageSize));
						}
					}
					this.ReadMasterTable((long)((this.ConvertToULong((int)A_1 + 8, 4) - 1UL) * this._pageSize));
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000124D0 File Offset: 0x000106D0
		public bool ReadTable(string A_1)
		{
			bool result;
			try
			{
				int num = -1;
				for (int i = 0; i <= this._masterTableEntries.Length; i++)
				{
					if (string.Compare(this._masterTableEntries[i].ItemName.ToLower(), A_1.ToLower(), StringComparison.Ordinal) == 0)
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
					string[] array = this._masterTableEntries[num].SqlStatement.Substring(this._masterTableEntries[num].SqlStatement.IndexOf(Strings.Get(107394796), StringComparison.Ordinal) + 1).Split(new char[]
					{
						','
					});
					for (int j = 0; j <= array.Length - 1; j++)
					{
						array[j] = array[j].TrimStart(new char[0]);
						int num2 = array[j].IndexOf(' ');
						if (num2 > 0)
						{
							array[j] = array[j].Substring(0, num2);
						}
						if (array[j].IndexOf(Strings.Get(107394791), StringComparison.Ordinal) != 0)
						{
							Array.Resize<string>(ref this._fieldNames, j + 1);
							this._fieldNames[j] = array[j];
						}
					}
					result = this.ReadTableFromOffset((ulong)((this._masterTableEntries[num].RootNum - 1L) * (long)this._pageSize));
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00012670 File Offset: 0x00010870
		private ulong ConvertToULong(int A_1, int A_2)
		{
			ulong result;
			try
			{
				if (A_2 > 8 | A_2 == 0)
				{
					result = 0UL;
				}
				else
				{
					ulong num = 0UL;
					for (int i = 0; i <= A_2 - 1; i++)
					{
						num = (num << 8 | (ulong)this._fileBytes[A_1 + i]);
					}
					result = num;
				}
			}
			catch
			{
				result = 0UL;
			}
			return result;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000126D8 File Offset: 0x000108D8
		private int Gvl(int A_1)
		{
			int result;
			try
			{
				if (A_1 > this._fileBytes.Length)
				{
					result = 0;
				}
				else
				{
					for (int i = A_1; i <= A_1 + 8; i++)
					{
						if (i > this._fileBytes.Length - 1)
						{
							return 0;
						}
						if ((this._fileBytes[i] & 128) != 128)
						{
							return i;
						}
					}
					result = A_1 + 8;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00012764 File Offset: 0x00010964
		private long Cvl(int A_1, int A_2)
		{
			long result;
			try
			{
				A_2++;
				byte[] array = new byte[8];
				int num = A_2 - A_1;
				bool flag = false;
				if (num == 0 | num > 9)
				{
					result = 0L;
				}
				else if (num == 1)
				{
					array[0] = (this._fileBytes[A_1] & 127);
					result = BitConverter.ToInt64(array, 0);
				}
				else
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
						array[0] = this._fileBytes[A_2 - 1];
						A_2--;
						num4 = 1;
					}
					for (int i = A_2 - 1; i >= A_1; i += -1)
					{
						if (i - 1 >= A_1)
						{
							array[num4] = (byte)((this._fileBytes[i] >> num2 - 1 & 255 >> num2) | (int)this._fileBytes[i - 1] << num3);
							num2++;
							num4++;
							num3--;
						}
						else if (!flag)
						{
							array[num4] = (byte)(this._fileBytes[i] >> num2 - 1 & 255 >> num2);
						}
					}
					result = BitConverter.ToInt64(array, 0);
				}
			}
			catch
			{
				result = 0L;
			}
			return result;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000128B4 File Offset: 0x00010AB4
		private static bool IsOdd(long A_0)
		{
			return (A_0 & 1L) == 1L;
		}

		// Token: 0x040000E6 RID: 230
		private readonly ulong _dbEncoding;

		// Token: 0x040000E7 RID: 231
		private readonly byte[] _fileBytes;

		// Token: 0x040000E8 RID: 232
		private readonly ulong _pageSize;

		// Token: 0x040000E9 RID: 233
		private readonly byte[] _sqlDataTypeSize = new byte[]
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

		// Token: 0x040000EA RID: 234
		private string[] _fieldNames;

		// Token: 0x040000EB RID: 235
		private SqlHandler.SqliteMasterEntry[] _masterTableEntries;

		// Token: 0x040000EC RID: 236
		private SqlHandler.TableEntry[] _tableEntries;

		// Token: 0x02000246 RID: 582
		private struct RecordHeaderField
		{
			// Token: 0x04000A3F RID: 2623
			public long Size;

			// Token: 0x04000A40 RID: 2624
			public long Type;
		}

		// Token: 0x02000247 RID: 583
		private struct TableEntry
		{
			// Token: 0x04000A41 RID: 2625
			public string[] Content;
		}

		// Token: 0x02000248 RID: 584
		private struct SqliteMasterEntry
		{
			// Token: 0x04000A42 RID: 2626
			public string ItemName;

			// Token: 0x04000A43 RID: 2627
			public long RootNum;

			// Token: 0x04000A44 RID: 2628
			public string SqlStatement;
		}
	}
}
