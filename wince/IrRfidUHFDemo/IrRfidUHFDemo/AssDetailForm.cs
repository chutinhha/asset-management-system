using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data.SQLite;

namespace IrRfidUHFDemo
{
    public partial class AssDetailForm : Form
    {
        string sPid = "";
        string sAssId = "";
        public AssDetailForm(string sData)
        {
            if (sData.Length > 12)
            {
                this.sPid = sData.Substring(sData.Length-12,12);
            }
            else
            {
                this.sPid = sData;
            }
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AssDetailForm_Load(object sender, EventArgs e)
        { 
            //SQLite��ʽ
            string sSql = "select ass_id,fin_id,pid,tid,cat_no,ass_nam,ass_desc,ass_pri,reg_date,use_dept,use_man,use_co,duty_man,stat,stat_sub,typ,supplier,supplier_info,sn,vender,mfr_date,unit,num,ppu,company,addr,memo,cre_man,cre_tm,mod_man,mod_tm,flag,input_typ from ass_list where pid = \'" + sPid + "\'";
            SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql, null);
            if (reader.Read())
            {
                sAssId = reader["ass_id"].ToString();
                listBox1.Items.Add("�ʲ����룺" + sAssId);
                listBox1.Items.Add("������룺" + reader["fin_id"].ToString());
                listBox1.Items.Add("��ǩ���룺" + reader["pid"].ToString());
                
                listBox1.Items.Add("�ʲ����" + reader["typ"].ToString());
                listBox1.Items.Add("�ʲ����ƣ�" + reader["ass_nam"].ToString());
                listBox1.Items.Add("�ʲ�״̬��" + reader["stat"].ToString());
                listBox1.Items.Add("�ʲ���" + reader["ass_pri"].ToString());
                listBox1.Items.Add("�ʲ�������" + reader["ass_desc"].ToString());
                listBox1.Items.Add("�Ǽ����ڣ�" + reader["reg_date"].ToString());
                listBox1.Items.Add("������Ա��" + reader["use_man"].ToString());
                listBox1.Items.Add("�������ţ�" + reader["use_dept"].ToString());
                listBox1.Items.Add("���ڵص㣺" + reader["addr"].ToString());
                listBox1.Items.Add("������˾��" + reader["company"].ToString());
                listBox1.Items.Add("��״̬��" + reader["stat_sub"].ToString());
                listBox1.Items.Add("��Ӧ�̣�" + reader["supplier"].ToString());
                listBox1.Items.Add("��Ӧ����Ϣ��" + reader["supplier_info"].ToString());
                listBox1.Items.Add("Ʒ�ƣ�" + reader["vender"].ToString());
                listBox1.Items.Add("�������ͣ�" + reader["input_typ"].ToString());
                listBox1.Items.Add("TID��" + reader["tid"].ToString());
            }
            /////XML��ʽ
            //string sFilename = LoginForm.sCodePath + "\\ass_list.xml";
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(sFilename);
            //XmlNodeList xnl = xmlDoc.SelectSingleNode("ass_list").ChildNodes;
            //foreach (XmlNode xnf in xnl)
            //{
            //    XmlElement xe = (XmlElement)xnf;
            //    XmlNodeList xnf1 = xe.ChildNodes;
            //    //Ѱ��PID
            //    bool bFindPid = false; 
            //    foreach (XmlNode xn2 in xnf1)
            //    {
            //        if(xn2.Name == "pid")
            //        {
            //            if(xn2.InnerText == sPid)
            //            {       
            //                bFindPid = true;
            //            }
            //            break;
            //        }
            //    }//xn2
            //    if(bFindPid)
            //    {
            //        foreach (XmlNode xn2 in xnf1) 
            //        {
            //            listBox1.Items.Add(xn2.Name +  ": "+ xn2.InnerText);
            //        }
            //        break;
            //    }
            //}
        }

        private void AssDetailForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                buttonCancel_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                buttonHistory_Click(null, null);
            }
        }

        private void buttonHistory_Click(object sender, EventArgs e)
        {
            HistoryForm f = new HistoryForm(sAssId);
            f.ShowDialog();
        }
    }
}