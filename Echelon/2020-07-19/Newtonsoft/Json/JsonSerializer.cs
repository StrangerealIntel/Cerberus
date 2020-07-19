using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000147 RID: 327
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonSerializer
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000AD6 RID: 2774 RVA: 0x0004098C File Offset: 0x0003EB8C
		// (remove) Token: 0x06000AD7 RID: 2775 RVA: 0x000409C8 File Offset: 0x0003EBC8
		[Nullable(new byte[]
		{
			2,
			1
		})]
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public virtual event EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> Error;

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00040A04 File Offset: 0x0003EC04
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x00040A0C File Offset: 0x0003EC0C
		[Nullable(2)]
		public virtual IReferenceResolver ReferenceResolver
		{
			[NullableContext(2)]
			get
			{
				return this.GetReferenceResolver();
			}
			[NullableContext(2)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Reference resolver cannot be null.");
				}
				this._referenceResolver = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00040A2C File Offset: 0x0003EC2C
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x00040A74 File Offset: 0x0003EC74
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public virtual SerializationBinder Binder
		{
			get
			{
				SerializationBinder serializationBinder = this._serializationBinder as SerializationBinder;
				if (serializationBinder != null)
				{
					return serializationBinder;
				}
				SerializationBinderAdapter serializationBinderAdapter = this._serializationBinder as SerializationBinderAdapter;
				if (serializationBinderAdapter != null)
				{
					return serializationBinderAdapter.SerializationBinder;
				}
				throw new InvalidOperationException("Cannot get SerializationBinder because an ISerializationBinder was previously set.");
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Serialization binder cannot be null.");
				}
				this._serializationBinder = ((value as ISerializationBinder) ?? new SerializationBinderAdapter(value));
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00040AA8 File Offset: 0x0003ECA8
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x00040AB0 File Offset: 0x0003ECB0
		public virtual ISerializationBinder SerializationBinder
		{
			get
			{
				return this._serializationBinder;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Serialization binder cannot be null.");
				}
				this._serializationBinder = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00040AD0 File Offset: 0x0003ECD0
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x00040AD8 File Offset: 0x0003ECD8
		[Nullable(2)]
		public virtual ITraceWriter TraceWriter
		{
			[NullableContext(2)]
			get
			{
				return this._traceWriter;
			}
			[NullableContext(2)]
			set
			{
				this._traceWriter = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00040AE4 File Offset: 0x0003ECE4
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x00040AEC File Offset: 0x0003ECEC
		[Nullable(2)]
		public virtual IEqualityComparer EqualityComparer
		{
			[NullableContext(2)]
			get
			{
				return this._equalityComparer;
			}
			[NullableContext(2)]
			set
			{
				this._equalityComparer = value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00040AF8 File Offset: 0x0003ECF8
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x00040B00 File Offset: 0x0003ED00
		public virtual TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling;
			}
			set
			{
				if (value < TypeNameHandling.None || value > TypeNameHandling.Auto)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameHandling = value;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00040B24 File Offset: 0x0003ED24
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x00040B2C File Offset: 0x0003ED2C
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public virtual FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return (FormatterAssemblyStyle)this._typeNameAssemblyFormatHandling;
			}
			set
			{
				if (value < FormatterAssemblyStyle.Simple || value > FormatterAssemblyStyle.Full)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameAssemblyFormatHandling = (TypeNameAssemblyFormatHandling)value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00040B50 File Offset: 0x0003ED50
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x00040B58 File Offset: 0x0003ED58
		public virtual TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._typeNameAssemblyFormatHandling;
			}
			set
			{
				if (value < TypeNameAssemblyFormatHandling.Simple || value > TypeNameAssemblyFormatHandling.Full)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameAssemblyFormatHandling = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00040B7C File Offset: 0x0003ED7C
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x00040B84 File Offset: 0x0003ED84
		public virtual PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._preserveReferencesHandling;
			}
			set
			{
				if (value < PreserveReferencesHandling.None || value > PreserveReferencesHandling.All)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._preserveReferencesHandling = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00040BA8 File Offset: 0x0003EDA8
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x00040BB0 File Offset: 0x0003EDB0
		public virtual ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling;
			}
			set
			{
				if (value < ReferenceLoopHandling.Error || value > ReferenceLoopHandling.Serialize)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._referenceLoopHandling = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00040BD4 File Offset: 0x0003EDD4
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x00040BDC File Offset: 0x0003EDDC
		public virtual MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling;
			}
			set
			{
				if (value < MissingMemberHandling.Ignore || value > MissingMemberHandling.Error)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._missingMemberHandling = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00040C00 File Offset: 0x0003EE00
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x00040C08 File Offset: 0x0003EE08
		public virtual NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling;
			}
			set
			{
				if (value < NullValueHandling.Include || value > NullValueHandling.Ignore)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._nullValueHandling = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00040C2C File Offset: 0x0003EE2C
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x00040C34 File Offset: 0x0003EE34
		public virtual DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling;
			}
			set
			{
				if (value < DefaultValueHandling.Include || value > DefaultValueHandling.IgnoreAndPopulate)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._defaultValueHandling = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00040C58 File Offset: 0x0003EE58
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x00040C60 File Offset: 0x0003EE60
		public virtual ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling;
			}
			set
			{
				if (value < ObjectCreationHandling.Auto || value > ObjectCreationHandling.Replace)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._objectCreationHandling = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00040C84 File Offset: 0x0003EE84
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x00040C8C File Offset: 0x0003EE8C
		public virtual ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._constructorHandling;
			}
			set
			{
				if (value < ConstructorHandling.Default || value > ConstructorHandling.AllowNonPublicDefaultConstructor)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._constructorHandling = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00040CB0 File Offset: 0x0003EEB0
		// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x00040CB8 File Offset: 0x0003EEB8
		public virtual MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._metadataPropertyHandling;
			}
			set
			{
				if (value < MetadataPropertyHandling.Default || value > MetadataPropertyHandling.Ignore)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._metadataPropertyHandling = value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00040CDC File Offset: 0x0003EEDC
		public virtual JsonConverterCollection Converters
		{
			get
			{
				if (this._converters == null)
				{
					this._converters = new JsonConverterCollection();
				}
				return this._converters;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00040CFC File Offset: 0x0003EEFC
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x00040D04 File Offset: 0x0003EF04
		public virtual IContractResolver ContractResolver
		{
			get
			{
				return this._contractResolver;
			}
			set
			{
				this._contractResolver = (value ?? DefaultContractResolver.Instance);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00040D1C File Offset: 0x0003EF1C
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x00040D24 File Offset: 0x0003EF24
		public virtual StreamingContext Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00040D30 File Offset: 0x0003EF30
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x00040D40 File Offset: 0x0003EF40
		public virtual Formatting Formatting
		{
			get
			{
				return this._formatting.GetValueOrDefault();
			}
			set
			{
				this._formatting = new Formatting?(value);
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00040D50 File Offset: 0x0003EF50
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x00040D60 File Offset: 0x0003EF60
		public virtual DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._dateFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._dateFormatHandling = new DateFormatHandling?(value);
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00040D70 File Offset: 0x0003EF70
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x00040DA0 File Offset: 0x0003EFA0
		public virtual DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				DateTimeZoneHandling? dateTimeZoneHandling = this._dateTimeZoneHandling;
				if (dateTimeZoneHandling == null)
				{
					return DateTimeZoneHandling.RoundtripKind;
				}
				return dateTimeZoneHandling.GetValueOrDefault();
			}
			set
			{
				this._dateTimeZoneHandling = new DateTimeZoneHandling?(value);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00040DB0 File Offset: 0x0003EFB0
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x00040DE0 File Offset: 0x0003EFE0
		public virtual DateParseHandling DateParseHandling
		{
			get
			{
				DateParseHandling? dateParseHandling = this._dateParseHandling;
				if (dateParseHandling == null)
				{
					return DateParseHandling.DateTime;
				}
				return dateParseHandling.GetValueOrDefault();
			}
			set
			{
				this._dateParseHandling = new DateParseHandling?(value);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00040DF0 File Offset: 0x0003EFF0
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00040E00 File Offset: 0x0003F000
		public virtual FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._floatParseHandling.GetValueOrDefault();
			}
			set
			{
				this._floatParseHandling = new FloatParseHandling?(value);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00040E10 File Offset: 0x0003F010
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00040E20 File Offset: 0x0003F020
		public virtual FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._floatFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._floatFormatHandling = new FloatFormatHandling?(value);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00040E30 File Offset: 0x0003F030
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x00040E40 File Offset: 0x0003F040
		public virtual StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._stringEscapeHandling.GetValueOrDefault();
			}
			set
			{
				this._stringEscapeHandling = new StringEscapeHandling?(value);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00040E50 File Offset: 0x0003F050
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x00040E64 File Offset: 0x0003F064
		public virtual string DateFormatString
		{
			get
			{
				return this._dateFormatString ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
			}
			set
			{
				this._dateFormatString = value;
				this._dateFormatStringSet = true;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00040E74 File Offset: 0x0003F074
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x00040E88 File Offset: 0x0003F088
		public virtual CultureInfo Culture
		{
			get
			{
				return this._culture ?? JsonSerializerSettings.DefaultCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00040E94 File Offset: 0x0003F094
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x00040E9C File Offset: 0x0003F09C
		public virtual int? MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				int? num = value;
				int num2 = 0;
				if (num.GetValueOrDefault() <= num2 & num != null)
				{
					throw new ArgumentException("Value must be positive.", "value");
				}
				this._maxDepth = value;
				this._maxDepthSet = true;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00040EEC File Offset: 0x0003F0EC
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x00040EFC File Offset: 0x0003F0FC
		public virtual bool CheckAdditionalContent
		{
			get
			{
				return this._checkAdditionalContent.GetValueOrDefault();
			}
			set
			{
				this._checkAdditionalContent = new bool?(value);
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00040F0C File Offset: 0x0003F10C
		internal bool IsCheckAdditionalContentSet()
		{
			return this._checkAdditionalContent != null;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00040F1C File Offset: 0x0003F11C
		public JsonSerializer()
		{
			this._referenceLoopHandling = ReferenceLoopHandling.Error;
			this._missingMemberHandling = MissingMemberHandling.Ignore;
			this._nullValueHandling = NullValueHandling.Include;
			this._defaultValueHandling = DefaultValueHandling.Include;
			this._objectCreationHandling = ObjectCreationHandling.Auto;
			this._preserveReferencesHandling = PreserveReferencesHandling.None;
			this._constructorHandling = ConstructorHandling.Default;
			this._typeNameHandling = TypeNameHandling.None;
			this._metadataPropertyHandling = MetadataPropertyHandling.Default;
			this._context = JsonSerializerSettings.DefaultContext;
			this._serializationBinder = DefaultSerializationBinder.Instance;
			this._culture = JsonSerializerSettings.DefaultCulture;
			this._contractResolver = DefaultContractResolver.Instance;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00040FA0 File Offset: 0x0003F1A0
		public static JsonSerializer Create()
		{
			return new JsonSerializer();
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00040FA8 File Offset: 0x0003F1A8
		public static JsonSerializer Create([Nullable(2)] JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.Create();
			if (settings != null)
			{
				JsonSerializer.ApplySerializerSettings(jsonSerializer, settings);
			}
			return jsonSerializer;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00040FD0 File Offset: 0x0003F1D0
		public static JsonSerializer CreateDefault()
		{
			Func<JsonSerializerSettings> defaultSettings = JsonConvert.DefaultSettings;
			return JsonSerializer.Create((defaultSettings != null) ? defaultSettings() : null);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00040FF0 File Offset: 0x0003F1F0
		public static JsonSerializer CreateDefault([Nullable(2)] JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();
			if (settings != null)
			{
				JsonSerializer.ApplySerializerSettings(jsonSerializer, settings);
			}
			return jsonSerializer;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00041018 File Offset: 0x0003F218
		private static void ApplySerializerSettings(JsonSerializer serializer, JsonSerializerSettings settings)
		{
			if (!CollectionUtils.IsNullOrEmpty<JsonConverter>(settings.Converters))
			{
				for (int i = 0; i < settings.Converters.Count; i++)
				{
					serializer.Converters.Insert(i, settings.Converters[i]);
				}
			}
			if (settings._typeNameHandling != null)
			{
				serializer.TypeNameHandling = settings.TypeNameHandling;
			}
			if (settings._metadataPropertyHandling != null)
			{
				serializer.MetadataPropertyHandling = settings.MetadataPropertyHandling;
			}
			if (settings._typeNameAssemblyFormatHandling != null)
			{
				serializer.TypeNameAssemblyFormatHandling = settings.TypeNameAssemblyFormatHandling;
			}
			if (settings._preserveReferencesHandling != null)
			{
				serializer.PreserveReferencesHandling = settings.PreserveReferencesHandling;
			}
			if (settings._referenceLoopHandling != null)
			{
				serializer.ReferenceLoopHandling = settings.ReferenceLoopHandling;
			}
			if (settings._missingMemberHandling != null)
			{
				serializer.MissingMemberHandling = settings.MissingMemberHandling;
			}
			if (settings._objectCreationHandling != null)
			{
				serializer.ObjectCreationHandling = settings.ObjectCreationHandling;
			}
			if (settings._nullValueHandling != null)
			{
				serializer.NullValueHandling = settings.NullValueHandling;
			}
			if (settings._defaultValueHandling != null)
			{
				serializer.DefaultValueHandling = settings.DefaultValueHandling;
			}
			if (settings._constructorHandling != null)
			{
				serializer.ConstructorHandling = settings.ConstructorHandling;
			}
			if (settings._context != null)
			{
				serializer.Context = settings.Context;
			}
			if (settings._checkAdditionalContent != null)
			{
				serializer._checkAdditionalContent = settings._checkAdditionalContent;
			}
			if (settings.Error != null)
			{
				serializer.Error += settings.Error;
			}
			if (settings.ContractResolver != null)
			{
				serializer.ContractResolver = settings.ContractResolver;
			}
			if (settings.ReferenceResolverProvider != null)
			{
				serializer.ReferenceResolver = settings.ReferenceResolverProvider();
			}
			if (settings.TraceWriter != null)
			{
				serializer.TraceWriter = settings.TraceWriter;
			}
			if (settings.EqualityComparer != null)
			{
				serializer.EqualityComparer = settings.EqualityComparer;
			}
			if (settings.SerializationBinder != null)
			{
				serializer.SerializationBinder = settings.SerializationBinder;
			}
			if (settings._formatting != null)
			{
				serializer._formatting = settings._formatting;
			}
			if (settings._dateFormatHandling != null)
			{
				serializer._dateFormatHandling = settings._dateFormatHandling;
			}
			if (settings._dateTimeZoneHandling != null)
			{
				serializer._dateTimeZoneHandling = settings._dateTimeZoneHandling;
			}
			if (settings._dateParseHandling != null)
			{
				serializer._dateParseHandling = settings._dateParseHandling;
			}
			if (settings._dateFormatStringSet)
			{
				serializer._dateFormatString = settings._dateFormatString;
				serializer._dateFormatStringSet = settings._dateFormatStringSet;
			}
			if (settings._floatFormatHandling != null)
			{
				serializer._floatFormatHandling = settings._floatFormatHandling;
			}
			if (settings._floatParseHandling != null)
			{
				serializer._floatParseHandling = settings._floatParseHandling;
			}
			if (settings._stringEscapeHandling != null)
			{
				serializer._stringEscapeHandling = settings._stringEscapeHandling;
			}
			if (settings._culture != null)
			{
				serializer._culture = settings._culture;
			}
			if (settings._maxDepthSet)
			{
				serializer._maxDepth = settings._maxDepth;
				serializer._maxDepthSet = settings._maxDepthSet;
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0004136C File Offset: 0x0003F56C
		[DebuggerStepThrough]
		public void Populate(TextReader reader, object target)
		{
			this.Populate(new JsonTextReader(reader), target);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0004137C File Offset: 0x0003F57C
		[DebuggerStepThrough]
		public void Populate(JsonReader reader, object target)
		{
			this.PopulateInternal(reader, target);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00041388 File Offset: 0x0003F588
		internal virtual void PopulateInternal(JsonReader reader, object target)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(target, "target");
			CultureInfo previousCulture;
			DateTimeZoneHandling? previousDateTimeZoneHandling;
			DateParseHandling? previousDateParseHandling;
			FloatParseHandling? previousFloatParseHandling;
			int? previousMaxDepth;
			string previousDateFormatString;
			this.SetupReader(reader, out previousCulture, out previousDateTimeZoneHandling, out previousDateParseHandling, out previousFloatParseHandling, out previousMaxDepth, out previousDateFormatString);
			TraceJsonReader traceJsonReader = (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose) ? this.CreateTraceJsonReader(reader) : null;
			new JsonSerializerInternalReader(this).Populate(traceJsonReader ?? reader, target);
			if (traceJsonReader != null)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, traceJsonReader.GetDeserializedJsonMessage(), null);
			}
			this.ResetReader(reader, previousCulture, previousDateTimeZoneHandling, previousDateParseHandling, previousFloatParseHandling, previousMaxDepth, previousDateFormatString);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00041430 File Offset: 0x0003F630
		[DebuggerStepThrough]
		[return: Nullable(2)]
		public object Deserialize(JsonReader reader)
		{
			return this.Deserialize(reader, null);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0004143C File Offset: 0x0003F63C
		[DebuggerStepThrough]
		[return: Nullable(2)]
		public object Deserialize(TextReader reader, Type objectType)
		{
			return this.Deserialize(new JsonTextReader(reader), objectType);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0004144C File Offset: 0x0003F64C
		[DebuggerStepThrough]
		[return: MaybeNull]
		public T Deserialize<[Nullable(2)] T>(JsonReader reader)
		{
			return (T)((object)this.Deserialize(reader, typeof(T)));
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00041464 File Offset: 0x0003F664
		[NullableContext(2)]
		[DebuggerStepThrough]
		public object Deserialize([Nullable(1)] JsonReader reader, Type objectType)
		{
			return this.DeserializeInternal(reader, objectType);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00041470 File Offset: 0x0003F670
		[NullableContext(2)]
		internal virtual object DeserializeInternal([Nullable(1)] JsonReader reader, Type objectType)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			CultureInfo previousCulture;
			DateTimeZoneHandling? previousDateTimeZoneHandling;
			DateParseHandling? previousDateParseHandling;
			FloatParseHandling? previousFloatParseHandling;
			int? previousMaxDepth;
			string previousDateFormatString;
			this.SetupReader(reader, out previousCulture, out previousDateTimeZoneHandling, out previousDateParseHandling, out previousFloatParseHandling, out previousMaxDepth, out previousDateFormatString);
			TraceJsonReader traceJsonReader = (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose) ? this.CreateTraceJsonReader(reader) : null;
			object result = new JsonSerializerInternalReader(this).Deserialize(traceJsonReader ?? reader, objectType, this.CheckAdditionalContent);
			if (traceJsonReader != null)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, traceJsonReader.GetDeserializedJsonMessage(), null);
			}
			this.ResetReader(reader, previousCulture, previousDateTimeZoneHandling, previousDateParseHandling, previousFloatParseHandling, previousMaxDepth, previousDateFormatString);
			return result;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00041510 File Offset: 0x0003F710
		[NullableContext(2)]
		private void SetupReader([Nullable(1)] JsonReader reader, out CultureInfo previousCulture, out DateTimeZoneHandling? previousDateTimeZoneHandling, out DateParseHandling? previousDateParseHandling, out FloatParseHandling? previousFloatParseHandling, out int? previousMaxDepth, out string previousDateFormatString)
		{
			if (this._culture != null && !this._culture.Equals(reader.Culture))
			{
				previousCulture = reader.Culture;
				reader.Culture = this._culture;
			}
			else
			{
				previousCulture = null;
			}
			if (this._dateTimeZoneHandling != null)
			{
				DateTimeZoneHandling dateTimeZoneHandling = reader.DateTimeZoneHandling;
				DateTimeZoneHandling? dateTimeZoneHandling2 = this._dateTimeZoneHandling;
				if (!(dateTimeZoneHandling == dateTimeZoneHandling2.GetValueOrDefault() & dateTimeZoneHandling2 != null))
				{
					previousDateTimeZoneHandling = new DateTimeZoneHandling?(reader.DateTimeZoneHandling);
					reader.DateTimeZoneHandling = this._dateTimeZoneHandling.GetValueOrDefault();
					goto IL_9E;
				}
			}
			previousDateTimeZoneHandling = null;
			IL_9E:
			if (this._dateParseHandling != null)
			{
				DateParseHandling dateParseHandling = reader.DateParseHandling;
				DateParseHandling? dateParseHandling2 = this._dateParseHandling;
				if (!(dateParseHandling == dateParseHandling2.GetValueOrDefault() & dateParseHandling2 != null))
				{
					previousDateParseHandling = new DateParseHandling?(reader.DateParseHandling);
					reader.DateParseHandling = this._dateParseHandling.GetValueOrDefault();
					goto IL_101;
				}
			}
			previousDateParseHandling = null;
			IL_101:
			if (this._floatParseHandling != null)
			{
				FloatParseHandling floatParseHandling = reader.FloatParseHandling;
				FloatParseHandling? floatParseHandling2 = this._floatParseHandling;
				if (!(floatParseHandling == floatParseHandling2.GetValueOrDefault() & floatParseHandling2 != null))
				{
					previousFloatParseHandling = new FloatParseHandling?(reader.FloatParseHandling);
					reader.FloatParseHandling = this._floatParseHandling.GetValueOrDefault();
					goto IL_164;
				}
			}
			previousFloatParseHandling = null;
			IL_164:
			if (this._maxDepthSet)
			{
				int? maxDepth = reader.MaxDepth;
				int? maxDepth2 = this._maxDepth;
				if (!(maxDepth.GetValueOrDefault() == maxDepth2.GetValueOrDefault() & maxDepth != null == (maxDepth2 != null)))
				{
					previousMaxDepth = reader.MaxDepth;
					reader.MaxDepth = this._maxDepth;
					goto IL_1CA;
				}
			}
			previousMaxDepth = null;
			IL_1CA:
			if (this._dateFormatStringSet && reader.DateFormatString != this._dateFormatString)
			{
				previousDateFormatString = reader.DateFormatString;
				reader.DateFormatString = this._dateFormatString;
			}
			else
			{
				previousDateFormatString = null;
			}
			JsonTextReader jsonTextReader = reader as JsonTextReader;
			if (jsonTextReader != null && jsonTextReader.PropertyNameTable == null)
			{
				DefaultContractResolver defaultContractResolver = this._contractResolver as DefaultContractResolver;
				if (defaultContractResolver != null)
				{
					jsonTextReader.PropertyNameTable = defaultContractResolver.GetNameTable();
				}
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00041768 File Offset: 0x0003F968
		[NullableContext(2)]
		private void ResetReader([Nullable(1)] JsonReader reader, CultureInfo previousCulture, DateTimeZoneHandling? previousDateTimeZoneHandling, DateParseHandling? previousDateParseHandling, FloatParseHandling? previousFloatParseHandling, int? previousMaxDepth, string previousDateFormatString)
		{
			if (previousCulture != null)
			{
				reader.Culture = previousCulture;
			}
			if (previousDateTimeZoneHandling != null)
			{
				reader.DateTimeZoneHandling = previousDateTimeZoneHandling.GetValueOrDefault();
			}
			if (previousDateParseHandling != null)
			{
				reader.DateParseHandling = previousDateParseHandling.GetValueOrDefault();
			}
			if (previousFloatParseHandling != null)
			{
				reader.FloatParseHandling = previousFloatParseHandling.GetValueOrDefault();
			}
			if (this._maxDepthSet)
			{
				reader.MaxDepth = previousMaxDepth;
			}
			if (this._dateFormatStringSet)
			{
				reader.DateFormatString = previousDateFormatString;
			}
			JsonTextReader jsonTextReader = reader as JsonTextReader;
			if (jsonTextReader != null && jsonTextReader.PropertyNameTable != null)
			{
				DefaultContractResolver defaultContractResolver = this._contractResolver as DefaultContractResolver;
				if (defaultContractResolver != null && jsonTextReader.PropertyNameTable == defaultContractResolver.GetNameTable())
				{
					jsonTextReader.PropertyNameTable = null;
				}
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0004183C File Offset: 0x0003FA3C
		public void Serialize(TextWriter textWriter, [Nullable(2)] object value)
		{
			this.Serialize(new JsonTextWriter(textWriter), value);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0004184C File Offset: 0x0003FA4C
		[NullableContext(2)]
		public void Serialize([Nullable(1)] JsonWriter jsonWriter, object value, Type objectType)
		{
			this.SerializeInternal(jsonWriter, value, objectType);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00041858 File Offset: 0x0003FA58
		public void Serialize(TextWriter textWriter, [Nullable(2)] object value, Type objectType)
		{
			this.Serialize(new JsonTextWriter(textWriter), value, objectType);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00041868 File Offset: 0x0003FA68
		public void Serialize(JsonWriter jsonWriter, [Nullable(2)] object value)
		{
			this.SerializeInternal(jsonWriter, value, null);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00041874 File Offset: 0x0003FA74
		private TraceJsonReader CreateTraceJsonReader(JsonReader reader)
		{
			TraceJsonReader traceJsonReader = new TraceJsonReader(reader);
			if (reader.TokenType != JsonToken.None)
			{
				traceJsonReader.WriteCurrentToken();
			}
			return traceJsonReader;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000418A0 File Offset: 0x0003FAA0
		[NullableContext(2)]
		internal virtual void SerializeInternal([Nullable(1)] JsonWriter jsonWriter, object value, Type objectType)
		{
			ValidationUtils.ArgumentNotNull(jsonWriter, "jsonWriter");
			Formatting? formatting = null;
			if (this._formatting != null)
			{
				Formatting formatting2 = jsonWriter.Formatting;
				Formatting? formatting3 = this._formatting;
				if (!(formatting2 == formatting3.GetValueOrDefault() & formatting3 != null))
				{
					formatting = new Formatting?(jsonWriter.Formatting);
					jsonWriter.Formatting = this._formatting.GetValueOrDefault();
				}
			}
			DateFormatHandling? dateFormatHandling = null;
			if (this._dateFormatHandling != null)
			{
				DateFormatHandling dateFormatHandling2 = jsonWriter.DateFormatHandling;
				DateFormatHandling? dateFormatHandling3 = this._dateFormatHandling;
				if (!(dateFormatHandling2 == dateFormatHandling3.GetValueOrDefault() & dateFormatHandling3 != null))
				{
					dateFormatHandling = new DateFormatHandling?(jsonWriter.DateFormatHandling);
					jsonWriter.DateFormatHandling = this._dateFormatHandling.GetValueOrDefault();
				}
			}
			DateTimeZoneHandling? dateTimeZoneHandling = null;
			if (this._dateTimeZoneHandling != null)
			{
				DateTimeZoneHandling dateTimeZoneHandling2 = jsonWriter.DateTimeZoneHandling;
				DateTimeZoneHandling? dateTimeZoneHandling3 = this._dateTimeZoneHandling;
				if (!(dateTimeZoneHandling2 == dateTimeZoneHandling3.GetValueOrDefault() & dateTimeZoneHandling3 != null))
				{
					dateTimeZoneHandling = new DateTimeZoneHandling?(jsonWriter.DateTimeZoneHandling);
					jsonWriter.DateTimeZoneHandling = this._dateTimeZoneHandling.GetValueOrDefault();
				}
			}
			FloatFormatHandling? floatFormatHandling = null;
			if (this._floatFormatHandling != null)
			{
				FloatFormatHandling floatFormatHandling2 = jsonWriter.FloatFormatHandling;
				FloatFormatHandling? floatFormatHandling3 = this._floatFormatHandling;
				if (!(floatFormatHandling2 == floatFormatHandling3.GetValueOrDefault() & floatFormatHandling3 != null))
				{
					floatFormatHandling = new FloatFormatHandling?(jsonWriter.FloatFormatHandling);
					jsonWriter.FloatFormatHandling = this._floatFormatHandling.GetValueOrDefault();
				}
			}
			StringEscapeHandling? stringEscapeHandling = null;
			if (this._stringEscapeHandling != null)
			{
				StringEscapeHandling stringEscapeHandling2 = jsonWriter.StringEscapeHandling;
				StringEscapeHandling? stringEscapeHandling3 = this._stringEscapeHandling;
				if (!(stringEscapeHandling2 == stringEscapeHandling3.GetValueOrDefault() & stringEscapeHandling3 != null))
				{
					stringEscapeHandling = new StringEscapeHandling?(jsonWriter.StringEscapeHandling);
					jsonWriter.StringEscapeHandling = this._stringEscapeHandling.GetValueOrDefault();
				}
			}
			CultureInfo cultureInfo = null;
			if (this._culture != null && !this._culture.Equals(jsonWriter.Culture))
			{
				cultureInfo = jsonWriter.Culture;
				jsonWriter.Culture = this._culture;
			}
			string dateFormatString = null;
			if (this._dateFormatStringSet && jsonWriter.DateFormatString != this._dateFormatString)
			{
				dateFormatString = jsonWriter.DateFormatString;
				jsonWriter.DateFormatString = this._dateFormatString;
			}
			TraceJsonWriter traceJsonWriter = (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose) ? new TraceJsonWriter(jsonWriter) : null;
			new JsonSerializerInternalWriter(this).Serialize(traceJsonWriter ?? jsonWriter, value, objectType);
			if (traceJsonWriter != null)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, traceJsonWriter.GetSerializedJsonMessage(), null);
			}
			if (formatting != null)
			{
				jsonWriter.Formatting = formatting.GetValueOrDefault();
			}
			if (dateFormatHandling != null)
			{
				jsonWriter.DateFormatHandling = dateFormatHandling.GetValueOrDefault();
			}
			if (dateTimeZoneHandling != null)
			{
				jsonWriter.DateTimeZoneHandling = dateTimeZoneHandling.GetValueOrDefault();
			}
			if (floatFormatHandling != null)
			{
				jsonWriter.FloatFormatHandling = floatFormatHandling.GetValueOrDefault();
			}
			if (stringEscapeHandling != null)
			{
				jsonWriter.StringEscapeHandling = stringEscapeHandling.GetValueOrDefault();
			}
			if (this._dateFormatStringSet)
			{
				jsonWriter.DateFormatString = dateFormatString;
			}
			if (cultureInfo != null)
			{
				jsonWriter.Culture = cultureInfo;
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00041BE8 File Offset: 0x0003FDE8
		internal IReferenceResolver GetReferenceResolver()
		{
			if (this._referenceResolver == null)
			{
				this._referenceResolver = new DefaultReferenceResolver();
			}
			return this._referenceResolver;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00041C08 File Offset: 0x0003FE08
		[return: Nullable(2)]
		internal JsonConverter GetMatchingConverter(Type type)
		{
			return JsonSerializer.GetMatchingConverter(this._converters, type);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00041C18 File Offset: 0x0003FE18
		[return: Nullable(2)]
		internal static JsonConverter GetMatchingConverter([Nullable(new byte[]
		{
			2,
			1
		})] IList<JsonConverter> converters, Type objectType)
		{
			if (converters != null)
			{
				for (int i = 0; i < converters.Count; i++)
				{
					JsonConverter jsonConverter = converters[i];
					if (jsonConverter.CanConvert(objectType))
					{
						return jsonConverter;
					}
				}
			}
			return null;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00041C5C File Offset: 0x0003FE5C
		internal void OnError(Newtonsoft.Json.Serialization.ErrorEventArgs e)
		{
			EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> error = this.Error;
			if (error == null)
			{
				return;
			}
			error(this, e);
		}

		// Token: 0x040005ED RID: 1517
		internal TypeNameHandling _typeNameHandling;

		// Token: 0x040005EE RID: 1518
		internal TypeNameAssemblyFormatHandling _typeNameAssemblyFormatHandling;

		// Token: 0x040005EF RID: 1519
		internal PreserveReferencesHandling _preserveReferencesHandling;

		// Token: 0x040005F0 RID: 1520
		internal ReferenceLoopHandling _referenceLoopHandling;

		// Token: 0x040005F1 RID: 1521
		internal MissingMemberHandling _missingMemberHandling;

		// Token: 0x040005F2 RID: 1522
		internal ObjectCreationHandling _objectCreationHandling;

		// Token: 0x040005F3 RID: 1523
		internal NullValueHandling _nullValueHandling;

		// Token: 0x040005F4 RID: 1524
		internal DefaultValueHandling _defaultValueHandling;

		// Token: 0x040005F5 RID: 1525
		internal ConstructorHandling _constructorHandling;

		// Token: 0x040005F6 RID: 1526
		internal MetadataPropertyHandling _metadataPropertyHandling;

		// Token: 0x040005F7 RID: 1527
		[Nullable(2)]
		internal JsonConverterCollection _converters;

		// Token: 0x040005F8 RID: 1528
		internal IContractResolver _contractResolver;

		// Token: 0x040005F9 RID: 1529
		[Nullable(2)]
		internal ITraceWriter _traceWriter;

		// Token: 0x040005FA RID: 1530
		[Nullable(2)]
		internal IEqualityComparer _equalityComparer;

		// Token: 0x040005FB RID: 1531
		internal ISerializationBinder _serializationBinder;

		// Token: 0x040005FC RID: 1532
		internal StreamingContext _context;

		// Token: 0x040005FD RID: 1533
		[Nullable(2)]
		private IReferenceResolver _referenceResolver;

		// Token: 0x040005FE RID: 1534
		private Formatting? _formatting;

		// Token: 0x040005FF RID: 1535
		private DateFormatHandling? _dateFormatHandling;

		// Token: 0x04000600 RID: 1536
		private DateTimeZoneHandling? _dateTimeZoneHandling;

		// Token: 0x04000601 RID: 1537
		private DateParseHandling? _dateParseHandling;

		// Token: 0x04000602 RID: 1538
		private FloatFormatHandling? _floatFormatHandling;

		// Token: 0x04000603 RID: 1539
		private FloatParseHandling? _floatParseHandling;

		// Token: 0x04000604 RID: 1540
		private StringEscapeHandling? _stringEscapeHandling;

		// Token: 0x04000605 RID: 1541
		private CultureInfo _culture;

		// Token: 0x04000606 RID: 1542
		private int? _maxDepth;

		// Token: 0x04000607 RID: 1543
		private bool _maxDepthSet;

		// Token: 0x04000608 RID: 1544
		private bool? _checkAdditionalContent;

		// Token: 0x04000609 RID: 1545
		[Nullable(2)]
		private string _dateFormatString;

		// Token: 0x0400060A RID: 1546
		private bool _dateFormatStringSet;
	}
}
