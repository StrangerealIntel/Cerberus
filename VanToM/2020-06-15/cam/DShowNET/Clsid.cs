using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000088 RID: 136
	[ComVisible(false)]
	public class Clsid
	{
		// Token: 0x04000255 RID: 597
		public static readonly Guid SystemDeviceEnum = new Guid(1656642832, 24811, 4560, 189, 59, 0, 160, 201, 17, 206, 134);

		// Token: 0x04000256 RID: 598
		public static readonly Guid FilterGraph = new Guid(3828804531u, 21071, 4558, 159, 83, 0, 32, 175, 11, 167, 112);

		// Token: 0x04000257 RID: 599
		public static readonly Guid CaptureGraphBuilder2 = new Guid(3213342433u, 35879, 4560, 179, 240, 0, 170, 0, 55, 97, 197);

		// Token: 0x04000258 RID: 600
		public static readonly Guid SampleGrabber = new Guid(3253993632u, 16136, 4563, 159, 11, 0, 96, 8, 3, 158, 55);

		// Token: 0x04000259 RID: 601
		public static readonly Guid DvdGraphBuilder = new Guid(4240528055u, 62322, 4560, 142, 0, 0, 192, 79, 215, 192, 139);

		// Token: 0x0400025A RID: 602
		public static readonly Guid StreamBufferSink = new Guid("2db47ae5-cf39-43c2-b4d6-0cd8d90946f4");

		// Token: 0x0400025B RID: 603
		public static readonly Guid StreamBufferSource = new Guid("c9f5fe02-f851-4eb5-99ee-ad602af1e619");

		// Token: 0x0400025C RID: 604
		public static readonly Guid VideoMixingRenderer = new Guid(3095128955u, 36137, 16959, 174, 77, 101, 130, 193, 1, 117, 172);

		// Token: 0x0400025D RID: 605
		public static readonly Guid VideoMixingRenderer9 = new Guid(1370794995, 29839, 20027, 162, 118, 200, 40, 51, 14, 146, 106);

		// Token: 0x0400025E RID: 606
		public static readonly Guid VideoRendererDefault = new Guid(1807863802u, 36801, 16993, 172, 34, 207, 180, 204, 56, 219, 80);

		// Token: 0x0400025F RID: 607
		public static readonly Guid AviSplitter = new Guid(458509344u, 64779, 4558, 140, 99, 0, 170, 0, 68, 181, 30);

		// Token: 0x04000260 RID: 608
		public static readonly Guid SmartTee = new Guid(3428377216u, 35489, 4561, 179, 241, 0, 170, 0, 55, 97, 197);
	}
}
