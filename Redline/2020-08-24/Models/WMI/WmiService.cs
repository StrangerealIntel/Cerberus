using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Reflection;

namespace RedLine.Models.WMI
{
	// Token: 0x0200002F RID: 47
	public class WmiService : IWmiService
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00004220 File Offset: 0x00002420
		private static TResult Extract<TResult>(ManagementBaseObject managementObject) where TResult : class, new()
		{
			TResult tresult = Activator.CreateInstance<TResult>();
			foreach (PropertyInfo propertyInfo in typeof(TResult).GetProperties())
			{
				WmiResultAttribute wmiResultAttribute = (WmiResultAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(WmiResultAttribute));
				if (wmiResultAttribute != null)
				{
					object value = managementObject.Properties[wmiResultAttribute.PropertyName].Value;
					Type type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
					object value2;
					if (value == null)
					{
						value2 = null;
					}
					else if (type == typeof(DateTime))
					{
						value2 = ManagementDateTimeConverter.ToDateTime(value.ToString()).ToUniversalTime();
					}
					else if (type == typeof(Guid))
					{
						value2 = Guid.Parse(value.ToString());
					}
					else
					{
						value2 = Convert.ChangeType(managementObject.Properties[wmiResultAttribute.PropertyName].Value, type);
					}
					propertyInfo.SetValue(tresult, value2, null);
				}
			}
			return tresult;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000433B File Offset: 0x0000253B
		private ManagementObjectCollection QueryAll(SelectQuery selectQuery, ManagementObjectSearcher searcher = null)
		{
			searcher = (searcher ?? new ManagementObjectSearcher());
			searcher.Query = selectQuery;
			return searcher.Get();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004356 File Offset: 0x00002556
		private ManagementBaseObject QueryFirst(SelectQuery selectQuery, ManagementObjectSearcher searcher = null)
		{
			return this.QueryAll(selectQuery, searcher).Cast<ManagementBaseObject>().FirstOrDefault<ManagementBaseObject>();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000436C File Offset: 0x0000256C
		public TResult QueryFirst<TResult>(WmiQueryBase wmiQuery) where TResult : class, new()
		{
			ManagementBaseObject managementBaseObject = this.QueryFirst(wmiQuery.SelectQuery, null);
			if (managementBaseObject != null)
			{
				return WmiService.Extract<TResult>(managementBaseObject);
			}
			return default(TResult);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000439A File Offset: 0x0000259A
		public ReadOnlyCollection<TResult> QueryAll<TResult>(WmiQueryBase wmiQuery, ManagementObjectSearcher searcher = null) where TResult : class, new()
		{
			ManagementObjectCollection managementObjectCollection = this.QueryAll(wmiQuery.SelectQuery, searcher);
			return new ReadOnlyCollection<TResult>((managementObjectCollection != null) ? managementObjectCollection.Cast<ManagementBaseObject>().Select(new Func<ManagementBaseObject, TResult>(WmiService.Extract<TResult>)).ToList<TResult>() : null);
		}
	}
}
