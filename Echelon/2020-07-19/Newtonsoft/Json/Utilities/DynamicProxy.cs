using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200016A RID: 362
	[NullableContext(1)]
	[Nullable(0)]
	internal class DynamicProxy<[Nullable(2)] T>
	{
		// Token: 0x06000D51 RID: 3409 RVA: 0x0004D6E0 File Offset: 0x0004B8E0
		public virtual IEnumerable<string> GetDynamicMemberNames(T instance)
		{
			return CollectionUtils.ArrayEmpty<string>();
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0004D6E8 File Offset: 0x0004B8E8
		public virtual bool TryBinaryOperation(T instance, BinaryOperationBinder binder, object arg, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0004D6F0 File Offset: 0x0004B8F0
		public virtual bool TryConvert(T instance, ConvertBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0004D6F8 File Offset: 0x0004B8F8
		public virtual bool TryCreateInstance(T instance, CreateInstanceBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0004D700 File Offset: 0x0004B900
		public virtual bool TryDeleteIndex(T instance, DeleteIndexBinder binder, object[] indexes)
		{
			return false;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0004D704 File Offset: 0x0004B904
		public virtual bool TryDeleteMember(T instance, DeleteMemberBinder binder)
		{
			return false;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0004D708 File Offset: 0x0004B908
		public virtual bool TryGetIndex(T instance, GetIndexBinder binder, object[] indexes, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0004D710 File Offset: 0x0004B910
		public virtual bool TryGetMember(T instance, GetMemberBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0004D718 File Offset: 0x0004B918
		public virtual bool TryInvoke(T instance, InvokeBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0004D720 File Offset: 0x0004B920
		public virtual bool TryInvokeMember(T instance, InvokeMemberBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0004D728 File Offset: 0x0004B928
		public virtual bool TrySetIndex(T instance, SetIndexBinder binder, object[] indexes, object value)
		{
			return false;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0004D72C File Offset: 0x0004B92C
		public virtual bool TrySetMember(T instance, SetMemberBinder binder, object value)
		{
			return false;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0004D730 File Offset: 0x0004B930
		public virtual bool TryUnaryOperation(T instance, UnaryOperationBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}
	}
}
