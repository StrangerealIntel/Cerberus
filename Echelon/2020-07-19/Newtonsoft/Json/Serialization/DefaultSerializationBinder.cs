using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000192 RID: 402
	[NullableContext(1)]
	[Nullable(0)]
	public class DefaultSerializationBinder : SerializationBinder, ISerializationBinder
	{
		// Token: 0x06000EDC RID: 3804 RVA: 0x00055AE8 File Offset: 0x00053CE8
		public DefaultSerializationBinder()
		{
			this._typeCache = new ThreadSafeStore<StructMultiKey<string, string>, Type>(new Func<StructMultiKey<string, string>, Type>(this.GetTypeFromTypeNameKey));
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00055B08 File Offset: 0x00053D08
		private Type GetTypeFromTypeNameKey([Nullable(new byte[]
		{
			0,
			2,
			1
		})] StructMultiKey<string, string> typeNameKey)
		{
			string value = typeNameKey.Value1;
			string value2 = typeNameKey.Value2;
			if (value == null)
			{
				return Type.GetType(value2);
			}
			Assembly assembly = Assembly.LoadWithPartialName(value);
			if (assembly == null)
			{
				foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
				{
					if (assembly2.FullName == value || assembly2.GetName().Name == value)
					{
						assembly = assembly2;
						break;
					}
				}
			}
			if (assembly == null)
			{
				throw new JsonSerializationException("Could not load assembly '{0}'.".FormatWith(CultureInfo.InvariantCulture, value));
			}
			Type type = assembly.GetType(value2);
			if (type == null)
			{
				if (value2.IndexOf('`') >= 0)
				{
					try
					{
						type = this.GetGenericTypeFromTypeName(value2, assembly);
					}
					catch (Exception innerException)
					{
						throw new JsonSerializationException("Could not find type '{0}' in assembly '{1}'.".FormatWith(CultureInfo.InvariantCulture, value2, assembly.FullName), innerException);
					}
				}
				if (type == null)
				{
					throw new JsonSerializationException("Could not find type '{0}' in assembly '{1}'.".FormatWith(CultureInfo.InvariantCulture, value2, assembly.FullName));
				}
			}
			return type;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00055C50 File Offset: 0x00053E50
		[return: Nullable(2)]
		private Type GetGenericTypeFromTypeName(string typeName, Assembly assembly)
		{
			Type result = null;
			int num = typeName.IndexOf('[');
			if (num >= 0)
			{
				string name = typeName.Substring(0, num);
				Type type = assembly.GetType(name);
				if (type != null)
				{
					List<Type> list = new List<Type>();
					int num2 = 0;
					int num3 = 0;
					int num4 = typeName.Length - 1;
					for (int i = num + 1; i < num4; i++)
					{
						char c = typeName[i];
						if (c != '[')
						{
							if (c == ']')
							{
								num2--;
								if (num2 == 0)
								{
									StructMultiKey<string, string> typeNameKey = ReflectionUtils.SplitFullyQualifiedTypeName(typeName.Substring(num3, i - num3));
									list.Add(this.GetTypeByName(typeNameKey));
								}
							}
						}
						else
						{
							if (num2 == 0)
							{
								num3 = i + 1;
							}
							num2++;
						}
					}
					result = type.MakeGenericType(list.ToArray());
				}
			}
			return result;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00055D38 File Offset: 0x00053F38
		private Type GetTypeByName([Nullable(new byte[]
		{
			0,
			2,
			1
		})] StructMultiKey<string, string> typeNameKey)
		{
			return this._typeCache.Get(typeNameKey);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00055D48 File Offset: 0x00053F48
		public override Type BindToType([Nullable(2)] string assemblyName, string typeName)
		{
			return this.GetTypeByName(new StructMultiKey<string, string>(assemblyName, typeName));
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00055D58 File Offset: 0x00053F58
		[NullableContext(2)]
		public override void BindToName([Nullable(1)] Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = serializedType.Assembly.FullName;
			typeName = serializedType.FullName;
		}

		// Token: 0x04000793 RID: 1939
		internal static readonly DefaultSerializationBinder Instance = new DefaultSerializationBinder();

		// Token: 0x04000794 RID: 1940
		[Nullable(new byte[]
		{
			1,
			0,
			2,
			1,
			1
		})]
		private readonly ThreadSafeStore<StructMultiKey<string, string>, Type> _typeCache;
	}
}
