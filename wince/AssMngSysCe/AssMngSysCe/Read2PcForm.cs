using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NameApi;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using System.Data.SQLite;

namespace IrRfidUHFDemo
{
    public partial class Read2PcForm : Form
    {
        Thread thrSyncIni;
        Thread thrSyncDiff;
        public bool bSyncing = false;
        public delegate int MyDelegate(string message);


        Thread thrCheck;
        public bool bChecking = false;
        int nAll = 0, nExp = 0, nCheck = 0;

        Thread thrInv;
        public bool bInving = false;
        public delegate void InvokeDelegate();
        String sOnetagInfo;

        static string sIp;
        static string sPort;
        enum Page { onepcs = 0, inv, check, sync };
        enum Index { no = 0, epc, pid, stat, cnt, assid };

        int nCurTab = 0;

        private System.Net.Sockets.UdpClient sendUdpClient;
        public Read2PcForm()
        {
            InitializeComponent();
        }
        private void Read2PcForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("N", 16, HorizontalAlignment.Left);
            listView1.Columns.Add("EPC", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("喷码", 96, HorizontalAlignment.Left);
            listView1.Columns.Add("状态", 38, HorizontalAlignment.Left);
            listView1.Columns.Add("C", 20, HorizontalAlignment.Left);
            listView1.Columns.Add("资产编码", 60, HorizontalAlignment.Left);

            listView2.FullRowSelect = true;
            listView2.Columns.Add("序", 16, HorizontalAlignment.Left);
            listView2.Columns.Add("喷码", 96, HorizontalAlignment.Left);
            listView2.Columns.Add("资产编码", 60, HorizontalAlignment.Left);
            listView2.Columns.Add("资产名称", 60, HorizontalAlignment.Left);
            listView2.Columns.Add("领用状态", 40, HorizontalAlignment.Left);
            listView2.Columns.Add("领用人员", 40, HorizontalAlignment.Left);
            listView2.Columns.Add("所属部门", 60, HorizontalAlignment.Left);
            listView2.Columns.Add("所在地点", 60, HorizontalAlignment.Left);
            listView2.Columns.Add("盘点结果", 40, HorizontalAlignment.Left);
            listView2.Columns.Add("ID", 0, HorizontalAlignment.Left);

            string sSql = "select distinct inv_no from inv_list order by inv_no desc";
            SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql);
            comboBoxInvListNo.Items.Clear();
            while (reader.Read())
            {
                comboBoxInvListNo.Items.Add(reader["inv_no"].ToString());
            }
            reader.Close();

        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == (int)Page.onepcs)//单次读取
            {
                readData();
            }
            else if (tabControl1.SelectedIndex == (int)Page.inv) //循环读取
            {
                InvFun();
            }
            else if (tabControl1.SelectedIndex == (int)Page.sync)//发送数据到PC
            {
                SendData();
            }
            else if (tabControl1.SelectedIndex == (int)Page.check)//发送数据到PC
            {
                CheckFun();
            }
        }
        private void CheckFun()
        {
            if (bChecking == false)
            {
                buttonRead.Text = "停止";
                bChecking = true;
                thrCheck = new Thread(new ThreadStart(checkThread));
                thrCheck.Start();
            }
            else
            {
                buttonRead.Text = "开始";
                bChecking = false;
                thrCheck.Abort();
                thrCheck.Join();
            }
        }
        private void checkThread()
        {
            byte nTagCount = 0;
            byte[] uReadData = new byte[512];
            String[] tagInfo = new String[50];
            while (bChecking)
            {
                if (1 == HTApi.WIrUHFInventoryOnce(ref nTagCount, ref uReadData[0]))
                {
                    PlaySound("\\Windows\\critical.wav", IntPtr.Zero, 0x0001);

                    int i = 0, j = 0;
                    int tagLen;

                    int dataIndex = 0;

                    int nRealTagCount = 0;
                    //读出nTagCount个标签数据
                    for (i = 0; i < nTagCount; i++)
                    {
                        tagLen = uReadData[dataIndex++];
                        nRealTagCount++;
                        for (j = 0; j < tagLen; j++)
                        {
                            String strTmp = string.Format("{0:X2}", uReadData[dataIndex++]);
                            if (j == 0)
                            {
                                tagInfo[i] = strTmp;
                            }
                            else
                            {
                                tagInfo[i] += strTmp;//tagInfo[i] += "-" + strTmp;
                            }
                        }
                    }
                    //把数据放到列表
                    for (i = 0; i < nRealTagCount; i++)
                    {
                        sOnetagInfo = tagInfo[i];
                        listView2.BeginInvoke(new InvokeDelegate(showCheck));
                    }
                }
            }
        }
        private void showCheck()
        {
            string sPid = sOnetagInfo;
            if (sPid.Length == 24)
            {
                sPid = sPid.Substring(12, 12);
            }
            int nfind = -1;
            int count = listView2.Items.Count;
            for (int j = 0; j < count; j++)
            {
                string findItem = listView2.Items[j].SubItems[1].Text;
                if (sPid == findItem)
                {
                    nfind = j;
                    break;
                }
            }
            if (nfind != -1)
            {
                if (listView2.Items[nfind].SubItems[8].Text.Length == 0)
                {
                    string sErr;
                    checkAss(nfind, "自动盘点", out sErr);
                }
            }
            else
            {
            }

        }
        private bool checkAss(int nIndex,string sTpe,out string sErr)
        {
            //找到显示绿色
            sErr = "";
            List<string> listSql = new List<string>();
            string sAssId = listView2.Items[nIndex].SubItems[2].Text;
            string sId = listView2.Items[nIndex].SubItems[9].Text;
            string sSql = "update inv_list set result = '" + sTpe + "' where id = " + sId;
            string sSqlLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
                "盘点结果", "0", sSql.Replace("'", "''"), SettingForm.sClientId, sAssId, LoginForm.getDateTime());
            listSql.Add(sSql);
            listSql.Add(sSqlLog);
            bool bOK = SQLiteHelper.ExecuteNoQueryTran(listSql);
            sErr = SQLiteHelper.sLastErr;
            if (bOK)
            {
                listView2.Items[nIndex].SubItems[8].Text = sTpe;
                listView2.Items[nIndex].BackColor = Color.FromArgb(0, 255, 0);
                nCheck++;
                label4.Text = string.Format("总计 {0};已盘 {1};异常 {2};未盘点 {3}.", nAll, nCheck, nExp, nAll - nCheck);
            }
            else
            {
                Console.Out.WriteLine(sErr);
            }
            return bOK;
        }


        private void readData()
        {
            labelMsg.Text = "";

            //byte _offset = Convert.ToByte(textBoxOff.Text.Trim());
            //byte _length = Convert.ToByte(textBoxlen.Text.Trim());

            byte[] nTagCount = new byte[1];
            byte[] uReadData = new byte[512];
            string strEpc = "";
            string strTid = "";
            //byte uBank = 0;


            //if (epcButton.Checked)
            //{
            //    uBank = 1;
            //}
            //else if (userButton.Checked)
            //{
            //    uBank = 3;
            //}
            //else if (tidButton.Checked)
            //{
            //    uBank = 2;
            //}
            //读取EPC
            byte _offset = 2;
            byte _length = 6;
            bool bOK = true;

            Array.Clear(nTagCount, 0, 1);
            Array.Clear(uReadData, 0, 512);
            if (1 == HTApi.WIrUHFReadData(1, _offset, _length, ref nTagCount[0], ref uReadData[0]))
            {
                //显示一张标签
                int _recLength = uReadData[0];
                ;
                int i = 0;

                for (i = 0; i < _recLength; i++)
                {
                    String strTmp = string.Format("{0:X2}", uReadData[i + 1]);
                    strEpc += strTmp;
                }
            }
            else
            {
                bOK = false;
            }
            //读取TID
            _offset = 0;
            _length = 6;
            Array.Clear(nTagCount, 0, 1);
            Array.Clear(uReadData, 0, 512);
            if (1 == HTApi.WIrUHFReadData(2, _offset, _length, ref nTagCount[0], ref uReadData[0]))
            {
                //显示一张标签
                int _recLength = uReadData[0];
                int i = 0;
                for (i = 0; i < _recLength; i++)
                {
                    String strTmp = string.Format("{0:X2}", uReadData[i + 1]);
                    strTid += strTmp;
                }
            }
            else
            {
                bOK = false;
            }

            textBoxEpc.Text = strEpc;
            textBoxTid.Text = strTid;

            if (bOK)
            {
                PlaySound("\\Windows\\critical.wav", IntPtr.Zero, 0x0001);
                labelMsg.Text = "读卡OK!";

                // 匿名模式(套接字绑定的端口由系统随机分配)
                sendUdpClient = new System.Net.Sockets.UdpClient(0);
                IPAddress remoteIp = IPAddress.Parse(SettingForm.sUdpIp);
                IPEndPoint remoteIpEndPoint = new IPEndPoint(remoteIp, int.Parse(SettingForm.sUdpPort));
                string message = strEpc + "," + strTid;
                byte[] sendbytes = Encoding.Unicode.GetBytes(message);
                sendUdpClient.Send(sendbytes, sendbytes.Length, remoteIpEndPoint);
                sendUdpClient.Close();
            }
            else
            {
                labelMsg.Text = "请再试一次...";
            }

        }
        private void InvFun()
        {
            if (bInving == false)
            {
                bInving = true;
                sIp = SettingForm.sUdpIp;
                sPort = SettingForm.sUdpPort;
                thrInv = new Thread(new ThreadStart(invThread));
                thrInv.Start();

                // thrInv = new Thread(new ParameterizedThreadStart(invThread));
                // thrInv.Start(textBoxIp.Text, textBoxPort.Text);

                buttonRead.Text = "停止";
            }
            else
            {
                buttonRead.Text = "开始";
                bInving = false;

                thrInv.Abort();
                thrInv.Join();
            }
        }
        private void invThread()
        {
            // 匿名模式(套接字绑定的端口由系统随机分配)
            sendUdpClient = new System.Net.Sockets.UdpClient(0);

            byte nTagCount = 0;
            byte[] uReadData = new byte[512];
            String[] tagInfo = new String[50];
            string strLastDat = "NULL";
            while (bInving)
            {
                if (1 == HTApi.WIrUHFInventoryOnce(ref nTagCount, ref uReadData[0]))
                {
                    PlaySound("\\Windows\\critical.wav", IntPtr.Zero, 0x0001);

                    int i = 0, j = 0;
                    int tagLen;

                    int dataIndex = 0;

                    int nRealTagCount = 0;
                    //读出nTagCount个标签数据
                    for (i = 0; i < nTagCount; i++)
                    {
                        tagLen = uReadData[dataIndex++];

                        nRealTagCount++;
                        for (j = 0; j < tagLen; j++)
                        {
                            String strTmp = string.Format("{0:X2}", uReadData[dataIndex++]);
                            if (j == 0)
                            {
                                tagInfo[i] = strTmp;
                            }
                            else
                            {
                                //tagInfo[i] += "-" + strTmp;
                                tagInfo[i] += strTmp;
                            }
                        }
                    }
                    //把数据放到列表
                    for (i = 0; i < nRealTagCount; i++)
                    {
                        sOnetagInfo = tagInfo[i];
                        listView1.BeginInvoke(new InvokeDelegate(showInv));

                        if (!strLastDat.Equals(sOnetagInfo))
                        {
                            strLastDat = sOnetagInfo;
                            string message = sOnetagInfo;
                            byte[] sendbytes = Encoding.Unicode.GetBytes(message);
                            IPAddress remoteIp = IPAddress.Parse(sIp);
                            IPEndPoint remoteIpEndPoint = new IPEndPoint(remoteIp, int.Parse(sPort));
                            sendUdpClient.Send(sendbytes, sendbytes.Length, remoteIpEndPoint);
                        }
                    }
                }
            }
            sendUdpClient.Close();
        }

        private void showInv()
        {
            int nfind = -1;
            int count = listView1.Items.Count;
            for (int j = 0; j < count; j++)
            {
                string findItem = listView1.Items[j].SubItems[(int)Index.epc].Text;
                if (sOnetagInfo == findItem)
                {
                    nfind = j;
                    break;
                }
            }

            if (nfind != -1)
            {
                String sCnt = listView1.Items[nfind].SubItems[(int)Index.cnt].Text;
                int nCnt = Convert.ToInt32(sCnt) + 1;
                String sumFind = nCnt.ToString();
                listView1.Items[nfind].SubItems[(int)Index.cnt].Text = sumFind;
            }
            else
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = (listView1.Items.Count + 1).ToString();
                lvi.SubItems.Add(sOnetagInfo);
                string sPid = sOnetagInfo;
                if (sOnetagInfo.Length == 24)
                {
                    sPid = sPid.Substring(12, 12);
                }
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems[(int)Index.epc].Text = sOnetagInfo;
                lvi.SubItems[(int)Index.pid].Text = sPid;
                string sAssId, sStat;
                GetAssInfo(sPid, out sAssId, out sStat);
                lvi.SubItems[(int)Index.stat].Text = sStat;
                lvi.SubItems[(int)Index.cnt].Text = "1";//读取次数
                lvi.SubItems[(int)Index.assid].Text = sAssId;
                this.listView1.Items.Add(lvi);
                listView1.EnsureVisible(listView1.Items.Count - 1);

                int nHasStat = 0;
                for (int k = 0; k < listView1.Items.Count; k++)
                {
                    string sTagStat = listView1.Items[k].SubItems[(int)Index.stat].Text;
                    if (sTagStat.Length != 0)
                    {
                        nHasStat++;
                    }
                    labelHit.Text = string.Format(@"总共: {0}张，有效: {1}张", listView1.Items.Count, nHasStat);
                }


                //this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度  
                //for (int i = 0; i < 10; i++)   //添加10行数据  
                //{
                //    ListViewItem lvi = new ListViewItem();
                //    lvi.Text = "subitem" + i;
                //    lvi.SubItems.Add("第2列,第" + i + "行");
                //    lvi.SubItems.Add("第3列,第" + i + "行");
                //    this.listView1.Items.Add(lvi);
                //}

                // this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘
            }
        }
        private void GetAssInfo(string sPid, out string sAssId, out string sStat)
        {
            sAssId = "";
            sStat = "";
            //SQLite方式
            string sSql = "select ass_id,stat from ass_list where pid = \'" + sPid + "\'";
            SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql, null);
            if (reader.Read())
            {
                sAssId = reader["ass_id"].ToString();
                sStat = reader["stat"].ToString();
            }
            reader.Close();
        }
        private string GetStat(string sPid)
        {
            string sStat = "";
            //SQLite方式
            string sSql = "select stat from ass_list where pid = \'" + sPid + "\'";
            SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql, null);
            if (reader.Read())
            {
                sStat = reader["stat"].ToString();
            }
            reader.Close();
            //XML方式
            //string sFilename = LoginForm.sCodePath + "\\ass_list.xml";
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(sFilename);
            //XmlNodeList xnl = xmlDoc.SelectSingleNode("ass_list").ChildNodes;
            //foreach (XmlNode xnf in xnl)
            //{
            //    XmlElement xe = (XmlElement)xnf;
            //    XmlNodeList xnf1 = xe.ChildNodes;
            //    //寻找PID
            //    bool bFindPid = false;
            //    foreach (XmlNode xn2 in xnf1)
            //    {
            //        if (xn2.Name == "pid")
            //        {
            //            if (xn2.InnerText == sPid)
            //            {
            //                bFindPid = true;
            //            }
            //            break;
            //        }
            //    }//xn2
            //    if (bFindPid)
            //    {
            //        foreach (XmlNode xn2 in xnf1)
            //        {
            //            if (xn2.Name == "stat")
            //            {
            //                sStat = xn2.InnerText;
            //                break;
            //            }
            //        }
            //        break;
            //    }
            //}
            return sStat;
        }
        //联网后发送数据到PC
        private void SendData()
        {
            listBox1.Items.Clear();

            PlaySound("\\Windows\\critical.wav", IntPtr.Zero, 0x0001);

            string sIp = SettingForm.sUdpIp;
            string sPort = SettingForm.sUdpPort;

            ShowStat(listBox1, "目标IP/端口：" + sIp + "/" + sPort);

            // 匿名模式(套接字绑定的端口由系统随机分配)
            sendUdpClient = new System.Net.Sockets.UdpClient(0);
            IPAddress remoteIp = IPAddress.Parse(SettingForm.sUdpIp);
            IPEndPoint remoteIpEndPoint = new IPEndPoint(remoteIp, int.Parse(SettingForm.sUdpPort));
            //发送循环读取画面数据
            for (int j = 0; j < listView1.Items.Count; j++)
            {
                string message2 = listView1.Items[j].SubItems[(int)Index.pid].Text;
                byte[] sendbytes2 = Encoding.Unicode.GetBytes(message2);
                sendUdpClient.Send(sendbytes2, sendbytes2.Length, remoteIpEndPoint);
                ShowStat(listBox1, "已发送：" + message2);
            }
            //发送单个读取画面数据
            string message1 = textBoxEpc.Text + "," + textBoxTid.Text;
            byte[] sendbytes1 = Encoding.Unicode.GetBytes(message1);
            sendUdpClient.Send(sendbytes1, sendbytes1.Length, remoteIpEndPoint);
            ShowStat(listBox1, "已发送：" + message1);
            sendUdpClient.Close();

            // MessageBox.Show("数据已发送至：" + sIp);


        }

        [DllImport("CoreDll.DLL")]
        private extern static int PlaySound(string szSound, IntPtr hMod, int flags);

        public static void StringToHexByte(string InputStr, byte[] OutPutByte)
        {
            if (InputStr.Length == 0)
                return;

            for (int strLen = 0; strLen < InputStr.Length / 2; strLen++)
                OutPutByte[strLen] = Convert.ToByte(InputStr.Substring(strLen * 2, 2), 16);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == (int)Page.onepcs)//单次读取
            {
                textBoxTid.Text = "";
                textBoxEpc.Text = "";
                labelMsg.Text = "";
            }
            else if (tabControl1.SelectedIndex == (int)Page.inv) //循环读取
            {
                listView1.Items.Clear();
            }
            else if (tabControl1.SelectedIndex == (int)Page.sync)//同步
            {
            }
            else if (tabControl1.SelectedIndex == (int)Page.check)//盘点
            {
                CheckOptForm checkoptform = new CheckOptForm();
                int nIndex = listView2.Items.IndexOf(listView2.FocusedItem);
                if (checkoptform.ShowDialog() == DialogResult.OK)
                {
                    string sErr;
                    bool bOK = false;
                    if (checkoptform.sOptTyp == "手动盘点")
                    {
                        bOK = checkAss(nIndex, checkoptform.sOptTyp, out sErr);
                       if (bOK)
                       {
                           MessageBox.Show("操作成功！");
                       }
                       else
                       {
                           MessageBox.Show("操作失败！\r\n" + sErr);
                       }
                    }
                    else if (checkoptform.sOptTyp == "报废" || checkoptform.sOptTyp == "报失")
                    {
                        GetReasonForm f = new GetReasonForm(checkoptform.sOptTyp);
                        f.ShowDialog();
                        if (f.ret != 0)
                        {
                            string sTyp = f.sTyp;
                            string sDept = f.sDept;
                            string sMan = f.sEmpNam;
                            string sAddr = f.sAddr;
                            string sReason = f.sReason;
                            List<string> listAssId = new List<string>();
                            string sAssId = listView2.Items[nIndex].SubItems[2].Text;
                            listAssId.Add(sAssId);               
                            bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, listAssId, out sErr);
                            if (bOK)
                            {
                                bOK = checkAss(nIndex, checkoptform.sOptTyp, out sErr);
                            }

                            if (bOK)
                            {
                                MessageBox.Show("操作成功！");
                            }
                            else
                            {
                                MessageBox.Show("操作失败！\r\n" + sErr);
                            }
                        }//GetReasonForm
                    }//f.sOptTyp
                }//f.ShowDialog()
            }//tabControl1.SelectedIndex
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (buttonSyncIni.Enabled && buttonSyncDiff.Enabled)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("初始化中同步进行中，请稍后！");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (nCurTab == (int)Page.inv)
            {
                if (bInving)
                {
                    InvFun();//停止寻卡线程
                }
            }
            else if (nCurTab == (int)Page.check)
            {
                if (bChecking)
                {
                    CheckFun();//停止寻卡线程
                }
            }

            if (tabControl1.SelectedIndex == (int)Page.onepcs)//单次读取
            {
                buttonRead.Text = "读取(F1)";
                buttonClear.Text = "清除(Ent)";
                if (bInving)
                {
                    InvFun();//停止寻卡线程
                }
            }
            else if (tabControl1.SelectedIndex == (int)Page.inv) //循环读取
            {
                buttonRead.Text = "开始(F1)";
                buttonClear.Text = "清除(Ent)";
                timer1.Enabled = true;
                label6.Visible = true;
            }
            else if (tabControl1.SelectedIndex == (int)Page.sync)//同步
            {
                buttonRead.Text = "发送(F1)";
                buttonClear.Text = "清除(Ent)";

            }
            else if (tabControl1.SelectedIndex == (int)Page.check)//盘点
            {
                buttonRead.Text = "开始(F1)";
                buttonClear.Text = "操作(Ent)";
            }
        }

        private void Read2PcForm_Closing(object sender, CancelEventArgs e)
        {
            if (bInving)
            {
                InvFun();//停止寻卡线程
            }
        }
        //private bool GetAssList()
        //{
        //    AssWebSrv.Service wbs = new AssWebSrv.Service();
        //    XmlNode root = wbs.GetAssListXml();
        //    if (root == null)
        //    {
        //        MessageBox.Show("资产清单同步失败！");
        //        return false;
        //    }
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(root.OuterXml);
        //    XmlNodeReader reader = new XmlNodeReader(doc);
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(reader);
        //    reader.Close();
        //    ds.WriteXml(LoginForm.sCodePath + "\\ass_list.xml");
        //    return true;
        //}
        //private bool GetEmp()
        //{
        //    AssWebSrv.Service wbs = new AssWebSrv.Service();
        //    XmlNode root = wbs.GetEmpXml();
        //    if (root == null)
        //    {
        //        MessageBox.Show("Emp同步失败！");
        //        return false;
        //    }
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(root.OuterXml);
        //    XmlNodeReader reader = new XmlNodeReader(doc);
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(reader);
        //    reader.Close();
        //    ds.WriteXml(LoginForm.sCodePath + "\\emp.xml");
        //    return true;
        //}
        //private bool GetUser()
        //{
        //    AssWebSrv.Service wbs = new AssWebSrv.Service();
        //    XmlNode root = wbs.GetUserXml();
        //    if (root == null)
        //    {
        //        MessageBox.Show("User同步失败！");
        //        return false;
        //    }
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(root.OuterXml);
        //    XmlNodeReader reader = new XmlNodeReader(doc);
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(reader);
        //    reader.Close();
        //    ds.WriteXml(LoginForm.sCodePath + "\\user.xml");
        //    return true;
        //}

        private void contextMenuShowDetail_Popup(object sender, EventArgs e)
        {
            MessageBox.Show("contextMenuShowDetail_Popup");
        }

        private void menuItemShowDetail_Click(object sender, EventArgs e)
        {
            //if (listView1.FocusedItem != null)
            //{
            //    int nIndex = listView1.Items.IndexOf(listView1.FocusedItem);
            //    string sPid = listView1.Items[nIndex].SubItems[1].Text;
            //    AssDetailForm fr = new AssDetailForm(sPid);
            //    fr.ShowDialog();
            //}
        }

        private void Read2PcForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                buttonCancel_Click(null, null);
            }
            else if (e.KeyCode == Keys.Insert || e.KeyCode == Keys.F1)//e.KeyCode.ToString().Equals("Insert")
            {
                buttonRead_Click(null, null);
            }
            else if (e.KeyCode == Keys.F4)
            {
                if (listView1.FocusedItem != null && tabControl1.SelectedIndex == (int)Page.inv)
                {
                    int nIndex = listView1.Items.IndexOf(listView1.FocusedItem);
                    string sPid = listView1.Items[nIndex].SubItems[(int)Index.pid].Text;
                    AssDetailForm fr = new AssDetailForm(sPid);
                    fr.ShowDialog();
                }
                else if (tabControl1.SelectedIndex == (int)Page.onepcs)
                {
                    AssDetailForm fr = new AssDetailForm(textBoxEpc.Text);
                    fr.ShowDialog();
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                buttonClear_Click(null, null);
            }

            if (tabControl1.SelectedIndex == (int)Page.inv) //循环读取
            {
                if (e.KeyCode == Keys.NumPad1)
                {
                    buttonApply_Click(null, null);
                }
                else if (e.KeyCode == Keys.NumPad2)
                {
                    buttonBorrow_Click(null, null);
                }
                else if (e.KeyCode == Keys.NumPad3)
                {
                    buttonRepair_Click(null, null);
                }
                else if (e.KeyCode == Keys.NumPad4)
                {
                    buttonRent_Click(null, null);
                }
                else if (e.KeyCode == Keys.NumPad5)
                {
                    buttonOut_Click(null, null);
                }
                else if (e.KeyCode == Keys.NumPad6)
                {
                    buttonReject_Click(null, null);
                }
                else if (e.KeyCode == Keys.NumPad7)
                {
                    buttonLose_Click(null, null);
                }
                else if (e.KeyCode == Keys.NumPad8)
                {
                    buttonDiscard_Click(null, null);
                }
                else if (e.KeyCode == Keys.NumPad9)
                {
                    buttonTransfer_Click(null, null);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            label6.Visible = false;
        }

        private bool AssChange(string sTyp, string sDept, string sMan, string sAddr, string sReason, List<string> listAssid, out string sErr)
        {
            List<string> listSql = new List<string>();
            List<string> listSqlLog = new List<string>();
            string sUpd = string.Format("update ass_list set stat = '{0}',use_man = '{1}',use_dept = '{2}',mod_tm = '{3}'", sTyp, sMan, sDept, LoginForm.getDateTime());
            if (sAddr.Length != 0)
            {
                sUpd += ",addr = \'" + sAddr + "\' ";
            }
            string sIns = string.Format("insert into ass_log(ass_id,opt_typ,opt_man,opt_date,cre_man,cre_tm,company,dept,reason,addr) select ass_id,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',addr from ass_list ", sTyp, sMan, LoginForm.getDate(), LoginForm.sUserName, LoginForm.getDateTime(), SettingForm.sCompany, sDept, sReason);
            foreach (string sAssId in listAssid)
            {
                if (sAssId.Length == 0) continue;
                //更新
                string sSqlUpd = sUpd + " where ass_id = '" + sAssId + "'";

                string sSqlUpdLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
                    sTyp, "0", sSqlUpd.Replace("'", "''"), SettingForm.sClientId, sAssId, LoginForm.getDateTime());

                //插入
                string sSqlIns = sIns + " where ass_id = '" + sAssId + "'";

                string sSqlInsLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
                    sTyp, "0", sSqlIns.Replace("'", "''"), SettingForm.sClientId, sAssId, LoginForm.getDateTime());

                listSql.Add(sSqlUpd);
                listSql.Add(sSqlIns);
                listSqlLog.Add(sSqlUpdLog);
                listSqlLog.Add(sSqlInsLog);
            }
            bool bOK = false;

            //if (LoginForm.bOnline)
            //{
            //    AssWebSrv.Service ws = new AssWebSrv.Service();
            //    bOK = ws.ExecuteNoQueryTran(listSql.ToArray(), out sErr);
            //}
            //else
            //{
            foreach (string sTmp in listSqlLog)
            {
                listSql.Add(sTmp);
            }
            bOK = SQLiteHelper.ExecuteNoQueryTran(listSql);
            sErr = SQLiteHelper.sLastErr;
            // }
            return bOK;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            GetReasonForm f = new GetReasonForm("领用");
            f.ShowDialog();
            if (f.ret != 0)
            {
                string sTyp = f.ret == 1 ? "领用" : "退领";
                string sDept = f.sDept;
                string sMan = f.sEmpNam;
                string sAddr = f.sAddr;
                string sReason = f.sReason;
                List<string> listAssId = new List<string>();
                for (int j = 0; j < listView1.Items.Count; j++)
                {
                    string sAssId = listView1.Items[j].SubItems[(int)Index.assid].Text;
                    listAssId.Add(sAssId);
                }
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, listAssId, out sErr);
                if (bOK)
                {
                    MessageBox.Show("操作成功！");
                    buttonClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr);
                }
            }
        }

        private void buttonBorrow_Click(object sender, EventArgs e)
        {
            GetReasonForm f = new GetReasonForm("借用");
            f.ShowDialog();
            if (f.ret != 0)
            {
                string sTyp = f.ret == 1 ? "借用" : "归还";
                string sDept = f.sDept;
                string sMan = f.sEmpNam;
                string sAddr = f.sAddr;
                string sReason = f.sReason;
                List<string> listAssId = new List<string>();
                for (int j = 0; j < listView1.Items.Count; j++)
                {
                    string sAssId = listView1.Items[j].SubItems[(int)Index.assid].Text;
                    listAssId.Add(sAssId);
                }
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, listAssId, out sErr);
                if (bOK)
                {
                    MessageBox.Show("操作成功！");
                    buttonClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr);
                }
            }
        }

        private void buttonRepair_Click(object sender, EventArgs e)
        {
            GetReasonForm f = new GetReasonForm("维修");
            f.ShowDialog();
            if (f.ret != 0)
            {
                string sTyp = f.ret == 1 ? "开始维修" : "结束维修";
                string sDept = f.sDept;
                string sMan = f.sEmpNam;
                string sAddr = f.sAddr;
                string sReason = f.sReason;
                List<string> listAssId = new List<string>();
                for (int j = 0; j < listView1.Items.Count; j++)
                {
                    string sAssId = listView1.Items[j].SubItems[(int)Index.assid].Text;
                    listAssId.Add(sAssId);
                }
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, listAssId, out sErr);
                if (bOK)
                {
                    MessageBox.Show("操作成功！");
                    buttonClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr);
                }
            }
        }

        private void buttonRent_Click(object sender, EventArgs e)
        {
            GetReasonForm f = new GetReasonForm("租还");
            f.ShowDialog();
            if (f.ret != 0)
            {
                string sTyp = f.sTyp;
                string sDept = f.sDept;
                string sMan = f.sEmpNam;
                string sAddr = f.sAddr;
                string sReason = f.sReason;
                List<string> listAssId = new List<string>();
                for (int j = 0; j < listView1.Items.Count; j++)
                {
                    string sAssId = listView1.Items[j].SubItems[(int)Index.assid].Text;
                    listAssId.Add(sAssId);
                }
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, listAssId, out sErr);
                if (bOK)
                {
                    MessageBox.Show("操作成功！");
                    buttonClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr);
                }
            }
        }

        private void buttonOut_Click(object sender, EventArgs e)
        {
            GetReasonForm f = new GetReasonForm("外出");
            f.ShowDialog();
            if (f.ret != 0)
            {
                string sTyp = f.ret == 1 ? "外出" : "返回";
                string sDept = f.sDept;
                string sMan = f.sEmpNam;
                string sAddr = f.sAddr;
                string sReason = f.sReason;
                List<string> listAssId = new List<string>();
                for (int j = 0; j < listView1.Items.Count; j++)
                {
                    string sAssId = listView1.Items[j].SubItems[(int)Index.assid].Text;
                    listAssId.Add(sAssId);
                }
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, listAssId, out sErr);
                if (bOK)
                {
                    MessageBox.Show("操作成功！");
                    buttonClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr);
                }
            }
        }

        private void buttonReject_Click(object sender, EventArgs e)
        {
            GetReasonForm f = new GetReasonForm("退返");
            f.ShowDialog();
            if (f.ret != 0)
            {
                string sTyp = f.sTyp;
                string sDept = f.sDept;
                string sMan = f.sEmpNam;
                string sAddr = f.sAddr;
                string sReason = f.sReason;
                List<string> listAssId = new List<string>();
                for (int j = 0; j < listView1.Items.Count; j++)
                {
                    string sAssId = listView1.Items[j].SubItems[(int)Index.assid].Text;
                    listAssId.Add(sAssId);
                }
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, listAssId, out sErr);
                if (bOK)
                {
                    MessageBox.Show("操作成功！");
                    buttonClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr);
                }
            }
        }

        private void buttonLose_Click(object sender, EventArgs e)
        {
            GetReasonForm f = new GetReasonForm("丢失");
            f.ShowDialog();
            if (f.ret != 0)
            {
                string sTyp = f.sTyp;
                string sDept = f.sDept;
                string sMan = f.sEmpNam;
                string sAddr = f.sAddr;
                string sReason = f.sReason;
                List<string> listAssId = new List<string>();
                for (int j = 0; j < listView1.Items.Count; j++)
                {
                    string sAssId = listView1.Items[j].SubItems[(int)Index.assid].Text;
                    listAssId.Add(sAssId);
                }
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, listAssId, out sErr);
                if (bOK)
                {
                    MessageBox.Show("操作成功！");
                    buttonClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr);
                }
            }
        }

        private void buttonDiscard_Click(object sender, EventArgs e)
        {
            GetReasonForm f = new GetReasonForm("报废");
            f.ShowDialog();
            if (f.ret != 0)
            {
                string sTyp = f.sTyp;
                string sDept = f.sDept;
                string sMan = f.sEmpNam;
                string sAddr = f.sAddr;
                string sReason = f.sReason;
                List<string> listAssId = new List<string>();
                for (int j = 0; j < listView1.Items.Count; j++)
                {
                    string sAssId = listView1.Items[j].SubItems[(int)Index.assid].Text;
                    listAssId.Add(sAssId);
                }
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, listAssId, out sErr);
                if (bOK)
                {
                    MessageBox.Show("操作成功！");
                    buttonClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr);
                }
            }
        }

        private void buttonTransfer_Click(object sender, EventArgs e)
        {
            GetReasonForm f = new GetReasonForm("转出");
            f.ShowDialog();
            if (f.ret != 0)
            {
                string sTyp = f.sTyp;
                string sDept = f.sDept;
                string sMan = f.sEmpNam;
                string sAddr = f.sAddr;
                string sReason = f.sReason;
                List<string> listAssId = new List<string>();
                for (int j = 0; j < listView1.Items.Count; j++)
                {
                    string sAssId = listView1.Items[j].SubItems[(int)Index.assid].Text;
                    listAssId.Add(sAssId);
                }
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, listAssId, out sErr);
                if (bOK)
                {
                    MessageBox.Show("操作成功！");
                    buttonClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr);
                }
            }
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            if (buttonHide.Text == "︽")
            {
                buttonHide.Text = "︾";
                listView1.Height = 101;
                buttonHide.Top = 101;
            }
            else
            {
                buttonHide.Text = "︽";
                listView1.Height = 188;
                buttonHide.Top = 188;
            }
            listView1.Focus();
        }
        private void buttonHideCheck_Click(object sender, EventArgs e)
        {
            if (buttonHideCheck.Text == "︽")
            {
                buttonHideCheck.Text = "︾";
                listView2.Top = 15;
                listView2.Height = 168;
                buttonHideCheck.Top = 0;
            }
            else
            {
                buttonHideCheck.Text = "︽";
                listView2.Top = 90;
                listView2.Height = 93;
                buttonHideCheck.Top = 75;
            }
            listView2.Focus();
        }

        private void buttonSyncDiff_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            ShowStat(listBox1, "正在同步，请勿关闭电源!");
            //启动同步线程
            buttonSyncDiff.Enabled = false;
            thrSyncDiff = new Thread(new ThreadStart(syncDiffThread));
            thrSyncDiff.Start();
        }
        //增量同步
        private void syncDiffThread()
        {
            AssWebSrv.Service ws = new AssWebSrv.Service();
            ws.Url = SettingForm.sWebSrvUrl;
            //获取本地待同步队列
            ShowStat(listBox1, "同步到服务器 ...");
            string sSql = "select * from sync_log where stat= '0' order by id asc";
            SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql);
            if (!reader.HasRows)
            {
                ShowStat(listBox1, "无待同步数据！");
            }
            else
            {
                List<string> listSql = new List<string>();
                string sId = "0";
                while (reader.Read())
                {
                    sId = reader["id"].ToString();
                    string sTyp = reader["typ"].ToString();
                    string sSqlContent = reader["sql_content"].ToString();
                    string sAssId = reader["ass_id"].ToString();

                    ShowStat(listBox1, "内容:" + sAssId + " " + sTyp);

                    listSql.Add(sSqlContent);

                    sSql = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
    sTyp, "0", sSqlContent.Replace("'", "''"), SettingForm.sClientId, sAssId, LoginForm.getDateTime());
                    listSql.Add(sSql);
                    listSql.Add(sSql);
                }
                reader.Close();

                ShowStat(listBox1, "提交...");
                string sLastErr;
                bool bOK = ws.ExecuteNoQueryTran(listSql.ToArray(), out sLastErr);
                if (bOK)
                {
                    sSql = "update sync_log set stat = '1' where stat = '0' and id <= " + sId;
                    int nReslt = SQLiteHelper.ExecuteNonQuery(sSql,null);
                    if (nReslt > 0)
                    {
                        ShowStat(listBox1, "提交OK!");
                    }
                    else
                    {
                        ShowStat(listBox1, "本地状态更新失败!" + SQLiteHelper.sLastErr);
                    }
                }
                else
                {
                    ShowStat(listBox1, "提交失败!" + sLastErr);
                }
            }
            //获取服务器待同步队列
            ShowStat(listBox1, "");
            ShowStat(listBox1, "同步到本地 ...");
            sSql = "select * from sync_log where client_id != '" + SettingForm.sClientId + "'  and id > " + SettingForm.sSyncMax + " order by id ASC";
            DataTable dt = ws.GetDataTableBySql(sSql);

            if (dt.Rows.Count == 0)
            {
                ShowStat(listBox1, "无待同步数据！");
            }
            else
            {
                List<string> listSql = new List<string>();
                string sMax = SettingForm.sSyncMax;
                foreach (DataRow dataRow in dt.Rows)
                {
                    sMax = dataRow["id"].ToString();
                    string sTyp = dataRow["typ"].ToString();
                    string sSqlContent = dataRow["sql_content"].ToString();
                    string sAssId = dataRow["ass_id"].ToString();
                    ShowStat(listBox1, "内容:" + sAssId + " " + sTyp);
                    listSql.Add(sSqlContent);
                    Console.Out.WriteLine(sSqlContent);
                }
                listSql.Add(string.Format("update sys_parms set parm_val = '{0}' where parm_id = '{1}'", sMax, "sync_max"));

                ShowStat(listBox1, "提交...");
                bool bOK = SQLiteHelper.ExecuteNoQueryTran(listSql);
                if (bOK)
                {
                    SettingForm.sSyncMax = sMax;
                    ShowStat(listBox1, "提交OK!");
                }
                else
                {
                    ShowStat(listBox1, "提交失败!" + SQLiteHelper.sLastErr);
                    Console.Out.WriteLine(SQLiteHelper.sLastErr);
                    MessageBox.Show(SQLiteHelper.sLastErr);
                }
            }
            ShowStat(listBox1, "同步完成！");
            ShowStat(listBox1, "finish-diff");

        }
        //初始化同步
        private void buttonSyncIni_Click(object sender, EventArgs e)
        {
            //提示
            if (DialogResult.Yes != MessageBox.Show("初始化会将本地基本资料重置!\r\n可能会导致本地部分操作丢失!\r\n请谨慎操作！是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                return;
            }
            //初始化数据
            listBox1.Items.Clear();
            ShowStat(listBox1, "正在同步，请勿关闭电源!");
            //启动同步线程
            buttonSyncIni.Enabled = false;
            bSyncing = true;
            thrSyncIni = new Thread(new ThreadStart(syncAllThread));
            thrSyncIni.Start();
        }
        private void syncAllThread()
        {
            AssWebSrv.Service ws = new AssWebSrv.Service();
            ws.Url = SettingForm.sWebSrvUrl;
            //同步资产清单
            syncFun(ws, "ass_list", "资产清单");
            //同步人员资料
            syncFun(ws, "emp", "人员资料");
            //同步账户信息
            syncFun(ws, "user", "账户信息");
            //同步地址信息
            syncFun(ws, "addr", "地址信息");
            //同步盘点清单
            syncFun(ws, "inv_list", "盘点清单");
            //同步资产历史
            syncFun(ws, "ass_log", "资产历史");
            //同步系统参数
            syncFun(ws, "sys_parms", "系统参数");

            //参数设置
            ShowStat(listBox1, "参数更新及恢复 ...");

            //获取同步号
            string sSql = "select max(id) maxid from sync_log";
            DataTable dt = ws.GetDataTableBySql(sSql);
            string sSyncMax = "";
            if (dt != null)
            {
                sSyncMax = dt.Rows[0]["maxid"].ToString();
            }

            if (sSyncMax.Length == 0)
            {
                ShowStat(listBox1, "获取同步号失败！");
            }
            else
            {
                ShowStat(listBox1, "同步号更新：" + sSyncMax);
                ShowStat(listBox1, "本地同步队列删除");
                ShowStat(listBox1, "客户端ID恢复：" + SettingForm.sWebSrvIp);
                ShowStat(listBox1, "Web Service恢复：" + SettingForm.sWebSrvIp);
                ShowStat(listBox1, "UDP IP恢复：" + SettingForm.sUdpIp);
                ShowStat(listBox1, "UDP 端口恢复：" + SettingForm.sUdpPort);
            }


            List<string> listSql = new List<string>();
            listSql.Add(string.Format("update sys_parms set parm_val = '{0}' where parm_id = '{1}'", sSyncMax, "sync_max"));
            listSql.Add(string.Format("update sys_parms set parm_val = '{0}' where parm_id = '{1}'", SettingForm.sClientId, "client_id"));
            listSql.Add(string.Format("update sys_parms set parm_val = '{0}' where parm_id = '{1}'", SettingForm.sWebSrvIp, "websrv_ip"));
            listSql.Add(string.Format("update sys_parms set parm_val = '{0}' where parm_id = '{1}'", SettingForm.sUdpIp, "udp_ip"));
            listSql.Add(string.Format("update sys_parms set parm_val = '{0}' where parm_id = '{1}'", SettingForm.sUdpPort, "udp_port"));
            listSql.Add("delete from sync_log");

            bool bOK = SQLiteHelper.ExecuteNoQueryTran(listSql);
            if (bOK)
            {
                SettingForm.sSyncMax = sSyncMax;
            }

            ShowStat(listBox1, bOK ? "参数OK！" : "参数恢复失败，请重新操作！");
            ShowStat(listBox1, "--------------");

            ShowStat(listBox1, "全部同步完成！");
            ShowStat(listBox1, "");
            ShowStat(listBox1, "finish-all");
        }
        private bool syncFun(AssWebSrv.Service ws, string sTblName, string sTblNameCn)
        {
            ShowStat(listBox1, sTblNameCn + "同步中 ...");
            string sSql = "select * from " + sTblName;
            DataTable dt = ws.GetDataTableBySql(sSql);

            if (dt != null)
            {
                ShowStat(listBox1, "Loading finished!");
            }
            else
            {
                ShowStat(listBox1, "数据加载失败!");
                return false;
            }

            sSql = "delete from " + sTblName;
            SQLiteHelper.ExecuteNonQuery(sSql);
            ShowStat(listBox1, "Local data deleted!");

            SQLiteHelper.SqlBulkInsert(dt, sTblName);
            ShowStat(listBox1, "Local data have updated!");
            ShowStat(listBox1, "同步完成！");
            ShowStat(listBox1, "--------------");
            return true;
        }
        delegate void ShowStatCallBack(ListBox lstBox, string str);
        private void ShowStat(ListBox lstBox, string str)
        {
            if (lstBox.InvokeRequired)
            {
                ShowStatCallBack ShowStatCallBack = ShowStat;
                lstBox.Invoke(ShowStatCallBack, new object[] { lstBox, str });
            }
            else
            {

                if (str.Equals("finish-all"))
                {
                    buttonSyncIni.Enabled = true; 
                    int nIdex = lstBox.Items.Add(str);
                    bSyncing = false;
                }
                else if (str.Equals("finish-diff"))
                {
                    buttonSyncDiff.Enabled = true; int nIdex = lstBox.Items.Add(str);
                }
                else
                {
                    int nIdex = 0;
                    if (str.Length == 0)
                    {
                        nIdex = lstBox.Items.Add("");
                    }
                    else
                    {
                        string sLine = DateTime.Now.ToString("mm:ss") + " " + str;
                        nIdex = lstBox.Items.Add(sLine);
                        //滚动到底部
                        listBox1.SelectedIndex = nIdex; //  listBox1.SelectedIndex = -1;
                    }
                    if (nIdex > 1000)
                        listBox1.Items.Clear();
                }
            }
        }

        private void comboBoxInvListNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxDept.Items.Clear();
            string sSql = "select distinct use_dept from inv_list where inv_no = \'" + comboBoxInvListNo.Text + "\'  order by use_dept";
            SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxDept.Items.Add(reader["use_dept"].ToString());
            }
            reader.Close();
            comboBoxDept.Items.Add("");

            comboBoxMan.Items.Clear();
            sSql = "select distinct use_man from inv_list where inv_no = \'" + comboBoxInvListNo.Text + "\' order by use_man";
            reader = SQLiteHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxMan.Items.Add(reader["use_man"].ToString());
            }
            reader.Close();
            comboBoxMan.Items.Add("");

            comboBoxAddr.Items.Clear();
            sSql = "select distinct addr from inv_list where inv_no = \'" + comboBoxInvListNo.Text + "\' order by addr";
            reader = SQLiteHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxAddr.Items.Add(reader["addr"].ToString());
            }
            reader.Close();
            comboBoxAddr.Items.Add("");

            GetInvList();
        }
        private void GetInvList()
        {
            bool blist = true; 
            nAll = 0;
            nExp = 0;
            nCheck = 0;
            listView2.Items.Clear();
            string sSql = "select * from inv_list where inv_no = \'" + comboBoxInvListNo.Text + "\'";
            if (comboBoxDept.Text.Length != 0)
            {
                sSql += " and use_dept = \'" + comboBoxDept.Text + "\'";
                blist = false;
            }
            if (comboBoxMan.Text.Length != 0)
            {
                sSql += " and use_man = \'" + comboBoxMan.Text + "\'";
                blist = false;
            }
            if (comboBoxAddr.Text.Length != 0)
            {
                sSql += " and addr = \'" + comboBoxAddr.Text + "\'";
                blist = false;
            }

            SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                //listView2.FullRowSelect = true;
                //listView2.Columns.Add("N", 16, HorizontalAlignment.Left);
                //listView2.Columns.Add("喷码", 96, HorizontalAlignment.Left);
                //listView2.Columns.Add("资产编码", 60, HorizontalAlignment.Left);
                //listView2.Columns.Add("资产名称", 60, HorizontalAlignment.Left);
                //listView2.Columns.Add("领用状态", 40, HorizontalAlignment.Left);
                //listView2.Columns.Add("领用人员", 40, HorizontalAlignment.Left);
                //listView2.Columns.Add("所属部门", 60, HorizontalAlignment.Left);
                //listView2.Columns.Add("所在地点", 60, HorizontalAlignment.Left);
                //listView2.Columns.Add("盘点结果", 40, HorizontalAlignment.Left);
                //listView2.Columns.Add("ID", 0, HorizontalAlignment.Left);
                nAll++;
                ListViewItem lvi = new ListViewItem();
                string sResult = reader["result"].ToString();
                lvi.Text = (listView2.Items.Count + 1).ToString();
                if (sResult == "自动盘点")
                {
                    lvi.BackColor = Color.FromArgb(124, 255, 0);
                    nCheck ++;
                }
                else if (sResult == "手动盘点")
                {
                    lvi.BackColor = Color.FromArgb(0, 255, 0);
                    nCheck++;
                }
                else if (sResult == "丢失")
                {
                    lvi.BackColor = Color.FromArgb(255,0,0);
                    nCheck++;
                }
                else if (sResult == "报废")
                {
                    lvi.BackColor = Color.FromArgb(255, 69, 0);
                    nExp++;
                }
                lvi.SubItems.Add(reader["pid"].ToString());
                lvi.SubItems.Add(reader["ass_id"].ToString());
                lvi.SubItems.Add(reader["ass_nam"].ToString());
                lvi.SubItems.Add(reader["stat"].ToString());
                lvi.SubItems.Add(reader["use_man"].ToString());
                lvi.SubItems.Add(reader["use_dept"].ToString());
                lvi.SubItems.Add(reader["addr"].ToString());
                lvi.SubItems.Add(sResult);
                lvi.SubItems.Add(reader["id"].ToString());
                this.listView2.Items.Add(lvi);
                //listView2.EnsureVisible(listView1.Items.Count - 1);
            }
            reader.Close();
            if (blist)
            {
                label8.Text = "总数：" + nAll.ToString();
            }
            label4.Text = string.Format("总计 {0};已盘 {1};异常 {2};未盘点 {3}.", nAll, nCheck, nExp, nAll - nCheck);
        }

        private void comboBoxDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetInvList();
        }

        private void comboBoxAddr_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetInvList();
        }

        private void comboBoxMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetInvList();
        }
    }
}