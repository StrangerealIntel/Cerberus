using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200008B RID: 139
	[ComVisible(false)]
	public class FormatType
	{
		// Token: 0x04000272 RID: 626
		public static readonly Guid None = new Guid(258217942u, 49944, 4560, 164, 63, 0, 160, 201, 34, 49, 150);

		// Token: 0x04000273 RID: 627
		public static readonly Guid VideoInfo = new Guid(89694080u, 50006, 4558, 191, 1, 0, 170, 0, 85, 89, 90);

		// Token: 0x04000274 RID: 628
		public static readonly Guid VideoInfo2 = new Guid(4146755232u, 60170, 4560, 172, 228, 0, 0, 192, 204, 22, 186);

		// Token: 0x04000275 RID: 629
		public static readonly Guid WaveEx = new Guid(89694081u, 50006, 4558, 191, 1, 0, 170, 0, 85, 89, 90);

		// Token: 0x04000276 RID: 630
		public static readonly Guid MpegVideo = new Guid(89694082u, 50006, 4558, 191, 1, 0, 170, 0, 85, 89, 90);

		// Token: 0x04000277 RID: 631
		public static readonly Guid MpegStreams = new Guid(89694083u, 50006, 4558, 191, 1, 0, 170, 0, 85, 89, 90);

		// Token: 0x04000278 RID: 632
		public static readonly Guid DvInfo = new Guid(89694084u, 50006, 4558, 191, 1, 0, 170, 0, 85, 89, 90);
	}
}
