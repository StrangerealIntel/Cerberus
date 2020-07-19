using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001DB RID: 475
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal class JPropertyKeyedCollection : Collection<JToken>
	{
		// Token: 0x06001306 RID: 4870 RVA: 0x000660DC File Offset: 0x000642DC
		public JPropertyKeyedCollection() : base(new List<JToken>())
		{
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x000660EC File Offset: 0x000642EC
		private void AddKey(string key, JToken item)
		{
			this.EnsureDictionary();
			this._dictionary[key] = item;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00066104 File Offset: 0x00064304
		protected void ChangeItemKey(JToken item, string newKey)
		{
			if (!this.ContainsItem(item))
			{
				throw new ArgumentException("The specified item does not exist in this KeyedCollection.");
			}
			string keyForItem = this.GetKeyForItem(item);
			if (!JPropertyKeyedCollection.Comparer.Equals(keyForItem, newKey))
			{
				if (newKey != null)
				{
					this.AddKey(newKey, item);
				}
				if (keyForItem != null)
				{
					this.RemoveKey(keyForItem);
				}
			}
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00066160 File Offset: 0x00064360
		protected override void ClearItems()
		{
			base.ClearItems();
			Dictionary<string, JToken> dictionary = this._dictionary;
			if (dictionary == null)
			{
				return;
			}
			dictionary.Clear();
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0006617C File Offset: 0x0006437C
		public bool Contains(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this._dictionary != null && this._dictionary.ContainsKey(key);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x000661A8 File Offset: 0x000643A8
		private bool ContainsItem(JToken item)
		{
			if (this._dictionary == null)
			{
				return false;
			}
			string keyForItem = this.GetKeyForItem(item);
			JToken jtoken;
			return this._dictionary.TryGetValue(keyForItem, out jtoken);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x000661DC File Offset: 0x000643DC
		private void EnsureDictionary()
		{
			if (this._dictionary == null)
			{
				this._dictionary = new Dictionary<string, JToken>(JPropertyKeyedCollection.Comparer);
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x000661FC File Offset: 0x000643FC
		private string GetKeyForItem(JToken item)
		{
			return ((JProperty)item).Name;
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0006620C File Offset: 0x0006440C
		protected override void InsertItem(int index, JToken item)
		{
			this.AddKey(this.GetKeyForItem(item), item);
			base.InsertItem(index, item);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00066224 File Offset: 0x00064424
		public bool Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			JToken item;
			return this._dictionary != null && this._dictionary.TryGetValue(key, out item) && base.Remove(item);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00066270 File Offset: 0x00064470
		protected override void RemoveItem(int index)
		{
			string keyForItem = this.GetKeyForItem(base.Items[index]);
			this.RemoveKey(keyForItem);
			base.RemoveItem(index);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000662A4 File Offset: 0x000644A4
		private void RemoveKey(string key)
		{
			Dictionary<string, JToken> dictionary = this._dictionary;
			if (dictionary == null)
			{
				return;
			}
			dictionary.Remove(key);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000662BC File Offset: 0x000644BC
		protected override void SetItem(int index, JToken item)
		{
			string keyForItem = this.GetKeyForItem(item);
			string keyForItem2 = this.GetKeyForItem(base.Items[index]);
			if (JPropertyKeyedCollection.Comparer.Equals(keyForItem2, keyForItem))
			{
				if (this._dictionary != null)
				{
					this._dictionary[keyForItem] = item;
				}
			}
			else
			{
				this.AddKey(keyForItem, item);
				if (keyForItem2 != null)
				{
					this.RemoveKey(keyForItem2);
				}
			}
			base.SetItem(index, item);
		}

		// Token: 0x17000412 RID: 1042
		public JToken this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (this._dictionary != null)
				{
					return this._dictionary[key];
				}
				throw new KeyNotFoundException();
			}
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00066364 File Offset: 0x00064564
		public bool TryGetValue(string key, [Nullable(2), NotNullWhen(true)] out JToken value)
		{
			if (this._dictionary == null)
			{
				value = null;
				return false;
			}
			return this._dictionary.TryGetValue(key, out value);
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x00066384 File Offset: 0x00064584
		public ICollection<string> Keys
		{
			get
			{
				this.EnsureDictionary();
				return this._dictionary.Keys;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00066398 File Offset: 0x00064598
		public ICollection<JToken> Values
		{
			get
			{
				this.EnsureDictionary();
				return this._dictionary.Values;
			}
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x000663AC File Offset: 0x000645AC
		public int IndexOfReference(JToken t)
		{
			return ((List<JToken>)base.Items).IndexOfReference(t);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x000663C0 File Offset: 0x000645C0
		public bool Compare(JPropertyKeyedCollection other)
		{
			if (this == other)
			{
				return true;
			}
			Dictionary<string, JToken> dictionary = this._dictionary;
			Dictionary<string, JToken> dictionary2 = other._dictionary;
			if (dictionary == null && dictionary2 == null)
			{
				return true;
			}
			if (dictionary == null)
			{
				return dictionary2.Count == 0;
			}
			if (dictionary2 == null)
			{
				return dictionary.Count == 0;
			}
			if (dictionary.Count != dictionary2.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, JToken> keyValuePair in dictionary)
			{
				JToken jtoken;
				if (!dictionary2.TryGetValue(keyValuePair.Key, out jtoken))
				{
					return false;
				}
				JProperty jproperty = (JProperty)keyValuePair.Value;
				JProperty jproperty2 = (JProperty)jtoken;
				if (jproperty.Value == null)
				{
					return jproperty2.Value == null;
				}
				if (!jproperty.Value.DeepEquals(jproperty2.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040008E0 RID: 2272
		private static readonly IEqualityComparer<string> Comparer = StringComparer.Ordinal;

		// Token: 0x040008E1 RID: 2273
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private Dictionary<string, JToken> _dictionary;
	}
}
