using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using CG.Web.MegaApiClient.Serialization;
using Medo.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000F2 RID: 242
	public class MegaApiClient : IMegaApiClient
	{
		// Token: 0x06000846 RID: 2118 RVA: 0x00039D54 File Offset: 0x00037F54
		public MegaApiClient() : this(new Options("axhQiYyQ", true, null, 65536, 1048576), new WebClient(-1, null))
		{
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00039D7C File Offset: 0x00037F7C
		public MegaApiClient(Options options) : this(options, new WebClient(-1, null))
		{
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00039D8C File Offset: 0x00037F8C
		public MegaApiClient(IWebClient webClient) : this(new Options("axhQiYyQ", true, null, 65536, 1048576), webClient)
		{
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00039DAC File Offset: 0x00037FAC
		public MegaApiClient(Options options, IWebClient webClient)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (webClient == null)
			{
				throw new ArgumentNullException("webClient");
			}
			this.options = options;
			this.webClient = webClient;
			this.webClient.BufferSize = options.BufferSize;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00039E2C File Offset: 0x0003802C
		public MegaApiClient.AuthInfos GenerateAuthInfos(string email, string password, string mfaKey = null)
		{
			if (string.IsNullOrEmpty(email))
			{
				throw new ArgumentNullException("email");
			}
			if (string.IsNullOrEmpty(password))
			{
				throw new ArgumentNullException("password");
			}
			PreLoginRequest request = new PreLoginRequest(email);
			PreLoginResponse preLoginResponse = this.Request<PreLoginResponse>(request, null);
			if (preLoginResponse.Version == 2 && !string.IsNullOrEmpty(preLoginResponse.Salt))
			{
				byte[] salt = preLoginResponse.Salt.FromBase64();
				byte[] password2 = password.ToBytesPassword();
				byte[] array = new byte[32];
				using (HMACSHA512 hmacsha = new HMACSHA512())
				{
					array = new Pbkdf2(hmacsha, password2, salt, 100000).GetBytes(array.Length);
				}
				if (!string.IsNullOrEmpty(mfaKey))
				{
					return new MegaApiClient.AuthInfos(email, array.Skip(16).ToArray<byte>().ToBase64(), array.Take(16).ToArray<byte>(), mfaKey);
				}
				return new MegaApiClient.AuthInfos(email, array.Skip(16).ToArray<byte>().ToBase64(), array.Take(16).ToArray<byte>(), null);
			}
			else
			{
				if (preLoginResponse.Version != 1)
				{
					throw new NotSupportedException("Version of account not supported");
				}
				byte[] passwordAesKey = MegaApiClient.PrepareKey(password.ToBytesPassword());
				string hash = MegaApiClient.GenerateHash(email.ToLowerInvariant(), passwordAesKey);
				if (!string.IsNullOrEmpty(mfaKey))
				{
					return new MegaApiClient.AuthInfos(email, hash, passwordAesKey, mfaKey);
				}
				return new MegaApiClient.AuthInfos(email, hash, passwordAesKey, null);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600084B RID: 2123 RVA: 0x00039FA4 File Offset: 0x000381A4
		// (remove) Token: 0x0600084C RID: 2124 RVA: 0x00039FE0 File Offset: 0x000381E0
		public event EventHandler<ApiRequestFailedEventArgs> ApiRequestFailed;

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0003A01C File Offset: 0x0003821C
		public bool IsLoggedIn
		{
			get
			{
				return this.sessionId != null;
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0003A028 File Offset: 0x00038228
		public MegaApiClient.LogonSessionToken Login(string email, string password)
		{
			return this.Login(this.GenerateAuthInfos(email, password, null));
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0003A03C File Offset: 0x0003823C
		public MegaApiClient.LogonSessionToken Login(string email, string password, string mfaKey)
		{
			return this.Login(this.GenerateAuthInfos(email, password, mfaKey));
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0003A050 File Offset: 0x00038250
		public MegaApiClient.LogonSessionToken Login(MegaApiClient.AuthInfos authInfos)
		{
			if (authInfos == null)
			{
				throw new ArgumentNullException("authInfos");
			}
			this.EnsureLoggedOut();
			this.authenticatedLogin = true;
			LoginRequest request;
			if (!string.IsNullOrEmpty(authInfos.MFAKey))
			{
				request = new LoginRequest(authInfos.Email, authInfos.Hash, authInfos.MFAKey);
			}
			else
			{
				request = new LoginRequest(authInfos.Email, authInfos.Hash);
			}
			LoginResponse loginResponse = this.Request<LoginResponse>(request, null);
			byte[] data = loginResponse.MasterKey.FromBase64();
			this.masterKey = Crypto.DecryptKey(data, authInfos.PasswordAesKey);
			BigInteger[] rsaPrivateKeyComponents = Crypto.GetRsaPrivateKeyComponents(loginResponse.PrivateKey.FromBase64(), this.masterKey);
			byte[] source = Crypto.RsaDecrypt(loginResponse.SessionId.FromBase64().FromMPINumber(), rsaPrivateKeyComponents[0], rsaPrivateKeyComponents[1], rsaPrivateKeyComponents[2]);
			this.sessionId = source.Take(43).ToArray<byte>().ToBase64();
			return new MegaApiClient.LogonSessionToken(this.sessionId, this.masterKey);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0003A150 File Offset: 0x00038350
		public void Login(MegaApiClient.LogonSessionToken logonSessionToken)
		{
			this.EnsureLoggedOut();
			this.authenticatedLogin = true;
			this.sessionId = logonSessionToken.SessionId;
			this.masterKey = logonSessionToken.MasterKey;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0003A178 File Offset: 0x00038378
		public void Login()
		{
			this.LoginAnonymous();
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0003A180 File Offset: 0x00038380
		public void LoginAnonymous()
		{
			this.EnsureLoggedOut();
			this.authenticatedLogin = false;
			Random random = new Random();
			this.masterKey = new byte[16];
			random.NextBytes(this.masterKey);
			byte[] array = new byte[16];
			random.NextBytes(array);
			byte[] array2 = new byte[16];
			random.NextBytes(array2);
			byte[] data = Crypto.EncryptAes(this.masterKey, array);
			byte[] array3 = Crypto.EncryptAes(array2, this.masterKey);
			byte[] array4 = new byte[32];
			Array.Copy(array2, 0, array4, 0, 16);
			Array.Copy(array3, 0, array4, 16, array3.Length);
			AnonymousLoginRequest request = new AnonymousLoginRequest(data.ToBase64(), array4.ToBase64());
			LoginRequest request2 = new LoginRequest(this.Request(request), null);
			LoginResponse loginResponse = this.Request<LoginResponse>(request2, null);
			this.sessionId = loginResponse.TemporarySessionId;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0003A24C File Offset: 0x0003844C
		public void Logout()
		{
			this.EnsureLoggedIn();
			if (this.authenticatedLogin)
			{
				this.Request(new LogoutRequest());
			}
			this.masterKey = null;
			this.sessionId = null;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0003A27C File Offset: 0x0003847C
		public string GetRecoveryKey()
		{
			this.EnsureLoggedIn();
			if (!this.authenticatedLogin)
			{
				throw new NotSupportedException("Anonymous login is not supported");
			}
			return this.masterKey.ToBase64();
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0003A2A8 File Offset: 0x000384A8
		public IAccountInformation GetAccountInformation()
		{
			this.EnsureLoggedIn();
			AccountInformationRequest request = new AccountInformationRequest();
			return this.Request<AccountInformationResponse>(request, null);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0003A2D0 File Offset: 0x000384D0
		public IEnumerable<ISession> GetSessionsHistory()
		{
			this.EnsureLoggedIn();
			SessionHistoryRequest request = new SessionHistoryRequest();
			return this.Request<SessionHistoryResponse>(request, null);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0003A2F8 File Offset: 0x000384F8
		public IEnumerable<INode> GetNodes()
		{
			this.EnsureLoggedIn();
			GetNodesRequest request = new GetNodesRequest(null);
			Node[] nodes = this.Request<GetNodesResponse>(request, this.masterKey).Nodes;
			if (this.trashNode == null)
			{
				this.trashNode = nodes.First((Node n) => n.Type == NodeType.Trash);
			}
			return nodes.Distinct<Node>().OfType<INode>();
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0003A370 File Offset: 0x00038570
		public IEnumerable<INode> GetNodes(INode parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			return from n in this.GetNodes()
			where n.ParentId == parent.Id
			select n;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0003A3BC File Offset: 0x000385BC
		public void Delete(INode node, bool moveToTrash = true)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.Type != NodeType.Directory && node.Type != NodeType.File)
			{
				throw new ArgumentException("Invalid node type");
			}
			this.EnsureLoggedIn();
			if (moveToTrash)
			{
				this.Move(node, this.trashNode);
				return;
			}
			this.Request(new DeleteRequest(node));
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0003A428 File Offset: 0x00038628
		public INode CreateFolder(string name, INode parent)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (parent.Type == NodeType.File)
			{
				throw new ArgumentException("Invalid parent node");
			}
			this.EnsureLoggedIn();
			byte[] array = Crypto.CreateAesKey();
			byte[] data = Crypto.EncryptAttributes(new Attributes(name), array);
			byte[] data2 = Crypto.EncryptAes(array, this.masterKey);
			CreateNodeRequest request = CreateNodeRequest.CreateFolderNodeRequest(parent, data.ToBase64(), data2.ToBase64(), array);
			return this.Request<GetNodesResponse>(request, this.masterKey).Nodes[0];
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0003A4C8 File Offset: 0x000386C8
		public Uri GetDownloadLink(INode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.Type != NodeType.File && node.Type != NodeType.Directory)
			{
				throw new ArgumentException("Invalid node");
			}
			this.EnsureLoggedIn();
			if (node.Type == NodeType.Directory)
			{
				this.Request(new ShareNodeRequest(node, this.masterKey, this.GetNodes()));
				node = this.GetNodes().First((INode x) => x.Equals(node));
			}
			INodeCrypto nodeCrypto = node as INodeCrypto;
			if (nodeCrypto == null)
			{
				throw new ArgumentException("node must implement INodeCrypto");
			}
			GetDownloadLinkRequest request = new GetDownloadLinkRequest(node);
			string arg = this.Request<string>(request, null);
			return new Uri(MegaApiClient.BaseUri, string.Format("/{0}/{1}#{2}", (node.Type == NodeType.Directory) ? "folder" : "file", arg, (node.Type == NodeType.Directory) ? nodeCrypto.SharedKey.ToBase64() : nodeCrypto.FullKey.ToBase64()));
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0003A610 File Offset: 0x00038810
		public void DownloadFile(INode node, string outputFile, CancellationToken? cancellationToken = null)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (string.IsNullOrEmpty(outputFile))
			{
				throw new ArgumentNullException("outputFile");
			}
			using (Stream stream = this.Download(node, cancellationToken))
			{
				this.SaveStream(stream, outputFile);
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0003A678 File Offset: 0x00038878
		public void DownloadFile(Uri uri, string outputFile, CancellationToken? cancellationToken = null)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (string.IsNullOrEmpty(outputFile))
			{
				throw new ArgumentNullException("outputFile");
			}
			using (Stream stream = this.Download(uri, cancellationToken))
			{
				this.SaveStream(stream, outputFile);
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0003A6E8 File Offset: 0x000388E8
		public Stream Download(INode node, CancellationToken? cancellationToken = null)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.Type != NodeType.File)
			{
				throw new ArgumentException("Invalid node");
			}
			INodeCrypto nodeCrypto = node as INodeCrypto;
			if (nodeCrypto == null)
			{
				throw new ArgumentException("node must implement INodeCrypto");
			}
			this.EnsureLoggedIn();
			DownloadUrlRequest request = new DownloadUrlRequest(node);
			DownloadUrlResponse downloadUrlResponse = this.Request<DownloadUrlResponse>(request, null);
			Stream stream = new MegaAesCtrStreamDecrypter(new BufferedStream(this.webClient.GetRequestRaw(new Uri(downloadUrlResponse.Url))), downloadUrlResponse.Size, nodeCrypto.Key, nodeCrypto.Iv, nodeCrypto.MetaMac);
			if (cancellationToken != null)
			{
				stream = new CancellableStream(stream, cancellationToken.Value);
			}
			return stream;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0003A7A4 File Offset: 0x000389A4
		public Stream Download(Uri uri, CancellationToken? cancellationToken = null)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.EnsureLoggedIn();
			string id;
			byte[] iv;
			byte[] expectedMetaMac;
			byte[] fileKey;
			this.GetPartsFromUri(uri, out id, out iv, out expectedMetaMac, out fileKey);
			DownloadUrlRequestFromId request = new DownloadUrlRequestFromId(id);
			DownloadUrlResponse downloadUrlResponse = this.Request<DownloadUrlResponse>(request, null);
			Stream stream = new MegaAesCtrStreamDecrypter(new BufferedStream(this.webClient.GetRequestRaw(new Uri(downloadUrlResponse.Url))), downloadUrlResponse.Size, fileKey, iv, expectedMetaMac);
			if (cancellationToken != null)
			{
				stream = new CancellableStream(stream, cancellationToken.Value);
			}
			return stream;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0003A840 File Offset: 0x00038A40
		public INodeInfo GetNodeFromLink(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.EnsureLoggedIn();
			string id;
			byte[] array;
			byte[] array2;
			byte[] key;
			this.GetPartsFromUri(uri, out id, out array, out array2, out key);
			DownloadUrlRequestFromId request = new DownloadUrlRequestFromId(id);
			DownloadUrlResponse downloadResponse = this.Request<DownloadUrlResponse>(request, null);
			return new NodeInfo(id, downloadResponse, key);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0003A89C File Offset: 0x00038A9C
		public IEnumerable<INode> GetNodesFromLink(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.EnsureLoggedIn();
			string shareId;
			byte[] array;
			byte[] array2;
			byte[] key;
			this.GetPartsFromUri(uri, out shareId, out array, out array2, out key);
			GetNodesRequest request = new GetNodesRequest(shareId);
			return (from x in this.Request<GetNodesResponse>(request, key).Nodes
			select new PublicNode(x, shareId)).OfType<INode>();
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0003A914 File Offset: 0x00038B14
		public INode UploadFile(string filename, INode parent, CancellationToken? cancellationToken = null)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (!File.Exists(filename))
			{
				throw new FileNotFoundException(filename);
			}
			this.EnsureLoggedIn();
			DateTime lastWriteTime = File.GetLastWriteTime(filename);
			INode result;
			using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
			{
				result = this.Upload(fileStream, Path.GetFileName(filename), parent, new DateTime?(lastWriteTime), cancellationToken);
			}
			return result;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0003A9AC File Offset: 0x00038BAC
		public INode Upload(Stream stream, string name, INode parent, DateTime? modificationDate = null, CancellationToken? cancellationToken = null)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (parent.Type == NodeType.File)
			{
				throw new ArgumentException("Invalid parent node");
			}
			this.EnsureLoggedIn();
			if (cancellationToken != null)
			{
				stream = new CancellableStream(stream, cancellationToken.Value);
			}
			string text = string.Empty;
			int num = 0;
			TimeSpan retryDelay;
			while (this.options.ComputeApiRequestRetryWaitDelay(++num, out retryDelay))
			{
				UploadUrlRequest request = new UploadUrlRequest(stream.Length);
				UploadUrlResponse uploadUrlResponse = this.Request<UploadUrlResponse>(request, null);
				ApiResultCode apiResultCode = ApiResultCode.Ok;
				using (MegaAesCtrStreamCrypter megaAesCtrStreamCrypter = new MegaAesCtrStreamCrypter(stream))
				{
					long num2 = 0L;
					int[] array = this.ComputeChunksSizesToUpload(megaAesCtrStreamCrypter.ChunksPositions, megaAesCtrStreamCrypter.Length).ToArray<int>();
					Uri url = null;
					for (int i = 0; i < array.Length; i++)
					{
						text = string.Empty;
						int num3 = array[i];
						byte[] buffer = new byte[num3];
						megaAesCtrStreamCrypter.Read(buffer, 0, num3);
						using (MemoryStream memoryStream = new MemoryStream(buffer))
						{
							url = new Uri(uploadUrlResponse.Url + "/" + num2);
							num2 += (long)num3;
							try
							{
								text = this.webClient.PostRequestRaw(url, memoryStream);
								long num4;
								if (string.IsNullOrEmpty(text))
								{
									apiResultCode = ApiResultCode.Ok;
								}
								else if (text.FromBase64().Length != 27 && long.TryParse(text, out num4))
								{
									apiResultCode = (ApiResultCode)num4;
									break;
								}
							}
							catch (Exception exception)
							{
								apiResultCode = ApiResultCode.RequestFailedRetry;
								EventHandler<ApiRequestFailedEventArgs> apiRequestFailed = this.ApiRequestFailed;
								if (apiRequestFailed != null)
								{
									apiRequestFailed(this, new ApiRequestFailedEventArgs(url, num, retryDelay, apiResultCode, exception));
								}
								break;
							}
						}
					}
					if (apiResultCode == ApiResultCode.Ok)
					{
						byte[] data = Crypto.EncryptAttributes(new Attributes(name, stream, modificationDate), megaAesCtrStreamCrypter.FileKey);
						byte[] array2 = new byte[32];
						for (int j = 0; j < 8; j++)
						{
							array2[j] = (megaAesCtrStreamCrypter.FileKey[j] ^ megaAesCtrStreamCrypter.Iv[j]);
							array2[j + 16] = megaAesCtrStreamCrypter.Iv[j];
						}
						for (int k = 8; k < 16; k++)
						{
							array2[k] = (megaAesCtrStreamCrypter.FileKey[k] ^ megaAesCtrStreamCrypter.MetaMac[k - 8]);
							array2[k + 16] = megaAesCtrStreamCrypter.MetaMac[k - 8];
						}
						byte[] data2 = Crypto.EncryptKey(array2, this.masterKey);
						CreateNodeRequest request2 = CreateNodeRequest.CreateFileNodeRequest(parent, data.ToBase64(), data2.ToBase64(), array2, text);
						return this.Request<GetNodesResponse>(request2, this.masterKey).Nodes[0];
					}
					EventHandler<ApiRequestFailedEventArgs> apiRequestFailed2 = this.ApiRequestFailed;
					if (apiRequestFailed2 != null)
					{
						apiRequestFailed2(this, new ApiRequestFailedEventArgs(url, num, retryDelay, apiResultCode, text));
					}
					if (apiResultCode == ApiResultCode.RequestFailedRetry || apiResultCode == ApiResultCode.RequestFailedPermanetly || apiResultCode == ApiResultCode.TooManyRequests)
					{
						this.Wait(retryDelay);
						stream.Seek(0L, SeekOrigin.Begin);
						continue;
					}
					throw new ApiException(apiResultCode);
				}
				break;
			}
			throw new UploadException(text);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0003AD40 File Offset: 0x00038F40
		public INode Move(INode node, INode destinationParentNode)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (destinationParentNode == null)
			{
				throw new ArgumentNullException("destinationParentNode");
			}
			if (node.Type != NodeType.Directory && node.Type != NodeType.File)
			{
				throw new ArgumentException("Invalid node type");
			}
			if (destinationParentNode.Type == NodeType.File)
			{
				throw new ArgumentException("Invalid destination parent node");
			}
			this.EnsureLoggedIn();
			this.Request(new MoveRequest(node, destinationParentNode));
			return this.GetNodes().First((INode n) => n.Equals(node));
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0003ADF8 File Offset: 0x00038FF8
		public INode Rename(INode node, string newName)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.Type != NodeType.Directory && node.Type != NodeType.File)
			{
				throw new ArgumentException("Invalid node type");
			}
			if (string.IsNullOrEmpty(newName))
			{
				throw new ArgumentNullException("newName");
			}
			INodeCrypto nodeCrypto = node as INodeCrypto;
			if (nodeCrypto == null)
			{
				throw new ArgumentException("node must implement INodeCrypto");
			}
			this.EnsureLoggedIn();
			byte[] data = Crypto.EncryptAttributes(new Attributes(newName, ((Node)node).Attributes), nodeCrypto.Key);
			this.Request(new RenameRequest(node, data.ToBase64()));
			return this.GetNodes().First((INode n) => n.Equals(node));
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0003AEE4 File Offset: 0x000390E4
		private static string GenerateHash(string email, byte[] passwordAesKey)
		{
			byte[] array = email.ToBytes();
			byte[] array2 = new byte[16];
			for (int i = 0; i < array.Length; i++)
			{
				byte[] array3 = array2;
				int num = i % 16;
				array3[num] ^= array[i];
			}
			using (ICryptoTransform cryptoTransform = Crypto.CreateAesEncryptor(passwordAesKey))
			{
				for (int j = 0; j < 16384; j++)
				{
					array2 = Crypto.EncryptAes(array2, cryptoTransform);
				}
			}
			byte[] array4 = new byte[8];
			Array.Copy(array2, 0, array4, 0, 4);
			Array.Copy(array2, 8, array4, 4, 4);
			return array4.ToBase64();
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0003AF90 File Offset: 0x00039190
		private static byte[] PrepareKey(byte[] data)
		{
			byte[] array = new byte[]
			{
				147,
				196,
				103,
				227,
				125,
				176,
				199,
				164,
				209,
				190,
				63,
				129,
				1,
				82,
				203,
				86
			};
			for (int i = 0; i < 65536; i++)
			{
				for (int j = 0; j < data.Length; j += 16)
				{
					byte[] key = data.CopySubArray(16, j);
					array = Crypto.EncryptAes(array, key);
				}
			}
			return array;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0003AFEC File Offset: 0x000391EC
		private string Request(RequestBase request)
		{
			return this.Request<string>(request, null);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0003AFF8 File Offset: 0x000391F8
		private TResponse Request<TResponse>(RequestBase request, byte[] key = null) where TResponse : class
		{
			if (this.options.SynchronizeApiRequests)
			{
				object obj = this.apiRequestLocker;
				lock (obj)
				{
					return this.RequestCore<TResponse>(request, key);
				}
			}
			return this.RequestCore<TResponse>(request, key);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0003B05C File Offset: 0x0003925C
		private TResponse RequestCore<TResponse>(RequestBase request, byte[] key) where TResponse : class
		{
			string jsonData = JsonConvert.SerializeObject(new object[]
			{
				request
			});
			Uri url = this.GenerateUrl(request.QueryArguments);
			object obj = null;
			int num = 0;
			TimeSpan retryDelay;
			while (this.options.ComputeApiRequestRetryWaitDelay(++num, out retryDelay))
			{
				string text = this.webClient.PostRequestJson(url, jsonData);
				if (!string.IsNullOrEmpty(text) && (obj = JsonConvert.DeserializeObject(text)) != null && !(obj is long) && (!(obj is JArray) || ((JArray)obj)[0].Type != JTokenType.Integer))
				{
					break;
				}
				ApiResultCode apiResultCode = (obj == null) ? ApiResultCode.RequestFailedRetry : ((obj is long) ? ((ApiResultCode)Enum.ToObject(typeof(ApiResultCode), obj)) : ((ApiResultCode)((JArray)obj)[0].Value<int>()));
				if (apiResultCode != ApiResultCode.Ok)
				{
					EventHandler<ApiRequestFailedEventArgs> apiRequestFailed = this.ApiRequestFailed;
					if (apiRequestFailed != null)
					{
						apiRequestFailed(this, new ApiRequestFailedEventArgs(url, num, retryDelay, apiResultCode, text));
					}
				}
				if (apiResultCode == ApiResultCode.RequestFailedRetry)
				{
					this.Wait(retryDelay);
				}
				else
				{
					if (apiResultCode != ApiResultCode.Ok)
					{
						throw new ApiException(apiResultCode);
					}
					break;
				}
			}
			string text2 = ((JArray)obj)[0].ToString();
			if (!(typeof(TResponse) == typeof(string)))
			{
				return JsonConvert.DeserializeObject<TResponse>(text2, new JsonConverter[]
				{
					new GetNodesResponseConverter(key)
				});
			}
			return text2 as TResponse;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0003B1EC File Offset: 0x000393EC
		private void Wait(TimeSpan retryDelay)
		{
			Thread.Sleep(retryDelay);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0003B1F4 File Offset: 0x000393F4
		private Uri GenerateUrl(Dictionary<string, string> queryArguments)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(queryArguments);
			Dictionary<string, string> dictionary2 = dictionary;
			string key = "id";
			uint num = this.sequenceIndex;
			this.sequenceIndex = num + 1u;
			dictionary2[key] = (num % uint.MaxValue).ToString(CultureInfo.InvariantCulture);
			dictionary["ak"] = this.options.ApplicationKey;
			if (!string.IsNullOrEmpty(this.sessionId))
			{
				dictionary["sid"] = this.sessionId;
			}
			UriBuilder uriBuilder = new UriBuilder(MegaApiClient.BaseApiUri);
			string text = "";
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				text = string.Concat(new string[]
				{
					text,
					keyValuePair.Key,
					"=",
					keyValuePair.Value,
					"&"
				});
			}
			text = text.Substring(0, text.Length - 1);
			uriBuilder.Query = text.ToString();
			return uriBuilder.Uri;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0003B314 File Offset: 0x00039514
		private void SaveStream(Stream stream, string outputFile)
		{
			using (FileStream fileStream = new FileStream(outputFile, FileMode.CreateNew, FileAccess.Write))
			{
				stream.CopyTo(fileStream, this.options.BufferSize);
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0003B360 File Offset: 0x00039560
		private void EnsureLoggedIn()
		{
			if (this.sessionId == null)
			{
				throw new NotSupportedException("Not logged in");
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0003B378 File Offset: 0x00039578
		private void EnsureLoggedOut()
		{
			if (this.sessionId != null)
			{
				throw new NotSupportedException("Already logged in");
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0003B390 File Offset: 0x00039590
		private void GetPartsFromUri(Uri uri, out string id, out byte[] iv, out byte[] metaMac, out byte[] key)
		{
			byte[] array;
			bool flag;
			if (!this.TryGetPartsFromUri(uri, out id, out array, out flag) && !this.TryGetPartsFromLegacyUri(uri, out id, out array, out flag))
			{
				throw new ArgumentException(string.Format("Invalid uri. Unable to extract Id and Key from the uri {0}", uri));
			}
			if (flag)
			{
				iv = null;
				metaMac = null;
				key = array;
				return;
			}
			Crypto.GetPartsFromDecryptedKey(array, out iv, out metaMac, out key);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0003B3F4 File Offset: 0x000395F4
		private bool TryGetPartsFromUri(Uri uri, out string id, out byte[] decryptedKey, out bool isFolder)
		{
			Match match = new Regex("/(?<type>(file|folder))/(?<id>[^#]+)#(?<key>[^$/]+)").Match(uri.PathAndQuery + uri.Fragment);
			if (match.Success)
			{
				id = match.Groups["id"].Value;
				decryptedKey = match.Groups["key"].Value.FromBase64();
				isFolder = (match.Groups["type"].Value == "folder");
				return true;
			}
			id = null;
			decryptedKey = null;
			isFolder = false;
			return false;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0003B494 File Offset: 0x00039694
		private bool TryGetPartsFromLegacyUri(Uri uri, out string id, out byte[] decryptedKey, out bool isFolder)
		{
			Match match = new Regex("#(?<type>F?)!(?<id>[^!]+)!(?<key>[^$!\\?]+)").Match(uri.Fragment);
			if (match.Success)
			{
				id = match.Groups["id"].Value;
				decryptedKey = match.Groups["key"].Value.FromBase64();
				isFolder = (match.Groups["type"].Value == "F");
				return true;
			}
			id = null;
			decryptedKey = null;
			isFolder = false;
			return false;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0003B528 File Offset: 0x00039728
		private IEnumerable<int> ComputeChunksSizesToUpload(long[] chunksPositions, long streamLength)
		{
			int num3;
			for (int i = 0; i < chunksPositions.Length; i = num3 + 1)
			{
				long num = chunksPositions[i];
				long num2 = (i == chunksPositions.Length - 1) ? streamLength : chunksPositions[i + 1];
				while (((int)(num2 - num) < this.options.ChunksPackSize || this.options.ChunksPackSize == -1) && i < chunksPositions.Length - 1)
				{
					num3 = i;
					i = num3 + 1;
					num2 = ((i == chunksPositions.Length - 1) ? streamLength : chunksPositions[i + 1]);
				}
				yield return (int)(num2 - num);
				num3 = i;
			}
			yield break;
		}

		// Token: 0x040004F0 RID: 1264
		private static readonly Uri BaseApiUri = new Uri("https://g.api.mega.co.nz/cs");

		// Token: 0x040004F1 RID: 1265
		private static readonly Uri BaseUri = new Uri("https://mega.nz");

		// Token: 0x040004F2 RID: 1266
		private readonly Options options;

		// Token: 0x040004F3 RID: 1267
		private readonly IWebClient webClient;

		// Token: 0x040004F4 RID: 1268
		private readonly object apiRequestLocker = new object();

		// Token: 0x040004F5 RID: 1269
		private Node trashNode;

		// Token: 0x040004F6 RID: 1270
		private string sessionId;

		// Token: 0x040004F7 RID: 1271
		private byte[] masterKey;

		// Token: 0x040004F8 RID: 1272
		private uint sequenceIndex = (uint)(4294967295.0 * new Random().NextDouble());

		// Token: 0x040004F9 RID: 1273
		private bool authenticatedLogin;

		// Token: 0x02000292 RID: 658
		public class AuthInfos
		{
			// Token: 0x06001712 RID: 5906 RVA: 0x00077854 File Offset: 0x00075A54
			public AuthInfos(string email, string hash, byte[] passwordAesKey, string mfaKey = null)
			{
				this.Email = email;
				this.Hash = hash;
				this.PasswordAesKey = passwordAesKey;
				this.MFAKey = mfaKey;
			}

			// Token: 0x170004B5 RID: 1205
			// (get) Token: 0x06001713 RID: 5907 RVA: 0x00077888 File Offset: 0x00075A88
			// (set) Token: 0x06001714 RID: 5908 RVA: 0x00077890 File Offset: 0x00075A90
			[JsonProperty]
			public string Email { get; private set; }

			// Token: 0x170004B6 RID: 1206
			// (get) Token: 0x06001715 RID: 5909 RVA: 0x0007789C File Offset: 0x00075A9C
			// (set) Token: 0x06001716 RID: 5910 RVA: 0x000778A4 File Offset: 0x00075AA4
			[JsonProperty]
			public string Hash { get; private set; }

			// Token: 0x170004B7 RID: 1207
			// (get) Token: 0x06001717 RID: 5911 RVA: 0x000778B0 File Offset: 0x00075AB0
			// (set) Token: 0x06001718 RID: 5912 RVA: 0x000778B8 File Offset: 0x00075AB8
			[JsonProperty]
			public byte[] PasswordAesKey { get; private set; }

			// Token: 0x170004B8 RID: 1208
			// (get) Token: 0x06001719 RID: 5913 RVA: 0x000778C4 File Offset: 0x00075AC4
			// (set) Token: 0x0600171A RID: 5914 RVA: 0x000778CC File Offset: 0x00075ACC
			[JsonProperty]
			public string MFAKey { get; private set; }
		}

		// Token: 0x02000293 RID: 659
		public class LogonSessionToken : IEquatable<MegaApiClient.LogonSessionToken>
		{
			// Token: 0x170004B9 RID: 1209
			// (get) Token: 0x0600171B RID: 5915 RVA: 0x000778D8 File Offset: 0x00075AD8
			[JsonProperty]
			public string SessionId { get; }

			// Token: 0x170004BA RID: 1210
			// (get) Token: 0x0600171C RID: 5916 RVA: 0x000778E0 File Offset: 0x00075AE0
			[JsonProperty]
			public byte[] MasterKey { get; }

			// Token: 0x0600171D RID: 5917 RVA: 0x000778E8 File Offset: 0x00075AE8
			private LogonSessionToken()
			{
			}

			// Token: 0x0600171E RID: 5918 RVA: 0x000778F0 File Offset: 0x00075AF0
			public LogonSessionToken(string sessionId, byte[] masterKey)
			{
				this.SessionId = sessionId;
				this.MasterKey = masterKey;
			}

			// Token: 0x0600171F RID: 5919 RVA: 0x00077908 File Offset: 0x00075B08
			public bool Equals(MegaApiClient.LogonSessionToken other)
			{
				return other != null && this.SessionId != null && other.SessionId != null && string.Compare(this.SessionId, other.SessionId) == 0 && this.MasterKey != null && other.MasterKey != null && this.MasterKey.SequenceEqual(other.MasterKey);
			}
		}
	}
}
