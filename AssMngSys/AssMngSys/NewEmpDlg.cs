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
    public partial class NewEmpDlg : Form
    {
        public DataGridView dataGridView1 = null;
        public bool bDone = false;
        public string sOptType = "新增";//新增，删除，修改
        public string sId  = "";
        public NewEmpDlg(DataGridView dv)
        {
            InitializeComponent();
            dataGridView1 = dv;
        }

        private void NewEmpDlg_Load(object sender, EventArgs e)
        {

            if (sOptType == "修改" || sOptType == "复制")
            {
                textBoxEmpNo.Text = dataGridView1.SelectedRows[0].Cells["工号"].Value.ToString();
                comboEmpNam.Text = dataGridView1.SelectedRows[0].Cells["姓名"].Value.ToString();
                comboDept.Text = dataGridView1.SelectedRows[0].Cells["部门名称"].Value.ToString();
                sId = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
            }

            //获取类别列表
            string sSql = "select distinct dept_nam from emp order by convert(dept_nam using gb2312) asc";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboDept.Items.Add(reader["dept_nam"].ToString());
            }
            reader.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (sOptType == "新增" || sOptType == "复制")
            {
                insertData();
            }
            else if (sOptType == "修改")
            {
                modifyData();
            }
        }
        private void modifyData()
        {
            if (comboEmpNam.Text.Equals(""))
            {
                MessageBox.Show("姓名不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboDept.Text.Equals(""))
            {
                MessageBox.Show("部门名称不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sSql = string.Format("select 'X' from emp where emp_nam = '{0}' and dept_nam = '{1}' and id != '{2}'", comboEmpNam.Text, comboDept.Text,sId);
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            if (reader.HasRows)
            {
                reader.Close();
                MessageBox.Show(this,"该资料（姓名-部门）已存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            reader.Close();

            string sSqlIns = string.Format("update emp set emp_no = '{0}', emp_nam = '{1}', dept_nam = '{2}' where id = '{3}'", textBoxEmpNo.Text, comboEmpNam.Text, comboDept.Text,sId);


            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
    "人员修改", "0", sSqlIns.Replace("'", "\\'"), Login.sClientId, "", MainForm.getDateTime());

            List<string> listSql = new List<string>();
            listSql.Add(sSqlIns);
            listSql.Add(sSqlInsLog);
            bool bOK = false;
            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bDone = true;
                this.Close();

            } 
            else
            {
                MessageBox.Show("修改失败！\r\n" + MysqlHelper.sLastErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void insertData()
        {
            if (comboEmpNam.Text.Equals(""))
            {
                MessageBox.Show(this,"姓名不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboDept.Text.Equals(""))
            {
                MessageBox.Show("部门名称不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sSql = string.Format("select 'X' from emp where emp_nam = '{0}' and dept_nam = '{1}'", comboEmpNam.Text, comboDept.Text);
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            if (reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("该资料（姓名-部门）已存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            reader.Close();


            string sSqlIns = string.Format("insert into emp(emp_no,emp_nam,dept_nam)values('{0}','{1}','{2}')", textBoxEmpNo.Text,comboEmpNam.Text, comboDept.Text);


            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
    "人员新增", "0", sSqlIns.Replace("'", "\\'"), Login.sClientId, "", MainForm.getDateTime());

            List<string> listSql = new List<string>();
            listSql.Add(sSqlIns);
            listSql.Add(sSqlInsLog);
            bool bOK = false;
            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("新增成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bDone = true;
                this.Close();

            }
            else
            {
                MessageBox.Show("新增失败！\r\n" + MysqlHelper.sLastErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void comboBoxCatName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //string str = comboEmpNam.SelectedItem.ToString();
            //comboBoxDept.Items.Clear();
            //string sSql = "select distinct prd_nam from ass_cat where cat_nam = '" + str + "'order by convert(prd_nam using gb2312) asc";
            //MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            //while (reader.Read())
            //{
            //    comboBoxDept.Items.Add(reader["prd_nam"].ToString());
            //}
            //reader.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }   
    }
}