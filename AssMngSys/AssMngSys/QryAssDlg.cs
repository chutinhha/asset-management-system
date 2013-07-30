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
            checkBoxDate.Checked = true;
        }
        private void RecvDataEvent(object sender, RecvEventArgs e)
        {
            ShowPid(textBoxPid, e.SPid);
        }
        // ����ί�лص�����ʵ�ֽ�������Ϣ������ʾ
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
            sSqlCondition = "";
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
            if (comboBoxYnPrint.Text.Length != 0)
            {
                sSqlCondition += " and ynprint = '" + comboBoxYnPrint.Text + "'";
            }
            if (comboBoxYnWrite.Text.Length != 0)
            {
                sSqlCondition += " and ynwrite = '" + comboBoxYnWrite.Text + "'";
            }
            if (checkBoxDate.Checked)
            {
                string sStartDate = string.Format("{0}-{1:00}-{2:00} 00:00:00", dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
                string sEndDate = string.Format("{0}-{1:00}-{2:00} 23:59:59", dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day);
                sSqlCondition += string.Format(" and cre_tm between '{0}' and '{1}'", sStartDate, sEndDate);
            }

            if (comboBoxAlive.Text == "��Ч�ʲ�")
            {
                sSqlCondition += " and stat in('���','����')";
            }
            else if (comboBoxAlive.Text == "��Ч�ʲ�")
            {
                sSqlCondition += " and stat not in('���','����')";
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

        private void QryAssDlg_Load(object sender, EventArgs e)
        {
        }

        private void checkBoxDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBoxDate.Checked;
            dateTimePicker2.Enabled = checkBoxDate.Checked;   
        }

        private void comboBoxAlive_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxStat.Items.Clear();
            comboBoxStat.Text = "";
            if (comboBoxAlive.Text == "��Ч�ʲ�")
            {
                comboBoxStat.Items.Add("");
                comboBoxStat.Items.Add("���");
                comboBoxStat.Items.Add("����");
            }
            else if (comboBoxAlive.Text == "��Ч�ʲ�")
            {
                comboBoxStat.Items.Add("");
                comboBoxStat.Items.Add("�⻹");
                comboBoxStat.Items.Add("�˷�");
                comboBoxStat.Items.Add("��ʧ");
                comboBoxStat.Items.Add("����");
                comboBoxStat.Items.Add("ת��");
            }
            else
            {
                comboBoxStat.Items.Add("");
                comboBoxStat.Items.Add("���");
                comboBoxStat.Items.Add("����");
                comboBoxStat.Items.Add("�⻹");
                comboBoxStat.Items.Add("�˷�");
                comboBoxStat.Items.Add("��ʧ");
                comboBoxStat.Items.Add("����");
                comboBoxStat.Items.Add("ת��");
            }
        }
    }
}