using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AssMngSys
{
    public partial class QryAssList : Form
    {
        MainForm mf;
        
        string sSQLSelect = "select Id ID,ass_id 资产编号,fin_id 财务编码,pid 标签喷码,tid 标签ID,cat_no 类别编码,typ 类型,ass_nam 资产名称,stat 库存状态,stat_sub 使用状态,duty_man 保管人员,dept 部门,ass_desc 资产描述,ass_pri 资产金额,reg_date 登记日期,addr 所在地点,use_co 所在公司,supplier 供应商,supplier_info 供应商信息,sn 序列号,vender 厂商品牌,mfr_date 生产日期,unit 单位,num 数量,ppu 单价,duty_man 责任人员,company 资产归属,memo 备注,cre_man 创建人员,cre_tm 创建时间,mod_man 修改人员,mod_tm 修改时间,input_typ 购置类型 from ass_list";
 
        public QryAssList(MainForm f)
        {
            InitializeComponent(); 
            f.recvEvent += new MainForm.RecvEventHandler(this.RecvDataEvent);
            mf = f;
        }
        
        private void AssLogForm_Load(object sender, EventArgs e)
        {
            buttonQry_Click(null,null);
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
        private void buttonQry_Click(object sender, EventArgs e)
        {
            string sStartDate = string.Format("{0}-{1:00}-{2:00}",dateTimePicker1.Value.Year,dateTimePicker1.Value.Month,dateTimePicker1.Value.Day);
            string sEndDate = string.Format("{0}-{1:00}-{2:00}",dateTimePicker2.Value.Year,dateTimePicker2.Value.Month,dateTimePicker2.Value.Day);
            string sSql = sSQLSelect + string.Format(" where reg_date between '{0}' and '{1}'", sStartDate, sEndDate);
            DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
            bindingSource1.DataSource = dt;
            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void textBoxPid_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPid.Text.Length == 12)
            {
                string sSql = sSQLSelect + " where ass_id = (select ass_id from ass_list where pid = '" + textBoxPid.Text + "' limit 0,1)";
                DataTable dt = MysqlHelper.ExecuteDataTable(sSql);
                bindingSource1.DataSource = dt;
                bindingNavigator1.BindingSource = bindingSource1;
                dataGridView1.DataSource = bindingSource1;
                //textBoxPid.Text = "";
            }
        }

    }
}