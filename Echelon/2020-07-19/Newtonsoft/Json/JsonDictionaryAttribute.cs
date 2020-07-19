using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x0200013A RID: 314
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonDictionaryAttribute : JsonContainerAttribute
	{
		// Token: 0x06000A38 RID: 2616 RVA: 0x0003EB88 File Offset: 0x0003CD88
		public JsonDictionaryAttribute()
		{
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0003EB90 File Offset: 0x0003CD90
		[NullableContext(1)]
		public JsonDictionaryAttribute(string id) : base(id)
		{
		}
	}
}
