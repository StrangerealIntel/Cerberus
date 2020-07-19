using System;
using System.Text;
using SmartAssembly.StringsEncoding;

namespace ChromV1
{
	// Token: 0x02000059 RID: 89
	public sealed class Arrays
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
		private Arrays()
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
		public static bool AreEqual(bool[] a, bool[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
		public static bool AreEqual(char[] a, char[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000E318 File Offset: 0x0000C518
		public static bool AreEqual(byte[] a, byte[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000E338 File Offset: 0x0000C538
		[Obsolete("Use 'AreEqual' method instead")]
		public static bool AreSame(byte[] a, byte[] b)
		{
			return Arrays.AreEqual(a, b);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000E344 File Offset: 0x0000C544
		public static bool ConstantTimeAreEqual(byte[] a, byte[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			int num2 = 0;
			while (num != 0)
			{
				num--;
				num2 |= (int)(a[num] ^ b[num]);
			}
			return num2 == 0;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000E380 File Offset: 0x0000C580
		public static bool AreEqual(int[] a, int[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000E3A0 File Offset: 0x0000C5A0
		private static bool HaveSameContents(bool[] A_0, bool[] A_1)
		{
			int num = A_0.Length;
			if (num != A_1.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (A_0[num] != A_1[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		private static bool HaveSameContents(char[] A_0, char[] A_1)
		{
			int num = A_0.Length;
			if (num != A_1.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (A_0[num] != A_1[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000E410 File Offset: 0x0000C610
		private static bool HaveSameContents(byte[] A_0, byte[] A_1)
		{
			int num = A_0.Length;
			if (num != A_1.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (A_0[num] != A_1[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000E448 File Offset: 0x0000C648
		private static bool HaveSameContents(int[] A_0, int[] A_1)
		{
			int num = A_0.Length;
			if (num != A_1.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (A_0[num] != A_1[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000E480 File Offset: 0x0000C680
		public static string ToString(object[] a)
		{
			StringBuilder stringBuilder = new StringBuilder(91);
			if (a.Length != 0)
			{
				stringBuilder.Append(a[0]);
				for (int i = 1; i < a.Length; i++)
				{
					stringBuilder.Append(Strings.Get(107396908)).Append(a[i]);
				}
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000E4E4 File Offset: 0x0000C6E4
		public static int GetHashCode(byte[] data)
		{
			if (data == null)
			{
				return 0;
			}
			int num = data.Length;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= (int)data[num];
			}
			return num2;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000E524 File Offset: 0x0000C724
		public static byte[] Clone(byte[] data)
		{
			if (data != null)
			{
				return (byte[])data.Clone();
			}
			return null;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000E53C File Offset: 0x0000C73C
		public static int[] Clone(int[] data)
		{
			if (data != null)
			{
				return (int[])data.Clone();
			}
			return null;
		}
	}
}
