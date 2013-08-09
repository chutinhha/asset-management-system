using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AssMngSys
{
    public partial class QryAssList : Form
    {
        MainForm mf;
        QryAssDlg qryassdlg;
        string sSQLSelect;
        public QryAssList(MainForm f)
        {
            InitializeComponent(); 
            f.recvEvent += new MainForm.RecvEventHandler(this.RecvDataEvent);
            mf = f;
            qryassdlg = new QryAssDlg(mf);
            sSQLSelect = AssInput.sSQLSelect;
        }
        
        private void AssLogForm_Load(object sender, EventArgs e)
        {
            string sSql = sSQLSelect + " and 1=1 ";
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bindingSource1.DataSource = dt;
            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
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
        //private void buttonQry_Click(object sender, EventArgs e)
        //{
        //    //string sStartDate = string.Format("{0}-{1:00}-{2:00}",dateTimePicker1.Value.Year,dateTimePicker1.Value.Month,dateTimePicker1.Value.Day);
        //    //string sEndDate = string.Format("{0}-{1:00}-{2:00}",dateTimePicker2.Value.Year,dateTimePicker2.Value.Month,dateTimePicker2.Value.Day);
        //    //string sSql = sSQLSelect + string.Format(" where reg_date between '{0}' and '{1}'", sStartDate, sEndDate);

        //    string sStartDate = string.Format("{0}-{1:00}-{2:00} 00:00:00", dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
        //    string sEndDate = string.Format("{0}-{1:00}-{2:00} 23:59:59", dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day);
        //    string sSql = sSQLSelect + string.Format(" where cre_tm between '{0}' and '{1}'", sStartDate, sEndDate);

        //    DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
        //    bindingSource1.DataSource = dt;
        //    bindingNavigator1.BindingSource = bindingSource1;
        //    dataGridView1.DataSource = bindingSource1;
        //    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        //}

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

        private void toolStripButtonQry_Click(object sender, EventArgs e)
        {
            if (qryassdlg.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = MysqlHelper.ExecuteDataTable(sSQLSelect + qryassdlg.sSqlCondition);
                bindingSource1.DataSource = dt;
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridViewTextBoxColumn dgv_Text = new DataGridViewTextBoxColumn();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //行号
                int j = i + 1;
                dataGridView1.Rows[i].HeaderCell.Value = j.ToString();
                //颜色
                string sStat = dataGridView1.Rows[i].Cells["库存状态"].Value.ToString();
                if (sStat != "库存" && sStat != "领用")
                {
                    try
                    {
                        this.dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(0xFF0000);
                    }
                    catch (Exception ex)
                    {
                        // new FileOper().writelog(ex.Message);
                        System.Diagnostics.Trace.WriteLine(ex.Message);
                    }
                }
            }
        }

    }
}