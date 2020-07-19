using System;

namespace Ionic.Zlib
{
	// Token: 0x020000C6 RID: 198
	internal sealed class InflateCodes
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x0002FE98 File Offset: 0x0002E098
		internal InflateCodes()
		{
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0002FEA0 File Offset: 0x0002E0A0
		internal void Init(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index)
		{
			this.mode = 0;
			this.lbits = (byte)bl;
			this.dbits = (byte)bd;
			this.ltree = tl;
			this.ltree_index = tl_index;
			this.dtree = td;
			this.dtree_index = td_index;
			this.tree = null;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0002FEE0 File Offset: 0x0002E0E0
		internal int Process(InflateBlocks blocks, int r)
		{
			ZlibCodec codec = blocks._codec;
			int num = codec.NextIn;
			int num2 = codec.AvailableBytesIn;
			int num3 = blocks.bitb;
			int i = blocks.bitk;
			int num4 = blocks.writeAt;
			int num5 = (num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4);
			for (;;)
			{
				int num6;
				switch (this.mode)
				{
				case 0:
					if (num5 >= 258 && num2 >= 10)
					{
						blocks.bitb = num3;
						blocks.bitk = i;
						codec.AvailableBytesIn = num2;
						codec.TotalBytesIn += (long)(num - codec.NextIn);
						codec.NextIn = num;
						blocks.writeAt = num4;
						r = this.InflateFast((int)this.lbits, (int)this.dbits, this.ltree, this.ltree_index, this.dtree, this.dtree_index, blocks, codec);
						num = codec.NextIn;
						num2 = codec.AvailableBytesIn;
						num3 = blocks.bitb;
						i = blocks.bitk;
						num4 = blocks.writeAt;
						num5 = ((num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4));
						if (r != 0)
						{
							this.mode = ((r == 1) ? 7 : 9);
							continue;
						}
					}
					this.need = (int)this.lbits;
					this.tree = this.ltree;
					this.tree_index = this.ltree_index;
					this.mode = 1;
					goto IL_1AC;
				case 1:
					goto IL_1AC;
				case 2:
					num6 = this.bitsToGet;
					while (i < num6)
					{
						if (num2 == 0)
						{
							goto IL_382;
						}
						r = 0;
						num2--;
						num3 |= (int)(codec.InputBuffer[num++] & byte.MaxValue) << i;
						i += 8;
					}
					this.len += (num3 & InternalInflateConstants.InflateMask[num6]);
					num3 >>= num6;
					i -= num6;
					this.need = (int)this.dbits;
					this.tree = this.dtree;
					this.tree_index = this.dtree_index;
					this.mode = 3;
					goto IL_43D;
				case 3:
					goto IL_43D;
				case 4:
					num6 = this.bitsToGet;
					while (i < num6)
					{
						if (num2 == 0)
						{
							goto IL_5D9;
						}
						r = 0;
						num2--;
						num3 |= (int)(codec.InputBuffer[num++] & byte.MaxValue) << i;
						i += 8;
					}
					this.dist += (num3 & InternalInflateConstants.InflateMask[num6]);
					num3 >>= num6;
					i -= num6;
					this.mode = 5;
					goto IL_670;
				case 5:
					goto IL_670;
				case 6:
					if (num5 == 0)
					{
						if (num4 == blocks.end && blocks.readAt != 0)
						{
							num4 = 0;
							num5 = ((num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4));
						}
						if (num5 == 0)
						{
							blocks.writeAt = num4;
							r = blocks.Flush(r);
							num4 = blocks.writeAt;
							num5 = ((num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4));
							if (num4 == blocks.end && blocks.readAt != 0)
							{
								num4 = 0;
								num5 = ((num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4));
							}
							if (num5 == 0)
							{
								goto Block_44;
							}
						}
					}
					r = 0;
					blocks.window[num4++] = (byte)this.lit;
					num5--;
					this.mode = 0;
					continue;
				case 7:
					goto IL_954;
				case 8:
					goto IL_A05;
				case 9:
					goto IL_A4B;
				}
				break;
				IL_1AC:
				num6 = this.need;
				while (i < num6)
				{
					if (num2 == 0)
					{
						goto IL_1C8;
					}
					r = 0;
					num2--;
					num3 |= (int)(codec.InputBuffer[num++] & byte.MaxValue) << i;
					i += 8;
				}
				int num7 = (this.tree_index + (num3 & InternalInflateConstants.InflateMask[num6])) * 3;
				num3 >>= this.tree[num7 + 1];
				i -= this.tree[num7 + 1];
				int num8 = this.tree[num7];
				if (num8 == 0)
				{
					this.lit = this.tree[num7 + 2];
					this.mode = 6;
					continue;
				}
				if ((num8 & 16) != 0)
				{
					this.bitsToGet = (num8 & 15);
					this.len = this.tree[num7 + 2];
					this.mode = 2;
					continue;
				}
				if ((num8 & 64) == 0)
				{
					this.need = num8;
					this.tree_index = num7 / 3 + this.tree[num7 + 2];
					continue;
				}
				if ((num8 & 32) != 0)
				{
					this.mode = 7;
					continue;
				}
				goto IL_30C;
				IL_43D:
				num6 = this.need;
				while (i < num6)
				{
					if (num2 == 0)
					{
						goto IL_459;
					}
					r = 0;
					num2--;
					num3 |= (int)(codec.InputBuffer[num++] & byte.MaxValue) << i;
					i += 8;
				}
				num7 = (this.tree_index + (num3 & InternalInflateConstants.InflateMask[num6])) * 3;
				num3 >>= this.tree[num7 + 1];
				i -= this.tree[num7 + 1];
				num8 = this.tree[num7];
				if ((num8 & 16) != 0)
				{
					this.bitsToGet = (num8 & 15);
					this.dist = this.tree[num7 + 2];
					this.mode = 4;
					continue;
				}
				if ((num8 & 64) == 0)
				{
					this.need = num8;
					this.tree_index = num7 / 3 + this.tree[num7 + 2];
					continue;
				}
				goto IL_563;
				IL_670:
				int j;
				for (j = num4 - this.dist; j < 0; j += blocks.end)
				{
				}
				while (this.len != 0)
				{
					if (num5 == 0)
					{
						if (num4 == blocks.end && blocks.readAt != 0)
						{
							num4 = 0;
							num5 = ((num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4));
						}
						if (num5 == 0)
						{
							blocks.writeAt = num4;
							r = blocks.Flush(r);
							num4 = blocks.writeAt;
							num5 = ((num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4));
							if (num4 == blocks.end && blocks.readAt != 0)
							{
								num4 = 0;
								num5 = ((num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4));
							}
							if (num5 == 0)
							{
								goto Block_32;
							}
						}
					}
					blocks.window[num4++] = blocks.window[j++];
					num5--;
					if (j == blocks.end)
					{
						j = 0;
					}
					this.len--;
				}
				this.mode = 0;
			}
			r = -2;
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_1C8:
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_30C:
			this.mode = 9;
			codec.Message = "invalid literal/length code";
			r = -3;
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_382:
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_459:
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_563:
			this.mode = 9;
			codec.Message = "invalid distance code";
			r = -3;
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_5D9:
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			Block_32:
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			Block_44:
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_954:
			if (i > 7)
			{
				i -= 8;
				num2++;
				num--;
			}
			blocks.writeAt = num4;
			r = blocks.Flush(r);
			num4 = blocks.writeAt;
			int num9 = (num4 < blocks.readAt) ? (blocks.readAt - num4 - 1) : (blocks.end - num4);
			if (blocks.readAt != blocks.writeAt)
			{
				blocks.bitb = num3;
				blocks.bitk = i;
				codec.AvailableBytesIn = num2;
				codec.TotalBytesIn += (long)(num - codec.NextIn);
				codec.NextIn = num;
				blocks.writeAt = num4;
				return blocks.Flush(r);
			}
			this.mode = 8;
			IL_A05:
			r = 1;
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
			IL_A4B:
			r = -3;
			blocks.bitb = num3;
			blocks.bitk = i;
			codec.AvailableBytesIn = num2;
			codec.TotalBytesIn += (long)(num - codec.NextIn);
			codec.NextIn = num;
			blocks.writeAt = num4;
			return blocks.Flush(r);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x000309CC File Offset: 0x0002EBCC
		internal int InflateFast(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index, InflateBlocks s, ZlibCodec z)
		{
			int num = z.NextIn;
			int num2 = z.AvailableBytesIn;
			int num3 = s.bitb;
			int i = s.bitk;
			int num4 = s.writeAt;
			int num5 = (num4 < s.readAt) ? (s.readAt - num4 - 1) : (s.end - num4);
			int num6 = InternalInflateConstants.InflateMask[bl];
			int num7 = InternalInflateConstants.InflateMask[bd];
			int num10;
			int num11;
			for (;;)
			{
				if (i >= 20)
				{
					int num8 = num3 & num6;
					int num9 = (tl_index + num8) * 3;
					if ((num10 = tl[num9]) == 0)
					{
						num3 >>= tl[num9 + 1];
						i -= tl[num9 + 1];
						s.window[num4++] = (byte)tl[num9 + 2];
						num5--;
					}
					else
					{
						for (;;)
						{
							num3 >>= tl[num9 + 1];
							i -= tl[num9 + 1];
							if ((num10 & 16) != 0)
							{
								break;
							}
							if ((num10 & 64) != 0)
							{
								goto IL_4E2;
							}
							num8 += tl[num9 + 2];
							num8 += (num3 & InternalInflateConstants.InflateMask[num10]);
							num9 = (tl_index + num8) * 3;
							if ((num10 = tl[num9]) == 0)
							{
								goto Block_20;
							}
						}
						num10 &= 15;
						num11 = tl[num9 + 2] + (num3 & InternalInflateConstants.InflateMask[num10]);
						num3 >>= num10;
						for (i -= num10; i < 15; i += 8)
						{
							num2--;
							num3 |= (int)(z.InputBuffer[num++] & byte.MaxValue) << i;
						}
						num8 = (num3 & num7);
						num9 = (td_index + num8) * 3;
						num10 = td[num9];
						for (;;)
						{
							num3 >>= td[num9 + 1];
							i -= td[num9 + 1];
							if ((num10 & 16) != 0)
							{
								break;
							}
							if ((num10 & 64) != 0)
							{
								goto IL_3EC;
							}
							num8 += td[num9 + 2];
							num8 += (num3 & InternalInflateConstants.InflateMask[num10]);
							num9 = (td_index + num8) * 3;
							num10 = td[num9];
						}
						num10 &= 15;
						while (i < num10)
						{
							num2--;
							num3 |= (int)(z.InputBuffer[num++] & byte.MaxValue) << i;
							i += 8;
						}
						int num12 = td[num9 + 2] + (num3 & InternalInflateConstants.InflateMask[num10]);
						num3 >>= num10;
						i -= num10;
						num5 -= num11;
						int num13;
						if (num4 >= num12)
						{
							num13 = num4 - num12;
							if (num4 - num13 > 0 && 2 > num4 - num13)
							{
								s.window[num4++] = s.window[num13++];
								s.window[num4++] = s.window[num13++];
								num11 -= 2;
							}
							else
							{
								Array.Copy(s.window, num13, s.window, num4, 2);
								num4 += 2;
								num13 += 2;
								num11 -= 2;
							}
						}
						else
						{
							num13 = num4 - num12;
							do
							{
								num13 += s.end;
							}
							while (num13 < 0);
							num10 = s.end - num13;
							if (num11 > num10)
							{
								num11 -= num10;
								if (num4 - num13 > 0 && num10 > num4 - num13)
								{
									do
									{
										s.window[num4++] = s.window[num13++];
									}
									while (--num10 != 0);
								}
								else
								{
									Array.Copy(s.window, num13, s.window, num4, num10);
									num4 += num10;
									num13 += num10;
								}
								num13 = 0;
							}
						}
						if (num4 - num13 > 0 && num11 > num4 - num13)
						{
							do
							{
								s.window[num4++] = s.window[num13++];
							}
							while (--num11 != 0);
							goto IL_5E3;
						}
						Array.Copy(s.window, num13, s.window, num4, num11);
						num4 += num11;
						num13 += num11;
						goto IL_5E3;
						Block_20:
						num3 >>= tl[num9 + 1];
						i -= tl[num9 + 1];
						s.window[num4++] = (byte)tl[num9 + 2];
						num5--;
					}
					IL_5E3:
					if (num5 < 258 || num2 < 10)
					{
						goto IL_5F7;
					}
				}
				else
				{
					num2--;
					num3 |= (int)(z.InputBuffer[num++] & byte.MaxValue) << i;
					i += 8;
				}
			}
			IL_3EC:
			z.Message = "invalid distance code";
			num11 = z.AvailableBytesIn - num2;
			num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
			num2 += num11;
			num -= num11;
			i -= num11 << 3;
			s.bitb = num3;
			s.bitk = i;
			z.AvailableBytesIn = num2;
			z.TotalBytesIn += (long)(num - z.NextIn);
			z.NextIn = num;
			s.writeAt = num4;
			return -3;
			IL_4E2:
			if ((num10 & 32) != 0)
			{
				num11 = z.AvailableBytesIn - num2;
				num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
				num2 += num11;
				num -= num11;
				i -= num11 << 3;
				s.bitb = num3;
				s.bitk = i;
				z.AvailableBytesIn = num2;
				z.TotalBytesIn += (long)(num - z.NextIn);
				z.NextIn = num;
				s.writeAt = num4;
				return 1;
			}
			z.Message = "invalid literal/length code";
			num11 = z.AvailableBytesIn - num2;
			num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
			num2 += num11;
			num -= num11;
			i -= num11 << 3;
			s.bitb = num3;
			s.bitk = i;
			z.AvailableBytesIn = num2;
			z.TotalBytesIn += (long)(num - z.NextIn);
			z.NextIn = num;
			s.writeAt = num4;
			return -3;
			IL_5F7:
			num11 = z.AvailableBytesIn - num2;
			num11 = ((i >> 3 < num11) ? (i >> 3) : num11);
			num2 += num11;
			num -= num11;
			i -= num11 << 3;
			s.bitb = num3;
			s.bitk = i;
			z.AvailableBytesIn = num2;
			z.TotalBytesIn += (long)(num - z.NextIn);
			z.NextIn = num;
			s.writeAt = num4;
			return 0;
		}

		// Token: 0x040003B6 RID: 950
		private const int START = 0;

		// Token: 0x040003B7 RID: 951
		private const int LEN = 1;

		// Token: 0x040003B8 RID: 952
		private const int LENEXT = 2;

		// Token: 0x040003B9 RID: 953
		private const int DIST = 3;

		// Token: 0x040003BA RID: 954
		private const int DISTEXT = 4;

		// Token: 0x040003BB RID: 955
		private const int COPY = 5;

		// Token: 0x040003BC RID: 956
		private const int LIT = 6;

		// Token: 0x040003BD RID: 957
		private const int WASH = 7;

		// Token: 0x040003BE RID: 958
		private const int END = 8;

		// Token: 0x040003BF RID: 959
		private const int BADCODE = 9;

		// Token: 0x040003C0 RID: 960
		internal int mode;

		// Token: 0x040003C1 RID: 961
		internal int len;

		// Token: 0x040003C2 RID: 962
		internal int[] tree;

		// Token: 0x040003C3 RID: 963
		internal int tree_index;

		// Token: 0x040003C4 RID: 964
		internal int need;

		// Token: 0x040003C5 RID: 965
		internal int lit;

		// Token: 0x040003C6 RID: 966
		internal int bitsToGet;

		// Token: 0x040003C7 RID: 967
		internal int dist;

		// Token: 0x040003C8 RID: 968
		internal byte lbits;

		// Token: 0x040003C9 RID: 969
		internal byte dbits;

		// Token: 0x040003CA RID: 970
		internal int[] ltree;

		// Token: 0x040003CB RID: 971
		internal int ltree_index;

		// Token: 0x040003CC RID: 972
		internal int[] dtree;

		// Token: 0x040003CD RID: 973
		internal int dtree_index;
	}
}
