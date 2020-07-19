using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001AC RID: 428
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonPrimitiveContract : JsonContract
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00057738 File Offset: 0x00055938
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x00057740 File Offset: 0x00055940
		internal PrimitiveTypeCode TypeCode { get; set; }

		// Token: 0x06000F97 RID: 3991 RVA: 0x0005774C File Offset: 0x0005594C
		public JsonPrimitiveContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Primitive;
			this.TypeCode = ConvertUtils.GetTypeCode(underlyingType);
			this.IsReadOnlyOrFixedSize = true;
			ReadType internalReadType;
			if (JsonPrimitiveContract.ReadTypeMap.TryGetValue(this.NonNullableUnderlyingType, out internalReadType))
			{
				this.InternalReadType = internalReadType;
			}
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0005779C File Offset: 0x0005599C
		// Note: this type is marked as 'beforefieldinit'.
		static JsonPrimitiveContract()
		{
			Dictionary<Type, ReadType> dictionary = new Dictionary<Type, ReadType>();
			Type typeFromHandle = typeof(byte[]);
			dictionary[typeFromHandle] = ReadType.ReadAsBytes;
			Type typeFromHandle2 = typeof(byte);
			dictionary[typeFromHandle2] = ReadType.ReadAsInt32;
			Type typeFromHandle3 = typeof(short);
			dictionary[typeFromHandle3] = ReadType.ReadAsInt32;
			Type typeFromHandle4 = typeof(int);
			dictionary[typeFromHandle4] = ReadType.ReadAsInt32;
			Type typeFromHandle5 = typeof(decimal);
			dictionary[typeFromHandle5] = ReadType.ReadAsDecimal;
			Type typeFromHandle6 = typeof(bool);
			dictionary[typeFromHandle6] = ReadType.ReadAsBoolean;
			Type typeFromHandle7 = typeof(string);
			dictionary[typeFromHandle7] = ReadType.ReadAsString;
			Type typeFromHandle8 = typeof(DateTime);
			dictionary[typeFromHandle8] = ReadType.ReadAsDateTime;
			Type typeFromHandle9 = typeof(DateTimeOffset);
			dictionary[typeFromHandle9] = ReadType.ReadAsDateTimeOffset;
			Type typeFromHandle10 = typeof(float);
			dictionary[typeFromHandle10] = ReadType.ReadAsDouble;
			Type typeFromHandle11 = typeof(double);
			dictionary[typeFromHandle11] = ReadType.ReadAsDouble;
			Type typeFromHandle12 = typeof(long);
			dictionary[typeFromHandle12] = ReadType.ReadAsInt64;
			JsonPrimitiveContract.ReadTypeMap = dictionary;
		}

		// Token: 0x040007FA RID: 2042
		private static readonly Dictionary<Type, ReadType> ReadTypeMap;
	}
}
