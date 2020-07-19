using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000180 RID: 384
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class ReflectionDelegateFactory
	{
		// Token: 0x06000E00 RID: 3584 RVA: 0x00051430 File Offset: 0x0004F630
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public Func<T, object> CreateGet<[Nullable(2)] T>(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				if (propertyInfo.PropertyType.IsByRef)
				{
					throw new InvalidOperationException("Could not create getter for {0}. ByRef return values are not supported.".FormatWith(CultureInfo.InvariantCulture, propertyInfo));
				}
				return this.CreateGet<T>(propertyInfo);
			}
			else
			{
				FieldInfo fieldInfo = memberInfo as FieldInfo;
				if (fieldInfo != null)
				{
					return this.CreateGet<T>(fieldInfo);
				}
				throw new Exception("Could not create getter for {0}.".FormatWith(CultureInfo.InvariantCulture, memberInfo));
			}
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x000514A8 File Offset: 0x0004F6A8
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public Action<T, object> CreateSet<[Nullable(2)] T>(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				return this.CreateSet<T>(propertyInfo);
			}
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			if (fieldInfo != null)
			{
				return this.CreateSet<T>(fieldInfo);
			}
			throw new Exception("Could not create setter for {0}.".FormatWith(CultureInfo.InvariantCulture, memberInfo));
		}

		// Token: 0x06000E02 RID: 3586
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public abstract MethodCall<T, object> CreateMethodCall<[Nullable(2)] T>(MethodBase method);

		// Token: 0x06000E03 RID: 3587
		public abstract ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method);

		// Token: 0x06000E04 RID: 3588
		public abstract Func<T> CreateDefaultConstructor<[Nullable(2)] T>(Type type);

		// Token: 0x06000E05 RID: 3589
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public abstract Func<T, object> CreateGet<[Nullable(2)] T>(PropertyInfo propertyInfo);

		// Token: 0x06000E06 RID: 3590
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public abstract Func<T, object> CreateGet<[Nullable(2)] T>(FieldInfo fieldInfo);

		// Token: 0x06000E07 RID: 3591
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public abstract Action<T, object> CreateSet<[Nullable(2)] T>(FieldInfo fieldInfo);

		// Token: 0x06000E08 RID: 3592
		[return: Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public abstract Action<T, object> CreateSet<[Nullable(2)] T>(PropertyInfo propertyInfo);
	}
}
