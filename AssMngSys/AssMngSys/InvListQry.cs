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

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            toolStripComboBoxInvNo.Items.Clear();
            string sSql = "select distinct inv_no from inv_list order by inv_no desc";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                toolStripComboBoxInvNo.Items.Add(reader["inv_no"].ToString());
            }
            reader.Close();

            if (toolStripComboBoxInvNo.Items.Count > 0)
            {
                toolStripComboBoxInvNo.SelectedIndex = 0;
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
            printDocument1.DocumentName = "����";
            title = printDocument1.DocumentName;  //����
            printDocument1.PrinterSettings = MyPrintDialog.PrinterSettings;
            printDocument1.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            printDocument1.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);

            dgvPrinter = new DataGridViewPrinter(dataGridView1, printDocument1, true, true, title, new Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
            return true;
        }

        void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            bool more = dgvPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }

        private void toolStripButtonDel_Click(object sender, EventArgs e)
        {
            List<string> listSql = new List<string>();
            string sSql = string.Format(" delete from inv_list where inv_no = '{0}'", toolStripComboBoxInvNo.Text);
            string sSqlLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
"ɾ���嵥", "0", sSql.Replace("'", "''"), Login.sClientId, toolStripComboBoxInvNo.Text, MainForm.getDateTime());
            listSql.Add(sSql);
            listSql.Add(sSqlLog);

            bool bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("ɾ���ɹ���\r\n�嵥�ţ�" + toolStripComboBoxInvNo.Text, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                toolStripComboBoxInvNo.Items.Remove(toolStripComboBoxInvNo.Text);
                dataGridView1.DataSource = null;
            }
            else
            {
                MessageBox.Show("ɾ��ʧ�ܣ�\r\n" + MysqlHelper.sLastErr, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void toolStripButtonQry_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("select inv_no �嵥��,pid ��ǩ����,ass_id �ʲ�����,ass_nam �ʲ�����,stat ���״̬,use_man ������Ա,stat_sub ʹ��״̬,duty_man ������Ա,result �̵���,memo ��ע,vender Ʒ��, ass_desc �ʲ�����,addr ���ڵص� ,dept ���ò��� from inv_list where 1=1 ");
            sSql += string.Format(" and inv_no = '{0}'", toolStripComboBoxInvNo.Text);
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bindingSource1.DataSource = dt;
            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            if (setPrinter())
            {
                PrintPreviewDialog printView = new PrintPreviewDialog();
                printView.Document = printDocument1;
                printView.ShowDialog();
            }
        }

        private void toolStripComboBoxInvNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripButtonQry_Click(null,null);
        }
        private void toolStripButtonExcel_Click(object sender, EventArgs e)
        {
            AssInput.ExportDataGridViewToExcel(dataGridView1);
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridViewTextBoxColumn dgv_Text = new DataGridViewTextBoxColumn();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //�к�
                int j = i + 1;
                dataGridView1.Rows[i].HeaderCell.Value = j.ToString();
                //��ɫ
                string sStat = dataGridView1.Rows[i].Cells["���״̬"].Value.ToString();
                if (sStat != "���" && sStat != "����")
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