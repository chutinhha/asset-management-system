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
        public string sOptType = "����";//������ɾ�����޸�
        public string sId  = "";
        public NewCatDlg(DataGridView dv)
        {
            InitializeComponent();
            dataGridView1 = dv;
        }

        private void NewCatDlg_Load(object sender, EventArgs e)
        {

            if (sOptType == "�޸�" || sOptType == "����")
            {
                textBoxCatNo.Text = dataGridView1.SelectedRows[0].Cells["������"].Value.ToString();
                comboBoxTyp.Text = dataGridView1.SelectedRows[0].Cells["�ʲ����"].Value.ToString();
                comboBoxAssNam.Text = dataGridView1.SelectedRows[0].Cells["�ʲ�����"].Value.ToString();
                sId = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
            }

            //��ȡ����б�
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
            if (sOptType == "����" || sOptType == "����")
            {
                insertData();
            }
            else if (sOptType == "�޸�")
            {
                modifyData();
            }
        }
        private void modifyData()
        {
            if (comboBoxTyp.Text.Equals(""))
            {
                MessageBox.Show("�ʲ������Ϊ��!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxAssNam.Text.Equals(""))
            {
                MessageBox.Show("�ʲ����Ʋ���Ϊ��!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sSql = string.Format("select 'X' from ass_cat where cat_nam = '{0}' and prd_nam = '{1}' and id != '{2}'", comboBoxTyp.Text, comboBoxAssNam.Text,sId);
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            if (reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("�ʲ������Ѵ���!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            reader.Close();

            string sSqlIns = string.Format("update ass_cat set cat_no = '{0}', cat_nam = '{1}', prd_nam = '{2}' where id = '{3}'", textBoxCatNo.Text, comboBoxTyp.Text, comboBoxAssNam.Text,sId);


            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
    "����޸�", "0", sSqlIns.Replace("'", "\\'"), Login.sClientId, "", MainForm.getDateTime());

            List<string> listSql = new List<string>();
            listSql.Add(sSqlIns);
            listSql.Add(sSqlInsLog);
            bool bOK = false;
            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("�޸ĳɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bDone = true;
                this.Close();

            }
            else
            {
                MessageBox.Show("�޸�ʧ�ܣ�\r\n" + MysqlHelper.sLastErr, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void insertData()
        {
            if (comboBoxTyp.Text.Equals(""))
            {
                MessageBox.Show("�ʲ������Ϊ��!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxAssNam.Text.Equals(""))
            {
                MessageBox.Show("�ʲ����Ʋ���Ϊ��!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sSql = string.Format("select 'X' from ass_cat where cat_nam = '{0}' and prd_nam = '{1}'", comboBoxTyp.Text, comboBoxAssNam.Text);
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            if (reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("�ʲ������Ѵ���!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            reader.Close();

            string sSqlIns = string.Format("insert into ass_cat(cat_no,cat_nam,prd_nam)values('{0}','{1}','{2}')", textBoxCatNo.Text,comboBoxTyp.Text, comboBoxAssNam.Text);

            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
    "�������", "0", sSqlIns.Replace("'", "\\'"), Login.sClientId, "", MainForm.getDateTime());

            List<string> listSql = new List<string>();
            listSql.Add(sSqlIns);
            listSql.Add(sSqlInsLog);
            bool bOK = false;
            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("�����ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bDone = true;
                this.Close();

            }
            else
            {
                MessageBox.Show("����ʧ�ܣ�\r\n" + MysqlHelper.sLastErr, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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