using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RedLine.Logic.Json
{
	// Token: 0x02000053 RID: 83
	public class JsonObject : JsonValue, IDictionary<string, JsonValue>, ICollection<KeyValuePair<string, JsonValue>>, IEnumerable<KeyValuePair<string, JsonValue>>, IEnumerable
	{
		// Token: 0x0600020D RID: 525 RVA: 0x00008CFC File Offset: 0x00006EFC
		public JsonObject(params KeyValuePair<string, JsonValue>[] items)
		{
			this.map = new SortedDictionary<string, JsonValue>(StringComparer.Ordinal);
			if (items != null)
			{
				this.AddRange(items);
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008D1E File Offset: 0x00006F1E
		public JsonObject(IEnumerable<KeyValuePair<string, JsonValue>> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.map = new SortedDictionary<string, JsonValue>(StringComparer.Ordinal);
			this.AddRange(items);
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00008D4B File Offset: 0x00006F4B
		public override int Count
		{
			get
			{
				return this.map.Count;
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008D58 File Offset: 0x00006F58
		public IEnumerator<KeyValuePair<string, JsonValue>> GetEnumerator()
		{
			return this.map.GetEnumerator();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008D58 File Offset: 0x00006F58
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.map.GetEnumerator();
		}

		// Token: 0x170000B8 RID: 184
		public sealed override JsonValue this[string key]
		{
			get
			{
				return this.map[key];
			}
			set
			{
				this.map[key] = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00008D87 File Offset: 0x00006F87
		public override JsonType JsonType
		{
			get
			{
				return JsonType.Object;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00008D8A File Offset: 0x00006F8A
		public ICollection<string> Keys
		{
			get
			{
				return this.map.Keys;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00008D97 File Offset: 0x00006F97
		public ICollection<JsonValue> Values
		{
			get
			{
				return this.map.Values;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008DA4 File Offset: 0x00006FA4
		public void Add(string key, JsonValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.map.Add(key, value);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008DC1 File Offset: 0x00006FC1
		public void Add(KeyValuePair<string, JsonValue> pair)
		{
			this.Add(pair.Key, pair.Value);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public void AddRange(IEnumerable<KeyValuePair<string, JsonValue>> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (KeyValuePair<string, JsonValue> keyValuePair in items)
			{
				this.map.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008E40 File Offset: 0x00007040
		public void AddRange(params KeyValuePair<string, JsonValue>[] items)
		{
			this.AddRange(items);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008E49 File Offset: 0x00007049
		public void Clear()
		{
			this.map.Clear();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008E56 File Offset: 0x00007056
		bool ICollection<KeyValuePair<string, JsonValue>>.Contains(KeyValuePair<string, JsonValue> item)
		{
			return ((ICollection<KeyValuePair<string, JsonValue>>)this.map).Contains(item);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008E64 File Offset: 0x00007064
		bool ICollection<KeyValuePair<string, JsonValue>>.Remove(KeyValuePair<string, JsonValue> item)
		{
			return ((ICollection<KeyValuePair<string, JsonValue>>)this.map).Remove(item);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008E72 File Offset: 0x00007072
		public override bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.map.ContainsKey(key);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008E8E File Offset: 0x0000708E
		public void CopyTo(KeyValuePair<string, JsonValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, JsonValue>>)this.map).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008E9D File Offset: 0x0000709D
		public bool Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.map.Remove(key);
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00008B82 File Offset: 0x00006D82
		bool ICollection<KeyValuePair<string, JsonValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008EBC File Offset: 0x000070BC
		public override void Save(Stream stream, bool parsing)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			stream.WriteByte(123);
			foreach (KeyValuePair<string, JsonValue> keyValuePair in this.map)
			{
				stream.WriteByte(34);
				byte[] bytes = Encoding.UTF8.GetBytes(base.EscapeString(keyValuePair.Key));
				stream.Write(bytes, 0, bytes.Length);
				stream.WriteByte(34);
				stream.WriteByte(44);
				stream.WriteByte(32);
				if (keyValuePair.Value == null)
				{
					stream.WriteByte(110);
					stream.WriteByte(117);
					stream.WriteByte(108);
					stream.WriteByte(108);
				}
				else
				{
					keyValuePair.Value.Save(stream, parsing);
				}
			}
			stream.WriteByte(125);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00008FA8 File Offset: 0x000071A8
		public bool TryGetValue(string key, out JsonValue value)
		{
			return this.map.TryGetValue(key, out value);
		}

		// Token: 0x0400012A RID: 298
		private SortedDictionary<string, JsonValue> map;
	}
}
