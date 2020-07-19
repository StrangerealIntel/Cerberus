using System;
using Ionic.Crc;

namespace Ionic.BZip2
{
	// Token: 0x020000B8 RID: 184
	internal class BZip2Compressor
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x00027034 File Offset: 0x00025234
		public BZip2Compressor(BitWriter writer) : this(writer, BZip2.MaxBlockSize)
		{
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00027044 File Offset: 0x00025244
		public BZip2Compressor(BitWriter writer, int blockSize)
		{
			this.blockSize100k = blockSize;
			this.bw = writer;
			this.outBlockFillThreshold = blockSize * BZip2.BlockSizeMultiple - 20;
			this.cstate = new BZip2Compressor.CompressionState(blockSize);
			this.Reset();
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000270A0 File Offset: 0x000252A0
		private void Reset()
		{
			this.crc.Reset();
			this.currentByte = -1;
			this.runLength = 0;
			this.last = -1;
			int num = 256;
			while (--num >= 0)
			{
				this.cstate.inUse[num] = false;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x000270F4 File Offset: 0x000252F4
		public int BlockSize
		{
			get
			{
				return this.blockSize100k;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x000270FC File Offset: 0x000252FC
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x00027104 File Offset: 0x00025304
		public uint Crc32 { get; private set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00027110 File Offset: 0x00025310
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x00027118 File Offset: 0x00025318
		public int AvailableBytesOut { get; private set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00027124 File Offset: 0x00025324
		public int UncompressedBytes
		{
			get
			{
				return this.last + 1;
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00027130 File Offset: 0x00025330
		public int Fill(byte[] buffer, int offset, int count)
		{
			if (this.last >= this.outBlockFillThreshold)
			{
				return 0;
			}
			int num = 0;
			int num2 = offset + count;
			int num3;
			do
			{
				num3 = this.write0(buffer[offset++]);
				if (num3 > 0)
				{
					num++;
				}
			}
			while (offset < num2 && num3 == 1);
			return num;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00027180 File Offset: 0x00025380
		private int write0(byte b)
		{
			if (this.currentByte == -1)
			{
				this.currentByte = (int)b;
				this.runLength++;
				return 1;
			}
			if (this.currentByte == (int)b)
			{
				if (++this.runLength <= 254)
				{
					return 1;
				}
				bool flag = this.AddRunToOutputBlock(false);
				this.currentByte = -1;
				this.runLength = 0;
				if (!flag)
				{
					return 1;
				}
				return 2;
			}
			else
			{
				bool flag = this.AddRunToOutputBlock(false);
				if (flag)
				{
					this.currentByte = -1;
					this.runLength = 0;
					return 0;
				}
				this.runLength = 1;
				this.currentByte = (int)b;
				return 1;
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0002722C File Offset: 0x0002542C
		private bool AddRunToOutputBlock(bool final)
		{
			this.runs++;
			int num = this.last;
			if (num >= this.outBlockFillThreshold && !final)
			{
				string message = string.Format("block overrun(final={2}): {0} >= threshold ({1})", num, this.outBlockFillThreshold, final);
				throw new Exception(message);
			}
			byte b = (byte)this.currentByte;
			byte[] block = this.cstate.block;
			this.cstate.inUse[(int)b] = true;
			int num2 = this.runLength;
			this.crc.UpdateCRC(b, num2);
			switch (num2)
			{
			case 1:
				block[num + 2] = b;
				this.last = num + 1;
				break;
			case 2:
				block[num + 2] = b;
				block[num + 3] = b;
				this.last = num + 2;
				break;
			case 3:
				block[num + 2] = b;
				block[num + 3] = b;
				block[num + 4] = b;
				this.last = num + 3;
				break;
			default:
				num2 -= 4;
				this.cstate.inUse[num2] = true;
				block[num + 2] = b;
				block[num + 3] = b;
				block[num + 4] = b;
				block[num + 5] = b;
				block[num + 6] = (byte)num2;
				this.last = num + 5;
				break;
			}
			return this.last >= this.outBlockFillThreshold;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00027380 File Offset: 0x00025580
		public void CompressAndWrite()
		{
			if (this.runLength > 0)
			{
				this.AddRunToOutputBlock(true);
			}
			this.currentByte = -1;
			if (this.last == -1)
			{
				return;
			}
			this.blockSort();
			this.bw.WriteByte(49);
			this.bw.WriteByte(65);
			this.bw.WriteByte(89);
			this.bw.WriteByte(38);
			this.bw.WriteByte(83);
			this.bw.WriteByte(89);
			this.Crc32 = (uint)this.crc.Crc32Result;
			this.bw.WriteInt(this.Crc32);
			this.bw.WriteBits(1, this.blockRandomised ? 1u : 0u);
			this.moveToFrontCodeAndSend();
			this.Reset();
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0002745C File Offset: 0x0002565C
		private void randomiseBlock()
		{
			bool[] inUse = this.cstate.inUse;
			byte[] block = this.cstate.block;
			int num = this.last;
			int num2 = 256;
			while (--num2 >= 0)
			{
				inUse[num2] = false;
			}
			int num3 = 0;
			int num4 = 0;
			int i = 0;
			int num5 = 1;
			while (i <= num)
			{
				if (num3 == 0)
				{
					num3 = (int)((ushort)Rand.Rnums(num4));
					if (++num4 == 512)
					{
						num4 = 0;
					}
				}
				num3--;
				byte[] array = block;
				int num6 = num5;
				array[num6] ^= ((num3 == 1) ? 1 : 0);
				inUse[(int)(block[num5] & byte.MaxValue)] = true;
				i = num5;
				num5++;
			}
			this.blockRandomised = true;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00027520 File Offset: 0x00025720
		private void mainSort()
		{
			BZip2Compressor.CompressionState compressionState = this.cstate;
			int[] mainSort_runningOrder = compressionState.mainSort_runningOrder;
			int[] mainSort_copy = compressionState.mainSort_copy;
			bool[] mainSort_bigDone = compressionState.mainSort_bigDone;
			int[] ftab = compressionState.ftab;
			byte[] block = compressionState.block;
			int[] fmap = compressionState.fmap;
			char[] quadrant = compressionState.quadrant;
			int num = this.last;
			int num2 = this.workLimit;
			bool flag = this.firstAttempt;
			int num3 = 65537;
			while (--num3 >= 0)
			{
				ftab[num3] = 0;
			}
			for (int i = 0; i < BZip2.NUM_OVERSHOOT_BYTES; i++)
			{
				block[num + i + 2] = block[i % (num + 1) + 1];
			}
			int num4 = num + BZip2.NUM_OVERSHOOT_BYTES + 1;
			while (--num4 >= 0)
			{
				quadrant[num4] = '\0';
			}
			block[0] = block[num + 1];
			int num5 = (int)(block[0] & byte.MaxValue);
			for (int j = 0; j <= num; j++)
			{
				int num6 = (int)(block[j + 1] & byte.MaxValue);
				ftab[(num5 << 8) + num6]++;
				num5 = num6;
			}
			for (int k = 1; k <= 65536; k++)
			{
				ftab[k] += ftab[k - 1];
			}
			num5 = (int)(block[1] & byte.MaxValue);
			for (int l = 0; l < num; l++)
			{
				int num7 = (int)(block[l + 2] & byte.MaxValue);
				fmap[--ftab[(num5 << 8) + num7]] = l;
				num5 = num7;
			}
			fmap[--ftab[((int)(block[num + 1] & byte.MaxValue) << 8) + (int)(block[1] & byte.MaxValue)]] = num;
			int num8 = 256;
			while (--num8 >= 0)
			{
				mainSort_bigDone[num8] = false;
				mainSort_runningOrder[num8] = num8;
			}
			int num9 = 364;
			while (num9 != 1)
			{
				num9 /= 3;
				for (int m = num9; m <= 255; m++)
				{
					int num10 = mainSort_runningOrder[m];
					int num11 = ftab[num10 + 1 << 8] - ftab[num10 << 8];
					int num12 = num9 - 1;
					int num13 = m;
					int num14 = mainSort_runningOrder[num13 - num9];
					while (ftab[num14 + 1 << 8] - ftab[num14 << 8] > num11)
					{
						mainSort_runningOrder[num13] = num14;
						num13 -= num9;
						if (num13 <= num12)
						{
							break;
						}
						num14 = mainSort_runningOrder[num13 - num9];
					}
					mainSort_runningOrder[num13] = num10;
				}
			}
			for (int n = 0; n <= 255; n++)
			{
				int num15 = mainSort_runningOrder[n];
				for (int num16 = 0; num16 <= 255; num16++)
				{
					int num17 = (num15 << 8) + num16;
					int num18 = ftab[num17];
					if ((num18 & BZip2Compressor.SETMASK) != BZip2Compressor.SETMASK)
					{
						int num19 = num18 & BZip2Compressor.CLEARMASK;
						int num20 = (ftab[num17 + 1] & BZip2Compressor.CLEARMASK) - 1;
						if (num20 > num19)
						{
							this.mainQSort3(compressionState, num19, num20, 2);
							if (flag && this.workDone > num2)
							{
								return;
							}
						}
						ftab[num17] = (num18 | BZip2Compressor.SETMASK);
					}
				}
				for (int num21 = 0; num21 <= 255; num21++)
				{
					mainSort_copy[num21] = (ftab[(num21 << 8) + num15] & BZip2Compressor.CLEARMASK);
				}
				int num22 = ftab[num15 << 8] & BZip2Compressor.CLEARMASK;
				int num23 = ftab[num15 + 1 << 8] & BZip2Compressor.CLEARMASK;
				while (num22 < num23)
				{
					int num24 = fmap[num22];
					num5 = (int)(block[num24] & byte.MaxValue);
					if (!mainSort_bigDone[num5])
					{
						fmap[mainSort_copy[num5]] = ((num24 == 0) ? num : (num24 - 1));
						mainSort_copy[num5]++;
					}
					num22++;
				}
				int num25 = 256;
				while (--num25 >= 0)
				{
					ftab[(num25 << 8) + num15] |= BZip2Compressor.SETMASK;
				}
				mainSort_bigDone[num15] = true;
				if (n < 255)
				{
					int num26 = ftab[num15 << 8] & BZip2Compressor.CLEARMASK;
					int num27 = (ftab[num15 + 1 << 8] & BZip2Compressor.CLEARMASK) - num26;
					int num28 = 0;
					while (num27 >> num28 > 65534)
					{
						num28++;
					}
					for (int num29 = 0; num29 < num27; num29++)
					{
						int num30 = fmap[num26 + num29];
						char c = (char)(num29 >> num28);
						quadrant[num30] = c;
						if (num30 < BZip2.NUM_OVERSHOOT_BYTES)
						{
							quadrant[num30 + num + 1] = c;
						}
					}
				}
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000279BC File Offset: 0x00025BBC
		private void blockSort()
		{
			this.workLimit = BZip2Compressor.WORK_FACTOR * this.last;
			this.workDone = 0;
			this.blockRandomised = false;
			this.firstAttempt = true;
			this.mainSort();
			if (this.firstAttempt && this.workDone > this.workLimit)
			{
				this.randomiseBlock();
				this.workLimit = (this.workDone = 0);
				this.firstAttempt = false;
				this.mainSort();
			}
			int[] fmap = this.cstate.fmap;
			this.origPtr = -1;
			int i = 0;
			int num = this.last;
			while (i <= num)
			{
				if (fmap[i] == 0)
				{
					this.origPtr = i;
					return;
				}
				i++;
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00027A74 File Offset: 0x00025C74
		private bool mainSimpleSort(BZip2Compressor.CompressionState dataShadow, int lo, int hi, int d)
		{
			int num = hi - lo + 1;
			if (num < 2)
			{
				return this.firstAttempt && this.workDone > this.workLimit;
			}
			int num2 = 0;
			while (BZip2Compressor.increments[num2] < num)
			{
				num2++;
			}
			int[] fmap = dataShadow.fmap;
			char[] quadrant = dataShadow.quadrant;
			byte[] block = dataShadow.block;
			int num3 = this.last;
			int num4 = num3 + 1;
			bool flag = this.firstAttempt;
			int num5 = this.workLimit;
			int num6 = this.workDone;
			while (--num2 >= 0)
			{
				int num7 = BZip2Compressor.increments[num2];
				int num8 = lo + num7 - 1;
				int i = lo + num7;
				while (i <= hi)
				{
					int num9 = 3;
					while (i <= hi && --num9 >= 0)
					{
						int num10 = fmap[i];
						int num11 = num10 + d;
						int num12 = i;
						bool flag2 = false;
						int num13 = 0;
						for (;;)
						{
							IL_BA:
							if (flag2)
							{
								fmap[num12] = num13;
								if ((num12 -= num7) <= num8)
								{
									break;
								}
							}
							else
							{
								flag2 = true;
							}
							num13 = fmap[num12 - num7];
							int num14 = num13 + d;
							int num15 = num11;
							if (block[num14 + 1] == block[num15 + 1])
							{
								if (block[num14 + 2] == block[num15 + 2])
								{
									if (block[num14 + 3] == block[num15 + 3])
									{
										if (block[num14 + 4] == block[num15 + 4])
										{
											if (block[num14 + 5] == block[num15 + 5])
											{
												if (block[num14 += 6] == block[num15 += 6])
												{
													int j = num3;
													while (j > 0)
													{
														j -= 4;
														if (block[num14 + 1] == block[num15 + 1])
														{
															if (quadrant[num14] == quadrant[num15])
															{
																if (block[num14 + 2] == block[num15 + 2])
																{
																	if (quadrant[num14 + 1] == quadrant[num15 + 1])
																	{
																		if (block[num14 + 3] == block[num15 + 3])
																		{
																			if (quadrant[num14 + 2] == quadrant[num15 + 2])
																			{
																				if (block[num14 + 4] == block[num15 + 4])
																				{
																					if (quadrant[num14 + 3] == quadrant[num15 + 3])
																					{
																						if ((num14 += 4) >= num4)
																						{
																							num14 -= num4;
																						}
																						if ((num15 += 4) >= num4)
																						{
																							num15 -= num4;
																						}
																						num6++;
																					}
																					else
																					{
																						if (quadrant[num14 + 3] > quadrant[num15 + 3])
																						{
																							goto IL_BA;
																						}
																						break;
																					}
																				}
																				else
																				{
																					if ((block[num14 + 4] & 255) > (block[num15 + 4] & 255))
																					{
																						goto IL_BA;
																					}
																					break;
																				}
																			}
																			else
																			{
																				if (quadrant[num14 + 2] > quadrant[num15 + 2])
																				{
																					goto IL_BA;
																				}
																				break;
																			}
																		}
																		else
																		{
																			if ((block[num14 + 3] & 255) > (block[num15 + 3] & 255))
																			{
																				goto IL_BA;
																			}
																			break;
																		}
																	}
																	else
																	{
																		if (quadrant[num14 + 1] > quadrant[num15 + 1])
																		{
																			goto IL_BA;
																		}
																		break;
																	}
																}
																else
																{
																	if ((block[num14 + 2] & 255) > (block[num15 + 2] & 255))
																	{
																		goto IL_BA;
																	}
																	break;
																}
															}
															else
															{
																if (quadrant[num14] > quadrant[num15])
																{
																	goto IL_BA;
																}
																break;
															}
														}
														else
														{
															if ((block[num14 + 1] & 255) > (block[num15 + 1] & 255))
															{
																goto IL_BA;
															}
															break;
														}
													}
													break;
												}
												if ((block[num14] & 255) <= (block[num15] & 255))
												{
													break;
												}
											}
											else if ((block[num14 + 5] & 255) <= (block[num15 + 5] & 255))
											{
												break;
											}
										}
										else if ((block[num14 + 4] & 255) <= (block[num15 + 4] & 255))
										{
											break;
										}
									}
									else if ((block[num14 + 3] & 255) <= (block[num15 + 3] & 255))
									{
										break;
									}
								}
								else if ((block[num14 + 2] & 255) <= (block[num15 + 2] & 255))
								{
									break;
								}
							}
							else if ((block[num14 + 1] & 255) <= (block[num15 + 1] & 255))
							{
								break;
							}
						}
						IL_3FA:
						fmap[num12] = num10;
						i++;
						continue;
						goto IL_3FA;
					}
					if (flag && i <= hi && num6 > num5)
					{
						goto IL_446;
					}
				}
			}
			IL_446:
			this.workDone = num6;
			return flag && num6 > num5;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00027EE4 File Offset: 0x000260E4
		private static void vswap(int[] fmap, int p1, int p2, int n)
		{
			n += p1;
			while (p1 < n)
			{
				int num = fmap[p1];
				fmap[p1++] = fmap[p2];
				fmap[p2++] = num;
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00027F1C File Offset: 0x0002611C
		private static byte med3(byte a, byte b, byte c)
		{
			if (a >= b)
			{
				if (b > c)
				{
					return b;
				}
				if (a <= c)
				{
					return a;
				}
				return c;
			}
			else
			{
				if (b < c)
				{
					return b;
				}
				if (a >= c)
				{
					return a;
				}
				return c;
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00027F4C File Offset: 0x0002614C
		private void mainQSort3(BZip2Compressor.CompressionState dataShadow, int loSt, int hiSt, int dSt)
		{
			int[] stack_ll = dataShadow.stack_ll;
			int[] stack_hh = dataShadow.stack_hh;
			int[] stack_dd = dataShadow.stack_dd;
			int[] fmap = dataShadow.fmap;
			byte[] block = dataShadow.block;
			stack_ll[0] = loSt;
			stack_hh[0] = hiSt;
			stack_dd[0] = dSt;
			int num = 1;
			while (--num >= 0)
			{
				int num2 = stack_ll[num];
				int num3 = stack_hh[num];
				int num4 = stack_dd[num];
				if (num3 - num2 < BZip2Compressor.SMALL_THRESH || num4 > BZip2Compressor.DEPTH_THRESH)
				{
					if (this.mainSimpleSort(dataShadow, num2, num3, num4))
					{
						return;
					}
				}
				else
				{
					int num5 = num4 + 1;
					int num6 = (int)(BZip2Compressor.med3(block[fmap[num2] + num5], block[fmap[num3] + num5], block[fmap[num2 + num3 >> 1] + num5]) & byte.MaxValue);
					int i = num2;
					int num7 = num3;
					int num8 = num2;
					int num9 = num3;
					for (;;)
					{
						if (i <= num7)
						{
							int num10 = (int)(block[fmap[i] + num5] & byte.MaxValue) - num6;
							if (num10 == 0)
							{
								int num11 = fmap[i];
								fmap[i++] = fmap[num8];
								fmap[num8++] = num11;
								continue;
							}
							if (num10 < 0)
							{
								i++;
								continue;
							}
						}
						while (i <= num7)
						{
							int num12 = (int)(block[fmap[num7] + num5] & byte.MaxValue) - num6;
							if (num12 == 0)
							{
								int num13 = fmap[num7];
								fmap[num7--] = fmap[num9];
								fmap[num9--] = num13;
							}
							else
							{
								if (num12 <= 0)
								{
									break;
								}
								num7--;
							}
						}
						if (i > num7)
						{
							break;
						}
						int num14 = fmap[i];
						fmap[i++] = fmap[num7];
						fmap[num7--] = num14;
					}
					if (num9 < num8)
					{
						stack_ll[num] = num2;
						stack_hh[num] = num3;
						stack_dd[num] = num5;
						num++;
					}
					else
					{
						int num15 = (num8 - num2 < i - num8) ? (num8 - num2) : (i - num8);
						BZip2Compressor.vswap(fmap, num2, i - num15, num15);
						int num16 = (num3 - num9 < num9 - num7) ? (num3 - num9) : (num9 - num7);
						BZip2Compressor.vswap(fmap, i, num3 - num16 + 1, num16);
						num15 = num2 + i - num8 - 1;
						num16 = num3 - (num9 - num7) + 1;
						stack_ll[num] = num2;
						stack_hh[num] = num15;
						stack_dd[num] = num4;
						num++;
						stack_ll[num] = num15 + 1;
						stack_hh[num] = num16 - 1;
						stack_dd[num] = num5;
						num++;
						stack_ll[num] = num16;
						stack_hh[num] = num3;
						stack_dd[num] = num4;
						num++;
					}
				}
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000281F0 File Offset: 0x000263F0
		private void generateMTFValues()
		{
			int num = this.last;
			BZip2Compressor.CompressionState compressionState = this.cstate;
			bool[] inUse = compressionState.inUse;
			byte[] block = compressionState.block;
			int[] fmap = compressionState.fmap;
			char[] sfmap = compressionState.sfmap;
			int[] mtfFreq = compressionState.mtfFreq;
			byte[] unseqToSeq = compressionState.unseqToSeq;
			byte[] generateMTFValues_yy = compressionState.generateMTFValues_yy;
			int num2 = 0;
			for (int i = 0; i < 256; i++)
			{
				if (inUse[i])
				{
					unseqToSeq[i] = (byte)num2;
					num2++;
				}
			}
			this.nInUse = num2;
			int num3 = num2 + 1;
			for (int j = num3; j >= 0; j--)
			{
				mtfFreq[j] = 0;
			}
			int num4 = num2;
			while (--num4 >= 0)
			{
				generateMTFValues_yy[num4] = (byte)num4;
			}
			int num5 = 0;
			int num6 = 0;
			for (int k = 0; k <= num; k++)
			{
				byte b = unseqToSeq[(int)(block[fmap[k]] & byte.MaxValue)];
				byte b2 = generateMTFValues_yy[0];
				int num7 = 0;
				while (b != b2)
				{
					num7++;
					byte b3 = b2;
					b2 = generateMTFValues_yy[num7];
					generateMTFValues_yy[num7] = b3;
				}
				generateMTFValues_yy[0] = b2;
				if (num7 == 0)
				{
					num6++;
				}
				else
				{
					if (num6 > 0)
					{
						num6--;
						for (;;)
						{
							if ((num6 & 1) == 0)
							{
								sfmap[num5] = BZip2.RUNA;
								num5++;
								mtfFreq[(int)BZip2.RUNA]++;
							}
							else
							{
								sfmap[num5] = BZip2.RUNB;
								num5++;
								mtfFreq[(int)BZip2.RUNB]++;
							}
							if (num6 < 2)
							{
								break;
							}
							num6 = num6 - 2 >> 1;
						}
						num6 = 0;
					}
					sfmap[num5] = (char)(num7 + 1);
					num5++;
					mtfFreq[num7 + 1]++;
				}
			}
			if (num6 > 0)
			{
				num6--;
				for (;;)
				{
					if ((num6 & 1) == 0)
					{
						sfmap[num5] = BZip2.RUNA;
						num5++;
						mtfFreq[(int)BZip2.RUNA]++;
					}
					else
					{
						sfmap[num5] = BZip2.RUNB;
						num5++;
						mtfFreq[(int)BZip2.RUNB]++;
					}
					if (num6 < 2)
					{
						break;
					}
					num6 = num6 - 2 >> 1;
				}
			}
			sfmap[num5] = (char)num3;
			mtfFreq[num3]++;
			this.nMTF = num5 + 1;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00028450 File Offset: 0x00026650
		private static void hbAssignCodes(int[] code, byte[] length, int minLen, int maxLen, int alphaSize)
		{
			int num = 0;
			for (int i = minLen; i <= maxLen; i++)
			{
				for (int j = 0; j < alphaSize; j++)
				{
					if ((int)(length[j] & 255) == i)
					{
						code[j] = num;
						num++;
					}
				}
				num <<= 1;
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000284A0 File Offset: 0x000266A0
		private void sendMTFValues()
		{
			byte[][] sendMTFValues_len = this.cstate.sendMTFValues_len;
			int num = this.nInUse + 2;
			int num2 = BZip2.NGroups;
			while (--num2 >= 0)
			{
				byte[] array = sendMTFValues_len[num2];
				int num3 = num;
				while (--num3 >= 0)
				{
					array[num3] = BZip2Compressor.GREATER_ICOST;
				}
			}
			int nGroups = (this.nMTF < 200) ? 2 : ((this.nMTF < 600) ? 3 : ((this.nMTF < 1200) ? 4 : ((this.nMTF < 2400) ? 5 : 6)));
			this.sendMTFValues0(nGroups, num);
			int nSelectors = this.sendMTFValues1(nGroups, num);
			this.sendMTFValues2(nGroups, nSelectors);
			this.sendMTFValues3(nGroups, num);
			this.sendMTFValues4();
			this.sendMTFValues5(nGroups, nSelectors);
			this.sendMTFValues6(nGroups, num);
			this.sendMTFValues7(nSelectors);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000285A0 File Offset: 0x000267A0
		private void sendMTFValues0(int nGroups, int alphaSize)
		{
			byte[][] sendMTFValues_len = this.cstate.sendMTFValues_len;
			int[] mtfFreq = this.cstate.mtfFreq;
			int num = this.nMTF;
			int num2 = 0;
			for (int i = nGroups; i > 0; i--)
			{
				int num3 = num / i;
				int num4 = num2 - 1;
				int num5 = 0;
				int num6 = alphaSize - 1;
				while (num5 < num3 && num4 < num6)
				{
					num5 += mtfFreq[++num4];
				}
				if (num4 > num2 && i != nGroups && i != 1 && (nGroups - i & 1) != 0)
				{
					num5 -= mtfFreq[num4--];
				}
				byte[] array = sendMTFValues_len[i - 1];
				int num7 = alphaSize;
				while (--num7 >= 0)
				{
					if (num7 >= num2 && num7 <= num4)
					{
						array[num7] = BZip2Compressor.LESSER_ICOST;
					}
					else
					{
						array[num7] = BZip2Compressor.GREATER_ICOST;
					}
				}
				num2 = num4 + 1;
				num -= num5;
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000286A0 File Offset: 0x000268A0
		private static void hbMakeCodeLengths(byte[] len, int[] freq, BZip2Compressor.CompressionState state1, int alphaSize, int maxLen)
		{
			int[] heap = state1.heap;
			int[] weight = state1.weight;
			int[] parent = state1.parent;
			int num = alphaSize;
			while (--num >= 0)
			{
				weight[num + 1] = ((freq[num] == 0) ? 1 : freq[num]) << 8;
			}
			bool flag = true;
			while (flag)
			{
				flag = false;
				int num2 = alphaSize;
				int i = 0;
				heap[0] = 0;
				weight[0] = 0;
				parent[0] = -2;
				for (int j = 1; j <= alphaSize; j++)
				{
					parent[j] = -1;
					i++;
					heap[i] = j;
					int num3 = i;
					int num4 = heap[num3];
					while (weight[num4] < weight[heap[num3 >> 1]])
					{
						heap[num3] = heap[num3 >> 1];
						num3 >>= 1;
					}
					heap[num3] = num4;
				}
				while (i > 1)
				{
					int num5 = heap[1];
					heap[1] = heap[i];
					i--;
					int num6 = 1;
					int num7 = heap[1];
					for (;;)
					{
						int num8 = num6 << 1;
						if (num8 > i)
						{
							break;
						}
						if (num8 < i && weight[heap[num8 + 1]] < weight[heap[num8]])
						{
							num8++;
						}
						if (weight[num7] < weight[heap[num8]])
						{
							break;
						}
						heap[num6] = heap[num8];
						num6 = num8;
					}
					heap[num6] = num7;
					int num9 = heap[1];
					heap[1] = heap[i];
					i--;
					num6 = 1;
					num7 = heap[1];
					for (;;)
					{
						int num8 = num6 << 1;
						if (num8 > i)
						{
							break;
						}
						if (num8 < i && weight[heap[num8 + 1]] < weight[heap[num8]])
						{
							num8++;
						}
						if (weight[num7] < weight[heap[num8]])
						{
							break;
						}
						heap[num6] = heap[num8];
						num6 = num8;
					}
					heap[num6] = num7;
					num2++;
					parent[num5] = (parent[num9] = num2);
					int num10 = weight[num5];
					int num11 = weight[num9];
					weight[num2] = ((num10 & -256) + (num11 & -256) | 1 + (((num10 & 255) > (num11 & 255)) ? (num10 & 255) : (num11 & 255)));
					parent[num2] = -1;
					i++;
					heap[i] = num2;
					num6 = i;
					num7 = heap[num6];
					int k = weight[num7];
					while (k < weight[heap[num6 >> 1]])
					{
						heap[num6] = heap[num6 >> 1];
						num6 >>= 1;
					}
					heap[num6] = num7;
				}
				for (int l = 1; l <= alphaSize; l++)
				{
					int num12 = 0;
					int num13 = l;
					int num14;
					while ((num14 = parent[num13]) >= 0)
					{
						num13 = num14;
						num12++;
					}
					len[l - 1] = (byte)num12;
					if (num12 > maxLen)
					{
						flag = true;
					}
				}
				if (flag)
				{
					for (int m = 1; m < alphaSize; m++)
					{
						int num15 = weight[m] >> 8;
						num15 = 1 + (num15 >> 1);
						weight[m] = num15 << 8;
					}
				}
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00028980 File Offset: 0x00026B80
		private int sendMTFValues1(int nGroups, int alphaSize)
		{
			BZip2Compressor.CompressionState compressionState = this.cstate;
			int[][] sendMTFValues_rfreq = compressionState.sendMTFValues_rfreq;
			int[] sendMTFValues_fave = compressionState.sendMTFValues_fave;
			short[] sendMTFValues_cost = compressionState.sendMTFValues_cost;
			char[] sfmap = compressionState.sfmap;
			byte[] selector = compressionState.selector;
			byte[][] sendMTFValues_len = compressionState.sendMTFValues_len;
			byte[] array = sendMTFValues_len[0];
			byte[] array2 = sendMTFValues_len[1];
			byte[] array3 = sendMTFValues_len[2];
			byte[] array4 = sendMTFValues_len[3];
			byte[] array5 = sendMTFValues_len[4];
			byte[] array6 = sendMTFValues_len[5];
			int num = this.nMTF;
			int num2 = 0;
			for (int i = 0; i < BZip2.N_ITERS; i++)
			{
				int num3 = nGroups;
				while (--num3 >= 0)
				{
					sendMTFValues_fave[num3] = 0;
					int[] array7 = sendMTFValues_rfreq[num3];
					int num4 = alphaSize;
					while (--num4 >= 0)
					{
						array7[num4] = 0;
					}
				}
				num2 = 0;
				int num5;
				for (int j = 0; j < this.nMTF; j = num5 + 1)
				{
					num5 = Math.Min(j + BZip2.G_SIZE - 1, num - 1);
					if (nGroups == BZip2.NGroups)
					{
						int[] array8 = new int[6];
						for (int k = j; k <= num5; k++)
						{
							int num6 = (int)sfmap[k];
							array8[0] += (int)(array[num6] & byte.MaxValue);
							array8[1] += (int)(array2[num6] & byte.MaxValue);
							array8[2] += (int)(array3[num6] & byte.MaxValue);
							array8[3] += (int)(array4[num6] & byte.MaxValue);
							array8[4] += (int)(array5[num6] & byte.MaxValue);
							array8[5] += (int)(array6[num6] & byte.MaxValue);
						}
						sendMTFValues_cost[0] = (short)array8[0];
						sendMTFValues_cost[1] = (short)array8[1];
						sendMTFValues_cost[2] = (short)array8[2];
						sendMTFValues_cost[3] = (short)array8[3];
						sendMTFValues_cost[4] = (short)array8[4];
						sendMTFValues_cost[5] = (short)array8[5];
					}
					else
					{
						int num7 = nGroups;
						while (--num7 >= 0)
						{
							sendMTFValues_cost[num7] = 0;
						}
						for (int l = j; l <= num5; l++)
						{
							int num8 = (int)sfmap[l];
							int num9 = nGroups;
							while (--num9 >= 0)
							{
								short[] array9 = sendMTFValues_cost;
								int num10 = num9;
								array9[num10] += (short)(sendMTFValues_len[num9][num8] & byte.MaxValue);
							}
						}
					}
					int num11 = -1;
					int num12 = nGroups;
					int num13 = 999999999;
					while (--num12 >= 0)
					{
						int num14 = (int)sendMTFValues_cost[num12];
						if (num14 < num13)
						{
							num13 = num14;
							num11 = num12;
						}
					}
					sendMTFValues_fave[num11]++;
					selector[num2] = (byte)num11;
					num2++;
					int[] array10 = sendMTFValues_rfreq[num11];
					for (int m = j; m <= num5; m++)
					{
						array10[(int)sfmap[m]]++;
					}
				}
				for (int n = 0; n < nGroups; n++)
				{
					BZip2Compressor.hbMakeCodeLengths(sendMTFValues_len[n], sendMTFValues_rfreq[n], this.cstate, alphaSize, 20);
				}
			}
			return num2;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00028C9C File Offset: 0x00026E9C
		private void sendMTFValues2(int nGroups, int nSelectors)
		{
			BZip2Compressor.CompressionState compressionState = this.cstate;
			byte[] sendMTFValues2_pos = compressionState.sendMTFValues2_pos;
			int num = nGroups;
			while (--num >= 0)
			{
				sendMTFValues2_pos[num] = (byte)num;
			}
			for (int i = 0; i < nSelectors; i++)
			{
				byte b = compressionState.selector[i];
				byte b2 = sendMTFValues2_pos[0];
				int num2 = 0;
				while (b != b2)
				{
					num2++;
					byte b3 = b2;
					b2 = sendMTFValues2_pos[num2];
					sendMTFValues2_pos[num2] = b3;
				}
				sendMTFValues2_pos[0] = b2;
				compressionState.selectorMtf[i] = (byte)num2;
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00028D24 File Offset: 0x00026F24
		private void sendMTFValues3(int nGroups, int alphaSize)
		{
			int[][] sendMTFValues_code = this.cstate.sendMTFValues_code;
			byte[][] sendMTFValues_len = this.cstate.sendMTFValues_len;
			for (int i = 0; i < nGroups; i++)
			{
				int num = 32;
				int num2 = 0;
				byte[] array = sendMTFValues_len[i];
				int num3 = alphaSize;
				while (--num3 >= 0)
				{
					int num4 = (int)(array[num3] & byte.MaxValue);
					if (num4 > num2)
					{
						num2 = num4;
					}
					if (num4 < num)
					{
						num = num4;
					}
				}
				BZip2Compressor.hbAssignCodes(sendMTFValues_code[i], sendMTFValues_len[i], num, num2, alphaSize);
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00028DBC File Offset: 0x00026FBC
		private void sendMTFValues4()
		{
			bool[] inUse = this.cstate.inUse;
			bool[] sentMTFValues4_inUse = this.cstate.sentMTFValues4_inUse16;
			int num = 16;
			while (--num >= 0)
			{
				sentMTFValues4_inUse[num] = false;
				int num2 = num * 16;
				int num3 = 16;
				while (--num3 >= 0)
				{
					if (inUse[num2 + num3])
					{
						sentMTFValues4_inUse[num] = true;
					}
				}
			}
			uint num4 = 0u;
			for (int i = 0; i < 16; i++)
			{
				if (sentMTFValues4_inUse[i])
				{
					num4 |= 1u << 16 - i - 1;
				}
			}
			this.bw.WriteBits(16, num4);
			for (int j = 0; j < 16; j++)
			{
				if (sentMTFValues4_inUse[j])
				{
					int num5 = j * 16;
					num4 = 0u;
					for (int k = 0; k < 16; k++)
					{
						if (inUse[num5 + k])
						{
							num4 |= 1u << 16 - k - 1;
						}
					}
					this.bw.WriteBits(16, num4);
				}
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00028EC8 File Offset: 0x000270C8
		private void sendMTFValues5(int nGroups, int nSelectors)
		{
			this.bw.WriteBits(3, (uint)nGroups);
			this.bw.WriteBits(15, (uint)nSelectors);
			byte[] selectorMtf = this.cstate.selectorMtf;
			for (int i = 0; i < nSelectors; i++)
			{
				int j = 0;
				int num = (int)(selectorMtf[i] & byte.MaxValue);
				while (j < num)
				{
					this.bw.WriteBits(1, 1u);
					j++;
				}
				this.bw.WriteBits(1, 0u);
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00028F44 File Offset: 0x00027144
		private void sendMTFValues6(int nGroups, int alphaSize)
		{
			byte[][] sendMTFValues_len = this.cstate.sendMTFValues_len;
			for (int i = 0; i < nGroups; i++)
			{
				byte[] array = sendMTFValues_len[i];
				uint num = (uint)(array[0] & byte.MaxValue);
				this.bw.WriteBits(5, num);
				for (int j = 0; j < alphaSize; j++)
				{
					int num2 = (int)(array[j] & byte.MaxValue);
					while ((ulong)num < (ulong)((long)num2))
					{
						this.bw.WriteBits(2, 2u);
						num += 1u;
					}
					while ((ulong)num > (ulong)((long)num2))
					{
						this.bw.WriteBits(2, 3u);
						num -= 1u;
					}
					this.bw.WriteBits(1, 0u);
				}
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00028FF8 File Offset: 0x000271F8
		private void sendMTFValues7(int nSelectors)
		{
			byte[][] sendMTFValues_len = this.cstate.sendMTFValues_len;
			int[][] sendMTFValues_code = this.cstate.sendMTFValues_code;
			byte[] selector = this.cstate.selector;
			char[] sfmap = this.cstate.sfmap;
			int num = this.nMTF;
			int num2 = 0;
			int i = 0;
			while (i < num)
			{
				int num3 = Math.Min(i + BZip2.G_SIZE - 1, num - 1);
				int num4 = (int)(selector[num2] & byte.MaxValue);
				int[] array = sendMTFValues_code[num4];
				byte[] array2 = sendMTFValues_len[num4];
				while (i <= num3)
				{
					int num5 = (int)sfmap[i];
					int nbits = (int)(array2[num5] & byte.MaxValue);
					this.bw.WriteBits(nbits, (uint)array[num5]);
					i++;
				}
				i = num3 + 1;
				num2++;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000290CC File Offset: 0x000272CC
		private void moveToFrontCodeAndSend()
		{
			this.bw.WriteBits(24, (uint)this.origPtr);
			this.generateMTFValues();
			this.sendMTFValues();
		}

		// Token: 0x040002D5 RID: 725
		private int blockSize100k;

		// Token: 0x040002D6 RID: 726
		private int currentByte = -1;

		// Token: 0x040002D7 RID: 727
		private int runLength;

		// Token: 0x040002D8 RID: 728
		private int last;

		// Token: 0x040002D9 RID: 729
		private int outBlockFillThreshold;

		// Token: 0x040002DA RID: 730
		private BZip2Compressor.CompressionState cstate;

		// Token: 0x040002DB RID: 731
		private readonly CRC32 crc = new CRC32(true);

		// Token: 0x040002DC RID: 732
		private BitWriter bw;

		// Token: 0x040002DD RID: 733
		private int runs;

		// Token: 0x040002DE RID: 734
		private int workDone;

		// Token: 0x040002DF RID: 735
		private int workLimit;

		// Token: 0x040002E0 RID: 736
		private bool firstAttempt;

		// Token: 0x040002E1 RID: 737
		private bool blockRandomised;

		// Token: 0x040002E2 RID: 738
		private int origPtr;

		// Token: 0x040002E3 RID: 739
		private int nInUse;

		// Token: 0x040002E4 RID: 740
		private int nMTF;

		// Token: 0x040002E5 RID: 741
		private static readonly int SETMASK = 2097152;

		// Token: 0x040002E6 RID: 742
		private static readonly int CLEARMASK = ~BZip2Compressor.SETMASK;

		// Token: 0x040002E7 RID: 743
		private static readonly byte GREATER_ICOST = 15;

		// Token: 0x040002E8 RID: 744
		private static readonly byte LESSER_ICOST = 0;

		// Token: 0x040002E9 RID: 745
		private static readonly int SMALL_THRESH = 20;

		// Token: 0x040002EA RID: 746
		private static readonly int DEPTH_THRESH = 10;

		// Token: 0x040002EB RID: 747
		private static readonly int WORK_FACTOR = 30;

		// Token: 0x040002EC RID: 748
		private static readonly int[] increments = new int[]
		{
			1,
			4,
			13,
			40,
			121,
			364,
			1093,
			3280,
			9841,
			29524,
			88573,
			265720,
			797161,
			2391484
		};

		// Token: 0x02000277 RID: 631
		private class CompressionState
		{
			// Token: 0x06001703 RID: 5891 RVA: 0x00077390 File Offset: 0x00075590
			public CompressionState(int blockSize100k)
			{
				int num = blockSize100k * BZip2.BlockSizeMultiple;
				this.block = new byte[num + 1 + BZip2.NUM_OVERSHOOT_BYTES];
				this.fmap = new int[num];
				this.sfmap = new char[2 * num];
				this.quadrant = this.sfmap;
				this.sendMTFValues_len = BZip2.InitRectangularArray<byte>(BZip2.NGroups, BZip2.MaxAlphaSize);
				this.sendMTFValues_rfreq = BZip2.InitRectangularArray<int>(BZip2.NGroups, BZip2.MaxAlphaSize);
				this.sendMTFValues_code = BZip2.InitRectangularArray<int>(BZip2.NGroups, BZip2.MaxAlphaSize);
			}

			// Token: 0x04000AE2 RID: 2786
			public readonly bool[] inUse = new bool[256];

			// Token: 0x04000AE3 RID: 2787
			public readonly byte[] unseqToSeq = new byte[256];

			// Token: 0x04000AE4 RID: 2788
			public readonly int[] mtfFreq = new int[BZip2.MaxAlphaSize];

			// Token: 0x04000AE5 RID: 2789
			public readonly byte[] selector = new byte[BZip2.MaxSelectors];

			// Token: 0x04000AE6 RID: 2790
			public readonly byte[] selectorMtf = new byte[BZip2.MaxSelectors];

			// Token: 0x04000AE7 RID: 2791
			public readonly byte[] generateMTFValues_yy = new byte[256];

			// Token: 0x04000AE8 RID: 2792
			public byte[][] sendMTFValues_len;

			// Token: 0x04000AE9 RID: 2793
			public int[][] sendMTFValues_rfreq;

			// Token: 0x04000AEA RID: 2794
			public readonly int[] sendMTFValues_fave = new int[BZip2.NGroups];

			// Token: 0x04000AEB RID: 2795
			public readonly short[] sendMTFValues_cost = new short[BZip2.NGroups];

			// Token: 0x04000AEC RID: 2796
			public int[][] sendMTFValues_code;

			// Token: 0x04000AED RID: 2797
			public readonly byte[] sendMTFValues2_pos = new byte[BZip2.NGroups];

			// Token: 0x04000AEE RID: 2798
			public readonly bool[] sentMTFValues4_inUse16 = new bool[16];

			// Token: 0x04000AEF RID: 2799
			public readonly int[] stack_ll = new int[BZip2.QSORT_STACK_SIZE];

			// Token: 0x04000AF0 RID: 2800
			public readonly int[] stack_hh = new int[BZip2.QSORT_STACK_SIZE];

			// Token: 0x04000AF1 RID: 2801
			public readonly int[] stack_dd = new int[BZip2.QSORT_STACK_SIZE];

			// Token: 0x04000AF2 RID: 2802
			public readonly int[] mainSort_runningOrder = new int[256];

			// Token: 0x04000AF3 RID: 2803
			public readonly int[] mainSort_copy = new int[256];

			// Token: 0x04000AF4 RID: 2804
			public readonly bool[] mainSort_bigDone = new bool[256];

			// Token: 0x04000AF5 RID: 2805
			public int[] heap = new int[BZip2.MaxAlphaSize + 2];

			// Token: 0x04000AF6 RID: 2806
			public int[] weight = new int[BZip2.MaxAlphaSize * 2];

			// Token: 0x04000AF7 RID: 2807
			public int[] parent = new int[BZip2.MaxAlphaSize * 2];

			// Token: 0x04000AF8 RID: 2808
			public readonly int[] ftab = new int[65537];

			// Token: 0x04000AF9 RID: 2809
			public byte[] block;

			// Token: 0x04000AFA RID: 2810
			public int[] fmap;

			// Token: 0x04000AFB RID: 2811
			public char[] sfmap;

			// Token: 0x04000AFC RID: 2812
			public char[] quadrant;
		}
	}
}
