using System;
using System.Xml;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AssMngSys
{
    public partial class AssInput : Form
    {

        public TextBox textBoxLog;

        private BindingSource bs = new BindingSource();


        string sSQLSelect;


        MainForm mf;
        public AssInput(MainForm f)
        {
            InitializeComponent();
            //myConn = f.myConn;
            mf = f;
        }

        private void AssInputBak_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.ass_list' table. You can move, or remove it, as needed.
            this.ShowInTaskbar = false;


            sSQLSelect = "select Id ID,ass_id �ʲ����,fin_id �������,pid ��ǩ����,tid ��ǩID,cat_no ������,typ ����,ass_nam �ʲ�����,ass_desc �ʲ�����,ass_pri �ʲ����,reg_date �Ǽ�����,dept ��������,addr ���ڵص�,use_co ���ڹ�˾,stat ���״̬,stat_sub ʹ��״̬,supplier ��Ӧ��,supplier_info ��Ӧ����Ϣ,sn ���к�,vender ����Ʒ��,mfr_date ��������,unit ��λ,num ����,ppu ����,duty_man ������Ա,company �ʲ�����,memo ��ע,cre_man ������Ա,cre_tm ����ʱ��,mod_man �޸���Ա,mod_tm �޸�ʱ��,input_typ �������� from ass_list where ynenable = 'Y' ";

            string sSql = sSQLSelect;
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bs.DataSource = dt;
            bindingNavigator1.BindingSource = bs;
            dataGridView1.DataSource = bs;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
    

        }
       
        private void AssInputBak_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        // ����ί�лص�����ʵ�ֽ�������Ϣ������ʾ
        delegate void AddLogCallBack(TextBox textBox, string str);
        private void AddLog(TextBox textBox, string str)
        {
            if (textBox.InvokeRequired)
            {
                AddLogCallBack addLogCallBack = AddLog;
                textBox.Invoke(addLogCallBack, new object[] { textBox, str });
            }
            else
            {
                str = DateTime.Now.ToString("hh:mm") + " " + str + @"
";
                textBoxLog.AppendText(str);
                textBoxLog.ScrollToCaret();
            }
        }



        private void AssInputBak_Resize(object sender, EventArgs e)
        {
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridViewTextBoxColumn dgv_Text = new DataGridViewTextBoxColumn();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int j = i + 1;
                dataGridView1.Rows[i].HeaderCell.Value = j.ToString();
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            NewAssDlg dlg = new NewAssDlg(mf,dataGridView1);
            dlg.sOptType = "����";
            dlg.ShowDialog();
            if (dlg.bDone)
            {
                resetData();
                bs.MoveLast();
            }
        }
        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            NewAssDlg dlg = new NewAssDlg(mf, dataGridView1);
            dlg.sOptType = "����";
            dlg.ShowDialog();
            if (dlg.bDone)
            {
                resetData();
                bs.MoveLast();
            }
        }
        private void toolStripButtonMod_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                NewAssDlg dlg = new NewAssDlg(mf,dataGridView1);
                dlg.sOptType = "�޸�";
                dlg.ShowDialog();
                if (dlg.bDone)
                {
                    resetData();
                }
            }
            else
            {
                MessageBox.Show("����ѡ��һ�����ϣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void toolStripButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                if (MessageBox.Show(string.Format("��ȷ��Ҫɾ��ѡ���ʲ���\r\n�ʲ�����{0}", dataGridView1.SelectedRows.Count), 
                    "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    List<string> listSql = new List<string>();
                    string sSqlUpd = "update ass_list set ynenable = 'N' where id = '";
                    string sSql = "";
                    for(int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        string sId = dataGridView1.SelectedRows[i].Cells["ID"].Value.ToString();
                        string sAssId = dataGridView1.SelectedRows[i].Cells["�ʲ����"].Value.ToString();
                        sSql = sSqlUpd + sId + "'";
                        listSql.Add(sSql);
                        string sSqlLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
                        "ɾ��", "0", sSql.Replace("'", "''"), MainForm.sClientId, sAssId, MainForm.getDateTime());
                        listSql.Add(sSqlLog);
                    }

                    bool bOK = false;
                    bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
                    if (bOK)
                    {
                        resetData();
                        MessageBox.Show("ɾ���ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ɾ��ʧ�ܣ�\r\n" + MysqlHelper.sLastErr, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                }
            }
            else
            {
                MessageBox.Show("����ѡ�����ϣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void toolStripButtonQry_Click(object sender, EventArgs e)
        {

        }
        private void resetData()
        {
            //��ȡ�б�
            DataTable dt = MysqlHelper.ExecuteDataTable(sSQLSelect);
            bs.DataSource = dt;
        }
    }
}