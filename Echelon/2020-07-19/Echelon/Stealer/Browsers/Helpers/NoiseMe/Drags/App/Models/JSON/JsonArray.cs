using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Echelon.Stealer.Browsers.Helpers.NoiseMe.Drags.App.Models.JSON
{
	// Token: 0x0200002F RID: 47
	public class JsonArray : JsonValue, IList<JsonValue>, ICollection<JsonValue>, IEnumerable<JsonValue>, IEnumerable
	{
		// Token: 0x060000CD RID: 205 RVA: 0x0000957C File Offset: 0x0000777C
		public JsonArray(params JsonValue[] items)
		{
			this.list = new List<JsonValue>();
			this.AddRange(items);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00009598 File Offset: 0x00007798
		public JsonArray(IEnumerable<JsonValue> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.list = new List<JsonValue>(items);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000095C0 File Offset: 0x000077C0
		public override JsonType JsonType
		{
			get
			{
				return JsonType.Array;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000095C4 File Offset: 0x000077C4
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000095D4 File Offset: 0x000077D4
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001A RID: 26
		public sealed override JsonValue this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000095F8 File Offset: 0x000077F8
		public void Add(JsonValue item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.list.Add(item);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00009618 File Offset: 0x00007818
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00009628 File Offset: 0x00007828
		public bool Contains(JsonValue item)
		{
			return this.list.Contains(item);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00009638 File Offset: 0x00007838
		public void CopyTo(JsonValue[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00009648 File Offset: 0x00007848
		public int IndexOf(JsonValue item)
		{
			return this.list.IndexOf(item);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00009658 File Offset: 0x00007858
		public void Insert(int index, JsonValue item)
		{
			this.list.Insert(index, item);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00009668 File Offset: 0x00007868
		public bool Remove(JsonValue item)
		{
			return this.list.Remove(item);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00009678 File Offset: 0x00007878
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00009688 File Offset: 0x00007888
		IEnumerator<JsonValue> IEnumerable<JsonValue>.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000969C File Offset: 0x0000789C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000096B0 File Offset: 0x000078B0
		public void AddRange(IEnumerable<JsonValue> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.list.AddRange(items);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000096D0 File Offset: 0x000078D0
		public void AddRange(params JsonValue[] items)
		{
			if (items != null)
			{
				this.list.AddRange(items);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000096E4 File Offset: 0x000078E4
		public override void Save(Stream stream, bool parsing)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			stream.WriteByte(91);
			for (int i = 0; i < this.list.Count; i++)
			{
				JsonValue jsonValue = this.list[i];
				if (jsonValue != null)
				{
					jsonValue.Save(stream, parsing);
				}
				else
				{
					stream.WriteByte(110);
					stream.WriteByte(117);
					stream.WriteByte(108);
					stream.WriteByte(108);
				}
				if (i < this.Count - 1)
				{
					stream.WriteByte(44);
					stream.WriteByte(32);
				}
			}
			stream.WriteByte(93);
		}

		// Token: 0x04000062 RID: 98
		private List<JsonValue> list;
	}
}
