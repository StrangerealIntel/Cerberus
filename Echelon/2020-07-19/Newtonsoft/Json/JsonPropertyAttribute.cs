using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000142 RID: 322
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class JsonPropertyAttribute : Attribute
	{
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0003EFBC File Offset: 0x0003D1BC
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x0003EFC4 File Offset: 0x0003D1C4
		public Type ItemConverterType { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0003EFD0 File Offset: 0x0003D1D0
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x0003EFD8 File Offset: 0x0003D1D8
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public object[] ItemConverterParameters { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0003EFE4 File Offset: 0x0003D1E4
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x0003EFEC File Offset: 0x0003D1EC
		public Type NamingStrategyType { get; set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0003EFF8 File Offset: 0x0003D1F8
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x0003F000 File Offset: 0x0003D200
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public object[] NamingStrategyParameters { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0003F00C File Offset: 0x0003D20C
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x0003F01C File Offset: 0x0003D21C
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

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0003F02C File Offset: 0x0003D22C
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x0003F03C File Offset: 0x0003D23C
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

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0003F04C File Offset: 0x0003D24C
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x0003F05C File Offset: 0x0003D25C
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

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0003F06C File Offset: 0x0003D26C
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x0003F07C File Offset: 0x0003D27C
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

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0003F08C File Offset: 0x0003D28C
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x0003F09C File Offset: 0x0003D29C
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

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0003F0AC File Offset: 0x0003D2AC
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x0003F0BC File Offset: 0x0003D2BC
		public bool IsReference
		{
			get
			{
				return this._isReference.GetValueOrDefault();
			}
			set
			{
				this._isReference = new bool?(value);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0003F0CC File Offset: 0x0003D2CC
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x0003F0DC File Offset: 0x0003D2DC
		public int Order
		{
			get
			{
				return this._order.GetValueOrDefault();
			}
			set
			{
				this._order = new int?(value);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0003F0EC File Offset: 0x0003D2EC
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x0003F0FC File Offset: 0x0003D2FC
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

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0003F10C File Offset: 0x0003D30C
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x0003F114 File Offset: 0x0003D314
		public string PropertyName { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0003F120 File Offset: 0x0003D320
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x0003F130 File Offset: 0x0003D330
		public ReferenceLoopHandling ItemReferenceLoopHandling
		{
			get
			{
				return this._itemReferenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._itemReferenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0003F140 File Offset: 0x0003D340
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x0003F150 File Offset: 0x0003D350
		public TypeNameHandling ItemTypeNameHandling
		{
			get
			{
				return this._itemTypeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._itemTypeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0003F160 File Offset: 0x0003D360
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x0003F170 File Offset: 0x0003D370
		public bool ItemIsReference
		{
			get
			{
				return this._itemIsReference.GetValueOrDefault();
			}
			set
			{
				this._itemIsReference = new bool?(value);
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0003F180 File Offset: 0x0003D380
		public JsonPropertyAttribute()
		{
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0003F188 File Offset: 0x0003D388
		[NullableContext(1)]
		public JsonPropertyAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x040005C8 RID: 1480
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x040005C9 RID: 1481
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x040005CA RID: 1482
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x040005CB RID: 1483
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x040005CC RID: 1484
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x040005CD RID: 1485
		internal bool? _isReference;

		// Token: 0x040005CE RID: 1486
		internal int? _order;

		// Token: 0x040005CF RID: 1487
		internal Required? _required;

		// Token: 0x040005D0 RID: 1488
		internal bool? _itemIsReference;

		// Token: 0x040005D1 RID: 1489
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x040005D2 RID: 1490
		internal TypeNameHandling? _itemTypeNameHandling;
	}
}
