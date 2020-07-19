using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200022C RID: 556
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonWriter : JsonWriter
	{
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x000735B0 File Offset: 0x000717B0
		// (set) Token: 0x0600160D RID: 5645 RVA: 0x000735C0 File Offset: 0x000717C0
		public DateTimeKind DateTimeKindHandling
		{
			get
			{
				return this._writer.DateTimeKindHandling;
			}
			set
			{
				this._writer.DateTimeKindHandling = value;
			}
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x000735D0 File Offset: 0x000717D0
		public BsonWriter(Stream stream)
		{
			ValidationUtils.ArgumentNotNull(stream, "stream");
			this._writer = new BsonBinaryWriter(new BinaryWriter(stream));
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x000735F4 File Offset: 0x000717F4
		public BsonWriter(BinaryWriter writer)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			this._writer = new BsonBinaryWriter(writer);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00073614 File Offset: 0x00071814
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00073624 File Offset: 0x00071824
		protected override void WriteEnd(JsonToken token)
		{
			base.WriteEnd(token);
			this.RemoveParent();
			if (base.Top == 0)
			{
				this._writer.WriteToken(this._root);
			}
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00073650 File Offset: 0x00071850
		public override void WriteComment(string text)
		{
			throw JsonWriterException.Create(this, "Cannot write JSON comment as BSON.", null);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00073660 File Offset: 0x00071860
		public override void WriteStartConstructor(string name)
		{
			throw JsonWriterException.Create(this, "Cannot write JSON constructor as BSON.", null);
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00073670 File Offset: 0x00071870
		public override void WriteRaw(string json)
		{
			throw JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00073680 File Offset: 0x00071880
		public override void WriteRawValue(string json)
		{
			throw JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00073690 File Offset: 0x00071890
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new BsonArray());
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x000736A4 File Offset: 0x000718A4
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new BsonObject());
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x000736B8 File Offset: 0x000718B8
		public override void WritePropertyName(string name)
		{
			base.WritePropertyName(name);
			this._propertyName = name;
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x000736C8 File Offset: 0x000718C8
		public override void Close()
		{
			base.Close();
			if (base.CloseOutput)
			{
				BsonBinaryWriter writer = this._writer;
				if (writer == null)
				{
					return;
				}
				writer.Close();
			}
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x000736F0 File Offset: 0x000718F0
		private void AddParent(BsonToken container)
		{
			this.AddToken(container);
			this._parent = container;
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00073700 File Offset: 0x00071900
		private void RemoveParent()
		{
			this._parent = this._parent.Parent;
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00073714 File Offset: 0x00071914
		private void AddValue(object value, BsonType type)
		{
			this.AddToken(new BsonValue(value, type));
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00073724 File Offset: 0x00071924
		internal void AddToken(BsonToken token)
		{
			if (this._parent != null)
			{
				BsonObject bsonObject = this._parent as BsonObject;
				if (bsonObject != null)
				{
					bsonObject.Add(this._propertyName, token);
					this._propertyName = null;
					return;
				}
				((BsonArray)this._parent).Add(token);
				return;
			}
			else
			{
				if (token.Type != BsonType.Object && token.Type != BsonType.Array)
				{
					throw JsonWriterException.Create(this, "Error writing {0} value. BSON must start with an Object or Array.".FormatWith(CultureInfo.InvariantCulture, token.Type), null);
				}
				this._parent = token;
				this._root = token;
				return;
			}
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x000737C4 File Offset: 0x000719C4
		public override void WriteValue(object value)
		{
			if (value is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger bigInteger = (System.Numerics.BigInteger)value;
				base.SetWriteState(JsonToken.Integer, null);
				this.AddToken(new BsonBinary(bigInteger.ToByteArray(), BsonBinaryType.Binary));
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0007380C File Offset: 0x00071A0C
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddToken(BsonEmpty.Null);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00073820 File Offset: 0x00071A20
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddToken(BsonEmpty.Undefined);
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00073834 File Offset: 0x00071A34
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddToken((value == null) ? BsonEmpty.Null : new BsonString(value, true));
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0007385C File Offset: 0x00071A5C
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x00073874 File Offset: 0x00071A74
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			if (value > 2147483647u)
			{
				throw JsonWriterException.Create(this, "Value is too large to fit in a signed 32 bit integer. BSON does not support unsigned values.", null);
			}
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x000738A4 File Offset: 0x00071AA4
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000738BC File Offset: 0x00071ABC
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			if (value > 9223372036854775807UL)
			{
				throw JsonWriterException.Create(this, "Value is too large to fit in a signed 64 bit integer. BSON does not support unsigned values.", null);
			}
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x000738F0 File Offset: 0x00071AF0
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00073908 File Offset: 0x00071B08
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00073920 File Offset: 0x00071B20
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddToken(value ? BsonBoolean.True : BsonBoolean.False);
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00073944 File Offset: 0x00071B44
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0007395C File Offset: 0x00071B5C
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00073974 File Offset: 0x00071B74
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string value2 = value.ToString(CultureInfo.InvariantCulture);
			this.AddToken(new BsonString(value2, true));
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000739A8 File Offset: 0x00071BA8
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x000739C0 File Offset: 0x00071BC0
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x000739D8 File Offset: 0x00071BD8
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000739F0 File Offset: 0x00071BF0
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddValue(value, BsonType.Date);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x00073A18 File Offset: 0x00071C18
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Date);
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x00073A30 File Offset: 0x00071C30
		public override void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.WriteValue(value);
			this.AddToken(new BsonBinary(value, BsonBinaryType.Binary));
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00073A54 File Offset: 0x00071C54
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonBinary(value.ToByteArray(), BsonBinaryType.Uuid));
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x00073A70 File Offset: 0x00071C70
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00073A94 File Offset: 0x00071C94
		public override void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x00073AD4 File Offset: 0x00071CD4
		public void WriteObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw JsonWriterException.Create(this, "An object id must be 12 bytes", null);
			}
			base.SetWriteState(JsonToken.Undefined, null);
			this.AddValue(value, BsonType.Oid);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00073B0C File Offset: 0x00071D0C
		public void WriteRegex(string pattern, string options)
		{
			ValidationUtils.ArgumentNotNull(pattern, "pattern");
			base.SetWriteState(JsonToken.Undefined, null);
			this.AddToken(new BsonRegex(pattern, options));
		}

		// Token: 0x040009B4 RID: 2484
		private readonly BsonBinaryWriter _writer;

		// Token: 0x040009B5 RID: 2485
		private BsonToken _root;

		// Token: 0x040009B6 RID: 2486
		private BsonToken _parent;

		// Token: 0x040009B7 RID: 2487
		private string _propertyName;
	}
}
