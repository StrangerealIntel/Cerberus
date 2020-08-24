using System;
using System.Management;

namespace RedLine.Models.WMI
{
	// Token: 0x0200002D RID: 45
	public class WmiQueryBase
	{
		// Token: 0x0600010A RID: 266 RVA: 0x000041E8 File Offset: 0x000023E8
		protected WmiQueryBase(string className, string condition = null, string[] selectedProperties = null)
		{
			this._selectQuery = new SelectQuery(className, condition, selectedProperties);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000041FE File Offset: 0x000023FE
		internal SelectQuery SelectQuery
		{
			get
			{
				return this._selectQuery;
			}
		}

		// Token: 0x040000A9 RID: 169
		private readonly SelectQuery _selectQuery;
	}
}
