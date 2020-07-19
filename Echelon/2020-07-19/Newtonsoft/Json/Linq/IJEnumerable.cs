using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001D3 RID: 467
	[NullableContext(1)]
	public interface IJEnumerable<[Nullable(0)] out T> : IEnumerable<T>, IEnumerable where T : JToken
	{
		// Token: 0x170003E3 RID: 995
		IJEnumerable<JToken> this[object key]
		{
			get;
		}
	}
}
