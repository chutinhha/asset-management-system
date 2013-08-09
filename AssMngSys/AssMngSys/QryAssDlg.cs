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
    public partial class QryAssDlg : Form
    {
        public string sSqlCondition = "";
        MainForm mf;
        public QryAssDlg(MainForm f)
        {
            InitializeComponent(); 
            mf = f;
            mf.recvEvent += new MainForm.RecvEventHandler(this.RecvDataEvent);
            //checkBoxDate.Checked = true;
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
        private void QryAssDlg_Load(object sender, EventArgs e)
        {
            //获取类别列表
            string sSql = "select distinct cat_nam from ass_cat order by convert(cat_nam using gb2312) asc";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            comboBoxTyp.Items.Add("");
            while (reader.Read())
            {
                comboBoxTyp.Items.Add(reader["cat_nam"].ToString());
            }
            reader.Close();

            sSql = "select distinct prd_nam from ass_cat where cat_nam = '" + comboBoxTyp.Text + "'order by convert(prd_nam using gb2312) asc";
            reader = MysqlHelper.ExecuteReader(sSql);
            comboBoxAssNam.Items.Add("");
            while (reader.Read())
            {
                comboBoxAssNam.Items.Add(reader["prd_nam"].ToString());
            }
            reader.Close();

            if (comboBoxAssNam.Items.Count == 1)
            {
                comboBoxAssNam.SelectedIndex = 0;
            }
            //获取部门列表
            sSql = "select distinct dept_nam from emp";
            reader = MysqlHelper.ExecuteReader(sSql);
            comboBoxDept.Items.Add("");
            while (reader.Read())
            {
                comboBoxDept.Items.Add(reader["dept_nam"].ToString());
            }
            reader.Close();
            //获取人员列表
            sSql = "select distinct emp_nam from emp order by convert(emp_nam using gb2312) asc";
            reader = MysqlHelper.ExecuteReader(sSql);
            comboBoxDutyMan.Items.Add("");
            comboBoxUseMan.Items.Add("");
            while (reader.Read())
            {
                comboBoxDutyMan.Items.Add(reader["emp_nam"].ToString());
                comboBoxUseMan.Items.Add(reader["emp_nam"].ToString());
            }
            reader.Close();
            //获取地点列表
            sSql = "select distinct addr_no from addr order by convert(addr_no using gb2312) asc";
            reader = MysqlHelper.ExecuteReader(sSql);
            comboBoxAddr.Items.Add("");
            while (reader.Read())
            {
                comboBoxAddr.Items.Add(reader["addr_no"].ToString());
            }
            reader.Close();
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
            if (comboBoxTyp.Text.Length != 0)
            {
                sSqlCondition += " and typ = '" + comboBoxTyp.Text + "'";
            }
            if (comboBoxAssNam.Text.Length != 0)
            {
                sSqlCondition += " and ass_nam = '" + comboBoxAssNam.Text + "'";
            }
            if (comboBoxUseMan.Text.Length != 0)
            {
                sSqlCondition += " and use_man = '" + comboBoxUseMan.Text + "'";
            }
            if (comboBoxDutyMan.Text.Length != 0)
            {
                sSqlCondition += " and duty_man = '" + comboBoxDutyMan.Text + "'";
            }
            if (comboBoxAddr.Text.Length != 0)
            {
                sSqlCondition += " and addr = '" + comboBoxAddr.Text + "'";
            }
            if (comboBoxDept.Text.Length != 0)
            {
                sSqlCondition += " and dept = '" + comboBoxDept.Text + "'";
            }
            if (comboBoxYnRepair.Text.Length != 0)
            {
                sSqlCondition += " and ynrepair = '" + comboBoxYnRepair.Text + "'";
            }

            if (comboBoxAlive.Text == "有效资产")
            {
                sSqlCondition += " and stat in('库存','领用')";
            }
            else if (comboBoxAlive.Text == "无效资产")
            {
                sSqlCondition += " and stat not in('库存','领用')";
            }   
            if (checkBoxDate.Checked)
            {
                string sStartDate = string.Format("{0}-{1:00}-{2:00} 00:00:00", dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
                string sEndDate = string.Format("{0}-{1:00}-{2:00} 23:59:59", dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day);
                sSqlCondition += string.Format(" and cre_tm between '{0}' and '{1}'", sStartDate, sEndDate);
            }
            if (checkBoxPri.Checked)
            {
                sSqlCondition += string.Format(" and ass_pri between '{0}' and '{1}'", textBoxPriLow.Text, textBoxPriUp.Text);
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

        private void checkBoxDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBoxDate.Checked;
            dateTimePicker2.Enabled = checkBoxDate.Checked;   
        }

        private void comboBoxAlive_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxStat.Items.Clear();
            comboBoxStat.Text = "";
            if (comboBoxAlive.Text == "有效资产")
            {
                comboBoxStat.Items.Add("");
                comboBoxStat.Items.Add("库存");
                comboBoxStat.Items.Add("领用");
            }
            else if (comboBoxAlive.Text == "无效资产")
            {
                comboBoxStat.Items.Add("");
                comboBoxStat.Items.Add("租还");
                comboBoxStat.Items.Add("退返");
                comboBoxStat.Items.Add("丢失");
                comboBoxStat.Items.Add("报废");
                comboBoxStat.Items.Add("转出");
            }
            else
            {
                comboBoxStat.Items.Add("");
                comboBoxStat.Items.Add("库存");
                comboBoxStat.Items.Add("领用");
                comboBoxStat.Items.Add("租还");
                comboBoxStat.Items.Add("退返");
                comboBoxStat.Items.Add("丢失");
                comboBoxStat.Items.Add("报废");
                comboBoxStat.Items.Add("转出");
            }
        }

        private void checkBoxPri_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPriLow.Enabled = checkBoxPri.Checked;
            textBoxPriUp.Enabled = checkBoxPri.Checked;   
        }

        private void comboBoxTyp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string str = comboBoxTyp.SelectedItem.ToString();
            comboBoxAssNam.Items.Clear();
            comboBoxAssNam.Text = "";
            string sSql = "select distinct prd_nam from ass_cat where cat_nam = '" + str + "'order by convert(prd_nam using gb2312) asc";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxAssNam.Items.Add(reader["prd_nam"].ToString());
            }
            reader.Close();

            if (comboBoxAssNam.Items.Count == 1)
            {
                comboBoxAssNam.SelectedIndex = 0;
            }
        }
    }
}