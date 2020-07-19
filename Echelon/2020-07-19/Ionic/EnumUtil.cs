using System;
using System.ComponentModel;
using System.Reflection;

namespace Ionic
{
	// Token: 0x0200009C RID: 156
	internal sealed class EnumUtil
	{
		// Token: 0x06000328 RID: 808 RVA: 0x00017E60 File Offset: 0x00016060
		private EnumUtil()
		{
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00017E68 File Offset: 0x00016068
		internal static string GetDescription(Enum value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (array.Length > 0)
			{
				return array[0].Description;
			}
			return value.ToString();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00017EC0 File Offset: 0x000160C0
		internal static object Parse(Type enumType, string stringRepresentation)
		{
			return EnumUtil.Parse(enumType, stringRepresentation, false);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00017ECC File Offset: 0x000160CC
		internal static object Parse(Type enumType, string stringRepresentation, bool ignoreCase)
		{
			if (ignoreCase)
			{
				stringRepresentation = stringRepresentation.ToLower();
			}
			foreach (object obj in Enum.GetValues(enumType))
			{
				Enum @enum = (Enum)obj;
				string text = EnumUtil.GetDescription(@enum);
				if (ignoreCase)
				{
					text = text.ToLower();
				}
				if (text == stringRepresentation)
				{
					return @enum;
				}
			}
			return Enum.Parse(enumType, stringRepresentation, ignoreCase);
		}
	}
}
