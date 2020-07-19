using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Echelon.Stealer.Browsers.Helpers.NoiseMe.Drags.App.Models.JSON
{
	// Token: 0x02000031 RID: 49
	public class JsonObject : JsonValue, IDictionary<string, JsonValue>, ICollection<KeyValuePair<string, JsonValue>>, IEnumerable<KeyValuePair<string, JsonValue>>, IEnumerable
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x000097B0 File Offset: 0x000079B0
		public JsonObject(params KeyValuePair<string, JsonValue>[] items)
		{
			this.map = new SortedDictionary<string, JsonValue>(StringComparer.Ordinal);
			if (items != null)
			{
				this.AddRange(items);
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000097D8 File Offset: 0x000079D8
		public JsonObject(IEnumerable<KeyValuePair<string, JsonValue>> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.map = new SortedDictionary<string, JsonValue>(StringComparer.Ordinal);
			this.AddRange(items);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00009808 File Offset: 0x00007A08
		public override JsonType JsonType
		{
			get
			{
				return JsonType.Object;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000980C File Offset: 0x00007A0C
		public override int Count
		{
			get
			{
				return this.map.Count;
			}
		}

		// Token: 0x1700001D RID: 29
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000983C File Offset: 0x00007A3C
		public ICollection<string> Keys
		{
			get
			{
				return this.map.Keys;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000984C File Offset: 0x00007A4C
		public ICollection<JsonValue> Values
		{
			get
			{
				return this.map.Values;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000985C File Offset: 0x00007A5C
		bool ICollection<KeyValuePair<string, JsonValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00009860 File Offset: 0x00007A60
		public IEnumerator<KeyValuePair<string, JsonValue>> GetEnumerator()
		{
			return this.map.GetEnumerator();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00009874 File Offset: 0x00007A74
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.map.GetEnumerator();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00009888 File Offset: 0x00007A88
		public void Add(string key, JsonValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.map.Add(key, value);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000098A8 File Offset: 0x00007AA8
		public void Add(KeyValuePair<string, JsonValue> pair)
		{
			this.Add(pair.Key, pair.Value);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000098C0 File Offset: 0x00007AC0
		public void Clear()
		{
			this.map.Clear();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000098D0 File Offset: 0x00007AD0
		bool ICollection<KeyValuePair<string, JsonValue>>.Contains(KeyValuePair<string, JsonValue> item)
		{
			return ((ICollection<KeyValuePair<string, JsonValue>>)this.map).Contains(item);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000098E0 File Offset: 0x00007AE0
		bool ICollection<KeyValuePair<string, JsonValue>>.Remove(KeyValuePair<string, JsonValue> item)
		{
			return ((ICollection<KeyValuePair<string, JsonValue>>)this.map).Remove(item);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000098F0 File Offset: 0x00007AF0
		public override bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.map.ContainsKey(key);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00009910 File Offset: 0x00007B10
		public void CopyTo(KeyValuePair<string, JsonValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, JsonValue>>)this.map).CopyTo(array, arrayIndex);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00009920 File Offset: 0x00007B20
		public bool Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.map.Remove(key);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00009940 File Offset: 0x00007B40
		public bool TryGetValue(string key, out JsonValue value)
		{
			return this.map.TryGetValue(key, out value);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00009950 File Offset: 0x00007B50
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

		// Token: 0x060000F8 RID: 248 RVA: 0x000099C4 File Offset: 0x00007BC4
		public void AddRange(params KeyValuePair<string, JsonValue>[] items)
		{
			this.AddRange(items);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000099D0 File Offset: 0x00007BD0
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

		// Token: 0x04000063 RID: 99
		private SortedDictionary<string, JsonValue> map;
	}
}
