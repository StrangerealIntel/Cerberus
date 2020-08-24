using System;
using System.Collections.Generic;
using System.Linq;

namespace RedLine.Models.WMI
{
	// Token: 0x0200002B RID: 43
	public class WmiProcessQuery : WmiQueryBase
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00004134 File Offset: 0x00002334
		public WmiProcessQuery() : base("Win32_Process", null, WmiProcessQuery.COLUMN_NAMES)
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004148 File Offset: 0x00002348
		public WmiProcessQuery(IEnumerable<string> processNames) : this()
		{
			base.SelectQuery.Condition = string.Join(" OR ", from processName in processNames
			select "Name LIKE '%" + processName + "%'");
		}

		// Token: 0x040000A6 RID: 166
		private static readonly string[] COLUMN_NAMES = new string[]
		{
			"CommandLine",
			"Name",
			"ExecutablePath",
			"SIDType",
			"ParentProcessId"
		};
	}
}
