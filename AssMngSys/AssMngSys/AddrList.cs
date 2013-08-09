using System;
using System.Xml;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AssMngSys
{
    public partial class AddrList : Form
    {

        public TextBox textBoxLog;

        private BindingSource bs = new BindingSource();


        string sSQLSelect;

        MainForm mf;
        public AddrList(MainForm f)
        {
            InitializeComponent();
            //myConn = f.myConn;
            mf = f;
        }

        private void AddrList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.ass_list' table. You can move, or remove it, as needed.
            this.ShowInTaskbar = false;

            sSQLSelect = "select Id ID,addr_no 地点 from addr order by convert(addr_no using gb2312) asc";

            string sSql = sSQLSelect;
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bs.DataSource = dt;
            bindingNavigator1.BindingSource = bs;
            dataGridView1.DataSource = bs;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void AddrList_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridViewTextBoxColumn dgv_Text = new DataGridViewTextBoxColumn();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //行号
                int j = i + 1;
                dataGridView1.Rows[i].HeaderCell.Value = j.ToString();
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            NewAddrDlg dlg = new NewAddrDlg(dataGridView1);
            dlg.sOptType = "新增";
            dlg.ShowDialog();
            if (dlg.bDone)
            {
                resetData();
                bs.MoveLast();
            }
        }
        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                NewAddrDlg dlg = new NewAddrDlg(dataGridView1);
                dlg.sOptType = "复制";
                dlg.ShowDialog();
                if (dlg.bDone)
                {
                    resetData();
                    bs.MoveLast();
                }
            }
        }
        private void toolStripButtonMod_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                NewAddrDlg dlg = new NewAddrDlg(dataGridView1);
                dlg.sOptType = "修改";
                dlg.ShowDialog();
                if (dlg.bDone)
                {
                    resetData();
                }
            }
            else
            {
                MessageBox.Show("请先选择一笔资料！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void toolStripButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                if (MessageBox.Show(string.Format("您确定要删除选中资料吗？\r\n资料数：{0}", dataGridView1.SelectedRows.Count),
                    "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    List<string> listSql = new List<string>();
                    string sSqlUpd = "delete from addr where id = '";  
                    string sSql = "";
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        string sId = dataGridView1.SelectedRows[i].Cells["ID"].Value.ToString();
                        sSql = sSqlUpd + sId + "'";
                        listSql.Add(sSql);
                        string sSqlLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
                        "删除", "0", sSql.Replace("'", "''"), Login.sClientId, "", MainForm.getDateTime());
                        listSql.Add(sSqlLog);
                    }

                    bool bOK = false;
                    bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
                    if (bOK)
                    {
                        resetData();
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("删除失败！\r\n" + MysqlHelper.sLastErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                }
            }
            else
            {
                MessageBox.Show("请先选择资料！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void resetData()
        {
            //获取列表
            DataTable dt = MysqlHelper.ExecuteDataTable(sSQLSelect);
            bs.DataSource = dt;
        }
    }
}