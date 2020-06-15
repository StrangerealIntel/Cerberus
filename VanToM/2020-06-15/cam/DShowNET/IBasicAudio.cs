using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200004E RID: 78
	[Guid("56a868b3-0ad4-11ce-b03a-0020af0ba770")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[ComImport]
	public interface IBasicAudio
	{
		// Token: 0x0600021A RID: 538
		[PreserveSig]
		int put_Volume(int lVolume);

		// Token: 0x0600021B RID: 539
		[PreserveSig]
		int get_Volume(ref int plVolume);

		// Token: 0x0600021C RID: 540
		[PreserveSig]
		int put_Balance(int lBalance);

		// Token: 0x0600021D RID: 541
		[PreserveSig]
		int get_Balance(ref int plBalance);
	}
}
