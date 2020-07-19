using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000199 RID: 409
	[NullableContext(1)]
	public interface IContractResolver
	{
		// Token: 0x06000EFC RID: 3836
		JsonContract ResolveContract(Type type);
	}
}
