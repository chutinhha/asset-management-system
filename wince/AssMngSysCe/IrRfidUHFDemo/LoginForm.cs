using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data.SQLite;
using NameApi;

namespace IrRfidUHFDemo
{
    public partial class LoginForm : Form
    {
        //public static string sClientId = "";
        //public static string sClientVer = "";
        //public static string sUdpIp = "";
        //public static string sUdpPort = "";
        //public static string sWebSrvUrl = "";
        //public static string sCompany = "";
        public static string sUserName = "";
        public static string sRole = "";
        public static bool bOnline = false;
        public static bool bLogin = false;
        public static string sCodePath = "";
        public static string sPort = "";
      //  public static IniFile setting = new IniFile();
     //   public static SettingForm setting = new SettingForm();
        public int nRet = 0;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            ////打开电源
            //HTApi.WIrUHFOpenPower();
            //HTApi.WIrUHFConnect(2);
            Scanner.openUHF();

            //最大化
            this.WindowState = FormWindowState.Maximized;


            //wince下不支持相对路径
            sCodePath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            sCodePath = sCodePath.Substring(0, sCodePath.LastIndexOf(@"\"));
            //SQLite DB路径
            string sdbFilename = LoginForm.sCodePath + "\\gvdb.db";
            SQLiteHelper.connectionString = "Data Source=" + sdbFilename;

            SettingForm.LoadSetting();

            textBoxUser.Text = SettingForm.sUser;
            textBoxPass.Text = SettingForm.sPass;

            if (textBoxPass.Text.Length != 0)
            {
                checkBoxStorePass.Checked = true;
            }
            

            if (SettingForm.sClientId.Equals("PC") || SettingForm.sClientId.Length == 0)
            {
                SettingForm.sClientId = "";
                MessageBox.Show("初次使用，请设置客户端ID!\r\r注：请勿与其他手持机重复，否则会发生同步错误！");
                SettingForm dlg = new SettingForm();
                dlg.ShowDialog();
                //初始化同步
                MainForm f = new MainForm(this);
                f.bIsFirstIni = true;
                f.ShowDialog();

            }
            //setting.Load(sCodePath + "\\setting.xml");
            //IniFile f = new IniFile();
            //textBoxIp.Text = f.sIP;
            //textBoxPort.Text = f.sPort;
            //setting = ;
        }

        private void LoginForm_Closed(object sender, EventArgs e)
        {
            //HTApi.WIrUHFDisconnect();
            //HTApi.WIrUHFClosePower();
        }

        //private void buttonRead2Pc_Click(object sender, EventArgs e)
        //{
        //    Read2PcForm dlg = new Read2PcForm();
        //    dlg.ShowDialog();
        //}

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            if (textBoxUser.Text.Length == 0 || textBoxPass.Text.Length == 0)
            {
                MessageBox.Show("账号或密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                return;
            }
            //SQLite方式
            string sSql = "select * from user where user_no = \'" + textBoxUser.Text + "\'";
            SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql, null);
            string sPass = "";
            string sStat = "";
            if (reader.Read())
            {
                sPass = reader["pass"].ToString();
                sUserName = reader["user_nam"].ToString();
                sStat = reader["stat"].ToString();
            }
            reader.Close();
            if (!bLogin)
            {
                if (sPass.Equals(textBoxPass.Text))
                {
                    if (!sStat.Equals("0"))
                    {
                        bLogin = true;
                        buttonLogin.Text = "注销(Ent)";
                        textBoxUser.Visible = false;
                        textBoxPass.Visible = false;
                        checkBoxStorePass.Visible = false;
                        label2.Visible = false;
                        label1.Text = textBoxUser.Text + @"
" + sUserName + " " + sRole + @"
" + SettingForm.sCompany + @"
欢迎使用!";
                        buttonStart.Visible = true;

                        if (checkBoxStorePass.Checked)
                        {
                            sSql = string.Format("update sys_parms set  parm_nam = '{0}',parm_val = '{1}' where parm_id = 'store_accont'", textBoxUser.Text, sPass);
                            SQLiteHelper.ExecuteNonQuery(sSql, null);
                            SettingForm.sUser = textBoxUser.Text;
                            SettingForm.sPass = textBoxPass.Text;
                        }
                        else
                        {
                            sSql = string.Format("update sys_parms set  parm_nam = '{0}',parm_val = '{1}' where parm_id = 'store_accont'", textBoxUser.Text, "");
                            SQLiteHelper.ExecuteNonQuery(sSql, null);
                            SettingForm.sUser = textBoxUser.Text;
                            SettingForm.sPass = "";
                            textBoxPass.Text = "";
                        }

                        MainForm dlg = new MainForm(this);
                        dlg.ShowDialog();
                       // nRet = 1;
                       // this.Close();


                    }
                    else
                    {
                        MessageBox.Show("账号无效！");
                    }

                }
                else
                {
                    MessageBox.Show("账号或密码有误！");
                }
            }
            else
            {
                buttonStart.Visible = false;
                bLogin = false;
                buttonLogin.Text = "登陆(Ent)";
                textBoxUser.Visible = true;
                textBoxPass.Visible = true;
                buttonLogin.Visible = true;
                label1.Text = @"账号：";
                label2.Visible = true;
                checkBoxStorePass.Visible = true;
            }

        }

        private void menuItemPowerRate_Click(object sender, EventArgs e)
        {
            PowerForm dlg = new PowerForm();
            dlg.ShowDialog();
        }

        private void menuItemModeSet_Click(object sender, EventArgs e)
        {
            ModeForm dlg = new ModeForm();
            dlg.ShowDialog();
        }

        private void menuItemSetQ_Click(object sender, EventArgs e)
        {
            SetQForm dlg = new SetQForm();
            dlg.ShowDialog();
        }

        private void menuItemLockTag_Click(object sender, EventArgs e)
        {
            LockForm dlg = new LockForm();
            dlg.ShowDialog();
        }

        private void menuItemInv_Click(object sender, EventArgs e)
        {
            FindForm dlg = new FindForm();
            dlg.ShowDialog();
        }

        private void menuItemRead_Click(object sender, EventArgs e)
        {
            ReadForm dlg = new ReadForm();
            dlg.ShowDialog();
        }

        private void LoginForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                buttonCancel_Click(null, null);
            }
            else if (e.KeyCode == Keys.F1)
            {
                buttonStart_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                buttonLogin_Click(null, null);
            }
        }

        private void menuItemSetting_Click(object sender, EventArgs e)
        {
            SettingForm dlg = new SettingForm();
            dlg.ShowDialog();
        }
        
        //获取时间：yyyy-mm-dd hh24::mm:ss
        static public string getDateTime()
        {
            string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return s;
        }
        //获取时间：yyyymmdd
        static public string getDate()
        {
            string s = DateTime.Now.ToString("yyyy-MM-dd");
            return s;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!bLogin)
            {
                return;
            }
            else
            {
                MainForm dlg = new MainForm(this);
                dlg.ShowDialog();
            }
        }   
    }
}