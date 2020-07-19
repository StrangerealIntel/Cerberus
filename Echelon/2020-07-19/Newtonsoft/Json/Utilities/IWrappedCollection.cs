using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200015F RID: 351
	internal interface IWrappedCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000CD2 RID: 3282
		[Nullable(1)]
		object UnderlyingCollection { [NullableContext(1)] get; }
	}
}
