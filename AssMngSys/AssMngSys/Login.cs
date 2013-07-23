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
        public string sCompany = "";
        public string sUserName = "";
        public  int nRet = 0;//0 登陆失败，1 登陆成功，2 取消登陆
        public Login()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxUser.Text.Length == 0 || textBoxPass.Text.Length == 0)
            {
                MessageBox.Show("账号密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("账号无效！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("账号或密码有误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            nRet = 2;
            this.Close();
        }
    }
}