using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000166 RID: 358
	[NullableContext(1)]
	[Nullable(0)]
	internal struct DateTimeParser
	{
		// Token: 0x06000D06 RID: 3334 RVA: 0x0004BA00 File Offset: 0x00049C00
		public bool Parse(char[] text, int startIndex, int length)
		{
			this._text = text;
			this._end = startIndex + length;
			return this.ParseDate(startIndex) && this.ParseChar(DateTimeParser.Lzyyyy_MM_dd + startIndex, 'T') && this.ParseTimeAndZoneAndWhitespace(DateTimeParser.Lzyyyy_MM_ddT + startIndex);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0004BA58 File Offset: 0x00049C58
		private bool ParseDate(int start)
		{
			return this.Parse4Digit(start, out this.Year) && 1 <= this.Year && this.ParseChar(start + DateTimeParser.Lzyyyy, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_, out this.Month) && 1 <= this.Month && this.Month <= 12 && this.ParseChar(start + DateTimeParser.Lzyyyy_MM, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_MM_, out this.Day) && 1 <= this.Day && this.Day <= DateTime.DaysInMonth(this.Year, this.Month);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0004BB24 File Offset: 0x00049D24
		private bool ParseTimeAndZoneAndWhitespace(int start)
		{
			return this.ParseTime(ref start) && this.ParseZone(start);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0004BB3C File Offset: 0x00049D3C
		private bool ParseTime(ref int start)
		{
			if (!this.Parse2Digit(start, out this.Hour) || this.Hour > 24 || !this.ParseChar(start + DateTimeParser.LzHH, ':') || !this.Parse2Digit(start + DateTimeParser.LzHH_, out this.Minute) || this.Minute >= 60 || !this.ParseChar(start + DateTimeParser.LzHH_mm, ':') || !this.Parse2Digit(start + DateTimeParser.LzHH_mm_, out this.Second) || this.Second >= 60 || (this.Hour == 24 && (this.Minute != 0 || this.Second != 0)))
			{
				return false;
			}
			start += DateTimeParser.LzHH_mm_ss;
			if (this.ParseChar(start, '.'))
			{
				this.Fraction = 0;
				int num = 0;
				for (;;)
				{
					int num2 = start + 1;
					start = num2;
					if (num2 >= this._end || num >= 7)
					{
						break;
					}
					int num3 = (int)(this._text[start] - '0');
					if (num3 < 0 || num3 > 9)
					{
						break;
					}
					this.Fraction = this.Fraction * 10 + num3;
					num++;
				}
				if (num < 7)
				{
					if (num == 0)
					{
						return false;
					}
					this.Fraction *= DateTimeParser.Power10[7 - num];
				}
				if (this.Hour == 24 && this.Fraction != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0004BCB8 File Offset: 0x00049EB8
		private bool ParseZone(int start)
		{
			if (start < this._end)
			{
				char c = this._text[start];
				if (c == 'Z' || c == 'z')
				{
					this.Zone = ParserTimeZone.Utc;
					start++;
				}
				else
				{
					if (start + 2 < this._end && this.Parse2Digit(start + DateTimeParser.Lz_, out this.ZoneHour) && this.ZoneHour <= 99)
					{
						if (c != '+')
						{
							if (c == '-')
							{
								this.Zone = ParserTimeZone.LocalWestOfUtc;
								start += DateTimeParser.Lz_zz;
							}
						}
						else
						{
							this.Zone = ParserTimeZone.LocalEastOfUtc;
							start += DateTimeParser.Lz_zz;
						}
					}
					if (start < this._end)
					{
						if (this.ParseChar(start, ':'))
						{
							start++;
							if (start + 1 < this._end && this.Parse2Digit(start, out this.ZoneMinute) && this.ZoneMinute <= 99)
							{
								start += 2;
							}
						}
						else if (start + 1 < this._end && this.Parse2Digit(start, out this.ZoneMinute) && this.ZoneMinute <= 99)
						{
							start += 2;
						}
					}
				}
			}
			return start == this._end;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0004BDF8 File Offset: 0x00049FF8
		private bool Parse4Digit(int start, out int num)
		{
			if (start + 3 < this._end)
			{
				int num2 = (int)(this._text[start] - '0');
				int num3 = (int)(this._text[start + 1] - '0');
				int num4 = (int)(this._text[start + 2] - '0');
				int num5 = (int)(this._text[start + 3] - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10 && 0 <= num4 && num4 < 10 && 0 <= num5 && num5 < 10)
				{
					num = ((num2 * 10 + num3) * 10 + num4) * 10 + num5;
					return true;
				}
			}
			num = 0;
			return false;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0004BEA4 File Offset: 0x0004A0A4
		private bool Parse2Digit(int start, out int num)
		{
			if (start + 1 < this._end)
			{
				int num2 = (int)(this._text[start] - '0');
				int num3 = (int)(this._text[start + 1] - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10)
				{
					num = num2 * 10 + num3;
					return true;
				}
			}
			num = 0;
			return false;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0004BF0C File Offset: 0x0004A10C
		private bool ParseChar(int start, char ch)
		{
			return start < this._end && this._text[start] == ch;
		}

		// Token: 0x04000709 RID: 1801
		public int Year;

		// Token: 0x0400070A RID: 1802
		public int Month;

		// Token: 0x0400070B RID: 1803
		public int Day;

		// Token: 0x0400070C RID: 1804
		public int Hour;

		// Token: 0x0400070D RID: 1805
		public int Minute;

		// Token: 0x0400070E RID: 1806
		public int Second;

		// Token: 0x0400070F RID: 1807
		public int Fraction;

		// Token: 0x04000710 RID: 1808
		public int ZoneHour;

		// Token: 0x04000711 RID: 1809
		public int ZoneMinute;

		// Token: 0x04000712 RID: 1810
		public ParserTimeZone Zone;

		// Token: 0x04000713 RID: 1811
		private char[] _text;

		// Token: 0x04000714 RID: 1812
		private int _end;

		// Token: 0x04000715 RID: 1813
		private static readonly int[] Power10 = new int[]
		{
			-1,
			10,
			100,
			1000,
			10000,
			100000,
			1000000
		};

		// Token: 0x04000716 RID: 1814
		private static readonly int Lzyyyy = "yyyy".Length;

		// Token: 0x04000717 RID: 1815
		private static readonly int Lzyyyy_ = "yyyy-".Length;

		// Token: 0x04000718 RID: 1816
		private static readonly int Lzyyyy_MM = "yyyy-MM".Length;

		// Token: 0x04000719 RID: 1817
		private static readonly int Lzyyyy_MM_ = "yyyy-MM-".Length;

		// Token: 0x0400071A RID: 1818
		private static readonly int Lzyyyy_MM_dd = "yyyy-MM-dd".Length;

		// Token: 0x0400071B RID: 1819
		private static readonly int Lzyyyy_MM_ddT = "yyyy-MM-ddT".Length;

		// Token: 0x0400071C RID: 1820
		private static readonly int LzHH = "HH".Length;

		// Token: 0x0400071D RID: 1821
		private static readonly int LzHH_ = "HH:".Length;

		// Token: 0x0400071E RID: 1822
		private static readonly int LzHH_mm = "HH:mm".Length;

		// Token: 0x0400071F RID: 1823
		private static readonly int LzHH_mm_ = "HH:mm:".Length;

		// Token: 0x04000720 RID: 1824
		private static readonly int LzHH_mm_ss = "HH:mm:ss".Length;

		// Token: 0x04000721 RID: 1825
		private static readonly int Lz_ = "-".Length;

		// Token: 0x04000722 RID: 1826
		private static readonly int Lz_zz = "-zz".Length;

		// Token: 0x04000723 RID: 1827
		private const short MaxFractionDigits = 7;
	}
}
