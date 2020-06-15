using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000052 RID: 82
	[ComVisible(false)]
	public enum PhysicalConnectorType
	{
		// Token: 0x040001B0 RID: 432
		Video_Tuner = 1,
		// Token: 0x040001B1 RID: 433
		Video_Composite,
		// Token: 0x040001B2 RID: 434
		Video_SVideo,
		// Token: 0x040001B3 RID: 435
		Video_RGB,
		// Token: 0x040001B4 RID: 436
		Video_YRYBY,
		// Token: 0x040001B5 RID: 437
		Video_SerialDigital,
		// Token: 0x040001B6 RID: 438
		Video_ParallelDigital,
		// Token: 0x040001B7 RID: 439
		Video_SCSI,
		// Token: 0x040001B8 RID: 440
		Video_AUX,
		// Token: 0x040001B9 RID: 441
		Video_1394,
		// Token: 0x040001BA RID: 442
		Video_USB,
		// Token: 0x040001BB RID: 443
		Video_VideoDecoder,
		// Token: 0x040001BC RID: 444
		Video_VideoEncoder,
		// Token: 0x040001BD RID: 445
		Video_SCART,
		// Token: 0x040001BE RID: 446
		Audio_Tuner = 4096,
		// Token: 0x040001BF RID: 447
		Audio_Line,
		// Token: 0x040001C0 RID: 448
		Audio_Mic,
		// Token: 0x040001C1 RID: 449
		Audio_AESDigital,
		// Token: 0x040001C2 RID: 450
		Audio_SPDIFDigital,
		// Token: 0x040001C3 RID: 451
		Audio_SCSI,
		// Token: 0x040001C4 RID: 452
		Audio_AUX,
		// Token: 0x040001C5 RID: 453
		Audio_1394,
		// Token: 0x040001C6 RID: 454
		Audio_USB,
		// Token: 0x040001C7 RID: 455
		Audio_AudioDecoder
	}
}
