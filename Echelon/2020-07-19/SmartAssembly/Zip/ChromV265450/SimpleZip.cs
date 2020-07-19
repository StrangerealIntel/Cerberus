using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace SmartAssembly.Zip
{
	// Token: 0x0200007A RID: 122
	public static class SimpleZip
	{
		// Token: 0x0600028A RID: 650 RVA: 0x00015950 File Offset: 0x00013B50
		private static bool PublicKeysMatch(Assembly A_0, Assembly A_1)
		{
			return true;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00015954 File Offset: 0x00013B54
		private static ICryptoTransform GetAesTransform(byte[] A_0, byte[] A_1, bool A_2)
		{
			ICryptoTransform result;
			using (SymmetricAlgorithm symmetricAlgorithm = new RijndaelManaged())
			{
				result = (A_2 ? symmetricAlgorithm.CreateDecryptor(A_0, A_1) : symmetricAlgorithm.CreateEncryptor(A_0, A_1));
			}
			return result;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000159A8 File Offset: 0x00013BA8
		private static ICryptoTransform GetDesTransform(byte[] A_0, byte[] A_1, bool A_2)
		{
			ICryptoTransform result;
			using (DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider())
			{
				result = (A_2 ? descryptoServiceProvider.CreateDecryptor(A_0, A_1) : descryptoServiceProvider.CreateEncryptor(A_0, A_1));
			}
			return result;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000159FC File Offset: 0x00013BFC
		public static byte[] Unzip(byte[] A_0)
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			if (callingAssembly != executingAssembly && !ChromV265450.SimpleZip.PublicKeysMatch(executingAssembly, callingAssembly))
			{
				return null;
			}
			ChromV265450.SimpleZip.ZipStream zipStream = new ChromV265450.SimpleZip.ZipStream(A_0);
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
							new ChromV265450.SimpleZip.Inflater(array2).Inflate(array, i, num5);
						}
					}
					if (num2 == 2)
					{
						byte[] array3 = new byte[]
						{
							47,
							20,
							227,
							160,
							229,
							156,
							104,
							146
						};
						byte[] array4 = new byte[]
						{
							170,
							159,
							162,
							17,
							100,
							39,
							183,
							23
						};
						using (ICryptoTransform desTransform = ChromV265450.SimpleZip.GetDesTransform(array3, array4, true))
						{
							array = ChromV265450.SimpleZip.Unzip(desTransform.TransformFinalBlock(A_0, 4, A_0.Length - 4));
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
					using (ICryptoTransform aesTransform = ChromV265450.SimpleZip.GetAesTransform(array5, array6, true))
					{
						array = ChromV265450.SimpleZip.Unzip(aesTransform.TransformFinalBlock(A_0, 4, A_0.Length - 4));
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
			ChromV265450.SimpleZip.Inflater inflater = new ChromV265450.SimpleZip.Inflater(array7);
			array = new byte[num9];
			inflater.Inflate(array, 0, array.Length);
			IL_287:
			zipStream.Close();
			zipStream = null;
			return array;
		}

		// Token: 0x02000261 RID: 609
		internal sealed class Inflater
		{
			// Token: 0x060016C9 RID: 5833 RVA: 0x00075C4C File Offset: 0x00073E4C
			public Inflater(byte[] A_1)
			{
				this.input = new ChromV265450.SimpleZip.StreamManipulator();
				this.outputWindow = new ChromV265450.SimpleZip.OutputWindow();
				this.mode = 2;
				this.input.SetInput(A_1, 0, A_1.Length);
			}

			// Token: 0x060016CA RID: 5834 RVA: 0x00075C84 File Offset: 0x00073E84
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
							this.repLength = ChromV265450.SimpleZip.Inflater.CPLENS[symbol - 257];
							this.neededBits = ChromV265450.SimpleZip.Inflater.CPLEXT[symbol - 257];
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
					this.repDist = ChromV265450.SimpleZip.Inflater.CPDIST[symbol];
					this.neededBits = ChromV265450.SimpleZip.Inflater.CPDEXT[symbol];
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

			// Token: 0x060016CB RID: 5835 RVA: 0x00075E64 File Offset: 0x00074064
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
						this.litlenTree = ChromV265450.SimpleZip.InflaterHuffmanTree.defLitLenTree;
						this.distTree = ChromV265450.SimpleZip.InflaterHuffmanTree.defDistTree;
						this.mode = 7;
						break;
					case 2:
						this.dynHeader = new ChromV265450.SimpleZip.InflaterDynHeader();
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

			// Token: 0x060016CC RID: 5836 RVA: 0x00076054 File Offset: 0x00074254
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

			// Token: 0x04000A92 RID: 2706
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

			// Token: 0x04000A93 RID: 2707
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

			// Token: 0x04000A94 RID: 2708
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

			// Token: 0x04000A95 RID: 2709
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

			// Token: 0x04000A96 RID: 2710
			private int mode;

			// Token: 0x04000A97 RID: 2711
			private int neededBits;

			// Token: 0x04000A98 RID: 2712
			private int repLength;

			// Token: 0x04000A99 RID: 2713
			private int repDist;

			// Token: 0x04000A9A RID: 2714
			private int uncomprLen;

			// Token: 0x04000A9B RID: 2715
			private bool isLastBlock;

			// Token: 0x04000A9C RID: 2716
			private ChromV265450.SimpleZip.StreamManipulator input;

			// Token: 0x04000A9D RID: 2717
			private ChromV265450.SimpleZip.OutputWindow outputWindow;

			// Token: 0x04000A9E RID: 2718
			private ChromV265450.SimpleZip.InflaterDynHeader dynHeader;

			// Token: 0x04000A9F RID: 2719
			private ChromV265450.SimpleZip.InflaterHuffmanTree litlenTree;

			// Token: 0x04000AA0 RID: 2720
			private ChromV265450.SimpleZip.InflaterHuffmanTree distTree;
		}

		// Token: 0x02000262 RID: 610
		internal sealed class StreamManipulator
		{
			// Token: 0x060016CE RID: 5838 RVA: 0x00076130 File Offset: 0x00074330
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

			// Token: 0x060016CF RID: 5839 RVA: 0x000761D8 File Offset: 0x000743D8
			public void DropBits(int A_1)
			{
				this.buffer >>= A_1;
				this.bits_in_buffer -= A_1;
			}

			// Token: 0x060016D0 RID: 5840 RVA: 0x000761FC File Offset: 0x000743FC
			public int get_AvailableBits()
			{
				return this.bits_in_buffer;
			}

			// Token: 0x060016D1 RID: 5841 RVA: 0x00076204 File Offset: 0x00074404
			public int get_AvailableBytes()
			{
				return this.window_end - this.window_start + (this.bits_in_buffer >> 3);
			}

			// Token: 0x060016D2 RID: 5842 RVA: 0x0007621C File Offset: 0x0007441C
			public void SkipToByteBoundary()
			{
				this.buffer >>= (this.bits_in_buffer & 7);
				this.bits_in_buffer &= -8;
			}

			// Token: 0x060016D3 RID: 5843 RVA: 0x00076248 File Offset: 0x00074448
			public bool get_IsNeedingInput()
			{
				return this.window_start == this.window_end;
			}

			// Token: 0x060016D4 RID: 5844 RVA: 0x00076258 File Offset: 0x00074458
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

			// Token: 0x060016D6 RID: 5846 RVA: 0x00076340 File Offset: 0x00074540
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

			// Token: 0x04000AA1 RID: 2721
			private byte[] window;

			// Token: 0x04000AA2 RID: 2722
			private int window_start;

			// Token: 0x04000AA3 RID: 2723
			private int window_end;

			// Token: 0x04000AA4 RID: 2724
			private uint buffer;

			// Token: 0x04000AA5 RID: 2725
			private int bits_in_buffer;
		}

		// Token: 0x02000263 RID: 611
		internal sealed class OutputWindow
		{
			// Token: 0x060016D7 RID: 5847 RVA: 0x000763DC File Offset: 0x000745DC
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

			// Token: 0x060016D8 RID: 5848 RVA: 0x0007643C File Offset: 0x0007463C
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

			// Token: 0x060016D9 RID: 5849 RVA: 0x0007649C File Offset: 0x0007469C
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

			// Token: 0x060016DA RID: 5850 RVA: 0x00076560 File Offset: 0x00074760
			public int CopyStored(ChromV265450.SimpleZip.StreamManipulator A_1, int A_2)
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

			// Token: 0x060016DB RID: 5851 RVA: 0x00076610 File Offset: 0x00074810
			public int GetFreeSpace()
			{
				return 32768 - this.windowFilled;
			}

			// Token: 0x060016DC RID: 5852 RVA: 0x00076620 File Offset: 0x00074820
			public int GetAvailable()
			{
				return this.windowFilled;
			}

			// Token: 0x060016DD RID: 5853 RVA: 0x00076628 File Offset: 0x00074828
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

			// Token: 0x04000AA6 RID: 2726
			private byte[] window = new byte[32768];

			// Token: 0x04000AA7 RID: 2727
			private int windowEnd;

			// Token: 0x04000AA8 RID: 2728
			private int windowFilled;
		}

		// Token: 0x02000264 RID: 612
		internal sealed class InflaterHuffmanTree
		{
			// Token: 0x060016DF RID: 5855 RVA: 0x000766E4 File Offset: 0x000748E4
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
				ChromV265450.SimpleZip.InflaterHuffmanTree.defLitLenTree = new ChromV265450.SimpleZip.InflaterHuffmanTree(array);
				array = new byte[32];
				i = 0;
				while (i < 32)
				{
					array[i++] = 5;
				}
				ChromV265450.SimpleZip.InflaterHuffmanTree.defDistTree = new ChromV265450.SimpleZip.InflaterHuffmanTree(array);
			}

			// Token: 0x060016E0 RID: 5856 RVA: 0x0007678C File Offset: 0x0007498C
			public InflaterHuffmanTree(byte[] A_1)
			{
				this.BuildTree(A_1);
			}

			// Token: 0x060016E1 RID: 5857 RVA: 0x0007679C File Offset: 0x0007499C
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
						this.tree[(int)ChromV265450.SimpleZip.DeflaterHuffman.BitReverse(l)] = (short)(-num6 << 4 | k);
						num6 += 1 << k - 9;
					}
				}
				for (int m = 0; m < A_1.Length; m++)
				{
					int num8 = (int)A_1[m];
					if (num8 != 0)
					{
						num2 = array2[num8];
						int num9 = (int)ChromV265450.SimpleZip.DeflaterHuffman.BitReverse(num2);
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

			// Token: 0x060016E2 RID: 5858 RVA: 0x000769A0 File Offset: 0x00074BA0
			public int GetSymbol(ChromV265450.SimpleZip.StreamManipulator A_1)
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

			// Token: 0x04000AA9 RID: 2729
			private short[] tree;

			// Token: 0x04000AAA RID: 2730
			public static readonly ChromV265450.SimpleZip.InflaterHuffmanTree defLitLenTree;

			// Token: 0x04000AAB RID: 2731
			public static readonly ChromV265450.SimpleZip.InflaterHuffmanTree defDistTree;
		}

		// Token: 0x02000265 RID: 613
		internal sealed class InflaterDynHeader
		{
			// Token: 0x060016E4 RID: 5860 RVA: 0x00076A94 File Offset: 0x00074C94
			public bool Decode(ChromV265450.SimpleZip.StreamManipulator A_1)
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
						this.blLens[ChromV265450.SimpleZip.InflaterDynHeader.BL_ORDER[this.ptr]] = (byte)num;
						this.ptr++;
					}
					this.blTree = new ChromV265450.SimpleZip.InflaterHuffmanTree(this.blLens);
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
					int num3 = ChromV265450.SimpleZip.InflaterDynHeader.repBits[this.repSymbol];
					int num4 = A_1.PeekBits(num3);
					if (num4 < 0)
					{
						return false;
					}
					A_1.DropBits(num3);
					num4 += ChromV265450.SimpleZip.InflaterDynHeader.repMin[this.repSymbol];
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

			// Token: 0x060016E5 RID: 5861 RVA: 0x00076D20 File Offset: 0x00074F20
			public ChromV265450.SimpleZip.InflaterHuffmanTree BuildLitLenTree()
			{
				byte[] array = new byte[this.lnum];
				Array.Copy(this.litdistLens, 0, array, 0, this.lnum);
				return new ChromV265450.SimpleZip.InflaterHuffmanTree(array);
			}

			// Token: 0x060016E6 RID: 5862 RVA: 0x00076D58 File Offset: 0x00074F58
			public ChromV265450.SimpleZip.InflaterHuffmanTree BuildDistTree()
			{
				byte[] array = new byte[this.dnum];
				Array.Copy(this.litdistLens, this.lnum, array, 0, this.dnum);
				return new ChromV265450.SimpleZip.InflaterHuffmanTree(array);
			}

			// Token: 0x04000AAC RID: 2732
			private static readonly int[] repMin = new int[]
			{
				3,
				3,
				11
			};

			// Token: 0x04000AAD RID: 2733
			private static readonly int[] repBits = new int[]
			{
				2,
				3,
				7
			};

			// Token: 0x04000AAE RID: 2734
			private byte[] blLens;

			// Token: 0x04000AAF RID: 2735
			private byte[] litdistLens;

			// Token: 0x04000AB0 RID: 2736
			private ChromV265450.SimpleZip.InflaterHuffmanTree blTree;

			// Token: 0x04000AB1 RID: 2737
			private int mode;

			// Token: 0x04000AB2 RID: 2738
			private int lnum;

			// Token: 0x04000AB3 RID: 2739
			private int dnum;

			// Token: 0x04000AB4 RID: 2740
			private int blnum;

			// Token: 0x04000AB5 RID: 2741
			private int num;

			// Token: 0x04000AB6 RID: 2742
			private int repSymbol;

			// Token: 0x04000AB7 RID: 2743
			private byte lastLen;

			// Token: 0x04000AB8 RID: 2744
			private int ptr;

			// Token: 0x04000AB9 RID: 2745
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

		// Token: 0x02000266 RID: 614
		internal sealed class DeflaterHuffman
		{
			// Token: 0x060016E8 RID: 5864 RVA: 0x00076DE8 File Offset: 0x00074FE8
			public static short BitReverse(int A_0)
			{
				return (short)((int)ChromV265450.SimpleZip.DeflaterHuffman.bit4Reverse[A_0 & 15] << 12 | (int)ChromV265450.SimpleZip.DeflaterHuffman.bit4Reverse[A_0 >> 4 & 15] << 8 | (int)ChromV265450.SimpleZip.DeflaterHuffman.bit4Reverse[A_0 >> 8 & 15] << 4 | (int)ChromV265450.SimpleZip.DeflaterHuffman.bit4Reverse[A_0 >> 12]);
			}

			// Token: 0x060016E9 RID: 5865 RVA: 0x00076E24 File Offset: 0x00075024
			static DeflaterHuffman()
			{
				int i = 0;
				while (i < 144)
				{
					ChromV265450.SimpleZip.DeflaterHuffman.staticLCodes[i] = ChromV265450.SimpleZip.DeflaterHuffman.BitReverse(48 + i << 8);
					ChromV265450.SimpleZip.DeflaterHuffman.staticLLength[i++] = 8;
				}
				while (i < 256)
				{
					ChromV265450.SimpleZip.DeflaterHuffman.staticLCodes[i] = ChromV265450.SimpleZip.DeflaterHuffman.BitReverse(256 + i << 7);
					ChromV265450.SimpleZip.DeflaterHuffman.staticLLength[i++] = 9;
				}
				while (i < 280)
				{
					ChromV265450.SimpleZip.DeflaterHuffman.staticLCodes[i] = ChromV265450.SimpleZip.DeflaterHuffman.BitReverse(-256 + i << 9);
					ChromV265450.SimpleZip.DeflaterHuffman.staticLLength[i++] = 7;
				}
				while (i < 286)
				{
					ChromV265450.SimpleZip.DeflaterHuffman.staticLCodes[i] = ChromV265450.SimpleZip.DeflaterHuffman.BitReverse(-88 + i << 8);
					ChromV265450.SimpleZip.DeflaterHuffman.staticLLength[i++] = 8;
				}
				ChromV265450.SimpleZip.DeflaterHuffman.staticDCodes = new short[30];
				ChromV265450.SimpleZip.DeflaterHuffman.staticDLength = new byte[30];
				for (i = 0; i < 30; i++)
				{
					ChromV265450.SimpleZip.DeflaterHuffman.staticDCodes[i] = ChromV265450.SimpleZip.DeflaterHuffman.BitReverse(i << 11);
					ChromV265450.SimpleZip.DeflaterHuffman.staticDLength[i] = 5;
				}
			}

			// Token: 0x04000ABA RID: 2746
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

			// Token: 0x04000ABB RID: 2747
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

			// Token: 0x04000ABC RID: 2748
			private static readonly short[] staticLCodes = new short[286];

			// Token: 0x04000ABD RID: 2749
			private static readonly byte[] staticLLength = new byte[286];

			// Token: 0x04000ABE RID: 2750
			private static readonly short[] staticDCodes;

			// Token: 0x04000ABF RID: 2751
			private static readonly byte[] staticDLength;
		}

		// Token: 0x02000267 RID: 615
		internal sealed class ZipStream : MemoryStream
		{
			// Token: 0x060016EA RID: 5866 RVA: 0x00076F78 File Offset: 0x00075178
			public int ReadShort()
			{
				return this.ReadByte() | this.ReadByte() << 8;
			}

			// Token: 0x060016EB RID: 5867 RVA: 0x00076F8C File Offset: 0x0007518C
			public int ReadInt()
			{
				return this.ReadShort() | this.ReadShort() << 16;
			}

			// Token: 0x060016EC RID: 5868 RVA: 0x00076FA0 File Offset: 0x000751A0
			public ZipStream(byte[] A_1) : base(A_1, false)
			{
			}
		}
	}
}
