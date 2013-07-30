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
        QryAssDlg qryassdlg;

        MainForm mf;
        public AssInput(MainForm f)
        {
            InitializeComponent();
            //myConn = f.myConn;
            mf = f;
            qryassdlg = new QryAssDlg(mf);
        }

        private void AssInputBak_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.ass_list' table. You can move, or remove it, as needed.
            this.ShowInTaskbar = false;


            sSQLSelect = "select Id ID,pid 标签喷码,ass_id 资产编码,fin_id 财务编码,typ 类型,ass_nam 资产名称,stat 库存状态,stat_sub 使用状态,dev_mode 设备型号,ass_desc 备注,ass_pri 资产金额,input_typ 购置类型,input_date 购置日期,dept 所属部门,addr 所在地点,use_co 所在公司,supplier 供应商,supplier_info 供应商信息,sn 序列号,vender 厂商品牌,num 数量,unit 单位,ppu 单价,duty_man 保管人员,company 资产归属,memo 备注,cre_man 创建人员,cre_tm 创建时间,mod_man 修改人员,mod_tm 修改时间 from ass_list where ynenable = 'Y' ";

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



        private void AssInputBak_Resize(object sender, EventArgs e)
        {
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridViewTextBoxColumn dgv_Text = new DataGridViewTextBoxColumn();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //行号
                int j = i + 1;
                dataGridView1.Rows[i].HeaderCell.Value = j.ToString();
                //颜色
                string sStat = dataGridView1.Rows[i].Cells["库存状态"].Value.ToString();
                if (sStat != "库存" && sStat != "领用")
                {
                    try
                    {
                        this.dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(0xFF0000);
                    }
                    catch (Exception ex)
                    {
                        // new FileOper().writelog(ex.Message);
                    }
                }
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            NewAssDlg dlg = new NewAssDlg(mf, dataGridView1);
            dlg.sOptType = "新增";
            dlg.ShowDialog();
            if (dlg.bDone)
            {
                resetData();
                bs.MoveLast();
            }
        }
        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                NewAssDlg dlg = new NewAssDlg(mf, dataGridView1);
                dlg.sOptType = "复制";
                dlg.ShowDialog();
                if (dlg.bDone)
                {
                    resetData();
                    bs.MoveLast();
                }
            }
        }
        private void toolStripButtonMod_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                NewAssDlg dlg = new NewAssDlg(mf, dataGridView1);
                dlg.sOptType = "修改";
                dlg.ShowDialog();
                if (dlg.bDone)
                {
                    resetData();
                }
            }
            else
            {
                MessageBox.Show("请先选择一笔资料！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void toolStripButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                if (MessageBox.Show(string.Format("您确定要删除选择资产吗？\r\n资产数：{0}", dataGridView1.SelectedRows.Count),
                    "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    List<string> listSql = new List<string>();
                    string sSqlUpd = "update ass_list set ynenable = 'N' where id = '";
                    string sSql = "";
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        string sId = dataGridView1.SelectedRows[i].Cells["ID"].Value.ToString();
                        string sAssId = dataGridView1.SelectedRows[i].Cells["资产编码"].Value.ToString();
                        sSql = sSqlUpd + sId + "'";
                        listSql.Add(sSql);
                        string sSqlLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
                        "删除", "0", sSql.Replace("'", "''"), MainForm.sClientId, sAssId, MainForm.getDateTime());
                        listSql.Add(sSqlLog);
                    }

                    bool bOK = false;
                    bOK = MysqlHelper.ExecuteNoQueryTran(listSql);
                    if (bOK)
                    {
                        resetData();
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("删除失败！\r\n" + MysqlHelper.sLastErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                }
            }
            else
            {
                MessageBox.Show("请先选择资料！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void toolStripButtonQry_Click(object sender, EventArgs e)
        {
            //QryAssDlg qryassdlg = new QryAssDlg(mf);
            if (qryassdlg.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = MysqlHelper.ExecuteDataTable(sSQLSelect + qryassdlg.sSqlCondition);
                bs.DataSource = dt;
            }
        }
        private void resetData()
        {
            //获取列表
            DataTable dt = MysqlHelper.ExecuteDataTable(sSQLSelect);
            bs.DataSource = dt;
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            //PrintTxt simple = new PrintTxt("D:\\12.txt", "txt");
            //PrintTxt simple = new PrintTxt("D:\\Water lilies.jpg", "image"); 
            PrintDlg dlg = new PrintDlg(dataGridView1);
            dlg.ShowDialog();
        }
    }
}