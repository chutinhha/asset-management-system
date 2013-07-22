using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using System.Data.SQLite;

namespace IrRfidUHFDemo
{
    public partial class GetReasonForm : Form
    {
        public string sDept;
        public string sEmpNam;
        public string sReason;
        public string sAddr;
        public int ret = 0;
        public int nIndex = 0;
        public List<string> listDept = new List<string>();
        public string sTyp;
        public GetReasonForm(string sTyp)
        {
            InitializeComponent();
            this.sTyp = sTyp;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            ret = 0;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            sDept = textBoxDept.Text;
            sEmpNam = comboBoxEmpNam.Text;
            sReason = comboBoxReason.Text;
            sAddr = "";
            ret = 1;
            this.Close();
        }
        private void buttonReturn_Click(object sender, EventArgs e)
        {
            sDept = textBoxDept.Text;
            sEmpNam = comboBoxEmpNam.Text;
            sReason = comboBoxReason.Text;
            sAddr = "";
            ret = 2;
            this.Close();
        }
        private void GetReasonForm_Load(object sender, EventArgs e)
        {
            if (sTyp == "领用")
            {
                buttonOK.Text = "领用(F1)";
                buttonReturn.Text = "退领(Ent)";
                buttonReturn.Visible = true;
            }
            else if (sTyp == "借用")
            {
                buttonOK.Text = "借用(F1)";
                buttonReturn.Text = "归还(Ent)";
                buttonReturn.Visible = true;
            }
            else if (sTyp == "维修")
            {
                buttonOK.Text = "送修(F1)";
                buttonReturn.Text = "修返(Ent)";
                buttonReturn.Visible = true;
            }
            else if (sTyp == "外出")
            {
                buttonOK.Text = "外出(F1)";
                buttonReturn.Text = "返回(Ent)";
                buttonReturn.Visible = true;
            }
            else if (sTyp == "租还")
            {
                buttonReturn.Text = "租还(Ent)";
                buttonOK.Visible = false;
            }
            else if (sTyp == "退返")
            {
                buttonReturn.Text = "退返(Ent)";
                buttonOK.Visible = false;
            }
            else if (sTyp == "报废")
            {
                buttonReturn.Text = "报废(Ent)";
                buttonOK.Visible = false;
            }
            else if (sTyp == "报失")
            {
                buttonReturn.Text = "报失(Ent)";
                buttonOK.Visible = false;
            }
            else if (sTyp == "转出")
            {
                buttonReturn.Text = "转出(Ent)";
                buttonOK.Visible = false;
            }
            //获取人员列表
            string sSql = "select distinct emp_nam from emp order by emp_nam asc";
            SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql,null);
            while (reader.Read())
            {
                comboBoxEmpNam.Items.Add(reader["emp_nam"].ToString());
            }
            //try
            //{
            //    string sFilename = LoginForm.sCodePath + "\\emp.xml";
            //    XmlDocument xmlDoc = new XmlDocument();
            //    xmlDoc.Load(sFilename);
            //    XmlNodeList xnl = xmlDoc.SelectSingleNode("emp").ChildNodes;
            //    foreach (XmlNode xnf in xnl)
            //    {
            //        XmlElement xe = (XmlElement)xnf;
            //        XmlNodeList xnf1 = xe.ChildNodes;
            //        foreach (XmlNode xn2 in xnf1)
            //        {
            //            if (xn2.Name == "emp_nam")
            //            {
            //                AddComboBoxData(comboBoxEmpNam, xn2.InnerText);
            //            }
            //            //else if (xn2.Name == "dept_nam")
            //            //{
            //            //    AddComboBoxData(comboBoxDept, xn2.InnerText);
            //            //}
            //        }//xn2
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //}
        }
        //private void AddComboBoxData(ComboBox comboBoxOData, string sData)
        //{
        //    bool bFind = false;
        //    for (int i = 0; i < comboBoxOData.Items.Count; i++)
        //    {
        //        if (sData.Equals(comboBoxOData.Items[i].ToString()))
        //        {
        //            bFind = true;
        //        }
        //    }
        //    if (!bFind)
        //    {
        //        comboBoxOData.Items.Add(sData);
        //    }
        //}

        private void comboBoxEmpNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            listDept.Clear();
            nIndex = 0;
            string sSql = "select distinct dept_nam from emp where emp_nam = '" + comboBoxEmpNam.Text + "' order by dept_nam asc";
            SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql, null);
            while (reader.Read())
            {
                listDept.Add(reader["dept_nam"].ToString());
            }
            //try
            //{
            //string sFilename = LoginForm.sCodePath + "\\emp.xml";
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(sFilename);
            //XmlNodeList xnl = xmlDoc.SelectSingleNode("emp").ChildNodes;
            //foreach (XmlNode xnf in xnl)
            //{
            //    XmlElement xe = (XmlElement)xnf;
            //    XmlNodeList xnf1 = xe.ChildNodes;
            //    bool bFind = false;
            //    foreach (XmlNode xn2 in xnf1)
            //    {
            //        if (xn2.Name == "emp_nam" && xn2.InnerText == comboBoxEmpNam.Text)
            //        {
            //            bFind = true;
            //            break;
            //        }
            //    }//xn2
            //    if(bFind)
            //    {
            //        foreach (XmlNode xn2 in xnf1)
            //        {
            //            if (xn2.Name == "dept_nam")
            //            {
            //                listDept.Add(xn2.InnerText);
            //                break;
            //            }
            //        }//xn2
            //    } 
            //}
            if (listDept.Count > 0)
            {
                textBoxDept.Text = listDept[0];
                 nIndex = 0;
                 labelHit.Text = string.Format("*多部门请选择!({0}/{1})", nIndex + 1, listDept.Count);
            }
           if (listDept.Count > 1)
           {
               buttonPri.Enabled =false;
               buttonNext.Enabled = true;
               labelHit.Visible = true;
           }
           else
           {
               buttonPri.Enabled =false;
               buttonNext.Enabled =false;
               labelHit.Visible = false;
           }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //}
        }

        private void buttonPri_Click(object sender, EventArgs e)
        {
            nIndex--;
            if(nIndex >= 0 && nIndex < listDept.Count)
            {
                textBoxDept.Text = listDept[nIndex];
                labelHit.Text = string.Format("*多部门请选择!({0}/{1})", nIndex + 1, listDept.Count);
            }
            if(nIndex <= 0)
            {
                buttonPri.Enabled =false;
                nIndex = 0;
            }
            buttonNext.Enabled =true;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            nIndex++;
            if(nIndex >= 0 && nIndex < listDept.Count)
            {
                textBoxDept.Text = listDept[nIndex];
                labelHit.Text = string.Format("*多部门请选择!({0}/{1})" , nIndex+1 , listDept.Count);
            }
            if(nIndex >= listDept.Count - 1)
            {
                 buttonNext.Enabled =false;
                 nIndex = listDept.Count - 1;
            }
            buttonPri.Enabled =true;
        }

        private void GetReasonForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                buttonCancel_Click(null, null);
            }
            else if (e.KeyCode == Keys.F1)
            {
                buttonOK_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                buttonReturn_Click(null, null);
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (buttonNext.Enabled == true)
                {
                    buttonNext_Click(null, null);
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (buttonPri.Enabled == true)
                {
                    buttonPri_Click(null, null);
                }
            }
        }
    }
}