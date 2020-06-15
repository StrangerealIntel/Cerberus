using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200004D RID: 77
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("56a868b2-0ad4-11ce-b03a-0020af0ba770")]
	[ComVisible(true)]
	[ComImport]
	public interface IMediaPosition
	{
		// Token: 0x0600020F RID: 527
		[PreserveSig]
		int get_Duration(ref double pLength);

		// Token: 0x06000210 RID: 528
		[PreserveSig]
		int put_CurrentPosition(double llTime);

		// Token: 0x06000211 RID: 529
		[PreserveSig]
		int get_CurrentPosition(ref double pllTime);

		// Token: 0x06000212 RID: 530
		[PreserveSig]
		int get_StopTime(ref double pllTime);

		// Token: 0x06000213 RID: 531
		[PreserveSig]
		int put_StopTime(double llTime);

		// Token: 0x06000214 RID: 532
		[PreserveSig]
		int get_PrerollTime(ref double pllTime);

		// Token: 0x06000215 RID: 533
		[PreserveSig]
		int put_PrerollTime(double llTime);

		// Token: 0x06000216 RID: 534
		[PreserveSig]
		int put_Rate(double dRate);

		// Token: 0x06000217 RID: 535
		[PreserveSig]
		int get_Rate(ref double pdRate);

		// Token: 0x06000218 RID: 536
		[PreserveSig]
		int CanSeekForward(ref int pCanSeekForward);

		// Token: 0x06000219 RID: 537
		[PreserveSig]
		int CanSeekBackward(ref int pCanSeekBackward);
	}
}
