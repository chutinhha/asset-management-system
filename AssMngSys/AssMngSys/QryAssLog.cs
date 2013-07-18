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
        MainWnd mf;
        string sSQLSelect = "select Id 记录好,ass_id 资产编码,opt_typ 操作类型,opt_man 确认人员,opt_date 操作日期,cre_man 操作人员,cre_tm 操作时间,company 所属公司,dept 所属部门,addr 地址,reason 事由 from ass_log ";
        public QryAssLog(MainWnd f)
        {
            InitializeComponent(); 
            f.recvEvent += new MainWnd.RecvEventHandler(this.RecvDataEvent);
            mf = f;
        }
        
        private void AssLogForm_Load(object sender, EventArgs e)
        {
            buttonQry_Click(null,null);
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
            string sStartDate = string.Format("{0}{1:00}{2:00}",dateTimePicker1.Value.Year,dateTimePicker1.Value.Month,dateTimePicker1.Value.Day);
            string sEndDate = string.Format("{0}{1:00}{2:00}",dateTimePicker2.Value.Year,dateTimePicker2.Value.Month,dateTimePicker2.Value.Day);
            string sSql = sSQLSelect + string.Format(" where opt_date between '{0}' and '{1}'", sStartDate, sEndDate);
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bindingSource1.DataSource = dt;
            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void textBoxPid_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPid.Text.Length == 12)
            {
                string sSql = sSQLSelect + " where ass_id = (select ass_id from ass_list where pid = '" + textBoxPid.Text + "' limit 0,1)";
                DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
                bindingSource1.DataSource = dt;
                bindingNavigator1.BindingSource = bindingSource1;
                dataGridView1.DataSource = bindingSource1;
                //textBoxPid.Text = "";
            }
        }

    }
}