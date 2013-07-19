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

        private void AssInput_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.ass_list' table. You can move, or remove it, as needed.
            this.ShowInTaskbar = false;
            textBoxAssPri.Text = "0.00";
            textBoxPpu.Text = "0.00";
            textBoxNum.Text = "1";
            comboBoxUnit.Items.Add("台");
            comboBoxUnit.Items.Add("张");
            comboBoxUnit.Items.Add("个");
            comboBoxUnit.Items.Add("辆");
            comboBoxUnit.Items.Add("件");
            comboBoxUnit.Items.Add("箱");

            //comboBoxAddr.Items.Add("深圳总公司");
            //comboBoxAddr.Items.Add("广州分公司");
            //comboBoxAddr.Items.Add("杭州办事处");
            //comboBoxAddr.Items.Add("北京分公司");
            //comboBoxAddr.Items.Add("武汉办事处");
            //comboBoxAddr.Items.Add("维修网点");

            //comboBoxStat.Items.Add("入库");
            //comboBoxStat.Items.Add("领用");
            //comboBoxStat.Items.Add("借用");
            //comboBoxStat.Items.Add("外出");


            comboBoxInputTyp.Items.Add("采购");
            comboBoxInputTyp.Items.Add("自建");
            comboBoxInputTyp.Items.Add("租入");
            comboBoxInputTyp.Items.Add("转入");
            comboBoxInputTyp.Items.Add("其他");

            radioButtonNew.Checked = true;
            radioButtonModify.Checked = false;
            radioButtonQuery.Checked = false;

            sSQLSelect = "select Id ID,ass_id 资产编号,fin_id 财务编码,pid 标签喷码,tid 标签ID,cat_no 类别编码,typ 类型,ass_nam 资产名称,ass_desc 资产描述,ass_pri 资产金额,reg_date 登记日期,dept 部门,duty_man 责任人员,addr 所在地点,use_co 所在公司,f_getStat(stat,stat_sub) 状态,supplier 供应商,supplier_info 供应商信息,sn 序列号,vender 厂商品牌,mfr_date 生产日期,unit 单位,num 数量,ppu 单价,duty_man 责任人,company 资产归属,memo 备注,cre_man 创建人员,cre_tm 创建时间,mod_man 修改人员,mod_tm 修改时间,input_typ 购置类型 from ass_list";

            string sSql = sSQLSelect;
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bs.DataSource = dt;
            bindingNavigator1.BindingSource = bs;
            dataGridView1.DataSource = bs;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            textBoxAssId.DataBindings.Add("Text", bs, "资产编号");
            textBoxFinId.DataBindings.Add("Text", bs, "财务编码");
            textBoxAssPri.DataBindings.Add("Text", bs, "资产金额");
            textBoxPpu.DataBindings.Add("Text", bs, "单价");
            comboBoxCat.DataBindings.Add("Text", bs, "类别编码");
            textBoxTyp.DataBindings.Add("Text", bs, "类型");
            textBoxAssNam.DataBindings.Add("Text", bs, "资产名称");
            textBoxNum.DataBindings.Add("Text", bs, "数量");
            textBoxSupplier.DataBindings.Add("Text", bs, "供应商");
            textBoxSupplierInfo.DataBindings.Add("Text", bs, "供应商信息");
            textBoxVender.DataBindings.Add("Text", bs, "厂商品牌");
            textBoxMfrDate.DataBindings.Add("Text", bs, "生产日期");
            comboBoxUnit.DataBindings.Add("Text", bs, "单位");
            textBoxSn.DataBindings.Add("Text", bs, "序列号");
            //textBoxPid.DataBindings.Add("Text", bs, "标签喷码");
            //textBoxTid.DataBindings.Add("Text", bs, "标签ID");
            textBoxAssDesc.DataBindings.Add("Text", bs, "资产描述");
            textBoxId.DataBindings.Add("Text", bs, "ID");
            comboBoxInputTyp.DataBindings.Add("Text", bs, "购置类型");
            comboBoxAddr.DataBindings.Add("Text", bs, "所在地点");
            //获取类别列表

            sSql = "select cat_no,cat_nam,prd_nam from ass_cat";
            MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxCat.Items.Add(
                    reader["cat_no"].ToString() + "-" + reader["cat_nam"].ToString() + "-" + reader["prd_nam"].ToString());
            }
            //try
            //{
            //    //打开DB
            //    myConn.Open();
            //    //获取列表
            //    sSQLSelect = "select Id ID,ass_id 资产编号,fin_id 财务编码,pid 标签喷码,tid 标签ID,cat_no 类别编码,typ 类型,ass_nam 资产名称,ass_desc 资产描述,ass_pri 资产金额,reg_date 登记日期,use_dept 领用部门,use_man 领用人员,addr 所在地点,use_co 所在公司,f_getStat(stat,stat_sub) 状态,supplier 供应商,supplier_info 供应商信息,sn 序列号,vender 厂商品牌,mfr_date 生产日期,unit 单位,num 数量,ppu 单价,duty_man 责任人,company 资产归属,memo 备注,cre_man 创建人员,cre_tm 创建时间,mod_man 修改人员,mod_tm 修改时间,input_typ 购置类型 from ass_list";
            //    da = new MySqlDataAdapter(sql, myConn);
            //    dt = new DataTable();
            //    da.Fill(dt);
            //    bs.DataSource = dt;
            //    bindingNavigator1.BindingSource = bs;
            //    dataGridView1.DataSource = bs;
            //    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            //    textBoxAssId.DataBindings.Add("Text", bs, "资产编号");
            //    textBoxFinId.DataBindings.Add("Text", bs, "财务编码");
            //    textBoxAssPri.DataBindings.Add("Text", bs, "资产金额");
            //    textBoxPpu.DataBindings.Add("Text", bs, "单价");
            //    comboBoxCat.DataBindings.Add("Text", bs, "类别编码");
            //    textBoxTyp.DataBindings.Add("Text", bs, "类型");
            //    textBoxAssNam.DataBindings.Add("Text", bs, "资产名称");
            //    textBoxNum.DataBindings.Add("Text", bs, "数量");
            //    textBoxSupplier.DataBindings.Add("Text", bs, "供应商");
            //    textBoxSupplierInfo.DataBindings.Add("Text", bs, "供应商信息");
            //    textBoxVender.DataBindings.Add("Text", bs, "厂商品牌");
            //    textBoxMfrDate.DataBindings.Add("Text", bs, "生产日期");
            //    comboBoxUnit.DataBindings.Add("Text", bs, "单位");
            //    textBoxSn.DataBindings.Add("Text", bs, "序列号");
            //    //textBoxPid.DataBindings.Add("Text", bs, "标签喷码");
            //    //textBoxTid.DataBindings.Add("Text", bs, "标签ID");
            //    textBoxAssDesc.DataBindings.Add("Text", bs, "资产描述");
            //    textBoxId.DataBindings.Add("Text", bs, "ID");
            //    comboBoxInputTyp.DataBindings.Add("Text", bs, "购置类型");
            //    comboBoxAddr.DataBindings.Add("Text", bs, "所在地点");
            //    //获取类别列表
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
                MessageBox.Show("资产编码不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sSql = "select 'X' from ass_list where ass_id = '" + textBoxAssId.Text + "'";
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
                sSql = "select ass_id from ass_list where pid = '" + textBoxPid.Text + "'";
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

            if (comboBoxCat.Text.Equals(""))
            {
                MessageBox.Show("请选择资产类别!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    "新增", "0", sSqlIns.Replace("'", "\\'"), MainForm.sClientId, textBoxAssId.Text,MainForm.getDateTime());

            List<string> listSql = new List<string>();
            listSql.Add(sSqlIns);
            listSql.Add(sSqlInsLog);
            bool bOK = false;
            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("新增成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                resetData();
                bs.MoveLast();
            }
            else
            {
                MessageBox.Show("新增失败！\r\n" + MysqlHelper.sLastErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
//        private void insertData()
//        {
//            if (textBoxAssId.Text.Equals(""))
//            {
//                MessageBox.Show("资产编号不能为空!");
//                return;
//            }
//            if (comboBoxCat.Text.Equals(""))
//            {
//                MessageBox.Show("请选择资产类别!");
//                return;
//            }

//            try
//            {
//                cmd = myConn.CreateCommand();//sql命令对象，表示要对sql数据库执行一个sql语句
//                cmd.CommandText = @"insert into ass_list
//                    (cre_tm,ass_id,fin_id,pid,tid,cat_no,ass_nam,ass_desc,ass_pri,reg_date,typ,
//                    supplier,sn,vender,mfr_date,unit,num,ppu,company,cre_man,supplier_info,input_typ,addr) 
        //                    values(CURRENT_TIMESTAMP,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17,@18,@19,@20,@21,@22))";//sql语句@表示参数
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
//                myConn.Open();//打开连接
//                cmd.ExecuteNonQuery();//执行不是查询的sql语句
//                MessageBox.Show("新增成功！");
//                resetData();
//                bs.MoveLast();
//            }
//            catch (Exception ex)//由于sql语句对半全角很敏感，捕捉异常
//            {
//                MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                myConn.Close();//不管打开成功还是失败，都能关闭连接
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
//                cmd.Parameters.Add("@18", MySqlDbType.VarChar).Value = "修改备注";//memo
//                cmd.Parameters.Add("@19", MySqlDbType.VarChar).Value = "SYS";//cre_man 
//                cmd.Parameters.Add("@20", MySqlDbType.VarChar).Value = comboBoxInputTyp.Text;
//                cmd.Parameters.Add("@21", MySqlDbType.VarChar).Value = comboBoxAddr.Text;
//                cmd.Parameters.Add("@22", MySqlDbType.VarChar).Value = textBoxId.Text;
               
//                cmd.ExecuteNonQuery();
//                MessageBox.Show("修改成功!");
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
                    "修改备注",//memo
                    MainForm.sUserName,//mod_man 
                    comboBoxInputTyp.Text,
                    comboBoxAddr.Text,
                    textBoxId.Text);

            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
    "修改", "0", sSqlUpd.Replace("'", "\\'"), MainForm.sClientId, textBoxAssId.Text,MainForm.getDateTime());

                List<string> listSql = new List<string>();
                listSql.Add(sSqlUpd);
                listSql.Add(sSqlInsLog);
                bool bOK = false;
                bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
                if (bOK)
                {
                    MessageBox.Show("修改成功！");
                    resetData();
                }
                else
                {
                    MessageBox.Show("修改失败！\r\n" + MysqlHelper.sLastErr);
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
        //        if (MessageBox.Show("新标签为空，确定要清空标签吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        //            return;
        //    }
        //    else
        //    {
        //        if (MessageBox.Show("您确定要替换该标签吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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
        //        cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = "标签更换";//memo
        //        cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = textBoxId.Text;

        //        cmd.ExecuteNonQuery();
        //        MessageBox.Show("更换成功!");
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
                if (MessageBox.Show("确定要清空标签吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            else
            {
                string sSql = "select ass_id from ass_list where pid = '" + textBoxPid.Text + "'";
                MySqlDataReader reader = MysqlHelper.ExecuteReader(sSql);
                if (reader.Read())
                {
                    string sAssid = reader["ass_id"].ToString();
                    MessageBox.Show("该标签已绑定资产:" + sAssid + "\r\n如需继续，请先清空！","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    return;
                }
                reader.Close();
                if (MessageBox.Show("您确定要替换该标签吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            string sSqlUpd
                = string.Format("update ass_list set pid = '{0}', tid = '{1}', mod_tm = '{2}',mod_man = '{3}',memo = '{4}' where id = '{5}'",
            textBoxPid.Text,
            textBoxTid.Text,
            MainForm.getDateTime(),
            "SYS",//cre_man
            "标签更换",//memo
            textBoxId.Text);
            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
"替换", "0", sSqlUpd.Replace("'", "\\'"), MainForm.sClientId, textBoxAssId.Text, MainForm.getDateTime());

            List<string> listSql = new List<string>();
            listSql.Add(sSqlUpd);
            listSql.Add(sSqlInsLog);
            bool bOK = false;
            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("替换成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                resetData();
            }
            else
            {
                MessageBox.Show("替换失败！\r\n" + MysqlHelper.sLastErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        } 
        private void resetData()
        {
            //获取列表
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
        //    if (MessageBox.Show("您确定要删除该笔资产吗？\r\n删除将不可恢复！","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
        //        return;
        //    try
        //    {
        //        cmd = myConn.CreateCommand();
        //        myConn.Open();
        //        cmd.CommandText = "delete from ass_list where id=@id";//删除语句，已ID为条件删除
        //        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = textBoxId.Text;//dataGridView1.CurrentRow.Cells[0].Value.ToString(); 
        //        cmd.ExecuteNonQuery();
        //        resetData();
        //        MessageBox.Show("删除成功！");

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
            if (MessageBox.Show("您确定要删除该笔资产吗？\r\n删除将不可恢复！","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string sSqlDel = string.Format("delete from ass_list where id='{0}'",textBoxId.Text);//删除语句，已ID为条件删除

            string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
"删除", "0", sSqlDel.Replace("'", "\\'"), MainForm.sClientId, textBoxAssId.Text, MainForm.getDateTime());

            List<string> listSql = new List<string>();
            listSql.Add(sSqlDel);
            listSql.Add(sSqlInsLog);
            bool bOK = false;
            bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                MessageBox.Show("删除成功！");
                resetData();
            }
            else
            {
                MessageBox.Show("删除失败！\r\n" + MysqlHelper.sLastErr);
            }
        }
       

        private void AssInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            //myConn.Close();
            mf.recvEvent -= new MainForm.RecvEventHandler(this.RecvDataEvent);
        }

        // 利用委托回调机制实现界面上消息内容显示
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

                buttonOK.Text = "保存";

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

                buttonOK.Text = "查询";

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

                buttonOK.Text = "保存";

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