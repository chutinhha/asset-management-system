using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AssMngSysCe
{
    public partial class SelectAssForm : Form
    {
        public string sPid = "";
        public string sYnWrite = "";
        public SelectAssForm()
        {
            InitializeComponent();
            checkBoxNotWrite.Checked = true;
        }

        private void SelectAss_Load(object sender, EventArgs e)
        {
            string sYnWrite = checkBoxNotWrite.Checked == true ? " and ynwrite = 'N'" : "";
            string sSql = "select pid 喷码, ass_id 资产编码, ass_id 资产名称, reg_date 登记日期,ynwrite 是否已发 from ass_list where stat in('库存','领用') " + sYnWrite;
            DataSet ds = new DataSet();
            ds = SQLiteHelper.ExecuteQuery(sSql);
            dataGrid1.DataSource = ds.Tables[0];
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            int nIndex = dataGrid1.CurrentRowIndex;
            if (dataGrid1.CurrentRowIndex > -1 && dataGrid1.CurrentRowIndex < ((DataTable)(dataGrid1.DataSource)).Rows.Count)
            {
                DataTable dt = (DataTable)(dataGrid1.DataSource);
                DataRow dr = dt.Rows[nIndex];
                sPid = dr["喷码"].ToString();
                sYnWrite = dr["是否已发"].ToString();
            }
            this.Close();

        }

        private void checkBoxNotWrite_CheckStateChanged(object sender, EventArgs e)
        {
            string sYnWrite = checkBoxNotWrite.Checked == true ? " and ynwrite = 'N'" : "";
            string sSql = "select pid 喷码, ass_id 资产编码, ass_id 资产名称, reg_date 登记日期,ynwrite 是否已发 from ass_list where stat in('库存','领用') " + sYnWrite;
            DataSet ds = new DataSet();
            ds = SQLiteHelper.ExecuteQuery(sSql);
            dataGrid1.DataSource = ds.Tables[0];

        }

        private void dataGrid1_GotFocus(object sender, EventArgs e)
        {
            DataGrid dataGrid1 = sender as DataGrid;
            int index = ((DataGrid)sender).CurrentCell.RowNumber;
            if (((DataTable)(dataGrid1.DataSource)).Rows.Count > 0)
            {
                ((DataGrid)sender).Select(index);
            }
        }
        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGrid dataGrid1 = sender as DataGrid;
            int index = ((DataGrid)sender).CurrentCell.RowNumber;
            if (((DataTable)(dataGrid1.DataSource)).Rows.Count > 0)
            {
                ((DataGrid)sender).Select(index);
            }

        }

        private void dataGrid1_DoubleClick(object sender, EventArgs e)
        {
            buttonOK_Click(null,null);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelectAssForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(null, null);
            }
            else if (e.KeyCode == Keys.F3)
            {
                buttonCancel_Click(null, null);
            }
        } 
    }
}