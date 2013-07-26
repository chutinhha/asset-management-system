using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AssMngSys
{
    public partial class QryAssDlg : Form
    {
        public string sSqlCondition = "";
        MainForm mf;
        public QryAssDlg(MainForm f)
        {
            InitializeComponent(); 
            mf = f;
            mf.recvEvent += new MainForm.RecvEventHandler(this.RecvDataEvent);
        }
        private void RecvDataEvent(object sender, RecvEventArgs e)
        {
            ShowPid(textBoxPid, e.SPid);
        }
        // 利用委托回调机制实现界面上消息内容显示
        delegate void ShowPidCallBack(TextBox textBox, string str);
        private void ShowPid(TextBox textBox, string str)
        {
            if (textBox.InvokeRequired)
            {
                ShowPidCallBack showPidCallBack = ShowPid;
                textBox.Invoke(showPidCallBack, new object[] { textBox, str });
            }
            else
            {
                textBox.Text = str;
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxPid.Text.Length != 0)
            {
                sSqlCondition += " and pid = '" + textBoxPid.Text + "'";
            }
            if (textBoxAssId.Text.Length != 0)
            {
                sSqlCondition += " and pid = '" + textBoxAssId.Text + "'";
            }
            if (comboBoxStat.Text.Length != 0)
            {
                sSqlCondition += " and stat = '" + comboBoxStat.Text + "'";
            }
            if (comboBoxStatSub.Text.Length != 0)
            {
                sSqlCondition += " and stat_sub = '" + comboBoxStatSub.Text + "'";
            }
            this.Close();
        }

        private void QryAssDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            mf.recvEvent -= new MainForm.RecvEventHandler(this.RecvDataEvent);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}