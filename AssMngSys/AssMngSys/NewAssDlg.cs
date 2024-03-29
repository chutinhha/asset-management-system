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
        public string sOptType = "新增";//新增，删除，修改
        public bool bDone = false;
        MainForm mf;

        private string sAssIdCur;
        private string sPidCur;
        public NewAssDlg(MainForm f, DataGridView dv)
        {
            InitializeComponent();
            mf = f;
            mf.recvEvent += new MainForm.RecvEventHandler(this.RecvDataEvent);
            dataGridView1 = dv;
        }
        private void RecvDataEvent(object sender, RecvEventArgs e)
        {
            // textBoxPid.Text = e.SPid;//线程间操作无效: 从不是创建控件“textBoxPid”的线程访问它。
            ShowPid(textBoxPid, e.SPid);
            ShowPid(textBoxTid, e.STid);
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
        private void AddAssDlg_Load(object sender, EventArgs e)
        {
            textBoxAssPri.Text = "0.00";
            textBoxPpu.Text = "0.00";
            textBoxNum.Text = "1";
            comboBoxUnit.Items.Add("台");
            comboBoxUnit.Items.Add("张");
            comboBoxUnit.Items.Add("个");
            comboBoxUnit.Items.Add("辆");
            comboBoxUnit.Items.Add("件");
            comboBoxUnit.Items.Add("箱");

            comboBoxInputTyp.Items.Add("采购");
            comboBoxInputTyp.Items.Add("自建");
            comboBoxInputTyp.Items.Add("租入");
            comboBoxInputTyp.Items.Add("转入");
            comboBoxInputTyp.Items.Add("其他");

            comboBoxUseCo.Items.Add("深圳总公司");
            comboBoxUseCo.Items.Add("广州分公司");
            comboBoxUseCo.Items.Add("");

            this.Text = string.Format("资产{0}", sOptType);

            if (sOptType == "修改" || sOptType == "复制")
            {
                if (sOptType == "复制")
                {
                    textBoxPid.Text = getNewPid();
                   // textBoxTid.Text = "";
                }
                else
                {
                    sPidCur = dataGridView1.SelectedRows[0].Cells["标签喷码"].Value.ToString();
                    textBoxPid.Text = sPidCur;
                   // textBoxTid.Text = dataGridView1.SelectedRows[0].Cells["标签ID"].Value.ToString();
                }
                sAssIdCur = dataGridView1.SelectedRows[0].Cells["资产编码"].Value.ToString();
                textBoxAssId.Text = sAssIdCur;
                textBoxFinId.Text = dataGridView1.SelectedRows[0].Cells["财务编码"].Value.ToString();
                comboBoxTyp.Text = dataGridView1.SelectedRows[0].Cells["类型"].Value.ToString();
                comboBoxAssNam.Text = dataGridView1.SelectedRows[0].Cells["资产名称"].Value.ToString();

                textBoxNum.Text = dataGridView1.SelectedRows[0].Cells["数量"].Value.ToString(); 
                textBoxPpu.Text = dataGridView1.SelectedRows[0].Cells["单价"].Value.ToString();
                textBoxAssPri.Text = dataGridView1.SelectedRows[0].Cells["资产金额"].Value.ToString();

                textBoxSupplier.Text = dataGridView1.SelectedRows[0].Cells["供应商"].Value.ToString();
                textBoxSupplierInfo.Text = dataGridView1.SelectedRows[0].Cells["供应商信息"].Value.ToString();
                textBoxVender.Text = dataGridView1.SelectedRows[0].Cells["厂商品牌"].Value.ToString();
                textBoxInputDate.Text = dataGridView1.SelectedRows[0].Cells["购置日期"].Value.ToString();
                comboBoxUnit.Text = dataGridView1.SelectedRows[0].Cells["单位"].Value.ToString();
                textBoxSn.Text = dataGridView1.SelectedRows[0].Cells["序列号"].Value.ToString();
                textBoxAssDesc.Text = dataGridView1.SelectedRows[0].Cells["资产描述"].Value.ToString();
                textBoxId.Text = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
                comboBoxInputTyp.Text = dataGridView1.SelectedRows[0].Cells["购置类型"].Value.ToString();
                comboBoxAddr.Text = dataGridView1.SelectedRows[0].Cells["所在地点"].Value.ToString();
                comboBoxStat.Text = dataGridView1.SelectedRows[0].Cells["库存状态"].Value.ToString();
                comboBoxStatSub.Text = dataGridView1.SelectedRows[0].Cells["使用状态"].Value.ToString();
                comboBoxDept.Text = dataGridView1.SelectedRows[0].Cells["所属部门"].Value.ToString();
                comboBoxDutyMan.Text = dataGridView1.SelectedRows[0].Cells["保管人员"].Value.ToString();
                comboBoxUseMan.Text = dataGridView1.SelectedRows[0].Cells["领用人员"].Value.ToString();
                textBoxDevMode.Text = dataGridView1.SelectedRows[0].Cells["设备型号"].Value.ToString();
                comboBoxYnRepair.Text = dataGridView1.SelectedRows[0].Cells["是否维修过"].Value.ToString();

                textBoxMemo.Text = dataGridView1.SelectedRows[0].Cells["备注"].Value.ToString();
                comboBoxUseCo.Text = dataGridView1.SelectedRows[0].Cells["资产归属"].Value.ToString();
            }
            else//新增
            {
                textBoxPid.Text = getNewPid();
            }
            //  pictureBox1.Image = BarCode.BuildBarCode(textBoxPid.Text);
            //IP提示
            //   IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
            //   IPAddress ipa = ipe.AddressList[0];
            //     groupBox2.Text = string.Format("电子标签信息(请用手持机扫描，本地IP：{0})", ipa.ToString());
            //获取类别列表
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
            //获取部门列表
            sSql = "select distinct dept_nam from emp";
            reader = MysqlHelper.ExecuteReader(sSql);
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
                comboBoxDutyMan.Items.Add(reader["emp_nam"].ToString());
            }
            reader.Close();
            //获取地点列表
            sSql = "select distinct addr_no from addr order by convert(addr_no using gb2312) asc";
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
            if (reader.Read())
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
            if (sOptType == "新增" || sOptType == "复制")
            {
                insertData();
            }
            else if (sOptType == "修改")
            {
                ModifyData();
            }
        }
        private void insertData()
        {
            if (textBoxAssId.Text.Equals(""))
            {
                MessageBox.Show("资产编码不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sSql = "select 'X' from ass_list where ass_id = '" + textBoxAssId.Text + "' and ynenable = 'Y'";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            if (reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("资产编码已存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("该标签已绑定资产:" + sAssid + "\r\n如需继续，请先清空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    return;
                }
                reader.Close();
            }

            if (comboBoxTyp.Text.Length == 0)
            {
                MessageBox.Show("请选择资产类型!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxAssNam.Text.Length == 0)
            {
                MessageBox.Show("请选择资产名称!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> listSql = new List<string>();
            sSql = string.Format(@"insert into ass_list
                (cre_tm,ass_id,fin_id,pid,tid,ass_nam,ass_desc,ass_pri,reg_date,typ,
                supplier,sn,vender,input_date,unit,num,ppu,company,cre_man,supplier_info,input_typ,addr,dept,duty_man,stat,stat_sub,dev_mode,use_man,ynrepair,memo,use_co) 
                values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',
                '{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}')",
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
                textBoxInputDate.Text,
                comboBoxUnit.Text,
                textBoxNum.Text,
                textBoxPpu.Text,
                Login.sCompany, // company
                Login.sUserName,//cre_man
                textBoxSupplierInfo.Text,
                comboBoxInputTyp.Text,
                comboBoxAddr.Text,
                comboBoxDept.Text,
                comboBoxDutyMan.Text,
                comboBoxStat.Text,
                comboBoxStatSub.Text,
                textBoxDevMode.Text,
                comboBoxUseMan.Text,
                comboBoxYnRepair.Text,
                textBoxMemo.Text,
                comboBoxUseCo.Text
                );
            listSql.Add(sSql);
            //插入日志
            sSql = string.Format("insert into ass_log(ass_id,opt_typ,opt_man,opt_date,cre_man,cre_tm,company,dept,reason,addr,pid) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                       textBoxAssId.Text, "新增", Login.sUserName, MainForm.getDate(), Login.sUserName, MainForm.getDateTime(), Login.sCompany, "", "", "",textBoxPid.Text);
            listSql.Add(sSql);

            //同步SQL
            int nCnt = listSql.Count;
            for (int i = 0; i < nCnt; i++)
            {
                string sAssId = textBoxAssId.Text;
                sSql = listSql[i];
                string sSqlLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
                "新增", "0", sSql.Replace("'", "''"), Login.sClientId, sAssId, MainForm.getDateTime());
                listSql.Add(sSqlLog);
            }
            bool bOK = false;
            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("新增成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bDone = true;
                this.Close();
                
            }
            else
            {
                MessageBox.Show("新增失败！\r\n" + MysqlHelper.sLastErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ModifyData()
        {
            string sSql = "";
            if (textBoxAssId.Text.Equals(""))
            {
                MessageBox.Show("资产编码不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxAssId.Text != sAssIdCur)
            {
                sSql = "select 'X' from ass_list where ass_id = '" + textBoxAssId.Text + "' and ynenable = 'Y'";
                MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
                if (reader.HasRows)
                {
                    reader.Close();
                    MessageBox.Show("资产编码已存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                reader.Close();
            }
            if (textBoxPid.Text.Length != 0 && textBoxPid.Text != sPidCur)
            {
                sSql = "select ass_id from ass_list where pid = '" + textBoxPid.Text + "' and ynenable = 'Y'";
                MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
                if (reader.Read())
                {
                    string sAssid = reader["ass_id"].ToString();
                    MessageBox.Show("该标签已绑定资产:" + sAssid + "\r\n如需继续，请先清空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    return;
                }
                reader.Close();
            }
            List<string> listSql = new List<string>();
            sSql = string.Format(@"update ass_list set fin_id = '{0}',ass_id = '{1}', ass_nam = '{2}', ass_desc = '{3}', ass_pri = '{4}', mod_tm = '{5}',
typ = '{6}', supplier = '{7}', supplier_info = '{8}', sn = '{9}',vender = '{10}', input_date = '{11}', unit = '{12}', 
num = '{13}', ppu = '{14}',memo = '{15}', mod_man = '{16}',input_typ = '{17}',addr = '{18}',pid = '{19}',tid = '{20}',dept ='{21}',duty_man ='{22}',stat ='{23}',stat_sub ='{24}',dev_mode = '{25}',use_man = '{26}',ynrepair = '{27}',use_co = '{28}' where id = '{29}'",
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
                    textBoxInputDate.Text,
                    comboBoxUnit.Text,
                    textBoxNum.Text,
                    textBoxPpu.Text,
                    textBoxMemo.Text,//memo
                    Login.sUserName,//mod_man 
                    comboBoxInputTyp.Text,
                    comboBoxAddr.Text,
                    textBoxPid.Text,
                    textBoxTid.Text,
                    comboBoxDept.Text,
                    comboBoxDutyMan.Text,
                    comboBoxStat.Text,
                    comboBoxStatSub.Text,
                    textBoxDevMode.Text,
                    comboBoxUseMan.Text,
                    comboBoxYnRepair.Text,
                    comboBoxUseCo.Text,
                    textBoxId.Text);
            listSql.Add(sSql);

            //插入日志
            sSql = string.Format("insert into ass_log(ass_id,opt_typ,opt_man,opt_date,cre_man,cre_tm,company,dept,reason,addr,pid) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                       textBoxAssId.Text, "修改", Login.sUserName, MainForm.getDate(), Login.sUserName, MainForm.getDateTime(), Login.sCompany, "", "", "",textBoxPid.Text);
            listSql.Add(sSql);


            //同步SQL
            int nCnt = listSql.Count;
            for (int i = 0; i < nCnt; i++)
            {
                string sAssId = textBoxAssId.Text;
                sSql = listSql[i];
                string sSqlLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
                "修改", "0", sSql.Replace("'", "''"), Login.sClientId, sAssId, MainForm.getDateTime());
                listSql.Add(sSqlLog);
            }


            bool bOK = false;
            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("修改成功！");
                bDone = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("修改失败！\r\n" + MysqlHelper.sLastErr);
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
            comboBoxAssNam.Text = "";
            string sSql = "select distinct prd_nam from ass_cat where cat_nam = '" + str + "'order by convert(prd_nam using gb2312) asc";
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

        private void textBoxPpu_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPpu.Text.Length == 0)
                textBoxPpu.Text = "0.00";

           textBoxAssPri.Text = string.Format("{0:0.00}", Convert.ToDouble(textBoxPpu.Text) * Convert.ToInt32(textBoxNum.Text));
        }

        private void textBoxNum_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNum.Text.Length == 0)
                textBoxNum.Text = "0";
            textBoxAssPri.Text = string.Format("{0:0.00}", Convert.ToDouble(textBoxPpu.Text) * Convert.ToInt32(textBoxNum.Text));
        }

        private void textBoxNum_KeyPress(object sender, KeyPressEventArgs e)
        {
             //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar!=(char)13 && e.KeyChar!=(char)8)
            {
                e.Handled = true;
            } 
        }
    }
}