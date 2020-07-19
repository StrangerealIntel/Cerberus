using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001AB RID: 427
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonObjectContract : JsonContainerContract
	{
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x000574C0 File Offset: 0x000556C0
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x000574C8 File Offset: 0x000556C8
		public MemberSerialization MemberSerialization { get; set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x000574D4 File Offset: 0x000556D4
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x000574DC File Offset: 0x000556DC
		public MissingMemberHandling? MissingMemberHandling { get; set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x000574E8 File Offset: 0x000556E8
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x000574F0 File Offset: 0x000556F0
		public Required? ItemRequired { get; set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x000574FC File Offset: 0x000556FC
		// (set) Token: 0x06000F83 RID: 3971 RVA: 0x00057504 File Offset: 0x00055704
		public NullValueHandling? ItemNullValueHandling { get; set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00057510 File Offset: 0x00055710
		[Nullable(1)]
		public JsonPropertyCollection Properties { [NullableContext(1)] get; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00057518 File Offset: 0x00055718
		[Nullable(1)]
		public JsonPropertyCollection CreatorParameters
		{
			[NullableContext(1)]
			get
			{
				if (this._creatorParameters == null)
				{
					this._creatorParameters = new JsonPropertyCollection(base.UnderlyingType);
				}
				return this._creatorParameters;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0005753C File Offset: 0x0005573C
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x00057544 File Offset: 0x00055744
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public ObjectConstructor<object> OverrideCreator
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				return this._overrideCreator;
			}
			[param: Nullable(new byte[]
			{
				2,
				1
			})]
			set
			{
				this._overrideCreator = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00057550 File Offset: 0x00055750
		// (set) Token: 0x06000F89 RID: 3977 RVA: 0x00057558 File Offset: 0x00055758
		[Nullable(new byte[]
		{
			2,
			1
		})]
		internal ObjectConstructor<object> ParameterizedCreator
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				return this._parameterizedCreator;
			}
			[param: Nullable(new byte[]
			{
				2,
				1
			})]
			set
			{
				this._parameterizedCreator = value;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00057564 File Offset: 0x00055764
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x0005756C File Offset: 0x0005576C
		public ExtensionDataSetter ExtensionDataSetter { get; set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00057578 File Offset: 0x00055778
		// (set) Token: 0x06000F8D RID: 3981 RVA: 0x00057580 File Offset: 0x00055780
		public ExtensionDataGetter ExtensionDataGetter { get; set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0005758C File Offset: 0x0005578C
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x00057594 File Offset: 0x00055794
		public Type ExtensionDataValueType
		{
			get
			{
				return this._extensionDataValueType;
			}
			set
			{
				this._extensionDataValueType = value;
				this.ExtensionDataIsJToken = (value != null && typeof(JToken).IsAssignableFrom(value));
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x000575C8 File Offset: 0x000557C8
		// (set) Token: 0x06000F91 RID: 3985 RVA: 0x000575D0 File Offset: 0x000557D0
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public Func<string, string> ExtensionDataNameResolver { [return: Nullable(new byte[]
		{
			2,
			1,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			1
		})] set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x000575DC File Offset: 0x000557DC
		internal bool HasRequiredOrDefaultValueProperties
		{
			get
			{
				if (this._hasRequiredOrDefaultValueProperties == null)
				{
					this._hasRequiredOrDefaultValueProperties = new bool?(false);
					if (this.ItemRequired.GetValueOrDefault(Required.Default) != Required.Default)
					{
						this._hasRequiredOrDefaultValueProperties = new bool?(true);
					}
					else
					{
						foreach (JsonProperty jsonProperty in this.Properties)
						{
							if (jsonProperty.Required == Required.Default)
							{
								DefaultValueHandling? defaultValueHandling = jsonProperty.DefaultValueHandling & DefaultValueHandling.Populate;
								DefaultValueHandling defaultValueHandling2 = DefaultValueHandling.Populate;
								if (!(defaultValueHandling.GetValueOrDefault() == defaultValueHandling2 & defaultValueHandling != null))
								{
									continue;
								}
							}
							this._hasRequiredOrDefaultValueProperties = new bool?(true);
							break;
						}
					}
				}
				return this._hasRequiredOrDefaultValueProperties.GetValueOrDefault();
			}
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x000576E0 File Offset: 0x000558E0
		[NullableContext(1)]
		public JsonObjectContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Object;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00057704 File Offset: 0x00055904
		[NullableContext(1)]
		[SecuritySafeCritical]
		internal object GetUninitializedObject()
		{
			if (!JsonTypeReflector.FullyTrusted)
			{
				throw new JsonException("Insufficient permissions. Creating an uninitialized '{0}' type requires full trust.".FormatWith(CultureInfo.InvariantCulture, this.NonNullableUnderlyingType));
			}
			return FormatterServices.GetUninitializedObject(this.NonNullableUnderlyingType);
		}

		// Token: 0x040007F3 RID: 2035
		internal bool ExtensionDataIsJToken;

		// Token: 0x040007F4 RID: 2036
		private bool? _hasRequiredOrDefaultValueProperties;

		// Token: 0x040007F5 RID: 2037
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _overrideCreator;

		// Token: 0x040007F6 RID: 2038
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _parameterizedCreator;

		// Token: 0x040007F7 RID: 2039
		private JsonPropertyCollection _creatorParameters;

		// Token: 0x040007F8 RID: 2040
		private Type _extensionDataValueType;
	}
}
