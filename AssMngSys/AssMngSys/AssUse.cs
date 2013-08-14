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
    public partial class AssUse : Form
    {
        private BindingSource bs = new BindingSource();

        string sSQLSelect;
        
        MainForm mf;

        List<string> aList = new List<string>();
        public AssUse(MainForm f)
        {
            InitializeComponent();
            f.recvEvent += new MainForm.RecvEventHandler(this.RecvDataEvent);
            mf = f;
        }

        private void AssUse_Load(object sender, EventArgs e)
        {
            sSQLSelect = AssSupply.sSQLSelect;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
          //  radioButtonApply.Checked = true;
            radioButtonBorrow.Checked = true;
            radioButtonStartRepair.Checked = true;
            radioButtonOut.Checked = true;
            //��ȡ�ʲ���Ϣ��ͷ
            string sSql = sSQLSelect + " and '1' = '0'";
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bs.DataSource = dt;
            bindingNavigator1.BindingSource = bs;
            dataGridView1.DataSource = bs;
            //��ȡ�����б�
            sSql = "select distinct dept_nam from emp";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxDept.Items.Add(reader["dept_nam"].ToString());
            }
            //��ȡ��Ա�б�
            sSql = "select distinct emp_nam from emp order by convert(emp_nam using gb2312) asc";
            reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxMan.Items.Add(reader["emp_nam"].ToString());
            }
            ////��ȡ��ַ�б�
            //sSql = "select distinct addr_no from addr";
            //reader = MysqlHelper.ExecuteReader(sSql);
            //while (reader.Read())
            //{
            //    comboBoxAddr.Items.Add(reader["addr_no"].ToString());
            //}
        }

        private void RecvDataEvent(object sender, RecvEventArgs e)
        {
            ShowPid(textBoxPid, e.SPid);
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
        private void GetAss(string sData, int nType) //0���٣�1����
        {
            if (nType == -1)
            {
                aList.Remove(sData);
            }
            else
            {
                bool bFind = false;
                foreach (object o in aList)
                {
                    if (o.ToString().Equals(sData))
                    {
                        bFind = true;
                    }
                }
                if (!bFind)
                {
                    aList.Add(sData);
                }
                else
                {
                    return;
                }
            }
            string sSql = sSQLSelect + " and pid in('0'";
            foreach (object o in aList)
            {
                sSql += ",\'" + o.ToString() + "\'";
            }
            sSql += (")");

            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bs.DataSource = dt;
            bindingNavigator1.BindingSource = bs;
            dataGridView1.DataSource = bs;
        }

        private void AssUse_FormClosing(object sender, FormClosingEventArgs e)
        {
            aList.Clear();
            mf.recvEvent -= new MainForm.RecvEventHandler(this.RecvDataEvent);
        }

        private void textBoxPid_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPid.Text.Length == 12)
            {
                GetAss(textBoxPid.Text, 1);
            }
        }

        private void toolStripTextBoxPid_TextChanged(object sender, EventArgs e)
        {
            if (toolStripTextBoxPid.Text.Length == 12)
            {
                GetAss(toolStripTextBoxPid.Text, 1);
            }
        }


        private void buttonClear_Click(object sender, EventArgs e)
        {
            // dt.Rows.Clear();
            textBoxPid.Text = "";
            aList.Clear();
            listBox1.Items.Clear();
            string sSql = sSQLSelect + " and '1' = '0'";
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bs.DataSource = dt;
            bindingNavigator1.BindingSource = bs;
            dataGridView1.DataSource = bs;
            //try
            //{
            //    //��DB
            //    if (myConn.State == ConnectionState.Closed) myConn.Open();
            //    da = new MySqlDataAdapter(sql + " where '1' = '0'", myConn);
            //    dt = new DataTable();
            //    da.Fill(dt);
            //    bs.DataSource = dt;
            //    bindingNavigator1.BindingSource = bs;
            //    dataGridView1.DataSource = bs;
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

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                GetAss(dataGridView1.CurrentRow.Cells["��ǩ����"].Value.ToString(), -1);
                // listBox1.Items.Clear();
                dataGridView1_SelectionChanged(null, null);
            }
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (aList.Count == 0)
            {
                MessageBox.Show("SORRY�����κμ�¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sErr = "";
            bool bOK = false;
            int nIndex = tabControl1.SelectedIndex;
            switch (nIndex)
            {
                case 0://����
                    bOK = Borrow(out sErr);
                    break;
                case 1://ά��
                    bOK = Repair(out sErr);
                    break;
                case 2://���
                    bOK = TakeOut(out sErr);
                    break;
                //case 0://����
                //    bOK = Apply(out sErr);
                //    break;
                //case 1://����
                //    bOK = Borrow(out sErr);
                //    break;
                //case 2://ά��
                //    bOK = Repair(out sErr);
                //    break;
                //case 3://���
                //    bOK = TakeOut(out sErr);
                //    break;
                //case 4://�⻹
                //    bOK = RentBack(out sErr);
                //    break;
                //case 5://�˷�
                //    bOK = Rejiect(out sErr);
                //    break;
                //case 6://��ʧ
                //    bOK = Lose(out sErr);
                //    break;
                //case 7://����
                //    bOK = Discard(out sErr);
                //    break;
                //case 8://ת��
                //    bOK = Transfer(out sErr);
                //    break;
                default:
                    break;
            }
            if (bOK)
            {
                MessageBox.Show("�����ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonClear_Click(null, null);
            }
            else
            {
                MessageBox.Show("����ʧ�ܣ�\r\n" + sErr, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private bool Apply(out string sErr)
        //{
        //    if (comboBoxDept.Text.Length == 0)
        //    {
        //        sErr = "SORRY�����ò��Ų���Ϊ�գ�";
        //        return false;
        //    }
        //    if (comboBoxMan.Text.Length == 0)
        //    {
        //        sErr = "SORRY�������˲���Ϊ�գ�";
        //        return false;
        //    }
        //    if (comboBoxAddr.Text.Length != 0)
        //    {
        //        if (DialogResult.OK !=
        //            MessageBox.Show("��ȷ��Ҫ����ʲ������ڵص���",
        //            "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
        //        {
        //            MessageBox.Show("���ڵص㲻��������գ�");
        //            sErr = "��ȡ��";
        //            return false;
        //        }
        //    }

        //    string sTyp = radioButtonApply.Checked == true ? "����" : "����";
        //    string sDept = comboBoxDept.Text;
        //    string sMan = comboBoxMan.Text;
        //    string sAddr = comboBoxAddr.Text;
        //    string sReason = textBoxReason.Text;
            
        //    List<string> listAssId = new List<string>();
        //    for( int i = 0; i< dataGridView1.RowCount; i++)
        //    {
        //        string sAssId = dataGridView1.Rows[i].Cells[1].Value.ToString();
        //        listAssId.Add(sAssId);
        //    }

        //    bool bOK = false;
        //    bOK = AssChange(sTyp,sDept, sMan, sAddr, sReason, out sErr,listAssId);

        //    return bOK;

        //}
        private bool Borrow(out string sErr)
        {
            if (comboBoxMan.Text.Length == 0)
            {
                sErr = "SORRY����Ա���ϲ���Ϊ�գ�";
                return false;
            }
            string sTyp = radioButtonBorrow.Checked == true ? "����" : "�黹";
            string sDept = comboBoxDept.Text;
            string sMan = comboBoxMan.Text;
            string sAddr = "";
            string sReason = textBoxReason.Text;

            //List<string> listAssId = new List<string>();
            //for (int i = 0; i < dataGridView1.RowCount; i++)
            //{
            //    string sAssId = dataGridView1.Rows[i].Cells[1].Value.ToString();
            //    listAssId.Add(sAssId);
            //}

            bool bOK = false;
            bOK = AssSupply.AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, dataGridView1);
            return bOK;
        }
        private bool Repair(out string sErr)
        {
            if (comboBoxMan.Text.Length == 0)
            {
                sErr = ("SORRY����Ա���ϲ���Ϊ�գ�");
                return false;
            }
            string sTyp = radioButtonStartRepair.Checked == true ? "����" : "�޷�";
            string sDept = comboBoxDept.Text;
            string sMan = comboBoxMan.Text;
            string sAddr = "";
            string sReason = textBoxReason.Text;

            //List<string> listAssId = new List<string>();
            //for (int i = 0; i < dataGridView1.RowCount; i++)
            //{
            //    string sAssId = dataGridView1.Rows[i].Cells[1].Value.ToString();
            //    listAssId.Add(sAssId);
            //}

            bool bOK = false;
            bOK = AssSupply.AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, dataGridView1);
            return bOK;
        }
        private bool TakeOut(out string sErr)
        {
            if (comboBoxMan.Text.Length == 0)
            {
                sErr = ("SORRY����Ա���ϲ���Ϊ�գ�");
                return false;
            }
            string sTyp = radioButtonOut.Checked == true ? "���" : "����";
            string sDept = comboBoxDept.Text;
            string sMan = comboBoxMan.Text;
            string sAddr = "";
            string sReason = textBoxReason.Text;

            //List<string> listAssId = new List<string>();
            //for (int i = 0; i < dataGridView1.RowCount; i++)
            //{
            //    string sAssId = dataGridView1.Rows[i].Cells[1].Value.ToString();
            //    listAssId.Add(sAssId);
            //}

            bool bOK = false;
            bOK = AssSupply.AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, dataGridView1);
            return bOK;
        }
        //private bool RentBack(out string sErr)
        //{
        //    if (comboBoxMan.Text.Length == 0)
        //    {
        //        sErr = ("SORRY����Ա���ϲ���Ϊ�գ�");
        //        return false;
        //    }
        //    string sTyp = "�⻹";
        //    string sDept = comboBoxDept.Text;
        //    string sMan = comboBoxMan.Text;
        //    string sAddr = comboBoxAddr.Text;
        //    string sReason = textBoxReason.Text;

        //    List<string> listAssId = new List<string>();
        //    for (int i = 0; i < dataGridView1.RowCount; i++)
        //    {
        //        string sAssId = dataGridView1.Rows[i].Cells[1].Value.ToString();
        //        listAssId.Add(sAssId);
        //    }

        //    bool bOK = false;
        //    bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, listAssId);
        //    return bOK;
        //}
        //private bool Rejiect(out string sErr)
        //{
        //    if (comboBoxMan.Text.Length == 0)
        //    {
        //        sErr = ("SORRY����Ա���ϲ���Ϊ�գ�");
        //        return false;
        //    }
        //    string sTyp = "�˷�";
        //    string sDept = comboBoxDept.Text;
        //    string sMan = comboBoxMan.Text;
        //    string sAddr = comboBoxAddr.Text;
        //    string sReason = textBoxReason.Text;

        //    List<string> listAssId = new List<string>();
        //    for (int i = 0; i < dataGridView1.RowCount; i++)
        //    {
        //        string sAssId = dataGridView1.Rows[i].Cells[1].Value.ToString();
        //        listAssId.Add(sAssId);
        //    }

        //    bool bOK = false;
        //    bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, listAssId);
        //    return bOK;
        //}
        //private bool Lose(out string sErr)
        //{
        //    if (comboBoxMan.Text.Length == 0)
        //    {
        //        sErr = ("SORRY����Ա���ϲ���Ϊ�գ�");
        //        return false;
        //    }
        //    string sTyp = "��ʧ";
        //    string sDept = comboBoxDept.Text;
        //    string sMan = comboBoxMan.Text;
        //    string sAddr = comboBoxAddr.Text;
        //    string sReason = textBoxReason.Text;

        //    List<string> listAssId = new List<string>();
        //    for (int i = 0; i < dataGridView1.RowCount; i++)
        //    {
        //        string sAssId = dataGridView1.Rows[i].Cells[1].Value.ToString();
        //        listAssId.Add(sAssId);
        //    }

        //    bool bOK = false;
        //    bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, listAssId);
        //    return bOK;
        //}
        //private bool Discard(out string sErr)
        //{
        //    if (comboBoxMan.Text.Length == 0)
        //    {
        //        sErr = ("SORRY����Ա���ϲ���Ϊ�գ�");
        //        return false;
        //    }
        //    string sTyp = "��ʧ";
        //    string sDept = comboBoxDept.Text;
        //    string sMan = comboBoxMan.Text;
        //    string sAddr = comboBoxAddr.Text;
        //    string sReason = textBoxReason.Text;

        //    List<string> listAssId = new List<string>();
        //    for (int i = 0; i < dataGridView1.RowCount; i++)
        //    {
        //        string sAssId = dataGridView1.Rows[i].Cells[1].Value.ToString();
        //        listAssId.Add(sAssId);
        //    }

        //    bool bOK = false;
        //    bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, listAssId);
        //    return bOK;
        //}
        //private bool Transfer(out string sErr)
        //{
        //    if (comboBoxMan.Text.Length == 0)
        //    {
        //        sErr = ("SORRY����Ա���ϲ���Ϊ�գ�");
        //        return false;
        //    }
        //    string sTyp = "ת��";
        //    string sDept = comboBoxDept.Text;
        //    string sMan = comboBoxMan.Text;
        //    string sAddr = comboBoxAddr.Text;
        //    string sReason = textBoxReason.Text;

        //    List<string> listAssId = new List<string>();
        //    for (int i = 0; i < dataGridView1.RowCount; i++)
        //    {
        //        string sAssId = dataGridView1.Rows[i].Cells[1].Value.ToString();
        //        listAssId.Add(sAssId);
        //    }

        //    bool bOK = false;
        //    bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, listAssId);
        //    return bOK;
        //}

        private void comboBoxDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DeptSelectChanged(comboBoxDept, comboBoxMan);
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            string sAssId = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //string sSQL = "select cre_tm ����ʱ��,opt_typ ��������,opt_man ʹ����Ա,cre_man ������ from ass_log where ass_id = '" + sAssId + "'";
            string strSql = "select cre_tm,opt_typ,opt_man,cre_man from ass_log where ass_id = '" + sAssId + "'";
            listBox1.Items.Clear();

            MySqlDataReader reader = MysqlHelper.ExecuteReader(strSql);
            while (reader.Read())
            {
                listBox1.Items.Add(reader["cre_tm"].ToString() + "   "
                        + reader["opt_typ"].ToString() + "��"
                        + reader["opt_man"].ToString() + "     "
                        + reader["cre_man"].ToString());
            }
            reader.Close();


        }

        private void radioButtonAppply_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1_SelectedIndexChanged(null, null);
        }
        private void radioButtonOut_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1_SelectedIndexChanged(null, null);
        }
        private void radioButtonBorrow_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1_SelectedIndexChanged(null, null);
        }
        private void radioButtonStartRepair_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1_SelectedIndexChanged(null, null);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabControl1.SelectedTab.Text == "����")
            //{
            //    textBoxReason.Enabled = true;
            //    labelAddr.Visible = true;
            //    comboBoxAddr.Visible = true;
            //    labelHit.Visible = true;
            //    if (radioButtonApply.Checked == true)
            //    {
            //        labelMan.Text = "������Ա��";
            //        labelReason.Text = "�������ɣ�";
            //    }
            //    else
            //    {
            //        labelMan.Text = "������Ա��";
            //        labelReason.Text = "����ԭ��";
            //    }
            //}
            //else
            if (tabControl1.SelectedTab.Text == "����")
            {
                //textBoxReason.Enabled = true;
               // labelAddr.Visible = false;
               // comboBoxAddr.Visible = false;
              //  labelHit.Visible = false;
                if (radioButtonBorrow.Checked == true)
                {
                    labelMan.Text = "������Ա��";
                    textBoxReason.Enabled = true;
                }
                else
                {
                    labelMan.Text = "�黹��Ա��";
                    textBoxReason.Enabled = false;
                }
            }
            else if (tabControl1.SelectedTab.Text == "ά��")
            {
                textBoxReason.Enabled = true;
               // labelAddr.Visible = false;
               // comboBoxAddr.Visible = false;
               // labelHit.Visible = false;
                if (radioButtonStartRepair.Checked == true)
                {
                    labelReason.Text = "����������";
                }
                else
                {
                    labelReason.Text = " ��ע��";
                }
            }
            else if (tabControl1.SelectedTab.Text == "���")
            {
                textBoxReason.Enabled = true;
             //   labelAddr.Visible = false;
             //   comboBoxAddr.Visible = false;
              //  labelHit.Visible = false;
                if (radioButtonOut.Checked == true)
                {
                    labelMan.Text = "�����Ա";
                    textBoxReason.Enabled = true;
                }
                else
                {
                    labelMan.Text = "������Ա";
                    textBoxReason.Enabled = false;
                }
            }
            //else if (tabControl1.SelectedTab.Text == "�⻹")
            //{
            //    //textBoxReason.Enabled = true;
            //    labelAddr.Visible = false;
            //    comboBoxAddr.Visible = false;
            //    labelHit.Visible = false;
            //    if (radioButtonBorrow.Checked == true)
            //    {
            //        labelMan.Text = "������Ա��";
            //        textBoxReason.Enabled = true;
            //    }
            //    else
            //    {
            //        labelMan.Text = "�黹��Ա��";
            //        textBoxReason.Enabled = false;
            //    }
            //}
            //else if (tabControl1.SelectedTab.Text == "�˻�")
            //{
            //    textBoxReason.Enabled = true;
            //    labelAddr.Visible = false;
            //    comboBoxAddr.Visible = false;
            //    labelHit.Visible = false;
            //    labelMan.Text = "ȷ����Ա��";
            //    labelReason.Text = "�˻�ԭ��";
            //}
            //else if (tabControl1.SelectedTab.Text == "��ʧ")
            //{
            //    textBoxReason.Enabled = true;
            //    labelAddr.Visible = false;
            //    comboBoxAddr.Visible = false;
            //    labelHit.Visible = false;
            //    labelMan.Text = "ȷ����Ա��";
            //    labelReason.Text = " ��ʧԭ��";
            //}
            //else if (tabControl1.SelectedTab.Text == "����")
            //{
            //    textBoxReason.Enabled = true;
            //    labelAddr.Visible = false;
            //    comboBoxAddr.Visible = false;
            //    labelHit.Visible = false;
            //    labelMan.Text = "ȷ����Ա��";
            //    labelReason.Text = "����ԭ��";
            //}
            //else if (tabControl1.SelectedTab.Text == "ת��")
            //{
            //    textBoxReason.Enabled = true;
            //    labelAddr.Visible = false;
            //    comboBoxAddr.Visible = false;
            //    labelHit.Visible = false;
            //    labelMan.Text = "ȷ����Ա��";
            //    labelReason.Text = "ת��ԭ��";
            //}
        }

        private void comboBoxMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //��ȡ�����б�
            comboBoxDept.Text = "";
            comboBoxDept.Items.Clear();

            string sSql = "select distinct dept_nam from emp where emp_nam = \'" + comboBoxMan.Text + "\' order by convert(dept_nam using gb2312) asc";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxDept.Items.Add(reader["dept_nam"].ToString());
            }
            reader.Close();
            if (comboBoxDept.Items.Count == 1)
            {
                comboBoxDept.SelectedIndex = 0;
            }
            else
            {
                comboBoxDept.Focus();
                comboBoxDept.DroppedDown = true;
            }
        }

        private void buttonQry_Click(object sender, EventArgs e)
        {
            List<string> list = AssSupply.FindPid(mf);
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    textBoxPid.Text = list[i];
                }
            }
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridViewTextBoxColumn dgv_Text = new DataGridViewTextBoxColumn();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //�к�
                int j = i + 1;
                dataGridView1.Rows[i].HeaderCell.Value = j.ToString();
                //��ɫ
                string sStat = dataGridView1.Rows[i].Cells["���״̬"].Value.ToString();
                if (sStat != "���" && sStat != "����")
                {
                    try
                    {
                        this.dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(0xFF0000);
                    }
                    catch (Exception ex)
                    {
                        // new FileOper().writelog(ex.Message);
                        System.Diagnostics.Trace.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
