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
    public partial class NewCatDlg : Form
    {
        public DataGridView dataGridView1 = null;
        public bool bDone = false;
        public string sOptType = "新增";//新增，删除，修改
        public string sId  = "";
        public NewCatDlg(DataGridView dv)
        {
            InitializeComponent();
            dataGridView1 = dv;
        }

        private void NewCatDlg_Load(object sender, EventArgs e)
        {

            if (sOptType == "修改" || sOptType == "复制")
            {
                textBoxCatNo.Text = dataGridView1.SelectedRows[0].Cells["类别编码"].Value.ToString();
                comboBoxTyp.Text = dataGridView1.SelectedRows[0].Cells["资产类别"].Value.ToString();
                comboBoxAssNam.Text = dataGridView1.SelectedRows[0].Cells["资产名称"].Value.ToString();
                sId = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
            }

            //获取类别列表
            string sSql = "select distinct cat_nam from ass_cat order by convert(cat_nam using gb2312) asc";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxTyp.Items.Add(reader["cat_nam"].ToString());
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
            if (comboBoxTyp.Text.Equals(""))
            {
                MessageBox.Show("资产类别不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxAssNam.Text.Equals(""))
            {
                MessageBox.Show("资产名称不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sSql = string.Format("select 'X' from ass_cat where cat_nam = '{0}' and prd_nam = '{1}' and id != '{2}'", comboBoxTyp.Text, comboBoxAssNam.Text,sId);
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            if (reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("资产名称已存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            reader.Close();

            string sSqlIns = string.Format("update ass_cat set cat_no = '{0}', cat_nam = '{1}', prd_nam = '{2}' where id = '{3}'", textBoxCatNo.Text, comboBoxTyp.Text, comboBoxAssNam.Text,sId);


            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
    "类别修改", "0", sSqlIns.Replace("'", "\\'"), Login.sClientId, "", MainForm.getDateTime());

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
            if (comboBoxTyp.Text.Equals(""))
            {
                MessageBox.Show("资产类别不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxAssNam.Text.Equals(""))
            {
                MessageBox.Show("资产名称不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sSql = string.Format("select 'X' from ass_cat where cat_nam = '{0}' and prd_nam = '{1}'", comboBoxTyp.Text, comboBoxAssNam.Text);
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            if (reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("资产名称已存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            reader.Close();

            string sSqlIns = string.Format("insert into ass_cat(cat_no,cat_nam,prd_nam)values('{0}','{1}','{2}')", textBoxCatNo.Text,comboBoxTyp.Text, comboBoxAssNam.Text);

            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
    "类别新增", "0", sSqlIns.Replace("'", "\\'"), Login.sClientId, "", MainForm.getDateTime());

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
            string str = comboBoxTyp.SelectedItem.ToString();
            comboBoxAssNam.Items.Clear();
            string sSql = "select distinct prd_nam from ass_cat where cat_nam = '" + str + "'order by convert(prd_nam using gb2312) asc";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxAssNam.Items.Add(reader["prd_nam"].ToString());
            }
            reader.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }   
    }
}