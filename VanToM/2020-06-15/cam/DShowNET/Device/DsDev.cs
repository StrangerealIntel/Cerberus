using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cam.DShowNET.Device
{
	// Token: 0x02000064 RID: 100
	[ComVisible(false)]
	public class DsDev
	{
		// Token: 0x0600027F RID: 639 RVA: 0x00012714 File Offset: 0x00010914
		public static bool GetDevicesOfCat(Guid cat, ref ArrayList devs)
		{
			devs = null;
			object obj = null;
			UCOMIEnumMoniker ucomienumMoniker = null;
			UCOMIMoniker[] array = new UCOMIMoniker[1];
			checked
			{
				bool result;
				try
				{
					Type typeFromCLSID = Type.GetTypeFromCLSID(Clsid.SystemDeviceEnum);
					obj = RuntimeHelpers.GetObjectValue(Activator.CreateInstance(typeFromCLSID));
					ICreateDevEnum createDevEnum = (ICreateDevEnum)obj;
					int num = createDevEnum.CreateClassEnumerator(ref cat, out ucomienumMoniker, 0);
					int num2 = 0;
					for (;;)
					{
						int num3;
						num = ucomienumMoniker.Next(1, array, out num3);
						if (num != 0)
						{
							break;
						}
						if (array[0] == null)
						{
							break;
						}
						DsDevice dsDevice = new DsDevice();
						dsDevice.Name = DsDev.GetFriendlyName(array[0]);
						if (devs == null)
						{
							devs = new ArrayList();
						}
						dsDevice.Mon = array[0];
						array[0] = null;
						devs.Add(dsDevice);
						num2++;
					}
					result = (num2 > 0);
				}
				catch (Exception ex)
				{
					if (devs != null)
					{
						try
						{
							foreach (object obj2 in devs)
							{
								DsDevice dsDevice2 = (DsDevice)obj2;
								dsDevice2.Dispose();
							}
						}
						finally
						{
							IEnumerator enumerator;
							if (enumerator is IDisposable)
							{
								(enumerator as IDisposable).Dispose();
							}
						}
						devs = null;
					}
					result = false;
				}
				finally
				{
					if (array[0] != null)
					{
						Marshal.ReleaseComObject(array[0]);
					}
					array[0] = null;
					if (ucomienumMoniker != null)
					{
						Marshal.ReleaseComObject(ucomienumMoniker);
					}
					ucomienumMoniker = null;
					if (obj != null)
					{
						Marshal.ReleaseComObject(RuntimeHelpers.GetObjectValue(obj));
					}
					obj = null;
				}
				return result;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000128C8 File Offset: 0x00010AC8
		private static string GetFriendlyName(UCOMIMoniker mon)
		{
			object obj = null;
			string result;
			try
			{
				Guid guid = typeof(IPropertyBag).GUID;
				mon.BindToStorage(null, null, ref guid, out obj);
				IPropertyBag propertyBag = (IPropertyBag)obj;
				object obj2 = "";
				int num = propertyBag.Read("FriendlyName", ref obj2, IntPtr.Zero);
				string text = obj2 as string;
				result = text;
			}
			catch (Exception ex)
			{
				result = null;
			}
			finally
			{
				if (obj != null)
				{
					Marshal.ReleaseComObject(RuntimeHelpers.GetObjectValue(obj));
				}
				obj = null;
			}
			return result;
		}
	}
}
