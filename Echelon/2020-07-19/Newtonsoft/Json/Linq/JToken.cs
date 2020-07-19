using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq.JsonPath;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001DF RID: 479
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JToken : IJEnumerable<JToken>, IEnumerable<JToken>, IEnumerable, IJsonLineInfo, ICloneable, IDynamicMetaObjectProvider
	{
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x000666B0 File Offset: 0x000648B0
		public static JTokenEqualityComparer EqualityComparer
		{
			get
			{
				if (JToken._equalityComparer == null)
				{
					JToken._equalityComparer = new JTokenEqualityComparer();
				}
				return JToken._equalityComparer;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x000666CC File Offset: 0x000648CC
		// (set) Token: 0x0600132E RID: 4910 RVA: 0x000666D4 File Offset: 0x000648D4
		[Nullable(2)]
		public JContainer Parent
		{
			[NullableContext(2)]
			[DebuggerStepThrough]
			get
			{
				return this._parent;
			}
			[NullableContext(2)]
			internal set
			{
				this._parent = value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x000666E0 File Offset: 0x000648E0
		public JToken Root
		{
			get
			{
				JContainer parent = this.Parent;
				if (parent == null)
				{
					return this;
				}
				while (parent.Parent != null)
				{
					parent = parent.Parent;
				}
				return parent;
			}
		}

		// Token: 0x06001330 RID: 4912
		internal abstract JToken CloneToken();

		// Token: 0x06001331 RID: 4913
		internal abstract bool DeepEquals(JToken node);

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001332 RID: 4914
		public abstract JTokenType Type { get; }

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001333 RID: 4915
		public abstract bool HasValues { get; }

		// Token: 0x06001334 RID: 4916 RVA: 0x00066710 File Offset: 0x00064910
		[NullableContext(2)]
		public static bool DeepEquals(JToken t1, JToken t2)
		{
			return t1 == t2 || (t1 != null && t2 != null && t1.DeepEquals(t2));
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x00066730 File Offset: 0x00064930
		// (set) Token: 0x06001336 RID: 4918 RVA: 0x00066738 File Offset: 0x00064938
		[Nullable(2)]
		public JToken Next
		{
			[NullableContext(2)]
			get
			{
				return this._next;
			}
			[NullableContext(2)]
			internal set
			{
				this._next = value;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x00066744 File Offset: 0x00064944
		// (set) Token: 0x06001338 RID: 4920 RVA: 0x0006674C File Offset: 0x0006494C
		[Nullable(2)]
		public JToken Previous
		{
			[NullableContext(2)]
			get
			{
				return this._previous;
			}
			[NullableContext(2)]
			internal set
			{
				this._previous = value;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x00066758 File Offset: 0x00064958
		public string Path
		{
			get
			{
				if (this.Parent == null)
				{
					return string.Empty;
				}
				List<JsonPosition> list = new List<JsonPosition>();
				JToken jtoken = null;
				for (JToken jtoken2 = this; jtoken2 != null; jtoken2 = jtoken2.Parent)
				{
					JTokenType type = jtoken2.Type;
					if (type - JTokenType.Array > 1)
					{
						if (type == JTokenType.Property)
						{
							JProperty jproperty = (JProperty)jtoken2;
							List<JsonPosition> list2 = list;
							JsonPosition item = new JsonPosition(JsonContainerType.Object)
							{
								PropertyName = jproperty.Name
							};
							list2.Add(item);
						}
					}
					else if (jtoken != null)
					{
						int position = ((IList<JToken>)jtoken2).IndexOf(jtoken);
						List<JsonPosition> list3 = list;
						JsonPosition item = new JsonPosition(JsonContainerType.Array)
						{
							Position = position
						};
						list3.Add(item);
					}
					jtoken = jtoken2;
				}
				list.FastReverse<JsonPosition>();
				return JsonPosition.BuildPath(list, null);
			}
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0006681C File Offset: 0x00064A1C
		internal JToken()
		{
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00066824 File Offset: 0x00064A24
		[NullableContext(2)]
		public void AddAfterSelf(object content)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			int num = this._parent.IndexOfItem(this);
			this._parent.AddInternal(num + 1, content, false);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00066868 File Offset: 0x00064A68
		[NullableContext(2)]
		public void AddBeforeSelf(object content)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			int index = this._parent.IndexOfItem(this);
			this._parent.AddInternal(index, content, false);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000668AC File Offset: 0x00064AAC
		public IEnumerable<JToken> Ancestors()
		{
			return this.GetAncestors(false);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000668B8 File Offset: 0x00064AB8
		public IEnumerable<JToken> AncestorsAndSelf()
		{
			return this.GetAncestors(true);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000668C4 File Offset: 0x00064AC4
		internal IEnumerable<JToken> GetAncestors(bool self)
		{
			JToken current;
			for (current = (self ? this : this.Parent); current != null; current = current.Parent)
			{
				yield return current;
			}
			current = null;
			yield break;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000668DC File Offset: 0x00064ADC
		public IEnumerable<JToken> AfterSelf()
		{
			if (this.Parent == null)
			{
				yield break;
			}
			JToken o;
			for (o = this.Next; o != null; o = o.Next)
			{
				yield return o;
			}
			o = null;
			yield break;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000668EC File Offset: 0x00064AEC
		public IEnumerable<JToken> BeforeSelf()
		{
			if (this.Parent == null)
			{
				yield break;
			}
			JToken o = this.Parent.First;
			while (o != this && o != null)
			{
				yield return o;
				o = o.Next;
			}
			o = null;
			yield break;
		}

		// Token: 0x17000423 RID: 1059
		[Nullable(2)]
		public virtual JToken this[object key]
		{
			[return: Nullable(2)]
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
			[param: Nullable(2)]
			set
			{
				throw new InvalidOperationException("Cannot set child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00066934 File Offset: 0x00064B34
		public virtual T Value<[Nullable(2)] T>(object key)
		{
			JToken jtoken = this[key];
			if (jtoken != null)
			{
				return jtoken.Convert<JToken, T>();
			}
			return default(T);
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x00066964 File Offset: 0x00064B64
		[Nullable(2)]
		public virtual JToken First
		{
			[NullableContext(2)]
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00066980 File Offset: 0x00064B80
		[Nullable(2)]
		public virtual JToken Last
		{
			[NullableContext(2)]
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0006699C File Offset: 0x00064B9C
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public virtual JEnumerable<JToken> Children()
		{
			return JEnumerable<JToken>.Empty;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000669A4 File Offset: 0x00064BA4
		[NullableContext(0)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public JEnumerable<T> Children<T>() where T : JToken
		{
			return new JEnumerable<T>(this.Children().OfType<T>());
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x000669BC File Offset: 0x00064BBC
		public virtual IEnumerable<T> Values<[Nullable(2)] T>()
		{
			throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x000669D8 File Offset: 0x00064BD8
		public void Remove()
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			this._parent.RemoveItem(this);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00066A00 File Offset: 0x00064C00
		public void Replace(JToken value)
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			this._parent.ReplaceItem(this, value);
		}

		// Token: 0x0600134C RID: 4940
		public abstract void WriteTo(JsonWriter writer, params JsonConverter[] converters);

		// Token: 0x0600134D RID: 4941 RVA: 0x00066A28 File Offset: 0x00064C28
		public override string ToString()
		{
			return this.ToString(Formatting.Indented, new JsonConverter[0]);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00066A38 File Offset: 0x00064C38
		public string ToString(Formatting formatting, params JsonConverter[] converters)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				this.WriteTo(new JsonTextWriter(stringWriter)
				{
					Formatting = formatting
				}, converters);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00066A90 File Offset: 0x00064C90
		[return: Nullable(2)]
		private static JValue EnsureValue(JToken value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			JProperty jproperty = value as JProperty;
			if (jproperty != null)
			{
				value = jproperty.Value;
			}
			return value as JValue;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00066AD0 File Offset: 0x00064CD0
		private static string GetType(JToken token)
		{
			ValidationUtils.ArgumentNotNull(token, "token");
			JProperty jproperty = token as JProperty;
			if (jproperty != null)
			{
				token = jproperty.Value;
			}
			return token.Type.ToString();
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00066B18 File Offset: 0x00064D18
		private static bool ValidateToken(JToken o, JTokenType[] validTypes, bool nullable)
		{
			return Array.IndexOf<JTokenType>(validTypes, o.Type) != -1 || (nullable && (o.Type == JTokenType.Null || o.Type == JTokenType.Undefined));
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00066B50 File Offset: 0x00064D50
		public static explicit operator bool(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BooleanTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Boolean.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return Convert.ToBoolean((int)value3);
			}
			return Convert.ToBoolean(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00066BD0 File Offset: 0x00064DD0
		public static explicit operator DateTimeOffset(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to DateTimeOffset.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is DateTimeOffset)
			{
				return (DateTimeOffset)value2;
			}
			string text = jvalue.Value as string;
			if (text != null)
			{
				return DateTimeOffset.Parse(text, CultureInfo.InvariantCulture);
			}
			return new DateTimeOffset(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00066C68 File Offset: 0x00064E68
		[NullableContext(2)]
		public static explicit operator bool?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BooleanTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Boolean.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return new bool?(Convert.ToBoolean((int)value3));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new bool?(Convert.ToBoolean(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00066D18 File Offset: 0x00064F18
		public static explicit operator long(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Int64.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return (long)value3;
			}
			return Convert.ToInt64(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00066D94 File Offset: 0x00064F94
		[NullableContext(2)]
		public static explicit operator DateTime?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to DateTime.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is DateTimeOffset)
			{
				return new DateTime?(((DateTimeOffset)value2).DateTime);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new DateTime?(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00066E40 File Offset: 0x00065040
		[NullableContext(2)]
		public static explicit operator DateTimeOffset?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to DateTimeOffset.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			object value2 = jvalue.Value;
			if (value2 is DateTimeOffset)
			{
				DateTimeOffset value3 = (DateTimeOffset)value2;
				return new DateTimeOffset?(value3);
			}
			string text = jvalue.Value as string;
			if (text != null)
			{
				return new DateTimeOffset?(DateTimeOffset.Parse(text, CultureInfo.InvariantCulture));
			}
			return new DateTimeOffset?(new DateTimeOffset(Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture)));
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00066F10 File Offset: 0x00065110
		[NullableContext(2)]
		public static explicit operator decimal?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Decimal.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return new decimal?((decimal)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new decimal?(Convert.ToDecimal(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00066FBC File Offset: 0x000651BC
		[NullableContext(2)]
		public static explicit operator double?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Double.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return new double?((double)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new double?(Convert.ToDouble(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00067068 File Offset: 0x00065268
		[NullableContext(2)]
		public static explicit operator char?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.CharTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Char.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return new char?((char)((ushort)value3));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new char?(Convert.ToChar(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00067114 File Offset: 0x00065314
		public static explicit operator int(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Int32.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return (int)value3;
			}
			return Convert.ToInt32(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00067190 File Offset: 0x00065390
		public static explicit operator short(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Int16.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return (short)value3;
			}
			return Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0006720C File Offset: 0x0006540C
		[CLSCompliant(false)]
		public static explicit operator ushort(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt16.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return (ushort)value3;
			}
			return Convert.ToUInt16(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00067288 File Offset: 0x00065488
		[CLSCompliant(false)]
		public static explicit operator char(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.CharTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Char.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return (char)((ushort)value3);
			}
			return Convert.ToChar(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00067304 File Offset: 0x00065504
		public static explicit operator byte(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Byte.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return (byte)value3;
			}
			return Convert.ToByte(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00067380 File Offset: 0x00065580
		[CLSCompliant(false)]
		public static explicit operator sbyte(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to SByte.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return (sbyte)value3;
			}
			return Convert.ToSByte(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x000673FC File Offset: 0x000655FC
		[NullableContext(2)]
		public static explicit operator int?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Int32.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return new int?((int)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new int?(Convert.ToInt32(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x000674A8 File Offset: 0x000656A8
		[NullableContext(2)]
		public static explicit operator short?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Int16.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return new short?((short)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new short?(Convert.ToInt16(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00067554 File Offset: 0x00065754
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static explicit operator ushort?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt16.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return new ushort?((ushort)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new ushort?(Convert.ToUInt16(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00067600 File Offset: 0x00065800
		[NullableContext(2)]
		public static explicit operator byte?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Byte.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return new byte?((byte)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new byte?(Convert.ToByte(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x000676AC File Offset: 0x000658AC
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static explicit operator sbyte?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to SByte.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return new sbyte?((sbyte)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new sbyte?(Convert.ToSByte(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00067758 File Offset: 0x00065958
		public static explicit operator DateTime(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.DateTimeTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to DateTime.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is DateTimeOffset)
			{
				return ((DateTimeOffset)value2).DateTime;
			}
			return Convert.ToDateTime(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x000677D4 File Offset: 0x000659D4
		[NullableContext(2)]
		public static explicit operator long?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Int64.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return new long?((long)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new long?(Convert.ToInt64(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00067880 File Offset: 0x00065A80
		[NullableContext(2)]
		public static explicit operator float?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Single.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return new float?((float)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new float?(Convert.ToSingle(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0006792C File Offset: 0x00065B2C
		public static explicit operator decimal(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Decimal.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return (decimal)value3;
			}
			return Convert.ToDecimal(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x000679A8 File Offset: 0x00065BA8
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static explicit operator uint?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt32.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return new uint?((uint)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new uint?(Convert.ToUInt32(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00067A54 File Offset: 0x00065C54
		[NullableContext(2)]
		[CLSCompliant(false)]
		public static explicit operator ulong?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to UInt64.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return new ulong?((ulong)value3);
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new ulong?(Convert.ToUInt64(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00067B00 File Offset: 0x00065D00
		public static explicit operator double(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Double.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return (double)value3;
			}
			return Convert.ToDouble(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00067B7C File Offset: 0x00065D7C
		public static explicit operator float(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Single.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return (float)value3;
			}
			return Convert.ToSingle(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00067BF8 File Offset: 0x00065DF8
		[NullableContext(2)]
		public static explicit operator string(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.StringTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to String.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			byte[] array = jvalue.Value as byte[];
			if (array != null)
			{
				return Convert.ToBase64String(array);
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				return ((System.Numerics.BigInteger)value2).ToString(CultureInfo.InvariantCulture);
			}
			return Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00067CA8 File Offset: 0x00065EA8
		[CLSCompliant(false)]
		public static explicit operator uint(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt32.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return (uint)value3;
			}
			return Convert.ToUInt32(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00067D24 File Offset: 0x00065F24
		[CLSCompliant(false)]
		public static explicit operator ulong(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.NumberTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to UInt64.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger value3 = (System.Numerics.BigInteger)value2;
				return (ulong)value3;
			}
			return Convert.ToUInt64(jvalue.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00067DA0 File Offset: 0x00065FA0
		[NullableContext(2)]
		public static explicit operator byte[](JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BytesTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to byte array.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value is string)
			{
				return Convert.FromBase64String(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			object value2 = jvalue.Value;
			if (value2 is System.Numerics.BigInteger)
			{
				return ((System.Numerics.BigInteger)value2).ToByteArray();
			}
			byte[] array = jvalue.Value as byte[];
			if (array != null)
			{
				return array;
			}
			throw new ArgumentException("Can not convert {0} to byte array.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00067E68 File Offset: 0x00066068
		public static explicit operator Guid(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.GuidTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to Guid.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			byte[] array = jvalue.Value as byte[];
			if (array != null)
			{
				return new Guid(array);
			}
			object value2 = jvalue.Value;
			if (value2 is Guid)
			{
				return (Guid)value2;
			}
			return new Guid(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00067F00 File Offset: 0x00066100
		[NullableContext(2)]
		public static explicit operator Guid?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.GuidTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Guid.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			byte[] array = jvalue.Value as byte[];
			if (array != null)
			{
				return new Guid?(new Guid(array));
			}
			object value2 = jvalue.Value;
			Guid value3;
			if (value2 is Guid)
			{
				Guid guid = (Guid)value2;
				value3 = guid;
			}
			else
			{
				value3 = new Guid(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return new Guid?(value3);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00067FD0 File Offset: 0x000661D0
		public static explicit operator TimeSpan(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.TimeSpanTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to TimeSpan.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			object value2 = jvalue.Value;
			if (value2 is TimeSpan)
			{
				return (TimeSpan)value2;
			}
			return ConvertUtils.ParseTimeSpan(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00068050 File Offset: 0x00066250
		[NullableContext(2)]
		public static explicit operator TimeSpan?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.TimeSpanTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to TimeSpan.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			object value2 = jvalue.Value;
			TimeSpan value3;
			if (value2 is TimeSpan)
			{
				TimeSpan timeSpan = (TimeSpan)value2;
				value3 = timeSpan;
			}
			else
			{
				value3 = ConvertUtils.ParseTimeSpan(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return new TimeSpan?(value3);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00068100 File Offset: 0x00066300
		[NullableContext(2)]
		public static explicit operator Uri(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.UriTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to Uri.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			Uri uri = jvalue.Value as Uri;
			if (uri == null)
			{
				return new Uri(Convert.ToString(jvalue.Value, CultureInfo.InvariantCulture));
			}
			return uri;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00068188 File Offset: 0x00066388
		private static System.Numerics.BigInteger ToBigInteger(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BigIntegerTypes, false))
			{
				throw new ArgumentException("Can not convert {0} to BigInteger.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			return ConvertUtils.ToBigInteger(jvalue.Value);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x000681E0 File Offset: 0x000663E0
		private static System.Numerics.BigInteger? ToBigIntegerNullable(JToken value)
		{
			JValue jvalue = JToken.EnsureValue(value);
			if (jvalue == null || !JToken.ValidateToken(jvalue, JToken.BigIntegerTypes, true))
			{
				throw new ArgumentException("Can not convert {0} to BigInteger.".FormatWith(CultureInfo.InvariantCulture, JToken.GetType(value)));
			}
			if (jvalue.Value == null)
			{
				return null;
			}
			return new System.Numerics.BigInteger?(ConvertUtils.ToBigInteger(jvalue.Value));
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00068250 File Offset: 0x00066450
		public static implicit operator JToken(bool value)
		{
			return new JValue(value);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00068258 File Offset: 0x00066458
		public static implicit operator JToken(DateTimeOffset value)
		{
			return new JValue(value);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00068260 File Offset: 0x00066460
		public static implicit operator JToken(byte value)
		{
			return new JValue((long)((ulong)value));
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0006826C File Offset: 0x0006646C
		public static implicit operator JToken(byte? value)
		{
			return new JValue(value);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0006827C File Offset: 0x0006647C
		[CLSCompliant(false)]
		public static implicit operator JToken(sbyte value)
		{
			return new JValue((long)value);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00068288 File Offset: 0x00066488
		[CLSCompliant(false)]
		public static implicit operator JToken(sbyte? value)
		{
			return new JValue(value);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00068298 File Offset: 0x00066498
		public static implicit operator JToken(bool? value)
		{
			return new JValue(value);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x000682A8 File Offset: 0x000664A8
		public static implicit operator JToken(long value)
		{
			return new JValue(value);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000682B0 File Offset: 0x000664B0
		public static implicit operator JToken(DateTime? value)
		{
			return new JValue(value);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x000682C0 File Offset: 0x000664C0
		public static implicit operator JToken(DateTimeOffset? value)
		{
			return new JValue(value);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000682D0 File Offset: 0x000664D0
		public static implicit operator JToken(decimal? value)
		{
			return new JValue(value);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000682E0 File Offset: 0x000664E0
		public static implicit operator JToken(double? value)
		{
			return new JValue(value);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x000682F0 File Offset: 0x000664F0
		[CLSCompliant(false)]
		public static implicit operator JToken(short value)
		{
			return new JValue((long)value);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x000682FC File Offset: 0x000664FC
		[CLSCompliant(false)]
		public static implicit operator JToken(ushort value)
		{
			return new JValue((long)((ulong)value));
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00068308 File Offset: 0x00066508
		public static implicit operator JToken(int value)
		{
			return new JValue((long)value);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00068314 File Offset: 0x00066514
		public static implicit operator JToken(int? value)
		{
			return new JValue(value);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00068324 File Offset: 0x00066524
		public static implicit operator JToken(DateTime value)
		{
			return new JValue(value);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0006832C File Offset: 0x0006652C
		public static implicit operator JToken(long? value)
		{
			return new JValue(value);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0006833C File Offset: 0x0006653C
		public static implicit operator JToken(float? value)
		{
			return new JValue(value);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0006834C File Offset: 0x0006654C
		public static implicit operator JToken(decimal value)
		{
			return new JValue(value);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00068354 File Offset: 0x00066554
		[CLSCompliant(false)]
		public static implicit operator JToken(short? value)
		{
			return new JValue(value);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00068364 File Offset: 0x00066564
		[CLSCompliant(false)]
		public static implicit operator JToken(ushort? value)
		{
			return new JValue(value);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00068374 File Offset: 0x00066574
		[CLSCompliant(false)]
		public static implicit operator JToken(uint? value)
		{
			return new JValue(value);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00068384 File Offset: 0x00066584
		[CLSCompliant(false)]
		public static implicit operator JToken(ulong? value)
		{
			return new JValue(value);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00068394 File Offset: 0x00066594
		public static implicit operator JToken(double value)
		{
			return new JValue(value);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0006839C File Offset: 0x0006659C
		public static implicit operator JToken(float value)
		{
			return new JValue(value);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x000683A4 File Offset: 0x000665A4
		public static implicit operator JToken([Nullable(2)] string value)
		{
			return new JValue(value);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x000683AC File Offset: 0x000665AC
		[CLSCompliant(false)]
		public static implicit operator JToken(uint value)
		{
			return new JValue((long)((ulong)value));
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x000683B8 File Offset: 0x000665B8
		[CLSCompliant(false)]
		public static implicit operator JToken(ulong value)
		{
			return new JValue(value);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000683C0 File Offset: 0x000665C0
		public static implicit operator JToken(byte[] value)
		{
			return new JValue(value);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x000683C8 File Offset: 0x000665C8
		public static implicit operator JToken([Nullable(2)] Uri value)
		{
			return new JValue(value);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x000683D0 File Offset: 0x000665D0
		public static implicit operator JToken(TimeSpan value)
		{
			return new JValue(value);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x000683D8 File Offset: 0x000665D8
		public static implicit operator JToken(TimeSpan? value)
		{
			return new JValue(value);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x000683E8 File Offset: 0x000665E8
		public static implicit operator JToken(Guid value)
		{
			return new JValue(value);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x000683F0 File Offset: 0x000665F0
		public static implicit operator JToken(Guid? value)
		{
			return new JValue(value);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00068400 File Offset: 0x00066600
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<JToken>)this).GetEnumerator();
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00068408 File Offset: 0x00066608
		IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		// Token: 0x0600139E RID: 5022
		internal abstract int GetDeepHashCode();

		// Token: 0x17000426 RID: 1062
		IJEnumerable<JToken> IJEnumerable<JToken>.this[object key]
		{
			get
			{
				return this[key];
			}
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00068434 File Offset: 0x00066634
		public JsonReader CreateReader()
		{
			return new JTokenReader(this);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0006843C File Offset: 0x0006663C
		internal static JToken FromObjectInternal(object o, JsonSerializer jsonSerializer)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			ValidationUtils.ArgumentNotNull(jsonSerializer, "jsonSerializer");
			JToken token;
			using (JTokenWriter jtokenWriter = new JTokenWriter())
			{
				jsonSerializer.Serialize(jtokenWriter, o);
				token = jtokenWriter.Token;
			}
			return token;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00068498 File Offset: 0x00066698
		public static JToken FromObject(object o)
		{
			return JToken.FromObjectInternal(o, JsonSerializer.CreateDefault());
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x000684A8 File Offset: 0x000666A8
		public static JToken FromObject(object o, JsonSerializer jsonSerializer)
		{
			return JToken.FromObjectInternal(o, jsonSerializer);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x000684B4 File Offset: 0x000666B4
		[return: MaybeNull]
		public T ToObject<[Nullable(2)] T>()
		{
			return (T)((object)this.ToObject(typeof(T)));
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x000684CC File Offset: 0x000666CC
		[return: Nullable(2)]
		public object ToObject(Type objectType)
		{
			if (JsonConvert.DefaultSettings == null)
			{
				bool flag;
				PrimitiveTypeCode typeCode = ConvertUtils.GetTypeCode(objectType, out flag);
				if (flag)
				{
					if (this.Type == JTokenType.String)
					{
						try
						{
							return this.ToObject(objectType, JsonSerializer.CreateDefault());
						}
						catch (Exception innerException)
						{
							Type type = objectType.IsEnum() ? objectType : Nullable.GetUnderlyingType(objectType);
							throw new ArgumentException("Could not convert '{0}' to {1}.".FormatWith(CultureInfo.InvariantCulture, (string)this, type.Name), innerException);
						}
					}
					if (this.Type == JTokenType.Integer)
					{
						return Enum.ToObject(objectType.IsEnum() ? objectType : Nullable.GetUnderlyingType(objectType), ((JValue)this).Value);
					}
				}
				switch (typeCode)
				{
				case PrimitiveTypeCode.Char:
					return (char)this;
				case PrimitiveTypeCode.CharNullable:
					return (char?)this;
				case PrimitiveTypeCode.Boolean:
					return (bool)this;
				case PrimitiveTypeCode.BooleanNullable:
					return (bool?)this;
				case PrimitiveTypeCode.SByte:
					return (sbyte)this;
				case PrimitiveTypeCode.SByteNullable:
					return (sbyte?)this;
				case PrimitiveTypeCode.Int16:
					return (short)this;
				case PrimitiveTypeCode.Int16Nullable:
					return (short?)this;
				case PrimitiveTypeCode.UInt16:
					return (ushort)this;
				case PrimitiveTypeCode.UInt16Nullable:
					return (ushort?)this;
				case PrimitiveTypeCode.Int32:
					return (int)this;
				case PrimitiveTypeCode.Int32Nullable:
					return (int?)this;
				case PrimitiveTypeCode.Byte:
					return (byte)this;
				case PrimitiveTypeCode.ByteNullable:
					return (byte?)this;
				case PrimitiveTypeCode.UInt32:
					return (uint)this;
				case PrimitiveTypeCode.UInt32Nullable:
					return (uint?)this;
				case PrimitiveTypeCode.Int64:
					return (long)this;
				case PrimitiveTypeCode.Int64Nullable:
					return (long?)this;
				case PrimitiveTypeCode.UInt64:
					return (ulong)this;
				case PrimitiveTypeCode.UInt64Nullable:
					return (ulong?)this;
				case PrimitiveTypeCode.Single:
					return (float)this;
				case PrimitiveTypeCode.SingleNullable:
					return (float?)this;
				case PrimitiveTypeCode.Double:
					return (double)this;
				case PrimitiveTypeCode.DoubleNullable:
					return (double?)this;
				case PrimitiveTypeCode.DateTime:
					return (DateTime)this;
				case PrimitiveTypeCode.DateTimeNullable:
					return (DateTime?)this;
				case PrimitiveTypeCode.DateTimeOffset:
					return (DateTimeOffset)this;
				case PrimitiveTypeCode.DateTimeOffsetNullable:
					return (DateTimeOffset?)this;
				case PrimitiveTypeCode.Decimal:
					return (decimal)this;
				case PrimitiveTypeCode.DecimalNullable:
					return (decimal?)this;
				case PrimitiveTypeCode.Guid:
					return (Guid)this;
				case PrimitiveTypeCode.GuidNullable:
					return (Guid?)this;
				case PrimitiveTypeCode.TimeSpan:
					return (TimeSpan)this;
				case PrimitiveTypeCode.TimeSpanNullable:
					return (TimeSpan?)this;
				case PrimitiveTypeCode.BigInteger:
					return JToken.ToBigInteger(this);
				case PrimitiveTypeCode.BigIntegerNullable:
					return JToken.ToBigIntegerNullable(this);
				case PrimitiveTypeCode.Uri:
					return (Uri)this;
				case PrimitiveTypeCode.String:
					return (string)this;
				}
			}
			return this.ToObject(objectType, JsonSerializer.CreateDefault());
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00068804 File Offset: 0x00066A04
		[return: MaybeNull]
		public T ToObject<[Nullable(2)] T>(JsonSerializer jsonSerializer)
		{
			return (T)((object)this.ToObject(typeof(T), jsonSerializer));
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0006881C File Offset: 0x00066A1C
		[return: Nullable(2)]
		public object ToObject(Type objectType, JsonSerializer jsonSerializer)
		{
			ValidationUtils.ArgumentNotNull(jsonSerializer, "jsonSerializer");
			object result;
			using (JTokenReader jtokenReader = new JTokenReader(this))
			{
				result = jsonSerializer.Deserialize(jtokenReader, objectType);
			}
			return result;
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00068868 File Offset: 0x00066A68
		public static JToken ReadFrom(JsonReader reader)
		{
			return JToken.ReadFrom(reader, null);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00068874 File Offset: 0x00066A74
		public static JToken ReadFrom(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			bool flag;
			if (reader.TokenType == JsonToken.None)
			{
				flag = ((settings != null && settings.CommentHandling == CommentHandling.Ignore) ? reader.ReadAndMoveToContent() : reader.Read());
			}
			else
			{
				flag = (reader.TokenType != JsonToken.Comment || settings == null || settings.CommentHandling != CommentHandling.Ignore || reader.ReadAndMoveToContent());
			}
			if (!flag)
			{
				throw JsonReaderException.Create(reader, "Error reading JToken from JsonReader.");
			}
			IJsonLineInfo lineInfo = reader as IJsonLineInfo;
			switch (reader.TokenType)
			{
			case JsonToken.StartObject:
				return JObject.Load(reader, settings);
			case JsonToken.StartArray:
				return JArray.Load(reader, settings);
			case JsonToken.StartConstructor:
				return JConstructor.Load(reader, settings);
			case JsonToken.PropertyName:
				return JProperty.Load(reader, settings);
			case JsonToken.Comment:
			{
				JValue jvalue = JValue.CreateComment(reader.Value.ToString());
				jvalue.SetLineInfo(lineInfo, settings);
				return jvalue;
			}
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Date:
			case JsonToken.Bytes:
			{
				JValue jvalue2 = new JValue(reader.Value);
				jvalue2.SetLineInfo(lineInfo, settings);
				return jvalue2;
			}
			case JsonToken.Null:
			{
				JValue jvalue3 = JValue.CreateNull();
				jvalue3.SetLineInfo(lineInfo, settings);
				return jvalue3;
			}
			case JsonToken.Undefined:
			{
				JValue jvalue4 = JValue.CreateUndefined();
				jvalue4.SetLineInfo(lineInfo, settings);
				return jvalue4;
			}
			}
			throw JsonReaderException.Create(reader, "Error reading JToken from JsonReader. Unexpected token: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x000689E8 File Offset: 0x00066BE8
		public static JToken Parse(string json)
		{
			return JToken.Parse(json, null);
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x000689F4 File Offset: 0x00066BF4
		public static JToken Parse(string json, [Nullable(2)] JsonLoadSettings settings)
		{
			JToken result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JToken jtoken = JToken.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				result = jtoken;
			}
			return result;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00068A44 File Offset: 0x00066C44
		public static JToken Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			return JToken.ReadFrom(reader, settings);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00068A50 File Offset: 0x00066C50
		public static JToken Load(JsonReader reader)
		{
			return JToken.Load(reader, null);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00068A5C File Offset: 0x00066C5C
		[NullableContext(2)]
		internal void SetLineInfo(IJsonLineInfo lineInfo, JsonLoadSettings settings)
		{
			if (settings != null && settings.LineInfoHandling != LineInfoHandling.Load)
			{
				return;
			}
			if (lineInfo == null || !lineInfo.HasLineInfo())
			{
				return;
			}
			this.SetLineInfo(lineInfo.LineNumber, lineInfo.LinePosition);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00068AA4 File Offset: 0x00066CA4
		internal void SetLineInfo(int lineNumber, int linePosition)
		{
			this.AddAnnotation(new JToken.LineInfoAnnotation(lineNumber, linePosition));
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00068AB4 File Offset: 0x00066CB4
		bool IJsonLineInfo.HasLineInfo()
		{
			return this.Annotation<JToken.LineInfoAnnotation>() != null;
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00068AC0 File Offset: 0x00066CC0
		int IJsonLineInfo.LineNumber
		{
			get
			{
				JToken.LineInfoAnnotation lineInfoAnnotation = this.Annotation<JToken.LineInfoAnnotation>();
				if (lineInfoAnnotation != null)
				{
					return lineInfoAnnotation.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x00068AE8 File Offset: 0x00066CE8
		int IJsonLineInfo.LinePosition
		{
			get
			{
				JToken.LineInfoAnnotation lineInfoAnnotation = this.Annotation<JToken.LineInfoAnnotation>();
				if (lineInfoAnnotation != null)
				{
					return lineInfoAnnotation.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00068B10 File Offset: 0x00066D10
		[return: Nullable(2)]
		public JToken SelectToken(string path)
		{
			return this.SelectToken(path, false);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00068B1C File Offset: 0x00066D1C
		[return: Nullable(2)]
		public JToken SelectToken(string path, bool errorWhenNoMatch)
		{
			JPath jpath = new JPath(path);
			JToken jtoken = null;
			foreach (JToken jtoken2 in jpath.Evaluate(this, this, errorWhenNoMatch))
			{
				if (jtoken != null)
				{
					throw new JsonException("Path returned multiple tokens.");
				}
				jtoken = jtoken2;
			}
			return jtoken;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00068B88 File Offset: 0x00066D88
		public IEnumerable<JToken> SelectTokens(string path)
		{
			return this.SelectTokens(path, false);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00068B94 File Offset: 0x00066D94
		public IEnumerable<JToken> SelectTokens(string path, bool errorWhenNoMatch)
		{
			return new JPath(path).Evaluate(this, this, errorWhenNoMatch);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00068BA4 File Offset: 0x00066DA4
		protected virtual DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JToken>(parameter, this, new DynamicProxy<JToken>());
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00068BB4 File Offset: 0x00066DB4
		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return this.GetMetaObject(parameter);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00068BC0 File Offset: 0x00066DC0
		object ICloneable.Clone()
		{
			return this.DeepClone();
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00068BC8 File Offset: 0x00066DC8
		public JToken DeepClone()
		{
			return this.CloneToken();
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00068BD0 File Offset: 0x00066DD0
		public void AddAnnotation(object annotation)
		{
			if (annotation == null)
			{
				throw new ArgumentNullException("annotation");
			}
			if (this._annotations == null)
			{
				object annotations;
				if (!(annotation is object[]))
				{
					annotations = annotation;
				}
				else
				{
					(annotations = new object[1])[0] = annotation;
				}
				this._annotations = annotations;
				return;
			}
			object[] array = this._annotations as object[];
			if (array == null)
			{
				this._annotations = new object[]
				{
					this._annotations,
					annotation
				};
				return;
			}
			int num = 0;
			while (num < array.Length && array[num] != null)
			{
				num++;
			}
			if (num == array.Length)
			{
				Array.Resize<object>(ref array, num * 2);
				this._annotations = array;
			}
			array[num] = annotation;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00068C84 File Offset: 0x00066E84
		[return: Nullable(2)]
		public T Annotation<T>() where T : class
		{
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					return this._annotations as T;
				}
				foreach (object obj in array)
				{
					if (obj == null)
					{
						break;
					}
					T t = obj as T;
					if (t != null)
					{
						return t;
					}
				}
			}
			return default(T);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00068D04 File Offset: 0x00066F04
		[return: Nullable(2)]
		public object Annotation(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (type.IsInstanceOfType(this._annotations))
					{
						return this._annotations;
					}
				}
				else
				{
					foreach (object obj in array)
					{
						if (obj == null)
						{
							break;
						}
						if (type.IsInstanceOfType(obj))
						{
							return obj;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00068D8C File Offset: 0x00066F8C
		public IEnumerable<T> Annotations<T>() where T : class
		{
			if (this._annotations == null)
			{
				yield break;
			}
			object annotations2 = this._annotations;
			object[] annotations = annotations2 as object[];
			if (annotations != null)
			{
				int num;
				for (int i = 0; i < annotations.Length; i = num + 1)
				{
					object obj = annotations[i];
					if (obj == null)
					{
						break;
					}
					T t = obj as T;
					if (t != null)
					{
						yield return t;
					}
					num = i;
				}
				yield break;
			}
			T t2 = this._annotations as T;
			if (t2 == null)
			{
				yield break;
			}
			yield return t2;
			yield break;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00068D9C File Offset: 0x00066F9C
		public IEnumerable<object> Annotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._annotations == null)
			{
				yield break;
			}
			object annotations2 = this._annotations;
			object[] annotations = annotations2 as object[];
			if (annotations != null)
			{
				int num;
				for (int i = 0; i < annotations.Length; i = num + 1)
				{
					object obj = annotations[i];
					if (obj == null)
					{
						break;
					}
					if (type.IsInstanceOfType(obj))
					{
						yield return obj;
					}
					num = i;
				}
				yield break;
			}
			if (!type.IsInstanceOfType(this._annotations))
			{
				yield break;
			}
			yield return this._annotations;
			yield break;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00068DB4 File Offset: 0x00066FB4
		public void RemoveAnnotations<T>() where T : class
		{
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (this._annotations is T)
					{
						this._annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!(obj is T))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j != 0)
					{
						while (j < i)
						{
							array[j++] = null;
						}
						return;
					}
					this._annotations = null;
				}
			}
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00068E4C File Offset: 0x0006704C
		public void RemoveAnnotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (type.IsInstanceOfType(this._annotations))
					{
						this._annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!type.IsInstanceOfType(obj))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j != 0)
					{
						while (j < i)
						{
							array[j++] = null;
						}
						return;
					}
					this._annotations = null;
				}
			}
		}

		// Token: 0x040008E8 RID: 2280
		[Nullable(2)]
		private static JTokenEqualityComparer _equalityComparer;

		// Token: 0x040008E9 RID: 2281
		[Nullable(2)]
		private JContainer _parent;

		// Token: 0x040008EA RID: 2282
		[Nullable(2)]
		private JToken _previous;

		// Token: 0x040008EB RID: 2283
		[Nullable(2)]
		private JToken _next;

		// Token: 0x040008EC RID: 2284
		[Nullable(2)]
		private object _annotations;

		// Token: 0x040008ED RID: 2285
		private static readonly JTokenType[] BooleanTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Boolean
		};

		// Token: 0x040008EE RID: 2286
		private static readonly JTokenType[] NumberTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Boolean
		};

		// Token: 0x040008EF RID: 2287
		private static readonly JTokenType[] BigIntegerTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Boolean,
			JTokenType.Bytes
		};

		// Token: 0x040008F0 RID: 2288
		private static readonly JTokenType[] StringTypes = new JTokenType[]
		{
			JTokenType.Date,
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Boolean,
			JTokenType.Bytes,
			JTokenType.Guid,
			JTokenType.TimeSpan,
			JTokenType.Uri
		};

		// Token: 0x040008F1 RID: 2289
		private static readonly JTokenType[] GuidTypes = new JTokenType[]
		{
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Guid,
			JTokenType.Bytes
		};

		// Token: 0x040008F2 RID: 2290
		private static readonly JTokenType[] TimeSpanTypes = new JTokenType[]
		{
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.TimeSpan
		};

		// Token: 0x040008F3 RID: 2291
		private static readonly JTokenType[] UriTypes = new JTokenType[]
		{
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Uri
		};

		// Token: 0x040008F4 RID: 2292
		private static readonly JTokenType[] CharTypes = new JTokenType[]
		{
			JTokenType.Integer,
			JTokenType.Float,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw
		};

		// Token: 0x040008F5 RID: 2293
		private static readonly JTokenType[] DateTimeTypes = new JTokenType[]
		{
			JTokenType.Date,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw
		};

		// Token: 0x040008F6 RID: 2294
		private static readonly JTokenType[] BytesTypes = new JTokenType[]
		{
			JTokenType.Bytes,
			JTokenType.String,
			JTokenType.Comment,
			JTokenType.Raw,
			JTokenType.Integer
		};

		// Token: 0x02000310 RID: 784
		[NullableContext(0)]
		private class LineInfoAnnotation
		{
			// Token: 0x060018A8 RID: 6312 RVA: 0x0007A324 File Offset: 0x00078524
			public LineInfoAnnotation(int lineNumber, int linePosition)
			{
				this.LineNumber = lineNumber;
				this.LinePosition = linePosition;
			}

			// Token: 0x04000C9F RID: 3231
			internal readonly int LineNumber;

			// Token: 0x04000CA0 RID: 3232
			internal readonly int LinePosition;
		}
	}
}
