using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001E4 RID: 484
	[NullableContext(1)]
	[Nullable(0)]
	public class JValue : JToken, IEquatable<JValue>, IFormattable, IComparable, IComparable<JValue>, IConvertible
	{
		// Token: 0x060013FE RID: 5118 RVA: 0x00069B84 File Offset: 0x00067D84
		[NullableContext(2)]
		internal JValue(object value, JTokenType type)
		{
			this._value = value;
			this._valueType = type;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x00069B9C File Offset: 0x00067D9C
		public JValue(JValue other) : this(other.Value, other.Type)
		{
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00069BB0 File Offset: 0x00067DB0
		public JValue(long value) : this(value, JTokenType.Integer)
		{
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00069BC0 File Offset: 0x00067DC0
		public JValue(decimal value) : this(value, JTokenType.Float)
		{
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00069BD0 File Offset: 0x00067DD0
		public JValue(char value) : this(value, JTokenType.String)
		{
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00069BE0 File Offset: 0x00067DE0
		[CLSCompliant(false)]
		public JValue(ulong value) : this(value, JTokenType.Integer)
		{
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00069BF0 File Offset: 0x00067DF0
		public JValue(double value) : this(value, JTokenType.Float)
		{
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00069C00 File Offset: 0x00067E00
		public JValue(float value) : this(value, JTokenType.Float)
		{
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00069C10 File Offset: 0x00067E10
		public JValue(DateTime value) : this(value, JTokenType.Date)
		{
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00069C20 File Offset: 0x00067E20
		public JValue(DateTimeOffset value) : this(value, JTokenType.Date)
		{
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00069C30 File Offset: 0x00067E30
		public JValue(bool value) : this(value, JTokenType.Boolean)
		{
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00069C40 File Offset: 0x00067E40
		[NullableContext(2)]
		public JValue(string value) : this(value, JTokenType.String)
		{
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00069C4C File Offset: 0x00067E4C
		public JValue(Guid value) : this(value, JTokenType.Guid)
		{
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00069C5C File Offset: 0x00067E5C
		[NullableContext(2)]
		public JValue(Uri value) : this(value, (value != null) ? JTokenType.Uri : JTokenType.Null)
		{
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00069C7C File Offset: 0x00067E7C
		public JValue(TimeSpan value) : this(value, JTokenType.TimeSpan)
		{
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x00069C8C File Offset: 0x00067E8C
		[NullableContext(2)]
		public JValue(object value) : this(value, JValue.GetValueType(null, value))
		{
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00069CB4 File Offset: 0x00067EB4
		internal override bool DeepEquals(JToken node)
		{
			JValue jvalue = node as JValue;
			return jvalue != null && (jvalue == this || JValue.ValuesEquals(this, jvalue));
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00069CE4 File Offset: 0x00067EE4
		public override bool HasValues
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x00069CE8 File Offset: 0x00067EE8
		private static int CompareBigInteger(System.Numerics.BigInteger i1, object i2)
		{
			int num = i1.CompareTo(ConvertUtils.ToBigInteger(i2));
			if (num != 0)
			{
				return num;
			}
			if (i2 is decimal)
			{
				decimal num2 = (decimal)i2;
				return 0m.CompareTo(Math.Abs(num2 - Math.Truncate(num2)));
			}
			if (i2 is double || i2 is float)
			{
				double num3 = Convert.ToDouble(i2, CultureInfo.InvariantCulture);
				return 0.0.CompareTo(Math.Abs(num3 - Math.Truncate(num3)));
			}
			return num;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x00069D84 File Offset: 0x00067F84
		[NullableContext(2)]
		internal static int Compare(JTokenType valueType, object objA, object objB)
		{
			if (objA == objB)
			{
				return 0;
			}
			if (objB == null)
			{
				return 1;
			}
			if (objA == null)
			{
				return -1;
			}
			switch (valueType)
			{
			case JTokenType.Comment:
			case JTokenType.String:
			case JTokenType.Raw:
			{
				string strA = Convert.ToString(objA, CultureInfo.InvariantCulture);
				string strB = Convert.ToString(objB, CultureInfo.InvariantCulture);
				return string.CompareOrdinal(strA, strB);
			}
			case JTokenType.Integer:
				if (objA is System.Numerics.BigInteger)
				{
					System.Numerics.BigInteger i = (System.Numerics.BigInteger)objA;
					return JValue.CompareBigInteger(i, objB);
				}
				if (objB is System.Numerics.BigInteger)
				{
					System.Numerics.BigInteger i2 = (System.Numerics.BigInteger)objB;
					return -JValue.CompareBigInteger(i2, objA);
				}
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				if (objA is float || objB is float || objA is double || objB is double)
				{
					return JValue.CompareFloat(objA, objB);
				}
				return Convert.ToInt64(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToInt64(objB, CultureInfo.InvariantCulture));
			case JTokenType.Float:
				if (objA is System.Numerics.BigInteger)
				{
					System.Numerics.BigInteger i3 = (System.Numerics.BigInteger)objA;
					return JValue.CompareBigInteger(i3, objB);
				}
				if (objB is System.Numerics.BigInteger)
				{
					System.Numerics.BigInteger i4 = (System.Numerics.BigInteger)objB;
					return -JValue.CompareBigInteger(i4, objA);
				}
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				return JValue.CompareFloat(objA, objB);
			case JTokenType.Boolean:
			{
				bool flag = Convert.ToBoolean(objA, CultureInfo.InvariantCulture);
				bool value = Convert.ToBoolean(objB, CultureInfo.InvariantCulture);
				return flag.CompareTo(value);
			}
			case JTokenType.Date:
			{
				if (objA is DateTime)
				{
					DateTime dateTime = (DateTime)objA;
					DateTime value2;
					if (objB is DateTimeOffset)
					{
						value2 = ((DateTimeOffset)objB).DateTime;
					}
					else
					{
						value2 = Convert.ToDateTime(objB, CultureInfo.InvariantCulture);
					}
					return dateTime.CompareTo(value2);
				}
				DateTimeOffset dateTimeOffset = (DateTimeOffset)objA;
				DateTimeOffset other;
				if (objB is DateTimeOffset)
				{
					other = (DateTimeOffset)objB;
				}
				else
				{
					other = new DateTimeOffset(Convert.ToDateTime(objB, CultureInfo.InvariantCulture));
				}
				return dateTimeOffset.CompareTo(other);
			}
			case JTokenType.Bytes:
			{
				byte[] array = objB as byte[];
				if (array == null)
				{
					throw new ArgumentException("Object must be of type byte[].");
				}
				return MiscellaneousUtils.ByteArrayCompare(objA as byte[], array);
			}
			case JTokenType.Guid:
			{
				if (!(objB is Guid))
				{
					throw new ArgumentException("Object must be of type Guid.");
				}
				Guid guid = (Guid)objA;
				Guid value3 = (Guid)objB;
				return guid.CompareTo(value3);
			}
			case JTokenType.Uri:
			{
				Uri uri = objB as Uri;
				if (uri == null)
				{
					throw new ArgumentException("Object must be of type Uri.");
				}
				Uri uri2 = (Uri)objA;
				return Comparer<string>.Default.Compare(uri2.ToString(), uri.ToString());
			}
			case JTokenType.TimeSpan:
			{
				if (!(objB is TimeSpan))
				{
					throw new ArgumentException("Object must be of type TimeSpan.");
				}
				TimeSpan timeSpan = (TimeSpan)objA;
				TimeSpan value4 = (TimeSpan)objB;
				return timeSpan.CompareTo(value4);
			}
			}
			throw MiscellaneousUtils.CreateArgumentOutOfRangeException("valueType", valueType, "Unexpected value type: {0}".FormatWith(CultureInfo.InvariantCulture, valueType));
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0006A108 File Offset: 0x00068308
		private static int CompareFloat(object objA, object objB)
		{
			double d = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
			double num = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
			if (MathUtils.ApproxEquals(d, num))
			{
				return 0;
			}
			return d.CompareTo(num);
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0006A148 File Offset: 0x00068348
		[NullableContext(2)]
		private static bool Operation(ExpressionType operation, object objA, object objB, out object result)
		{
			if ((objA is string || objB is string) && (operation == ExpressionType.Add || operation == ExpressionType.AddAssign))
			{
				result = ((objA != null) ? objA.ToString() : null) + ((objB != null) ? objB.ToString() : null);
				return true;
			}
			if (objA is System.Numerics.BigInteger || objB is System.Numerics.BigInteger)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				System.Numerics.BigInteger bigInteger = ConvertUtils.ToBigInteger(objA);
				System.Numerics.BigInteger bigInteger2 = ConvertUtils.ToBigInteger(objB);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_48F;
							}
							goto IL_120;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_110;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_48F;
						}
						goto IL_100;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_48F;
						}
						goto IL_120;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_110;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_48F;
					}
					goto IL_100;
				}
				result = bigInteger + bigInteger2;
				return true;
				IL_100:
				result = bigInteger - bigInteger2;
				return true;
				IL_110:
				result = bigInteger * bigInteger2;
				return true;
				IL_120:
				result = bigInteger / bigInteger2;
				return true;
			}
			else if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				decimal d = Convert.ToDecimal(objA, CultureInfo.InvariantCulture);
				decimal d2 = Convert.ToDecimal(objB, CultureInfo.InvariantCulture);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_48F;
							}
							goto IL_21F;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_20F;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_48F;
						}
						goto IL_1FF;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_48F;
						}
						goto IL_21F;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_20F;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_48F;
					}
					goto IL_1FF;
				}
				result = d + d2;
				return true;
				IL_1FF:
				result = d - d2;
				return true;
				IL_20F:
				result = d * d2;
				return true;
				IL_21F:
				result = d / d2;
				return true;
			}
			else if (objA is float || objB is float || objA is double || objB is double)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				double num = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
				double num2 = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_48F;
							}
							goto IL_31A;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_30C;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_48F;
						}
						goto IL_2FE;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_48F;
						}
						goto IL_31A;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_30C;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_48F;
					}
					goto IL_2FE;
				}
				result = num + num2;
				return true;
				IL_2FE:
				result = num - num2;
				return true;
				IL_30C:
				result = num * num2;
				return true;
				IL_31A:
				result = num / num2;
				return true;
			}
			else if (objA is int || objA is uint || objA is long || objA is short || objA is ushort || objA is sbyte || objA is byte || objB is int || objB is uint || objB is long || objB is short || objB is ushort || objB is sbyte || objB is byte)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				long num3 = Convert.ToInt64(objA, CultureInfo.InvariantCulture);
				long num4 = Convert.ToInt64(objB, CultureInfo.InvariantCulture);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_48F;
							}
							goto IL_481;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_473;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_48F;
						}
						goto IL_465;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_48F;
						}
						goto IL_481;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_473;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_48F;
					}
					goto IL_465;
				}
				result = num3 + num4;
				return true;
				IL_465:
				result = num3 - num4;
				return true;
				IL_473:
				result = num3 * num4;
				return true;
				IL_481:
				result = num3 / num4;
				return true;
			}
			IL_48F:
			result = null;
			return false;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0006A5EC File Offset: 0x000687EC
		internal override JToken CloneToken()
		{
			return new JValue(this);
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0006A5F4 File Offset: 0x000687F4
		public static JValue CreateComment([Nullable(2)] string value)
		{
			return new JValue(value, JTokenType.Comment);
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0006A600 File Offset: 0x00068800
		public static JValue CreateString([Nullable(2)] string value)
		{
			return new JValue(value, JTokenType.String);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0006A60C File Offset: 0x0006880C
		public static JValue CreateNull()
		{
			return new JValue(null, JTokenType.Null);
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0006A618 File Offset: 0x00068818
		public static JValue CreateUndefined()
		{
			return new JValue(null, JTokenType.Undefined);
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0006A624 File Offset: 0x00068824
		[NullableContext(2)]
		private static JTokenType GetValueType(JTokenType? current, object value)
		{
			if (value == null)
			{
				return JTokenType.Null;
			}
			if (value == DBNull.Value)
			{
				return JTokenType.Null;
			}
			if (value is string)
			{
				return JValue.GetStringValueType(current);
			}
			if (value is long || value is int || value is short || value is sbyte || value is ulong || value is uint || value is ushort || value is byte)
			{
				return JTokenType.Integer;
			}
			if (value is Enum)
			{
				return JTokenType.Integer;
			}
			if (value is System.Numerics.BigInteger)
			{
				return JTokenType.Integer;
			}
			if (value is double || value is float || value is decimal)
			{
				return JTokenType.Float;
			}
			if (value is DateTime)
			{
				return JTokenType.Date;
			}
			if (value is DateTimeOffset)
			{
				return JTokenType.Date;
			}
			if (value is byte[])
			{
				return JTokenType.Bytes;
			}
			if (value is bool)
			{
				return JTokenType.Boolean;
			}
			if (value is Guid)
			{
				return JTokenType.Guid;
			}
			if (value is Uri)
			{
				return JTokenType.Uri;
			}
			if (value is TimeSpan)
			{
				return JTokenType.TimeSpan;
			}
			throw new ArgumentException("Could not determine JSON object type for type {0}.".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0006A774 File Offset: 0x00068974
		private static JTokenType GetStringValueType(JTokenType? current)
		{
			if (current == null)
			{
				return JTokenType.String;
			}
			JTokenType valueOrDefault = current.GetValueOrDefault();
			if (valueOrDefault == JTokenType.Comment || valueOrDefault == JTokenType.String || valueOrDefault == JTokenType.Raw)
			{
				return current.GetValueOrDefault();
			}
			return JTokenType.String;
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0006A7BC File Offset: 0x000689BC
		public override JTokenType Type
		{
			get
			{
				return this._valueType;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0006A7C4 File Offset: 0x000689C4
		// (set) Token: 0x0600141D RID: 5149 RVA: 0x0006A7CC File Offset: 0x000689CC
		[Nullable(2)]
		public new object Value
		{
			[NullableContext(2)]
			get
			{
				return this._value;
			}
			[NullableContext(2)]
			set
			{
				object value2 = this._value;
				Type left = (value2 != null) ? value2.GetType() : null;
				Type right = (value != null) ? value.GetType() : null;
				if (left != right)
				{
					this._valueType = JValue.GetValueType(new JTokenType?(this._valueType), value);
				}
				this._value = value;
			}
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0006A834 File Offset: 0x00068A34
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			if (converters != null && converters.Length != 0 && this._value != null)
			{
				JsonConverter matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType());
				if (matchingConverter != null && matchingConverter.CanWrite)
				{
					matchingConverter.WriteJson(writer, this._value, JsonSerializer.CreateDefault());
					return;
				}
			}
			switch (this._valueType)
			{
			case JTokenType.Comment:
			{
				object value = this._value;
				writer.WriteComment((value != null) ? value.ToString() : null);
				return;
			}
			case JTokenType.Integer:
			{
				object value2 = this._value;
				if (value2 is int)
				{
					int value3 = (int)value2;
					writer.WriteValue(value3);
					return;
				}
				value2 = this._value;
				if (value2 is long)
				{
					long value4 = (long)value2;
					writer.WriteValue(value4);
					return;
				}
				value2 = this._value;
				if (value2 is ulong)
				{
					ulong value5 = (ulong)value2;
					writer.WriteValue(value5);
					return;
				}
				value2 = this._value;
				if (value2 is System.Numerics.BigInteger)
				{
					System.Numerics.BigInteger bigInteger = (System.Numerics.BigInteger)value2;
					writer.WriteValue(bigInteger);
					return;
				}
				writer.WriteValue(Convert.ToInt64(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.Float:
			{
				object value2 = this._value;
				if (value2 is decimal)
				{
					decimal value6 = (decimal)value2;
					writer.WriteValue(value6);
					return;
				}
				value2 = this._value;
				if (value2 is double)
				{
					double value7 = (double)value2;
					writer.WriteValue(value7);
					return;
				}
				value2 = this._value;
				if (value2 is float)
				{
					float value8 = (float)value2;
					writer.WriteValue(value8);
					return;
				}
				writer.WriteValue(Convert.ToDouble(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.String:
			{
				object value9 = this._value;
				writer.WriteValue((value9 != null) ? value9.ToString() : null);
				return;
			}
			case JTokenType.Boolean:
				writer.WriteValue(Convert.ToBoolean(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.Null:
				writer.WriteNull();
				return;
			case JTokenType.Undefined:
				writer.WriteUndefined();
				return;
			case JTokenType.Date:
			{
				object value2 = this._value;
				if (value2 is DateTimeOffset)
				{
					DateTimeOffset value10 = (DateTimeOffset)value2;
					writer.WriteValue(value10);
					return;
				}
				writer.WriteValue(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.Raw:
			{
				object value11 = this._value;
				writer.WriteRawValue((value11 != null) ? value11.ToString() : null);
				return;
			}
			case JTokenType.Bytes:
				writer.WriteValue((byte[])this._value);
				return;
			case JTokenType.Guid:
				writer.WriteValue((this._value != null) ? ((Guid?)this._value) : null);
				return;
			case JTokenType.Uri:
				writer.WriteValue((Uri)this._value);
				return;
			case JTokenType.TimeSpan:
				writer.WriteValue((this._value != null) ? ((TimeSpan?)this._value) : null);
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", this._valueType, "Unexpected token type.");
			}
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0006AB54 File Offset: 0x00068D54
		internal override int GetDeepHashCode()
		{
			int num = (this._value != null) ? this._value.GetHashCode() : 0;
			int valueType = (int)this._valueType;
			return valueType.GetHashCode() ^ num;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0006AB94 File Offset: 0x00068D94
		private static bool ValuesEquals(JValue v1, JValue v2)
		{
			return v1 == v2 || (v1._valueType == v2._valueType && JValue.Compare(v1._valueType, v1._value, v2._value) == 0);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0006ABCC File Offset: 0x00068DCC
		public bool Equals([AllowNull] JValue other)
		{
			return other != null && JValue.ValuesEquals(this, other);
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0006ABE0 File Offset: 0x00068DE0
		public override bool Equals(object obj)
		{
			JValue jvalue = obj as JValue;
			return jvalue != null && this.Equals(jvalue);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0006AC08 File Offset: 0x00068E08
		public override int GetHashCode()
		{
			if (this._value == null)
			{
				return 0;
			}
			return this._value.GetHashCode();
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0006AC24 File Offset: 0x00068E24
		public override string ToString()
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			return this._value.ToString();
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0006AC44 File Offset: 0x00068E44
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0006AC54 File Offset: 0x00068E54
		public string ToString(IFormatProvider formatProvider)
		{
			return this.ToString(null, formatProvider);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0006AC60 File Offset: 0x00068E60
		public string ToString([Nullable(2)] string format, IFormatProvider formatProvider)
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			IFormattable formattable = this._value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(format, formatProvider);
			}
			return this._value.ToString();
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0006ACA8 File Offset: 0x00068EA8
		protected override DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JValue>(parameter, this, new JValue.JValueDynamicProxy());
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0006ACB8 File Offset: 0x00068EB8
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			JValue jvalue = obj as JValue;
			object objB;
			JTokenType valueType;
			if (jvalue != null)
			{
				objB = jvalue.Value;
				valueType = ((this._valueType == JTokenType.String && this._valueType != jvalue._valueType) ? jvalue._valueType : this._valueType);
			}
			else
			{
				objB = obj;
				valueType = this._valueType;
			}
			return JValue.Compare(valueType, this._value, objB);
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0006AD30 File Offset: 0x00068F30
		public int CompareTo(JValue obj)
		{
			if (obj == null)
			{
				return 1;
			}
			return JValue.Compare((this._valueType == JTokenType.String && this._valueType != obj._valueType) ? obj._valueType : this._valueType, this._value, obj._value);
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0006AD88 File Offset: 0x00068F88
		TypeCode IConvertible.GetTypeCode()
		{
			if (this._value == null)
			{
				return TypeCode.Empty;
			}
			IConvertible convertible = this._value as IConvertible;
			if (convertible != null)
			{
				return convertible.GetTypeCode();
			}
			return TypeCode.Object;
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0006ADC0 File Offset: 0x00068FC0
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return (bool)this;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0006ADC8 File Offset: 0x00068FC8
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return (char)this;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0006ADD0 File Offset: 0x00068FD0
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return (sbyte)this;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0006ADD8 File Offset: 0x00068FD8
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return (byte)this;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0006ADE0 File Offset: 0x00068FE0
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return (short)this;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0006ADE8 File Offset: 0x00068FE8
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return (ushort)this;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0006ADF0 File Offset: 0x00068FF0
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return (int)this;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0006ADF8 File Offset: 0x00068FF8
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return (uint)this;
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0006AE00 File Offset: 0x00069000
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return (long)this;
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0006AE08 File Offset: 0x00069008
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return (ulong)this;
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0006AE10 File Offset: 0x00069010
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return (float)this;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0006AE1C File Offset: 0x0006901C
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return (double)this;
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0006AE28 File Offset: 0x00069028
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return (decimal)this;
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0006AE30 File Offset: 0x00069030
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return (DateTime)this;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0006AE38 File Offset: 0x00069038
		[return: Nullable(2)]
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return base.ToObject(conversionType);
		}

		// Token: 0x04000912 RID: 2322
		private JTokenType _valueType;

		// Token: 0x04000913 RID: 2323
		[Nullable(2)]
		private object _value;

		// Token: 0x02000316 RID: 790
		[Nullable(new byte[]
		{
			0,
			1
		})]
		private class JValueDynamicProxy : DynamicProxy<JValue>
		{
			// Token: 0x060018D1 RID: 6353 RVA: 0x0007AA68 File Offset: 0x00078C68
			public override bool TryConvert(JValue instance, ConvertBinder binder, [Nullable(2), NotNullWhen(true)] out object result)
			{
				if (binder.Type == typeof(JValue) || binder.Type == typeof(JToken))
				{
					result = instance;
					return true;
				}
				object value = instance.Value;
				if (value == null)
				{
					result = null;
					return ReflectionUtils.IsNullable(binder.Type);
				}
				result = ConvertUtils.Convert(value, CultureInfo.InvariantCulture, binder.Type);
				return true;
			}

			// Token: 0x060018D2 RID: 6354 RVA: 0x0007AAE4 File Offset: 0x00078CE4
			public override bool TryBinaryOperation(JValue instance, BinaryOperationBinder binder, object arg, [Nullable(2), NotNullWhen(true)] out object result)
			{
				JValue jvalue = arg as JValue;
				object objB = (jvalue != null) ? jvalue.Value : arg;
				ExpressionType operation = binder.Operation;
				if (operation <= ExpressionType.NotEqual)
				{
					if (operation <= ExpressionType.LessThanOrEqual)
					{
						if (operation != ExpressionType.Add)
						{
							switch (operation)
							{
							case ExpressionType.Divide:
								break;
							case ExpressionType.Equal:
								result = (JValue.Compare(instance.Type, instance.Value, objB) == 0);
								return true;
							case ExpressionType.ExclusiveOr:
							case ExpressionType.Invoke:
							case ExpressionType.Lambda:
							case ExpressionType.LeftShift:
								goto IL_1A2;
							case ExpressionType.GreaterThan:
								result = (JValue.Compare(instance.Type, instance.Value, objB) > 0);
								return true;
							case ExpressionType.GreaterThanOrEqual:
								result = (JValue.Compare(instance.Type, instance.Value, objB) >= 0);
								return true;
							case ExpressionType.LessThan:
								result = (JValue.Compare(instance.Type, instance.Value, objB) < 0);
								return true;
							case ExpressionType.LessThanOrEqual:
								result = (JValue.Compare(instance.Type, instance.Value, objB) <= 0);
								return true;
							default:
								goto IL_1A2;
							}
						}
					}
					else if (operation != ExpressionType.Multiply)
					{
						if (operation != ExpressionType.NotEqual)
						{
							goto IL_1A2;
						}
						result = (JValue.Compare(instance.Type, instance.Value, objB) != 0);
						return true;
					}
				}
				else if (operation <= ExpressionType.AddAssign)
				{
					if (operation != ExpressionType.Subtract && operation != ExpressionType.AddAssign)
					{
						goto IL_1A2;
					}
				}
				else if (operation != ExpressionType.DivideAssign && operation != ExpressionType.MultiplyAssign && operation != ExpressionType.SubtractAssign)
				{
					goto IL_1A2;
				}
				if (JValue.Operation(binder.Operation, instance.Value, objB, out result))
				{
					result = new JValue(result);
					return true;
				}
				IL_1A2:
				result = null;
				return false;
			}
		}
	}
}
