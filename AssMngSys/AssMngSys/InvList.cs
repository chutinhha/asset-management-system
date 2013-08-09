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
        QryAssDlg qryassdlg;
        public InvList(MainForm f)
        {
            InitializeComponent();
            mf = f;
            qryassdlg = new QryAssDlg(mf);
        }
        private void InvList_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            toolStripTextBoxInvId.Text = DateTime.Now.ToString("yyyyMMdd#01");
            toolStripButtonQry_Click(null,null);
        }
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            string sSql = "select 'X' from inv_list where inv_no = '" + toolStripTextBoxInvId.Text + "' limit 0,1";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("无数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (reader.HasRows)
            {
                MessageBox.Show("该清单号已存在！\r\n" + toolStripTextBoxInvId.Text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<string> listSql = new List<string>();
            List<string> listSqlLog = new List<string>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string sAssId = dataGridView1.Rows[i].Cells["资产编码"].Value.ToString();
                string sId = toolStripTextBoxInvId.Text.Replace("#", "") + i.ToString();
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
                    toolStripTextBoxInvId.Text,
                    Login.sUserName,
                    MainForm.getDateTime()
                    );
                string sSqlLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
"盘点清单", "0", sSql.Replace("'", "''"), Login.sClientId, sAssId, MainForm.getDateTime());
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
                MessageBox.Show("保存成功！\r\n清单号：" + toolStripTextBoxInvId.Text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = null;
            }
            else
            {
                MessageBox.Show("保存失败！\r\n" + MysqlHelper.sLastErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void toolStripButtonQry_Click(object sender, EventArgs e)
        {
            //QryAssDlg qryassdlg = new QryAssDlg(mf);
            if (qryassdlg.ShowDialog() == DialogResult.OK)
            {
                string sSql = "select pid 标签喷码,ass_id 资产编码,ass_nam 资产名称,stat 库存状态, stat_sub 使用状态,duty_man 保管人员,vender 品牌, ass_desc 备注,addr 所在地点 ,dept 部门 from ass_list where ynenable = 'Y' and stat in('库存','领用') ";
                DataTable dt = MysqlHelper.ExecuteDataTable(sSql + qryassdlg.sSqlCondition);
                bindingSource1.DataSource = dt;
                bindingNavigator1.BindingSource = bindingSource1;
                dataGridView1.DataSource = bindingSource1;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
        }
    }
}