using System;

namespace RedLine.Models.WMI
{
	// Token: 0x0200001B RID: 27
	public class WmiBaseBoard
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003D53 File Offset: 0x00001F53
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003D5B File Offset: 0x00001F5B
		[WmiResult("Manufacturer")]
		public string Manufacturer { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003D64 File Offset: 0x00001F64
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003D6C File Offset: 0x00001F6C
		[WmiResult("Product")]
		public string Product { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003D75 File Offset: 0x00001F75
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003D7D File Offset: 0x00001F7D
		[WmiResult("SerialNumber")]
		public string SerialNumber { get; private set; }

		// Token: 0x04000063 RID: 99
		internal const string MANUFACTURER = "Manufacturer";

		// Token: 0x04000064 RID: 100
		internal const string PRODUCT = "Product";

		// Token: 0x04000065 RID: 101
		internal const string SERIAL_NUMBER = "SerialNumber";
	}
}
