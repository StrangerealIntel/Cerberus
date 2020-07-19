using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000185 RID: 389
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct StringReference
	{
		// Token: 0x170002FC RID: 764
		public char this[int i]
		{
			get
			{
				return this._chars[i];
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x00052CAC File Offset: 0x00050EAC
		public char[] Chars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00052CB4 File Offset: 0x00050EB4
		public int StartIndex
		{
			get
			{
				return this._startIndex;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00052CBC File Offset: 0x00050EBC
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00052CC4 File Offset: 0x00050EC4
		public StringReference(char[] chars, int startIndex, int length)
		{
			this._chars = chars;
			this._startIndex = startIndex;
			this._length = length;
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00052CDC File Offset: 0x00050EDC
		public override string ToString()
		{
			return new string(this._chars, this._startIndex, this._length);
		}

		// Token: 0x04000776 RID: 1910
		private readonly char[] _chars;

		// Token: 0x04000777 RID: 1911
		private readonly int _startIndex;

		// Token: 0x04000778 RID: 1912
		private readonly int _length;
	}
}
