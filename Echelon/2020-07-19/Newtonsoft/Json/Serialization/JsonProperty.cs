using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001AD RID: 429
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonProperty
	{
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x000578AC File Offset: 0x00055AAC
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x000578B4 File Offset: 0x00055AB4
		internal JsonContract PropertyContract { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x000578C0 File Offset: 0x00055AC0
		// (set) Token: 0x06000F9C RID: 3996 RVA: 0x000578C8 File Offset: 0x00055AC8
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
			set
			{
				this._propertyName = value;
				this._skipPropertyNameEscape = !JavaScriptUtils.ShouldEscapeJavaScriptString(this._propertyName, JavaScriptUtils.HtmlCharEscapeFlags);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x000578EC File Offset: 0x00055AEC
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x000578F4 File Offset: 0x00055AF4
		public Type DeclaringType { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00057900 File Offset: 0x00055B00
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x00057908 File Offset: 0x00055B08
		public int? Order { get; set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00057914 File Offset: 0x00055B14
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x0005791C File Offset: 0x00055B1C
		public string UnderlyingName { get; set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00057928 File Offset: 0x00055B28
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x00057930 File Offset: 0x00055B30
		public IValueProvider ValueProvider { get; set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0005793C File Offset: 0x00055B3C
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x00057944 File Offset: 0x00055B44
		public IAttributeProvider AttributeProvider { get; set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00057950 File Offset: 0x00055B50
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x00057958 File Offset: 0x00055B58
		public Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
			set
			{
				if (this._propertyType != value)
				{
					this._propertyType = value;
					this._hasGeneratedDefaultValue = false;
				}
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0005797C File Offset: 0x00055B7C
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x00057984 File Offset: 0x00055B84
		public JsonConverter Converter { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00057990 File Offset: 0x00055B90
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x00057998 File Offset: 0x00055B98
		[Obsolete("MemberConverter is obsolete. Use Converter instead.")]
		public JsonConverter MemberConverter
		{
			get
			{
				return this.Converter;
			}
			set
			{
				this.Converter = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x000579A4 File Offset: 0x00055BA4
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x000579AC File Offset: 0x00055BAC
		public bool Ignored { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x000579B8 File Offset: 0x00055BB8
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x000579C0 File Offset: 0x00055BC0
		public bool Readable { get; set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x000579CC File Offset: 0x00055BCC
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x000579D4 File Offset: 0x00055BD4
		public bool Writable { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x000579E0 File Offset: 0x00055BE0
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x000579E8 File Offset: 0x00055BE8
		public bool HasMemberAttribute { get; set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x000579F4 File Offset: 0x00055BF4
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x00057A0C File Offset: 0x00055C0C
		public object DefaultValue
		{
			get
			{
				if (!this._hasExplicitDefaultValue)
				{
					return null;
				}
				return this._defaultValue;
			}
			set
			{
				this._hasExplicitDefaultValue = true;
				this._defaultValue = value;
			}
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00057A1C File Offset: 0x00055C1C
		internal object GetResolvedDefaultValue()
		{
			if (this._propertyType == null)
			{
				return null;
			}
			if (!this._hasExplicitDefaultValue && !this._hasGeneratedDefaultValue)
			{
				this._defaultValue = ReflectionUtils.GetDefaultValue(this._propertyType);
				this._hasGeneratedDefaultValue = true;
			}
			return this._defaultValue;
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00057A74 File Offset: 0x00055C74
		// (set) Token: 0x06000FB9 RID: 4025 RVA: 0x00057A84 File Offset: 0x00055C84
		public Required Required
		{
			get
			{
				return this._required.GetValueOrDefault();
			}
			set
			{
				this._required = new Required?(value);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00057A94 File Offset: 0x00055C94
		public bool IsRequiredSpecified
		{
			get
			{
				return this._required != null;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00057AA4 File Offset: 0x00055CA4
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x00057AAC File Offset: 0x00055CAC
		public bool? IsReference { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00057AB8 File Offset: 0x00055CB8
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x00057AC0 File Offset: 0x00055CC0
		public NullValueHandling? NullValueHandling { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00057ACC File Offset: 0x00055CCC
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x00057AD4 File Offset: 0x00055CD4
		public DefaultValueHandling? DefaultValueHandling { get; set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00057AE0 File Offset: 0x00055CE0
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x00057AE8 File Offset: 0x00055CE8
		public ReferenceLoopHandling? ReferenceLoopHandling { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00057AF4 File Offset: 0x00055CF4
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x00057AFC File Offset: 0x00055CFC
		public ObjectCreationHandling? ObjectCreationHandling { get; set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00057B08 File Offset: 0x00055D08
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x00057B10 File Offset: 0x00055D10
		public TypeNameHandling? TypeNameHandling { get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00057B1C File Offset: 0x00055D1C
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x00057B24 File Offset: 0x00055D24
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public Predicate<object> ShouldSerialize { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00057B30 File Offset: 0x00055D30
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x00057B38 File Offset: 0x00055D38
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public Predicate<object> ShouldDeserialize { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00057B44 File Offset: 0x00055D44
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x00057B4C File Offset: 0x00055D4C
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public Predicate<object> GetIsSpecified { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00057B58 File Offset: 0x00055D58
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x00057B60 File Offset: 0x00055D60
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		public Action<object, object> SetIsSpecified { [return: Nullable(new byte[]
		{
			2,
			1,
			2
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			2
		})] set; }

		// Token: 0x06000FCF RID: 4047 RVA: 0x00057B6C File Offset: 0x00055D6C
		[NullableContext(1)]
		public override string ToString()
		{
			return this.PropertyName ?? string.Empty;
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00057B80 File Offset: 0x00055D80
		// (set) Token: 0x06000FD1 RID: 4049 RVA: 0x00057B88 File Offset: 0x00055D88
		public JsonConverter ItemConverter { get; set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00057B94 File Offset: 0x00055D94
		// (set) Token: 0x06000FD3 RID: 4051 RVA: 0x00057B9C File Offset: 0x00055D9C
		public bool? ItemIsReference { get; set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00057BA8 File Offset: 0x00055DA8
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x00057BB0 File Offset: 0x00055DB0
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00057BBC File Offset: 0x00055DBC
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x00057BC4 File Offset: 0x00055DC4
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00057BD0 File Offset: 0x00055DD0
		[NullableContext(1)]
		internal void WritePropertyName(JsonWriter writer)
		{
			string propertyName = this.PropertyName;
			if (this._skipPropertyNameEscape)
			{
				writer.WritePropertyName(propertyName, false);
				return;
			}
			writer.WritePropertyName(propertyName);
		}

		// Token: 0x040007FB RID: 2043
		internal Required? _required;

		// Token: 0x040007FC RID: 2044
		internal bool _hasExplicitDefaultValue;

		// Token: 0x040007FD RID: 2045
		private object _defaultValue;

		// Token: 0x040007FE RID: 2046
		private bool _hasGeneratedDefaultValue;

		// Token: 0x040007FF RID: 2047
		private string _propertyName;

		// Token: 0x04000800 RID: 2048
		internal bool _skipPropertyNameEscape;

		// Token: 0x04000801 RID: 2049
		private Type _propertyType;
	}
}
