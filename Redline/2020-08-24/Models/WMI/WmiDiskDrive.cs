using System;

namespace RedLine.Models.WMI
{
	// Token: 0x0200001D RID: 29
	public class WmiDiskDrive
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003DB2 File Offset: 0x00001FB2
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003DBA File Offset: 0x00001FBA
		[WmiResult("DeviceID")]
		public string DeviceId { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003DC3 File Offset: 0x00001FC3
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003DCB File Offset: 0x00001FCB
		[WmiResult("MediaType")]
		public string MediaType { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003DD4 File Offset: 0x00001FD4
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003DDC File Offset: 0x00001FDC
		[WmiResult("Model")]
		public string Model { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003DE5 File Offset: 0x00001FE5
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003DED File Offset: 0x00001FED
		[WmiResult("PNPDeviceID")]
		public string PnpDeviceId { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003DF6 File Offset: 0x00001FF6
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003DFE File Offset: 0x00001FFE
		[WmiResult("SerialNumber")]
		public string SerialNumber { get; private set; }

		// Token: 0x04000069 RID: 105
		internal const string DEVICE_ID = "DeviceID";

		// Token: 0x0400006A RID: 106
		internal const string MEDIA_TYPE = "MediaType";

		// Token: 0x0400006B RID: 107
		internal const string MODEL = "Model";

		// Token: 0x0400006C RID: 108
		internal const string PNP_DEVICE_ID = "PNPDeviceID";

		// Token: 0x0400006D RID: 109
		internal const string SERIAL_NUMBER = "SerialNumber";
	}
}
