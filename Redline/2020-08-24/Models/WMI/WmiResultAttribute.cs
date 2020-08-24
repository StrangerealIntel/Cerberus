using System;

namespace RedLine.Models.WMI
{
	// Token: 0x0200002E RID: 46
	[AttributeUsage(AttributeTargets.Property)]
	public class WmiResultAttribute : Attribute
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00004206 File Offset: 0x00002406
		public WmiResultAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00004215 File Offset: 0x00002415
		public string PropertyName { get; }
	}
}
