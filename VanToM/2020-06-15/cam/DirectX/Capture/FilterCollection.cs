using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using cam.DShowNET;
using cam.DShowNET.Device;

namespace cam.DirectX.Capture
{
	// Token: 0x02000097 RID: 151
	public class FilterCollection : CollectionBase
	{
		// Token: 0x06000340 RID: 832 RVA: 0x000139F8 File Offset: 0x00011BF8
		internal FilterCollection(Guid category)
		{
			this.getFilters(category);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00013A08 File Offset: 0x00011C08
		protected void getFilters(Guid category)
		{
			object obj = null;
			UCOMIEnumMoniker ucomienumMoniker = null;
			UCOMIMoniker[] array = new UCOMIMoniker[1];
			try
			{
				Type typeFromCLSID = Type.GetTypeFromCLSID(Clsid.SystemDeviceEnum);
				obj = RuntimeHelpers.GetObjectValue(Activator.CreateInstance(typeFromCLSID));
				ICreateDevEnum createDevEnum = (ICreateDevEnum)obj;
				int num = createDevEnum.CreateClassEnumerator(ref category, out ucomienumMoniker, 0);
				if (num != 0)
				{
				}
				for (;;)
				{
					int num2;
					num = ucomienumMoniker.Next(1, array, out num2);
					if (num != 0)
					{
						break;
					}
					if (array[0] == null)
					{
						break;
					}
					Filter value = new Filter(array[0]);
					this.InnerList.Add(value);
					Marshal.ReleaseComObject(array[0]);
					array[0] = null;
				}
				this.InnerList.Sort();
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
		}

		// Token: 0x1700002E RID: 46
		public Filter this[int index]
		{
			get
			{
				return (Filter)this.InnerList[index];
			}
		}
	}
}
