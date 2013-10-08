using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using MySQLDriverCS;
using System.Xml;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MySql.Data.MySqlClient;


namespace AssMngSys
{
    public partial class MainForm : Form
    {
        //static public string sCompany = "";
        //static public string sUserName = "";
        //static public string sClientId = "PC";
        Form curForm = null;
        public MySqlConnection myConn = 
            new MySqlConnection("server=localhost;user id=root; password=gv; database=gvdb; pooling=false;port=3306");

        // 创建一个委托，返回类型为void，两个参数
        public delegate void RecvEventHandler(object sender, RecvEventArgs e);
        // 将创建的委托和特定事件关联,在这里特定的事件为KeyDown
        public event RecvEventHandler recvEvent;

        //创建一个Thread类   
        private Thread thread1;
        static string receiveString;
        public static bool messageReceived = false;
        public static bool bIsReceiving = true;

        public MainForm()
        {
            InitializeComponent();
        }
        public void AddLog(string str)
        {
            str = DateTime.Now.ToString("hh:mm") + " " + str + @"
";
            textBoxLog.AppendText(str);
            textBoxLog.ScrollToCaret();
        }
        private void MainWnd_Load(object sender, EventArgs e)
        {
            bIsReceiving = true;
            //初始化该线程并指定线程执行时要调用的方法   
            thread1 = new Thread(new ThreadStart(ReceiveMessages));
            //启动线程   
            thread1.Start();
            //treeView1.BeginUpdate();
            //treeView1.Nodes.Clear();
            //treeView1.Nodes.Add("Parent");
            //treeView1.Nodes[0].Nodes.Add("Child1");
            //treeView1.Nodes[0].Nodes.Add("Child2");
            //treeView1.Nodes[0].Nodes[1].Nodes.Add("Grandchild");
            //treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Great Grandchild");
            //treeView1.EndUpdate();


            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            treeView1.ImageList = imageList1;
            treeView1.ImageIndex = 0;
            treeView1.SelectedImageIndex = 1; 

            treeView1.Nodes.Add("基本资料");
            treeView1.Nodes[0].ImageIndex = 2;
            treeView1.Nodes[0].SelectedImageIndex = 3;
            treeView1.Nodes[0].Nodes.Add("资产登记"); 
            treeView1.Nodes[0].Nodes.Add("类别维护");
            treeView1.Nodes[0].Nodes.Add("人员维护"); 
            treeView1.Nodes[0].Nodes.Add("地点维护"); 
            //权限
            if (Login.sRole != "系统管理员" && Login.sRole != "资产管理员")
            {
                treeView1.Nodes[0].ForeColor = System.Drawing.Color.Gray;
                treeView1.Nodes[0].Nodes[0].ForeColor = System.Drawing.Color.Gray;
                treeView1.Nodes[0].Nodes[1].ForeColor = System.Drawing.Color.Gray;
                treeView1.Nodes[0].Nodes[2].ForeColor = System.Drawing.Color.Gray;
                treeView1.Nodes[0].Nodes[3].ForeColor = System.Drawing.Color.Gray;
            }

            treeView1.Nodes.Add("资产管理");
            treeView1.Nodes[1].ImageIndex = 2;
            treeView1.Nodes[1].SelectedImageIndex = 3;
            treeView1.Nodes[1].Nodes.Add("领用管理");
            treeView1.Nodes[1].Nodes.Add("使用管理");
            treeView1.Nodes[1].Nodes.Add("资产注销");
            //权限
            if (Login.sRole != "系统管理员" && Login.sRole != "资产管理员")
            {
                
                if (Login.sRole != "实验室管理员")
                {
                    treeView1.Nodes[1].ForeColor = System.Drawing.Color.Gray;
                    treeView1.Nodes[1].Nodes[1].ForeColor = System.Drawing.Color.Gray;
                }
                treeView1.Nodes[1].Nodes[0].ForeColor = System.Drawing.Color.Gray;
                treeView1.Nodes[1].Nodes[2].ForeColor = System.Drawing.Color.Gray;
            }


            treeView1.Nodes.Add("资产盘点");
            treeView1.Nodes[2].ImageIndex = 2;
            treeView1.Nodes[2].SelectedImageIndex = 3;
            treeView1.Nodes[2].Nodes.Add("创建清单");
            treeView1.Nodes[2].Nodes.Add("清单查询");
             //权限
            if (Login.sRole != "系统管理员" && Login.sRole != "资产管理员")
            {
                treeView1.Nodes[2].ForeColor = System.Drawing.Color.Gray;
                treeView1.Nodes[2].Nodes[0].ForeColor = System.Drawing.Color.Gray;
                treeView1.Nodes[2].Nodes[1].ForeColor = System.Drawing.Color.Gray;
            }

            treeView1.Nodes.Add("查询统计");
            treeView1.Nodes[3].ImageIndex = 2;
            treeView1.Nodes[3].SelectedImageIndex = 3;
            treeView1.Nodes[3].Nodes.Add("资产查询");
            treeView1.Nodes[3].Nodes.Add("资产历史");
            treeView1.EndUpdate();

            AddLog("----欢迎使用!----");
            AddLog("<双击清空>");

            splitContainer1.Panel2.Controls.Clear();//这里是清空panel2中的控件的。
            Welcome obj = new Welcome();
            obj.TopLevel = false;
            obj.MdiParent = this;//这句代码也要写上，否则会出错。
            obj.Parent = splitContainer1.Panel2;   //Form3的parent是panel2.  
            //obj.textBoxLog = textBoxLog;
            obj.FormBorderStyle = FormBorderStyle.None;
            obj.WindowState = FormWindowState.Maximized;
            obj.BringToFront();
            obj.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            obj.Show();
            curForm = obj;
            

        }
        public static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).u;
                IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).e;

                Byte[] receiveBytes = u.EndReceive(ar, ref e);
                receiveString = Encoding.ASCII.GetString(receiveBytes);
                receiveString = receiveString.Replace("\0", "");
                Console.WriteLine("Received: {0} \n", receiveString);
                //AddLog(textBoxLog,receiveString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            messageReceived = true;
        }
        public void ReceiveMessages()
        {
            // Receive a message and write it to the console.
            IPEndPoint e = new IPEndPoint(IPAddress.Any, 6035);
            UdpClient u = null;
            UdpState s = null;
            while (true && bIsReceiving)
            {
                try
                {
                    u = new UdpClient(e);
                    s = new UdpState();
                    s.e = e;
                    s.u = u;
                    _AddLog(textBoxLog, "UDP创建OK！");
                    break;
                }
                catch (Exception)
                {
                    _AddLog(textBoxLog, "UDP创建中...");
                    Thread.Sleep(3000);
                }
            }
            while (bIsReceiving)
            {
                Console.WriteLine("listening for messages");
                messageReceived = false;
                u.BeginReceive(new AsyncCallback(ReceiveCallback), s);

                // Do some work while we wait for a message. For this example,
                // we'll just sleep
                while (!messageReceived && bIsReceiving)
                {
                    Thread.Sleep(100);
                }
                if (messageReceived)
                {
                    _AddLog(textBoxLog, receiveString);
                    int nIndex = receiveString.IndexOf(',');
                    string sEpc = "";
                    string sTid = "";
                    if (nIndex != -1)
                    {
                        sEpc = receiveString.Substring(0, nIndex);
                        sTid = receiveString.Substring(nIndex + 1);
                    }
                    else
                    {
                        sEpc = receiveString;
                    }

                    if (sEpc.Length == 24)
                    {
                        sEpc = sEpc.Substring(12, 12);
                    }

                    if (recvEvent != null)
                    {
                        // 得到按键信息的参数
                        RecvEventArgs args = new RecvEventArgs(sEpc, sTid);
                        // 触发事件
                        recvEvent(this, args);
                    }
                }
            }
            s.u.Close();
        }
        // 利用委托回调机制实现界面上消息内容显示
        delegate void AddLogCallBack(TextBox textBox, string str);
        private void _AddLog(TextBox textBox, string str)
        {
            if (textBox.InvokeRequired)
            {
                AddLogCallBack addLogCallBack = _AddLog;
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
        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (curForm != null)
            {
                curForm.Close();
                curForm = null;
            }

           // AddLog(treeView1.SelectedNode.Text);
            string sCurNode = treeView1.SelectedNode.Text;
            if (treeView1.SelectedNode.ForeColor == System.Drawing.Color.Gray)
            {
                sCurNode = "";
            }
            if (sCurNode.Equals("资产登记"))
            {
                splitContainer1.Panel2.Controls.Clear();//这里是清空panel2中的控件的。
                AssInput obj = new AssInput(this);
                obj.TopLevel = false;
                obj.MdiParent = this;//这句代码也要写上，否则会出错。
                obj.Parent = splitContainer1.Panel2;   //Form3的parent是panel2.  
                //obj.textBoxLog = textBoxLog;
                obj.FormBorderStyle = FormBorderStyle.None;
                obj.WindowState = FormWindowState.Maximized;
                obj.BringToFront();
                obj.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                obj.Show();
                curForm = obj;
            }
            else if (sCurNode.Equals("类别维护"))
            {
                splitContainer1.Panel2.Controls.Clear();//这里是清空panel2中的控件的。
                CatList obj = new CatList(this);
                obj.TopLevel = false;
                obj.MdiParent = this;//这句代码也要写上，否则会出错。
                obj.Parent = splitContainer1.Panel2;   //Form3的parent是panel2.  
               //obj.textBoxLog = textBoxLog;
                obj.FormBorderStyle = FormBorderStyle.None;
                obj.WindowState = FormWindowState.Maximized;
                obj.BringToFront();
                obj.Anchor = AnchorStyles.Left | AnchorStyles.Top; 
                obj.Show();
                curForm = obj;
            }
            else if (sCurNode.Equals("人员维护"))
            {
                splitContainer1.Panel2.Controls.Clear();//这里是清空panel2中的控件的。
                EmpList obj = new EmpList(this);
                obj.TopLevel = false;
                obj.MdiParent = this;//这句代码也要写上，否则会出错。
                obj.Parent = splitContainer1.Panel2;   //Form3的parent是panel2.  
               //obj.textBoxLog = textBoxLog;
                obj.FormBorderStyle = FormBorderStyle.None;
                obj.WindowState = FormWindowState.Maximized;
                obj.BringToFront();
                obj.Anchor = AnchorStyles.Left | AnchorStyles.Top; 
                obj.Show();
                curForm = obj;
            }      
            else if (sCurNode.Equals("地点维护"))
            {
                splitContainer1.Panel2.Controls.Clear();//这里是清空panel2中的控件的。
                AddrList obj = new AddrList(this);
                obj.TopLevel = false;
                obj.MdiParent = this;//这句代码也要写上，否则会出错。
                obj.Parent = splitContainer1.Panel2;   //Form3的parent是panel2.  
               //obj.textBoxLog = textBoxLog;
                obj.FormBorderStyle = FormBorderStyle.None;
                obj.WindowState = FormWindowState.Maximized;
                obj.BringToFront();
                obj.Anchor = AnchorStyles.Left | AnchorStyles.Top; 
                obj.Show();
                curForm = obj;
            }        
            else if (sCurNode.Equals("领用管理"))
            {
                splitContainer1.Panel2.Controls.Clear();//这里是清空panel2中的控件的。
                AssSupply obj = new AssSupply(this);
                obj.TopLevel = false;
                obj.MdiParent = this;//这句代码也要写上，否则会出错。
                obj.Parent = splitContainer1.Panel2;   //Form3的parent是panel2.  
               //obj.textBoxLog = textBoxLog;
                obj.FormBorderStyle = FormBorderStyle.None;
                obj.WindowState = FormWindowState.Maximized;
                obj.BringToFront();
                obj.Anchor = AnchorStyles.Left | AnchorStyles.Top; 
                obj.Show();
                curForm = obj;
            }
            else if (sCurNode.Equals("使用管理"))
            {
                splitContainer1.Panel2.Controls.Clear();//这里是清空panel2中的控件的。
                AssUse obj = new AssUse(this);
                obj.TopLevel = false;
                obj.MdiParent = this;//这句代码也要写上，否则会出错。
                obj.Parent = splitContainer1.Panel2;   //Form3的parent是panel2.  
                //obj.textBoxLog = textBoxLog;
                obj.FormBorderStyle = FormBorderStyle.None;
                obj.WindowState = FormWindowState.Maximized;
                obj.BringToFront();
                obj.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                obj.Show();
                curForm = obj;
            }
            else if (sCurNode.Equals("资产注销"))
            {
                splitContainer1.Panel2.Controls.Clear();//这里是清空panel2中的控件的。
                AssLogoff obj = new AssLogoff(this);
                obj.TopLevel = false;
                obj.MdiParent = this;//这句代码也要写上，否则会出错。
                obj.Parent = splitContainer1.Panel2;   //Form3的parent是panel2.  
                //obj.textBoxLog = textBoxLog;
                obj.FormBorderStyle = FormBorderStyle.None;
                obj.WindowState = FormWindowState.Maximized;
                obj.BringToFront();
                obj.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                obj.Show();
                curForm = obj;
            }
            else if (sCurNode.Equals("资产查询"))
            {
                splitContainer1.Panel2.Controls.Clear();//这里是清空panel2中的控件的。
                QryAssList obj = new QryAssList(this);
                obj.TopLevel = false;
                obj.MdiParent = this;//这句代码也要写上，否则会出错。
                obj.Parent = splitContainer1.Panel2;   //Form3的parent是panel2.  
                //obj.textBoxLog = textBoxLog;
                obj.FormBorderStyle = FormBorderStyle.None;
                obj.WindowState = FormWindowState.Maximized;
                obj.BringToFront();
                obj.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                obj.Show();
                curForm = obj;
            }
            else if(sCurNode.Equals("资产历史"))
            {
                splitContainer1.Panel2.Controls.Clear();//这里是清空panel2中的控件的。
                QryAssLog obj = new QryAssLog(this);
                obj.TopLevel = false;
                obj.MdiParent = this;//这句代码也要写上，否则会出错。
                obj.Parent = splitContainer1.Panel2;   //Form3的parent是panel2.  
               //obj.textBoxLog = textBoxLog;
                obj.FormBorderStyle = FormBorderStyle.None;
                obj.WindowState = FormWindowState.Maximized;
                obj.BringToFront();
                obj.Anchor = AnchorStyles.Left | AnchorStyles.Top; 
                obj.Show();
                curForm = obj;
            }
            else if (sCurNode.Equals("创建清单"))
            {
                splitContainer1.Panel2.Controls.Clear();//这里是清空panel2中的控件的。
                InvList obj = new InvList(this);
                obj.TopLevel = false;
                obj.MdiParent = this;//这句代码也要写上，否则会出错。
                obj.Parent = splitContainer1.Panel2;   //Form3的parent是panel2.  
                //obj.textBoxLog = textBoxLog;
                obj.FormBorderStyle = FormBorderStyle.None;
                obj.WindowState = FormWindowState.Maximized;
                obj.BringToFront();
                obj.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                obj.Show();
                curForm = obj;
            }
            else if (sCurNode.Equals("清单查询"))
            {
                splitContainer1.Panel2.Controls.Clear();//这里是清空panel2中的控件的。
                InvListQry obj = new InvListQry(this);
                obj.TopLevel = false;
                obj.MdiParent = this;//这句代码也要写上，否则会出错。
                obj.Parent = splitContainer1.Panel2;   //Form3的parent是panel2.  
                //obj.textBoxLog = textBoxLog;
                obj.FormBorderStyle = FormBorderStyle.None;
                obj.WindowState = FormWindowState.Maximized;
                obj.BringToFront();
                obj.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                obj.Show();
                curForm = obj;
            }
            else
            {
                splitContainer1.Panel2.Controls.Clear();//这里是清空panel2中的控件的。
                Welcome obj = new Welcome();
                obj.TopLevel = false;
                obj.MdiParent = this;//这句代码也要写上，否则会出错。
                obj.Parent = splitContainer1.Panel2;   //Form3的parent是panel2.  
                //obj.textBoxLog = textBoxLog;
                obj.FormBorderStyle = FormBorderStyle.None;
                obj.WindowState = FormWindowState.Maximized;
                obj.BringToFront();
                obj.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                obj.Show();
                curForm = obj;
            }
        }

        private void textBoxLog_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxLog.Clear();
            AddLog("<双击清空>");
        }

        private void MainWnd_FormClosing(object sender, FormClosingEventArgs e)
        {  
            bIsReceiving = false;
            if (curForm != null)
            {
                curForm.Close();
                curForm = null;
            }
            //终止线程   
            thread1.Abort();
            thread1.Join(10000);
        }

        private void splitContainer1_Panel2_Resize(object sender, EventArgs e)
        {
            if (curForm != null)
            {
                if (curForm.WindowState == FormWindowState.Maximized)
                    curForm.WindowState = FormWindowState.Normal;
                curForm.Size = splitContainer1.Panel2.Size;
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxLog_TextChanged(object sender, EventArgs e)
        {

        }

        //获取时间：yyyy-mm-dd hh24::mm:ss
        static public string getDateTime()
        {
            string s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return s;
        }
        //获取时间：yyyymmdd
        static public string getDate()
        {
            string s = DateTime.Now.ToString("yyyy-MM-dd");
            return s;
        }

        private void MenuItemQuit_Click(object sender, EventArgs e)
        {
             this.Close();
        }

        private void ttoolStripButtonAssInput_Click(object sender, EventArgs e)
        {
            //List<string> aTmpList = new List<string>(); //记录最近5个标签
            //aTmpList.Add("abc");
            //aTmpList.Add("cccc");
            //aTmpList.Add("bbbbb");
            //aTmpList.Add("aaaaaa");
            //int nIndex = aTmpList.FindIndex(delegate(string p) { return p == "abc"; });
            //MessageBox.Show(string.Format("{0}",nIndex));

            treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
            treeView1_DoubleClick(null, null);
        }

        private void toolStripButtonSupply_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode = treeView1.Nodes[1].Nodes[0];
            treeView1_DoubleClick(null, null);
        }

        private void toolStripButtonUse_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode = treeView1.Nodes[1].Nodes[1];
            treeView1_DoubleClick(null, null);
        }

        private void toolStripButtonLogoff_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode = treeView1.Nodes[1].Nodes[2];
            treeView1_DoubleClick(null, null);
        }

        private void toolStripButtonInv_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode = treeView1.Nodes[2].Nodes[0];
            treeView1_DoubleClick(null, null);
        }
    }
}