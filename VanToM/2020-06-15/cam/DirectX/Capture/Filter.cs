using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using cam.DShowNET;
using cam.DShowNET.Device;

namespace cam.DirectX.Capture
{
	// Token: 0x02000096 RID: 150
	public class Filter : IComparable
	{
		// Token: 0x06000339 RID: 825 RVA: 0x000137B0 File Offset: 0x000119B0
		public Filter(string monikerString__1)
		{
			this.Name = this.getName(monikerString__1);
			this.MonikerString = monikerString__1;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000137CC File Offset: 0x000119CC
		internal Filter(UCOMIMoniker moniker)
		{
			this.Name = this.getName(moniker);
			this.MonikerString = this.getMonikerString(moniker);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000137F0 File Offset: 0x000119F0
		protected string getMonikerString(UCOMIMoniker moniker)
		{
			string result;
			moniker.GetDisplayName(null, null, out result);
			return result;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00013808 File Offset: 0x00011A08
		protected string getName(UCOMIMoniker moniker)
		{
			object obj = null;
			string result;
			try
			{
				Guid guid = typeof(IPropertyBag).GUID;
				moniker.BindToStorage(null, null, ref guid, out obj);
				IPropertyBag propertyBag = (IPropertyBag)obj;
				object obj2 = "";
				int num = propertyBag.Read("FriendlyName", ref obj2, IntPtr.Zero);
				string text = obj2 as string;
				result = text;
			}
			catch (Exception ex)
			{
				result = "";
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

		// Token: 0x0600033D RID: 829 RVA: 0x000138B8 File Offset: 0x00011AB8
		protected string getName(string monikerString)
		{
			UCOMIMoniker ucomimoniker = null;
			UCOMIMoniker ucomimoniker2 = null;
			string name;
			try
			{
				ucomimoniker = this.getAnyMoniker();
				int num;
				ucomimoniker.ParseDisplayName(null, null, monikerString, out num, out ucomimoniker2);
				name = this.getName(ucomimoniker);
			}
			finally
			{
				if (ucomimoniker != null)
				{
					Marshal.ReleaseComObject(ucomimoniker);
				}
				ucomimoniker = null;
				if (ucomimoniker2 != null)
				{
					Marshal.ReleaseComObject(ucomimoniker2);
				}
				ucomimoniker2 = null;
			}
			return name;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0001391C File Offset: 0x00011B1C
		protected UCOMIMoniker getAnyMoniker()
		{
			Guid videoCompressorCategory = FilterCategory.VideoCompressorCategory;
			object obj = null;
			UCOMIEnumMoniker ucomienumMoniker = null;
			UCOMIMoniker[] array = new UCOMIMoniker[1];
			UCOMIMoniker result;
			try
			{
				Type typeFromCLSID = Type.GetTypeFromCLSID(Clsid.SystemDeviceEnum);
				obj = RuntimeHelpers.GetObjectValue(Activator.CreateInstance(typeFromCLSID));
				ICreateDevEnum createDevEnum = (ICreateDevEnum)obj;
				int num = createDevEnum.CreateClassEnumerator(ref videoCompressorCategory, out ucomienumMoniker, 0);
				int num2;
				num = ucomienumMoniker.Next(1, array, out num2);
				if (num != 0)
				{
					array[0] = null;
				}
				result = array[0];
			}
			finally
			{
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

		// Token: 0x0600033F RID: 831 RVA: 0x000139C8 File Offset: 0x00011BC8
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			Filter filter = (Filter)obj;
			return this.Name.CompareTo(filter.Name);
		}

		// Token: 0x0400028C RID: 652
		public string Name;

		// Token: 0x0400028D RID: 653
		public string MonikerString;
	}
}
