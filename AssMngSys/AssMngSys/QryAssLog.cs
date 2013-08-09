using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AssMngSys
{
    public partial class QryAssLog : Form
    {
        MainForm mf;
        string sSQLSelect = "select Id 记录好,pid 标签喷码,ass_id 资产编码,opt_typ 操作类型,opt_man 确认人员,opt_date 操作日期,cre_man 操作人员,cre_tm 操作时间,company 所属公司,dept 所属部门,addr 地址,reason 事由 from ass_log ";
        public QryAssLog(MainForm f)
        {
            InitializeComponent(); 
            f.recvEvent += new MainForm.RecvEventHandler(this.RecvDataEvent);
            mf = f;
        }
        
        private void AssLogForm_Load(object sender, EventArgs e)
        {
            checkBoxDate.Checked = true;
            buttonQry_Click(null,null);
            comboBoxOptTyp.Items.Add("");
            comboBoxOptTyp.Items.Add("领用");
            comboBoxOptTyp.Items.Add("退领");
            comboBoxOptTyp.Items.Add("借用");
            comboBoxOptTyp.Items.Add("归还");
            comboBoxOptTyp.Items.Add("送修");
            comboBoxOptTyp.Items.Add("修返");
            comboBoxOptTyp.Items.Add("外出");
            comboBoxOptTyp.Items.Add("返回");
            comboBoxOptTyp.Items.Add("租还");
            comboBoxOptTyp.Items.Add("退返");
            comboBoxOptTyp.Items.Add("丢失");
            comboBoxOptTyp.Items.Add("报废");
            comboBoxOptTyp.Items.Add("转出");
            comboBoxOptTyp.Items.Add("新增");
            comboBoxOptTyp.Items.Add("修改");
            comboBoxOptTyp.Items.Add("删除");
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
        private void buttonQry_Click(object sender, EventArgs e)
        {
            string sSql = sSQLSelect + " where 1=1";
            if (checkBoxDate.Checked)
            { 
                string sStartDate = string.Format("{0}-{1:00}-{2:00}",dateTimePicker1.Value.Year,dateTimePicker1.Value.Month,dateTimePicker1.Value.Day);
                string sEndDate = string.Format("{0}-{1:00}-{2:00}",dateTimePicker2.Value.Year,dateTimePicker2.Value.Month,dateTimePicker2.Value.Day);
                sSql += string.Format(" and opt_date between '{0}' and '{1}'", sStartDate, sEndDate);
            }
            if (textBoxAssId.Text.Length != 0)
            {
                sSql += string.Format(" and ass_id = '{0}'",textBoxAssId.Text);
            }
            if (textBoxPid.Text.Length != 0)
            {
                sSql += string.Format(" and ass_id = (select ass_id from ass_list where pid = '{0}' limit 0,1)", textBoxPid.Text);
            }
            if (comboBoxOptTyp.Text.Length != 0)
            {
                sSql += string.Format(" and opt_typ = '{0}'", comboBoxOptTyp.Text);
            }
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bindingSource1.DataSource = dt;
            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void textBoxPid_TextChanged(object sender, EventArgs e)
        {
            //if (textBoxPid.Text.Length == 12)
            //{
            //    string sSql = sSQLSelect + " where ass_id = (select ass_id from ass_list where pid = '" + textBoxPid.Text + "' limit 0,1)";
            //    DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            //    bindingSource1.DataSource = dt;
            //    bindingNavigator1.BindingSource = bindingSource1;
            //    dataGridView1.DataSource = bindingSource1;
            //    //textBoxPid.Text = "";
            //}
        }

        private void checkBoxDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBoxDate.Checked;
            dateTimePicker2.Enabled = checkBoxDate.Checked;
        }

    }
}