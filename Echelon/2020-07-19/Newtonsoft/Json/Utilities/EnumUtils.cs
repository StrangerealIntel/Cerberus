using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000172 RID: 370
	[NullableContext(1)]
	[Nullable(0)]
	internal static class EnumUtils
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x0004EC78 File Offset: 0x0004CE78
		private static EnumInfo InitializeValuesAndNames([Nullable(new byte[]
		{
			0,
			1,
			2
		})] StructMultiKey<Type, NamingStrategy> key)
		{
			Type value = key.Value1;
			string[] names = Enum.GetNames(value);
			string[] array = new string[names.Length];
			ulong[] array2 = new ulong[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				string text = names[i];
				FieldInfo field = value.GetField(text, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				array2[i] = EnumUtils.ToUInt64(field.GetValue(null));
				string text2 = (from EnumMemberAttribute a in field.GetCustomAttributes(typeof(EnumMemberAttribute), true)
				select a.Value).SingleOrDefault<string>();
				bool hasSpecifiedName = text2 != null;
				string text3 = text2 ?? text;
				if (Array.IndexOf<string>(array, text3, 0, i) != -1)
				{
					throw new InvalidOperationException("Enum name '{0}' already exists on enum '{1}'.".FormatWith(CultureInfo.InvariantCulture, text3, value.Name));
				}
				array[i] = ((key.Value2 != null) ? key.Value2.GetPropertyName(text3, hasSpecifiedName) : text3);
			}
			return new EnumInfo(value.IsDefined(typeof(FlagsAttribute), false), array2, names, array);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0004EDA8 File Offset: 0x0004CFA8
		[NullableContext(0)]
		[return: Nullable(new byte[]
		{
			1,
			0
		})]
		public static IList<T> GetFlagsValues<T>(T value) where T : struct
		{
			Type typeFromHandle = typeof(T);
			if (!typeFromHandle.IsDefined(typeof(FlagsAttribute), false))
			{
				throw new ArgumentException("Enum type {0} is not a set of flags.".FormatWith(CultureInfo.InvariantCulture, typeFromHandle));
			}
			Type underlyingType = Enum.GetUnderlyingType(value.GetType());
			ulong num = EnumUtils.ToUInt64(value);
			EnumInfo enumValuesAndNames = EnumUtils.GetEnumValuesAndNames(typeFromHandle);
			IList<T> list = new List<T>();
			for (int i = 0; i < enumValuesAndNames.Values.Length; i++)
			{
				ulong num2 = enumValuesAndNames.Values[i];
				if ((num & num2) == num2 && num2 != 0UL)
				{
					list.Add((T)((object)Convert.ChangeType(num2, underlyingType, CultureInfo.CurrentCulture)));
				}
			}
			if (list.Count == 0)
			{
				if (enumValuesAndNames.Values.Any((ulong v) => v == 0UL))
				{
					list.Add(default(T));
				}
			}
			return list;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0004EEC4 File Offset: 0x0004D0C4
		public static bool TryToString(Type enumType, object value, bool camelCase, [Nullable(2), NotNullWhen(true)] out string name)
		{
			return EnumUtils.TryToString(enumType, value, camelCase ? EnumUtils._camelCaseNamingStrategy : null, out name);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0004EEE0 File Offset: 0x0004D0E0
		public static bool TryToString(Type enumType, object value, [Nullable(2)] NamingStrategy namingStrategy, [Nullable(2), NotNullWhen(true)] out string name)
		{
			EnumInfo enumInfo = EnumUtils.ValuesAndNamesPerEnum.Get(new StructMultiKey<Type, NamingStrategy>(enumType, namingStrategy));
			ulong num = EnumUtils.ToUInt64(value);
			if (enumInfo.IsFlags)
			{
				name = EnumUtils.InternalFlagsFormat(enumInfo, num);
				return name != null;
			}
			int num2 = Array.BinarySearch<ulong>(enumInfo.Values, num);
			if (num2 >= 0)
			{
				name = enumInfo.ResolvedNames[num2];
				return true;
			}
			name = null;
			return false;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0004EF4C File Offset: 0x0004D14C
		[return: Nullable(2)]
		private static string InternalFlagsFormat(EnumInfo entry, ulong result)
		{
			string[] resolvedNames = entry.ResolvedNames;
			ulong[] values = entry.Values;
			int num = values.Length - 1;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			ulong num2 = result;
			while (num >= 0 && (num != 0 || values[num] != 0UL))
			{
				if ((result & values[num]) == values[num])
				{
					result -= values[num];
					if (!flag)
					{
						stringBuilder.Insert(0, ", ");
					}
					string value = resolvedNames[num];
					stringBuilder.Insert(0, value);
					flag = false;
				}
				num--;
			}
			string result2;
			if (result != 0UL)
			{
				result2 = null;
			}
			else if (num2 == 0UL)
			{
				if (values.Length != 0 && values[0] == 0UL)
				{
					result2 = resolvedNames[0];
				}
				else
				{
					result2 = null;
				}
			}
			else
			{
				result2 = stringBuilder.ToString();
			}
			return result2;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0004F01C File Offset: 0x0004D21C
		public static EnumInfo GetEnumValuesAndNames(Type enumType)
		{
			return EnumUtils.ValuesAndNamesPerEnum.Get(new StructMultiKey<Type, NamingStrategy>(enumType, null));
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0004F030 File Offset: 0x0004D230
		private static ulong ToUInt64(object value)
		{
			bool flag;
			switch (ConvertUtils.GetTypeCode(value.GetType(), out flag))
			{
			case PrimitiveTypeCode.Char:
				return (ulong)((char)value);
			case PrimitiveTypeCode.Boolean:
				return (ulong)Convert.ToByte((bool)value);
			case PrimitiveTypeCode.SByte:
				return (ulong)((long)((sbyte)value));
			case PrimitiveTypeCode.Int16:
				return (ulong)((long)((short)value));
			case PrimitiveTypeCode.UInt16:
				return (ulong)((ushort)value);
			case PrimitiveTypeCode.Int32:
				return (ulong)((long)((int)value));
			case PrimitiveTypeCode.Byte:
				return (ulong)((byte)value);
			case PrimitiveTypeCode.UInt32:
				return (ulong)((uint)value);
			case PrimitiveTypeCode.Int64:
				return (ulong)((long)value);
			case PrimitiveTypeCode.UInt64:
				return (ulong)value;
			}
			throw new InvalidOperationException("Unknown enum type.");
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0004F108 File Offset: 0x0004D308
		public static object ParseEnum(Type enumType, [Nullable(2)] NamingStrategy namingStrategy, string value, bool disallowNumber)
		{
			ValidationUtils.ArgumentNotNull(enumType, "enumType");
			ValidationUtils.ArgumentNotNull(value, "value");
			if (!enumType.IsEnum())
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			EnumInfo enumInfo = EnumUtils.ValuesAndNamesPerEnum.Get(new StructMultiKey<Type, NamingStrategy>(enumType, namingStrategy));
			string[] names = enumInfo.Names;
			string[] resolvedNames = enumInfo.ResolvedNames;
			ulong[] values = enumInfo.Values;
			int? num = EnumUtils.FindIndexByName(resolvedNames, value, 0, value.Length, StringComparison.Ordinal);
			if (num != null)
			{
				return Enum.ToObject(enumType, values[num.Value]);
			}
			int num2 = -1;
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsWhiteSpace(value[i]))
				{
					num2 = i;
					break;
				}
			}
			if (num2 == -1)
			{
				throw new ArgumentException("Must specify valid information for parsing in the string.");
			}
			char c = value[num2];
			if (char.IsDigit(c) || c == '-' || c == '+')
			{
				Type underlyingType = Enum.GetUnderlyingType(enumType);
				value = value.Trim();
				object obj = null;
				try
				{
					obj = Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
				}
				catch (FormatException)
				{
				}
				if (obj != null)
				{
					if (disallowNumber)
					{
						throw new FormatException("Integer string '{0}' is not allowed.".FormatWith(CultureInfo.InvariantCulture, value));
					}
					return Enum.ToObject(enumType, obj);
				}
			}
			ulong num3 = 0UL;
			int j = num2;
			while (j <= value.Length)
			{
				int num4 = value.IndexOf(',', j);
				if (num4 == -1)
				{
					num4 = value.Length;
				}
				int num5 = num4;
				while (j < num4)
				{
					if (!char.IsWhiteSpace(value[j]))
					{
						break;
					}
					j++;
				}
				while (num5 > j && char.IsWhiteSpace(value[num5 - 1]))
				{
					num5--;
				}
				int valueSubstringLength = num5 - j;
				num = EnumUtils.MatchName(value, names, resolvedNames, j, valueSubstringLength, StringComparison.Ordinal);
				if (num == null)
				{
					num = EnumUtils.MatchName(value, names, resolvedNames, j, valueSubstringLength, StringComparison.OrdinalIgnoreCase);
				}
				if (num == null)
				{
					num = EnumUtils.FindIndexByName(resolvedNames, value, 0, value.Length, StringComparison.OrdinalIgnoreCase);
					if (num != null)
					{
						return Enum.ToObject(enumType, values[num.Value]);
					}
					throw new ArgumentException("Requested value '{0}' was not found.".FormatWith(CultureInfo.InvariantCulture, value));
				}
				else
				{
					num3 |= values[num.Value];
					j = num4 + 1;
				}
			}
			return Enum.ToObject(enumType, num3);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0004F38C File Offset: 0x0004D58C
		private static int? MatchName(string value, string[] enumNames, string[] resolvedNames, int valueIndex, int valueSubstringLength, StringComparison comparison)
		{
			int? result = EnumUtils.FindIndexByName(resolvedNames, value, valueIndex, valueSubstringLength, comparison);
			if (result == null)
			{
				result = EnumUtils.FindIndexByName(enumNames, value, valueIndex, valueSubstringLength, comparison);
			}
			return result;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0004F3C4 File Offset: 0x0004D5C4
		private static int? FindIndexByName(string[] enumNames, string value, int valueIndex, int valueSubstringLength, StringComparison comparison)
		{
			for (int i = 0; i < enumNames.Length; i++)
			{
				if (enumNames[i].Length == valueSubstringLength && string.Compare(enumNames[i], 0, value, valueIndex, valueSubstringLength, comparison) == 0)
				{
					return new int?(i);
				}
			}
			return null;
		}

		// Token: 0x04000739 RID: 1849
		private const char EnumSeparatorChar = ',';

		// Token: 0x0400073A RID: 1850
		private const string EnumSeparatorString = ", ";

		// Token: 0x0400073B RID: 1851
		[Nullable(new byte[]
		{
			1,
			0,
			1,
			2,
			1
		})]
		private static readonly ThreadSafeStore<StructMultiKey<Type, NamingStrategy>, EnumInfo> ValuesAndNamesPerEnum = new ThreadSafeStore<StructMultiKey<Type, NamingStrategy>, EnumInfo>(new Func<StructMultiKey<Type, NamingStrategy>, EnumInfo>(EnumUtils.InitializeValuesAndNames));

		// Token: 0x0400073C RID: 1852
		private static CamelCaseNamingStrategy _camelCaseNamingStrategy = new CamelCaseNamingStrategy();
	}
}
