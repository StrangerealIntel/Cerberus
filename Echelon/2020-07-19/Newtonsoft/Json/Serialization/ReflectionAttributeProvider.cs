using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001BA RID: 442
	[NullableContext(1)]
	[Nullable(0)]
	public class ReflectionAttributeProvider : IAttributeProvider
	{
		// Token: 0x060010B4 RID: 4276 RVA: 0x0005EDB4 File Offset: 0x0005CFB4
		public ReflectionAttributeProvider(object attributeProvider)
		{
			ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
			this._attributeProvider = attributeProvider;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0005EDD0 File Offset: 0x0005CFD0
		public IList<Attribute> GetAttributes(bool inherit)
		{
			return ReflectionUtils.GetAttributes(this._attributeProvider, null, inherit);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0005EDE0 File Offset: 0x0005CFE0
		public IList<Attribute> GetAttributes(Type attributeType, bool inherit)
		{
			return ReflectionUtils.GetAttributes(this._attributeProvider, attributeType, inherit);
		}

		// Token: 0x0400083B RID: 2107
		private readonly object _attributeProvider;
	}
}
