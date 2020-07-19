using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200018E RID: 398
	[NullableContext(1)]
	[Nullable(0)]
	public class CamelCasePropertyNamesContractResolver : DefaultContractResolver
	{
		// Token: 0x06000E8F RID: 3727 RVA: 0x00053644 File Offset: 0x00051844
		public CamelCasePropertyNamesContractResolver()
		{
			base.NamingStrategy = new CamelCaseNamingStrategy
			{
				ProcessDictionaryKeys = true,
				OverrideSpecifiedNames = true
			};
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00053674 File Offset: 0x00051874
		public override JsonContract ResolveContract(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			StructMultiKey<Type, Type> key = new StructMultiKey<Type, Type>(base.GetType(), type);
			Dictionary<StructMultiKey<Type, Type>, JsonContract> contractCache = CamelCasePropertyNamesContractResolver._contractCache;
			JsonContract jsonContract;
			if (contractCache == null || !contractCache.TryGetValue(key, out jsonContract))
			{
				jsonContract = this.CreateContract(type);
				object typeContractCacheLock = CamelCasePropertyNamesContractResolver.TypeContractCacheLock;
				lock (typeContractCacheLock)
				{
					contractCache = CamelCasePropertyNamesContractResolver._contractCache;
					Dictionary<StructMultiKey<Type, Type>, JsonContract> dictionary = (contractCache != null) ? new Dictionary<StructMultiKey<Type, Type>, JsonContract>(contractCache) : new Dictionary<StructMultiKey<Type, Type>, JsonContract>();
					dictionary[key] = jsonContract;
					CamelCasePropertyNamesContractResolver._contractCache = dictionary;
				}
			}
			return jsonContract;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00053728 File Offset: 0x00051928
		internal override DefaultJsonNameTable GetNameTable()
		{
			return CamelCasePropertyNamesContractResolver.NameTable;
		}

		// Token: 0x04000783 RID: 1923
		private static readonly object TypeContractCacheLock = new object();

		// Token: 0x04000784 RID: 1924
		private static readonly DefaultJsonNameTable NameTable = new DefaultJsonNameTable();

		// Token: 0x04000785 RID: 1925
		[Nullable(new byte[]
		{
			2,
			0,
			1,
			1,
			1
		})]
		private static Dictionary<StructMultiKey<Type, Type>, JsonContract> _contractCache;
	}
}
