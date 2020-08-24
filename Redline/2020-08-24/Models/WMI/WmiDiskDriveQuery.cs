using System;

namespace RedLine.Models.WMI
{
	// Token: 0x0200001E RID: 30
	public class WmiDiskDriveQuery : WmiQueryBase
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00003E07 File Offset: 0x00002007
		public WmiDiskDriveQuery() : base("Win32_DiskDrive", null, new string[]
		{
			"DeviceID",
			"MediaType",
			"Model",
			"PNPDeviceID",
			"SerialNumber"
		})
		{
		}
	}
}
