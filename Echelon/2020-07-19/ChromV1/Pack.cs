using System;

namespace ChromV1
{
	// Token: 0x02000064 RID: 100
	internal sealed class Pack
	{
		// Token: 0x06000231 RID: 561 RVA: 0x00011950 File Offset: 0x0000FB50
		private Pack()
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00011958 File Offset: 0x0000FB58
		internal static void UInt32_To_BE(uint A_0, byte[] A_1, int A_2)
		{
			A_1[A_2] = (byte)(A_0 >> 24);
			A_1[++A_2] = (byte)(A_0 >> 16);
			A_1[++A_2] = (byte)(A_0 >> 8);
			A_1[++A_2] = (byte)A_0;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00011988 File Offset: 0x0000FB88
		internal static uint BE_To_UInt32(byte[] A_0, int A_1)
		{
			return (uint)((int)A_0[A_1] << 24 | (int)A_0[++A_1] << 16 | (int)A_0[++A_1] << 8 | (int)A_0[++A_1]);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000119B0 File Offset: 0x0000FBB0
		internal static void UInt32_To_LE(uint A_0, byte[] A_1, int A_2)
		{
			A_1[A_2] = (byte)A_0;
			A_1[++A_2] = (byte)(A_0 >> 8);
			A_1[++A_2] = (byte)(A_0 >> 16);
			A_1[++A_2] = (byte)(A_0 >> 24);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000119E0 File Offset: 0x0000FBE0
		internal static uint LE_To_UInt32(byte[] A_0, int A_1)
		{
			return (uint)((int)A_0[A_1] | (int)A_0[++A_1] << 8 | (int)A_0[++A_1] << 16 | (int)A_0[++A_1] << 24);
		}
	}
}
