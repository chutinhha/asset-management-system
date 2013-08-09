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
    public partial class NewAddrDlg : Form
    {
        public DataGridView dataGridView1 = null;
        public bool bDone = false;
        public string sOptType = "����";//������ɾ�����޸�
        public string sId  = "";
        public NewAddrDlg(DataGridView dv)
        {
            InitializeComponent();
            dataGridView1 = dv;
        }

        private void NewAddrDlg_Load(object sender, EventArgs e)
        {

            if (sOptType == "�޸�" || sOptType == "����")
            {
                textBoxAddr.Text = dataGridView1.SelectedRows[0].Cells["�ص�"].Value.ToString();
                sId = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
            }
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
            if (textBoxAddr.Text.Equals(""))
            {
                MessageBox.Show("����Ϊ��!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sSql = string.Format("select 'X' from addr where addr_no = '{0}' and id != '{1}'", textBoxAddr.Text, sId);
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            if (reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("�ص��Ѵ���!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            reader.Close();

            string sSqlIns = string.Format("update addr set addr_no = '{0}' where id = '{1}'", textBoxAddr.Text,sId);


            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
    "�ص��޸�", "0", sSqlIns.Replace("'", "\\'"), Login.sClientId, "", MainForm.getDateTime());

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
            if (textBoxAddr.Text.Equals(""))
            {
                MessageBox.Show("����Ϊ��!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sSql = string.Format("select 'X' from addr where addr_no = '{0}'", textBoxAddr.Text);
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            if (reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("�ص��Ѵ���!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            reader.Close();


            string sSqlIns = string.Format("insert into addr(addr_no)values('{0}')", textBoxAddr.Text);


            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
    "�����ص�", "0", sSqlIns.Replace("'", "\\'"), Login.sClientId, "", MainForm.getDateTime());

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
            //string str = comboBoxTyp.SelectedItem.ToString();
            //comboBoxAssNam.Items.Clear();
            //string sSql = "select distinct prd_nam from ass_cat where cat_nam = '" + str + "'order by convert(prd_nam using gb2312) asc";
            //MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            //while (reader.Read())
            //{
            //    comboBoxAssNam.Items.Add(reader["prd_nam"].ToString());
            //}
            //reader.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }   
    }
}