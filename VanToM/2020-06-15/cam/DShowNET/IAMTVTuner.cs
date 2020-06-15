using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000077 RID: 119
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("211A8766-03AC-11d1-8D13-00AA00BD8339")]
	[ComVisible(true)]
	[ComImport]
	public interface IAMTVTuner
	{
		// Token: 0x060002BC RID: 700
		[PreserveSig]
		int put_Channel(int lChannel, AMTunerSubChannel lVideoSubChannel, AMTunerSubChannel lAudioSubChannel);

		// Token: 0x060002BD RID: 701
		[PreserveSig]
		int get_Channel(ref int plChannel, ref int plVideoSubChannel, ref int plAudioSubChannel);

		// Token: 0x060002BE RID: 702
		[PreserveSig]
		int ChannelMinMax(ref int lChannelMin, ref int lChannelMax);

		// Token: 0x060002BF RID: 703
		[PreserveSig]
		int put_CountryCode(int lCountryCode);

		// Token: 0x060002C0 RID: 704
		[PreserveSig]
		int get_CountryCode(ref int plCountryCode);

		// Token: 0x060002C1 RID: 705
		[PreserveSig]
		int put_TuningSpace(int lTuningSpace);

		// Token: 0x060002C2 RID: 706
		[PreserveSig]
		int get_TuningSpace(ref int plTuningSpace);

		// Token: 0x060002C3 RID: 707
		[PreserveSig]
		int Logon(IntPtr hCurrentUser);

		// Token: 0x060002C4 RID: 708
		[PreserveSig]
		int Logout();

		// Token: 0x060002C5 RID: 709
		[PreserveSig]
		int SignalPresent(ref AMTunerSignalStrength plSignalStrength);

		// Token: 0x060002C6 RID: 710
		[PreserveSig]
		int put_Mode(AMTunerModeType lMode);

		// Token: 0x060002C7 RID: 711
		[PreserveSig]
		int get_Mode(ref AMTunerModeType plMode);

		// Token: 0x060002C8 RID: 712
		[PreserveSig]
		int GetAvailableModes(ref AMTunerModeType plModes);

		// Token: 0x060002C9 RID: 713
		[PreserveSig]
		int RegisterNotificationCallBack(IAMTunerNotification pNotify, AMTunerEventType lEvents);

		// Token: 0x060002CA RID: 714
		[PreserveSig]
		int UnRegisterNotificationCallBack(IAMTunerNotification pNotify);

		// Token: 0x060002CB RID: 715
		[PreserveSig]
		int get_AvailableTVFormats(ref AnalogVideoStandard lAnalogVideoStandard);

		// Token: 0x060002CC RID: 716
		[PreserveSig]
		int get_TVFormat(ref AnalogVideoStandard lAnalogVideoStandard);

		// Token: 0x060002CD RID: 717
		[PreserveSig]
		int AutoTune(int lChannel, ref int plFoundSignal);

		// Token: 0x060002CE RID: 718
		[PreserveSig]
		int StoreAutoTune();

		// Token: 0x060002CF RID: 719
		[PreserveSig]
		int get_NumInputConnections(ref int plNumInputConnections);

		// Token: 0x060002D0 RID: 720
		[PreserveSig]
		int put_InputType(int lIndex, TunerInputType inputType);

		// Token: 0x060002D1 RID: 721
		[PreserveSig]
		int get_InputType(int lIndex, ref TunerInputType inputType);

		// Token: 0x060002D2 RID: 722
		[PreserveSig]
		int put_ConnectInput(int lIndex);

		// Token: 0x060002D3 RID: 723
		[PreserveSig]
		int get_ConnectInput(ref int lIndex);

		// Token: 0x060002D4 RID: 724
		[PreserveSig]
		int get_VideoFrequency(ref int lFreq);

		// Token: 0x060002D5 RID: 725
		[PreserveSig]
		int get_AudioFrequency(ref int lFreq);
	}
}
