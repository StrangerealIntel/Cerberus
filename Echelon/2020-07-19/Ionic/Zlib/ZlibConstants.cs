using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x020000D8 RID: 216
	[ComVisible(true)]
	public static class ZlibConstants
	{
		// Token: 0x0400047B RID: 1147
		public const int WindowBitsMax = 15;

		// Token: 0x0400047C RID: 1148
		public const int WindowBitsDefault = 15;

		// Token: 0x0400047D RID: 1149
		public const int Z_OK = 0;

		// Token: 0x0400047E RID: 1150
		public const int Z_STREAM_END = 1;

		// Token: 0x0400047F RID: 1151
		public const int Z_NEED_DICT = 2;

		// Token: 0x04000480 RID: 1152
		public const int Z_STREAM_ERROR = -2;

		// Token: 0x04000481 RID: 1153
		public const int Z_DATA_ERROR = -3;

		// Token: 0x04000482 RID: 1154
		public const int Z_BUF_ERROR = -5;

		// Token: 0x04000483 RID: 1155
		public const int WorkingBufferSizeDefault = 16384;

		// Token: 0x04000484 RID: 1156
		public const int WorkingBufferSizeMin = 1024;
	}
}
