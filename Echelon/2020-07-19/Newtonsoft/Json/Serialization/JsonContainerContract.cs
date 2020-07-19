using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200019F RID: 415
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonContainerContract : JsonContract
	{
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x0005680C File Offset: 0x00054A0C
		// (set) Token: 0x06000F18 RID: 3864 RVA: 0x00056814 File Offset: 0x00054A14
		internal JsonContract ItemContract
		{
			get
			{
				return this._itemContract;
			}
			set
			{
				this._itemContract = value;
				if (this._itemContract != null)
				{
					this._finalItemContract = (this._itemContract.UnderlyingType.IsSealed() ? this._itemContract : null);
					return;
				}
				this._finalItemContract = null;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00056868 File Offset: 0x00054A68
		internal JsonContract FinalItemContract
		{
			get
			{
				return this._finalItemContract;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00056870 File Offset: 0x00054A70
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x00056878 File Offset: 0x00054A78
		public JsonConverter ItemConverter { get; set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00056884 File Offset: 0x00054A84
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x0005688C File Offset: 0x00054A8C
		public bool? ItemIsReference { get; set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x00056898 File Offset: 0x00054A98
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x000568A0 File Offset: 0x00054AA0
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x000568AC File Offset: 0x00054AAC
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x000568B4 File Offset: 0x00054AB4
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		// Token: 0x06000F22 RID: 3874 RVA: 0x000568C0 File Offset: 0x00054AC0
		[NullableContext(1)]
		internal JsonContainerContract(Type underlyingType) : base(underlyingType)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(underlyingType);
			if (cachedAttribute != null)
			{
				if (cachedAttribute.ItemConverterType != null)
				{
					this.ItemConverter = JsonTypeReflector.CreateJsonConverterInstance(cachedAttribute.ItemConverterType, cachedAttribute.ItemConverterParameters);
				}
				this.ItemIsReference = cachedAttribute._itemIsReference;
				this.ItemReferenceLoopHandling = cachedAttribute._itemReferenceLoopHandling;
				this.ItemTypeNameHandling = cachedAttribute._itemTypeNameHandling;
			}
		}

		// Token: 0x040007B1 RID: 1969
		private JsonContract _itemContract;

		// Token: 0x040007B2 RID: 1970
		private JsonContract _finalItemContract;
	}
}
