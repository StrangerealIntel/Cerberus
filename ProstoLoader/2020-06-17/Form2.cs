using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ProstoLoader.Properties;

// Token: 0x02000027 RID: 39
public partial class Form2 : Form
{
	// Token: 0x060003BE RID: 958 RVA: 0x0000A118 File Offset: 0x00008318
	public Form2()
	{
		this.InitializeComponent();
		this.flatCheckBox1.Boolean_0 = Settings.Default.Pos;
		if (this.flatCheckBox1.Boolean_0)
		{
			Form2.smethod_1(this.Login, Settings.Default.Login);
			Form2.smethod_1(this.Pass, Settings.Default.Pass);
		}
	}

	// Token: 0x060003BF RID: 959 RVA: 0x0000A180 File Offset: 0x00008380
	public static bool smethod_0()
	{
		bool result;
		try
		{
			WebClient webClient = Form2.smethod_2();
			try
			{
				Stream stream = Form2.smethod_3(webClient, "http://www.google.com");
				try
				{
					result = true;
				}
				finally
				{
					if (stream != null)
					{
						Form2.smethod_4(stream);
					}
				}
			}
			finally
			{
				if (webClient != null)
				{
					Form2.smethod_4(webClient);
				}
			}
		}
		catch
		{
			result = false;
		}
		return result;
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x0000A1E8 File Offset: 0x000083E8
	private void flatButton1_Click(object sender, EventArgs e)
	{
		if (Form2.smethod_0())
		{
			string string_ = "http://45.88.76.158/loader/i.php";
			WebClient webClient = Form2.smethod_2();
			try
			{
				NameValueCollection nameValueCollection_ = Form2.smethod_5();
				Form2.smethod_7(nameValueCollection_, "login", Form2.smethod_6(this.Login));
				Form2.smethod_7(nameValueCollection_, "password", Form2.smethod_6(this.Pass));
				Form2.smethod_7(nameValueCollection_, "count", "1");
				byte[] byte_ = Form2.smethod_8(webClient, string_, nameValueCollection_);
				Form2.string_0 = Form2.smethod_10(Form2.smethod_9(), byte_);
			}
			finally
			{
				if (webClient != null)
				{
					Form2.smethod_4(webClient);
				}
			}
			string text = Form2.string_0;
			if (text != null)
			{
				if (Form2.smethod_11(text, "1"))
				{
					if (this.flatCheckBox1.Boolean_0)
					{
						Settings.Default.Pos = this.flatCheckBox1.Boolean_0;
						Settings.Default.Login = Form2.smethod_6(this.Login);
						Settings.Default.Pass = Form2.smethod_6(this.Pass);
						Form2.smethod_12(Settings.Default);
					}
					else
					{
						Settings.Default.Pos = false;
						Settings.Default.Login = "";
						Settings.Default.Pass = "";
						Form2.smethod_12(Settings.Default);
					}
					Form2.smethod_13(new Form1());
					return;
				}
				if (Form2.smethod_11(text, "2"))
				{
					Form2.smethod_14("Неправильный логин или пароль!", " Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				if (Form2.smethod_11(text, "3"))
				{
					Form2.smethod_15("0!");
					return;
				}
				if (!Form2.smethod_11(text, "6"))
				{
					return;
				}
				Form2.smethod_14("Проблема с сервером!", " Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x0000A398 File Offset: 0x00008598
	private void method_0(object object_0)
	{
		if (!this.flatCheckBox2.Boolean_0)
		{
			goto IL_31;
		}
		IL_0D:
		int num = 1157833056;
		IL_12:
		switch ((num ^ 1613821797) % 4)
		{
		case 1:
			this.Pass.Boolean_1 = false;
			return;
		case 2:
			IL_31:
			this.Pass.Boolean_1 = true;
			num = 833052461;
			goto IL_12;
		case 3:
			goto IL_0D;
		}
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x000036E8 File Offset: 0x000018E8
	protected virtual void Dispose(bool disposing)
	{
		if (disposing && this.icontainer_0 != null)
		{
			Form2.smethod_4(this.icontainer_0);
		}
		base.Dispose(disposing);
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00002340 File Offset: 0x00000540
	static void smethod_1(Control control_0, string string_1)
	{
		control_0.Text = string_1;
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00003707 File Offset: 0x00001907
	static WebClient smethod_2()
	{
		return new WebClient();
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x0000370E File Offset: 0x0000190E
	static Stream smethod_3(WebClient webClient_0, string string_1)
	{
		return webClient_0.OpenRead(string_1);
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x00002FE1 File Offset: 0x000011E1
	static void smethod_4(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x00003717 File Offset: 0x00001917
	static NameValueCollection smethod_5()
	{
		return new NameValueCollection();
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x00002476 File Offset: 0x00000676
	static string smethod_6(Control control_0)
	{
		return control_0.Text;
	}

	// Token: 0x060003CA RID: 970 RVA: 0x0000371E File Offset: 0x0000191E
	static void smethod_7(NameValueCollection nameValueCollection_0, string string_1, string string_2)
	{
		nameValueCollection_0.Add(string_1, string_2);
	}

	// Token: 0x060003CB RID: 971 RVA: 0x00003728 File Offset: 0x00001928
	static byte[] smethod_8(WebClient webClient_0, string string_1, NameValueCollection nameValueCollection_0)
	{
		return webClient_0.UploadValues(string_1, nameValueCollection_0);
	}

	// Token: 0x060003CC RID: 972 RVA: 0x00003732 File Offset: 0x00001932
	static Encoding smethod_9()
	{
		return Encoding.UTF8;
	}

	// Token: 0x060003CD RID: 973 RVA: 0x00003739 File Offset: 0x00001939
	static string smethod_10(Encoding encoding_0, byte[] byte_0)
	{
		return encoding_0.GetString(byte_0);
	}

	// Token: 0x060003CE RID: 974 RVA: 0x00003742 File Offset: 0x00001942
	static bool smethod_11(string string_1, string string_2)
	{
		return string_1 == string_2;
	}

	// Token: 0x060003CF RID: 975 RVA: 0x0000374B File Offset: 0x0000194B
	static void smethod_12(SettingsBase settingsBase_0)
	{
		settingsBase_0.Save();
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x00003753 File Offset: 0x00001953
	static void smethod_13(Control control_0)
	{
		control_0.Show();
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x0000375B File Offset: 0x0000195B
	static DialogResult smethod_14(string string_1, string string_2, MessageBoxButtons messageBoxButtons_0, MessageBoxIcon messageBoxIcon_0)
	{
		return MessageBox.Show(string_1, string_2, messageBoxButtons_0, messageBoxIcon_0);
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00003766 File Offset: 0x00001966
	static DialogResult smethod_15(string string_1)
	{
		return MessageBox.Show(string_1);
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x00002068 File Offset: 0x00000268
	static Type smethod_16(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x0000376E File Offset: 0x0000196E
	static ComponentResourceManager smethod_17(Type type_0)
	{
		return new ComponentResourceManager(type_0);
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00003776 File Offset: 0x00001976
	static void smethod_18(Control control_0)
	{
		control_0.SuspendLayout();
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x0000377E File Offset: 0x0000197E
	static void smethod_19(Control control_0)
	{
		control_0.SuspendLayout();
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_20(Control control_0, Color color_0)
	{
		control_0.BackColor = color_0;
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x00002FC1 File Offset: 0x000011C1
	static Control.ControlCollection smethod_21(Control control_0)
	{
		return control_0.Controls;
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x00002B79 File Offset: 0x00000D79
	static void smethod_22(Control.ControlCollection controlCollection_0, Control control_0)
	{
		controlCollection_0.Add(control_0);
	}

	// Token: 0x060003DA RID: 986 RVA: 0x0000305F File Offset: 0x0000125F
	static void smethod_23(Control control_0, DockStyle dockStyle_0)
	{
		control_0.Dock = dockStyle_0;
	}

	// Token: 0x060003DB RID: 987 RVA: 0x000025D1 File Offset: 0x000007D1
	static Font smethod_24(string string_1, float float_0)
	{
		return new Font(string_1, float_0);
	}

	// Token: 0x060003DC RID: 988 RVA: 0x000025DA File Offset: 0x000007DA
	static void smethod_25(Control control_0, Font font_0)
	{
		control_0.Font = font_0;
	}

	// Token: 0x040000CD RID: 205
	private static string string_0;

	// Token: 0x040000CE RID: 206
	private IContainer icontainer_0;
}
