using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000188 RID: 392
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct StructMultiKey<[Nullable(2)] T1, [Nullable(2)] T2> : IEquatable<StructMultiKey<T1, T2>>
	{
		// Token: 0x06000E70 RID: 3696 RVA: 0x000532D4 File Offset: 0x000514D4
		public StructMultiKey(T1 v1, T2 v2)
		{
			this.Value1 = v1;
			this.Value2 = v2;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000532E4 File Offset: 0x000514E4
		public override int GetHashCode()
		{
			T1 value = this.Value1;
			ref T1 ptr = ref value;
			T1 t = default(T1);
			int num;
			if (t == null)
			{
				t = value;
				ptr = ref t;
				if (t == null)
				{
					num = 0;
					goto IL_41;
				}
			}
			num = ptr.GetHashCode();
			IL_41:
			T2 value2 = this.Value2;
			ref T2 ptr2 = ref value2;
			T2 t2 = default(T2);
			int num2;
			if (t2 == null)
			{
				t2 = value2;
				ptr2 = ref t2;
				if (t2 == null)
				{
					num2 = 0;
					goto IL_82;
				}
			}
			num2 = ptr2.GetHashCode();
			IL_82:
			return num ^ num2;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00053378 File Offset: 0x00051578
		public override bool Equals(object obj)
		{
			if (obj is StructMultiKey<T1, T2>)
			{
				StructMultiKey<T1, T2> other = (StructMultiKey<T1, T2>)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x000533AC File Offset: 0x000515AC
		public bool Equals([Nullable(new byte[]
		{
			0,
			1,
			1
		})] StructMultiKey<T1, T2> other)
		{
			return object.Equals(this.Value1, other.Value1) && object.Equals(this.Value2, other.Value2);
		}

		// Token: 0x0400077E RID: 1918
		public readonly T1 Value1;

		// Token: 0x0400077F RID: 1919
		public readonly T2 Value2;
	}
}
