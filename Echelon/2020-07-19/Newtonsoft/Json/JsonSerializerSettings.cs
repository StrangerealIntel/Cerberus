using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000148 RID: 328
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonSerializerSettings
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00041C74 File Offset: 0x0003FE74
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x00041C84 File Offset: 0x0003FE84
		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x00041C94 File Offset: 0x0003FE94
		// (set) Token: 0x06000B31 RID: 2865 RVA: 0x00041CA4 File Offset: 0x0003FEA4
		public MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling.GetValueOrDefault();
			}
			set
			{
				this._missingMemberHandling = new MissingMemberHandling?(value);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00041CB4 File Offset: 0x0003FEB4
		// (set) Token: 0x06000B33 RID: 2867 RVA: 0x00041CC4 File Offset: 0x0003FEC4
		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00041CD4 File Offset: 0x0003FED4
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x00041CE4 File Offset: 0x0003FEE4
		public NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00041CF4 File Offset: 0x0003FEF4
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x00041D04 File Offset: 0x0003FF04
		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00041D14 File Offset: 0x0003FF14
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x00041D1C File Offset: 0x0003FF1C
		[Nullable(1)]
		public IList<JsonConverter> Converters { [NullableContext(1)] get; [NullableContext(1)] set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00041D28 File Offset: 0x0003FF28
		// (set) Token: 0x06000B3B RID: 2875 RVA: 0x00041D38 File Offset: 0x0003FF38
		public PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._preserveReferencesHandling.GetValueOrDefault();
			}
			set
			{
				this._preserveReferencesHandling = new PreserveReferencesHandling?(value);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00041D48 File Offset: 0x0003FF48
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x00041D58 File Offset: 0x0003FF58
		public TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00041D68 File Offset: 0x0003FF68
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x00041D78 File Offset: 0x0003FF78
		public MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._metadataPropertyHandling.GetValueOrDefault();
			}
			set
			{
				this._metadataPropertyHandling = new MetadataPropertyHandling?(value);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00041D88 File Offset: 0x0003FF88
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x00041D90 File Offset: 0x0003FF90
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return (FormatterAssemblyStyle)this.TypeNameAssemblyFormatHandling;
			}
			set
			{
				this.TypeNameAssemblyFormatHandling = (TypeNameAssemblyFormatHandling)value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00041D9C File Offset: 0x0003FF9C
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x00041DAC File Offset: 0x0003FFAC
		public TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._typeNameAssemblyFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameAssemblyFormatHandling = new TypeNameAssemblyFormatHandling?(value);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00041DBC File Offset: 0x0003FFBC
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x00041DCC File Offset: 0x0003FFCC
		public ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._constructorHandling.GetValueOrDefault();
			}
			set
			{
				this._constructorHandling = new ConstructorHandling?(value);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00041DDC File Offset: 0x0003FFDC
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x00041DE4 File Offset: 0x0003FFE4
		public IContractResolver ContractResolver { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00041DF0 File Offset: 0x0003FFF0
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x00041DF8 File Offset: 0x0003FFF8
		public IEqualityComparer EqualityComparer { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00041E04 File Offset: 0x00040004
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00041E1C File Offset: 0x0004001C
		[Obsolete("ReferenceResolver property is obsolete. Use the ReferenceResolverProvider property to set the IReferenceResolver: settings.ReferenceResolverProvider = () => resolver")]
		public IReferenceResolver ReferenceResolver
		{
			get
			{
				Func<IReferenceResolver> referenceResolverProvider = this.ReferenceResolverProvider;
				if (referenceResolverProvider == null)
				{
					return null;
				}
				return referenceResolverProvider();
			}
			set
			{
				this.ReferenceResolverProvider = ((value != null) ? (() => value) : null);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00041E60 File Offset: 0x00040060
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x00041E68 File Offset: 0x00040068
		public Func<IReferenceResolver> ReferenceResolverProvider { get; set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00041E74 File Offset: 0x00040074
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x00041E7C File Offset: 0x0004007C
		public ITraceWriter TraceWriter { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00041E88 File Offset: 0x00040088
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x00041ECC File Offset: 0x000400CC
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public SerializationBinder Binder
		{
			get
			{
				if (this.SerializationBinder == null)
				{
					return null;
				}
				SerializationBinderAdapter serializationBinderAdapter = this.SerializationBinder as SerializationBinderAdapter;
				if (serializationBinderAdapter != null)
				{
					return serializationBinderAdapter.SerializationBinder;
				}
				throw new InvalidOperationException("Cannot get SerializationBinder because an ISerializationBinder was previously set.");
			}
			set
			{
				this.SerializationBinder = ((value == null) ? null : new SerializationBinderAdapter(value));
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00041EE8 File Offset: 0x000400E8
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x00041EF0 File Offset: 0x000400F0
		public ISerializationBinder SerializationBinder { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00041EFC File Offset: 0x000400FC
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x00041F04 File Offset: 0x00040104
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public EventHandler<ErrorEventArgs> Error { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00041F10 File Offset: 0x00040110
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x00041F44 File Offset: 0x00040144
		public StreamingContext Context
		{
			get
			{
				StreamingContext? context = this._context;
				if (context == null)
				{
					return JsonSerializerSettings.DefaultContext;
				}
				return context.GetValueOrDefault();
			}
			set
			{
				this._context = new StreamingContext?(value);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00041F54 File Offset: 0x00040154
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x00041F68 File Offset: 0x00040168
		[Nullable(1)]
		public string DateFormatString
		{
			[NullableContext(1)]
			get
			{
				return this._dateFormatString ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
			}
			[NullableContext(1)]
			set
			{
				this._dateFormatString = value;
				this._dateFormatStringSet = true;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00041F78 File Offset: 0x00040178
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x00041F80 File Offset: 0x00040180
		public int? MaxDepth
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

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00041FD0 File Offset: 0x000401D0
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x00041FE0 File Offset: 0x000401E0
		public Formatting Formatting
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

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00041FF0 File Offset: 0x000401F0
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00042000 File Offset: 0x00040200
		public DateFormatHandling DateFormatHandling
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

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00042010 File Offset: 0x00040210
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x00042040 File Offset: 0x00040240
		public DateTimeZoneHandling DateTimeZoneHandling
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

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00042050 File Offset: 0x00040250
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x00042080 File Offset: 0x00040280
		public DateParseHandling DateParseHandling
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

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00042090 File Offset: 0x00040290
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x000420A0 File Offset: 0x000402A0
		public FloatFormatHandling FloatFormatHandling
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

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x000420B0 File Offset: 0x000402B0
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x000420C0 File Offset: 0x000402C0
		public FloatParseHandling FloatParseHandling
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

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x000420D0 File Offset: 0x000402D0
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x000420E0 File Offset: 0x000402E0
		public StringEscapeHandling StringEscapeHandling
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

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x000420F0 File Offset: 0x000402F0
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x00042104 File Offset: 0x00040304
		[Nullable(1)]
		public CultureInfo Culture
		{
			[NullableContext(1)]
			get
			{
				return this._culture ?? JsonSerializerSettings.DefaultCulture;
			}
			[NullableContext(1)]
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00042110 File Offset: 0x00040310
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x00042120 File Offset: 0x00040320
		public bool CheckAdditionalContent
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

		// Token: 0x06000B6F RID: 2927 RVA: 0x00042148 File Offset: 0x00040348
		[DebuggerStepThrough]
		public JsonSerializerSettings()
		{
			this.Converters = new List<JsonConverter>();
		}

		// Token: 0x0400060C RID: 1548
		internal const ReferenceLoopHandling DefaultReferenceLoopHandling = ReferenceLoopHandling.Error;

		// Token: 0x0400060D RID: 1549
		internal const MissingMemberHandling DefaultMissingMemberHandling = MissingMemberHandling.Ignore;

		// Token: 0x0400060E RID: 1550
		internal const NullValueHandling DefaultNullValueHandling = NullValueHandling.Include;

		// Token: 0x0400060F RID: 1551
		internal const DefaultValueHandling DefaultDefaultValueHandling = DefaultValueHandling.Include;

		// Token: 0x04000610 RID: 1552
		internal const ObjectCreationHandling DefaultObjectCreationHandling = ObjectCreationHandling.Auto;

		// Token: 0x04000611 RID: 1553
		internal const PreserveReferencesHandling DefaultPreserveReferencesHandling = PreserveReferencesHandling.None;

		// Token: 0x04000612 RID: 1554
		internal const ConstructorHandling DefaultConstructorHandling = ConstructorHandling.Default;

		// Token: 0x04000613 RID: 1555
		internal const TypeNameHandling DefaultTypeNameHandling = TypeNameHandling.None;

		// Token: 0x04000614 RID: 1556
		internal const MetadataPropertyHandling DefaultMetadataPropertyHandling = MetadataPropertyHandling.Default;

		// Token: 0x04000615 RID: 1557
		internal static readonly StreamingContext DefaultContext = default(StreamingContext);

		// Token: 0x04000616 RID: 1558
		internal const Formatting DefaultFormatting = Formatting.None;

		// Token: 0x04000617 RID: 1559
		internal const DateFormatHandling DefaultDateFormatHandling = DateFormatHandling.IsoDateFormat;

		// Token: 0x04000618 RID: 1560
		internal const DateTimeZoneHandling DefaultDateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;

		// Token: 0x04000619 RID: 1561
		internal const DateParseHandling DefaultDateParseHandling = DateParseHandling.DateTime;

		// Token: 0x0400061A RID: 1562
		internal const FloatParseHandling DefaultFloatParseHandling = FloatParseHandling.Double;

		// Token: 0x0400061B RID: 1563
		internal const FloatFormatHandling DefaultFloatFormatHandling = FloatFormatHandling.String;

		// Token: 0x0400061C RID: 1564
		internal const StringEscapeHandling DefaultStringEscapeHandling = StringEscapeHandling.Default;

		// Token: 0x0400061D RID: 1565
		internal const TypeNameAssemblyFormatHandling DefaultTypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

		// Token: 0x0400061E RID: 1566
		[Nullable(1)]
		internal static readonly CultureInfo DefaultCulture = CultureInfo.InvariantCulture;

		// Token: 0x0400061F RID: 1567
		internal const bool DefaultCheckAdditionalContent = false;

		// Token: 0x04000620 RID: 1568
		[Nullable(1)]
		internal const string DefaultDateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

		// Token: 0x04000621 RID: 1569
		internal Formatting? _formatting;

		// Token: 0x04000622 RID: 1570
		internal DateFormatHandling? _dateFormatHandling;

		// Token: 0x04000623 RID: 1571
		internal DateTimeZoneHandling? _dateTimeZoneHandling;

		// Token: 0x04000624 RID: 1572
		internal DateParseHandling? _dateParseHandling;

		// Token: 0x04000625 RID: 1573
		internal FloatFormatHandling? _floatFormatHandling;

		// Token: 0x04000626 RID: 1574
		internal FloatParseHandling? _floatParseHandling;

		// Token: 0x04000627 RID: 1575
		internal StringEscapeHandling? _stringEscapeHandling;

		// Token: 0x04000628 RID: 1576
		internal CultureInfo _culture;

		// Token: 0x04000629 RID: 1577
		internal bool? _checkAdditionalContent;

		// Token: 0x0400062A RID: 1578
		internal int? _maxDepth;

		// Token: 0x0400062B RID: 1579
		internal bool _maxDepthSet;

		// Token: 0x0400062C RID: 1580
		internal string _dateFormatString;

		// Token: 0x0400062D RID: 1581
		internal bool _dateFormatStringSet;

		// Token: 0x0400062E RID: 1582
		internal TypeNameAssemblyFormatHandling? _typeNameAssemblyFormatHandling;

		// Token: 0x0400062F RID: 1583
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x04000630 RID: 1584
		internal PreserveReferencesHandling? _preserveReferencesHandling;

		// Token: 0x04000631 RID: 1585
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x04000632 RID: 1586
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x04000633 RID: 1587
		internal MissingMemberHandling? _missingMemberHandling;

		// Token: 0x04000634 RID: 1588
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x04000635 RID: 1589
		internal StreamingContext? _context;

		// Token: 0x04000636 RID: 1590
		internal ConstructorHandling? _constructorHandling;

		// Token: 0x04000637 RID: 1591
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x04000638 RID: 1592
		internal MetadataPropertyHandling? _metadataPropertyHandling;
	}
}
