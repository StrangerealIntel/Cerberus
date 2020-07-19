using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000189 RID: 393
	[NullableContext(1)]
	[Nullable(0)]
	internal class ThreadSafeStore<[Nullable(2)] TKey, [Nullable(2)] TValue>
	{
		// Token: 0x06000E74 RID: 3700 RVA: 0x000533EC File Offset: 0x000515EC
		public ThreadSafeStore(Func<TKey, TValue> creator)
		{
			ValidationUtils.ArgumentNotNull(creator, "creator");
			this._creator = creator;
			this._concurrentStore = new ConcurrentDictionary<TKey, TValue>();
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00053414 File Offset: 0x00051614
		public TValue Get(TKey key)
		{
			return this._concurrentStore.GetOrAdd(key, this._creator);
		}

		// Token: 0x04000780 RID: 1920
		private readonly ConcurrentDictionary<TKey, TValue> _concurrentStore;

		// Token: 0x04000781 RID: 1921
		private readonly Func<TKey, TValue> _creator;
	}
}
