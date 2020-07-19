using System;
using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000120 RID: 288
	[CompilerGenerated]
	[Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
	internal sealed class NullableAttribute : Attribute
	{
		// Token: 0x060009A9 RID: 2473 RVA: 0x0003DA7C File Offset: 0x0003BC7C
		public NullableAttribute(byte A_1)
		{
			this.NullableFlags = new byte[]
			{
				A_1
			};
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0003DA94 File Offset: 0x0003BC94
		public NullableAttribute(byte[] A_1)
		{
			this.NullableFlags = A_1;
		}

		// Token: 0x0400057B RID: 1403
		public readonly byte[] NullableFlags;
	}
}
