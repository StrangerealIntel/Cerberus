using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000220 RID: 544
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonReader : JsonReader
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00072620 File Offset: 0x00070820
		// (set) Token: 0x060015C1 RID: 5569 RVA: 0x00072628 File Offset: 0x00070828
		[Obsolete("JsonNet35BinaryCompatibility will be removed in a future version of Json.NET.")]
		public bool JsonNet35BinaryCompatibility
		{
			get
			{
				return this._jsonNet35BinaryCompatibility;
			}
			set
			{
				this._jsonNet35BinaryCompatibility = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00072634 File Offset: 0x00070834
		// (set) Token: 0x060015C3 RID: 5571 RVA: 0x0007263C File Offset: 0x0007083C
		public bool ReadRootValueAsArray
		{
			get
			{
				return this._readRootValueAsArray;
			}
			set
			{
				this._readRootValueAsArray = value;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00072648 File Offset: 0x00070848
		// (set) Token: 0x060015C5 RID: 5573 RVA: 0x00072650 File Offset: 0x00070850
		public DateTimeKind DateTimeKindHandling
		{
			get
			{
				return this._dateTimeKindHandling;
			}
			set
			{
				this._dateTimeKindHandling = value;
			}
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0007265C File Offset: 0x0007085C
		public BsonReader(Stream stream) : this(stream, false, DateTimeKind.Local)
		{
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00072668 File Offset: 0x00070868
		public BsonReader(BinaryReader reader) : this(reader, false, DateTimeKind.Local)
		{
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00072674 File Offset: 0x00070874
		public BsonReader(Stream stream, bool readRootValueAsArray, DateTimeKind dateTimeKindHandling)
		{
			ValidationUtils.ArgumentNotNull(stream, "stream");
			this._reader = new BinaryReader(stream);
			this._stack = new List<BsonReader.ContainerContext>();
			this._readRootValueAsArray = readRootValueAsArray;
			this._dateTimeKindHandling = dateTimeKindHandling;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x000726AC File Offset: 0x000708AC
		public BsonReader(BinaryReader reader, bool readRootValueAsArray, DateTimeKind dateTimeKindHandling)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this._reader = reader;
			this._stack = new List<BsonReader.ContainerContext>();
			this._readRootValueAsArray = readRootValueAsArray;
			this._dateTimeKindHandling = dateTimeKindHandling;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x000726E0 File Offset: 0x000708E0
		private string ReadElement()
		{
			this._currentElementType = this.ReadType();
			return this.ReadString();
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x000726F4 File Offset: 0x000708F4
		public override bool Read()
		{
			bool result;
			try
			{
				bool flag;
				switch (this._bsonReaderState)
				{
				case BsonReader.BsonReaderState.Normal:
					flag = this.ReadNormal();
					break;
				case BsonReader.BsonReaderState.ReferenceStart:
				case BsonReader.BsonReaderState.ReferenceRef:
				case BsonReader.BsonReaderState.ReferenceId:
					flag = this.ReadReference();
					break;
				case BsonReader.BsonReaderState.CodeWScopeStart:
				case BsonReader.BsonReaderState.CodeWScopeCode:
				case BsonReader.BsonReaderState.CodeWScopeScope:
				case BsonReader.BsonReaderState.CodeWScopeScopeObject:
				case BsonReader.BsonReaderState.CodeWScopeScopeEnd:
					flag = this.ReadCodeWScope();
					break;
				default:
					throw JsonReaderException.Create(this, "Unexpected state: {0}".FormatWith(CultureInfo.InvariantCulture, this._bsonReaderState));
				}
				if (!flag)
				{
					base.SetToken(JsonToken.None);
					result = false;
				}
				else
				{
					result = true;
				}
			}
			catch (EndOfStreamException)
			{
				base.SetToken(JsonToken.None);
				result = false;
			}
			return result;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x000727B8 File Offset: 0x000709B8
		public override void Close()
		{
			base.Close();
			if (base.CloseInput)
			{
				BinaryReader reader = this._reader;
				if (reader == null)
				{
					return;
				}
				reader.Close();
			}
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x000727E0 File Offset: 0x000709E0
		private bool ReadCodeWScope()
		{
			switch (this._bsonReaderState)
			{
			case BsonReader.BsonReaderState.CodeWScopeStart:
				base.SetToken(JsonToken.PropertyName, "$code");
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeCode;
				return true;
			case BsonReader.BsonReaderState.CodeWScopeCode:
				this.ReadInt32();
				base.SetToken(JsonToken.String, this.ReadLengthString());
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeScope;
				return true;
			case BsonReader.BsonReaderState.CodeWScopeScope:
			{
				if (base.CurrentState == JsonReader.State.PostValue)
				{
					base.SetToken(JsonToken.PropertyName, "$scope");
					return true;
				}
				base.SetToken(JsonToken.StartObject);
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeScopeObject;
				BsonReader.ContainerContext containerContext = new BsonReader.ContainerContext(BsonType.Object);
				this.PushContext(containerContext);
				containerContext.Length = this.ReadInt32();
				return true;
			}
			case BsonReader.BsonReaderState.CodeWScopeScopeObject:
			{
				bool flag = this.ReadNormal();
				if (flag && this.TokenType == JsonToken.EndObject)
				{
					this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeScopeEnd;
				}
				return flag;
			}
			case BsonReader.BsonReaderState.CodeWScopeScopeEnd:
				base.SetToken(JsonToken.EndObject);
				this._bsonReaderState = BsonReader.BsonReaderState.Normal;
				return true;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x000728C8 File Offset: 0x00070AC8
		private bool ReadReference()
		{
			JsonReader.State currentState = base.CurrentState;
			if (currentState != JsonReader.State.Property)
			{
				if (currentState == JsonReader.State.ObjectStart)
				{
					base.SetToken(JsonToken.PropertyName, "$ref");
					this._bsonReaderState = BsonReader.BsonReaderState.ReferenceRef;
					return true;
				}
				if (currentState != JsonReader.State.PostValue)
				{
					throw JsonReaderException.Create(this, "Unexpected state when reading BSON reference: " + base.CurrentState.ToString());
				}
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceRef)
				{
					base.SetToken(JsonToken.PropertyName, "$id");
					this._bsonReaderState = BsonReader.BsonReaderState.ReferenceId;
					return true;
				}
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceId)
				{
					base.SetToken(JsonToken.EndObject);
					this._bsonReaderState = BsonReader.BsonReaderState.Normal;
					return true;
				}
				throw JsonReaderException.Create(this, "Unexpected state when reading BSON reference: " + this._bsonReaderState.ToString());
			}
			else
			{
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceRef)
				{
					base.SetToken(JsonToken.String, this.ReadLengthString());
					return true;
				}
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceId)
				{
					base.SetToken(JsonToken.Bytes, this.ReadBytes(12));
					return true;
				}
				throw JsonReaderException.Create(this, "Unexpected state when reading BSON reference: " + this._bsonReaderState.ToString());
			}
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000729F0 File Offset: 0x00070BF0
		private bool ReadNormal()
		{
			switch (base.CurrentState)
			{
			case JsonReader.State.Start:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.Closed:
				return false;
			case JsonReader.State.Property:
				this.ReadType(this._currentElementType);
				return true;
			case JsonReader.State.ObjectStart:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.PostValue:
			{
				BsonReader.ContainerContext currentContext = this._currentContext;
				if (currentContext == null)
				{
					if (!base.SupportMultipleContent)
					{
						return false;
					}
				}
				else
				{
					int num = currentContext.Length - 1;
					if (currentContext.Position < num)
					{
						if (currentContext.Type == BsonType.Array)
						{
							this.ReadElement();
							this.ReadType(this._currentElementType);
							return true;
						}
						base.SetToken(JsonToken.PropertyName, this.ReadElement());
						return true;
					}
					else
					{
						if (currentContext.Position != num)
						{
							throw JsonReaderException.Create(this, "Read past end of current container context.");
						}
						if (this.ReadByte() != 0)
						{
							throw JsonReaderException.Create(this, "Unexpected end of object byte value.");
						}
						this.PopContext();
						if (this._currentContext != null)
						{
							this.MovePosition(currentContext.Length);
						}
						JsonToken token = (currentContext.Type == BsonType.Object) ? JsonToken.EndObject : JsonToken.EndArray;
						base.SetToken(token);
						return true;
					}
				}
				break;
			}
			case JsonReader.State.Object:
			case JsonReader.State.Array:
				goto IL_169;
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
			case JsonReader.State.Error:
			case JsonReader.State.Finished:
				return false;
			default:
				goto IL_169;
			}
			JsonToken token2 = (!this._readRootValueAsArray) ? JsonToken.StartObject : JsonToken.StartArray;
			BsonType type = (!this._readRootValueAsArray) ? BsonType.Object : BsonType.Array;
			base.SetToken(token2);
			BsonReader.ContainerContext containerContext = new BsonReader.ContainerContext(type);
			this.PushContext(containerContext);
			containerContext.Length = this.ReadInt32();
			return true;
			IL_169:
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00072B74 File Offset: 0x00070D74
		private void PopContext()
		{
			this._stack.RemoveAt(this._stack.Count - 1);
			if (this._stack.Count == 0)
			{
				this._currentContext = null;
				return;
			}
			this._currentContext = this._stack[this._stack.Count - 1];
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00072BD4 File Offset: 0x00070DD4
		private void PushContext(BsonReader.ContainerContext newContext)
		{
			this._stack.Add(newContext);
			this._currentContext = newContext;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00072BEC File Offset: 0x00070DEC
		private byte ReadByte()
		{
			this.MovePosition(1);
			return this._reader.ReadByte();
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00072C00 File Offset: 0x00070E00
		private void ReadType(BsonType type)
		{
			switch (type)
			{
			case BsonType.Number:
			{
				double num = this.ReadDouble();
				if (this._floatParseHandling == FloatParseHandling.Decimal)
				{
					base.SetToken(JsonToken.Float, Convert.ToDecimal(num, CultureInfo.InvariantCulture));
					return;
				}
				base.SetToken(JsonToken.Float, num);
				return;
			}
			case BsonType.String:
			case BsonType.Symbol:
				base.SetToken(JsonToken.String, this.ReadLengthString());
				return;
			case BsonType.Object:
			{
				base.SetToken(JsonToken.StartObject);
				BsonReader.ContainerContext containerContext = new BsonReader.ContainerContext(BsonType.Object);
				this.PushContext(containerContext);
				containerContext.Length = this.ReadInt32();
				return;
			}
			case BsonType.Array:
			{
				base.SetToken(JsonToken.StartArray);
				BsonReader.ContainerContext containerContext2 = new BsonReader.ContainerContext(BsonType.Array);
				this.PushContext(containerContext2);
				containerContext2.Length = this.ReadInt32();
				return;
			}
			case BsonType.Binary:
			{
				BsonBinaryType bsonBinaryType;
				byte[] array = this.ReadBinary(out bsonBinaryType);
				object value = (bsonBinaryType != BsonBinaryType.Uuid) ? array : new Guid(array);
				base.SetToken(JsonToken.Bytes, value);
				return;
			}
			case BsonType.Undefined:
				base.SetToken(JsonToken.Undefined);
				return;
			case BsonType.Oid:
			{
				byte[] value2 = this.ReadBytes(12);
				base.SetToken(JsonToken.Bytes, value2);
				return;
			}
			case BsonType.Boolean:
			{
				bool flag = Convert.ToBoolean(this.ReadByte());
				base.SetToken(JsonToken.Boolean, flag);
				return;
			}
			case BsonType.Date:
			{
				DateTime dateTime = DateTimeUtils.ConvertJavaScriptTicksToDateTime(this.ReadInt64());
				DateTimeKind dateTimeKindHandling = this.DateTimeKindHandling;
				DateTime dateTime2;
				if (dateTimeKindHandling != DateTimeKind.Unspecified)
				{
					if (dateTimeKindHandling != DateTimeKind.Local)
					{
						dateTime2 = dateTime;
					}
					else
					{
						dateTime2 = dateTime.ToLocalTime();
					}
				}
				else
				{
					dateTime2 = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
				}
				base.SetToken(JsonToken.Date, dateTime2);
				return;
			}
			case BsonType.Null:
				base.SetToken(JsonToken.Null);
				return;
			case BsonType.Regex:
			{
				string str = this.ReadString();
				string str2 = this.ReadString();
				string value3 = "/" + str + "/" + str2;
				base.SetToken(JsonToken.String, value3);
				return;
			}
			case BsonType.Reference:
				base.SetToken(JsonToken.StartObject);
				this._bsonReaderState = BsonReader.BsonReaderState.ReferenceStart;
				return;
			case BsonType.Code:
				base.SetToken(JsonToken.String, this.ReadLengthString());
				return;
			case BsonType.CodeWScope:
				base.SetToken(JsonToken.StartObject);
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeStart;
				return;
			case BsonType.Integer:
				base.SetToken(JsonToken.Integer, (long)this.ReadInt32());
				return;
			case BsonType.TimeStamp:
			case BsonType.Long:
				base.SetToken(JsonToken.Integer, this.ReadInt64());
				return;
			default:
				throw new ArgumentOutOfRangeException("type", "Unexpected BsonType value: " + type.ToString());
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00072E6C File Offset: 0x0007106C
		private byte[] ReadBinary(out BsonBinaryType binaryType)
		{
			int count = this.ReadInt32();
			binaryType = (BsonBinaryType)this.ReadByte();
			if (binaryType == BsonBinaryType.BinaryOld && !this._jsonNet35BinaryCompatibility)
			{
				count = this.ReadInt32();
			}
			return this.ReadBytes(count);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00072EB0 File Offset: 0x000710B0
		private string ReadString()
		{
			this.EnsureBuffers();
			StringBuilder stringBuilder = null;
			int num = 0;
			int num2 = 0;
			int num4;
			for (;;)
			{
				int num3 = num2;
				byte b;
				while (num3 < 128 && (b = this._reader.ReadByte()) > 0)
				{
					this._byteBuffer[num3++] = b;
				}
				num4 = num3 - num2;
				num += num4;
				if (num3 < 128 && stringBuilder == null)
				{
					break;
				}
				int lastFullCharStop = this.GetLastFullCharStop(num3 - 1);
				int chars = Encoding.UTF8.GetChars(this._byteBuffer, 0, lastFullCharStop + 1, this._charBuffer, 0);
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(256);
				}
				stringBuilder.Append(this._charBuffer, 0, chars);
				if (lastFullCharStop < num4 - 1)
				{
					num2 = num4 - lastFullCharStop - 1;
					Array.Copy(this._byteBuffer, lastFullCharStop + 1, this._byteBuffer, 0, num2);
				}
				else
				{
					if (num3 < 128)
					{
						goto Block_6;
					}
					num2 = 0;
				}
			}
			int chars2 = Encoding.UTF8.GetChars(this._byteBuffer, 0, num4, this._charBuffer, 0);
			this.MovePosition(num + 1);
			return new string(this._charBuffer, 0, chars2);
			Block_6:
			this.MovePosition(num + 1);
			return stringBuilder.ToString();
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00072FE8 File Offset: 0x000711E8
		private string ReadLengthString()
		{
			int num = this.ReadInt32();
			this.MovePosition(num);
			string @string = this.GetString(num - 1);
			this._reader.ReadByte();
			return @string;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0007301C File Offset: 0x0007121C
		private string GetString(int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			this.EnsureBuffers();
			StringBuilder stringBuilder = null;
			int num = 0;
			int num2 = 0;
			int num3;
			for (;;)
			{
				int count = (length - num > 128 - num2) ? (128 - num2) : (length - num);
				num3 = this._reader.Read(this._byteBuffer, num2, count);
				if (num3 == 0)
				{
					break;
				}
				num += num3;
				num3 += num2;
				if (num3 == length)
				{
					goto Block_4;
				}
				int lastFullCharStop = this.GetLastFullCharStop(num3 - 1);
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(length);
				}
				int chars = Encoding.UTF8.GetChars(this._byteBuffer, 0, lastFullCharStop + 1, this._charBuffer, 0);
				stringBuilder.Append(this._charBuffer, 0, chars);
				if (lastFullCharStop < num3 - 1)
				{
					num2 = num3 - lastFullCharStop - 1;
					Array.Copy(this._byteBuffer, lastFullCharStop + 1, this._byteBuffer, 0, num2);
				}
				else
				{
					num2 = 0;
				}
				if (num >= length)
				{
					goto Block_7;
				}
			}
			throw new EndOfStreamException("Unable to read beyond the end of the stream.");
			Block_4:
			int chars2 = Encoding.UTF8.GetChars(this._byteBuffer, 0, num3, this._charBuffer, 0);
			return new string(this._charBuffer, 0, chars2);
			Block_7:
			return stringBuilder.ToString();
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00073150 File Offset: 0x00071350
		private int GetLastFullCharStop(int start)
		{
			int i = start;
			int num = 0;
			while (i >= 0)
			{
				num = this.BytesInSequence(this._byteBuffer[i]);
				if (num == 0)
				{
					i--;
				}
				else
				{
					if (num != 1)
					{
						i--;
						break;
					}
					break;
				}
			}
			if (num == start - i)
			{
				return start;
			}
			return i;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x000731A8 File Offset: 0x000713A8
		private int BytesInSequence(byte b)
		{
			if (b <= BsonReader.SeqRange1[1])
			{
				return 1;
			}
			if (b >= BsonReader.SeqRange2[0] && b <= BsonReader.SeqRange2[1])
			{
				return 2;
			}
			if (b >= BsonReader.SeqRange3[0] && b <= BsonReader.SeqRange3[1])
			{
				return 3;
			}
			if (b >= BsonReader.SeqRange4[0] && b <= BsonReader.SeqRange4[1])
			{
				return 4;
			}
			return 0;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00073220 File Offset: 0x00071420
		private void EnsureBuffers()
		{
			if (this._byteBuffer == null)
			{
				this._byteBuffer = new byte[128];
			}
			if (this._charBuffer == null)
			{
				int maxCharCount = Encoding.UTF8.GetMaxCharCount(128);
				this._charBuffer = new char[maxCharCount];
			}
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00073274 File Offset: 0x00071474
		private double ReadDouble()
		{
			this.MovePosition(8);
			return this._reader.ReadDouble();
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00073288 File Offset: 0x00071488
		private int ReadInt32()
		{
			this.MovePosition(4);
			return this._reader.ReadInt32();
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x0007329C File Offset: 0x0007149C
		private long ReadInt64()
		{
			this.MovePosition(8);
			return this._reader.ReadInt64();
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000732B0 File Offset: 0x000714B0
		private BsonType ReadType()
		{
			this.MovePosition(1);
			return (BsonType)this._reader.ReadSByte();
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000732C4 File Offset: 0x000714C4
		private void MovePosition(int count)
		{
			this._currentContext.Position += count;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x000732DC File Offset: 0x000714DC
		private byte[] ReadBytes(int count)
		{
			this.MovePosition(count);
			return this._reader.ReadBytes(count);
		}

		// Token: 0x0400097E RID: 2430
		private const int MaxCharBytesSize = 128;

		// Token: 0x0400097F RID: 2431
		private static readonly byte[] SeqRange1 = new byte[]
		{
			0,
			127
		};

		// Token: 0x04000980 RID: 2432
		private static readonly byte[] SeqRange2 = new byte[]
		{
			194,
			223
		};

		// Token: 0x04000981 RID: 2433
		private static readonly byte[] SeqRange3 = new byte[]
		{
			224,
			239
		};

		// Token: 0x04000982 RID: 2434
		private static readonly byte[] SeqRange4 = new byte[]
		{
			240,
			244
		};

		// Token: 0x04000983 RID: 2435
		private readonly BinaryReader _reader;

		// Token: 0x04000984 RID: 2436
		private readonly List<BsonReader.ContainerContext> _stack;

		// Token: 0x04000985 RID: 2437
		private byte[] _byteBuffer;

		// Token: 0x04000986 RID: 2438
		private char[] _charBuffer;

		// Token: 0x04000987 RID: 2439
		private BsonType _currentElementType;

		// Token: 0x04000988 RID: 2440
		private BsonReader.BsonReaderState _bsonReaderState;

		// Token: 0x04000989 RID: 2441
		private BsonReader.ContainerContext _currentContext;

		// Token: 0x0400098A RID: 2442
		private bool _readRootValueAsArray;

		// Token: 0x0400098B RID: 2443
		private bool _jsonNet35BinaryCompatibility;

		// Token: 0x0400098C RID: 2444
		private DateTimeKind _dateTimeKindHandling;

		// Token: 0x02000325 RID: 805
		private enum BsonReaderState
		{
			// Token: 0x04000D2E RID: 3374
			Normal,
			// Token: 0x04000D2F RID: 3375
			ReferenceStart,
			// Token: 0x04000D30 RID: 3376
			ReferenceRef,
			// Token: 0x04000D31 RID: 3377
			ReferenceId,
			// Token: 0x04000D32 RID: 3378
			CodeWScopeStart,
			// Token: 0x04000D33 RID: 3379
			CodeWScopeCode,
			// Token: 0x04000D34 RID: 3380
			CodeWScopeScope,
			// Token: 0x04000D35 RID: 3381
			CodeWScopeScopeObject,
			// Token: 0x04000D36 RID: 3382
			CodeWScopeScopeEnd
		}

		// Token: 0x02000326 RID: 806
		private class ContainerContext
		{
			// Token: 0x06001935 RID: 6453 RVA: 0x0007C918 File Offset: 0x0007AB18
			public ContainerContext(BsonType type)
			{
				this.Type = type;
			}

			// Token: 0x04000D37 RID: 3383
			public readonly BsonType Type;

			// Token: 0x04000D38 RID: 3384
			public int Length;

			// Token: 0x04000D39 RID: 3385
			public int Position;
		}
	}
}
