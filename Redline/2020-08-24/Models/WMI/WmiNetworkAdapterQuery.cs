using System;

namespace RedLine.Models.WMI
{
	// Token: 0x02000026 RID: 38
	public class WmiNetworkAdapterQuery : WmiQueryBase
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00003F7C File Offset: 0x0000217C
		public WmiNetworkAdapterQuery(WmiNetworkAdapterType adapterType = WmiNetworkAdapterType.All) : base("Win32_NetworkAdapter", null, WmiNetworkAdapterQuery.COLUMN_NAMES)
		{
			if (adapterType == WmiNetworkAdapterType.Physical)
			{
				base.SelectQuery.Condition = "PhysicalAdapter=1";
				return;
			}
			if (adapterType == WmiNetworkAdapterType.Virtual)
			{
				base.SelectQuery.Condition = "PhysicalAdapter=0";
			}
		}

		// Token: 0x04000085 RID: 133
		private static readonly string[] COLUMN_NAMES = new string[]
		{
			"GUID",
			"MACAddress",
			"PNPDeviceID"
		};
	}
}
