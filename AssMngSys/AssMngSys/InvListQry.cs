using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;
using System.IO;

namespace AssMngSys
{
    public partial class InvListQry : Form
    {
        DataGridViewPrinter dgvPrinter;
        MainForm mf;
        public InvListQry(MainForm f)
        {
            InitializeComponent();
            mf = f;
        }
        private void InvListQry_Load(object sender, EventArgs e)
        {

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;

            comboBoxInvNo.Items.Clear();
            string sSql = "select distinct inv_no from inv_list";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxInvNo.Items.Add(reader["inv_no"].ToString());
            }

        }

        private void buttonQryHistory_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("select inv_no 清单号,pid 标签喷码,ass_id 资产编码,ass_nam 资产名称,stat 库存状态,stat_sub 使用状态,result 盘点结果,memo 备注,duty_man 保管人员,vender 品牌, ass_desc 备注,addr 所在地点 ,dept 领用部门 from inv_list where 1=1 ");
            sSql += string.Format(" and inv_no = '{0}'", comboBoxInvNo.Text);
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bindingSource2.DataSource = dt;
            bindingNavigator2.BindingSource = bindingSource2;
            dataGridView2.DataSource = bindingSource2;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            //if (checkNull())
            //{
            //DataTable dt = new DataTable();
            //DataRow tableDr;
            //for (int columnHeader = 0; columnHeader < dgvStock.Columns.Count; columnHeader++)
            //{
            //    dt.Columns.Add(new DataColumn(dgvStock.Columns[columnHeader].HeaderText));
            //}
            //for (int rowIndex = 0; rowIndex < dgvStock.Rows.Count-1; rowIndex++)
            //{
            //    tableDr = dt.NewRow();
            //    for (int colIndex = 0; colIndex < dgvStock.Columns.Count; colIndex++)
            //    {

            //        tableDr[colIndex] = dgvStock.Rows[rowIndex].Cells[colIndex].Value.ToString();
            //    }
            //    dt.Rows.Add(tableDr);
            //}
            //StockPrintReport stockReport = new StockPrintReport();
            //stockReport.setReportSource(dt);
            //stockReport.ShowDialog();
            //}
            if (setPrinter())
            {
                PrintPreviewDialog printView = new PrintPreviewDialog();
                printView.Document = printDocument1;
                printView.ShowDialog();
            }
        }


        private bool setPrinter()
        {
            printDocument1 = new PrintDocument();
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;
            if (MyPrintDialog.ShowDialog() != DialogResult.OK)
                return false;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            string title;
            printDocument1.DocumentName = "报表";
            title = printDocument1.DocumentName;  //标题
            printDocument1.PrinterSettings = MyPrintDialog.PrinterSettings;
            printDocument1.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            printDocument1.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);

            dgvPrinter = new DataGridViewPrinter(dataGridView2, printDocument1, true, true, title, new Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
            return true;
        }

        void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            bool more = dgvPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }


    }
}