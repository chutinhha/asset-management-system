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
    public partial class InvListQry : Form
    {
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
            string sSql = string.Format("select inv_no �嵥��,pid ��ǩ����,ass_id �ʲ�����,ass_nam �ʲ�����,stat ���״̬,stat_sub ʹ��״̬,result �̵���,memo ��ע,duty_man ������Ա,vender Ʒ��, ass_desc �ʲ�����,addr ���ڵص� ,dept ���ò��� from inv_list where 1=1 ");
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
"ɾ���嵥", "0", sSql.Replace("'", "''"), MainForm.sClientId, comboBoxInvNo.Text, MainForm.getDateTime());
            listSql.Add(sSql);
            listSql.Add(sSqlLog);

            bool bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("ɾ���ɹ���\r\n�嵥�ţ�" + comboBoxInvNo.Text, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBoxInvNo.Items.Remove(comboBoxInvNo.Text);
                dataGridView2.DataSource = null;
            }
            else
            {
                MessageBox.Show("ɾ��ʧ�ܣ�\r\n" + MysqlHelper.sLastErr, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

    }
}