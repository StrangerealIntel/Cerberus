using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001BE RID: 446
	[NullableContext(1)]
	[Nullable(0)]
	internal class TraceJsonReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x060010C1 RID: 4289 RVA: 0x0005EF60 File Offset: 0x0005D160
		public TraceJsonReader(JsonReader innerReader)
		{
			this._innerReader = innerReader;
			this._sw = new StringWriter(CultureInfo.InvariantCulture);
			this._sw.Write("Deserialized JSON: " + Environment.NewLine);
			this._textWriter = new JsonTextWriter(this._sw);
			this._textWriter.Formatting = Formatting.Indented;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0005EFC8 File Offset: 0x0005D1C8
		public string GetDeserializedJsonMessage()
		{
			return this._sw.ToString();
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0005EFD8 File Offset: 0x0005D1D8
		public override bool Read()
		{
			bool result = this._innerReader.Read();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0005EFEC File Offset: 0x0005D1EC
		public override int? ReadAsInt32()
		{
			int? result = this._innerReader.ReadAsInt32();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0005F000 File Offset: 0x0005D200
		[NullableContext(2)]
		public override string ReadAsString()
		{
			string result = this._innerReader.ReadAsString();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0005F014 File Offset: 0x0005D214
		[NullableContext(2)]
		public override byte[] ReadAsBytes()
		{
			byte[] result = this._innerReader.ReadAsBytes();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0005F028 File Offset: 0x0005D228
		public override decimal? ReadAsDecimal()
		{
			decimal? result = this._innerReader.ReadAsDecimal();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0005F03C File Offset: 0x0005D23C
		public override double? ReadAsDouble()
		{
			double? result = this._innerReader.ReadAsDouble();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0005F050 File Offset: 0x0005D250
		public override bool? ReadAsBoolean()
		{
			bool? result = this._innerReader.ReadAsBoolean();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0005F064 File Offset: 0x0005D264
		public override DateTime? ReadAsDateTime()
		{
			DateTime? result = this._innerReader.ReadAsDateTime();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0005F078 File Offset: 0x0005D278
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? result = this._innerReader.ReadAsDateTimeOffset();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0005F08C File Offset: 0x0005D28C
		public void WriteCurrentToken()
		{
			this._textWriter.WriteToken(this._innerReader, false, false, true);
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0005F0A4 File Offset: 0x0005D2A4
		public override int Depth
		{
			get
			{
				return this._innerReader.Depth;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x0005F0B4 File Offset: 0x0005D2B4
		public override string Path
		{
			get
			{
				return this._innerReader.Path;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0005F0C4 File Offset: 0x0005D2C4
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x0005F0D4 File Offset: 0x0005D2D4
		public override char QuoteChar
		{
			get
			{
				return this._innerReader.QuoteChar;
			}
			protected internal set
			{
				this._innerReader.QuoteChar = value;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x0005F0E4 File Offset: 0x0005D2E4
		public override JsonToken TokenType
		{
			get
			{
				return this._innerReader.TokenType;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x0005F0F4 File Offset: 0x0005D2F4
		[Nullable(2)]
		public override object Value
		{
			[NullableContext(2)]
			get
			{
				return this._innerReader.Value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0005F104 File Offset: 0x0005D304
		[Nullable(2)]
		public override Type ValueType
		{
			[NullableContext(2)]
			get
			{
				return this._innerReader.ValueType;
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0005F114 File Offset: 0x0005D314
		public override void Close()
		{
			this._innerReader.Close();
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0005F124 File Offset: 0x0005D324
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x0005F150 File Offset: 0x0005D350
		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0005F17C File Offset: 0x0005D37C
		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		// Token: 0x0400083E RID: 2110
		private readonly JsonReader _innerReader;

		// Token: 0x0400083F RID: 2111
		private readonly JsonTextWriter _textWriter;

		// Token: 0x04000840 RID: 2112
		private readonly StringWriter _sw;
	}
}
