using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AssMngSys
{
    public partial class Login : Form
    {
        static public string sCompany = "";
        static public string sUserName = "";
        static public string sRole = "";
        static public string sClientId = "PC";
        public  int nRet = 0;//0 ��½ʧ�ܣ�1 ��½�ɹ���2 ȡ����½
        public Login()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            //string str = EncryptDES.EncryptHex("0123456789012345");//8���ֽ�
            //System.Diagnostics.Debug.WriteLine(str);
            //string str2 = EncryptDES.DecryptHex(str);
            //System.Diagnostics.Debug.WriteLine(str2);
            //return;


            //MessageBox.Show(int32.MaxValue.ToString());
            if (textBoxUser.Text.Length == 0 || textBoxPass.Text.Length == 0)
            {
                MessageBox.Show("�˺����벻��Ϊ�գ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sSql = "select * from user where user_no = \'" + textBoxUser.Text + "\'";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            string sPass = "";
            string sStat = "";
            if (reader.Read())
            {
                sUserName = reader["user_nam"].ToString();
                sCompany = reader["company"].ToString();
                sRole = reader["role"].ToString();
                
                sStat = reader["stat"].ToString();
                sPass = reader["pass"].ToString();
            }
            if (sPass.Equals(textBoxPass.Text))
            {
                if (!sStat.Equals("0"))
                {
                    nRet = 1;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("�˺���Ч��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("�˺Ż���������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            nRet = 2;
            this.Close();
        }
    }
}