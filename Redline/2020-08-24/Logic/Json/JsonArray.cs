using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace RedLine.Logic.Json
{
	// Token: 0x02000052 RID: 82
	public class JsonArray : JsonValue, IList<JsonValue>, ICollection<JsonValue>, IEnumerable<JsonValue>, IEnumerable
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x00008B39 File Offset: 0x00006D39
		public JsonArray(params JsonValue[] items)
		{
			this.list = new List<JsonValue>();
			this.AddRange(items);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008B53 File Offset: 0x00006D53
		public JsonArray(IEnumerable<JsonValue> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.list = new List<JsonValue>(items);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00008B75 File Offset: 0x00006D75
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00008B82 File Offset: 0x00006D82
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B5 RID: 181
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

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00008BA2 File Offset: 0x00006DA2
		public override JsonType JsonType
		{
			get
			{
				return JsonType.Array;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008BA5 File Offset: 0x00006DA5
		public void Add(JsonValue item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.list.Add(item);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008BC1 File Offset: 0x00006DC1
		public void AddRange(IEnumerable<JsonValue> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.list.AddRange(items);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008BDD File Offset: 0x00006DDD
		public void AddRange(params JsonValue[] items)
		{
			if (items == null)
			{
				return;
			}
			this.list.AddRange(items);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008BEF File Offset: 0x00006DEF
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008BFC File Offset: 0x00006DFC
		public bool Contains(JsonValue item)
		{
			return this.list.Contains(item);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008C0A File Offset: 0x00006E0A
		public void CopyTo(JsonValue[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008C19 File Offset: 0x00006E19
		public int IndexOf(JsonValue item)
		{
			return this.list.IndexOf(item);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008C27 File Offset: 0x00006E27
		public void Insert(int index, JsonValue item)
		{
			this.list.Insert(index, item);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008C36 File Offset: 0x00006E36
		public bool Remove(JsonValue item)
		{
			return this.list.Remove(item);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008C44 File Offset: 0x00006E44
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008C54 File Offset: 0x00006E54
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

		// Token: 0x0600020B RID: 523 RVA: 0x00008CEA File Offset: 0x00006EEA
		IEnumerator<JsonValue> IEnumerable<JsonValue>.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008CEA File Offset: 0x00006EEA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x04000129 RID: 297
		private List<JsonValue> list;
	}
}
