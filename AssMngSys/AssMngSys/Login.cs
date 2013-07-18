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
        public string sCompany = "�����ܹ�˾";
        public string sUserName = "SYS";
        public  int nRet = 0;//0 ��½ʧ�ܣ�1 ��½�ɹ���2 ȡ����½
        public Login()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxUser.Text.Length == 0 || textBoxPass.Text.Length == 0)
            {
                MessageBox.Show("�˺����벻��Ϊ�գ�");
                return;
            }
            string sSql = "select * from user where user_no = \'" + textBoxUser.Text + "\'";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            string sPass = "";
            string sStat = "";
            if (reader.Read())
            {
                sPass = reader["pass"].ToString();
                sUserName = reader["user_nam"].ToString();
                sCompany = reader["company"].ToString();
                sStat = reader["stat"].ToString();
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
                    MessageBox.Show("�˺���Ч��");
                }

            }
            else
            {
                MessageBox.Show("�˺Ż���������");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            nRet = 2;
            this.Close();
        }
    }
}