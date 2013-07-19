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
        //class UdpState
        //{
        //    public UdpClient u;
        //    public IPEndPoint e;
        //}
        public TextBox textBoxLog;

        private BindingSource bs = new BindingSource();

        //MySqlDataAdapter da;
        string sSQLSelect;
        //DataTable dt;
        //MySqlDataReader dr;
        //MySqlCommand cmd;
        //MySqlConnection myConn = null;

        MainForm mf;
        public AssInput(MainForm f)
        {
            InitializeComponent();
            f.recvEvent += new MainForm.RecvEventHandler(this.RecvDataEvent);
            //myConn = f.myConn;
            mf = f;
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

        private void AssInput_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.ass_list' table. You can move, or remove it, as needed.
            this.ShowInTaskbar = false;
            textBoxAssPri.Text = "0.00";
            textBoxPpu.Text = "0.00";
            textBoxNum.Text = "1";
            comboBoxUnit.Items.Add("̨");
            comboBoxUnit.Items.Add("��");
            comboBoxUnit.Items.Add("��");
            comboBoxUnit.Items.Add("��");
            comboBoxUnit.Items.Add("��");
            comboBoxUnit.Items.Add("��");

            //comboBoxAddr.Items.Add("�����ܹ�˾");
            //comboBoxAddr.Items.Add("���ݷֹ�˾");
            //comboBoxAddr.Items.Add("���ݰ��´�");
            //comboBoxAddr.Items.Add("�����ֹ�˾");
            //comboBoxAddr.Items.Add("�人���´�");
            //comboBoxAddr.Items.Add("ά������");

            //comboBoxStat.Items.Add("���");
            //comboBoxStat.Items.Add("����");
            //comboBoxStat.Items.Add("����");
            //comboBoxStat.Items.Add("���");


            comboBoxInputTyp.Items.Add("�ɹ�");
            comboBoxInputTyp.Items.Add("�Խ�");
            comboBoxInputTyp.Items.Add("����");
            comboBoxInputTyp.Items.Add("ת��");
            comboBoxInputTyp.Items.Add("����");

            radioButtonNew.Checked = true;
            radioButtonModify.Checked = false;
            radioButtonQuery.Checked = false;

            sSQLSelect = "select Id ID,ass_id �ʲ����,fin_id �������,pid ��ǩ����,tid ��ǩID,cat_no ������,typ ����,ass_nam �ʲ�����,ass_desc �ʲ�����,ass_pri �ʲ����,reg_date �Ǽ�����,dept ����,duty_man ������Ա,addr ���ڵص�,use_co ���ڹ�˾,f_getStat(stat,stat_sub) ״̬,supplier ��Ӧ��,supplier_info ��Ӧ����Ϣ,sn ���к�,vender ����Ʒ��,mfr_date ��������,unit ��λ,num ����,ppu ����,duty_man ������,company �ʲ�����,memo ��ע,cre_man ������Ա,cre_tm ����ʱ��,mod_man �޸���Ա,mod_tm �޸�ʱ��,input_typ �������� from ass_list";

            string sSql = sSQLSelect;
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bs.DataSource = dt;
            bindingNavigator1.BindingSource = bs;
            dataGridView1.DataSource = bs;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            textBoxAssId.DataBindings.Add("Text", bs, "�ʲ����");
            textBoxFinId.DataBindings.Add("Text", bs, "�������");
            textBoxAssPri.DataBindings.Add("Text", bs, "�ʲ����");
            textBoxPpu.DataBindings.Add("Text", bs, "����");
            comboBoxCat.DataBindings.Add("Text", bs, "������");
            textBoxTyp.DataBindings.Add("Text", bs, "����");
            textBoxAssNam.DataBindings.Add("Text", bs, "�ʲ�����");
            textBoxNum.DataBindings.Add("Text", bs, "����");
            textBoxSupplier.DataBindings.Add("Text", bs, "��Ӧ��");
            textBoxSupplierInfo.DataBindings.Add("Text", bs, "��Ӧ����Ϣ");
            textBoxVender.DataBindings.Add("Text", bs, "����Ʒ��");
            textBoxMfrDate.DataBindings.Add("Text", bs, "��������");
            comboBoxUnit.DataBindings.Add("Text", bs, "��λ");
            textBoxSn.DataBindings.Add("Text", bs, "���к�");
            //textBoxPid.DataBindings.Add("Text", bs, "��ǩ����");
            //textBoxTid.DataBindings.Add("Text", bs, "��ǩID");
            textBoxAssDesc.DataBindings.Add("Text", bs, "�ʲ�����");
            textBoxId.DataBindings.Add("Text", bs, "ID");
            comboBoxInputTyp.DataBindings.Add("Text", bs, "��������");
            comboBoxAddr.DataBindings.Add("Text", bs, "���ڵص�");
            //��ȡ����б�

            sSql = "select cat_no,cat_nam,prd_nam from ass_cat";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxCat.Items.Add(
                    reader["cat_no"].ToString() + "-" + reader["cat_nam"].ToString() + "-" + reader["prd_nam"].ToString());
            }
            //try
            //{
            //    //��DB
            //    myConn.Open();
            //    //��ȡ�б�
            //    sSQLSelect = "select Id ID,ass_id �ʲ����,fin_id �������,pid ��ǩ����,tid ��ǩID,cat_no ������,typ ����,ass_nam �ʲ�����,ass_desc �ʲ�����,ass_pri �ʲ����,reg_date �Ǽ�����,use_dept ���ò���,use_man ������Ա,addr ���ڵص�,use_co ���ڹ�˾,f_getStat(stat,stat_sub) ״̬,supplier ��Ӧ��,supplier_info ��Ӧ����Ϣ,sn ���к�,vender ����Ʒ��,mfr_date ��������,unit ��λ,num ����,ppu ����,duty_man ������,company �ʲ�����,memo ��ע,cre_man ������Ա,cre_tm ����ʱ��,mod_man �޸���Ա,mod_tm �޸�ʱ��,input_typ �������� from ass_list";
            //    da = new MySqlDataAdapter(sql, myConn);
            //    dt = new DataTable();
            //    da.Fill(dt);
            //    bs.DataSource = dt;
            //    bindingNavigator1.BindingSource = bs;
            //    dataGridView1.DataSource = bs;
            //    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            //    textBoxAssId.DataBindings.Add("Text", bs, "�ʲ����");
            //    textBoxFinId.DataBindings.Add("Text", bs, "�������");
            //    textBoxAssPri.DataBindings.Add("Text", bs, "�ʲ����");
            //    textBoxPpu.DataBindings.Add("Text", bs, "����");
            //    comboBoxCat.DataBindings.Add("Text", bs, "������");
            //    textBoxTyp.DataBindings.Add("Text", bs, "����");
            //    textBoxAssNam.DataBindings.Add("Text", bs, "�ʲ�����");
            //    textBoxNum.DataBindings.Add("Text", bs, "����");
            //    textBoxSupplier.DataBindings.Add("Text", bs, "��Ӧ��");
            //    textBoxSupplierInfo.DataBindings.Add("Text", bs, "��Ӧ����Ϣ");
            //    textBoxVender.DataBindings.Add("Text", bs, "����Ʒ��");
            //    textBoxMfrDate.DataBindings.Add("Text", bs, "��������");
            //    comboBoxUnit.DataBindings.Add("Text", bs, "��λ");
            //    textBoxSn.DataBindings.Add("Text", bs, "���к�");
            //    //textBoxPid.DataBindings.Add("Text", bs, "��ǩ����");
            //    //textBoxTid.DataBindings.Add("Text", bs, "��ǩID");
            //    textBoxAssDesc.DataBindings.Add("Text", bs, "�ʲ�����");
            //    textBoxId.DataBindings.Add("Text", bs, "ID");
            //    comboBoxInputTyp.DataBindings.Add("Text", bs, "��������");
            //    comboBoxAddr.DataBindings.Add("Text", bs, "���ڵص�");
            //    //��ȡ����б�
            //    cmd = myConn.CreateCommand();
            //    cmd.CommandText = "select cat_no,cat_nam,prd_nam from ass_cat";
            //    // cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = textBox1.Text;
            //    dr = cmd.ExecuteReader();

            //    comboBoxCat.Items.Clear();
            //    while (dr.Read())
            //    {
            //        comboBoxCat.Items.Add(dr.GetValue(0).ToString() + "-" + dr.GetValue(1).ToString() + "-" + dr.GetValue(2).ToString());
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    myConn.Close();
            //}
           
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (radioButtonNew.Checked)
            {
                insertData();
            }
            else if (radioButtonQuery.Checked)
            {
                QueryData();
            }
            else if (radioButtonModify.Checked)
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
            string sSql = "select 'X' from ass_list where ass_id = '" + textBoxAssId.Text + "'";
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
                sSql = "select ass_id from ass_list where pid = '" + textBoxPid.Text + "'";
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

            if (comboBoxCat.Text.Equals(""))
            {
                MessageBox.Show("��ѡ���ʲ����!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sSqlIns = string.Format(@"insert into ass_list
                (cre_tm,ass_id,fin_id,pid,tid,cat_no,ass_nam,ass_desc,ass_pri,reg_date,typ,
                supplier,sn,vender,mfr_date,unit,num,ppu,company,cre_man,supplier_info,input_typ,addr) 
                values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',
                '{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')",
                MainForm.getDateTime(),
                textBoxAssId.Text,
                textBoxFinId.Text,
                textBoxPid.Text,
                textBoxTid.Text,
                comboBoxCat.Text.Substring(0, 3),
                textBoxAssNam.Text,
                textBoxAssDesc.Text,
                textBoxAssPri.Text,
                MainForm.getDate(),
                textBoxTyp.Text,//typ
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
                comboBoxAddr.Text);

            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
    "����", "0", sSqlIns.Replace("'", "\\'"), MainForm.sClientId, textBoxAssId.Text,MainForm.getDateTime());

            List<string> listSql = new List<string>();
            listSql.Add(sSqlIns);
            listSql.Add(sSqlInsLog);
            bool bOK = false;
            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("�����ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                resetData();
                bs.MoveLast();
            }
            else
            {
                MessageBox.Show("����ʧ�ܣ�\r\n" + MysqlHelper.sLastErr, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
//        private void insertData()
//        {
//            if (textBoxAssId.Text.Equals(""))
//            {
//                MessageBox.Show("�ʲ���Ų���Ϊ��!");
//                return;
//            }
//            if (comboBoxCat.Text.Equals(""))
//            {
//                MessageBox.Show("��ѡ���ʲ����!");
//                return;
//            }

//            try
//            {
//                cmd = myConn.CreateCommand();//sql������󣬱�ʾҪ��sql���ݿ�ִ��һ��sql���
//                cmd.CommandText = @"insert into ass_list
//                    (cre_tm,ass_id,fin_id,pid,tid,cat_no,ass_nam,ass_desc,ass_pri,reg_date,typ,
//                    supplier,sn,vender,mfr_date,unit,num,ppu,company,cre_man,supplier_info,input_typ,addr) 
        //                    values(CURRENT_TIMESTAMP,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17,@18,@19,@20,@21,@22))";//sql���@��ʾ����
//                cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = textBoxAssId.Text;
//                cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = textBoxFinId.Text;
//                cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = textBoxPid.Text;
//                cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = textBoxTid.Text;
//                cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = comboBoxCat.Text.Substring(0, 3);
//                cmd.Parameters.Add("@6", MySqlDbType.VarChar).Value = textBoxAssNam.Text;
//                cmd.Parameters.Add("@7", MySqlDbType.VarChar).Value = textBoxAssDesc.Text;
//                cmd.Parameters.Add("@8", MySqlDbType.VarChar).Value = textBoxAssPri.Text;
//                cmd.Parameters.Add("@9", MySqlDbType.VarChar).Value = DateTime.Now.ToString("yyyyMMdd");
//                cmd.Parameters.Add("@10", MySqlDbType.VarChar).Value = textBoxTyp.Text;//typ
//                cmd.Parameters.Add("@11", MySqlDbType.VarChar).Value = textBoxSupplier.Text;
//                cmd.Parameters.Add("@12", MySqlDbType.VarChar).Value = textBoxSn.Text;
//                cmd.Parameters.Add("@13", MySqlDbType.VarChar).Value = textBoxVender.Text;
//                cmd.Parameters.Add("@14", MySqlDbType.VarChar).Value = textBoxMfrDate.Text;
//                cmd.Parameters.Add("@15", MySqlDbType.VarChar).Value = comboBoxUnit.Text;
//                cmd.Parameters.Add("@16", MySqlDbType.VarChar).Value = textBoxNum.Text;
//                cmd.Parameters.Add("@17", MySqlDbType.VarChar).Value = textBoxPpu.Text;
//                cmd.Parameters.Add("@18", MySqlDbType.VarChar).Value = MainWnd.sCompany; // company
//                cmd.Parameters.Add("@19", MySqlDbType.VarChar).Value = "SYS";//cre_man
//                cmd.Parameters.Add("@20", MySqlDbType.VarChar).Value = textBoxSupplierInfo.Text;
//                cmd.Parameters.Add("@21", MySqlDbType.VarChar).Value = comboBoxInputTyp.Text;
//                cmd.Parameters.Add("@22", MySqlDbType.VarChar).Value = comboBoxAddr.Text;
//                myConn.Open();//������
//                cmd.ExecuteNonQuery();//ִ�в��ǲ�ѯ��sql���
//                MessageBox.Show("�����ɹ���");
//                resetData();
//                bs.MoveLast();
//            }
//            catch (Exception ex)//����sql���԰�ȫ�Ǻ����У���׽�쳣
//            {
//                MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                myConn.Close();//���ܴ򿪳ɹ�����ʧ�ܣ����ܹر�����
//            }
//        }
        private void QueryData()
        {
            string sSql = sSQLSelect;
            if (textBoxAssId.Text.Length != 0)
            {
                sSql += " where ass_id = \'" + textBoxAssId.Text + "\'";
            }
            //da = new MySqlDataAdapter(tmp, myConn);
            //dt = new DataTable();
            //da.Fill(dt);
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bs.DataSource = dt;
        }        
        
//        private void ModifyData()
//        {
//            try
//            {
//                cmd = myConn.CreateCommand();
//                myConn.Open();
//                cmd.CommandText = @"update ass_list set 
        //ass_id = @1, fin_id = @2, cat_no = @5, ass_nam = @6, ass_desc = @7, ass_pri = @8, mod_tm = CURRENT_TIMESTAMP,
//typ = @9, supplier = @10, supplier_info = @11, sn = @12,vender = @13, mfr_date = @14, unit = @15, num = @16, ppu = @17, 
//memo = @18, mod_man = @19,input_typ = @20,input_typ = @21 where id = @22";
//                cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = textBoxAssId.Text;
//                cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = textBoxFinId.Text;
//                cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = textBoxPid.Text;
//                cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = textBoxTid.Text;
//                cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = comboBoxCat.Text.Substring(0, 3);
//                cmd.Parameters.Add("@6", MySqlDbType.VarChar).Value = textBoxAssNam.Text;
//                cmd.Parameters.Add("@7", MySqlDbType.VarChar).Value = textBoxAssDesc.Text;
//                cmd.Parameters.Add("@8", MySqlDbType.VarChar).Value = textBoxAssPri.Text;
//                cmd.Parameters.Add("@9", MySqlDbType.VarChar).Value = textBoxTyp.Text;//typ
//                cmd.Parameters.Add("@10", MySqlDbType.VarChar).Value = textBoxSupplier.Text;
//                cmd.Parameters.Add("@11", MySqlDbType.VarChar).Value = textBoxSupplierInfo.Text;
//                cmd.Parameters.Add("@12", MySqlDbType.VarChar).Value = textBoxSn.Text;
//                cmd.Parameters.Add("@13", MySqlDbType.VarChar).Value = textBoxVender.Text;
//                cmd.Parameters.Add("@14", MySqlDbType.VarChar).Value = textBoxMfrDate.Text;
//                cmd.Parameters.Add("@15", MySqlDbType.VarChar).Value = comboBoxUnit.Text;
//                cmd.Parameters.Add("@16", MySqlDbType.VarChar).Value = textBoxNum.Text;
//                cmd.Parameters.Add("@17", MySqlDbType.VarChar).Value = textBoxPpu.Text;
//                cmd.Parameters.Add("@18", MySqlDbType.VarChar).Value = "�޸ı�ע";//memo
//                cmd.Parameters.Add("@19", MySqlDbType.VarChar).Value = "SYS";//cre_man 
//                cmd.Parameters.Add("@20", MySqlDbType.VarChar).Value = comboBoxInputTyp.Text;
//                cmd.Parameters.Add("@21", MySqlDbType.VarChar).Value = comboBoxAddr.Text;
//                cmd.Parameters.Add("@22", MySqlDbType.VarChar).Value = textBoxId.Text;
               
//                cmd.ExecuteNonQuery();
//                MessageBox.Show("�޸ĳɹ�!");
//                resetData();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                myConn.Close();
//            }
//        }
        private void ModifyData()
        {
            string sSqlUpd = string.Format(@"update ass_list set fin_id = '{0}',cat_no = '{1}', ass_nam = '{2}', ass_desc = '{3}', ass_pri = '{4}', mod_tm = '{5}',
typ = '{6}', supplier = '{7}', supplier_info = '{8}', sn = '{9}',vender = '{10}', mfr_date = '{11}', unit = '{12}', 
num = '{13}', ppu = '{14}',memo = '{15}', mod_man = '{16}',input_typ = '{17}',addr = '{18}' where id = '{19}'",
                    //textBoxAssId.Text,
                    textBoxFinId.Text,
                    //textBoxPid.Text,
                    //textBoxTid.Text,
                    comboBoxCat.Text.Substring(0, 3),
                    textBoxAssNam.Text,
                    textBoxAssDesc.Text,
                    textBoxAssPri.Text,
                    MainForm.getDateTime(),
                    textBoxTyp.Text,//typ
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
                    textBoxId.Text);

            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
    "�޸�", "0", sSqlUpd.Replace("'", "\\'"), MainForm.sClientId, textBoxAssId.Text,MainForm.getDateTime());

                List<string> listSql = new List<string>();
                listSql.Add(sSqlUpd);
                listSql.Add(sSqlInsLog);
                bool bOK = false;
                bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
                if (bOK)
                {
                    MessageBox.Show("�޸ĳɹ���");
                    resetData();
                }
                else
                {
                    MessageBox.Show("�޸�ʧ�ܣ�\r\n" + MysqlHelper.sLastErr);
                }
        }
        private void buttonReplace_Click(object sender, EventArgs e)
        {
            ReplaceTag();
        }
        //private void ReplaceTag()
        //{
        //    if (textBoxPid.Text.Length == 0)
        //    {
        //        if (MessageBox.Show("�±�ǩΪ�գ�ȷ��Ҫ��ձ�ǩ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        //            return;
        //    }
        //    else
        //    {
        //        if (MessageBox.Show("��ȷ��Ҫ�滻�ñ�ǩ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        //           return;
        //    }
        //    try
        //    {
        //        cmd = myConn.CreateCommand();
        //        myConn.Open();
        //        cmd.CommandText = @"update ass_list set pid = @1, tid = @2, mod_tm = CURRENT_TIMESTAMP,mod_man = @3,memo = @4 where id = @5";
        //        cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = textBoxPid.Text;
        //        cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = textBoxTid.Text;
        //        cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = "SYS";//cre_man
        //        cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = "��ǩ����";//memo
        //        cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = textBoxId.Text;

        //        cmd.ExecuteNonQuery();
        //        MessageBox.Show("�����ɹ�!");
        //        resetData();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        myConn.Close();
        //    }
        //} 
        private void ReplaceTag()
        {
            if (textBoxPid.Text.Length == 0)
            {
                if (MessageBox.Show("ȷ��Ҫ��ձ�ǩ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            else
            {
                string sSql = "select ass_id from ass_list where pid = '" + textBoxPid.Text + "'";
                MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
                if (reader.Read())
                {
                    string sAssid = reader["ass_id"].ToString();
                    MessageBox.Show("�ñ�ǩ�Ѱ��ʲ�:" + sAssid + "\r\n���������������գ�","��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    return;
                }
                reader.Close();
                if (MessageBox.Show("��ȷ��Ҫ�滻�ñ�ǩ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            string sSqlUpd
                = string.Format("update ass_list set pid = '{0}', tid = '{1}', mod_tm = '{2}',mod_man = '{3}',memo = '{4}' where id = '{5}'",
            textBoxPid.Text,
            textBoxTid.Text,
            MainForm.getDateTime(),
            "SYS",//cre_man
            "��ǩ����",//memo
            textBoxId.Text);
            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
"�滻", "0", sSqlUpd.Replace("'", "\\'"), MainForm.sClientId, textBoxAssId.Text, MainForm.getDateTime());

            List<string> listSql = new List<string>();
            listSql.Add(sSqlUpd);
            listSql.Add(sSqlInsLog);
            bool bOK = false;
            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("�滻�ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                resetData();
            }
            else
            {
                MessageBox.Show("�滻ʧ�ܣ�\r\n" + MysqlHelper.sLastErr, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        } 
        private void resetData()
        {
            //��ȡ�б�
            //da = new MySqlDataAdapter(sql, myConn);
            //dt = new DataTable();
            //da.Fill(dt);
            DataTable dt = MysqlHelper.ExecuteDataTable(sSQLSelect);
            bs.DataSource = dt;
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }    
        //private void DeleteData()
        //{
        //    if (MessageBox.Show("��ȷ��Ҫɾ���ñ��ʲ���\r\nɾ�������ɻָ���","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
        //        return;
        //    try
        //    {
        //        cmd = myConn.CreateCommand();
        //        myConn.Open();
        //        cmd.CommandText = "delete from ass_list where id=@id";//ɾ����䣬��IDΪ����ɾ��
        //        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = textBoxId.Text;//dataGridView1.CurrentRow.Cells[0].Value.ToString(); 
        //        cmd.ExecuteNonQuery();
        //        resetData();
        //        MessageBox.Show("ɾ���ɹ���");

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        myConn.Close();
        //    }
        //}
        private void DeleteData()
        {
            if (MessageBox.Show("��ȷ��Ҫɾ���ñ��ʲ���\r\nɾ�������ɻָ���","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string sSqlDel = string.Format("delete from ass_list where id='{0}'",textBoxId.Text);//ɾ����䣬��IDΪ����ɾ��

            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
"ɾ��", "0", sSqlDel.Replace("'", "\\'"), MainForm.sClientId, textBoxAssId.Text, MainForm.getDateTime());

            List<string> listSql = new List<string>();
            listSql.Add(sSqlDel);
            listSql.Add(sSqlInsLog);
            bool bOK = false;
            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("ɾ���ɹ���");
                resetData();
            }
            else
            {
                MessageBox.Show("ɾ��ʧ�ܣ�\r\n" + MysqlHelper.sLastErr);
            }
        }
       

        private void AssInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            //myConn.Close();
            mf.recvEvent -= new MainForm.RecvEventHandler(this.RecvDataEvent);
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
        private void comboBoxCat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string str = comboBoxCat.SelectedItem.ToString();
            int nIndex1 = str.IndexOf('-');
            int nIndex2 = str.LastIndexOf('-');
            string str1 = str.Substring(nIndex1 + 1, nIndex2 - nIndex1 - 1);
            string str2 = str.Substring(nIndex2 + 1);
            textBoxTyp.Text = str1;
            textBoxAssNam.Text = str2;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add");
        }

        private void AssInput_Resize(object sender, EventArgs e)
        {
            //int i = this.Size.Width; ;
            //int j = this.Parent.Size.Width; ;
            //AddLog(textBoxLog, "Parent:" + this.Parent.Size.ToString());
            //AddLog(textBoxLog, "AssInput:" + this.Size.ToString());
            //AddLog(textBoxLog, "dataGridView1:" + dataGridView1.Size.ToString());
            //  dataGridView1.Size = this.Size;
            // dataGridView1.Size.Width = this.Size.Width;
            // dataGridView1.Size.Height = this.Size.Height-200;
        }

        private void radioButtonNew_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNew.Checked)
            {
                AddLog(textBoxLog, radioButtonNew.Text);
                radioButtonQuery.Checked = false;
                radioButtonModify.Checked = false;

                buttonOK.Text = "����";

                textBoxAssId.ReadOnly = false;
                textBoxFinId.ReadOnly = false;
                textBoxAssPri.ReadOnly = false;
                textBoxPpu.ReadOnly = false;
                textBoxTyp.ReadOnly = false;
                textBoxAssNam.ReadOnly = false;
                textBoxNum.ReadOnly = false;
                textBoxSupplier.ReadOnly = false;
                textBoxVender.ReadOnly = false;
                textBoxMfrDate.ReadOnly = false;
                comboBoxUnit.Enabled = true;
                textBoxSn.ReadOnly = false;
                textBoxPid.ReadOnly = false;
                textBoxTid.ReadOnly = false;
                textBoxAssDesc.ReadOnly = false;
                comboBoxCat.Enabled = true;
                comboBoxCat.Enabled = true;
                comboBoxUnit.Enabled = true;

                bs.MoveFirst();

            }
        }

        private void radioButtonQuery_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonQuery.Checked)
            {
                AddLog(textBoxLog, radioButtonQuery.Text);
                radioButtonNew.Checked = false;
                radioButtonModify.Checked = false;

                buttonOK.Text = "��ѯ";

                textBoxAssId.ReadOnly = false;
                textBoxFinId.ReadOnly = true;
                textBoxAssPri.ReadOnly = true;
                textBoxPpu.ReadOnly = true;
                textBoxTyp.ReadOnly = true;
                textBoxAssNam.ReadOnly = true;
                textBoxNum.ReadOnly = true;
                textBoxSupplier.ReadOnly = true;
                textBoxSupplierInfo.ReadOnly = true;
                textBoxVender.ReadOnly = true;
                textBoxMfrDate.ReadOnly = true;
                comboBoxUnit.Enabled = true;
                textBoxSn.ReadOnly = true;
                textBoxPid.ReadOnly = true;
                textBoxTid.ReadOnly = true;
                textBoxAssDesc.ReadOnly = true;
                comboBoxCat.Enabled = true;
                comboBoxCat.Enabled = false;
                comboBoxUnit.Enabled = false;
                bs.MoveFirst();

            }

        }

        private void radioButtonModify_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonModify.Checked)
            {
                AddLog(textBoxLog, radioButtonModify.Text);
                radioButtonNew.Checked = false;
                radioButtonQuery.Checked = false;

                buttonOK.Text = "����";

                textBoxAssId.ReadOnly = true;
                textBoxFinId.ReadOnly = false;
                textBoxAssPri.ReadOnly = false;
                textBoxPpu.ReadOnly = false;
                textBoxTyp.ReadOnly = false;
                textBoxAssNam.ReadOnly = false;
                textBoxNum.ReadOnly = false;
                textBoxSupplier.ReadOnly = false;
                textBoxSupplierInfo.ReadOnly = false;
                textBoxVender.ReadOnly = false;
                textBoxMfrDate.ReadOnly = false;
                comboBoxUnit.Enabled = false;
                textBoxSn.ReadOnly = false;
                textBoxPid.ReadOnly = false;
                textBoxTid.ReadOnly = false;
                textBoxAssDesc.ReadOnly = false;
                comboBoxCat.Enabled = false;
                comboBoxCat.Enabled = true;
                comboBoxUnit.Enabled = true;
                bs.MoveFirst();
            }

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

    }
}