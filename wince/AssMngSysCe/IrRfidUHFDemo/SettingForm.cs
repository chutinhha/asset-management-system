using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace IrRfidUHFDemo
{
    public partial class SettingForm : Form
    {
        public static string sClientId = "";
        public static string sClientVer = "";
        public static string sUdpIp = "10.1.1.52";
        public static string sUdpPort = "6035";
        public static string sWebSrvIp = "10.1.1.52";
        public static string sWebSrvUrl = "";
        public static string sCompany = "";
        public static string sSyncMax = "0";
        public static string sUser = "";
        public static string sPass = "";
        public SettingForm()
        {
            InitializeComponent();
        }
        public static void LoadSetting()
        {
            string sSql = "select * from sys_parms";
            SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql, null);
            string sParmId = "";
            string sParmVal = "";
            while (reader.Read())
            {
                sParmId = reader["parm_id"].ToString();
                sParmVal = reader["parm_val"].ToString();
                if (sParmId.Equals("client_id"))
                {
                    sClientId = sParmVal;
                }
                else if (sParmId.Equals("company"))
                {
                    sCompany = sParmVal;
                }
                else if (sParmId.Equals("client_ver"))
                {
                    sClientVer = sParmVal;
                }
                else if (sParmId.Equals("udp_ip"))
                {
                    sUdpIp = sParmVal;
                }
                else if (sParmId.Equals("udp_port"))
                {
                    sUdpPort = sParmVal;
                }
                else if (sParmId.Equals("websrv_ip"))
                {
                    sWebSrvIp = sParmVal;
                    sWebSrvUrl = "http://" + sWebSrvIp + "/AssWebSrv/Service.asmx";
                }
                else if (sParmId.Equals("sync_max"))
                {
                    sSyncMax = sParmVal;
                }
                else if (sParmId.Equals("store_accont"))
                {
                    sUser = reader["parm_nam"].ToString();
                    sPass = sParmVal;
                } 
            }
            reader.Close();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            comboBoxClientId.Text = sClientId;
            textBoxWebSrvIp.Text = sWebSrvIp;
            textBoxIp.Text = sUdpIp;
            textBoxPort.Text = sUdpPort;
        }

        private void buttonSaveSet_Click(object sender, EventArgs e)
        {
            //LoginForm.setting.sIP = textBoxIp.Text;
            //LoginForm.setting.sPort = textBoxPort.Text;
            //LoginForm.setting.Save();

            if (comboBoxClientId.Text.Length == 0)
            {
                MessageBox.Show("客户端号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                return;
            }

            List<string> listSql = new List<string>();
            listSql.Add(string.Format("update sys_parms set parm_val = '{0}' where parm_id = '{1}'", comboBoxClientId.Text, "client_id"));
            listSql.Add(string.Format("update sys_parms set parm_val = '{0}' where parm_id = '{1}'", textBoxWebSrvIp.Text, "websrv_ip"));
            listSql.Add(string.Format("update sys_parms set parm_val = '{0}' where parm_id = '{1}'", textBoxIp.Text, "udp_ip"));
            listSql.Add(string.Format("update sys_parms set parm_val = '{0}' where parm_id = '{1}'", textBoxPort.Text, "udp_port"));
            
            bool bOK = SQLiteHelper.ExecuteNoQueryTran(listSql);

            if (bOK)
            {
                sClientId = comboBoxClientId.Text;
                sWebSrvIp = textBoxWebSrvIp.Text;
                sUdpIp = textBoxIp.Text;
                sUdpPort = textBoxPort.Text;
                sWebSrvUrl = "http://" + sWebSrvIp + "/AssWebSrv/Service.asmx";
                MessageBox.Show("设置OK!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                this.Close();
            }
            else
            {
                MessageBox.Show("设置失败！\r\n" + SQLiteHelper.sLastErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSaveSet_Click(null, null);
            }
            else if (e.KeyCode == Keys.F3)
            {
                buttonCancel_Click(null, null);
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            string sErr;
            string sWebSrvUrl = "http://" + textBoxWebSrvIp.Text + "/AssWebSrv/Service.asmx";
            if (MainForm.getWSStatus(sWebSrvUrl, out sErr))
            {
                MessageBox.Show("测试OK!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("不可用！\r\n" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }

       }
    }
}