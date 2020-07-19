using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000225 RID: 549
	internal class BsonValue : BsonToken
	{
		// Token: 0x060015F5 RID: 5621 RVA: 0x00073488 File Offset: 0x00071688
		public BsonValue(object value, BsonType type)
		{
			this._value = value;
			this._type = type;
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x000734A0 File Offset: 0x000716A0
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x000734A8 File Offset: 0x000716A8
		public override BsonType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000994 RID: 2452
		private readonly object _value;

		// Token: 0x04000995 RID: 2453
		private readonly BsonType _type;
	}
}
