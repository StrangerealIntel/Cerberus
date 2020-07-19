using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000168 RID: 360
	internal interface IWrappedDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000D2F RID: 3375
		[Nullable(1)]
		object UnderlyingDictionary { [NullableContext(1)] get; }
	}
}
