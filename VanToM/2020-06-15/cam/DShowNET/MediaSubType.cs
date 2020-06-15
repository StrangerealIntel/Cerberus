using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200008A RID: 138
	[ComVisible(false)]
	public class MediaSubType
	{
		// Token: 0x04000266 RID: 614
		public static readonly Guid YUYV = new Guid(1448695129, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

		// Token: 0x04000267 RID: 615
		public static readonly Guid IYUV = new Guid(1448433993, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

		// Token: 0x04000268 RID: 616
		public static readonly Guid DVSD = new Guid(1146312260, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

		// Token: 0x04000269 RID: 617
		public static readonly Guid RGB1 = new Guid(3828804472u, 21071, 4558, 159, 83, 0, 32, 175, 11, 167, 112);

		// Token: 0x0400026A RID: 618
		public static readonly Guid RGB4 = new Guid(3828804473u, 21071, 4558, 159, 83, 0, 32, 175, 11, 167, 112);

		// Token: 0x0400026B RID: 619
		public static readonly Guid RGB8 = new Guid(3828804474u, 21071, 4558, 159, 83, 0, 32, 175, 11, 167, 112);

		// Token: 0x0400026C RID: 620
		public static readonly Guid RGB565 = new Guid(3828804475u, 21071, 4558, 159, 83, 0, 32, 175, 11, 167, 112);

		// Token: 0x0400026D RID: 621
		public static readonly Guid RGB555 = new Guid(3828804476u, 21071, 4558, 159, 83, 0, 32, 175, 11, 167, 112);

		// Token: 0x0400026E RID: 622
		public static readonly Guid RGB24 = new Guid(3828804477u, 21071, 4558, 159, 83, 0, 32, 175, 11, 167, 112);

		// Token: 0x0400026F RID: 623
		public static readonly Guid RGB32 = new Guid(3828804478u, 21071, 4558, 159, 83, 0, 32, 175, 11, 167, 112);

		// Token: 0x04000270 RID: 624
		public static readonly Guid Avi = new Guid(3828804488u, 21071, 4558, 159, 83, 0, 32, 175, 11, 167, 112);

		// Token: 0x04000271 RID: 625
		public static readonly Guid Asf = new Guid(1035472784u, 37906, 4561, 173, 237, 0, 0, 248, 117, 75, 153);
	}
}
