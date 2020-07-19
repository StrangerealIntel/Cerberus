using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200016D RID: 365
	[NullableContext(1)]
	[Nullable(0)]
	internal static class DynamicUtils
	{
		// Token: 0x06000D89 RID: 3465 RVA: 0x0004EB24 File Offset: 0x0004CD24
		public static IEnumerable<string> GetDynamicMemberNames(this IDynamicMetaObjectProvider dynamicProvider)
		{
			return dynamicProvider.GetMetaObject(Expression.Constant(dynamicProvider)).GetDynamicMemberNames();
		}

		// Token: 0x020002CB RID: 715
		[Nullable(0)]
		internal static class BinderWrapper
		{
			// Token: 0x060017C9 RID: 6089 RVA: 0x000788E4 File Offset: 0x00076AE4
			private static void Init()
			{
				if (!DynamicUtils.BinderWrapper._init)
				{
					if (Type.GetType("Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", false) == null)
					{
						throw new InvalidOperationException("Could not resolve type '{0}'. You may need to add a reference to Microsoft.CSharp.dll to work with dynamic types.".FormatWith(CultureInfo.InvariantCulture, "Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
					}
					DynamicUtils.BinderWrapper._getCSharpArgumentInfoArray = DynamicUtils.BinderWrapper.CreateSharpArgumentInfoArray(new int[1]);
					DynamicUtils.BinderWrapper._setCSharpArgumentInfoArray = DynamicUtils.BinderWrapper.CreateSharpArgumentInfoArray(new int[]
					{
						0,
						3
					});
					DynamicUtils.BinderWrapper.CreateMemberCalls();
					DynamicUtils.BinderWrapper._init = true;
				}
			}

			// Token: 0x060017CA RID: 6090 RVA: 0x00078960 File Offset: 0x00076B60
			private static object CreateSharpArgumentInfoArray(params int[] values)
			{
				Type type = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				Type type2 = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				Array array = Array.CreateInstance(type, values.Length);
				for (int i = 0; i < values.Length; i++)
				{
					MethodBase method = type.GetMethod("Create", new Type[]
					{
						type2,
						typeof(string)
					});
					object obj = null;
					object[] array2 = new object[2];
					array2[0] = 0;
					object value = method.Invoke(obj, array2);
					array.SetValue(value, i);
				}
				return array;
			}

			// Token: 0x060017CB RID: 6091 RVA: 0x000789E8 File Offset: 0x00076BE8
			private static void CreateMemberCalls()
			{
				Type type = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
				Type type2 = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
				Type type3 = Type.GetType("Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
				Type type4 = typeof(IEnumerable<>).MakeGenericType(new Type[]
				{
					type
				});
				MethodInfo method = type3.GetMethod("GetMember", new Type[]
				{
					type2,
					typeof(string),
					typeof(Type),
					type4
				});
				DynamicUtils.BinderWrapper._getMemberCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
				MethodInfo method2 = type3.GetMethod("SetMember", new Type[]
				{
					type2,
					typeof(string),
					typeof(Type),
					type4
				});
				DynamicUtils.BinderWrapper._setMemberCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method2);
			}

			// Token: 0x060017CC RID: 6092 RVA: 0x00078AC4 File Offset: 0x00076CC4
			public static CallSiteBinder GetMember(string name, Type context)
			{
				DynamicUtils.BinderWrapper.Init();
				return (CallSiteBinder)DynamicUtils.BinderWrapper._getMemberCall(null, new object[]
				{
					0,
					name,
					context,
					DynamicUtils.BinderWrapper._getCSharpArgumentInfoArray
				});
			}

			// Token: 0x060017CD RID: 6093 RVA: 0x00078AFC File Offset: 0x00076CFC
			public static CallSiteBinder SetMember(string name, Type context)
			{
				DynamicUtils.BinderWrapper.Init();
				return (CallSiteBinder)DynamicUtils.BinderWrapper._setMemberCall(null, new object[]
				{
					0,
					name,
					context,
					DynamicUtils.BinderWrapper._setCSharpArgumentInfoArray
				});
			}

			// Token: 0x04000BFF RID: 3071
			public const string CSharpAssemblyName = "Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x04000C00 RID: 3072
			private const string BinderTypeName = "Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x04000C01 RID: 3073
			private const string CSharpArgumentInfoTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x04000C02 RID: 3074
			private const string CSharpArgumentInfoFlagsTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x04000C03 RID: 3075
			private const string CSharpBinderFlagsTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x04000C04 RID: 3076
			[Nullable(2)]
			private static object _getCSharpArgumentInfoArray;

			// Token: 0x04000C05 RID: 3077
			[Nullable(2)]
			private static object _setCSharpArgumentInfoArray;

			// Token: 0x04000C06 RID: 3078
			[Nullable(2)]
			private static MethodCall<object, object> _getMemberCall;

			// Token: 0x04000C07 RID: 3079
			[Nullable(2)]
			private static MethodCall<object, object> _setMemberCall;

			// Token: 0x04000C08 RID: 3080
			private static bool _init;
		}
	}
}
