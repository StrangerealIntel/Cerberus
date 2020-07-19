using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200018A RID: 394
	[NullableContext(1)]
	[Nullable(0)]
	internal static class TypeExtensions
	{
		// Token: 0x06000E76 RID: 3702 RVA: 0x00053428 File Offset: 0x00051628
		public static MethodInfo Method(this Delegate d)
		{
			return d.Method;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00053430 File Offset: 0x00051630
		public static MemberTypes MemberType(this MemberInfo memberInfo)
		{
			return memberInfo.MemberType;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00053438 File Offset: 0x00051638
		public static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00053440 File Offset: 0x00051640
		public static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00053448 File Offset: 0x00051648
		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00053450 File Offset: 0x00051650
		public static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00053458 File Offset: 0x00051658
		public static Type BaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00053460 File Offset: 0x00051660
		public static Assembly Assembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00053468 File Offset: 0x00051668
		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00053470 File Offset: 0x00051670
		public static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00053478 File Offset: 0x00051678
		public static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00053480 File Offset: 0x00051680
		public static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00053488 File Offset: 0x00051688
		public static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00053490 File Offset: 0x00051690
		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00053498 File Offset: 0x00051698
		public static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x000534A0 File Offset: 0x000516A0
		public static bool AssignableToTypeName(this Type type, string fullTypeName, bool searchInterfaces, [Nullable(2), NotNullWhen(true)] out Type match)
		{
			Type type2 = type;
			while (type2 != null)
			{
				if (string.Equals(type2.FullName, fullTypeName, StringComparison.Ordinal))
				{
					match = type2;
					return true;
				}
				type2 = type2.BaseType();
			}
			if (searchInterfaces)
			{
				Type[] interfaces = type.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					if (string.Equals(interfaces[i].Name, fullTypeName, StringComparison.Ordinal))
					{
						match = type;
						return true;
					}
				}
			}
			match = null;
			return false;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00053520 File Offset: 0x00051720
		public static bool AssignableToTypeName(this Type type, string fullTypeName, bool searchInterfaces)
		{
			Type type2;
			return type.AssignableToTypeName(fullTypeName, searchInterfaces, out type2);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0005353C File Offset: 0x0005173C
		public static bool ImplementInterface(this Type type, Type interfaceType)
		{
			Type type2 = type;
			while (type2 != null)
			{
				foreach (Type type3 in ((IEnumerable<Type>)type2.GetInterfaces()))
				{
					if (type3 == interfaceType || (type3 != null && type3.ImplementInterface(interfaceType)))
					{
						return true;
					}
				}
				type2 = type2.BaseType();
			}
			return false;
		}
	}
}
