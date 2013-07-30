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
    public partial class AssSupply : Form 
    {
        private BindingSource bs = new BindingSource();

        public static string sSQLSelect = "select Id ID,ass_id 资产编码,fin_id 财务编码,pid 标签喷码,typ 资产类型,ass_nam 资产名称,stat 库存状态,stat_sub 使用状态,duty_man 保管人员,dept 部门,ass_desc 备注,ass_pri 资产金额,reg_date 登记日期,addr 所在地点,use_co 所在公司,supplier 供应商,supplier_info 供应商信息,sn 序列号,vender 厂商品牌,input_date 购置日期,input_typ 购置类型,unit 单位,num 数量,ppu 单价,duty_man 责任人员,company 资产归属,cre_man 创建人员,cre_tm 创建时间,mod_man 修改人员,mod_tm 修改时间 from ass_list";
 
        
        MainForm mf;

        static List<string> aList = new List<string>();
        public AssSupply(MainForm f)
        {
            InitializeComponent();
            f.recvEvent += new MainForm.RecvEventHandler(this.RecvDataEvent);
            mf = f;
        }

        private void AssSupply_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            radioButtonApply.Checked = true;
            //radioButtonBorrow.Checked = true;
            //radioButtonStartRepair.Checked = true;
            //radioButtonOut.Checked = true;
            //获取资产信息表头
            string sSql = sSQLSelect + " where '1' = '0'";
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bs.DataSource = dt;
            bindingNavigator1.BindingSource = bs;
            dataGridView1.DataSource = bs;
            //获取部门列表
            sSql = "select distinct dept_nam from emp";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxDept.Items.Add(reader["dept_nam"].ToString());
            }
            reader.Close();
            //获取人员列表
            sSql = "select distinct emp_nam from emp order by convert(emp_nam using gb2312) asc";
            reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxMan.Items.Add(reader["emp_nam"].ToString());
            }
            reader.Close();
            //获取地址列表
            sSql = "select distinct addr_no from addr order by convert(addr_no using gb2312) asc";
            reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxAddr.Items.Add(reader["addr_no"].ToString());
            }
            reader.Close();
        }

        private void RecvDataEvent(object sender, RecvEventArgs e)
        {
            ShowPid(textBoxPid, e.SPid);
        }
        // 利用委托回调机制实现界面上消息内容显示
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
        private void GetAss(string sData, int nType) //0减少，1增加
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
            string sSql = sSQLSelect + " where pid in('0'";
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

        private void AssSupply_FormClosing(object sender, FormClosingEventArgs e)
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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // dt.Rows.Clear();
            textBoxPid.Text = "";
            aList.Clear();
            listBox1.Items.Clear();
            string sSql = sSQLSelect + " where '1' = '0'";
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bs.DataSource = dt;
            bindingNavigator1.BindingSource = bs;
            dataGridView1.DataSource = bs;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                GetAss(dataGridView1.CurrentRow.Cells[3].Value.ToString(), -1);
                listBox1.Items.Clear();
            }
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (aList.Count == 0)
            {
                MessageBox.Show("SORRY，无任何记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sErr = "";
            bool bOK = false;
            int nIndex = tabControl1.SelectedIndex;
            switch (nIndex)
            {
                case 0://领用
                    bOK = Apply(out sErr);
                    break;
                //case 1://借用
                //    bOK = Borrow(out sErr);
                //    break;
                //case 2://维修
                //    bOK = Repair(out sErr);
                //    break;
                //case 3://外出
                //    bOK = TakeOut(out sErr);
                //    break;
                //case 4://租还
                //    bOK = RentBack(out sErr);
                //    break;
                //case 5://退返
                //    bOK = Rejiect(out sErr);
                //    break;
                //case 6://丢失
                //    bOK = Lose(out sErr);
                //    break;
                //case 7://报废
                //    bOK = Discard(out sErr);
                //    break;
                //case 8://转出
                //    bOK = Transfer(out sErr);
                //    break;
                default:
                    break;
            }
            if (bOK)
            {
                MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonClear_Click(null, null);
            }
            else
            {
                MessageBox.Show("操作失败！\r\n" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool Apply(out string sErr)
        {
            if (comboBoxDept.Text.Length == 0)
            {
                sErr = "SORRY，领用部门不能为空！";
                return false;
            }
            if (comboBoxMan.Text.Length == 0)
            {
                sErr = "SORRY，领用人不能为空！";
                return false;
            }
            if (comboBoxAddr.Text.Length != 0)
            {
                if (DialogResult.OK !=
                    MessageBox.Show("您确定要变更资产的所在地点吗？",
                    "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    MessageBox.Show("所在地点不变更请留空！");
                    sErr = "已取消";
                    return false;
                }
            }

            string sTyp = radioButtonApply.Checked == true ? "领用" : "退领";
            string sDept = comboBoxDept.Text;
            string sMan = comboBoxMan.Text;
            string sAddr = comboBoxAddr.Text;
            string sReason = textBoxReason.Text;
            
            //List<string> listAssId = new List<string>();
            //for( int i = 0; i< dataGridView1.RowCount; i++)
            //{
            //    string sAssId = dataGridView1.Rows[i].Cells["资产编码"].Value.ToString();
            //    listAssId.Add(sAssId);
            //}

            bool bOK = false;
            bOK = AssSupply.AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, dataGridView1);

            return bOK;

        }
        //private bool Borrow(out string sErr)
        //{
        //    if (comboBoxMan.Text.Length == 0)
        //    {
        //        sErr = "SORRY，人员资料不能为空！";
        //        return false;
        //    }
        //    string sTyp = radioButtonBorrow.Checked == true ? "借用" : "归还";
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
        //private bool Repair(out string sErr)
        //{
        //    if (comboBoxMan.Text.Length == 0)
        //    {
        //        sErr = ("SORRY，人员资料不能为空！");
        //        return false;
        //    }
        //    string sTyp = radioButtonStartRepair.Checked == true ? "开始维修" : "结束维修";
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
        //private bool TakeOut(out string sErr)
        //{
        //    if (comboBoxMan.Text.Length == 0)
        //    {
        //        sErr = ("SORRY，人员资料不能为空！");
        //        return false;
        //    }
        //    string sTyp = radioButtonOut.Checked == true ? "外出" : "返回";
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
        //private bool RentBack(out string sErr)
        //{
        //    if (comboBoxMan.Text.Length == 0)
        //    {
        //        sErr = ("SORRY，人员资料不能为空！");
        //        return false;
        //    }
        //    string sTyp = "租还";
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
        //        sErr = ("SORRY，人员资料不能为空！");
        //        return false;
        //    }
        //    string sTyp = "退返";
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
        //        sErr = ("SORRY，人员资料不能为空！");
        //        return false;
        //    }
        //    string sTyp = "丢失";
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
        //        sErr = ("SORRY，人员资料不能为空！");
        //        return false;
        //    }
        //    string sTyp = "丢失";
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
        //        sErr = ("SORRY，人员资料不能为空！");
        //        return false;
        //    }
        //    string sTyp = "转出";
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
        static public bool CheckStat(string sNewOpt,string sStat, string sStatSub, out string sErr)
        {
            sErr = "";
            //判断资产是否有效
            if (sStat != "领用" && sStat != "库存")
            {
                sErr = "该资产已" + sStat;
                return false;
            }

            if (sNewOpt.Equals("领用"))
            {
                //库存状态下才可以领用
                if (sStat != "库存")
                {
                    sErr = "该资产已领用";
                    return false;
                }
            }
            else if (sNewOpt.Equals("退领"))
            {
                //领用状态下才可以退领
                if (sStat != "领用")
                {
                    sErr = "必须是领用状态";
                    return false;
                }
            }
            else if (sNewOpt.Equals("借用"))
            {
                if (sStatSub.Length != 0 && sStatSub != "归还" && sStatSub != "返回" && sStatSub != "修返")
                {
                    sErr = "该资产正处于" + sStatSub + "状态";
                    return false;
                }
            }
            else if (sNewOpt.Equals("归还"))
            {
                //借用状态下才可以归还
                if (sStatSub != "借用")
                {
                    sErr = "必须是借用状态";
                    return false;
                }
            }
            else if (sNewOpt.Equals("送修"))
            {
                //领用或库存状态下才可以 送修
                if (sStatSub.Length != 0 && sStatSub != "归还" && sStatSub != "返回" && sStatSub != "修返")
                {
                    sErr = "该资产正处于" + sStatSub + "状态";
                    return false;
                }
            }
            else if (sNewOpt.Equals("修返"))
            {
                //领用或库存状态下才可以 返修
                if (sStatSub != "送修")
                {
                    sErr = "必须是送修状态";
                    return false;
                }
            }
            else if (sNewOpt.Equals("外出"))
            {
                //领用或库存状态下才可以 外出
                if (sStatSub.Length != 0 && sStatSub != "归还" && sStatSub != "返回" && sStatSub != "修返")
                {
                    sErr = "该资产正处于" + sStatSub + "状态";
                    return false;
                }
            }
            else if (sNewOpt.Equals("返回"))
            {
                //外出状态下才可以 返回
                if (sStatSub != "返回")
                {
                    sErr = "必须是返回状态";
                    return false;
                }
            }
            else if (sNewOpt.Equals("租还"))
            {
                //领用或库存状态下才可以 租还
            }
            else if (sNewOpt.Equals("退返"))
            {
                //领用或库存状态下才可以 退返
            }
            else if (sNewOpt.Equals("丢失"))
            {
                //领用或库存状态下才可以 丢失
            }
            else if (sNewOpt.Equals("报废"))
            {
                //领用或库存状态下才可以 报废
            }
            else if (sNewOpt.Equals("转出"))
            {
                //领用或库存状态下才可以 转出
            }
            return true;
        }
        static public bool AssChange(string sTyp, string sDept, string sMan, string sAddr, string sReason, out string sErr, DataGridView dgv)
        {
            sErr = "";
            List<string> listSql = new List<string>();
            List<string> listSqlLog = new List<string>();
            List<string> listAssId = new List<string>();
            string sUpd;
            if (sTyp == "领用")// )
            {
                sUpd = string.Format("update ass_list set stat = '领用',duty_man = '{0}',dept = '{1}',mod_man = '{2}',mod_tm = '{3}'", sMan, sDept, MainForm.sUserName, MainForm.getDateTime());
                if (sAddr.Length != 0)
                {
                    sUpd += ",addr = '" + sAddr + "' ";
                }
            }
            else if (sTyp == "退领")
            {
                sUpd = string.Format("update ass_list set stat = '库存',mod_man = '{0}',mod_tm = '{1}'", MainForm.sUserName, MainForm.getDateTime());
            }
            else if (sTyp == "租还" || sTyp == "退返" || sTyp == "丢失" || sTyp == "报废" || sTyp == "转出")
            {
                sUpd = string.Format("update ass_list set stat = '{0}',mod_man = '{1}',mod_tm = '{2}'", sTyp, sMan, MainForm.getDateTime());
            }
            else//借用，归还，送修，修返，外出，返回
            {
                sUpd = string.Format("update ass_list set stat_sub = '{0}',duty_man = '{1}',dept = '{2}',mod_tm = '{3}'", sTyp, sMan, sDept, MainForm.getDateTime());
            }

            string sSql = "";
         //   foreach (string sAssId in listAssid)
            bool bOK = true;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                string sAssId = dgv.Rows[i].Cells["资产编码"].Value.ToString();
                string sStat = dgv.Rows[i].Cells["库存状态"].Value.ToString();
                string sStatSub = dgv.Rows[i].Cells["使用状态"].Value.ToString();
                
                if (sAssId.Length == 0) continue;

                //验证逻辑
                if (!AssSupply.CheckStat(sTyp, sStat, sStatSub, out sErr))
                {
                    sErr = sErr + ",序号：" + (i+1) + "\r\n资产编码：" + sAssId;
                    bOK = false;
                    break;
                }
                
                //更新
                sSql = sUpd + " where ass_id = '" + sAssId + "' and ynenable = 'Y'";
                listSql.Add(sSql);
                listAssId.Add(sAssId);
                //插入
                sSql = string.Format("insert into ass_log(ass_id,opt_typ,opt_man,opt_date,cre_man,cre_tm,company,dept,reason,addr) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                sAssId, sTyp, sMan, MainForm.getDate(), MainForm.sUserName, MainForm.getDateTime(), MainForm.sCompany, sDept, sReason, sAddr);
                listSql.Add(sSql);
                listAssId.Add(sAssId);
            }
            if (!bOK)
            {
                listSql.Clear();
                listAssId.Clear();
                return false;
            }
            //同步SQL
            int nCnt = listSql.Count;
            for (int i = 0; i < nCnt; i++)
            {
                string sAssId = listAssId[i];
                sSql = listSql[i];
                string sSqlLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
                sTyp, "0", sSql.Replace("'", "''"), MainForm.sClientId, sAssId, MainForm.getDateTime());
                listSql.Add(sSqlLog);
            }

            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            sErr = MysqlHelper.sLastErr;
            return bOK;
        }

        private void DeptSelectChanged(ComboBox dept, ComboBox emp)
        {
            //获取人员列表
            emp.Text = "";
            emp.Items.Clear();
            string sSql = "select emp_nam from emp where dept_nam = \'" + dept.Text + "\'";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                emp.Items.Add(reader["emp_nam"].ToString());
            }

            if (emp.Items.Count == 1)
            {
                emp.SelectedIndex = 0;
            }
            else
            {
                emp.Focus();
                emp.DroppedDown = true;
            }
        }

        private void comboBoxDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DeptSelectChanged(comboBoxDept, comboBoxMan);
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            string sAssId = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //string sSQL = "select cre_tm 操作时间,opt_typ 操作类型,opt_man 使用人员,cre_man 经手人 from ass_log where ass_id = '" + sAssId + "'";
            string strSql = "select cre_tm,opt_typ,opt_man,cre_man from ass_log where ass_id = '" + sAssId + "'";
            listBox1.Items.Clear();

            MySqlDataReader reader = MysqlHelper.ExecuteReader(strSql);
            while (reader.Read())
            {
                listBox1.Items.Add(reader["cre_tm"].ToString() + "   "
                        + reader["opt_typ"].ToString() + "，"
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
            //if (tabControl1.SelectedTab.Text == "领用")
            //{
            //    textBoxReason.Enabled = true;
            //    labelAddr.Visible = true;
            //    comboBoxAddr.Visible = true;
            //    labelHit.Visible = true;
            //    if (radioButtonApply.Checked == true)
            //    {
            //        labelMan.Text = "领用人员：";
            //        labelReason.Text = "领用事由：";
            //    }
            //    else
            //    {
            //        labelMan.Text = "退领人员：";
            //        labelReason.Text = "退领原因：";
            //    }
            //}
            //else if (tabControl1.SelectedTab.Text == "借用")
            //{
            //    //textBoxReason.Enabled = true;
            //    labelAddr.Visible = false;
            //    comboBoxAddr.Visible = false;
            //    labelHit.Visible = false;
            //    if (radioButtonBorrow.Checked == true)
            //    {
            //        labelMan.Text = "借用人员：";
            //        textBoxReason.Enabled = true;
            //    }
            //    else
            //    {
            //        labelMan.Text = "归还人员：";
            //        textBoxReason.Enabled = false;
            //    }
            //}
            //else if (tabControl1.SelectedTab.Text == "维修")
            //{
            //    textBoxReason.Enabled = true;
            //    labelAddr.Visible = false;
            //    comboBoxAddr.Visible = false;
            //    labelHit.Visible = false;
            //    if (radioButtonStartRepair.Checked == true)
            //    {
            //        labelReason.Text = "故障描述：";
            //    }
            //    else
            //    {
            //        labelReason.Text = " 备注：";
            //    }
            //}
            //else if (tabControl1.SelectedTab.Text == "外出")
            //{
            //    textBoxReason.Enabled = true;
            //    labelAddr.Visible = false;
            //    comboBoxAddr.Visible = false;
            //    labelHit.Visible = false;
            //    if (radioButtonOut.Checked == true)
            //    {
            //        labelMan.Text = "外出人员";
            //        textBoxReason.Enabled = true;
            //    }
            //    else
            //    {
            //        labelMan.Text = "返回人员";
            //        textBoxReason.Enabled = false;
            //    }
            //}
            //else if (tabControl1.SelectedTab.Text == "租还")
            //{
            //    //textBoxReason.Enabled = true;
            //    labelAddr.Visible = false;
            //    comboBoxAddr.Visible = false;
            //    labelHit.Visible = false;
            //    if (radioButtonBorrow.Checked == true)
            //    {
            //        labelMan.Text = "借用人员：";
            //        textBoxReason.Enabled = true;
            //    }
            //    else
            //    {
            //        labelMan.Text = "归还人员：";
            //        textBoxReason.Enabled = false;
            //    }
            //}
            //else if (tabControl1.SelectedTab.Text == "退还")
            //{
            //    textBoxReason.Enabled = true;
            //    labelAddr.Visible = false;
            //    comboBoxAddr.Visible = false;
            //    labelHit.Visible = false;
            //    labelMan.Text = "确认人员：";
            //    labelReason.Text = "退还原因：";
            //}
            //else if (tabControl1.SelectedTab.Text == "丢失")
            //{
            //    textBoxReason.Enabled = true;
            //    labelAddr.Visible = false;
            //    comboBoxAddr.Visible = false;
            //    labelHit.Visible = false;
            //    labelMan.Text = "确认人员：";
            //    labelReason.Text = " 丢失原因：";
            //}
            //else if (tabControl1.SelectedTab.Text == "报废")
            //{
            //    textBoxReason.Enabled = true;
            //    labelAddr.Visible = false;
            //    comboBoxAddr.Visible = false;
            //    labelHit.Visible = false;
            //    labelMan.Text = "确认人员：";
            //    labelReason.Text = "报废原因：";
            //}
            //else if (tabControl1.SelectedTab.Text == "转出")
            //{
            //    textBoxReason.Enabled = true;
            //    labelAddr.Visible = false;
            //    comboBoxAddr.Visible = false;
            //    labelHit.Visible = false;
            //    labelMan.Text = "确认人员：";
            //    labelReason.Text = "转出原因：";
            //}
        }

        private void comboBoxMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取部门列表
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
    }
}
