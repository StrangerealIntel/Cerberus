using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001A8 RID: 424
	[NullableContext(1)]
	[Nullable(0)]
	internal class JsonFormatterConverter : IFormatterConverter
	{
		// Token: 0x06000F65 RID: 3941 RVA: 0x000572DC File Offset: 0x000554DC
		public JsonFormatterConverter(JsonSerializerInternalReader reader, JsonISerializableContract contract, [Nullable(2)] JsonProperty member)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(contract, "contract");
			this._reader = reader;
			this._contract = contract;
			this._member = member;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00057310 File Offset: 0x00055510
		private T GetTokenValue<[Nullable(2)] T>(object value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			return (T)((object)System.Convert.ChangeType(((JValue)value).Value, typeof(T), CultureInfo.InvariantCulture));
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00057344 File Offset: 0x00055544
		[return: Nullable(2)]
		public object Convert(object value, Type type)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JToken jtoken = value as JToken;
			if (jtoken == null)
			{
				throw new ArgumentException("Value is not a JToken.", "value");
			}
			return this._reader.CreateISerializableItem(jtoken, type, this._contract, this._member);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00057398 File Offset: 0x00055598
		public object Convert(object value, TypeCode typeCode)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JValue jvalue = value as JValue;
			return System.Convert.ChangeType((jvalue != null) ? jvalue.Value : value, typeCode, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x000573D8 File Offset: 0x000555D8
		public bool ToBoolean(object value)
		{
			return this.GetTokenValue<bool>(value);
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x000573E4 File Offset: 0x000555E4
		public byte ToByte(object value)
		{
			return this.GetTokenValue<byte>(value);
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x000573F0 File Offset: 0x000555F0
		public char ToChar(object value)
		{
			return this.GetTokenValue<char>(value);
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x000573FC File Offset: 0x000555FC
		public DateTime ToDateTime(object value)
		{
			return this.GetTokenValue<DateTime>(value);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00057408 File Offset: 0x00055608
		public decimal ToDecimal(object value)
		{
			return this.GetTokenValue<decimal>(value);
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00057414 File Offset: 0x00055614
		public double ToDouble(object value)
		{
			return this.GetTokenValue<double>(value);
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00057420 File Offset: 0x00055620
		public short ToInt16(object value)
		{
			return this.GetTokenValue<short>(value);
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0005742C File Offset: 0x0005562C
		public int ToInt32(object value)
		{
			return this.GetTokenValue<int>(value);
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x00057438 File Offset: 0x00055638
		public long ToInt64(object value)
		{
			return this.GetTokenValue<long>(value);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00057444 File Offset: 0x00055644
		public sbyte ToSByte(object value)
		{
			return this.GetTokenValue<sbyte>(value);
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x00057450 File Offset: 0x00055650
		public float ToSingle(object value)
		{
			return this.GetTokenValue<float>(value);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0005745C File Offset: 0x0005565C
		public string ToString(object value)
		{
			return this.GetTokenValue<string>(value);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00057468 File Offset: 0x00055668
		public ushort ToUInt16(object value)
		{
			return this.GetTokenValue<ushort>(value);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00057474 File Offset: 0x00055674
		public uint ToUInt32(object value)
		{
			return this.GetTokenValue<uint>(value);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00057480 File Offset: 0x00055680
		public ulong ToUInt64(object value)
		{
			return this.GetTokenValue<ulong>(value);
		}

		// Token: 0x040007E7 RID: 2023
		private readonly JsonSerializerInternalReader _reader;

		// Token: 0x040007E8 RID: 2024
		private readonly JsonISerializableContract _contract;

		// Token: 0x040007E9 RID: 2025
		[Nullable(2)]
		private readonly JsonProperty _member;
	}
}
