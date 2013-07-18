using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
namespace IrRfidUHFDemo
{
    public partial class HistoryForm : Form
    {
        public string sAssid = "";
        public HistoryForm(string sAssid)
        {
            InitializeComponent();
            this.sAssid = sAssid;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            dataGrid1.DataSource = null;
            this.Close();
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            string sSql = "select cre_tm 时间, opt_typ 动作, opt_man 人员,dept 部门, reason 事由 from ass_log where ass_id = \'" + sAssid + "\'";
            DataSet ds = new DataSet();
            ds = SQLiteHelper.ExecuteQuery(sSql);
            dataGrid1.DataSource = ds.Tables[0];
        }

        private void HistoryForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                buttonCancel_Click(null, null);
            }
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {

        }
    }
}