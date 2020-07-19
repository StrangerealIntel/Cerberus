using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000169 RID: 361
	[NullableContext(1)]
	[Nullable(0)]
	internal class DictionaryWrapper<[Nullable(2)] TKey, [Nullable(2)] TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IWrappedDictionary, IDictionary, ICollection
	{
		// Token: 0x06000D30 RID: 3376 RVA: 0x0004D010 File Offset: 0x0004B210
		public DictionaryWrapper(IDictionary dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._dictionary = dictionary;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0004D02C File Offset: 0x0004B22C
		public DictionaryWrapper(IDictionary<TKey, TValue> dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._genericDictionary = dictionary;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0004D048 File Offset: 0x0004B248
		internal IDictionary<TKey, TValue> GenericDictionary
		{
			get
			{
				return this._genericDictionary;
			}
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0004D050 File Offset: 0x0004B250
		public void Add(TKey key, TValue value)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Add(key, value);
				return;
			}
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Add(key, value);
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0004D0A4 File Offset: 0x0004B2A4
		public bool ContainsKey(TKey key)
		{
			if (this._dictionary != null)
			{
				return this._dictionary.Contains(key);
			}
			return this.GenericDictionary.ContainsKey(key);
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0004D0D0 File Offset: 0x0004B2D0
		public ICollection<TKey> Keys
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Keys.Cast<TKey>().ToList<TKey>();
				}
				return this.GenericDictionary.Keys;
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0004D100 File Offset: 0x0004B300
		public bool Remove(TKey key)
		{
			if (this._dictionary == null)
			{
				return this.GenericDictionary.Remove(key);
			}
			if (this._dictionary.Contains(key))
			{
				this._dictionary.Remove(key);
				return true;
			}
			return false;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0004D154 File Offset: 0x0004B354
		public bool TryGetValue(TKey key, [MaybeNull] out TValue value)
		{
			if (this._dictionary == null)
			{
				return this.GenericDictionary.TryGetValue(key, out value);
			}
			if (!this._dictionary.Contains(key))
			{
				value = default(TValue);
				return false;
			}
			value = (TValue)((object)this._dictionary[key]);
			return true;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0004D1BC File Offset: 0x0004B3BC
		public ICollection<TValue> Values
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Values.Cast<TValue>().ToList<TValue>();
				}
				return this.GenericDictionary.Values;
			}
		}

		// Token: 0x170002DB RID: 731
		public TValue this[TKey key]
		{
			get
			{
				if (this._dictionary != null)
				{
					return (TValue)((object)this._dictionary[key]);
				}
				return this.GenericDictionary[key];
			}
			set
			{
				if (this._dictionary != null)
				{
					this._dictionary[key] = value;
					return;
				}
				this.GenericDictionary[key] = value;
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0004D250 File Offset: 0x0004B450
		public void Add([Nullable(new byte[]
		{
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				((IList)this._dictionary).Add(item);
				return;
			}
			IDictionary<TKey, TValue> genericDictionary = this._genericDictionary;
			if (genericDictionary == null)
			{
				return;
			}
			genericDictionary.Add(item);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0004D28C File Offset: 0x0004B48C
		public void Clear()
		{
			if (this._dictionary != null)
			{
				this._dictionary.Clear();
				return;
			}
			this.GenericDictionary.Clear();
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0004D2B0 File Offset: 0x0004B4B0
		public bool Contains([Nullable(new byte[]
		{
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				return ((IList)this._dictionary).Contains(item);
			}
			return this.GenericDictionary.Contains(item);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0004D2E0 File Offset: 0x0004B4E0
		public void CopyTo([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (this._dictionary != null)
			{
				using (IDictionaryEnumerator enumerator = this._dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DictionaryEntry entry = enumerator.Entry;
						array[arrayIndex++] = new KeyValuePair<TKey, TValue>((TKey)((object)entry.Key), (TValue)((object)entry.Value));
					}
					return;
				}
			}
			this.GenericDictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0004D37C File Offset: 0x0004B57C
		public int Count
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Count;
				}
				return this.GenericDictionary.Count;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0004D3A0 File Offset: 0x0004B5A0
		public bool IsReadOnly
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.IsReadOnly;
				}
				return this.GenericDictionary.IsReadOnly;
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0004D3C4 File Offset: 0x0004B5C4
		public bool Remove([Nullable(new byte[]
		{
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary == null)
			{
				return this.GenericDictionary.Remove(item);
			}
			if (!this._dictionary.Contains(item.Key))
			{
				return true;
			}
			if (object.Equals(this._dictionary[item.Key], item.Value))
			{
				this._dictionary.Remove(item.Key);
				return true;
			}
			return false;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0004D454 File Offset: 0x0004B654
		[return: Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})]
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			if (this._dictionary != null)
			{
				return (from DictionaryEntry de in this._dictionary
				select new KeyValuePair<TKey, TValue>((TKey)((object)de.Key), (TValue)((object)de.Value))).GetEnumerator();
			}
			return this.GenericDictionary.GetEnumerator();
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0004D4B4 File Offset: 0x0004B6B4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0004D4BC File Offset: 0x0004B6BC
		void IDictionary.Add(object key, object value)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Add(key, value);
				return;
			}
			this.GenericDictionary.Add((TKey)((object)key), (TValue)((object)value));
		}

		// Token: 0x170002DE RID: 734
		[Nullable(2)]
		object IDictionary.this[object key]
		{
			[return: Nullable(2)]
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary[key];
				}
				return this.GenericDictionary[(TKey)((object)key)];
			}
			[param: Nullable(2)]
			set
			{
				if (this._dictionary != null)
				{
					this._dictionary[key] = value;
					return;
				}
				this.GenericDictionary[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0004D554 File Offset: 0x0004B754
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			if (this._dictionary != null)
			{
				return this._dictionary.GetEnumerator();
			}
			return new DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(this.GenericDictionary.GetEnumerator());
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0004D584 File Offset: 0x0004B784
		bool IDictionary.Contains(object key)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.ContainsKey((TKey)((object)key));
			}
			return this._dictionary.Contains(key);
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0004D5B0 File Offset: 0x0004B7B0
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._genericDictionary == null && this._dictionary.IsFixedSize;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0004D5CC File Offset: 0x0004B7CC
		ICollection IDictionary.Keys
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Keys.ToList<TKey>();
				}
				return this._dictionary.Keys;
			}
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0004D5F8 File Offset: 0x0004B7F8
		public void Remove(object key)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Remove(key);
				return;
			}
			this.GenericDictionary.Remove((TKey)((object)key));
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x0004D624 File Offset: 0x0004B824
		ICollection IDictionary.Values
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Values.ToList<TValue>();
				}
				return this._dictionary.Values;
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0004D650 File Offset: 0x0004B850
		void ICollection.CopyTo(Array array, int index)
		{
			if (this._dictionary != null)
			{
				this._dictionary.CopyTo(array, index);
				return;
			}
			this.GenericDictionary.CopyTo((KeyValuePair<TKey, TValue>[])array, index);
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0004D680 File Offset: 0x0004B880
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._dictionary != null && this._dictionary.IsSynchronized;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0004D69C File Offset: 0x0004B89C
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0004D6C4 File Offset: 0x0004B8C4
		public object UnderlyingDictionary
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary;
				}
				return this.GenericDictionary;
			}
		}

		// Token: 0x0400072D RID: 1837
		[Nullable(2)]
		private readonly IDictionary _dictionary;

		// Token: 0x0400072E RID: 1838
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private readonly IDictionary<TKey, TValue> _genericDictionary;

		// Token: 0x0400072F RID: 1839
		[Nullable(2)]
		private object _syncRoot;

		// Token: 0x020002B9 RID: 697
		[Nullable(0)]
		private readonly struct DictionaryEnumerator<[Nullable(2)] TEnumeratorKey, [Nullable(2)] TEnumeratorValue> : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x0600179B RID: 6043 RVA: 0x000785B8 File Offset: 0x000767B8
			public DictionaryEnumerator([Nullable(new byte[]
			{
				1,
				0,
				1,
				1
			})] IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> e)
			{
				ValidationUtils.ArgumentNotNull(e, "e");
				this._e = e;
			}

			// Token: 0x170004D4 RID: 1236
			// (get) Token: 0x0600179C RID: 6044 RVA: 0x000785CC File Offset: 0x000767CC
			public DictionaryEntry Entry
			{
				get
				{
					return (DictionaryEntry)this.Current;
				}
			}

			// Token: 0x170004D5 RID: 1237
			// (get) Token: 0x0600179D RID: 6045 RVA: 0x000785DC File Offset: 0x000767DC
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x170004D6 RID: 1238
			// (get) Token: 0x0600179E RID: 6046 RVA: 0x000785FC File Offset: 0x000767FC
			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x170004D7 RID: 1239
			// (get) Token: 0x0600179F RID: 6047 RVA: 0x0007861C File Offset: 0x0007681C
			public object Current
			{
				get
				{
					KeyValuePair<TEnumeratorKey, TEnumeratorValue> keyValuePair = this._e.Current;
					object key = keyValuePair.Key;
					keyValuePair = this._e.Current;
					return new DictionaryEntry(key, keyValuePair.Value);
				}
			}

			// Token: 0x060017A0 RID: 6048 RVA: 0x00078668 File Offset: 0x00076868
			public bool MoveNext()
			{
				return this._e.MoveNext();
			}

			// Token: 0x060017A1 RID: 6049 RVA: 0x00078678 File Offset: 0x00076878
			public void Reset()
			{
				this._e.Reset();
			}

			// Token: 0x04000BD8 RID: 3032
			[Nullable(new byte[]
			{
				1,
				0,
				1,
				1
			})]
			private readonly IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> _e;
		}
	}
}
