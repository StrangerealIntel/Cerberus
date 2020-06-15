using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cam.DShowNET
{
	// Token: 0x0200007E RID: 126
	[ComVisible(false)]
	public class DsUtils
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x000129B4 File Offset: 0x00010BB4
		public static bool IsCorrectDirectXVersion()
		{
			return File.Exists(Path.Combine(Environment.SystemDirectory, "dpnhpast.dll"));
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000129CC File Offset: 0x00010BCC
		public static bool ShowCapPinDialog(ICaptureGraphBuilder2 bld, IBaseFilter flt, IntPtr hwnd)
		{
			object obj = null;
			DsCAUUID dsCAUUID = default(DsCAUUID);
			bool result;
			try
			{
				Guid capture = PinCategory.Capture;
				Guid guid = MediaType.Interleaved;
				Guid guid2 = typeof(IAMStreamConfig).GUID;
				int num = bld.FindInterface(ref capture, ref guid, flt, ref guid2, out obj);
				if (num != 0)
				{
					guid = MediaType.Video;
					num = bld.FindInterface(ref capture, ref guid, flt, ref guid2, out obj);
					if (num != 0)
					{
						return false;
					}
				}
				ISpecifyPropertyPages specifyPropertyPages = obj as ISpecifyPropertyPages;
				if (specifyPropertyPages == null)
				{
					result = false;
				}
				else
				{
					num = specifyPropertyPages.GetPages(ref dsCAUUID);
					num = DsUtils.OleCreatePropertyFrame(hwnd, 30, 30, null, 1, ref obj, dsCAUUID.cElems, dsCAUUID.pElems, 0, 0, IntPtr.Zero);
					result = true;
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine("!Ds.NET: ShowCapPinDialog " + ex.Message);
				result = false;
			}
			finally
			{
				if (dsCAUUID.pElems != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(dsCAUUID.pElems);
				}
				if (obj != null)
				{
					Marshal.ReleaseComObject(RuntimeHelpers.GetObjectValue(obj));
				}
				obj = null;
			}
			return result;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00012B14 File Offset: 0x00010D14
		public static bool ShowTunerPinDialog(ICaptureGraphBuilder2 bld, IBaseFilter flt, IntPtr hwnd)
		{
			object obj = null;
			DsCAUUID dsCAUUID = default(DsCAUUID);
			bool result;
			try
			{
				Guid capture = PinCategory.Capture;
				Guid guid = MediaType.Interleaved;
				Guid guid2 = typeof(IAMTVTuner).GUID;
				int num = bld.FindInterface(ref capture, ref guid, flt, ref guid2, out obj);
				if (num != 0)
				{
					guid = MediaType.Video;
					num = bld.FindInterface(ref capture, ref guid, flt, ref guid2, out obj);
					if (num != 0)
					{
						return false;
					}
				}
				ISpecifyPropertyPages specifyPropertyPages = obj as ISpecifyPropertyPages;
				if (specifyPropertyPages == null)
				{
					result = false;
				}
				else
				{
					num = specifyPropertyPages.GetPages(ref dsCAUUID);
					num = DsUtils.OleCreatePropertyFrame(hwnd, 30, 30, null, 1, ref obj, dsCAUUID.cElems, dsCAUUID.pElems, 0, 0, IntPtr.Zero);
					result = true;
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine("!Ds.NET: ShowCapPinDialog " + ex.Message);
				result = false;
			}
			finally
			{
				if (dsCAUUID.pElems != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(dsCAUUID.pElems);
				}
				if (obj != null)
				{
					Marshal.ReleaseComObject(RuntimeHelpers.GetObjectValue(obj));
				}
				obj = null;
			}
			return result;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00012C5C File Offset: 0x00010E5C
		public int GetPin(IBaseFilter filter, PinDirection dirrequired, int num, ref IPin ppPin)
		{
			ppPin = null;
			IEnumPins enumPins;
			int num2 = filter.EnumPins(out enumPins);
			if (num2 < 0 || enumPins == null)
			{
				return num2;
			}
			IPin[] array = new IPin[1];
			checked
			{
				do
				{
					int num3;
					num2 = enumPins.Next(1, array, out num3);
					if (num2 != 0)
					{
						break;
					}
					if (array[0] == null)
					{
						break;
					}
					PinDirection pinDirection = (PinDirection)3;
					num2 = array[0].QueryDirection(ref pinDirection);
					if (num2 == 0 && pinDirection == dirrequired)
					{
						if (num == 0)
						{
							goto Block_6;
						}
						num--;
					}
					Marshal.ReleaseComObject(array[0]);
					array[0] = null;
				}
				while (num2 == 0);
				goto IL_8B;
				Block_6:
				ppPin = array[0];
				array[0] = null;
				IL_8B:
				Marshal.ReleaseComObject(enumPins);
				enumPins = null;
				return num2;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00012D00 File Offset: 0x00010F00
		public static void FreeAMMediaType(AMMediaType mediaType)
		{
			if (mediaType.formatSize != 0)
			{
				Marshal.FreeCoTaskMem(mediaType.formatPtr);
			}
			if (mediaType.unkPtr != IntPtr.Zero)
			{
				Marshal.Release(mediaType.unkPtr);
			}
			mediaType.formatSize = 0;
			mediaType.formatPtr = IntPtr.Zero;
			mediaType.unkPtr = IntPtr.Zero;
		}

		// Token: 0x060002F7 RID: 759
		[DllImport("olepro32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		private static extern int OleCreatePropertyFrame(IntPtr hwndOwner, int x, int y, string lpszCaption, int cObjects, [MarshalAs(UnmanagedType.Interface)] [In] ref object ppUnk, int cPages, IntPtr pPageClsID, int lcid, int dwReserved, IntPtr pvReserved);
	}
}
