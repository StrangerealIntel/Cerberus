using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x0200012B RID: 299
	[NullableContext(1)]
	[Nullable(0)]
	public class DefaultJsonNameTable : JsonNameTable
	{
		// Token: 0x060009B4 RID: 2484 RVA: 0x0003DB08 File Offset: 0x0003BD08
		public DefaultJsonNameTable()
		{
			this._entries = new DefaultJsonNameTable.Entry[this._mask + 1];
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0003DB2C File Offset: 0x0003BD2C
		[return: Nullable(2)]
		public override string Get(char[] key, int start, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			int num = length + DefaultJsonNameTable.HashCodeRandomizer;
			num += (num << 7 ^ (int)key[start]);
			int num2 = start + length;
			for (int i = start + 1; i < num2; i++)
			{
				num += (num << 7 ^ (int)key[i]);
			}
			num -= num >> 17;
			num -= num >> 11;
			num -= num >> 5;
			int num3 = num & this._mask;
			for (DefaultJsonNameTable.Entry entry = this._entries[num3]; entry != null; entry = entry.Next)
			{
				if (entry.HashCode == num && DefaultJsonNameTable.TextEquals(entry.Value, key, start, length))
				{
					return entry.Value;
				}
			}
			return null;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0003DBE4 File Offset: 0x0003BDE4
		public string Add(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int length = key.Length;
			if (length == 0)
			{
				return string.Empty;
			}
			int num = length + DefaultJsonNameTable.HashCodeRandomizer;
			for (int i = 0; i < key.Length; i++)
			{
				num += (num << 7 ^ (int)key[i]);
			}
			num -= num >> 17;
			num -= num >> 11;
			num -= num >> 5;
			for (DefaultJsonNameTable.Entry entry = this._entries[num & this._mask]; entry != null; entry = entry.Next)
			{
				if (entry.HashCode == num && entry.Value.Equals(key, StringComparison.Ordinal))
				{
					return entry.Value;
				}
			}
			return this.AddEntry(key, num);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0003DCA8 File Offset: 0x0003BEA8
		private string AddEntry(string str, int hashCode)
		{
			int num = hashCode & this._mask;
			DefaultJsonNameTable.Entry entry = new DefaultJsonNameTable.Entry(str, hashCode, this._entries[num]);
			this._entries[num] = entry;
			int count = this._count;
			this._count = count + 1;
			if (count == this._mask)
			{
				this.Grow();
			}
			return entry.Value;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0003DD0C File Offset: 0x0003BF0C
		private void Grow()
		{
			DefaultJsonNameTable.Entry[] entries = this._entries;
			int num = this._mask * 2 + 1;
			DefaultJsonNameTable.Entry[] array = new DefaultJsonNameTable.Entry[num + 1];
			foreach (DefaultJsonNameTable.Entry entry in entries)
			{
				while (entry != null)
				{
					int num2 = entry.HashCode & num;
					DefaultJsonNameTable.Entry next = entry.Next;
					entry.Next = array[num2];
					array[num2] = entry;
					entry = next;
				}
			}
			this._entries = array;
			this._mask = num;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0003DD9C File Offset: 0x0003BF9C
		private static bool TextEquals(string str1, char[] str2, int str2Start, int str2Length)
		{
			if (str1.Length != str2Length)
			{
				return false;
			}
			for (int i = 0; i < str1.Length; i++)
			{
				if (str1[i] != str2[str2Start + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400058E RID: 1422
		private static readonly int HashCodeRandomizer = Environment.TickCount;

		// Token: 0x0400058F RID: 1423
		private int _count;

		// Token: 0x04000590 RID: 1424
		private DefaultJsonNameTable.Entry[] _entries;

		// Token: 0x04000591 RID: 1425
		private int _mask = 31;

		// Token: 0x020002B0 RID: 688
		[Nullable(0)]
		private class Entry
		{
			// Token: 0x06001780 RID: 6016 RVA: 0x00078348 File Offset: 0x00076548
			internal Entry(string value, int hashCode, DefaultJsonNameTable.Entry next)
			{
				this.Value = value;
				this.HashCode = hashCode;
				this.Next = next;
			}

			// Token: 0x04000BA5 RID: 2981
			internal readonly string Value;

			// Token: 0x04000BA6 RID: 2982
			internal readonly int HashCode;

			// Token: 0x04000BA7 RID: 2983
			internal DefaultJsonNameTable.Entry Next;
		}
	}
}
