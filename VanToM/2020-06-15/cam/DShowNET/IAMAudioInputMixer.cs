using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000079 RID: 121
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("54C39221-8380-11d0-B3F0-00AA003761C5")]
	[ComVisible(true)]
	[ComImport]
	public interface IAMAudioInputMixer
	{
		// Token: 0x060002DB RID: 731
		int put_Enable([In] bool fEnable);

		// Token: 0x060002DC RID: 732
		int get_Enable(out bool pfEnable);

		// Token: 0x060002DD RID: 733
		int put_Mono([In] bool fMono);

		// Token: 0x060002DE RID: 734
		int get_Mono(out bool pfMono);

		// Token: 0x060002DF RID: 735
		int put_MixLevel([In] double Level);

		// Token: 0x060002E0 RID: 736
		int get_MixLevel(out double pLevel);

		// Token: 0x060002E1 RID: 737
		int put_Pan([In] double Pan);

		// Token: 0x060002E2 RID: 738
		int get_Pan(out double pPan);

		// Token: 0x060002E3 RID: 739
		int put_Loudness([In] bool fLoudness);

		// Token: 0x060002E4 RID: 740
		int get_Loudness(out bool pfLoudness);

		// Token: 0x060002E5 RID: 741
		int put_Treble([In] double Treble);

		// Token: 0x060002E6 RID: 742
		int get_Treble(out double pTreble);

		// Token: 0x060002E7 RID: 743
		int get_TrebleRange(out double pRange);

		// Token: 0x060002E8 RID: 744
		int put_Bass([In] double Bass);

		// Token: 0x060002E9 RID: 745
		int get_Bass(out double pBass);

		// Token: 0x060002EA RID: 746
		int get_BassRange(out double pRange);
	}
}
