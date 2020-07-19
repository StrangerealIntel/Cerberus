using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000198 RID: 408
	[NullableContext(1)]
	public interface IAttributeProvider
	{
		// Token: 0x06000EFA RID: 3834
		IList<Attribute> GetAttributes(bool inherit);

		// Token: 0x06000EFB RID: 3835
		IList<Attribute> GetAttributes(Type attributeType, bool inherit);
	}
}
