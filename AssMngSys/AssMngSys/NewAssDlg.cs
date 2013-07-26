using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Net;

namespace AssMngSys
{
    public partial class NewAssDlg : Form
    {
        public DataGridView dataGridView1 = null;
        public string sOptType = "����";//������ɾ�����޸�
        public bool bDone = false;
        MainForm mf;

        private string sAssIdCur;
        private string sPidCur;
        public NewAssDlg(MainForm f,DataGridView dv)
        {
            InitializeComponent();
            mf = f;
            mf.recvEvent += new MainForm.RecvEventHandler(this.RecvDataEvent);
            dataGridView1 = dv;
        }
        private void RecvDataEvent(object sender, RecvEventArgs e)
        {
            ShowPid(textBoxPid, e.SPid);
            ShowPid(textBoxTid, e.STid);
        }
        // ����ί�лص�����ʵ�ֽ�������Ϣ������ʾ
        delegate void ShowPidCallBack(TextBox textBox, string str);
        private void ShowPid(TextBox textBox, string str)
        {
            if (textBox.InvokeRequired)
            {
                ShowPidCallBack showPidCallBack = ShowPid;
                textBox.Invoke(showPidCallBack, new object[] { textBox, str });
            }
            else
            {
                textBox.Text = str;
            }
        }
        private void AddAssDlg_Load(object sender, EventArgs e)
        {
            textBoxAssPri.Text = "0.00";
            textBoxPpu.Text = "0.00";
            textBoxNum.Text = "1";
            comboBoxUnit.Items.Add("̨");
            comboBoxUnit.Items.Add("��");
            comboBoxUnit.Items.Add("��");
            comboBoxUnit.Items.Add("��");
            comboBoxUnit.Items.Add("��");
            comboBoxUnit.Items.Add("��");

            comboBoxInputTyp.Items.Add("�ɹ�");
            comboBoxInputTyp.Items.Add("�Խ�");
            comboBoxInputTyp.Items.Add("����");
            comboBoxInputTyp.Items.Add("ת��");
            comboBoxInputTyp.Items.Add("����");

            this.Text = string.Format("�ʲ�{0}",sOptType);

            if (sOptType == "�޸�" || sOptType == "����")
            {
                if (sOptType == "����")
                {
                    textBoxAssId.Text = "";
                    textBoxPid.Text = getNewPid();
                    textBoxTid.Text = "";
                }
                else
                {
                    sPidCur = dataGridView1.SelectedRows[0].Cells["��ǩ����"].Value.ToString();
                    sAssIdCur = dataGridView1.SelectedRows[0].Cells["�ʲ����"].Value.ToString();
                    textBoxAssId.Text = sAssIdCur;
                    textBoxPid.Text = sPidCur;
                    textBoxTid.Text = dataGridView1.SelectedRows[0].Cells["��ǩID"].Value.ToString();
                }
                textBoxFinId.Text = dataGridView1.SelectedRows[0].Cells["�������"].Value.ToString();
                textBoxAssPri.Text = dataGridView1.SelectedRows[0].Cells["�ʲ����"].Value.ToString();
                textBoxPpu.Text = dataGridView1.SelectedRows[0].Cells["����"].Value.ToString();
               // comboBoxCat.Text = dataGridView1.SelectedRows[0].Cells["������"].Value.ToString();
                comboBoxTyp.Text = dataGridView1.SelectedRows[0].Cells["����"].Value.ToString();
                comboBoxAssNam.Text = dataGridView1.SelectedRows[0].Cells["�ʲ�����"].Value.ToString();
                textBoxNum.Text = dataGridView1.SelectedRows[0].Cells["����"].Value.ToString();
                textBoxSupplier.Text = dataGridView1.SelectedRows[0].Cells["��Ӧ��"].Value.ToString();
                textBoxSupplierInfo.Text = dataGridView1.SelectedRows[0].Cells["��Ӧ����Ϣ"].Value.ToString();
                textBoxVender.Text = dataGridView1.SelectedRows[0].Cells["����Ʒ��"].Value.ToString();
                textBoxMfrDate.Text = dataGridView1.SelectedRows[0].Cells["��������"].Value.ToString();
                comboBoxUnit.Text = dataGridView1.SelectedRows[0].Cells["��λ"].Value.ToString();
                textBoxSn.Text = dataGridView1.SelectedRows[0].Cells["���к�"].Value.ToString();
                textBoxAssDesc.Text = dataGridView1.SelectedRows[0].Cells["�ʲ�����"].Value.ToString();
                textBoxId.Text = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
                comboBoxInputTyp.Text = dataGridView1.SelectedRows[0].Cells["��������"].Value.ToString();
                comboBoxAddr.Text = dataGridView1.SelectedRows[0].Cells["���ڵص�"].Value.ToString();
                comboBoxStat.Text = dataGridView1.SelectedRows[0].Cells["���״̬"].Value.ToString();
                comboBoxStatSub.Text = dataGridView1.SelectedRows[0].Cells["ʹ��״̬"].Value.ToString();
                comboBoxDept.Text = dataGridView1.SelectedRows[0].Cells["��������"].Value.ToString();
                comboBoxDutyMan.Text = dataGridView1.SelectedRows[0].Cells["������Ա"].Value.ToString();
            }
            else//����
            {
                textBoxPid.Text = getNewPid();
            }
          //  pictureBox1.Image = BarCode.BuildBarCode(textBoxPid.Text);
            //IP��ʾ
         //   IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
         //   IPAddress ipa = ipe.AddressList[0];
         //     groupBox2.Text = string.Format("���ӱ�ǩ��Ϣ(�����ֳֻ�ɨ�裬����IP��{0})", ipa.ToString());
            //��ȡ����б�
            string sSql = "select distinct cat_nam from ass_cat order by convert(cat_nam using gb2312) asc";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxTyp.Items.Add(reader["cat_nam"].ToString());
            }
            reader.Close();

            sSql = "select distinct prd_nam from ass_cat where cat_nam = '" + comboBoxTyp.Text + "'order by convert(prd_nam using gb2312) asc";
            reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxAssNam.Items.Add(reader["prd_nam"].ToString());
            }
            reader.Close();

            if (comboBoxAssNam.Items.Count == 1)
            {
                comboBoxAssNam.SelectedIndex = 0;
            }
            //��ȡ�����б�
            sSql = "select distinct dept_nam from emp";
            reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxDept.Items.Add(reader["dept_nam"].ToString());
            }
            reader.Close();
            //��ȡ��Ա�б�
            sSql = "select distinct emp_nam from emp order by convert(emp_nam using gb2312) asc";
            reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxDutyMan.Items.Add(reader["emp_nam"].ToString());
            }
            reader.Close();
            //��ȡ�ص��б�
            sSql = "select distinct addr_no from addr";
            reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxAddr.Items.Add(reader["addr_no"].ToString());
            }
            reader.Close();

        }
        private string getNewPid()
        {
            string sNewPid = "000000000001";
            string sSql = "select max(pid) maxpid from ass_list where ynenable = 'Y'";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            if(reader.Read())
            {
                sNewPid = reader["maxpid"].ToString();
                if (sNewPid.Length == 0) sNewPid = "000000000000";
                sNewPid = string.Format("{0:000000000000}", Convert.ToDouble(sNewPid) + 1);
            }
            reader.Close();
            return sNewPid;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (sOptType == "����" || sOptType == "����")
            {
                insertData();
            }
            else if (sOptType == "�޸�")
            {
                ModifyData();
            }
        }
        private void insertData()
        {
            if (textBoxAssId.Text.Equals(""))
            {
                MessageBox.Show("�ʲ����벻��Ϊ��!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sSql = "select 'X' from ass_list where ass_id = '" + textBoxAssId.Text + "' and ynenable = 'Y'";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            if (reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("�ʲ������Ѵ���!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            reader.Close();

            if (textBoxPid.Text.Length != 0)
            {
                sSql = "select ass_id from ass_list where pid = '" + textBoxPid.Text + "' and ynenable = 'Y'";
                reader = MysqlHelper.ExecuteReader(sSql);
                if (reader.Read())
                {
                    string sAssid = reader["ass_id"].ToString();
                    MessageBox.Show("�ñ�ǩ�Ѱ��ʲ�:" + sAssid + "\r\n���������������գ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    return;
                }
                reader.Close();
            }

            if (comboBoxTyp.Text.Length == 0)
            {
                MessageBox.Show("��ѡ���ʲ�����!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxAssNam.Text.Length == 0)
            {
                MessageBox.Show("��ѡ���ʲ�����!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sSqlIns = string.Format(@"insert into ass_list
                (cre_tm,ass_id,fin_id,pid,tid,ass_nam,ass_desc,ass_pri,reg_date,typ,
                supplier,sn,vender,mfr_date,unit,num,ppu,company,cre_man,supplier_info,input_typ,addr,dept,duty_man,stat,stat_sub) 
                values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',
                '{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}')",
                MainForm.getDateTime(),
                textBoxAssId.Text,
                textBoxFinId.Text,
                textBoxPid.Text,
                textBoxTid.Text,
              //  comboBoxCat.Text.Substring(0, 3),
                comboBoxAssNam.Text,
                textBoxAssDesc.Text,
                textBoxAssPri.Text,
                MainForm.getDate(),
                comboBoxTyp.Text,//typ
                textBoxSupplier.Text,
                textBoxSn.Text,
                textBoxVender.Text,
                textBoxMfrDate.Text,
                comboBoxUnit.Text,
                textBoxNum.Text,
                textBoxPpu.Text,
                MainForm.sCompany, // company
                "SYS",//cre_man
                textBoxSupplierInfo.Text,
                comboBoxInputTyp.Text,
                comboBoxAddr.Text,
                comboBoxDept.Text,
                comboBoxDutyMan.Text,
                comboBoxStat.Text,
                comboBoxStatSub.Text
                );

            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
    "����", "0", sSqlIns.Replace("'", "\\'"), MainForm.sClientId, textBoxAssId.Text, MainForm.getDateTime());

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
        private void ModifyData()
        {
            if (textBoxAssId.Text.Equals(""))
            {
                MessageBox.Show("�ʲ����벻��Ϊ��!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxAssId.Text != sAssIdCur)
            {
                string sSql = "select 'X' from ass_list where ass_id = '" + textBoxAssId.Text + "' and ynenable = 'Y'";
                MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
                if (reader.HasRows)
                {
                    reader.Close();
                    MessageBox.Show("�ʲ������Ѵ���!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                reader.Close();
            }

            if (textBoxPid.Text .Length != 0 && textBoxPid.Text != sPidCur)
            {
                string sSql = "select ass_id from ass_list where pid = '" + textBoxPid.Text + "' and ynenable = 'Y'";
                MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
                if (reader.Read())
                {
                    string sAssid = reader["ass_id"].ToString();
                    MessageBox.Show("�ñ�ǩ�Ѱ��ʲ�:" + sAssid + "\r\n���������������գ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    return;
                }
                reader.Close();
            }
            
            string sSqlUpd = string.Format(@"update ass_list set fin_id = '{0}',ass_id = '{1}', ass_nam = '{2}', ass_desc = '{3}', ass_pri = '{4}', mod_tm = '{5}',
typ = '{6}', supplier = '{7}', supplier_info = '{8}', sn = '{9}',vender = '{10}', mfr_date = '{11}', unit = '{12}', 
num = '{13}', ppu = '{14}',memo = '{15}', mod_man = '{16}',input_typ = '{17}',addr = '{18}',pid = '{19}',tid = '{20}',dept ='{21}',duty_man ='{22}',stat ='{23}',stat_sub ='{24}' where id = '{25}'",
                    textBoxFinId.Text,
                    textBoxAssId.Text,
                    comboBoxAssNam.Text,
                    textBoxAssDesc.Text,
                    textBoxAssPri.Text,
                    MainForm.getDateTime(),
                    comboBoxTyp.Text,//typ
                    textBoxSupplier.Text,
                    textBoxSupplierInfo.Text,
                    textBoxSn.Text,
                    textBoxVender.Text,
                    textBoxMfrDate.Text,
                    comboBoxUnit.Text,
                    textBoxNum.Text,
                    textBoxPpu.Text,
                    "�޸ı�ע",//memo
                    MainForm.sUserName,//mod_man 
                    comboBoxInputTyp.Text,
                    comboBoxAddr.Text,
                    textBoxPid.Text,
                    textBoxTid.Text,
                    comboBoxDept.Text,
                    comboBoxDutyMan.Text,
                    comboBoxStat.Text,
                    comboBoxStatSub.Text,
                    textBoxId.Text);

            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
    "�޸�", "0", sSqlUpd.Replace("'", "''"), MainForm.sClientId, textBoxAssId.Text, MainForm.getDateTime());

            List<string> listSql = new List<string>();
            listSql.Add(sSqlUpd);
            listSql.Add(sSqlInsLog);
            bool bOK = false;
            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("�޸ĳɹ���");
                bDone = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("�޸�ʧ�ܣ�\r\n" + MysqlHelper.sLastErr);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddAssDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            mf.recvEvent -= new MainForm.RecvEventHandler(this.RecvDataEvent);
        }

        private void comboBoxTyp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string str = comboBoxTyp.SelectedItem.ToString();
            comboBoxAssNam.Items.Clear();
            string sSql = "select distinct prd_nam from ass_cat where cat_nam = '"+ str +"'order by convert(prd_nam using gb2312) asc";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxAssNam.Items.Add(reader["prd_nam"].ToString());
            }
            reader.Close();

            if (comboBoxAssNam.Items.Count == 1)
            {
                comboBoxAssNam.SelectedIndex = 0;
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            textBoxPid.Text = getNewPid();
           // pictureBox1.Image = BarCode.BuildBarCode(textBoxPid.Text);
        }

    }
}