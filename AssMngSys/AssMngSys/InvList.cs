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
    public partial class InvList : Form
    {
        MainForm mf;
        public InvList(MainForm f)
        {
            InitializeComponent();
            mf = f;
        }
        private void InvList_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;

            textBoxInvId.Text = DateTime.Now.ToString("yyyyMMdd#01");
            //获取部门列表
            string sSql = "select distinct dept_nam from emp";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxDept.Items.Add(reader["dept_nam"].ToString());
            }
            comboBoxDept.Items.Add("");
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBox1.Checked;
            dateTimePicker2.Enabled = checkBox1.Checked;
        }

        private void buttonQry_Click(object sender, EventArgs e)
        {
            string sStartDate = string.Format("{0}-{1:00}-{2:00}", dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
            string sEndDate = string.Format("{0}-{1:00}-{2:00}", dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day);
            string sSql = "select pid 标签喷码,ass_id 资产编码,ass_nam 资产名称,stat 库存状态, stat_sub 使用状态,duty_man 保管人员,vender 品牌, ass_desc 资产描述,addr 所在地点 ,dept 部门 from ass_list where 1=1 ";

            if (checkBox1.Checked)
            {
                sSql += string.Format(" and reg_date between '{0}' and '{1}'", sStartDate, sEndDate);
            }
            if (comboBoxDept.Text.Length != 0)
            {
                sSql += string.Format(" and use_dept = '{0}'", comboBoxDept.Text);
            }
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bindingSource1.DataSource = dt;
            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void buttonQryHistory_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("select inv_no 清单号,pid 标签喷码,ass_id 资产编码,ass_nam 资产名称,stat 库存状态,stat_sub 使用状态,result 盘点结果,memo 备注,duty_man 保管人员,vender 品牌, ass_desc 资产描述,addr 所在地点 ,dept 部门 from inv_list where 1=1 ");
            sSql += string.Format(" and inv_no = '{0}'", comboBoxInvNo.Text);
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bindingSource2.DataSource = dt;
            bindingNavigator2.BindingSource = bindingSource2;
            dataGridView2.DataSource = bindingSource2;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string sSql = "select 'X' from inv_list where inv_no = '" + textBoxInvId.Text + "' limit 0,1";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("无数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (reader.HasRows)
            {
                MessageBox.Show("该清单号已存在！\r\n" + textBoxInvId.Text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<string> listSql = new List<string>();
            List<string> listSqlLog = new List<string>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string sAssId = dataGridView1.Rows[i].Cells["资产编码"].Value.ToString();
                string sId = textBoxInvId.Text.Replace("#", "") + i.ToString();
                sSql = string.Format
                    (@"insert into inv_list(id,pid,ass_id,ass_nam,stat,stat_sub,duty_man,vender,ass_desc,addr,dept,inv_no,cre_man,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')",
                    sId,
                    dataGridView1.Rows[i].Cells[0].Value.ToString(),
                    dataGridView1.Rows[i].Cells[1].Value.ToString(),
                    dataGridView1.Rows[i].Cells[2].Value.ToString(),
                    dataGridView1.Rows[i].Cells[3].Value.ToString(),
                    dataGridView1.Rows[i].Cells[4].Value.ToString(),
                    dataGridView1.Rows[i].Cells[5].Value.ToString(),
                    dataGridView1.Rows[i].Cells[6].Value.ToString(),
                    dataGridView1.Rows[i].Cells[7].Value.ToString(),
                    dataGridView1.Rows[i].Cells[8].Value.ToString(),
                    dataGridView1.Rows[i].Cells[9].Value.ToString(),
                    textBoxInvId.Text,
                    MainForm.sUserName,
                    MainForm.getDateTime()
                    );
                string sSqlLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
"盘点清单", "0", sSql.Replace("'", "''"), MainForm.sClientId, sAssId, MainForm.getDateTime());
                listSql.Add(sSql);
                listSqlLog.Add(sSqlLog);
            }
            bool bOK = false;
            foreach (string sTmp in listSqlLog)
            {
                listSql.Add(sTmp);
            }
            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("保存成功！\r\n清单号：" + textBoxInvId.Text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = null;
            }
            else
            {
                MessageBox.Show("保存失败！\r\n" + MysqlHelper.sLastErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                comboBoxInvNo.Items.Clear();
                string sSql = "select distinct inv_no from inv_list";
                MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
                while (reader.Read())
                {
                    comboBoxInvNo.Items.Add(reader["inv_no"].ToString());
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            List<string> listSql = new List<string>();
            string sSql = string.Format(" delete from inv_list where inv_no = '{0}'", comboBoxInvNo.Text);
            string sSqlLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
"删除清单", "0", sSql.Replace("'", "''"), MainForm.sClientId, comboBoxInvNo.Text, MainForm.getDateTime());
            listSql.Add(sSql);
            listSql.Add(sSqlLog);

            bool bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("删除成功！\r\n清单号：" + comboBoxInvNo.Text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBoxInvNo.Items.Remove(comboBoxInvNo.Text);
                dataGridView2.DataSource = null;
            }
            else
            {
                MessageBox.Show("删除失败！\r\n" + MysqlHelper.sLastErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

    }
}