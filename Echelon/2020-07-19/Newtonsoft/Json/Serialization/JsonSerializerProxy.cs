using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001B2 RID: 434
	[NullableContext(1)]
	[Nullable(0)]
	internal class JsonSerializerProxy : JsonSerializer
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600103E RID: 4158 RVA: 0x0005DF30 File Offset: 0x0005C130
		// (remove) Token: 0x0600103F RID: 4159 RVA: 0x0005DF40 File Offset: 0x0005C140
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public override event EventHandler<ErrorEventArgs> Error
		{
			add
			{
				this._serializer.Error += value;
			}
			remove
			{
				this._serializer.Error -= value;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0005DF50 File Offset: 0x0005C150
		// (set) Token: 0x06001041 RID: 4161 RVA: 0x0005DF60 File Offset: 0x0005C160
		[Nullable(2)]
		public override IReferenceResolver ReferenceResolver
		{
			[NullableContext(2)]
			get
			{
				return this._serializer.ReferenceResolver;
			}
			[NullableContext(2)]
			set
			{
				this._serializer.ReferenceResolver = value;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0005DF70 File Offset: 0x0005C170
		// (set) Token: 0x06001043 RID: 4163 RVA: 0x0005DF80 File Offset: 0x0005C180
		[Nullable(2)]
		public override ITraceWriter TraceWriter
		{
			[NullableContext(2)]
			get
			{
				return this._serializer.TraceWriter;
			}
			[NullableContext(2)]
			set
			{
				this._serializer.TraceWriter = value;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0005DF90 File Offset: 0x0005C190
		// (set) Token: 0x06001045 RID: 4165 RVA: 0x0005DFA0 File Offset: 0x0005C1A0
		[Nullable(2)]
		public override IEqualityComparer EqualityComparer
		{
			[NullableContext(2)]
			get
			{
				return this._serializer.EqualityComparer;
			}
			[NullableContext(2)]
			set
			{
				this._serializer.EqualityComparer = value;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0005DFB0 File Offset: 0x0005C1B0
		public override JsonConverterCollection Converters
		{
			get
			{
				return this._serializer.Converters;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x0005DFC0 File Offset: 0x0005C1C0
		// (set) Token: 0x06001048 RID: 4168 RVA: 0x0005DFD0 File Offset: 0x0005C1D0
		public override DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._serializer.DefaultValueHandling;
			}
			set
			{
				this._serializer.DefaultValueHandling = value;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x0005DFE0 File Offset: 0x0005C1E0
		// (set) Token: 0x0600104A RID: 4170 RVA: 0x0005DFF0 File Offset: 0x0005C1F0
		public override IContractResolver ContractResolver
		{
			get
			{
				return this._serializer.ContractResolver;
			}
			set
			{
				this._serializer.ContractResolver = value;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x0005E000 File Offset: 0x0005C200
		// (set) Token: 0x0600104C RID: 4172 RVA: 0x0005E010 File Offset: 0x0005C210
		public override MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._serializer.MissingMemberHandling;
			}
			set
			{
				this._serializer.MissingMemberHandling = value;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0005E020 File Offset: 0x0005C220
		// (set) Token: 0x0600104E RID: 4174 RVA: 0x0005E030 File Offset: 0x0005C230
		public override NullValueHandling NullValueHandling
		{
			get
			{
				return this._serializer.NullValueHandling;
			}
			set
			{
				this._serializer.NullValueHandling = value;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0005E040 File Offset: 0x0005C240
		// (set) Token: 0x06001050 RID: 4176 RVA: 0x0005E050 File Offset: 0x0005C250
		public override ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._serializer.ObjectCreationHandling;
			}
			set
			{
				this._serializer.ObjectCreationHandling = value;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x0005E060 File Offset: 0x0005C260
		// (set) Token: 0x06001052 RID: 4178 RVA: 0x0005E070 File Offset: 0x0005C270
		public override ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._serializer.ReferenceLoopHandling;
			}
			set
			{
				this._serializer.ReferenceLoopHandling = value;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x0005E080 File Offset: 0x0005C280
		// (set) Token: 0x06001054 RID: 4180 RVA: 0x0005E090 File Offset: 0x0005C290
		public override PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._serializer.PreserveReferencesHandling;
			}
			set
			{
				this._serializer.PreserveReferencesHandling = value;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0005E0A0 File Offset: 0x0005C2A0
		// (set) Token: 0x06001056 RID: 4182 RVA: 0x0005E0B0 File Offset: 0x0005C2B0
		public override TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._serializer.TypeNameHandling;
			}
			set
			{
				this._serializer.TypeNameHandling = value;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x0005E0C0 File Offset: 0x0005C2C0
		// (set) Token: 0x06001058 RID: 4184 RVA: 0x0005E0D0 File Offset: 0x0005C2D0
		public override MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._serializer.MetadataPropertyHandling;
			}
			set
			{
				this._serializer.MetadataPropertyHandling = value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x0005E0E0 File Offset: 0x0005C2E0
		// (set) Token: 0x0600105A RID: 4186 RVA: 0x0005E0F0 File Offset: 0x0005C2F0
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public override FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return this._serializer.TypeNameAssemblyFormat;
			}
			set
			{
				this._serializer.TypeNameAssemblyFormat = value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x0005E100 File Offset: 0x0005C300
		// (set) Token: 0x0600105C RID: 4188 RVA: 0x0005E110 File Offset: 0x0005C310
		public override TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._serializer.TypeNameAssemblyFormatHandling;
			}
			set
			{
				this._serializer.TypeNameAssemblyFormatHandling = value;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x0005E120 File Offset: 0x0005C320
		// (set) Token: 0x0600105E RID: 4190 RVA: 0x0005E130 File Offset: 0x0005C330
		public override ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._serializer.ConstructorHandling;
			}
			set
			{
				this._serializer.ConstructorHandling = value;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x0005E140 File Offset: 0x0005C340
		// (set) Token: 0x06001060 RID: 4192 RVA: 0x0005E150 File Offset: 0x0005C350
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public override SerializationBinder Binder
		{
			get
			{
				return this._serializer.Binder;
			}
			set
			{
				this._serializer.Binder = value;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x0005E160 File Offset: 0x0005C360
		// (set) Token: 0x06001062 RID: 4194 RVA: 0x0005E170 File Offset: 0x0005C370
		public override ISerializationBinder SerializationBinder
		{
			get
			{
				return this._serializer.SerializationBinder;
			}
			set
			{
				this._serializer.SerializationBinder = value;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x0005E180 File Offset: 0x0005C380
		// (set) Token: 0x06001064 RID: 4196 RVA: 0x0005E190 File Offset: 0x0005C390
		public override StreamingContext Context
		{
			get
			{
				return this._serializer.Context;
			}
			set
			{
				this._serializer.Context = value;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0005E1A0 File Offset: 0x0005C3A0
		// (set) Token: 0x06001066 RID: 4198 RVA: 0x0005E1B0 File Offset: 0x0005C3B0
		public override Formatting Formatting
		{
			get
			{
				return this._serializer.Formatting;
			}
			set
			{
				this._serializer.Formatting = value;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0005E1C0 File Offset: 0x0005C3C0
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x0005E1D0 File Offset: 0x0005C3D0
		public override DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._serializer.DateFormatHandling;
			}
			set
			{
				this._serializer.DateFormatHandling = value;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x0005E1E0 File Offset: 0x0005C3E0
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x0005E1F0 File Offset: 0x0005C3F0
		public override DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._serializer.DateTimeZoneHandling;
			}
			set
			{
				this._serializer.DateTimeZoneHandling = value;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0005E200 File Offset: 0x0005C400
		// (set) Token: 0x0600106C RID: 4204 RVA: 0x0005E210 File Offset: 0x0005C410
		public override DateParseHandling DateParseHandling
		{
			get
			{
				return this._serializer.DateParseHandling;
			}
			set
			{
				this._serializer.DateParseHandling = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x0005E220 File Offset: 0x0005C420
		// (set) Token: 0x0600106E RID: 4206 RVA: 0x0005E230 File Offset: 0x0005C430
		public override FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._serializer.FloatFormatHandling;
			}
			set
			{
				this._serializer.FloatFormatHandling = value;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x0005E240 File Offset: 0x0005C440
		// (set) Token: 0x06001070 RID: 4208 RVA: 0x0005E250 File Offset: 0x0005C450
		public override FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._serializer.FloatParseHandling;
			}
			set
			{
				this._serializer.FloatParseHandling = value;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0005E260 File Offset: 0x0005C460
		// (set) Token: 0x06001072 RID: 4210 RVA: 0x0005E270 File Offset: 0x0005C470
		public override StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._serializer.StringEscapeHandling;
			}
			set
			{
				this._serializer.StringEscapeHandling = value;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x0005E280 File Offset: 0x0005C480
		// (set) Token: 0x06001074 RID: 4212 RVA: 0x0005E290 File Offset: 0x0005C490
		public override string DateFormatString
		{
			get
			{
				return this._serializer.DateFormatString;
			}
			set
			{
				this._serializer.DateFormatString = value;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x0005E2A0 File Offset: 0x0005C4A0
		// (set) Token: 0x06001076 RID: 4214 RVA: 0x0005E2B0 File Offset: 0x0005C4B0
		public override CultureInfo Culture
		{
			get
			{
				return this._serializer.Culture;
			}
			set
			{
				this._serializer.Culture = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0005E2C0 File Offset: 0x0005C4C0
		// (set) Token: 0x06001078 RID: 4216 RVA: 0x0005E2D0 File Offset: 0x0005C4D0
		public override int? MaxDepth
		{
			get
			{
				return this._serializer.MaxDepth;
			}
			set
			{
				this._serializer.MaxDepth = value;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x0005E2E0 File Offset: 0x0005C4E0
		// (set) Token: 0x0600107A RID: 4218 RVA: 0x0005E2F0 File Offset: 0x0005C4F0
		public override bool CheckAdditionalContent
		{
			get
			{
				return this._serializer.CheckAdditionalContent;
			}
			set
			{
				this._serializer.CheckAdditionalContent = value;
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0005E300 File Offset: 0x0005C500
		internal JsonSerializerInternalBase GetInternalSerializer()
		{
			if (this._serializerReader != null)
			{
				return this._serializerReader;
			}
			return this._serializerWriter;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0005E31C File Offset: 0x0005C51C
		public JsonSerializerProxy(JsonSerializerInternalReader serializerReader)
		{
			ValidationUtils.ArgumentNotNull(serializerReader, "serializerReader");
			this._serializerReader = serializerReader;
			this._serializer = serializerReader.Serializer;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0005E344 File Offset: 0x0005C544
		public JsonSerializerProxy(JsonSerializerInternalWriter serializerWriter)
		{
			ValidationUtils.ArgumentNotNull(serializerWriter, "serializerWriter");
			this._serializerWriter = serializerWriter;
			this._serializer = serializerWriter.Serializer;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0005E36C File Offset: 0x0005C56C
		[NullableContext(2)]
		internal override object DeserializeInternal([Nullable(1)] JsonReader reader, Type objectType)
		{
			if (this._serializerReader != null)
			{
				return this._serializerReader.Deserialize(reader, objectType, false);
			}
			return this._serializer.Deserialize(reader, objectType);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0005E398 File Offset: 0x0005C598
		internal override void PopulateInternal(JsonReader reader, object target)
		{
			if (this._serializerReader != null)
			{
				this._serializerReader.Populate(reader, target);
				return;
			}
			this._serializer.Populate(reader, target);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0005E3C0 File Offset: 0x0005C5C0
		[NullableContext(2)]
		internal override void SerializeInternal([Nullable(1)] JsonWriter jsonWriter, object value, Type rootType)
		{
			if (this._serializerWriter != null)
			{
				this._serializerWriter.Serialize(jsonWriter, value, rootType);
				return;
			}
			this._serializer.Serialize(jsonWriter, value);
		}

		// Token: 0x04000825 RID: 2085
		[Nullable(2)]
		private readonly JsonSerializerInternalReader _serializerReader;

		// Token: 0x04000826 RID: 2086
		[Nullable(2)]
		private readonly JsonSerializerInternalWriter _serializerWriter;

		// Token: 0x04000827 RID: 2087
		private readonly JsonSerializer _serializer;
	}
}
