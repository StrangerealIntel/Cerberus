using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000126 RID: 294
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x060009B1 RID: 2481 RVA: 0x0003DAE4 File Offset: 0x0003BCE4
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0003DAF4 File Offset: 0x0003BCF4
		public bool ParameterValue { get; }
	}
}
