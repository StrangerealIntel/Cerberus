using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001BB RID: 443
	[NullableContext(1)]
	[Nullable(0)]
	public class ReflectionValueProvider : IValueProvider
	{
		// Token: 0x060010B7 RID: 4279 RVA: 0x0005EDF0 File Offset: 0x0005CFF0
		public ReflectionValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, "memberInfo");
			this._memberInfo = memberInfo;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0005EE0C File Offset: 0x0005D00C
		public void SetValue(object target, [Nullable(2)] object value)
		{
			try
			{
				ReflectionUtils.SetMemberValue(this._memberInfo, target, value);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException("Error setting value to '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0005EE64 File Offset: 0x0005D064
		[return: Nullable(2)]
		public object GetValue(object target)
		{
			object memberValue;
			try
			{
				PropertyInfo propertyInfo = this._memberInfo as PropertyInfo;
				if (propertyInfo != null && propertyInfo.PropertyType.IsByRef)
				{
					throw new InvalidOperationException("Could not create getter for {0}. ByRef return values are not supported.".FormatWith(CultureInfo.InvariantCulture, propertyInfo));
				}
				memberValue = ReflectionUtils.GetMemberValue(this._memberInfo, target);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException("Error getting value from '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), innerException);
			}
			return memberValue;
		}

		// Token: 0x0400083C RID: 2108
		private readonly MemberInfo _memberInfo;
	}
}
