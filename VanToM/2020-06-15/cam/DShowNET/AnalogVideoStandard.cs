using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000075 RID: 117
	[ComVisible(false)]
	[Flags]
	public enum AnalogVideoStandard
	{
		// Token: 0x040001FE RID: 510
		None = 0,
		// Token: 0x040001FF RID: 511
		NTSC_M = 1,
		// Token: 0x04000200 RID: 512
		NTSC_M_J = 2,
		// Token: 0x04000201 RID: 513
		NTSC_433 = 4,
		// Token: 0x04000202 RID: 514
		PAL_B = 16,
		// Token: 0x04000203 RID: 515
		PAL_D = 32,
		// Token: 0x04000204 RID: 516
		PAL_G = 64,
		// Token: 0x04000205 RID: 517
		PAL_H = 128,
		// Token: 0x04000206 RID: 518
		PAL_I = 256,
		// Token: 0x04000207 RID: 519
		PAL_M = 512,
		// Token: 0x04000208 RID: 520
		PAL_N = 1024,
		// Token: 0x04000209 RID: 521
		PAL_60 = 2048,
		// Token: 0x0400020A RID: 522
		SECAM_B = 4096,
		// Token: 0x0400020B RID: 523
		SECAM_D = 8192,
		// Token: 0x0400020C RID: 524
		SECAM_G = 16384,
		// Token: 0x0400020D RID: 525
		SECAM_H = 32768,
		// Token: 0x0400020E RID: 526
		SECAM_K = 65536,
		// Token: 0x0400020F RID: 527
		SECAM_K1 = 131072,
		// Token: 0x04000210 RID: 528
		SECAM_L = 262144,
		// Token: 0x04000211 RID: 529
		SECAM_L1 = 524288,
		// Token: 0x04000212 RID: 530
		PAL_N_COMBO = 1048576
	}
}
