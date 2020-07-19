using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace SmartAssembly.Zip
{
	// Token: 0x0200006D RID: 109
	public static class SimpleZip
	{
		// Token: 0x0600024F RID: 591 RVA: 0x00012EDC File Offset: 0x000110DC
		private static bool PublicKeysMatch(Assembly A_0, Assembly A_1)
		{
			return true;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00012EE0 File Offset: 0x000110E0
		private static ICryptoTransform GetAesTransform(byte[] A_0, byte[] A_1, bool A_2)
		{
			ICryptoTransform result;
			using (SymmetricAlgorithm symmetricAlgorithm = new RijndaelManaged())
			{
				result = (A_2 ? symmetricAlgorithm.CreateDecryptor(A_0, A_1) : symmetricAlgorithm.CreateEncryptor(A_0, A_1));
			}
			return result;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00012F34 File Offset: 0x00011134
		private static ICryptoTransform GetDesTransform(byte[] A_0, byte[] A_1, bool A_2)
		{
			ICryptoTransform result;
			using (DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider())
			{
				result = (A_2 ? descryptoServiceProvider.CreateDecryptor(A_0, A_1) : descryptoServiceProvider.CreateEncryptor(A_0, A_1));
			}
			return result;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00012F88 File Offset: 0x00011188
		public static byte[] Unzip(byte[] A_0)
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			if (callingAssembly != executingAssembly && !SimpleZip.PublicKeysMatch(executingAssembly, callingAssembly))
			{
				return null;
			}
			SimpleZip.ZipStream zipStream = new SimpleZip.ZipStream(A_0);
			byte[] array = new byte[0];
			int num = zipStream.ReadInt();
			if (num != 67324752)
			{
				int num2 = num >> 24;
				num -= num2 << 24;
				if (num == 8223355)
				{
					if (num2 == 1)
					{
						int num3 = zipStream.ReadInt();
						array = new byte[num3];
						int num5;
						for (int i = 0; i < num3; i += num5)
						{
							int num4 = zipStream.ReadInt();
							num5 = zipStream.ReadInt();
							byte[] array2 = new byte[num4];
							zipStream.Read(array2, 0, array2.Length);
							new SimpleZip.Inflater(array2).Inflate(array, i, num5);
						}
					}
					if (num2 == 2)
					{
						byte[] array3 = new byte[]
						{
							72,
							196,
							226,
							220,
							77,
							90,
							77,
							41
						};
						byte[] array4 = new byte[]
						{
							17,
							242,
							176,
							1,
							248,
							117,
							118,
							164
						};
						using (ICryptoTransform desTransform = SimpleZip.GetDesTransform(array3, array4, true))
						{
							array = SimpleZip.Unzip(desTransform.TransformFinalBlock(A_0, 4, A_0.Length - 4));
						}
					}
					if (num2 != 3)
					{
						goto IL_287;
					}
					byte[] array5 = new byte[]
					{
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1
					};
					byte[] array6 = new byte[]
					{
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2,
						2
					};
					using (ICryptoTransform aesTransform = SimpleZip.GetAesTransform(array5, array6, true))
					{
						array = SimpleZip.Unzip(aesTransform.TransformFinalBlock(A_0, 4, A_0.Length - 4));
						goto IL_287;
					}
				}
				throw new FormatException("Unknown Header");
			}
			short num6 = (short)zipStream.ReadShort();
			int num7 = zipStream.ReadShort();
			int num8 = zipStream.ReadShort();
			if (num != 67324752 || num6 != 20 || num7 != 0 || num8 != 8)
			{
				throw new FormatException("Wrong Header Signature");
			}
			zipStream.ReadInt();
			zipStream.ReadInt();
			zipStream.ReadInt();
			int num9 = zipStream.ReadInt();
			int num10 = zipStream.ReadShort();
			int num11 = zipStream.ReadShort();
			if (num10 > 0)
			{
				byte[] buffer = new byte[num10];
				zipStream.Read(buffer, 0, num10);
			}
			if (num11 > 0)
			{
				byte[] buffer2 = new byte[num11];
				zipStream.Read(buffer2, 0, num11);
			}
			byte[] array7 = new byte[zipStream.Length - zipStream.Position];
			zipStream.Read(array7, 0, array7.Length);
			SimpleZip.Inflater inflater = new SimpleZip.Inflater(array7);
			array = new byte[num9];
			inflater.Inflate(array, 0, array.Length);
			IL_287:
			zipStream.Close();
			zipStream = null;
			return array;
		}

		// Token: 0x0200024D RID: 589
		internal sealed class Inflater
		{
			// Token: 0x060016A1 RID: 5793 RVA: 0x00074740 File Offset: 0x00072940
			public Inflater(byte[] A_1)
			{
				this.input = new SimpleZip.StreamManipulator();
				this.outputWindow = new SimpleZip.OutputWindow();
				this.mode = 2;
				this.input.SetInput(A_1, 0, A_1.Length);
			}

			// Token: 0x060016A2 RID: 5794 RVA: 0x00074778 File Offset: 0x00072978
			private bool DecodeHuffman()
			{
				int i = this.outputWindow.GetFreeSpace();
				while (i >= 258)
				{
					int symbol;
					switch (this.mode)
					{
					case 7:
						while (((symbol = this.litlenTree.GetSymbol(this.input)) & -256) == 0)
						{
							this.outputWindow.Write(symbol);
							if (--i < 258)
							{
								return true;
							}
						}
						if (symbol >= 257)
						{
							this.repLength = SimpleZip.Inflater.CPLENS[symbol - 257];
							this.neededBits = SimpleZip.Inflater.CPLEXT[symbol - 257];
							goto IL_BE;
						}
						if (symbol < 0)
						{
							return false;
						}
						this.distTree = null;
						this.litlenTree = null;
						this.mode = 2;
						return true;
					case 8:
						goto IL_BE;
					case 9:
						goto IL_113;
					case 10:
						break;
					default:
						continue;
					}
					IL_148:
					if (this.neededBits > 0)
					{
						this.mode = 10;
						int num = this.input.PeekBits(this.neededBits);
						if (num < 0)
						{
							return false;
						}
						this.input.DropBits(this.neededBits);
						this.repDist += num;
					}
					this.outputWindow.Repeat(this.repLength, this.repDist);
					i -= this.repLength;
					this.mode = 7;
					continue;
					IL_113:
					symbol = this.distTree.GetSymbol(this.input);
					if (symbol < 0)
					{
						return false;
					}
					this.repDist = SimpleZip.Inflater.CPDIST[symbol];
					this.neededBits = SimpleZip.Inflater.CPDEXT[symbol];
					goto IL_148;
					IL_BE:
					if (this.neededBits > 0)
					{
						this.mode = 8;
						int num2 = this.input.PeekBits(this.neededBits);
						if (num2 < 0)
						{
							return false;
						}
						this.input.DropBits(this.neededBits);
						this.repLength += num2;
					}
					this.mode = 9;
					goto IL_113;
				}
				return true;
			}

			// Token: 0x060016A3 RID: 5795 RVA: 0x00074958 File Offset: 0x00072B58
			private bool Decode()
			{
				switch (this.mode)
				{
				case 2:
				{
					if (this.isLastBlock)
					{
						this.mode = 12;
						return false;
					}
					int num = this.input.PeekBits(3);
					if (num < 0)
					{
						return false;
					}
					this.input.DropBits(3);
					if ((num & 1) != 0)
					{
						this.isLastBlock = true;
					}
					switch (num >> 1)
					{
					case 0:
						this.input.SkipToByteBoundary();
						this.mode = 3;
						break;
					case 1:
						this.litlenTree = SimpleZip.InflaterHuffmanTree.defLitLenTree;
						this.distTree = SimpleZip.InflaterHuffmanTree.defDistTree;
						this.mode = 7;
						break;
					case 2:
						this.dynHeader = new SimpleZip.InflaterDynHeader();
						this.mode = 6;
						break;
					}
					return true;
				}
				case 3:
					if ((this.uncomprLen = this.input.PeekBits(16)) < 0)
					{
						return false;
					}
					this.input.DropBits(16);
					this.mode = 4;
					break;
				case 4:
					break;
				case 5:
					goto IL_149;
				case 6:
					if (!this.dynHeader.Decode(this.input))
					{
						return false;
					}
					this.litlenTree = this.dynHeader.BuildLitLenTree();
					this.distTree = this.dynHeader.BuildDistTree();
					this.mode = 7;
					goto IL_1D3;
				case 7:
				case 8:
				case 9:
				case 10:
					goto IL_1D3;
				case 11:
					return false;
				case 12:
					return false;
				default:
					return false;
				}
				if (this.input.PeekBits(16) < 0)
				{
					return false;
				}
				this.input.DropBits(16);
				this.mode = 5;
				IL_149:
				int num2 = this.outputWindow.CopyStored(this.input, this.uncomprLen);
				this.uncomprLen -= num2;
				if (this.uncomprLen == 0)
				{
					this.mode = 2;
					return true;
				}
				return !this.input.get_IsNeedingInput();
				IL_1D3:
				return this.DecodeHuffman();
			}

			// Token: 0x060016A4 RID: 5796 RVA: 0x00074B48 File Offset: 0x00072D48
			public int Inflate(byte[] A_1, int A_2, int A_3)
			{
				int num = 0;
				for (;;)
				{
					if (this.mode != 11)
					{
						int num2 = this.outputWindow.CopyOutput(A_1, A_2, A_3);
						A_2 += num2;
						num += num2;
						A_3 -= num2;
						if (A_3 == 0)
						{
							break;
						}
					}
					if (!this.Decode() && (this.outputWindow.GetAvailable() <= 0 || this.mode == 11))
					{
						return num;
					}
				}
				return num;
			}

			// Token: 0x04000A45 RID: 2629
			private static readonly int[] CPLENS = new int[]
			{
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				13,
				15,
				17,
				19,
				23,
				27,
				31,
				35,
				43,
				51,
				59,
				67,
				83,
				99,
				115,
				131,
				163,
				195,
				227,
				258
			};

			// Token: 0x04000A46 RID: 2630
			private static readonly int[] CPLEXT = new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1,
				2,
				2,
				2,
				2,
				3,
				3,
				3,
				3,
				4,
				4,
				4,
				4,
				5,
				5,
				5,
				5,
				0
			};

			// Token: 0x04000A47 RID: 2631
			private static readonly int[] CPDIST = new int[]
			{
				1,
				2,
				3,
				4,
				5,
				7,
				9,
				13,
				17,
				25,
				33,
				49,
				65,
				97,
				129,
				193,
				257,
				385,
				513,
				769,
				1025,
				1537,
				2049,
				3073,
				4097,
				6145,
				8193,
				12289,
				16385,
				24577
			};

			// Token: 0x04000A48 RID: 2632
			private static readonly int[] CPDEXT = new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				2,
				2,
				3,
				3,
				4,
				4,
				5,
				5,
				6,
				6,
				7,
				7,
				8,
				8,
				9,
				9,
				10,
				10,
				11,
				11,
				12,
				12,
				13,
				13
			};

			// Token: 0x04000A49 RID: 2633
			private int mode;

			// Token: 0x04000A4A RID: 2634
			private int neededBits;

			// Token: 0x04000A4B RID: 2635
			private int repLength;

			// Token: 0x04000A4C RID: 2636
			private int repDist;

			// Token: 0x04000A4D RID: 2637
			private int uncomprLen;

			// Token: 0x04000A4E RID: 2638
			private bool isLastBlock;

			// Token: 0x04000A4F RID: 2639
			private SimpleZip.StreamManipulator input;

			// Token: 0x04000A50 RID: 2640
			private SimpleZip.OutputWindow outputWindow;

			// Token: 0x04000A51 RID: 2641
			private SimpleZip.InflaterDynHeader dynHeader;

			// Token: 0x04000A52 RID: 2642
			private SimpleZip.InflaterHuffmanTree litlenTree;

			// Token: 0x04000A53 RID: 2643
			private SimpleZip.InflaterHuffmanTree distTree;
		}

		// Token: 0x0200024E RID: 590
		internal sealed class StreamManipulator
		{
			// Token: 0x060016A6 RID: 5798 RVA: 0x00074C24 File Offset: 0x00072E24
			public int PeekBits(int A_1)
			{
				if (this.bits_in_buffer < A_1)
				{
					if (this.window_start == this.window_end)
					{
						return -1;
					}
					uint num = this.buffer;
					byte[] array = this.window;
					int num2 = this.window_start;
					this.window_start = num2 + 1;
					uint num3 = array[num2] & 255u;
					byte[] array2 = this.window;
					num2 = this.window_start;
					this.window_start = num2 + 1;
					this.buffer = (num | (num3 | (array2[num2] & 255u) << 8) << this.bits_in_buffer);
					this.bits_in_buffer += 16;
				}
				return (int)((ulong)this.buffer & (ulong)((long)((1 << A_1) - 1)));
			}

			// Token: 0x060016A7 RID: 5799 RVA: 0x00074CCC File Offset: 0x00072ECC
			public void DropBits(int A_1)
			{
				this.buffer >>= A_1;
				this.bits_in_buffer -= A_1;
			}

			// Token: 0x060016A8 RID: 5800 RVA: 0x00074CF0 File Offset: 0x00072EF0
			public int get_AvailableBits()
			{
				return this.bits_in_buffer;
			}

			// Token: 0x060016A9 RID: 5801 RVA: 0x00074CF8 File Offset: 0x00072EF8
			public int get_AvailableBytes()
			{
				return this.window_end - this.window_start + (this.bits_in_buffer >> 3);
			}

			// Token: 0x060016AA RID: 5802 RVA: 0x00074D10 File Offset: 0x00072F10
			public void SkipToByteBoundary()
			{
				this.buffer >>= (this.bits_in_buffer & 7);
				this.bits_in_buffer &= -8;
			}

			// Token: 0x060016AB RID: 5803 RVA: 0x00074D3C File Offset: 0x00072F3C
			public bool get_IsNeedingInput()
			{
				return this.window_start == this.window_end;
			}

			// Token: 0x060016AC RID: 5804 RVA: 0x00074D4C File Offset: 0x00072F4C
			public int CopyBytes(byte[] A_1, int A_2, int A_3)
			{
				int num = 0;
				while (this.bits_in_buffer > 0 && A_3 > 0)
				{
					A_1[A_2++] = (byte)this.buffer;
					this.buffer >>= 8;
					this.bits_in_buffer -= 8;
					A_3--;
					num++;
				}
				if (A_3 == 0)
				{
					return num;
				}
				int num2 = this.window_end - this.window_start;
				if (A_3 > num2)
				{
					A_3 = num2;
				}
				Array.Copy(this.window, this.window_start, A_1, A_2, A_3);
				this.window_start += A_3;
				if ((this.window_start - this.window_end & 1) != 0)
				{
					byte[] array = this.window;
					int num3 = this.window_start;
					this.window_start = num3 + 1;
					this.buffer = (array[num3] & 255u);
					this.bits_in_buffer = 8;
				}
				return num + A_3;
			}

			// Token: 0x060016AE RID: 5806 RVA: 0x00074E34 File Offset: 0x00073034
			public void SetInput(byte[] A_1, int A_2, int A_3)
			{
				if (this.window_start < this.window_end)
				{
					throw new InvalidOperationException();
				}
				int num = A_2 + A_3;
				if (0 > A_2 || A_2 > num || num > A_1.Length)
				{
					throw new ArgumentOutOfRangeException();
				}
				if ((A_3 & 1) != 0)
				{
					this.buffer |= (uint)((uint)(A_1[A_2++] & byte.MaxValue) << this.bits_in_buffer);
					this.bits_in_buffer += 8;
				}
				this.window = A_1;
				this.window_start = A_2;
				this.window_end = num;
			}

			// Token: 0x04000A54 RID: 2644
			private byte[] window;

			// Token: 0x04000A55 RID: 2645
			private int window_start;

			// Token: 0x04000A56 RID: 2646
			private int window_end;

			// Token: 0x04000A57 RID: 2647
			private uint buffer;

			// Token: 0x04000A58 RID: 2648
			private int bits_in_buffer;
		}

		// Token: 0x0200024F RID: 591
		internal sealed class OutputWindow
		{
			// Token: 0x060016AF RID: 5807 RVA: 0x00074ED0 File Offset: 0x000730D0
			public void Write(int A_1)
			{
				int num = this.windowFilled;
				this.windowFilled = num + 1;
				if (num == 32768)
				{
					throw new InvalidOperationException();
				}
				byte[] array = this.window;
				num = this.windowEnd;
				this.windowEnd = num + 1;
				array[num] = (byte)A_1;
				this.windowEnd &= 32767;
			}

			// Token: 0x060016B0 RID: 5808 RVA: 0x00074F30 File Offset: 0x00073130
			private void SlowRepeat(int A_1, int A_2, int A_3)
			{
				while (A_2-- > 0)
				{
					byte[] array = this.window;
					int num = this.windowEnd;
					this.windowEnd = num + 1;
					array[num] = this.window[A_1++];
					this.windowEnd &= 32767;
					A_1 &= 32767;
				}
			}

			// Token: 0x060016B1 RID: 5809 RVA: 0x00074F90 File Offset: 0x00073190
			public void Repeat(int A_1, int A_2)
			{
				if ((this.windowFilled += A_1) > 32768)
				{
					throw new InvalidOperationException();
				}
				int num = this.windowEnd - A_2 & 32767;
				int num2 = 32768 - A_1;
				if (num > num2 || this.windowEnd >= num2)
				{
					this.SlowRepeat(num, A_1, A_2);
					return;
				}
				if (A_1 <= A_2)
				{
					Array.Copy(this.window, num, this.window, this.windowEnd, A_1);
					this.windowEnd += A_1;
					return;
				}
				while (A_1-- > 0)
				{
					byte[] array = this.window;
					int num3 = this.windowEnd;
					this.windowEnd = num3 + 1;
					array[num3] = this.window[num++];
				}
			}

			// Token: 0x060016B2 RID: 5810 RVA: 0x00075054 File Offset: 0x00073254
			public int CopyStored(SimpleZip.StreamManipulator A_1, int A_2)
			{
				A_2 = Math.Min(Math.Min(A_2, 32768 - this.windowFilled), A_1.get_AvailableBytes());
				int num = 32768 - this.windowEnd;
				int num2;
				if (A_2 > num)
				{
					num2 = A_1.CopyBytes(this.window, this.windowEnd, num);
					if (num2 == num)
					{
						num2 += A_1.CopyBytes(this.window, 0, A_2 - num);
					}
				}
				else
				{
					num2 = A_1.CopyBytes(this.window, this.windowEnd, A_2);
				}
				this.windowEnd = (this.windowEnd + num2 & 32767);
				this.windowFilled += num2;
				return num2;
			}

			// Token: 0x060016B3 RID: 5811 RVA: 0x00075104 File Offset: 0x00073304
			public int GetFreeSpace()
			{
				return 32768 - this.windowFilled;
			}

			// Token: 0x060016B4 RID: 5812 RVA: 0x00075114 File Offset: 0x00073314
			public int GetAvailable()
			{
				return this.windowFilled;
			}

			// Token: 0x060016B5 RID: 5813 RVA: 0x0007511C File Offset: 0x0007331C
			public int CopyOutput(byte[] A_1, int A_2, int A_3)
			{
				int num = this.windowEnd;
				if (A_3 > this.windowFilled)
				{
					A_3 = this.windowFilled;
				}
				else
				{
					num = (this.windowEnd - this.windowFilled + A_3 & 32767);
				}
				int num2 = A_3;
				int num3 = A_3 - num;
				if (num3 > 0)
				{
					Array.Copy(this.window, 32768 - num3, A_1, A_2, num3);
					A_2 += num3;
					A_3 = num;
				}
				Array.Copy(this.window, num - A_3, A_1, A_2, A_3);
				this.windowFilled -= num2;
				if (this.windowFilled < 0)
				{
					throw new InvalidOperationException();
				}
				return num2;
			}

			// Token: 0x04000A59 RID: 2649
			private byte[] window = new byte[32768];

			// Token: 0x04000A5A RID: 2650
			private int windowEnd;

			// Token: 0x04000A5B RID: 2651
			private int windowFilled;
		}

		// Token: 0x02000250 RID: 592
		internal sealed class InflaterHuffmanTree
		{
			// Token: 0x060016B7 RID: 5815 RVA: 0x000751D8 File Offset: 0x000733D8
			static InflaterHuffmanTree()
			{
				byte[] array = new byte[288];
				int i = 0;
				while (i < 144)
				{
					array[i++] = 8;
				}
				while (i < 256)
				{
					array[i++] = 9;
				}
				while (i < 280)
				{
					array[i++] = 7;
				}
				while (i < 288)
				{
					array[i++] = 8;
				}
				SimpleZip.InflaterHuffmanTree.defLitLenTree = new SimpleZip.InflaterHuffmanTree(array);
				array = new byte[32];
				i = 0;
				while (i < 32)
				{
					array[i++] = 5;
				}
				SimpleZip.InflaterHuffmanTree.defDistTree = new SimpleZip.InflaterHuffmanTree(array);
			}

			// Token: 0x060016B8 RID: 5816 RVA: 0x00075280 File Offset: 0x00073480
			public InflaterHuffmanTree(byte[] A_1)
			{
				this.BuildTree(A_1);
			}

			// Token: 0x060016B9 RID: 5817 RVA: 0x00075290 File Offset: 0x00073490
			private void BuildTree(byte[] A_1)
			{
				int[] array = new int[16];
				int[] array2 = new int[16];
				foreach (int num in A_1)
				{
					if (num > 0)
					{
						array[num]++;
					}
				}
				int num2 = 0;
				int num3 = 512;
				for (int j = 1; j <= 15; j++)
				{
					array2[j] = num2;
					num2 += array[j] << 16 - j;
					if (j >= 10)
					{
						int num4 = array2[j] & 130944;
						int num5 = num2 & 130944;
						num3 += num5 - num4 >> 16 - j;
					}
				}
				this.tree = new short[num3];
				int num6 = 512;
				for (int k = 15; k >= 10; k--)
				{
					int num7 = num2 & 130944;
					num2 -= array[k] << 16 - k;
					for (int l = num2 & 130944; l < num7; l += 128)
					{
						this.tree[(int)SimpleZip.DeflaterHuffman.BitReverse(l)] = (short)(-num6 << 4 | k);
						num6 += 1 << k - 9;
					}
				}
				for (int m = 0; m < A_1.Length; m++)
				{
					int num8 = (int)A_1[m];
					if (num8 != 0)
					{
						num2 = array2[num8];
						int num9 = (int)SimpleZip.DeflaterHuffman.BitReverse(num2);
						if (num8 <= 9)
						{
							do
							{
								this.tree[num9] = (short)(m << 4 | num8);
								num9 += 1 << num8;
							}
							while (num9 < 512);
						}
						else
						{
							int num10 = (int)this.tree[num9 & 511];
							int num11 = 1 << (num10 & 15);
							num10 = -(num10 >> 4);
							do
							{
								this.tree[num10 | num9 >> 9] = (short)(m << 4 | num8);
								num9 += 1 << num8;
							}
							while (num9 < num11);
						}
						array2[num8] = num2 + (1 << 16 - num8);
					}
				}
			}

			// Token: 0x060016BA RID: 5818 RVA: 0x00075494 File Offset: 0x00073694
			public int GetSymbol(SimpleZip.StreamManipulator A_1)
			{
				int num;
				if ((num = A_1.PeekBits(9)) >= 0)
				{
					int num2;
					if ((num2 = (int)this.tree[num]) >= 0)
					{
						A_1.DropBits(num2 & 15);
						return num2 >> 4;
					}
					int num3 = -(num2 >> 4);
					int num4 = num2 & 15;
					if ((num = A_1.PeekBits(num4)) >= 0)
					{
						num2 = (int)this.tree[num3 | num >> 9];
						A_1.DropBits(num2 & 15);
						return num2 >> 4;
					}
					int availableBits = A_1.get_AvailableBits();
					num = A_1.PeekBits(availableBits);
					num2 = (int)this.tree[num3 | num >> 9];
					if ((num2 & 15) <= availableBits)
					{
						A_1.DropBits(num2 & 15);
						return num2 >> 4;
					}
					return -1;
				}
				else
				{
					int availableBits2 = A_1.get_AvailableBits();
					num = A_1.PeekBits(availableBits2);
					int num2 = (int)this.tree[num];
					if (num2 >= 0 && (num2 & 15) <= availableBits2)
					{
						A_1.DropBits(num2 & 15);
						return num2 >> 4;
					}
					return -1;
				}
			}

			// Token: 0x04000A5C RID: 2652
			private short[] tree;

			// Token: 0x04000A5D RID: 2653
			public static readonly SimpleZip.InflaterHuffmanTree defLitLenTree;

			// Token: 0x04000A5E RID: 2654
			public static readonly SimpleZip.InflaterHuffmanTree defDistTree;
		}

		// Token: 0x02000251 RID: 593
		internal sealed class InflaterDynHeader
		{
			// Token: 0x060016BC RID: 5820 RVA: 0x00075588 File Offset: 0x00073788
			public bool Decode(SimpleZip.StreamManipulator A_1)
			{
				for (;;)
				{
					switch (this.mode)
					{
					case 0:
						this.lnum = A_1.PeekBits(5);
						if (this.lnum < 0)
						{
							return false;
						}
						this.lnum += 257;
						A_1.DropBits(5);
						this.mode = 1;
						goto IL_62;
					case 1:
						goto IL_62;
					case 2:
						goto IL_BD;
					case 3:
						break;
					case 4:
						goto IL_1B9;
					case 5:
						goto IL_1F5;
					default:
						continue;
					}
					IL_148:
					while (this.ptr < this.blnum)
					{
						int num = A_1.PeekBits(3);
						if (num < 0)
						{
							return false;
						}
						A_1.DropBits(3);
						this.blLens[SimpleZip.InflaterDynHeader.BL_ORDER[this.ptr]] = (byte)num;
						this.ptr++;
					}
					this.blTree = new SimpleZip.InflaterHuffmanTree(this.blLens);
					this.blLens = null;
					this.ptr = 0;
					this.mode = 4;
					IL_1B9:
					int symbol;
					while (((symbol = this.blTree.GetSymbol(A_1)) & -16) == 0)
					{
						byte[] array = this.litdistLens;
						int num2 = this.ptr;
						this.ptr = num2 + 1;
						array[num2] = (this.lastLen = (byte)symbol);
						if (this.ptr == this.num)
						{
							return true;
						}
					}
					if (symbol < 0)
					{
						return false;
					}
					if (symbol >= 17)
					{
						this.lastLen = 0;
					}
					this.repSymbol = symbol - 16;
					this.mode = 5;
					IL_1F5:
					int num3 = SimpleZip.InflaterDynHeader.repBits[this.repSymbol];
					int num4 = A_1.PeekBits(num3);
					if (num4 < 0)
					{
						return false;
					}
					A_1.DropBits(num3);
					num4 += SimpleZip.InflaterDynHeader.repMin[this.repSymbol];
					while (num4-- > 0)
					{
						byte[] array2 = this.litdistLens;
						int num2 = this.ptr;
						this.ptr = num2 + 1;
						array2[num2] = this.lastLen;
					}
					if (this.ptr == this.num)
					{
						return true;
					}
					this.mode = 4;
					continue;
					IL_BD:
					this.blnum = A_1.PeekBits(4);
					if (this.blnum < 0)
					{
						return false;
					}
					this.blnum += 4;
					A_1.DropBits(4);
					this.blLens = new byte[19];
					this.ptr = 0;
					this.mode = 3;
					goto IL_148;
					IL_62:
					this.dnum = A_1.PeekBits(5);
					if (this.dnum < 0)
					{
						return false;
					}
					this.dnum++;
					A_1.DropBits(5);
					this.num = this.lnum + this.dnum;
					this.litdistLens = new byte[this.num];
					this.mode = 2;
					goto IL_BD;
				}
				return false;
			}

			// Token: 0x060016BD RID: 5821 RVA: 0x00075814 File Offset: 0x00073A14
			public SimpleZip.InflaterHuffmanTree BuildLitLenTree()
			{
				byte[] array = new byte[this.lnum];
				Array.Copy(this.litdistLens, 0, array, 0, this.lnum);
				return new SimpleZip.InflaterHuffmanTree(array);
			}

			// Token: 0x060016BE RID: 5822 RVA: 0x0007584C File Offset: 0x00073A4C
			public SimpleZip.InflaterHuffmanTree BuildDistTree()
			{
				byte[] array = new byte[this.dnum];
				Array.Copy(this.litdistLens, this.lnum, array, 0, this.dnum);
				return new SimpleZip.InflaterHuffmanTree(array);
			}

			// Token: 0x04000A5F RID: 2655
			private static readonly int[] repMin = new int[]
			{
				3,
				3,
				11
			};

			// Token: 0x04000A60 RID: 2656
			private static readonly int[] repBits = new int[]
			{
				2,
				3,
				7
			};

			// Token: 0x04000A61 RID: 2657
			private byte[] blLens;

			// Token: 0x04000A62 RID: 2658
			private byte[] litdistLens;

			// Token: 0x04000A63 RID: 2659
			private SimpleZip.InflaterHuffmanTree blTree;

			// Token: 0x04000A64 RID: 2660
			private int mode;

			// Token: 0x04000A65 RID: 2661
			private int lnum;

			// Token: 0x04000A66 RID: 2662
			private int dnum;

			// Token: 0x04000A67 RID: 2663
			private int blnum;

			// Token: 0x04000A68 RID: 2664
			private int num;

			// Token: 0x04000A69 RID: 2665
			private int repSymbol;

			// Token: 0x04000A6A RID: 2666
			private byte lastLen;

			// Token: 0x04000A6B RID: 2667
			private int ptr;

			// Token: 0x04000A6C RID: 2668
			private static readonly int[] BL_ORDER = new int[]
			{
				16,
				17,
				18,
				0,
				8,
				7,
				9,
				6,
				10,
				5,
				11,
				4,
				12,
				3,
				13,
				2,
				14,
				1,
				15
			};
		}

		// Token: 0x02000252 RID: 594
		internal sealed class DeflaterHuffman
		{
			// Token: 0x060016C0 RID: 5824 RVA: 0x000758DC File Offset: 0x00073ADC
			public static short BitReverse(int A_0)
			{
				return (short)((int)SimpleZip.DeflaterHuffman.bit4Reverse[A_0 & 15] << 12 | (int)SimpleZip.DeflaterHuffman.bit4Reverse[A_0 >> 4 & 15] << 8 | (int)SimpleZip.DeflaterHuffman.bit4Reverse[A_0 >> 8 & 15] << 4 | (int)SimpleZip.DeflaterHuffman.bit4Reverse[A_0 >> 12]);
			}

			// Token: 0x060016C1 RID: 5825 RVA: 0x00075918 File Offset: 0x00073B18
			static DeflaterHuffman()
			{
				int i = 0;
				while (i < 144)
				{
					SimpleZip.DeflaterHuffman.staticLCodes[i] = SimpleZip.DeflaterHuffman.BitReverse(48 + i << 8);
					SimpleZip.DeflaterHuffman.staticLLength[i++] = 8;
				}
				while (i < 256)
				{
					SimpleZip.DeflaterHuffman.staticLCodes[i] = SimpleZip.DeflaterHuffman.BitReverse(256 + i << 7);
					SimpleZip.DeflaterHuffman.staticLLength[i++] = 9;
				}
				while (i < 280)
				{
					SimpleZip.DeflaterHuffman.staticLCodes[i] = SimpleZip.DeflaterHuffman.BitReverse(-256 + i << 9);
					SimpleZip.DeflaterHuffman.staticLLength[i++] = 7;
				}
				while (i < 286)
				{
					SimpleZip.DeflaterHuffman.staticLCodes[i] = SimpleZip.DeflaterHuffman.BitReverse(-88 + i << 8);
					SimpleZip.DeflaterHuffman.staticLLength[i++] = 8;
				}
				SimpleZip.DeflaterHuffman.staticDCodes = new short[30];
				SimpleZip.DeflaterHuffman.staticDLength = new byte[30];
				for (i = 0; i < 30; i++)
				{
					SimpleZip.DeflaterHuffman.staticDCodes[i] = SimpleZip.DeflaterHuffman.BitReverse(i << 11);
					SimpleZip.DeflaterHuffman.staticDLength[i] = 5;
				}
			}

			// Token: 0x04000A6D RID: 2669
			private static readonly int[] BL_ORDER = new int[]
			{
				16,
				17,
				18,
				0,
				8,
				7,
				9,
				6,
				10,
				5,
				11,
				4,
				12,
				3,
				13,
				2,
				14,
				1,
				15
			};

			// Token: 0x04000A6E RID: 2670
			private static readonly byte[] bit4Reverse = new byte[]
			{
				0,
				8,
				4,
				12,
				2,
				10,
				6,
				14,
				1,
				9,
				5,
				13,
				3,
				11,
				7,
				15
			};

			// Token: 0x04000A6F RID: 2671
			private static readonly short[] staticLCodes = new short[286];

			// Token: 0x04000A70 RID: 2672
			private static readonly byte[] staticLLength = new byte[286];

			// Token: 0x04000A71 RID: 2673
			private static readonly short[] staticDCodes;

			// Token: 0x04000A72 RID: 2674
			private static readonly byte[] staticDLength;
		}

		// Token: 0x02000253 RID: 595
		internal sealed class ZipStream : MemoryStream
		{
			// Token: 0x060016C2 RID: 5826 RVA: 0x00075A6C File Offset: 0x00073C6C
			public int ReadShort()
			{
				return this.ReadByte() | this.ReadByte() << 8;
			}

			// Token: 0x060016C3 RID: 5827 RVA: 0x00075A80 File Offset: 0x00073C80
			public int ReadInt()
			{
				return this.ReadShort() | this.ReadShort() << 16;
			}

			// Token: 0x060016C4 RID: 5828 RVA: 0x00075A94 File Offset: 0x00073C94
			public ZipStream(byte[] A_1) : base(A_1, false)
			{
			}
		}
	}
}
