using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000197 RID: 407
	[NullableContext(1)]
	[Nullable(0)]
	public class ExpressionValueProvider : IValueProvider
	{
		// Token: 0x06000EF7 RID: 3831 RVA: 0x00056050 File Offset: 0x00054250
		public ExpressionValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, "memberInfo");
			this._memberInfo = memberInfo;
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0005606C File Offset: 0x0005426C
		public void SetValue(object target, [Nullable(2)] object value)
		{
			try
			{
				if (this._setter == null)
				{
					this._setter = ExpressionReflectionDelegateFactory.Instance.CreateSet<object>(this._memberInfo);
				}
				this._setter(target, value);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException("Error setting value to '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000560E4 File Offset: 0x000542E4
		[return: Nullable(2)]
		public object GetValue(object target)
		{
			object result;
			try
			{
				if (this._getter == null)
				{
					this._getter = ExpressionReflectionDelegateFactory.Instance.CreateGet<object>(this._memberInfo);
				}
				result = this._getter(target);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException("Error getting value from '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
			return result;
		}

		// Token: 0x040007A1 RID: 1953
		private readonly MemberInfo _memberInfo;

		// Token: 0x040007A2 RID: 1954
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		private Func<object, object> _getter;

		// Token: 0x040007A3 RID: 1955
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		private Action<object, object> _setter;
	}
}
