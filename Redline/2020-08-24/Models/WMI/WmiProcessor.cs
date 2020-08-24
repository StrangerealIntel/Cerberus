using System;

namespace RedLine.Models.WMI
{
	// Token: 0x02000029 RID: 41
	public class WmiProcessor
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004032 File Offset: 0x00002232
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000403A File Offset: 0x0000223A
		[WmiResult("Manufacturer")]
		public string Manufacturer { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004043 File Offset: 0x00002243
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x0000404B File Offset: 0x0000224B
		[WmiResult("Caption")]
		public string Caption { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004054 File Offset: 0x00002254
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x0000405C File Offset: 0x0000225C
		[WmiResult("Name")]
		public string Name { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004065 File Offset: 0x00002265
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000406D File Offset: 0x0000226D
		[WmiResult("ProcessorId")]
		public string ProcessorId { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004076 File Offset: 0x00002276
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x0000407E File Offset: 0x0000227E
		[WmiResult("NumberOfCores")]
		public int? NumberOfCores { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004087 File Offset: 0x00002287
		// (set) Token: 0x060000FB RID: 251 RVA: 0x0000408F File Offset: 0x0000228F
		[WmiResult("NumberOfLogicalProcessors")]
		public int? NumberOfLogicalProcessors { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004098 File Offset: 0x00002298
		// (set) Token: 0x060000FD RID: 253 RVA: 0x000040A0 File Offset: 0x000022A0
		[WmiResult("L2CacheSize")]
		public int? L2CacheSize { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000040A9 File Offset: 0x000022A9
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000040B1 File Offset: 0x000022B1
		[WmiResult("L3CacheSize")]
		public int? L3CacheSize { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000040BA File Offset: 0x000022BA
		// (set) Token: 0x06000101 RID: 257 RVA: 0x000040C2 File Offset: 0x000022C2
		[WmiResult("SocketDesignation")]
		public string SocketDesignation { get; private set; }

		// Token: 0x04000094 RID: 148
		internal const string MANUFACTURER = "Manufacturer";

		// Token: 0x04000095 RID: 149
		internal const string CAPTION = "Caption";

		// Token: 0x04000096 RID: 150
		internal const string NAME = "Name";

		// Token: 0x04000097 RID: 151
		internal const string PROCESSOR_ID = "ProcessorId";

		// Token: 0x04000098 RID: 152
		internal const string NUM_OF_CORES = "NumberOfCores";

		// Token: 0x04000099 RID: 153
		internal const string NUM_OF_LOGICAL_PROCESSORS = "NumberOfLogicalProcessors";

		// Token: 0x0400009A RID: 154
		internal const string L2_CACHE_SIZE = "L2CacheSize";

		// Token: 0x0400009B RID: 155
		internal const string L3_CACHE_SIZE = "L3CacheSize";

		// Token: 0x0400009C RID: 156
		internal const string SOCKET_DESIGNATION = "SocketDesignation";
	}
}
