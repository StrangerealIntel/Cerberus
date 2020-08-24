using System;
using System.Runtime.Serialization;

namespace RedLine.Models
{
	// Token: 0x02000010 RID: 16
	[DataContract(Name = "HardwareType")]
	public enum HardwareType
	{
		// Token: 0x04000033 RID: 51
		[EnumMember]
		Processor,
		// Token: 0x04000034 RID: 52
		[EnumMember]
		Graphic
	}
}
