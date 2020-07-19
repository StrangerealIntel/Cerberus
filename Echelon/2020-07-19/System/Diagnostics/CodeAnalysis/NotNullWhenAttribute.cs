using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000123 RID: 291
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x060009AD RID: 2477 RVA: 0x0003DABC File Offset: 0x0003BCBC
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0003DACC File Offset: 0x0003BCCC
		public bool ReturnValue { get; }
	}
}
