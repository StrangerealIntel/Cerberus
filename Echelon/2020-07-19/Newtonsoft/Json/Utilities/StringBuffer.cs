using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000184 RID: 388
	[NullableContext(2)]
	[Nullable(0)]
	internal struct StringBuffer
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00052B20 File Offset: 0x00050D20
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x00052B28 File Offset: 0x00050D28
		public int Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00052B34 File Offset: 0x00050D34
		public bool IsEmpty
		{
			get
			{
				return this._buffer == null;
			}
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00052B40 File Offset: 0x00050D40
		public StringBuffer(IArrayPool<char> bufferPool, int initalSize)
		{
			this = new StringBuffer(BufferUtils.RentBuffer(bufferPool, initalSize));
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00052B50 File Offset: 0x00050D50
		[NullableContext(1)]
		private StringBuffer(char[] buffer)
		{
			this._buffer = buffer;
			this._position = 0;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00052B60 File Offset: 0x00050D60
		public void Append(IArrayPool<char> bufferPool, char value)
		{
			if (this._position == this._buffer.Length)
			{
				this.EnsureSize(bufferPool, 1);
			}
			char[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			buffer[position] = value;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00052BA8 File Offset: 0x00050DA8
		[NullableContext(1)]
		public void Append([Nullable(2)] IArrayPool<char> bufferPool, char[] buffer, int startIndex, int count)
		{
			if (this._position + count >= this._buffer.Length)
			{
				this.EnsureSize(bufferPool, count);
			}
			Array.Copy(buffer, startIndex, this._buffer, this._position, count);
			this._position += count;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00052BFC File Offset: 0x00050DFC
		public void Clear(IArrayPool<char> bufferPool)
		{
			if (this._buffer != null)
			{
				BufferUtils.ReturnBuffer(bufferPool, this._buffer);
				this._buffer = null;
			}
			this._position = 0;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00052C24 File Offset: 0x00050E24
		private void EnsureSize(IArrayPool<char> bufferPool, int appendLength)
		{
			char[] array = BufferUtils.RentBuffer(bufferPool, (this._position + appendLength) * 2);
			if (this._buffer != null)
			{
				Array.Copy(this._buffer, array, this._position);
				BufferUtils.ReturnBuffer(bufferPool, this._buffer);
			}
			this._buffer = array;
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00052C78 File Offset: 0x00050E78
		[NullableContext(1)]
		public override string ToString()
		{
			return this.ToString(0, this._position);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00052C88 File Offset: 0x00050E88
		[NullableContext(1)]
		public string ToString(int start, int length)
		{
			return new string(this._buffer, start, length);
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x00052C98 File Offset: 0x00050E98
		public char[] InternalBuffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x04000774 RID: 1908
		private char[] _buffer;

		// Token: 0x04000775 RID: 1909
		private int _position;
	}
}
