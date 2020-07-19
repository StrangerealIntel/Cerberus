using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001D7 RID: 471
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct JEnumerable<[Nullable(0)] T> : IJEnumerable<T>, IEnumerable<T>, IEnumerable, IEquatable<JEnumerable<T>> where T : JToken
	{
		// Token: 0x06001299 RID: 4761 RVA: 0x00065060 File Offset: 0x00063260
		public JEnumerable(IEnumerable<T> enumerable)
		{
			ValidationUtils.ArgumentNotNull(enumerable, "enumerable");
			this._enumerable = enumerable;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00065074 File Offset: 0x00063274
		public IEnumerator<T> GetEnumerator()
		{
			return (this._enumerable ?? JEnumerable<T>.Empty).GetEnumerator();
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00065094 File Offset: 0x00063294
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000402 RID: 1026
		public IJEnumerable<JToken> this[object key]
		{
			get
			{
				if (this._enumerable == null)
				{
					return JEnumerable<JToken>.Empty;
				}
				return new JEnumerable<JToken>(this._enumerable.Values(key));
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x000650CC File Offset: 0x000632CC
		public bool Equals([Nullable(new byte[]
		{
			0,
			1
		})] JEnumerable<T> other)
		{
			return object.Equals(this._enumerable, other._enumerable);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x000650E0 File Offset: 0x000632E0
		public override bool Equals(object obj)
		{
			if (obj is JEnumerable<T>)
			{
				JEnumerable<T> other = (JEnumerable<T>)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0006510C File Offset: 0x0006330C
		public override int GetHashCode()
		{
			if (this._enumerable == null)
			{
				return 0;
			}
			return this._enumerable.GetHashCode();
		}

		// Token: 0x040008D9 RID: 2265
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public static readonly JEnumerable<T> Empty = new JEnumerable<T>(Enumerable.Empty<T>());

		// Token: 0x040008DA RID: 2266
		private readonly IEnumerable<T> _enumerable;
	}
}
