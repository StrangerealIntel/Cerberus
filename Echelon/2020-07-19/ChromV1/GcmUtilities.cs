using System;

namespace ChromV1
{
	// Token: 0x02000060 RID: 96
	internal abstract class GcmUtilities
	{
		// Token: 0x06000221 RID: 545 RVA: 0x000116D8 File Offset: 0x0000F8D8
		internal static uint[] AsUints(byte[] A_0)
		{
			return new uint[]
			{
				Pack.BE_To_UInt32(A_0, 0),
				Pack.BE_To_UInt32(A_0, 4),
				Pack.BE_To_UInt32(A_0, 8),
				Pack.BE_To_UInt32(A_0, 12)
			};
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0001170C File Offset: 0x0000F90C
		internal static void MultiplyP(uint[] A_0)
		{
			bool flag = (A_0[3] & 1u) > 0u;
			GcmUtilities.ShiftRight(A_0);
			if (flag)
			{
				A_0[0] ^= 3774873600u;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00011734 File Offset: 0x0000F934
		internal static void MultiplyP8(uint[] A_0)
		{
			uint num = A_0[3];
			GcmUtilities.ShiftRightN(A_0, 8);
			for (int i = 7; i >= 0; i--)
			{
				if (((ulong)num & (ulong)(1L << (i & 31))) != 0UL)
				{
					A_0[0] ^= 3774873600u >> 7 - i;
				}
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00011788 File Offset: 0x0000F988
		internal static void ShiftRight(uint[] A_0)
		{
			int num = 0;
			uint num2 = 0u;
			for (;;)
			{
				uint num3 = A_0[num];
				A_0[num] = (num3 >> 1 | num2);
				if (++num == 4)
				{
					break;
				}
				num2 = num3 << 31;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000117BC File Offset: 0x0000F9BC
		internal static void ShiftRightN(uint[] A_0, int A_1)
		{
			int num = 0;
			uint num2 = 0u;
			for (;;)
			{
				uint num3 = A_0[num];
				A_0[num] = (num3 >> A_1 | num2);
				if (++num == 4)
				{
					break;
				}
				num2 = num3 << 32 - A_1;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000117F8 File Offset: 0x0000F9F8
		internal static void Xor(byte[] A_0, byte[] A_1)
		{
			for (int i = 15; i >= 0; i--)
			{
				int num = i;
				A_0[num] ^= A_1[i];
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00011828 File Offset: 0x0000FA28
		internal static void Xor(uint[] A_0, uint[] A_1)
		{
			for (int i = 3; i >= 0; i--)
			{
				A_0[i] ^= A_1[i];
			}
		}
	}
}
