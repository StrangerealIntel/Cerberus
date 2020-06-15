using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000073 RID: 115
	[Guid("211A8761-03AC-11d1-8D13-00AA00BD8339")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IAMTuner
	{
		// Token: 0x060002AC RID: 684
		[PreserveSig]
		int put_Channel(int lChannel, AMTunerSubChannel lVideoSubChannel, AMTunerSubChannel lAudioSubChannel);

		// Token: 0x060002AD RID: 685
		[PreserveSig]
		int get_Channel(ref int plChannel, ref int plVideoSubChannel, ref int plAudioSubChannel);

		// Token: 0x060002AE RID: 686
		[PreserveSig]
		int ChannelMinMax(ref int lChannelMin, ref int lChannelMax);

		// Token: 0x060002AF RID: 687
		[PreserveSig]
		int put_CountryCode(int lCountryCode);

		// Token: 0x060002B0 RID: 688
		[PreserveSig]
		int get_CountryCode(ref int plCountryCode);

		// Token: 0x060002B1 RID: 689
		[PreserveSig]
		int put_TuningSpace(int lTuningSpace);

		// Token: 0x060002B2 RID: 690
		[PreserveSig]
		int get_TuningSpace(ref int plTuningSpace);

		// Token: 0x060002B3 RID: 691
		[PreserveSig]
		int Logon(IntPtr hCurrentUser);

		// Token: 0x060002B4 RID: 692
		[PreserveSig]
		int Logout();

		// Token: 0x060002B5 RID: 693
		[PreserveSig]
		int SignalPresent(ref AMTunerSignalStrength plSignalStrength);

		// Token: 0x060002B6 RID: 694
		[PreserveSig]
		int put_Mode(AMTunerModeType lMode);

		// Token: 0x060002B7 RID: 695
		[PreserveSig]
		int get_Mode(ref AMTunerModeType plMode);

		// Token: 0x060002B8 RID: 696
		[PreserveSig]
		int GetAvailableModes(ref AMTunerModeType plModes);

		// Token: 0x060002B9 RID: 697
		[PreserveSig]
		int RegisterNotificationCallBack(IAMTunerNotification pNotify, AMTunerEventType lEvents);

		// Token: 0x060002BA RID: 698
		[PreserveSig]
		int UnRegisterNotificationCallBack(IAMTunerNotification pNotify);
	}
}
