using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AssMngSys
{
    public partial class SelectAssList : Form
    {
        //public string sPid = "";
        public string sSQLSelect = "";
        public List<string> aPidList = new List<string>();
        public SelectAssList()
        {
            InitializeComponent();
        }

        private void SelectAssList_Load(object sender, EventArgs e)
        {
            string sSql = sSQLSelect;
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bindingSource1.DataSource = dt;
            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
               string sPid = dataGridView1.CurrentRow.Cells["标签喷码"].Value.ToString();
               aPidList.Add(sPid);
               this.Close();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.CurrentRow != null)
            //{
            //    sPid = dataGridView1.CurrentRow.Cells["标签喷码"].Value.ToString();
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("SORRY，您没有选择任何资料！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            if (dataGridView1.SelectedRows.Count != 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    string sPid = dataGridView1.SelectedRows[i].Cells["标签喷码"].Value.ToString();
                    aPidList.Add(sPid);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("SORRY，您没有选择任何资料！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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