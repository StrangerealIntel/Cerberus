using System;
using System.Collections.ObjectModel;
using System.Management;

namespace RedLine.Models.WMI
{
	// Token: 0x0200001A RID: 26
	public interface IWmiService
	{
		// Token: 0x060000B4 RID: 180
		TResult QueryFirst<TResult>(WmiQueryBase wmiQuery) where TResult : class, new();

		// Token: 0x060000B5 RID: 181
		ReadOnlyCollection<TResult> QueryAll<TResult>(WmiQueryBase wmiQuery, ManagementObjectSearcher searcher) where TResult : class, new();
	}
}
