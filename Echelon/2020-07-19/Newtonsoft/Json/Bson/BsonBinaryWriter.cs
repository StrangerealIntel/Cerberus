using System;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200021E RID: 542
	internal class BsonBinaryWriter
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x00071E34 File Offset: 0x00070034
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x00071E3C File Offset: 0x0007003C
		public DateTimeKind DateTimeKindHandling { get; set; }

		// Token: 0x060015B3 RID: 5555 RVA: 0x00071E48 File Offset: 0x00070048
		public BsonBinaryWriter(BinaryWriter writer)
		{
			this.DateTimeKindHandling = DateTimeKind.Utc;
			this._writer = writer;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00071E60 File Offset: 0x00070060
		public void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00071E70 File Offset: 0x00070070
		public void Close()
		{
			this._writer.Close();
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00071E80 File Offset: 0x00070080
		public void WriteToken(BsonToken t)
		{
			this.CalculateSize(t);
			this.WriteTokenInternal(t);
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00071E94 File Offset: 0x00070094
		private void WriteTokenInternal(BsonToken t)
		{
			switch (t.Type)
			{
			case BsonType.Number:
			{
				BsonValue bsonValue = (BsonValue)t;
				this._writer.Write(Convert.ToDouble(bsonValue.Value, CultureInfo.InvariantCulture));
				return;
			}
			case BsonType.String:
			{
				BsonString bsonString = (BsonString)t;
				this.WriteString((string)bsonString.Value, bsonString.ByteCount, new int?(bsonString.CalculatedSize - 4));
				return;
			}
			case BsonType.Object:
			{
				BsonObject bsonObject = (BsonObject)t;
				this._writer.Write(bsonObject.CalculatedSize);
				foreach (BsonProperty bsonProperty in bsonObject)
				{
					this._writer.Write((sbyte)bsonProperty.Value.Type);
					this.WriteString((string)bsonProperty.Name.Value, bsonProperty.Name.ByteCount, null);
					this.WriteTokenInternal(bsonProperty.Value);
				}
				this._writer.Write(0);
				return;
			}
			case BsonType.Array:
			{
				BsonArray bsonArray = (BsonArray)t;
				this._writer.Write(bsonArray.CalculatedSize);
				ulong num = 0UL;
				foreach (BsonToken bsonToken in bsonArray)
				{
					this._writer.Write((sbyte)bsonToken.Type);
					this.WriteString(num.ToString(CultureInfo.InvariantCulture), MathUtils.IntLength(num), null);
					this.WriteTokenInternal(bsonToken);
					num += 1UL;
				}
				this._writer.Write(0);
				return;
			}
			case BsonType.Binary:
			{
				BsonBinary bsonBinary = (BsonBinary)t;
				byte[] array = (byte[])bsonBinary.Value;
				this._writer.Write(array.Length);
				this._writer.Write((byte)bsonBinary.BinaryType);
				this._writer.Write(array);
				return;
			}
			case BsonType.Undefined:
			case BsonType.Null:
				return;
			case BsonType.Oid:
			{
				byte[] buffer = (byte[])((BsonValue)t).Value;
				this._writer.Write(buffer);
				return;
			}
			case BsonType.Boolean:
				this._writer.Write(t == BsonBoolean.True);
				return;
			case BsonType.Date:
			{
				BsonValue bsonValue2 = (BsonValue)t;
				object value = bsonValue2.Value;
				long value2;
				if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					if (this.DateTimeKindHandling == DateTimeKind.Utc)
					{
						dateTime = dateTime.ToUniversalTime();
					}
					else if (this.DateTimeKindHandling == DateTimeKind.Local)
					{
						dateTime = dateTime.ToLocalTime();
					}
					value2 = DateTimeUtils.ConvertDateTimeToJavaScriptTicks(dateTime, false);
				}
				else
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)bsonValue2.Value;
					value2 = DateTimeUtils.ConvertDateTimeToJavaScriptTicks(dateTimeOffset.UtcDateTime, dateTimeOffset.Offset);
				}
				this._writer.Write(value2);
				return;
			}
			case BsonType.Regex:
			{
				BsonRegex bsonRegex = (BsonRegex)t;
				this.WriteString((string)bsonRegex.Pattern.Value, bsonRegex.Pattern.ByteCount, null);
				this.WriteString((string)bsonRegex.Options.Value, bsonRegex.Options.ByteCount, null);
				return;
			}
			case BsonType.Integer:
			{
				BsonValue bsonValue3 = (BsonValue)t;
				this._writer.Write(Convert.ToInt32(bsonValue3.Value, CultureInfo.InvariantCulture));
				return;
			}
			case BsonType.Long:
			{
				BsonValue bsonValue4 = (BsonValue)t;
				this._writer.Write(Convert.ToInt64(bsonValue4.Value, CultureInfo.InvariantCulture));
				return;
			}
			}
			throw new ArgumentOutOfRangeException("t", "Unexpected token when writing BSON: {0}".FormatWith(CultureInfo.InvariantCulture, t.Type));
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00072294 File Offset: 0x00070494
		private void WriteString(string s, int byteCount, int? calculatedlengthPrefix)
		{
			if (calculatedlengthPrefix != null)
			{
				this._writer.Write(calculatedlengthPrefix.GetValueOrDefault());
			}
			this.WriteUtf8Bytes(s, byteCount);
			this._writer.Write(0);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x000722C8 File Offset: 0x000704C8
		public void WriteUtf8Bytes(string s, int byteCount)
		{
			if (s != null)
			{
				if (byteCount <= 256)
				{
					if (this._largeByteBuffer == null)
					{
						this._largeByteBuffer = new byte[256];
					}
					BsonBinaryWriter.Encoding.GetBytes(s, 0, s.Length, this._largeByteBuffer, 0);
					this._writer.Write(this._largeByteBuffer, 0, byteCount);
					return;
				}
				byte[] bytes = BsonBinaryWriter.Encoding.GetBytes(s);
				this._writer.Write(bytes);
			}
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0007234C File Offset: 0x0007054C
		private int CalculateSize(int stringByteCount)
		{
			return stringByteCount + 1;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00072354 File Offset: 0x00070554
		private int CalculateSizeWithLength(int stringByteCount, bool includeSize)
		{
			return (includeSize ? 5 : 1) + stringByteCount;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00072368 File Offset: 0x00070568
		private int CalculateSize(BsonToken t)
		{
			switch (t.Type)
			{
			case BsonType.Number:
				return 8;
			case BsonType.String:
			{
				BsonString bsonString = (BsonString)t;
				string text = (string)bsonString.Value;
				bsonString.ByteCount = ((text != null) ? BsonBinaryWriter.Encoding.GetByteCount(text) : 0);
				bsonString.CalculatedSize = this.CalculateSizeWithLength(bsonString.ByteCount, bsonString.IncludeLength);
				return bsonString.CalculatedSize;
			}
			case BsonType.Object:
			{
				BsonObject bsonObject = (BsonObject)t;
				int num = 4;
				foreach (BsonProperty bsonProperty in bsonObject)
				{
					int num2 = 1;
					num2 += this.CalculateSize(bsonProperty.Name);
					num2 += this.CalculateSize(bsonProperty.Value);
					num += num2;
				}
				num++;
				bsonObject.CalculatedSize = num;
				return num;
			}
			case BsonType.Array:
			{
				BsonArray bsonArray = (BsonArray)t;
				int num3 = 4;
				ulong num4 = 0UL;
				foreach (BsonToken t2 in bsonArray)
				{
					num3++;
					num3 += this.CalculateSize(MathUtils.IntLength(num4));
					num3 += this.CalculateSize(t2);
					num4 += 1UL;
				}
				num3++;
				bsonArray.CalculatedSize = num3;
				return bsonArray.CalculatedSize;
			}
			case BsonType.Binary:
			{
				BsonBinary bsonBinary = (BsonBinary)t;
				byte[] array = (byte[])bsonBinary.Value;
				bsonBinary.CalculatedSize = 5 + array.Length;
				return bsonBinary.CalculatedSize;
			}
			case BsonType.Undefined:
			case BsonType.Null:
				return 0;
			case BsonType.Oid:
				return 12;
			case BsonType.Boolean:
				return 1;
			case BsonType.Date:
				return 8;
			case BsonType.Regex:
			{
				BsonRegex bsonRegex = (BsonRegex)t;
				int num5 = 0;
				num5 += this.CalculateSize(bsonRegex.Pattern);
				num5 += this.CalculateSize(bsonRegex.Options);
				bsonRegex.CalculatedSize = num5;
				return bsonRegex.CalculatedSize;
			}
			case BsonType.Integer:
				return 4;
			case BsonType.Long:
				return 8;
			}
			throw new ArgumentOutOfRangeException("t", "Unexpected token when writing BSON: {0}".FormatWith(CultureInfo.InvariantCulture, t.Type));
		}

		// Token: 0x04000979 RID: 2425
		private static readonly Encoding Encoding = new UTF8Encoding(false);

		// Token: 0x0400097A RID: 2426
		private readonly BinaryWriter _writer;

		// Token: 0x0400097B RID: 2427
		private byte[] _largeByteBuffer;
	}
}
