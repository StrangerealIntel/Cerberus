using System;

namespace Ionic.BZip2
{
	// Token: 0x020000BA RID: 186
	internal static class BZip2
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x0002A890 File Offset: 0x00028A90
		internal static T[][] InitRectangularArray<T>(int d1, int d2)
		{
			T[][] array = new T[d1][];
			for (int i = 0; i < d1; i++)
			{
				array[i] = new T[d2];
			}
			return array;
		}

		// Token: 0x0400030B RID: 779
		public static readonly int BlockSizeMultiple = 100000;

		// Token: 0x0400030C RID: 780
		public static readonly int MinBlockSize = 1;

		// Token: 0x0400030D RID: 781
		public static readonly int MaxBlockSize = 9;

		// Token: 0x0400030E RID: 782
		public static readonly int MaxAlphaSize = 258;

		// Token: 0x0400030F RID: 783
		public static readonly int MaxCodeLength = 23;

		// Token: 0x04000310 RID: 784
		public static readonly char RUNA = '\0';

		// Token: 0x04000311 RID: 785
		public static readonly char RUNB = '\u0001';

		// Token: 0x04000312 RID: 786
		public static readonly int NGroups = 6;

		// Token: 0x04000313 RID: 787
		public static readonly int G_SIZE = 50;

		// Token: 0x04000314 RID: 788
		public static readonly int N_ITERS = 4;

		// Token: 0x04000315 RID: 789
		public static readonly int MaxSelectors = 2 + 900000 / BZip2.G_SIZE;

		// Token: 0x04000316 RID: 790
		public static readonly int NUM_OVERSHOOT_BYTES = 20;

		// Token: 0x04000317 RID: 791
		internal static readonly int QSORT_STACK_SIZE = 1000;
	}
}
