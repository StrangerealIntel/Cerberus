using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000162 RID: 354
	[NullableContext(1)]
	[Nullable(0)]
	internal class TypeInformation
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0004A368 File Offset: 0x00048568
		public Type Type { get; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0004A370 File Offset: 0x00048570
		public PrimitiveTypeCode TypeCode { get; }

		// Token: 0x06000CEF RID: 3311 RVA: 0x0004A378 File Offset: 0x00048578
		public TypeInformation(Type type, PrimitiveTypeCode typeCode)
		{
			this.Type = type;
			this.TypeCode = typeCode;
		}
	}
}
