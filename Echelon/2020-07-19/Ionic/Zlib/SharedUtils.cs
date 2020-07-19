using System;
using System.IO;
using System.Text;

namespace Ionic.Zlib
{
	// Token: 0x020000D1 RID: 209
	internal class SharedUtils
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x000333F4 File Offset: 0x000315F4
		public static int URShift(int number, int bits)
		{
			return (int)((uint)number >> bits);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000333FC File Offset: 0x000315FC
		public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
		{
			if (target.Length == 0)
			{
				return 0;
			}
			char[] array = new char[target.Length];
			int num = sourceTextReader.Read(array, start, count);
			if (num == 0)
			{
				return -1;
			}
			for (int i = start; i < start + num; i++)
			{
				target[i] = (byte)array[i];
			}
			return num;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0003344C File Offset: 0x0003164C
		internal static byte[] ToByteArray(string sourceString)
		{
			return Encoding.UTF8.GetBytes(sourceString);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0003345C File Offset: 0x0003165C
		internal static char[] ToCharArray(byte[] byteArray)
		{
			return Encoding.UTF8.GetChars(byteArray);
		}
	}
}
