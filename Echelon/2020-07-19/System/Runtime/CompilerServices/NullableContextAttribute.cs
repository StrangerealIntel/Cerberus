using System;
using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000121 RID: 289
	[CompilerGenerated]
	[Embedded]
	[AttributeUsage(AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	internal sealed class NullableContextAttribute : Attribute
	{
		// Token: 0x060009AB RID: 2475 RVA: 0x0003DAA4 File Offset: 0x0003BCA4
		public NullableContextAttribute(byte A_1)
		{
			this.Flag = A_1;
		}

		// Token: 0x0400057C RID: 1404
		public readonly byte Flag;
	}
}
