using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000194 RID: 404
	[NullableContext(1)]
	[Nullable(0)]
	public class DynamicValueProvider : IValueProvider
	{
		// Token: 0x06000EE8 RID: 3816 RVA: 0x00055EA8 File Offset: 0x000540A8
		public DynamicValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, "memberInfo");
			this._memberInfo = memberInfo;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00055EC4 File Offset: 0x000540C4
		public void SetValue(object target, [Nullable(2)] object value)
		{
			try
			{
				if (this._setter == null)
				{
					this._setter = DynamicReflectionDelegateFactory.Instance.CreateSet<object>(this._memberInfo);
				}
				this._setter(target, value);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException("Error setting value to '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00055F3C File Offset: 0x0005413C
		[return: Nullable(2)]
		public object GetValue(object target)
		{
			object result;
			try
			{
				if (this._getter == null)
				{
					this._getter = DynamicReflectionDelegateFactory.Instance.CreateGet<object>(this._memberInfo);
				}
				result = this._getter(target);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException("Error getting value from '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
			return result;
		}

		// Token: 0x04000796 RID: 1942
		private readonly MemberInfo _memberInfo;

		// Token: 0x04000797 RID: 1943
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		private Func<object, object> _getter;

		// Token: 0x04000798 RID: 1944
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		private Action<object, object> _setter;
	}
}
