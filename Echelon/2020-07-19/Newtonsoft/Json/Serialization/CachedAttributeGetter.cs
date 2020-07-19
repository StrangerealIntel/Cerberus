using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200018C RID: 396
	internal static class CachedAttributeGetter<T> where T : Attribute
	{
		// Token: 0x06000E89 RID: 3721 RVA: 0x000535E0 File Offset: 0x000517E0
		[NullableContext(1)]
		[return: Nullable(2)]
		public static T GetAttribute(object type)
		{
			return CachedAttributeGetter<T>.TypeAttributeCache.Get(type);
		}

		// Token: 0x04000782 RID: 1922
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		private static readonly ThreadSafeStore<object, T> TypeAttributeCache = new ThreadSafeStore<object, T>(new Func<object, T>(JsonTypeReflector.GetAttribute<T>));
	}
}
