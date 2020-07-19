using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200015D RID: 349
	[NullableContext(1)]
	[Nullable(0)]
	internal class BidirectionalDictionary<[Nullable(2)] TFirst, [Nullable(2)] TSecond>
	{
		// Token: 0x06000CBA RID: 3258 RVA: 0x0004989C File Offset: 0x00047A9C
		public BidirectionalDictionary() : this(EqualityComparer<TFirst>.Default, EqualityComparer<TSecond>.Default)
		{
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000498B0 File Offset: 0x00047AB0
		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer) : this(firstEqualityComparer, secondEqualityComparer, "Duplicate item already exists for '{0}'.", "Duplicate item already exists for '{0}'.")
		{
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000498C4 File Offset: 0x00047AC4
		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer, string duplicateFirstErrorMessage, string duplicateSecondErrorMessage)
		{
			this._firstToSecond = new Dictionary<TFirst, TSecond>(firstEqualityComparer);
			this._secondToFirst = new Dictionary<TSecond, TFirst>(secondEqualityComparer);
			this._duplicateFirstErrorMessage = duplicateFirstErrorMessage;
			this._duplicateSecondErrorMessage = duplicateSecondErrorMessage;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x000498F4 File Offset: 0x00047AF4
		public void Set(TFirst first, TSecond second)
		{
			TSecond tsecond;
			if (this._firstToSecond.TryGetValue(first, out tsecond) && !tsecond.Equals(second))
			{
				throw new ArgumentException(this._duplicateFirstErrorMessage.FormatWith(CultureInfo.InvariantCulture, first));
			}
			TFirst tfirst;
			if (this._secondToFirst.TryGetValue(second, out tfirst) && !tfirst.Equals(first))
			{
				throw new ArgumentException(this._duplicateSecondErrorMessage.FormatWith(CultureInfo.InvariantCulture, second));
			}
			this._firstToSecond.Add(first, second);
			this._secondToFirst.Add(second, first);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x000499B0 File Offset: 0x00047BB0
		public bool TryGetByFirst(TFirst first, out TSecond second)
		{
			return this._firstToSecond.TryGetValue(first, out second);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x000499C0 File Offset: 0x00047BC0
		public bool TryGetBySecond(TSecond second, out TFirst first)
		{
			return this._secondToFirst.TryGetValue(second, out first);
		}

		// Token: 0x040006C8 RID: 1736
		private readonly IDictionary<TFirst, TSecond> _firstToSecond;

		// Token: 0x040006C9 RID: 1737
		private readonly IDictionary<TSecond, TFirst> _secondToFirst;

		// Token: 0x040006CA RID: 1738
		private readonly string _duplicateFirstErrorMessage;

		// Token: 0x040006CB RID: 1739
		private readonly string _duplicateSecondErrorMessage;
	}
}
