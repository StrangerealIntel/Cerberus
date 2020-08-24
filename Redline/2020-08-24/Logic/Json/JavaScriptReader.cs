using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace RedLine.Logic.Json
{
	// Token: 0x02000051 RID: 81
	public class JavaScriptReader
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00008338 File Offset: 0x00006538
		public JavaScriptReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Reader = reader;
			this.SBuilder = new StringBuilder();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008368 File Offset: 0x00006568
		public object Read()
		{
			object result = this.ReadCore();
			this.SkipSpaces();
			if (this.ReadChar() >= 0)
			{
				throw this.JsonError(string.Format("extra characters in JSON input", new object[0]));
			}
			return result;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000083A4 File Offset: 0x000065A4
		private object ReadCore()
		{
			this.SkipSpaces();
			int num = this.PeekChar();
			if (num < 0)
			{
				throw this.JsonError("Incomplete JSON input");
			}
			if (num <= 102)
			{
				if (num == 34)
				{
					return this.ReadStringLiteral();
				}
				if (num != 91)
				{
					if (num == 102)
					{
						this.Expect("false");
						return false;
					}
				}
				else
				{
					this.ReadChar();
					List<object> list = new List<object>();
					this.SkipSpaces();
					if (this.PeekChar() == 93)
					{
						this.ReadChar();
						return list;
					}
					for (;;)
					{
						object item = this.ReadCore();
						list.Add(item);
						this.SkipSpaces();
						num = this.PeekChar();
						if (num != 44)
						{
							break;
						}
						this.ReadChar();
					}
					if (this.ReadChar() != 93)
					{
						throw this.JsonError("JSON array must end with ']'");
					}
					return list.ToArray();
				}
			}
			else
			{
				if (num == 110)
				{
					this.Expect("null");
					return null;
				}
				if (num == 116)
				{
					this.Expect("true");
					return true;
				}
				if (num == 123)
				{
					this.ReadChar();
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					this.SkipSpaces();
					if (this.PeekChar() == 125)
					{
						this.ReadChar();
						return dictionary;
					}
					for (;;)
					{
						this.SkipSpaces();
						if (this.PeekChar() == 125)
						{
							break;
						}
						string key = this.ReadStringLiteral();
						this.SkipSpaces();
						this.Expect(':');
						this.SkipSpaces();
						dictionary[key] = this.ReadCore();
						this.SkipSpaces();
						num = this.ReadChar();
						if (num != 44 && num == 125)
						{
							goto IL_144;
						}
					}
					this.ReadChar();
					IL_144:
					int num2 = 0;
					KeyValuePair<string, object>[] array = new KeyValuePair<string, object>[dictionary.Count];
					foreach (KeyValuePair<string, object> keyValuePair in dictionary)
					{
						array[num2++] = keyValuePair;
					}
					return array;
				}
			}
			if ((48 <= num && num <= 57) || num == 45)
			{
				return this.ReadNumericLiteral();
			}
			throw this.JsonError(string.Format("Unexpected character '{0}'", (char)num));
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000085B8 File Offset: 0x000067B8
		private int PeekChar()
		{
			if (!this.HasPeek)
			{
				this.Peek = this.Reader.Read();
				this.HasPeek = true;
			}
			return this.Peek;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000085E0 File Offset: 0x000067E0
		private int ReadChar()
		{
			object obj = this.HasPeek ? this.Peek : this.Reader.Read();
			this.HasPeek = false;
			if (this.Prev_Lf)
			{
				this.Line++;
				this.Column = 0;
				this.Prev_Lf = false;
			}
			object obj2 = obj;
			if (obj2 == 10)
			{
				this.Prev_Lf = true;
			}
			this.Column++;
			return obj2;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008650 File Offset: 0x00006850
		private void SkipSpaces()
		{
			for (;;)
			{
				int num = this.PeekChar();
				if (num - 9 > 1 && num != 13 && num != 32)
				{
					break;
				}
				this.ReadChar();
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008680 File Offset: 0x00006880
		private object ReadNumericLiteral()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.PeekChar() == 45)
			{
				stringBuilder.Append((char)this.ReadChar());
			}
			int num = 0;
			bool flag = this.PeekChar() == 48;
			int num2;
			for (;;)
			{
				num2 = this.PeekChar();
				if (num2 < 48 || 57 < num2)
				{
					goto IL_63;
				}
				stringBuilder.Append((char)this.ReadChar());
				if (flag && num == 1)
				{
					break;
				}
				num++;
			}
			throw this.JsonError("leading zeros are not allowed");
			IL_63:
			if (num == 0)
			{
				throw this.JsonError("Invalid JSON numeric literal; no digit found");
			}
			bool flag2 = false;
			int num3 = 0;
			if (this.PeekChar() == 46)
			{
				flag2 = true;
				stringBuilder.Append((char)this.ReadChar());
				if (this.PeekChar() < 0)
				{
					throw this.JsonError("Invalid JSON numeric literal; extra dot");
				}
				for (;;)
				{
					num2 = this.PeekChar();
					if (num2 < 48 || 57 < num2)
					{
						break;
					}
					stringBuilder.Append((char)this.ReadChar());
					num3++;
				}
				if (num3 == 0)
				{
					throw this.JsonError("Invalid JSON numeric literal; extra dot");
				}
			}
			num2 = this.PeekChar();
			if (num2 != 101 && num2 != 69)
			{
				if (!flag2)
				{
					int num4;
					if (int.TryParse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num4))
					{
						return num4;
					}
					long num5;
					if (long.TryParse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num5))
					{
						return num5;
					}
					ulong num6;
					if (ulong.TryParse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num6))
					{
						return num6;
					}
				}
				decimal num7;
				if (decimal.TryParse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num7) && num7 != 0m)
				{
					return num7;
				}
			}
			else
			{
				stringBuilder.Append((char)this.ReadChar());
				if (this.PeekChar() < 0)
				{
					throw new ArgumentException("Invalid JSON numeric literal; incomplete exponent");
				}
				num2 = this.PeekChar();
				if (num2 == 45)
				{
					stringBuilder.Append((char)this.ReadChar());
				}
				else if (num2 == 43)
				{
					stringBuilder.Append((char)this.ReadChar());
				}
				if (this.PeekChar() < 0)
				{
					throw this.JsonError("Invalid JSON numeric literal; incomplete exponent");
				}
				for (;;)
				{
					num2 = this.PeekChar();
					if (num2 < 48 || 57 < num2)
					{
						break;
					}
					stringBuilder.Append((char)this.ReadChar());
				}
			}
			return double.Parse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000088C0 File Offset: 0x00006AC0
		private string ReadStringLiteral()
		{
			if (this.PeekChar() != 34)
			{
				throw this.JsonError("Invalid JSON string literal format");
			}
			this.ReadChar();
			this.SBuilder.Length = 0;
			for (;;)
			{
				int num = this.ReadChar();
				if (num < 0)
				{
					break;
				}
				if (num == 34)
				{
					goto Block_3;
				}
				if (num != 92)
				{
					this.SBuilder.Append((char)num);
				}
				else
				{
					num = this.ReadChar();
					if (num < 0)
					{
						goto Block_5;
					}
					if (num <= 92)
					{
						if (num != 34 && num != 47 && num != 92)
						{
							goto Block_9;
						}
						this.SBuilder.Append((char)num);
					}
					else if (num <= 102)
					{
						if (num != 98)
						{
							if (num != 102)
							{
								goto Block_12;
							}
							this.SBuilder.Append('\f');
						}
						else
						{
							this.SBuilder.Append('\b');
						}
					}
					else
					{
						if (num != 110)
						{
							switch (num)
							{
							case 114:
								this.SBuilder.Append('\r');
								continue;
							case 116:
								this.SBuilder.Append('\t');
								continue;
							case 117:
							{
								ushort num2 = 0;
								for (int i = 0; i < 4; i++)
								{
									num2 = (ushort)(num2 << 4);
									if ((num = this.ReadChar()) < 0)
									{
										goto Block_15;
									}
									if (48 <= num && num <= 57)
									{
										num2 += (ushort)(num - 48);
									}
									if (65 <= num && num <= 70)
									{
										num2 += (ushort)(num - 65 + 10);
									}
									if (97 <= num && num <= 102)
									{
										num2 += (ushort)(num - 97 + 10);
									}
								}
								this.SBuilder.Append((char)num2);
								continue;
							}
							}
							goto Block_14;
						}
						this.SBuilder.Append('\n');
					}
				}
			}
			throw this.JsonError("JSON string is not closed");
			Block_3:
			return this.SBuilder.ToString();
			Block_5:
			throw this.JsonError("Invalid JSON string literal; incomplete escape sequence");
			Block_9:
			Block_12:
			Block_14:
			goto IL_1B9;
			Block_15:
			throw this.JsonError("Incomplete unicode character escape literal");
			IL_1B9:
			throw this.JsonError("Invalid JSON string literal; unexpected escape character");
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008A94 File Offset: 0x00006C94
		private void Expect(char expected)
		{
			int num;
			if ((num = this.ReadChar()) != (int)expected)
			{
				throw this.JsonError(string.Format("Expected '{0}', got '{1}'", expected, (char)num));
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008ACC File Offset: 0x00006CCC
		private void Expect(string expected)
		{
			for (int i = 0; i < expected.Length; i++)
			{
				if (this.ReadChar() != (int)expected[i])
				{
					throw this.JsonError(string.Format("Expected '{0}', differed at {1}", expected, i));
				}
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008B11 File Offset: 0x00006D11
		private Exception JsonError(string msg)
		{
			return new ArgumentException(string.Format("{0}. At line {1}, column {2}", msg, this.Line, this.Column));
		}

		// Token: 0x04000122 RID: 290
		private readonly StringBuilder SBuilder;

		// Token: 0x04000123 RID: 291
		private readonly TextReader Reader;

		// Token: 0x04000124 RID: 292
		private int Line = 1;

		// Token: 0x04000125 RID: 293
		private int Column;

		// Token: 0x04000126 RID: 294
		private int Peek;

		// Token: 0x04000127 RID: 295
		private bool HasPeek;

		// Token: 0x04000128 RID: 296
		private bool Prev_Lf;
	}
}
