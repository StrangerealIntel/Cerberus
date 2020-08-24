using System;

namespace RedLine.Models.WMI
{
	// Token: 0x02000025 RID: 37
	public class WmiNetworkAdapter
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003F49 File Offset: 0x00002149
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00003F51 File Offset: 0x00002151
		[WmiResult("PNPDeviceID")]
		public string PnpDeviceId { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003F5A File Offset: 0x0000215A
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00003F62 File Offset: 0x00002162
		[WmiResult("GUID")]
		public Guid? Guid { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003F6B File Offset: 0x0000216B
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00003F73 File Offset: 0x00002173
		[WmiResult("MACAddress")]
		public string MacAddress { get; private set; }

		// Token: 0x0400007F RID: 127
		internal const string PNP_DEVICE_ID = "PNPDeviceID";

		// Token: 0x04000080 RID: 128
		internal const string GUID = "GUID";

		// Token: 0x04000081 RID: 129
		internal const string MAC_ADDRESS = "MACAddress";
	}
}
