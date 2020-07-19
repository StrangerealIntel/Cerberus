using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000138 RID: 312
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class JsonConverterAttribute : Attribute
	{
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0003EB38 File Offset: 0x0003CD38
		public Type ConverterType
		{
			get
			{
				return this._converterType;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0003EB40 File Offset: 0x0003CD40
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public object[] ConverterParameters { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; }

		// Token: 0x06000A35 RID: 2613 RVA: 0x0003EB48 File Offset: 0x0003CD48
		public JsonConverterAttribute(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType");
			}
			this._converterType = converterType;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0003EB70 File Offset: 0x0003CD70
		public JsonConverterAttribute(Type converterType, params object[] converterParameters) : this(converterType)
		{
			this.ConverterParameters = converterParameters;
		}

		// Token: 0x040005B6 RID: 1462
		private readonly Type _converterType;
	}
}
