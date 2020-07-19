using System;
using System.Collections.Generic;

// Token: 0x020000DE RID: 222
public class BigInteger
{
	// Token: 0x06000781 RID: 1921 RVA: 0x0003576C File Offset: 0x0003396C
	public BigInteger()
	{
		this.data = new uint[1000];
		this.dataLength = 1;
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0003578C File Offset: 0x0003398C
	public BigInteger(long value)
	{
		this.data = new uint[1000];
		long num = value;
		this.dataLength = 0;
		while (value != 0L && this.dataLength < 1000)
		{
			this.data[this.dataLength] = (uint)(value & (long)((ulong)-1));
			value >>= 32;
			this.dataLength++;
		}
		if (num > 0L)
		{
			if (value != 0L || (this.data[999] & 2147483648u) != 0u)
			{
				throw new ArithmeticException("Positive overflow in constructor.");
			}
		}
		else if (num < 0L && (value != -1L || (this.data[this.dataLength - 1] & 2147483648u) == 0u))
		{
			throw new ArithmeticException("Negative underflow in constructor.");
		}
		if (this.dataLength == 0)
		{
			this.dataLength = 1;
		}
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00035874 File Offset: 0x00033A74
	public BigInteger(ulong value)
	{
		this.data = new uint[1000];
		this.dataLength = 0;
		while (value != 0UL && this.dataLength < 1000)
		{
			this.data[this.dataLength] = (uint)(value & (ulong)-1);
			value >>= 32;
			this.dataLength++;
		}
		if (value != 0UL || (this.data[999] & 2147483648u) != 0u)
		{
			throw new ArithmeticException("Positive overflow in constructor.");
		}
		if (this.dataLength == 0)
		{
			this.dataLength = 1;
		}
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0003591C File Offset: 0x00033B1C
	public BigInteger(BigInteger bi)
	{
		this.data = new uint[1000];
		this.dataLength = bi.dataLength;
		for (int i = 0; i < this.dataLength; i++)
		{
			this.data[i] = bi.data[i];
		}
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x00035974 File Offset: 0x00033B74
	public BigInteger(string value, int radix)
	{
		BigInteger bi = new BigInteger(1L);
		BigInteger bigInteger = new BigInteger();
		value = value.ToUpper().Trim();
		int num = 0;
		if (value[0] == '-')
		{
			num = 1;
		}
		for (int i = value.Length - 1; i >= num; i--)
		{
			int num2 = (int)value[i];
			if (num2 >= 48 && num2 <= 57)
			{
				num2 -= 48;
			}
			else if (num2 >= 65 && num2 <= 90)
			{
				num2 = num2 - 65 + 10;
			}
			else
			{
				num2 = 9999999;
			}
			if (num2 >= radix)
			{
				throw new ArithmeticException("Invalid string in constructor.");
			}
			if (value[0] == '-')
			{
				num2 = -num2;
			}
			bigInteger += bi * num2;
			if (i - 1 >= num)
			{
				bi *= radix;
			}
		}
		if (value[0] == '-')
		{
			if ((bigInteger.data[999] & 2147483648u) == 0u)
			{
				throw new ArithmeticException("Negative underflow in constructor.");
			}
		}
		else if ((bigInteger.data[999] & 2147483648u) != 0u)
		{
			throw new ArithmeticException("Positive overflow in constructor.");
		}
		this.data = new uint[1000];
		for (int j = 0; j < bigInteger.dataLength; j++)
		{
			this.data[j] = bigInteger.data[j];
		}
		this.dataLength = bigInteger.dataLength;
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x00035B08 File Offset: 0x00033D08
	public BigInteger(IList<byte> inData, int length = -1, int offset = 0)
	{
		int num = (length == -1) ? (inData.Count - offset) : length;
		this.dataLength = num >> 2;
		int num2 = num & 3;
		if (num2 != 0)
		{
			this.dataLength++;
		}
		if (this.dataLength > 1000 || num > inData.Count - offset)
		{
			throw new ArithmeticException("Byte overflow in constructor.");
		}
		this.data = new uint[1000];
		int i = num - 1;
		int num3 = 0;
		while (i >= 3)
		{
			this.data[num3] = (uint)(((int)inData[offset + i - 3] << 24) + ((int)inData[offset + i - 2] << 16) + ((int)inData[offset + i - 1] << 8) + (int)inData[offset + i]);
			i -= 4;
			num3++;
		}
		if (num2 == 1)
		{
			this.data[this.dataLength - 1] = (uint)inData[offset];
		}
		else if (num2 == 2)
		{
			this.data[this.dataLength - 1] = (uint)(((int)inData[offset] << 8) + (int)inData[offset + 1]);
		}
		else if (num2 == 3)
		{
			this.data[this.dataLength - 1] = (uint)(((int)inData[offset] << 16) + ((int)inData[offset + 1] << 8) + (int)inData[offset + 2]);
		}
		if (this.dataLength == 0)
		{
			this.dataLength = 1;
		}
		while (this.dataLength > 1 && this.data[this.dataLength - 1] == 0u)
		{
			this.dataLength--;
		}
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x00035CAC File Offset: 0x00033EAC
	public BigInteger(uint[] inData)
	{
		this.dataLength = inData.Length;
		if (this.dataLength > 1000)
		{
			throw new ArithmeticException("Byte overflow in constructor.");
		}
		this.data = new uint[1000];
		int i = this.dataLength - 1;
		int num = 0;
		while (i >= 0)
		{
			this.data[num] = inData[i];
			i--;
			num++;
		}
		while (this.dataLength > 1 && this.data[this.dataLength - 1] == 0u)
		{
			this.dataLength--;
		}
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x00035D50 File Offset: 0x00033F50
	public static implicit operator BigInteger(long value)
	{
		return new BigInteger(value);
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x00035D58 File Offset: 0x00033F58
	public static implicit operator BigInteger(ulong value)
	{
		return new BigInteger(value);
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x00035D60 File Offset: 0x00033F60
	public static implicit operator BigInteger(int value)
	{
		return new BigInteger((long)value);
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x00035D6C File Offset: 0x00033F6C
	public static implicit operator BigInteger(uint value)
	{
		return new BigInteger((ulong)value);
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x00035D78 File Offset: 0x00033F78
	public static BigInteger operator +(BigInteger bi1, BigInteger bi2)
	{
		BigInteger bigInteger = new BigInteger
		{
			dataLength = ((bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength)
		};
		long num = 0L;
		for (int i = 0; i < bigInteger.dataLength; i++)
		{
			long num2 = (long)((ulong)bi1.data[i] + (ulong)bi2.data[i] + (ulong)num);
			num = num2 >> 32;
			bigInteger.data[i] = (uint)(num2 & (long)((ulong)-1));
		}
		if (num != 0L && bigInteger.dataLength < 1000)
		{
			bigInteger.data[bigInteger.dataLength] = (uint)num;
			bigInteger.dataLength++;
		}
		while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
		{
			bigInteger.dataLength--;
		}
		int num3 = 999;
		if ((bi1.data[num3] & 2147483648u) == (bi2.data[num3] & 2147483648u) && (bigInteger.data[num3] & 2147483648u) != (bi1.data[num3] & 2147483648u))
		{
			throw new ArithmeticException();
		}
		return bigInteger;
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x00035EB0 File Offset: 0x000340B0
	public static BigInteger operator ++(BigInteger bi1)
	{
		BigInteger bigInteger = new BigInteger(bi1);
		long num = 1L;
		int num2 = 0;
		while (num != 0L && num2 < 1000)
		{
			long num3 = (long)((ulong)bigInteger.data[num2]);
			num3 += 1L;
			bigInteger.data[num2] = (uint)(num3 & (long)((ulong)-1));
			num = num3 >> 32;
			num2++;
		}
		if (num2 > bigInteger.dataLength)
		{
			bigInteger.dataLength = num2;
		}
		else
		{
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
			{
				bigInteger.dataLength--;
			}
		}
		int num4 = 999;
		if ((bi1.data[num4] & 2147483648u) == 0u && (bigInteger.data[num4] & 2147483648u) != (bi1.data[num4] & 2147483648u))
		{
			throw new ArithmeticException("Overflow in ++.");
		}
		return bigInteger;
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x00035F94 File Offset: 0x00034194
	public static BigInteger operator -(BigInteger bi1, BigInteger bi2)
	{
		BigInteger bigInteger = new BigInteger
		{
			dataLength = ((bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength)
		};
		long num = 0L;
		for (int i = 0; i < bigInteger.dataLength; i++)
		{
			long num2 = (long)((ulong)bi1.data[i] - (ulong)bi2.data[i] - (ulong)num);
			bigInteger.data[i] = (uint)(num2 & (long)((ulong)-1));
			if (num2 < 0L)
			{
				num = 1L;
			}
			else
			{
				num = 0L;
			}
		}
		if (num != 0L)
		{
			for (int j = bigInteger.dataLength; j < 1000; j++)
			{
				bigInteger.data[j] = uint.MaxValue;
			}
			bigInteger.dataLength = 1000;
		}
		while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
		{
			bigInteger.dataLength--;
		}
		int num3 = 999;
		if ((bi1.data[num3] & 2147483648u) != (bi2.data[num3] & 2147483648u) && (bigInteger.data[num3] & 2147483648u) != (bi1.data[num3] & 2147483648u))
		{
			throw new ArithmeticException();
		}
		return bigInteger;
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x000360DC File Offset: 0x000342DC
	public static BigInteger operator --(BigInteger bi1)
	{
		BigInteger bigInteger = new BigInteger(bi1);
		bool flag = true;
		int num = 0;
		while (flag && num < 1000)
		{
			long num2 = (long)((ulong)bigInteger.data[num]);
			num2 -= 1L;
			bigInteger.data[num] = (uint)(num2 & (long)((ulong)-1));
			if (num2 >= 0L)
			{
				flag = false;
			}
			num++;
		}
		if (num > bigInteger.dataLength)
		{
			bigInteger.dataLength = num;
		}
		while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
		{
			bigInteger.dataLength--;
		}
		int num3 = 999;
		if ((bi1.data[num3] & 2147483648u) != 0u && (bigInteger.data[num3] & 2147483648u) != (bi1.data[num3] & 2147483648u))
		{
			throw new ArithmeticException("Underflow in --.");
		}
		return bigInteger;
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x000361C4 File Offset: 0x000343C4
	public static BigInteger operator *(BigInteger bi1, BigInteger bi2)
	{
		int num = 999;
		bool flag = false;
		bool flag2 = false;
		try
		{
			if ((bi1.data[num] & 2147483648u) != 0u)
			{
				flag = true;
				bi1 = -bi1;
			}
			if ((bi2.data[num] & 2147483648u) != 0u)
			{
				flag2 = true;
				bi2 = -bi2;
			}
		}
		catch (Exception)
		{
		}
		BigInteger bigInteger = new BigInteger();
		try
		{
			for (int i = 0; i < bi1.dataLength; i++)
			{
				if (bi1.data[i] != 0u)
				{
					ulong num2 = 0UL;
					int j = 0;
					int num3 = i;
					while (j < bi2.dataLength)
					{
						ulong num4 = (ulong)bi1.data[i] * (ulong)bi2.data[j] + (ulong)bigInteger.data[num3] + num2;
						bigInteger.data[num3] = (uint)(num4 & (ulong)-1);
						num2 = num4 >> 32;
						j++;
						num3++;
					}
					if (num2 != 0UL)
					{
						bigInteger.data[i + bi2.dataLength] = (uint)num2;
					}
				}
			}
		}
		catch (Exception)
		{
			throw new ArithmeticException("Multiplication overflow.");
		}
		bigInteger.dataLength = bi1.dataLength + bi2.dataLength;
		if (bigInteger.dataLength > 1000)
		{
			bigInteger.dataLength = 1000;
		}
		while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
		{
			bigInteger.dataLength--;
		}
		if ((bigInteger.data[num] & 2147483648u) != 0u)
		{
			if (flag != flag2 && bigInteger.data[num] == 2147483648u)
			{
				if (bigInteger.dataLength == 1)
				{
					return bigInteger;
				}
				bool flag3 = true;
				int num5 = 0;
				while (num5 < bigInteger.dataLength - 1 && flag3)
				{
					if (bigInteger.data[num5] != 0u)
					{
						flag3 = false;
					}
					num5++;
				}
				if (flag3)
				{
					return bigInteger;
				}
			}
			throw new ArithmeticException("Multiplication overflow.");
		}
		if (flag != flag2)
		{
			return -bigInteger;
		}
		return bigInteger;
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x000363E8 File Offset: 0x000345E8
	public static BigInteger operator <<(BigInteger bi1, int shiftVal)
	{
		BigInteger bigInteger = new BigInteger(bi1);
		bigInteger.dataLength = BigInteger.shiftLeft(bigInteger.data, shiftVal);
		return bigInteger;
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x00036404 File Offset: 0x00034604
	private static int shiftLeft(uint[] buffer, int shiftVal)
	{
		int num = 32;
		int num2 = buffer.Length;
		while (num2 > 1 && buffer[num2 - 1] == 0u)
		{
			num2--;
		}
		for (int i = shiftVal; i > 0; i -= num)
		{
			if (i < num)
			{
				num = i;
			}
			ulong num3 = 0UL;
			for (int j = 0; j < num2; j++)
			{
				ulong num4 = (ulong)buffer[j] << num;
				num4 |= num3;
				buffer[j] = (uint)(num4 & (ulong)-1);
				num3 = num4 >> 32;
			}
			if (num3 != 0UL && num2 + 1 <= buffer.Length)
			{
				buffer[num2] = (uint)num3;
				num2++;
			}
		}
		return num2;
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x000364A0 File Offset: 0x000346A0
	public static BigInteger operator >>(BigInteger bi1, int shiftVal)
	{
		BigInteger bigInteger = new BigInteger(bi1);
		bigInteger.dataLength = BigInteger.shiftRight(bigInteger.data, shiftVal);
		if ((bi1.data[999] & 2147483648u) != 0u)
		{
			for (int i = 999; i >= bigInteger.dataLength; i--)
			{
				bigInteger.data[i] = uint.MaxValue;
			}
			uint num = 2147483648u;
			int num2 = 0;
			while (num2 < 32 && (bigInteger.data[bigInteger.dataLength - 1] & num) == 0u)
			{
				bigInteger.data[bigInteger.dataLength - 1] |= num;
				num >>= 1;
				num2++;
			}
			bigInteger.dataLength = 1000;
		}
		return bigInteger;
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x00036558 File Offset: 0x00034758
	private static int shiftRight(uint[] buffer, int shiftVal)
	{
		int num = 32;
		int num2 = 0;
		int num3 = buffer.Length;
		while (num3 > 1 && buffer[num3 - 1] == 0u)
		{
			num3--;
		}
		for (int i = shiftVal; i > 0; i -= num)
		{
			if (i < num)
			{
				num = i;
				num2 = 32 - num;
			}
			ulong num4 = 0UL;
			for (int j = num3 - 1; j >= 0; j--)
			{
				ulong num5 = (ulong)buffer[j] >> num;
				num5 |= num4;
				num4 = ((ulong)buffer[j] << num2 & (ulong)-1);
				buffer[j] = (uint)num5;
			}
		}
		while (num3 > 1 && buffer[num3 - 1] == 0u)
		{
			num3--;
		}
		return num3;
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x00036600 File Offset: 0x00034800
	public static BigInteger operator ~(BigInteger bi1)
	{
		BigInteger bigInteger = new BigInteger(bi1);
		for (int i = 0; i < 1000; i++)
		{
			bigInteger.data[i] = ~bi1.data[i];
		}
		bigInteger.dataLength = 1000;
		while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
		{
			bigInteger.dataLength--;
		}
		return bigInteger;
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00036678 File Offset: 0x00034878
	public static BigInteger operator -(BigInteger bi1)
	{
		if (bi1.dataLength == 1 && bi1.data[0] == 0u)
		{
			return new BigInteger();
		}
		BigInteger bigInteger = new BigInteger(bi1);
		for (int i = 0; i < 1000; i++)
		{
			bigInteger.data[i] = ~bi1.data[i];
		}
		long num = 1L;
		int num2 = 0;
		while (num != 0L && num2 < 1000)
		{
			long num3 = (long)((ulong)bigInteger.data[num2]);
			num3 += 1L;
			bigInteger.data[num2] = (uint)(num3 & (long)((ulong)-1));
			num = num3 >> 32;
			num2++;
		}
		if ((bi1.data[999] & 2147483648u) == (bigInteger.data[999] & 2147483648u))
		{
			throw new ArithmeticException("Overflow in negation.\n");
		}
		bigInteger.dataLength = 1000;
		while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
		{
			bigInteger.dataLength--;
		}
		return bigInteger;
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x00036788 File Offset: 0x00034988
	public static bool operator ==(BigInteger bi1, BigInteger bi2)
	{
		return bi1.Equals(bi2);
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00036794 File Offset: 0x00034994
	public static bool operator !=(BigInteger bi1, BigInteger bi2)
	{
		return !bi1.Equals(bi2);
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x000367A0 File Offset: 0x000349A0
	public override bool Equals(object o)
	{
		BigInteger bigInteger = (BigInteger)o;
		if (this.dataLength != bigInteger.dataLength)
		{
			return false;
		}
		for (int i = 0; i < this.dataLength; i++)
		{
			if (this.data[i] != bigInteger.data[i])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x000367F8 File Offset: 0x000349F8
	public override int GetHashCode()
	{
		return this.ToString().GetHashCode();
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x00036808 File Offset: 0x00034A08
	public static bool operator >(BigInteger bi1, BigInteger bi2)
	{
		int num = 999;
		if ((bi1.data[num] & 2147483648u) != 0u && (bi2.data[num] & 2147483648u) == 0u)
		{
			return false;
		}
		if ((bi1.data[num] & 2147483648u) == 0u && (bi2.data[num] & 2147483648u) != 0u)
		{
			return true;
		}
		num = ((bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength) - 1;
		while (num >= 0 && bi1.data[num] == bi2.data[num])
		{
			num--;
		}
		return num >= 0 && bi1.data[num] > bi2.data[num];
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x000368D8 File Offset: 0x00034AD8
	public static bool operator <(BigInteger bi1, BigInteger bi2)
	{
		int num = 999;
		if ((bi1.data[num] & 2147483648u) != 0u && (bi2.data[num] & 2147483648u) == 0u)
		{
			return true;
		}
		if ((bi1.data[num] & 2147483648u) == 0u && (bi2.data[num] & 2147483648u) != 0u)
		{
			return false;
		}
		num = ((bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength) - 1;
		while (num >= 0 && bi1.data[num] == bi2.data[num])
		{
			num--;
		}
		return num >= 0 && bi1.data[num] < bi2.data[num];
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x000369A8 File Offset: 0x00034BA8
	public static bool operator >=(BigInteger bi1, BigInteger bi2)
	{
		return bi1 == bi2 || bi1 > bi2;
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x000369C0 File Offset: 0x00034BC0
	public static bool operator <=(BigInteger bi1, BigInteger bi2)
	{
		return bi1 == bi2 || bi1 < bi2;
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x000369D8 File Offset: 0x00034BD8
	private static void multiByteDivide(BigInteger bi1, BigInteger bi2, BigInteger outQuotient, BigInteger outRemainder)
	{
		uint[] array = new uint[1000];
		int num = bi1.dataLength + 1;
		uint[] array2 = new uint[num];
		uint num2 = 2147483648u;
		uint num3 = bi2.data[bi2.dataLength - 1];
		int num4 = 0;
		int num5 = 0;
		while (num2 != 0u && (num3 & num2) == 0u)
		{
			num4++;
			num2 >>= 1;
		}
		for (int i = 0; i < bi1.dataLength; i++)
		{
			array2[i] = bi1.data[i];
		}
		BigInteger.shiftLeft(array2, num4);
		bi2 <<= num4;
		int j = num - bi2.dataLength;
		int num6 = num - 1;
		ulong num7 = (ulong)bi2.data[bi2.dataLength - 1];
		ulong num8 = (ulong)bi2.data[bi2.dataLength - 2];
		int num9 = bi2.dataLength + 1;
		uint[] array3 = new uint[num9];
		while (j > 0)
		{
			ulong num10 = ((ulong)array2[num6] << 32) + (ulong)array2[num6 - 1];
			ulong num11 = num10 / num7;
			ulong num12 = num10 % num7;
			bool flag = false;
			while (!flag)
			{
				flag = true;
				if (num11 == 4294967296UL || num11 * num8 > (num12 << 32) + (ulong)array2[num6 - 2])
				{
					num11 -= 1UL;
					num12 += num7;
					if (num12 < 4294967296UL)
					{
						flag = false;
					}
				}
			}
			for (int k = 0; k < num9; k++)
			{
				array3[k] = array2[num6 - k];
			}
			BigInteger bigInteger = new BigInteger(array3);
			BigInteger bigInteger2 = bi2 * (long)num11;
			while (bigInteger2 > bigInteger)
			{
				num11 -= 1UL;
				bigInteger2 -= bi2;
			}
			BigInteger bigInteger3 = bigInteger - bigInteger2;
			for (int l = 0; l < num9; l++)
			{
				array2[num6 - l] = bigInteger3.data[bi2.dataLength - l];
			}
			array[num5++] = (uint)num11;
			num6--;
			j--;
		}
		outQuotient.dataLength = num5;
		int m = 0;
		int n = outQuotient.dataLength - 1;
		while (n >= 0)
		{
			outQuotient.data[m] = array[n];
			n--;
			m++;
		}
		while (m < 1000)
		{
			outQuotient.data[m] = 0u;
			m++;
		}
		while (outQuotient.dataLength > 1 && outQuotient.data[outQuotient.dataLength - 1] == 0u)
		{
			outQuotient.dataLength--;
		}
		if (outQuotient.dataLength == 0)
		{
			outQuotient.dataLength = 1;
		}
		outRemainder.dataLength = BigInteger.shiftRight(array2, num4);
		for (m = 0; m < outRemainder.dataLength; m++)
		{
			outRemainder.data[m] = array2[m];
		}
		while (m < 1000)
		{
			outRemainder.data[m] = 0u;
			m++;
		}
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x00036CCC File Offset: 0x00034ECC
	private static void singleByteDivide(BigInteger bi1, BigInteger bi2, BigInteger outQuotient, BigInteger outRemainder)
	{
		uint[] array = new uint[1000];
		int num = 0;
		for (int i = 0; i < 1000; i++)
		{
			outRemainder.data[i] = bi1.data[i];
		}
		outRemainder.dataLength = bi1.dataLength;
		while (outRemainder.dataLength > 1 && outRemainder.data[outRemainder.dataLength - 1] == 0u)
		{
			outRemainder.dataLength--;
		}
		ulong num2 = (ulong)bi2.data[0];
		int j = outRemainder.dataLength - 1;
		ulong num3 = (ulong)outRemainder.data[j];
		if (num3 >= num2)
		{
			ulong num4 = num3 / num2;
			array[num++] = (uint)num4;
			outRemainder.data[j] = (uint)(num3 % num2);
		}
		j--;
		while (j >= 0)
		{
			num3 = ((ulong)outRemainder.data[j + 1] << 32) + (ulong)outRemainder.data[j];
			ulong num5 = num3 / num2;
			array[num++] = (uint)num5;
			outRemainder.data[j + 1] = 0u;
			outRemainder.data[j--] = (uint)(num3 % num2);
		}
		outQuotient.dataLength = num;
		int k = 0;
		int l = outQuotient.dataLength - 1;
		while (l >= 0)
		{
			outQuotient.data[k] = array[l];
			l--;
			k++;
		}
		while (k < 1000)
		{
			outQuotient.data[k] = 0u;
			k++;
		}
		while (outQuotient.dataLength > 1 && outQuotient.data[outQuotient.dataLength - 1] == 0u)
		{
			outQuotient.dataLength--;
		}
		if (outQuotient.dataLength == 0)
		{
			outQuotient.dataLength = 1;
		}
		while (outRemainder.dataLength > 1 && outRemainder.data[outRemainder.dataLength - 1] == 0u)
		{
			outRemainder.dataLength--;
		}
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x00036EB4 File Offset: 0x000350B4
	public static BigInteger operator /(BigInteger bi1, BigInteger bi2)
	{
		BigInteger bigInteger = new BigInteger();
		BigInteger outRemainder = new BigInteger();
		int num = 999;
		bool flag = false;
		bool flag2 = false;
		if ((bi1.data[num] & 2147483648u) != 0u)
		{
			bi1 = -bi1;
			flag2 = true;
		}
		if ((bi2.data[num] & 2147483648u) != 0u)
		{
			bi2 = -bi2;
			flag = true;
		}
		if (bi1 < bi2)
		{
			return bigInteger;
		}
		if (bi2.dataLength == 1)
		{
			BigInteger.singleByteDivide(bi1, bi2, bigInteger, outRemainder);
		}
		else
		{
			BigInteger.multiByteDivide(bi1, bi2, bigInteger, outRemainder);
		}
		if (flag2 != flag)
		{
			return -bigInteger;
		}
		return bigInteger;
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x00036F58 File Offset: 0x00035158
	public static BigInteger operator %(BigInteger bi1, BigInteger bi2)
	{
		BigInteger outQuotient = new BigInteger();
		BigInteger bigInteger = new BigInteger(bi1);
		int num = 999;
		bool flag = false;
		if ((bi1.data[num] & 2147483648u) != 0u)
		{
			bi1 = -bi1;
			flag = true;
		}
		if ((bi2.data[num] & 2147483648u) != 0u)
		{
			bi2 = -bi2;
		}
		if (bi1 < bi2)
		{
			return bigInteger;
		}
		if (bi2.dataLength == 1)
		{
			BigInteger.singleByteDivide(bi1, bi2, outQuotient, bigInteger);
		}
		else
		{
			BigInteger.multiByteDivide(bi1, bi2, outQuotient, bigInteger);
		}
		if (flag)
		{
			return -bigInteger;
		}
		return bigInteger;
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x00036FF8 File Offset: 0x000351F8
	public static BigInteger operator &(BigInteger bi1, BigInteger bi2)
	{
		BigInteger bigInteger = new BigInteger();
		int num = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
		for (int i = 0; i < num; i++)
		{
			uint num2 = bi1.data[i] & bi2.data[i];
			bigInteger.data[i] = num2;
		}
		bigInteger.dataLength = 1000;
		while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
		{
			bigInteger.dataLength--;
		}
		return bigInteger;
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x00037098 File Offset: 0x00035298
	public static BigInteger operator |(BigInteger bi1, BigInteger bi2)
	{
		BigInteger bigInteger = new BigInteger();
		int num = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
		for (int i = 0; i < num; i++)
		{
			uint num2 = bi1.data[i] | bi2.data[i];
			bigInteger.data[i] = num2;
		}
		bigInteger.dataLength = 1000;
		while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
		{
			bigInteger.dataLength--;
		}
		return bigInteger;
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x00037138 File Offset: 0x00035338
	public static BigInteger operator ^(BigInteger bi1, BigInteger bi2)
	{
		BigInteger bigInteger = new BigInteger();
		int num = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
		for (int i = 0; i < num; i++)
		{
			uint num2 = bi1.data[i] ^ bi2.data[i];
			bigInteger.data[i] = num2;
		}
		bigInteger.dataLength = 1000;
		while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
		{
			bigInteger.dataLength--;
		}
		return bigInteger;
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x000371D8 File Offset: 0x000353D8
	public BigInteger max(BigInteger bi)
	{
		if (this > bi)
		{
			return new BigInteger(this);
		}
		return new BigInteger(bi);
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x000371F4 File Offset: 0x000353F4
	public BigInteger min(BigInteger bi)
	{
		if (this < bi)
		{
			return new BigInteger(this);
		}
		return new BigInteger(bi);
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x00037210 File Offset: 0x00035410
	public BigInteger abs()
	{
		if ((this.data[999] & 2147483648u) != 0u)
		{
			return -this;
		}
		return new BigInteger(this);
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x00037238 File Offset: 0x00035438
	public override string ToString()
	{
		return this.ToString(10);
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x00037244 File Offset: 0x00035444
	public string ToString(int radix)
	{
		if (radix < 2 || radix > 36)
		{
			throw new ArgumentException("Radix must be >= 2 and <= 36");
		}
		string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string text2 = "";
		BigInteger bigInteger = this;
		bool flag = false;
		if ((bigInteger.data[999] & 2147483648u) != 0u)
		{
			flag = true;
			try
			{
				bigInteger = -bigInteger;
			}
			catch (Exception)
			{
			}
		}
		BigInteger bigInteger2 = new BigInteger();
		BigInteger bigInteger3 = new BigInteger();
		BigInteger bi = new BigInteger((long)radix);
		if (bigInteger.dataLength == 1 && bigInteger.data[0] == 0u)
		{
			text2 = "0";
		}
		else
		{
			while (bigInteger.dataLength > 1 || (bigInteger.dataLength == 1 && bigInteger.data[0] != 0u))
			{
				BigInteger.singleByteDivide(bigInteger, bi, bigInteger2, bigInteger3);
				if (bigInteger3.data[0] < 10u)
				{
					text2 = bigInteger3.data[0] + text2;
				}
				else
				{
					text2 = text[(int)(bigInteger3.data[0] - 10u)].ToString() + text2;
				}
				bigInteger = bigInteger2;
			}
			if (flag)
			{
				text2 = "-" + text2;
			}
		}
		return text2;
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x00037380 File Offset: 0x00035580
	public string ToHexString()
	{
		string text = this.data[this.dataLength - 1].ToString("X");
		for (int i = this.dataLength - 2; i >= 0; i--)
		{
			text += this.data[i].ToString("X8");
		}
		return text;
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x000373E4 File Offset: 0x000355E4
	public BigInteger modPow(BigInteger exp, BigInteger n)
	{
		if ((exp.data[999] & 2147483648u) != 0u)
		{
			throw new ArithmeticException("Positive exponents only.");
		}
		BigInteger bigInteger = 1;
		bool flag = false;
		BigInteger bigInteger2;
		if ((this.data[999] & 2147483648u) != 0u)
		{
			bigInteger2 = -this % n;
			flag = true;
		}
		else
		{
			bigInteger2 = this % n;
		}
		if ((n.data[999] & 2147483648u) != 0u)
		{
			n = -n;
		}
		BigInteger bigInteger3 = new BigInteger();
		int num = n.dataLength << 1;
		bigInteger3.data[num] = 1u;
		bigInteger3.dataLength = num + 1;
		bigInteger3 /= n;
		int num2 = exp.bitCount();
		int num3 = 0;
		for (int i = 0; i < exp.dataLength; i++)
		{
			uint num4 = 1u;
			int j = 0;
			while (j < 32)
			{
				if ((exp.data[i] & num4) != 0u)
				{
					bigInteger = this.BarrettReduction(bigInteger * bigInteger2, n, bigInteger3);
				}
				num4 <<= 1;
				bigInteger2 = this.BarrettReduction(bigInteger2 * bigInteger2, n, bigInteger3);
				if (bigInteger2.dataLength == 1 && bigInteger2.data[0] == 1u)
				{
					if (flag && (exp.data[0] & 1u) != 0u)
					{
						return -bigInteger;
					}
					return bigInteger;
				}
				else
				{
					num3++;
					if (num3 == num2)
					{
						break;
					}
					j++;
				}
			}
		}
		if (flag && (exp.data[0] & 1u) != 0u)
		{
			return -bigInteger;
		}
		return bigInteger;
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x0003757C File Offset: 0x0003577C
	private BigInteger BarrettReduction(BigInteger x, BigInteger n, BigInteger constant)
	{
		int num = n.dataLength;
		int num2 = num + 1;
		int num3 = num - 1;
		BigInteger bigInteger = new BigInteger();
		int i = num3;
		int num4 = 0;
		while (i < x.dataLength)
		{
			bigInteger.data[num4] = x.data[i];
			i++;
			num4++;
		}
		bigInteger.dataLength = x.dataLength - num3;
		if (bigInteger.dataLength <= 0)
		{
			bigInteger.dataLength = 1;
		}
		BigInteger bigInteger2 = bigInteger * constant;
		BigInteger bigInteger3 = new BigInteger();
		int j = num2;
		int num5 = 0;
		while (j < bigInteger2.dataLength)
		{
			bigInteger3.data[num5] = bigInteger2.data[j];
			j++;
			num5++;
		}
		bigInteger3.dataLength = bigInteger2.dataLength - num2;
		if (bigInteger3.dataLength <= 0)
		{
			bigInteger3.dataLength = 1;
		}
		BigInteger bigInteger4 = new BigInteger();
		int num6 = (x.dataLength > num2) ? num2 : x.dataLength;
		for (int k = 0; k < num6; k++)
		{
			bigInteger4.data[k] = x.data[k];
		}
		bigInteger4.dataLength = num6;
		BigInteger bigInteger5 = new BigInteger();
		for (int l = 0; l < bigInteger3.dataLength; l++)
		{
			if (bigInteger3.data[l] != 0u)
			{
				ulong num7 = 0UL;
				int num8 = l;
				int num9 = 0;
				while (num9 < n.dataLength && num8 < num2)
				{
					ulong num10 = (ulong)bigInteger3.data[l] * (ulong)n.data[num9] + (ulong)bigInteger5.data[num8] + num7;
					bigInteger5.data[num8] = (uint)(num10 & (ulong)-1);
					num7 = num10 >> 32;
					num9++;
					num8++;
				}
				if (num8 < num2)
				{
					bigInteger5.data[num8] = (uint)num7;
				}
			}
		}
		bigInteger5.dataLength = num2;
		while (bigInteger5.dataLength > 1 && bigInteger5.data[bigInteger5.dataLength - 1] == 0u)
		{
			bigInteger5.dataLength--;
		}
		bigInteger4 -= bigInteger5;
		if ((bigInteger4.data[999] & 2147483648u) != 0u)
		{
			BigInteger bigInteger6 = new BigInteger();
			bigInteger6.data[num2] = 1u;
			bigInteger6.dataLength = num2 + 1;
			bigInteger4 += bigInteger6;
		}
		while (bigInteger4 >= n)
		{
			bigInteger4 -= n;
		}
		return bigInteger4;
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x00037800 File Offset: 0x00035A00
	public BigInteger gcd(BigInteger bi)
	{
		BigInteger bigInteger;
		if ((this.data[999] & 2147483648u) != 0u)
		{
			bigInteger = -this;
		}
		else
		{
			bigInteger = this;
		}
		BigInteger bigInteger2;
		if ((bi.data[999] & 2147483648u) != 0u)
		{
			bigInteger2 = -bi;
		}
		else
		{
			bigInteger2 = bi;
		}
		BigInteger bigInteger3 = bigInteger2;
		while (bigInteger.dataLength > 1 || (bigInteger.dataLength == 1 && bigInteger.data[0] != 0u))
		{
			bigInteger3 = bigInteger;
			bigInteger = bigInteger2 % bigInteger;
			bigInteger2 = bigInteger3;
		}
		return bigInteger3;
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x00037890 File Offset: 0x00035A90
	public void genRandomBits(int bits, Random rand)
	{
		int num = bits >> 5;
		int num2 = bits & 31;
		if (num2 != 0)
		{
			num++;
		}
		if (num > 1000 || bits <= 0)
		{
			throw new ArithmeticException("Number of required bits is not valid.");
		}
		byte[] array = new byte[num * 4];
		rand.NextBytes(array);
		for (int i = 0; i < num; i++)
		{
			this.data[i] = BitConverter.ToUInt32(array, i * 4);
		}
		for (int j = num; j < 1000; j++)
		{
			this.data[j] = 0u;
		}
		if (num2 != 0)
		{
			uint num3;
			if (bits != 1)
			{
				num3 = 1u << num2 - 1;
				this.data[num - 1] |= num3;
			}
			num3 = uint.MaxValue >> 32 - num2;
			this.data[num - 1] &= num3;
		}
		else
		{
			this.data[num - 1] |= 2147483648u;
		}
		this.dataLength = num;
		if (this.dataLength == 0)
		{
			this.dataLength = 1;
		}
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x000379A4 File Offset: 0x00035BA4
	public int bitCount()
	{
		while (this.dataLength > 1 && this.data[this.dataLength - 1] == 0u)
		{
			this.dataLength--;
		}
		uint num = this.data[this.dataLength - 1];
		uint num2 = 2147483648u;
		int num3 = 32;
		while (num3 > 0 && (num & num2) == 0u)
		{
			num3--;
			num2 >>= 1;
		}
		num3 += this.dataLength - 1 << 5;
		if (num3 != 0)
		{
			return num3;
		}
		return 1;
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x00037A30 File Offset: 0x00035C30
	public bool FermatLittleTest(int confidence)
	{
		BigInteger bigInteger;
		if ((this.data[999] & 2147483648u) != 0u)
		{
			bigInteger = -this;
		}
		else
		{
			bigInteger = this;
		}
		if (bigInteger.dataLength == 1)
		{
			if (bigInteger.data[0] == 0u || bigInteger.data[0] == 1u)
			{
				return false;
			}
			if (bigInteger.data[0] == 2u || bigInteger.data[0] == 3u)
			{
				return true;
			}
		}
		if ((bigInteger.data[0] & 1u) == 0u)
		{
			return false;
		}
		int num = bigInteger.bitCount();
		BigInteger bigInteger2 = new BigInteger();
		BigInteger exp = bigInteger - new BigInteger(1L);
		Random random = new Random();
		for (int i = 0; i < confidence; i++)
		{
			bool flag = false;
			while (!flag)
			{
				int j;
				for (j = 0; j < 2; j = (int)(random.NextDouble() * (double)num))
				{
				}
				bigInteger2.genRandomBits(j, random);
				int num2 = bigInteger2.dataLength;
				if (num2 > 1 || (num2 == 1 && bigInteger2.data[0] != 1u))
				{
					flag = true;
				}
			}
			BigInteger bigInteger3 = bigInteger2.gcd(bigInteger);
			if (bigInteger3.dataLength == 1 && bigInteger3.data[0] != 1u)
			{
				return false;
			}
			BigInteger bigInteger4 = bigInteger2.modPow(exp, bigInteger);
			int num3 = bigInteger4.dataLength;
			if (num3 > 1 || (num3 == 1 && bigInteger4.data[0] != 1u))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x00037BAC File Offset: 0x00035DAC
	public bool RabinMillerTest(int confidence)
	{
		BigInteger bigInteger;
		if ((this.data[999] & 2147483648u) != 0u)
		{
			bigInteger = -this;
		}
		else
		{
			bigInteger = this;
		}
		if (bigInteger.dataLength == 1)
		{
			if (bigInteger.data[0] == 0u || bigInteger.data[0] == 1u)
			{
				return false;
			}
			if (bigInteger.data[0] == 2u || bigInteger.data[0] == 3u)
			{
				return true;
			}
		}
		if ((bigInteger.data[0] & 1u) == 0u)
		{
			return false;
		}
		BigInteger bigInteger2 = bigInteger - new BigInteger(1L);
		int num = 0;
		for (int i = 0; i < bigInteger2.dataLength; i++)
		{
			uint num2 = 1u;
			for (int j = 0; j < 32; j++)
			{
				if ((bigInteger2.data[i] & num2) != 0u)
				{
					i = bigInteger2.dataLength;
					break;
				}
				num2 <<= 1;
				num++;
			}
		}
		BigInteger exp = bigInteger2 >> num;
		int num3 = bigInteger.bitCount();
		BigInteger bigInteger3 = new BigInteger();
		Random random = new Random();
		for (int k = 0; k < confidence; k++)
		{
			bool flag = false;
			while (!flag)
			{
				int l;
				for (l = 0; l < 2; l = (int)(random.NextDouble() * (double)num3))
				{
				}
				bigInteger3.genRandomBits(l, random);
				int num4 = bigInteger3.dataLength;
				if (num4 > 1 || (num4 == 1 && bigInteger3.data[0] != 1u))
				{
					flag = true;
				}
			}
			BigInteger bigInteger4 = bigInteger3.gcd(bigInteger);
			if (bigInteger4.dataLength == 1 && bigInteger4.data[0] != 1u)
			{
				return false;
			}
			BigInteger bigInteger5 = bigInteger3.modPow(exp, bigInteger);
			bool flag2 = false;
			if (bigInteger5.dataLength == 1 && bigInteger5.data[0] == 1u)
			{
				flag2 = true;
			}
			int num5 = 0;
			while (!flag2 && num5 < num)
			{
				if (bigInteger5 == bigInteger2)
				{
					flag2 = true;
					break;
				}
				bigInteger5 = bigInteger5 * bigInteger5 % bigInteger;
				num5++;
			}
			if (!flag2)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x00037DD0 File Offset: 0x00035FD0
	public bool SolovayStrassenTest(int confidence)
	{
		BigInteger bigInteger;
		if ((this.data[999] & 2147483648u) != 0u)
		{
			bigInteger = -this;
		}
		else
		{
			bigInteger = this;
		}
		if (bigInteger.dataLength == 1)
		{
			if (bigInteger.data[0] == 0u || bigInteger.data[0] == 1u)
			{
				return false;
			}
			if (bigInteger.data[0] == 2u || bigInteger.data[0] == 3u)
			{
				return true;
			}
		}
		if ((bigInteger.data[0] & 1u) == 0u)
		{
			return false;
		}
		int num = bigInteger.bitCount();
		BigInteger bigInteger2 = new BigInteger();
		BigInteger bigInteger3 = bigInteger - 1;
		BigInteger exp = bigInteger3 >> 1;
		Random random = new Random();
		for (int i = 0; i < confidence; i++)
		{
			bool flag = false;
			while (!flag)
			{
				int j;
				for (j = 0; j < 2; j = (int)(random.NextDouble() * (double)num))
				{
				}
				bigInteger2.genRandomBits(j, random);
				int num2 = bigInteger2.dataLength;
				if (num2 > 1 || (num2 == 1 && bigInteger2.data[0] != 1u))
				{
					flag = true;
				}
			}
			BigInteger bigInteger4 = bigInteger2.gcd(bigInteger);
			if (bigInteger4.dataLength == 1 && bigInteger4.data[0] != 1u)
			{
				return false;
			}
			BigInteger bi = bigInteger2.modPow(exp, bigInteger);
			if (bi == bigInteger3)
			{
				bi = -1;
			}
			BigInteger bi2 = BigInteger.Jacobi(bigInteger2, bigInteger);
			if (bi != bi2)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00037F60 File Offset: 0x00036160
	public bool LucasStrongTest()
	{
		BigInteger bigInteger;
		if ((this.data[999] & 2147483648u) != 0u)
		{
			bigInteger = -this;
		}
		else
		{
			bigInteger = this;
		}
		if (bigInteger.dataLength == 1)
		{
			if (bigInteger.data[0] == 0u || bigInteger.data[0] == 1u)
			{
				return false;
			}
			if (bigInteger.data[0] == 2u || bigInteger.data[0] == 3u)
			{
				return true;
			}
		}
		return (bigInteger.data[0] & 1u) != 0u && this.LucasStrongTestHelper(bigInteger);
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00037FF8 File Offset: 0x000361F8
	private bool LucasStrongTestHelper(BigInteger thisVal)
	{
		long num = 5L;
		long num2 = -1L;
		long num3 = 0L;
		bool flag = false;
		while (!flag)
		{
			int num4 = BigInteger.Jacobi(num, thisVal);
			if (num4 == -1)
			{
				flag = true;
			}
			else
			{
				if (num4 == 0 && Math.Abs(num) < thisVal)
				{
					return false;
				}
				if (num3 == 20L)
				{
					BigInteger bigInteger = thisVal.sqrt();
					if (bigInteger * bigInteger == thisVal)
					{
						return false;
					}
				}
				num = (Math.Abs(num) + 2L) * num2;
				num2 = -num2;
			}
			num3 += 1L;
		}
		long num5 = 1L - num >> 2;
		BigInteger bigInteger2 = thisVal + 1;
		int num6 = 0;
		for (int i = 0; i < bigInteger2.dataLength; i++)
		{
			uint num7 = 1u;
			for (int j = 0; j < 32; j++)
			{
				if ((bigInteger2.data[i] & num7) != 0u)
				{
					i = bigInteger2.dataLength;
					break;
				}
				num7 <<= 1;
				num6++;
			}
		}
		BigInteger k = bigInteger2 >> num6;
		BigInteger bigInteger3 = new BigInteger();
		int num8 = thisVal.dataLength << 1;
		bigInteger3.data[num8] = 1u;
		bigInteger3.dataLength = num8 + 1;
		bigInteger3 /= thisVal;
		BigInteger[] array = BigInteger.LucasSequenceHelper(1, num5, k, thisVal, bigInteger3, 0);
		bool flag2 = false;
		if ((array[0].dataLength == 1 && array[0].data[0] == 0u) || (array[1].dataLength == 1 && array[1].data[0] == 0u))
		{
			flag2 = true;
		}
		for (int l = 1; l < num6; l++)
		{
			if (!flag2)
			{
				array[1] = thisVal.BarrettReduction(array[1] * array[1], thisVal, bigInteger3);
				array[1] = (array[1] - (array[2] << 1)) % thisVal;
				if (array[1].dataLength == 1 && array[1].data[0] == 0u)
				{
					flag2 = true;
				}
			}
			array[2] = thisVal.BarrettReduction(array[2] * array[2], thisVal, bigInteger3);
		}
		if (flag2)
		{
			BigInteger bigInteger4 = thisVal.gcd(num5);
			if (bigInteger4.dataLength == 1 && bigInteger4.data[0] == 1u)
			{
				if ((array[2].data[999] & 2147483648u) != 0u)
				{
					BigInteger[] array2 = array;
					array2[2] = array2[2] + thisVal;
				}
				BigInteger bigInteger5 = num5 * (long)BigInteger.Jacobi(num5, thisVal) % thisVal;
				if ((bigInteger5.data[999] & 2147483648u) != 0u)
				{
					bigInteger5 += thisVal;
				}
				if (array[2] != bigInteger5)
				{
					flag2 = false;
				}
			}
		}
		return flag2;
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x00038330 File Offset: 0x00036530
	public bool isProbablePrime(int confidence)
	{
		BigInteger bigInteger;
		if ((this.data[999] & 2147483648u) != 0u)
		{
			bigInteger = -this;
		}
		else
		{
			bigInteger = this;
		}
		for (int i = 0; i < BigInteger.primesBelow2000.Length; i++)
		{
			BigInteger bigInteger2 = BigInteger.primesBelow2000[i];
			if (bigInteger2 >= bigInteger)
			{
				break;
			}
			if ((bigInteger % bigInteger2).IntValue() == 0)
			{
				return false;
			}
		}
		return bigInteger.RabinMillerTest(confidence);
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x000383B8 File Offset: 0x000365B8
	public bool isProbablePrime()
	{
		BigInteger bigInteger;
		if ((this.data[999] & 2147483648u) != 0u)
		{
			bigInteger = -this;
		}
		else
		{
			bigInteger = this;
		}
		if (bigInteger.dataLength == 1)
		{
			if (bigInteger.data[0] == 0u || bigInteger.data[0] == 1u)
			{
				return false;
			}
			if (bigInteger.data[0] == 2u || bigInteger.data[0] == 3u)
			{
				return true;
			}
		}
		if ((bigInteger.data[0] & 1u) == 0u)
		{
			return false;
		}
		for (int i = 0; i < BigInteger.primesBelow2000.Length; i++)
		{
			BigInteger bigInteger2 = BigInteger.primesBelow2000[i];
			if (bigInteger2 >= bigInteger)
			{
				break;
			}
			if ((bigInteger % bigInteger2).IntValue() == 0)
			{
				return false;
			}
		}
		BigInteger bigInteger3 = bigInteger - new BigInteger(1L);
		int num = 0;
		for (int j = 0; j < bigInteger3.dataLength; j++)
		{
			uint num2 = 1u;
			for (int k = 0; k < 32; k++)
			{
				if ((bigInteger3.data[j] & num2) != 0u)
				{
					j = bigInteger3.dataLength;
					break;
				}
				num2 <<= 1;
				num++;
			}
		}
		BigInteger exp = bigInteger3 >> num;
		bigInteger.bitCount();
		BigInteger bigInteger4 = 2.modPow(exp, bigInteger);
		bool flag = false;
		if (bigInteger4.dataLength == 1 && bigInteger4.data[0] == 1u)
		{
			flag = true;
		}
		int num3 = 0;
		while (!flag && num3 < num)
		{
			if (bigInteger4 == bigInteger3)
			{
				flag = true;
				break;
			}
			bigInteger4 = bigInteger4 * bigInteger4 % bigInteger;
			num3++;
		}
		if (flag)
		{
			flag = this.LucasStrongTestHelper(bigInteger);
		}
		return flag;
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x00038588 File Offset: 0x00036788
	public int IntValue()
	{
		return (int)this.data[0];
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x00038594 File Offset: 0x00036794
	public long LongValue()
	{
		long num = 0L;
		num = (long)((ulong)this.data[0]);
		try
		{
			num |= (long)((long)((ulong)this.data[1]) << 32);
		}
		catch (Exception)
		{
			if ((this.data[0] & 2147483648u) != 0u)
			{
				num = (long)this.data[0];
			}
		}
		return num;
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x000385F8 File Offset: 0x000367F8
	public static int Jacobi(BigInteger a, BigInteger b)
	{
		if ((b.data[0] & 1u) == 0u)
		{
			throw new ArgumentException("Jacobi defined only for odd integers.");
		}
		if (a >= b)
		{
			a %= b;
		}
		if (a.dataLength == 1 && a.data[0] == 0u)
		{
			return 0;
		}
		if (a.dataLength == 1 && a.data[0] == 1u)
		{
			return 1;
		}
		if (a < 0)
		{
			if (((b - 1).data[0] & 2u) == 0u)
			{
				return BigInteger.Jacobi(-a, b);
			}
			return -BigInteger.Jacobi(-a, b);
		}
		else
		{
			int num = 0;
			for (int i = 0; i < a.dataLength; i++)
			{
				uint num2 = 1u;
				for (int j = 0; j < 32; j++)
				{
					if ((a.data[i] & num2) != 0u)
					{
						i = a.dataLength;
						break;
					}
					num2 <<= 1;
					num++;
				}
			}
			BigInteger bigInteger = a >> num;
			int num3 = 1;
			if ((num & 1) != 0 && ((b.data[0] & 7u) == 3u || (b.data[0] & 7u) == 5u))
			{
				num3 = -1;
			}
			if ((b.data[0] & 3u) == 3u && (bigInteger.data[0] & 3u) == 3u)
			{
				num3 = -num3;
			}
			if (bigInteger.dataLength == 1 && bigInteger.data[0] == 1u)
			{
				return num3;
			}
			return num3 * BigInteger.Jacobi(b % bigInteger, bigInteger);
		}
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x00038790 File Offset: 0x00036990
	public static BigInteger genPseudoPrime(int bits, int confidence, Random rand)
	{
		BigInteger bigInteger = new BigInteger();
		bool flag = false;
		while (!flag)
		{
			bigInteger.genRandomBits(bits, rand);
			bigInteger.data[0] |= 1u;
			flag = bigInteger.isProbablePrime(confidence);
		}
		return bigInteger;
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x000387D4 File Offset: 0x000369D4
	public BigInteger genCoPrime(int bits, Random rand)
	{
		bool flag = false;
		BigInteger bigInteger = new BigInteger();
		while (!flag)
		{
			bigInteger.genRandomBits(bits, rand);
			BigInteger bigInteger2 = bigInteger.gcd(this);
			if (bigInteger2.dataLength == 1 && bigInteger2.data[0] == 1u)
			{
				flag = true;
			}
		}
		return bigInteger;
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x00038824 File Offset: 0x00036A24
	public BigInteger modInverse(BigInteger modulus)
	{
		BigInteger[] array = new BigInteger[]
		{
			0,
			1
		};
		BigInteger[] array2 = new BigInteger[2];
		BigInteger[] array3 = new BigInteger[]
		{
			0,
			0
		};
		int num = 0;
		BigInteger bi = modulus;
		BigInteger bigInteger = this;
		while (bigInteger.dataLength > 1 || (bigInteger.dataLength == 1 && bigInteger.data[0] != 0u))
		{
			BigInteger bigInteger2 = new BigInteger();
			BigInteger bigInteger3 = new BigInteger();
			if (num > 1)
			{
				BigInteger bigInteger4 = (array[0] - array[1] * array2[0]) % modulus;
				array[0] = array[1];
				array[1] = bigInteger4;
			}
			if (bigInteger.dataLength == 1)
			{
				BigInteger.singleByteDivide(bi, bigInteger, bigInteger2, bigInteger3);
			}
			else
			{
				BigInteger.multiByteDivide(bi, bigInteger, bigInteger2, bigInteger3);
			}
			array2[0] = array2[1];
			array3[0] = array3[1];
			array2[1] = bigInteger2;
			array3[1] = bigInteger3;
			bi = bigInteger;
			bigInteger = bigInteger3;
			num++;
		}
		if (array3[0].dataLength > 1 || (array3[0].dataLength == 1 && array3[0].data[0] != 1u))
		{
			throw new ArithmeticException("No inverse!");
		}
		BigInteger bigInteger5 = (array[0] - array[1] * array2[0]) % modulus;
		if ((bigInteger5.data[999] & 2147483648u) != 0u)
		{
			bigInteger5 += modulus;
		}
		return bigInteger5;
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x000389EC File Offset: 0x00036BEC
	public byte[] getBytes()
	{
		int num = this.bitCount();
		int num2 = num >> 3;
		if ((num & 7) != 0)
		{
			num2++;
		}
		byte[] array = new byte[num2];
		int num3 = 0;
		uint num4 = this.data[this.dataLength - 1];
		uint num5;
		if ((num5 = (num4 >> 24 & 255u)) != 0u)
		{
			array[num3++] = (byte)num5;
		}
		if ((num5 = (num4 >> 16 & 255u)) != 0u)
		{
			array[num3++] = (byte)num5;
		}
		else if (num3 > 0)
		{
			num3++;
		}
		if ((num5 = (num4 >> 8 & 255u)) != 0u)
		{
			array[num3++] = (byte)num5;
		}
		else if (num3 > 0)
		{
			num3++;
		}
		if ((num5 = (num4 & 255u)) != 0u)
		{
			array[num3++] = (byte)num5;
		}
		else if (num3 > 0)
		{
			num3++;
		}
		int i = this.dataLength - 2;
		while (i >= 0)
		{
			num4 = this.data[i];
			array[num3 + 3] = (byte)(num4 & 255u);
			num4 >>= 8;
			array[num3 + 2] = (byte)(num4 & 255u);
			num4 >>= 8;
			array[num3 + 1] = (byte)(num4 & 255u);
			num4 >>= 8;
			array[num3] = (byte)(num4 & 255u);
			i--;
			num3 += 4;
		}
		return array;
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x00038B30 File Offset: 0x00036D30
	public void setBit(uint bitNum)
	{
		uint num = bitNum >> 5;
		byte b = (byte)(bitNum & 31u);
		uint num2 = 1u << (int)b;
		this.data[(int)num] |= num2;
		if ((ulong)num >= (ulong)((long)this.dataLength))
		{
			this.dataLength = (int)(num + 1u);
		}
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x00038B7C File Offset: 0x00036D7C
	public void unsetBit(uint bitNum)
	{
		uint num = bitNum >> 5;
		if ((ulong)num < (ulong)((long)this.dataLength))
		{
			byte b = (byte)(bitNum & 31u);
			uint num2 = 1u << (int)b;
			uint num3 = uint.MaxValue ^ num2;
			this.data[(int)num] &= num3;
			if (this.dataLength > 1 && this.data[this.dataLength - 1] == 0u)
			{
				this.dataLength--;
			}
		}
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x00038BF0 File Offset: 0x00036DF0
	public BigInteger sqrt()
	{
		uint num = (uint)this.bitCount();
		if ((num & 1u) != 0u)
		{
			num = (num >> 1) + 1u;
		}
		else
		{
			num >>= 1;
		}
		uint num2 = num >> 5;
		byte b = (byte)(num & 31u);
		BigInteger bigInteger = new BigInteger();
		uint num3;
		if (b == 0)
		{
			num3 = 2147483648u;
		}
		else
		{
			num3 = 1u << (int)b;
			num2 += 1u;
		}
		bigInteger.dataLength = (int)num2;
		for (int i = (int)(num2 - 1u); i >= 0; i--)
		{
			while (num3 != 0u)
			{
				bigInteger.data[i] ^= num3;
				if (bigInteger * bigInteger > this)
				{
					bigInteger.data[i] ^= num3;
				}
				num3 >>= 1;
			}
			num3 = 2147483648u;
		}
		return bigInteger;
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x00038CB4 File Offset: 0x00036EB4
	public static BigInteger[] LucasSequence(BigInteger P, BigInteger Q, BigInteger k, BigInteger n)
	{
		if (k.dataLength == 1 && k.data[0] == 0u)
		{
			return new BigInteger[]
			{
				0,
				2 % n,
				1 % n
			};
		}
		BigInteger bigInteger = new BigInteger();
		int num = n.dataLength << 1;
		bigInteger.data[num] = 1u;
		bigInteger.dataLength = num + 1;
		bigInteger /= n;
		int num2 = 0;
		for (int i = 0; i < k.dataLength; i++)
		{
			uint num3 = 1u;
			for (int j = 0; j < 32; j++)
			{
				if ((k.data[i] & num3) != 0u)
				{
					i = k.dataLength;
					break;
				}
				num3 <<= 1;
				num2++;
			}
		}
		BigInteger k2 = k >> num2;
		return BigInteger.LucasSequenceHelper(P, Q, k2, n, bigInteger, num2);
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x00038DA0 File Offset: 0x00036FA0
	private static BigInteger[] LucasSequenceHelper(BigInteger P, BigInteger Q, BigInteger k, BigInteger n, BigInteger constant, int s)
	{
		BigInteger[] array = new BigInteger[3];
		if ((k.data[0] & 1u) == 0u)
		{
			throw new ArgumentException("Argument k must be odd.");
		}
		int num = k.bitCount();
		uint num2 = 1u << (num & 31) - 1;
		BigInteger bigInteger = 2 % n;
		BigInteger bigInteger2 = 1 % n;
		BigInteger bigInteger3 = P % n;
		BigInteger bigInteger4 = bigInteger2;
		bool flag = true;
		for (int i = k.dataLength - 1; i >= 0; i--)
		{
			while (num2 != 0u && (i != 0 || num2 != 1u))
			{
				if ((k.data[i] & num2) != 0u)
				{
					bigInteger4 = bigInteger4 * bigInteger3 % n;
					bigInteger = (bigInteger * bigInteger3 - P * bigInteger2) % n;
					bigInteger3 = n.BarrettReduction(bigInteger3 * bigInteger3, n, constant);
					bigInteger3 = (bigInteger3 - (bigInteger2 * Q << 1)) % n;
					if (flag)
					{
						flag = false;
					}
					else
					{
						bigInteger2 = n.BarrettReduction(bigInteger2 * bigInteger2, n, constant);
					}
					bigInteger2 = bigInteger2 * Q % n;
				}
				else
				{
					bigInteger4 = (bigInteger4 * bigInteger - bigInteger2) % n;
					bigInteger3 = (bigInteger * bigInteger3 - P * bigInteger2) % n;
					bigInteger = n.BarrettReduction(bigInteger * bigInteger, n, constant);
					bigInteger = (bigInteger - (bigInteger2 << 1)) % n;
					if (flag)
					{
						bigInteger2 = Q % n;
						flag = false;
					}
					else
					{
						bigInteger2 = n.BarrettReduction(bigInteger2 * bigInteger2, n, constant);
					}
				}
				num2 >>= 1;
			}
			num2 = 2147483648u;
		}
		bigInteger4 = (bigInteger4 * bigInteger - bigInteger2) % n;
		bigInteger = (bigInteger * bigInteger3 - P * bigInteger2) % n;
		if (flag)
		{
			flag = false;
		}
		else
		{
			bigInteger2 = n.BarrettReduction(bigInteger2 * bigInteger2, n, constant);
		}
		bigInteger2 = bigInteger2 * Q % n;
		for (int j = 0; j < s; j++)
		{
			bigInteger4 = bigInteger4 * bigInteger % n;
			bigInteger = (bigInteger * bigInteger - (bigInteger2 << 1)) % n;
			if (flag)
			{
				bigInteger2 = Q % n;
				flag = false;
			}
			else
			{
				bigInteger2 = n.BarrettReduction(bigInteger2 * bigInteger2, n, constant);
			}
		}
		array[0] = bigInteger4;
		array[1] = bigInteger;
		array[2] = bigInteger2;
		return array;
	}

	// Token: 0x040004AC RID: 1196
	private const int maxLength = 1000;

	// Token: 0x040004AD RID: 1197
	public static readonly int[] primesBelow2000 = new int[]
	{
		2,
		3,
		5,
		7,
		11,
		13,
		17,
		19,
		23,
		29,
		31,
		37,
		41,
		43,
		47,
		53,
		59,
		61,
		67,
		71,
		73,
		79,
		83,
		89,
		97,
		101,
		103,
		107,
		109,
		113,
		127,
		131,
		137,
		139,
		149,
		151,
		157,
		163,
		167,
		173,
		179,
		181,
		191,
		193,
		197,
		199,
		211,
		223,
		227,
		229,
		233,
		239,
		241,
		251,
		257,
		263,
		269,
		271,
		277,
		281,
		283,
		293,
		307,
		311,
		313,
		317,
		331,
		337,
		347,
		349,
		353,
		359,
		367,
		373,
		379,
		383,
		389,
		397,
		401,
		409,
		419,
		421,
		431,
		433,
		439,
		443,
		449,
		457,
		461,
		463,
		467,
		479,
		487,
		491,
		499,
		503,
		509,
		521,
		523,
		541,
		547,
		557,
		563,
		569,
		571,
		577,
		587,
		593,
		599,
		601,
		607,
		613,
		617,
		619,
		631,
		641,
		643,
		647,
		653,
		659,
		661,
		673,
		677,
		683,
		691,
		701,
		709,
		719,
		727,
		733,
		739,
		743,
		751,
		757,
		761,
		769,
		773,
		787,
		797,
		809,
		811,
		821,
		823,
		827,
		829,
		839,
		853,
		857,
		859,
		863,
		877,
		881,
		883,
		887,
		907,
		911,
		919,
		929,
		937,
		941,
		947,
		953,
		967,
		971,
		977,
		983,
		991,
		997,
		1009,
		1013,
		1019,
		1021,
		1031,
		1033,
		1039,
		1049,
		1051,
		1061,
		1063,
		1069,
		1087,
		1091,
		1093,
		1097,
		1103,
		1109,
		1117,
		1123,
		1129,
		1151,
		1153,
		1163,
		1171,
		1181,
		1187,
		1193,
		1201,
		1213,
		1217,
		1223,
		1229,
		1231,
		1237,
		1249,
		1259,
		1277,
		1279,
		1283,
		1289,
		1291,
		1297,
		1301,
		1303,
		1307,
		1319,
		1321,
		1327,
		1361,
		1367,
		1373,
		1381,
		1399,
		1409,
		1423,
		1427,
		1429,
		1433,
		1439,
		1447,
		1451,
		1453,
		1459,
		1471,
		1481,
		1483,
		1487,
		1489,
		1493,
		1499,
		1511,
		1523,
		1531,
		1543,
		1549,
		1553,
		1559,
		1567,
		1571,
		1579,
		1583,
		1597,
		1601,
		1607,
		1609,
		1613,
		1619,
		1621,
		1627,
		1637,
		1657,
		1663,
		1667,
		1669,
		1693,
		1697,
		1699,
		1709,
		1721,
		1723,
		1733,
		1741,
		1747,
		1753,
		1759,
		1777,
		1783,
		1787,
		1789,
		1801,
		1811,
		1823,
		1831,
		1847,
		1861,
		1867,
		1871,
		1873,
		1877,
		1879,
		1889,
		1901,
		1907,
		1913,
		1931,
		1933,
		1949,
		1951,
		1973,
		1979,
		1987,
		1993,
		1997,
		1999
	};

	// Token: 0x040004AE RID: 1198
	private uint[] data;

	// Token: 0x040004AF RID: 1199
	public int dataLength;
}
