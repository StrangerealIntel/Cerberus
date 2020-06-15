using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x02000078 RID: 120
	[Guid("C6E13380-30AC-11d0-A18C-00A0C9118956")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	[ComImport]
	public interface IAMCrossbar
	{
		// Token: 0x060002D6 RID: 726
		[PreserveSig]
		int get_PinCounts(out int OutputPinCount, out int InputPinCount);

		// Token: 0x060002D7 RID: 727
		[PreserveSig]
		int CanRoute([In] int OutputPinIndex, [In] int InputPinIndex);

		// Token: 0x060002D8 RID: 728
		[PreserveSig]
		int Route([In] int OutputPinIndex, [In] int InputPinIndex);

		// Token: 0x060002D9 RID: 729
		[PreserveSig]
		int get_IsRoutedTo([In] int OutputPinIndex, out int InputPinIndex);

		// Token: 0x060002DA RID: 730
		[PreserveSig]
		int get_CrossbarPinInfo([MarshalAs(UnmanagedType.Bool)] [In] bool IsInputPin, [In] int PinIndex, out int PinIndexRelated, out PhysicalConnectorType PhysicalType);
	}
}
