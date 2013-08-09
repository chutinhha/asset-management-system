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

namespace AssMngSysCe
{
    public partial class MainForm : Form
    {
        static class MngIndex
        {
            public const int no = 0;
            public const int epc = 1;
            public const int pid = 2;
            public const int assnam = 3;
            public const int stat = 4;
            public const int statsub = 5;
            public const int cnt = 6;
            public const int assid = 7;

        }
        static class ChkIndex
        {
            public const int no = 0;
            public const int pid = 1;
            public const int assid = 2;
            public const int assnam = 3;
            public const int stat = 4;
            public const int dutyman = 5;
            public const int dept = 6;
            public const int addr = 7;
            public const int result = 8;
            public const int id = 9;
        }
        static public bool bHasSrv = false;
        public bool bIsFirstIni = false;
        Thread thrSyncIni;
        Thread thrSyncDiff;
        public bool bSyncing = false;
        public delegate int MyDelegate(string message);


        Thread thrCheck;
        public bool bChecking = false;
        int nAll = 0, nExp = 0, nCheck = 0;
        bool bIsCheckHide = false;

        Thread thrInv;
        public bool bInving = false;
        public delegate void InvokeDelegate();
        String sOnetagInfo;

        static string sIp;
        static string sPort;
        enum Page { wrtag = 0, onepcs, inv, check, sync };
        int nCurTab = 0;
        LoginForm loginForm = null;
        private System.Net.Sockets.UdpClient sendUdpClient;

        public string sBarcode;
        HTApi.PWSCAN_CALLBACK scanCall;

        public MainForm(LoginForm f)
        {
            InitializeComponent();
            loginForm = f;


        }
        private void Read2PcForm_Load(object sender, EventArgs e)
        {
            
            this.WindowState = FormWindowState.Maximized;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("序", 16, HorizontalAlignment.Left);
            listView1.Columns.Add("EPC", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("喷码", 96, HorizontalAlignment.Left);
            listView1.Columns.Add("资产名称", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("库存状态", 38, HorizontalAlignment.Left);
            listView1.Columns.Add("使用状态", 38, HorizontalAlignment.Left);
            listView1.Columns.Add("计数", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("资产编码", 60, HorizontalAlignment.Left);


            listView2.FullRowSelect = true;
            listView2.Columns.Add("序", 16, HorizontalAlignment.Left);
            listView2.Columns.Add("喷码", 96, HorizontalAlignment.Left);
            listView2.Columns.Add("资产编码", 60, HorizontalAlignment.Left);
            listView2.Columns.Add("资产名称", 60, HorizontalAlignment.Left);
            listView2.Columns.Add("库存状态", 40, HorizontalAlignment.Left);
            listView2.Columns.Add("保管人员", 40, HorizontalAlignment.Left);
            listView2.Columns.Add("所属部门", 60, HorizontalAlignment.Left);
            listView2.Columns.Add("所在地点", 60, HorizontalAlignment.Left);
            listView2.Columns.Add("盘点结果", 40, HorizontalAlignment.Left);
            listView2.Columns.Add("ID", 0, HorizontalAlignment.Left);

           // iniData();

            tabControl1.SelectedIndex = (int)Page.sync;

            if (bIsFirstIni)
            {
                //tabControl1.SelectedIndex = (int)Page.sync;
                buttonSyncIni_Click(null, null);
            }
            else
            {
                timer2.Enabled = true;
                ShowStat(listBox1, "自动同步中 ...");
            }

            //scanCall = new HTApi.PWSCAN_CALLBACK(ScanCallBack);
            //HTApi.WScanSetCallBack(scanCall);

            //HTApi.WScanOpenPower();
            //HTApi.W1DScanConnect(2);
            ////设置声音
            //HTApi.WScanSetSound(true); ;

        }
        private void iniData()
        {
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
                Scanner.openUHF();
                readData();
            }
            else if (tabControl1.SelectedIndex == (int)Page.inv) //循环读取
            {
                Scanner.openUHF();
                InvFun();
            }
            else if (tabControl1.SelectedIndex == (int)Page.sync)//发送数据到PC
            {
                SendData();
            }
            else if (tabControl1.SelectedIndex == (int)Page.check)//发送数据到PC
            {
                Scanner.openUHF();
                CheckFun();
            }
            else if (tabControl1.SelectedIndex == (int)Page.wrtag)//发卡
            {
                Scanner.openUHF();
                WriteTag();
            }
        }
        private void WriteTag()
        {
            if (textBoxPid.Text.Length == 0)
            {
                MessageBox.Show("请先获取标签喷码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                return;
            }
            else if (textBoxAssId.Text.Length == 0)
            {
                MessageBox.Show("该资产不存在，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                return;
            }
            //else if (textBoxYnWrite.Text == "Y")
            //{
            //    if (MessageBox.Show("该标签已发，是否继续？",
            //        "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes) ;
            //    {
            //        return;
            //    }
            //}
            string sWriteData = "000000000000" + textBoxPid.Text;
            byte[] writedata = new byte[100];
            StringToHexByte(sWriteData.Trim(), writedata);//转换为16进制

            //EPC
            byte uBank = 1;
            byte _offset = 2;
            byte _length = 6;
            bool bOK = true;
            //1.写入
            if (1 == HTApi.WIrUHFWriteData(uBank, _offset, _length, ref writedata[0]))
            {
                bOK = true;
                label1WriteMsg.Text = "*写卡成功!";
                PlaySound("\\Windows\\critical.wav", IntPtr.Zero, 0x0001);
            }
            else
            {
                bOK = false;
                label1WriteMsg.Text = "*写卡失败!";

            }

            if (!bOK)
            {
                return;
            }

            label1WriteMsg.Update();

            ////2.写锁
            //byte[] epc = new byte[100];
            //StringToHexByte(sWriteData, epc);
            //byte uSelect = 0;//epc
            //byte uAction = 2;//写锁，密码为0可写，密码非0时，需输入正确密码才可写
            //byte[] password = new byte[100];
            ////转换为16进制
            //string sPass = "88888888";
            //StringToHexByte(sPass, password);
            //if (1 == HTApi.WIrUHFLockTag(uSelect, uAction, ref password[0], 12, ref epc[0]))
            //{
            //    bOK = false;
            //    PlaySound("\\Windows\\critical.wav", IntPtr.Zero, 0x0001);
            //    label1WriteMsg.Text = "*锁定成功!";
            //}
            //else
            //{
            //    bOK = false;
            //    label1WriteMsg.Text = "*锁定失败!";
            //}

            //if (!bOK)
            //{
            //    return;
            //}

            //label1WriteMsg.Update();

            //3.更新DB
            List<string> listSql = new List<string>();
            string sAssId = textBoxAssId.Text;
            string sSql = string.Format("update ass_list set ynwrite = 'Y' where ass_id = '{0}' and ynenable = 'Y'", sAssId);
            listSql.Add(sSql);
            string sSqlLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
                "发卡", "0", sSql.Replace("'", "''"), SettingForm.sClientId, sAssId, LoginForm.getDateTime());
            listSql.Add(sSqlLog);

            bOK = SQLiteHelper.ExecuteNoQueryTran(listSql);
            string sErr = SQLiteHelper.sLastErr;
            if (bOK)
            {
                label1WriteMsg.Text = "*发卡OK! 标签喷码:" + textBoxPid.Text;
                PlaySound("\\Windows\\critical.wav", IntPtr.Zero, 0x0001);
                textBoxPid.Text = "";
            }
            else
            {
                label1WriteMsg.Text = "*发卡失败!";
            }


        }
        private void CheckFun()
        {
            if (bChecking == false)
            {
                buttonRead.Text = "停止(F1)";
                bChecking = true;
                thrCheck = new Thread(new ThreadStart(checkThread));
                thrCheck.Start();
            }
            else
            {
                buttonRead.Text = "开始(F1)";
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
                string findItem = listView2.Items[j].SubItems[ChkIndex.pid].Text;
                if (sPid == findItem)
                {
                    nfind = j;
                    break;
                }
            }
            if (nfind != -1)
            {
                if (listView2.Items[nfind].SubItems[ChkIndex.result].Text.Length == 0)
                {
                    string sErr;
                    checkAss(nfind, "自动盘点", out sErr);
                    buttonSyncDiff_Click(null, null);
                }
                listView2.Items[nfind].Selected = true;
            }
            else
            {
            }

        }
        private bool checkAss(int nIndex, string sTpe, out string sErr)
        {
            //找到显示绿色
            sErr = "";
            List<string> listSql = new List<string>();
            string sAssId = listView2.Items[nIndex].SubItems[ChkIndex.assid].Text;
            string sId = listView2.Items[nIndex].SubItems[ChkIndex.id].Text;
            //更新盘点清单
            string sSql = "update inv_list set result = '" + sTpe + "' where id = " + sId;
            listSql.Add(sSql);
            //更新新资产清单
            sSql = string.Format("update ass_list set inv_result = '{0}',inv_man = '{1}',inv_date = '{2}' where id = {3}", sTpe, LoginForm.sUserName, LoginForm.getDate(), sId);
            listSql.Add(sSql);
            //增加资产历史记录
            sSql = string.Format("insert into ass_log(ass_id,opt_typ,opt_man,opt_date,cre_man,cre_tm,company,dept,reason,addr) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}') ",
                sAssId, "盘点", LoginForm.sUserName, LoginForm.getDate(), LoginForm.sUserName, LoginForm.getDateTime(), SettingForm.sCompany,
                listView2.Items[nIndex].SubItems[ChkIndex.dept].Text, sTpe, "");
            listSql.Add(sSql);

            int nCnt = listSql.Count;
            for (int i = 0; i < nCnt; i++)
            {
                sSql = listSql[i];
                string sSqlLog = string.Format("insert into sync_log(typ,stat,sql_content,client_id,ass_id,cre_tm)values('{0}','{1}','{2}','{3}','{4}','{5}')",
                "资产盘点", "0", sSql.Replace("'", "''"), SettingForm.sClientId, sAssId, LoginForm.getDateTime());
                listSql.Add(sSqlLog);
            }

            bool bOK = SQLiteHelper.ExecuteNoQueryTran(listSql);
            sErr = SQLiteHelper.sLastErr;
            if (bOK)
            {
                listView2.Items[nIndex].SubItems[ChkIndex.result].Text = sTpe;
                if (sTpe == "自动盘点")
                {
                    listView2.Items[nIndex].BackColor = Color.FromArgb(0x99CC66);
                    nCheck++;
                }
                else if (sTpe == "手动盘点")
                {
                    listView2.Items[nIndex].BackColor = Color.FromArgb(0x0099CC);
                    nCheck++;
                }
                else if (sTpe == "丢失")
                {
                    listView2.Items[nIndex].BackColor = Color.FromArgb(0xFFFF66);
                    nExp++;
                }
                else if (sTpe == "报废")
                {
                    listView2.Items[nIndex].BackColor = Color.FromArgb(0xFF6666);
                    nExp++;
                }
                sumCheckText();
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
            //读取EPC
            byte uBank = 1;
            byte _offset = 2;
            byte _length = 6;
            bool bOK = true;

            Array.Clear(nTagCount, 0, 1);
            Array.Clear(uReadData, 0, 512);
            if (1 == HTApi.WIrUHFReadData(uBank, _offset, _length, ref nTagCount[0], ref uReadData[0]))
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

                buttonRead.Text = "停止(F1)";
            }
            else
            {
                buttonRead.Text = "开始(F1)";
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
                string findItem = listView1.Items[j].SubItems[(int)MngIndex.epc].Text;
                if (sOnetagInfo == findItem)
                {
                    nfind = j;
                    break;
                }
            }

            if (nfind != -1)
            {
                String sCnt = listView1.Items[nfind].SubItems[(int)MngIndex.cnt].Text;
                int nCnt = Convert.ToInt32(sCnt) + 1;
                String sumFind = nCnt.ToString();
                listView1.Items[nfind].SubItems[(int)MngIndex.cnt].Text = sumFind;
                listView1.Items[nfind].Selected = true;
                listView1.Items[nfind].Focused = true; ;
                listView1.EnsureVisible(nfind);
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
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems[(int)MngIndex.epc].Text = sOnetagInfo;
                lvi.SubItems[(int)MngIndex.pid].Text = sPid;
                string sAssId = "", sAssNam = "", sStat = "", sStatSub = "";
                //SQLite方式
                string sSql = "select ass_id,ass_nam,stat,stat_sub,ynwrite from ass_list where pid = \'" + sPid + "\'  and ynenable = 'Y'";
                SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql, null);
                if (reader.Read())
                {
                    sAssId = reader["ass_id"].ToString();
                    sStat = reader["stat"].ToString();
                    sStatSub = reader["stat_sub"].ToString();
                    sAssNam = reader["ass_nam"].ToString();
                }
                reader.Close();

                lvi.SubItems[(int)MngIndex.stat].Text = sStat;
                lvi.SubItems[(int)MngIndex.statsub].Text = sStatSub;
                lvi.SubItems[(int)MngIndex.cnt].Text = "1";//读取次数
                lvi.SubItems[(int)MngIndex.assid].Text = sAssId;
                lvi.SubItems[(int)MngIndex.assnam].Text = sAssNam;
                this.listView1.Items.Add(lvi);
                listView1.Items[listView1.Items.Count - 1].Selected = true;
                listView1.Items[listView1.Items.Count - 1].Focused = true; ;;
                listView1.EnsureVisible(listView1.Items.Count - 1);

                int nHasStat = 0;
                for (int k = 0; k < listView1.Items.Count; k++)
                {
                    string sTagStat = listView1.Items[k].SubItems[(int)MngIndex.stat].Text;
                    if (sTagStat.Length != 0)
                    {
                        nHasStat++;
                    }
                    labelHit.Text = string.Format("{0}/{1}", nHasStat, listView1.Items.Count);
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
        private void GetAssInfo(string sPid, out string sAssId, out string sStat, out string sStatSub, out string sYnWrite)
        {
            sAssId = "";
            sStat = "";
            sStatSub = "";
            sYnWrite = "";

            //SQLite方式
            string sSql = "select ass_id,stat,stat_sub,ynwrite from ass_list where pid = \'" + sPid + "\'  and ynenable = 'Y'";
            SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql, null);
            if (reader.Read())
            {
                sAssId = reader["ass_id"].ToString();
                sStat = reader["stat"].ToString();
                sStatSub = reader["stat_sub"].ToString();
                sYnWrite = reader["ynwrite"].ToString();
            }
            reader.Close();
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
                string message2 = listView1.Items[j].SubItems[(int)MngIndex.pid].Text;
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

            // MessageBox.Show("数据已发送至：" + sIp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);


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
                labelHit.Text = "0/0";
            }
            else if (tabControl1.SelectedIndex == (int)Page.sync)//同步
            {
                listBox1.Items.Clear();
            }
            else if (tabControl1.SelectedIndex == (int)Page.check)//盘点
            {
                CheckOpt();
            }
            else if (tabControl1.SelectedIndex == (int)Page.wrtag)//盘点
            {
                textBoxPid.Text = "";
            }
        }

        private void CheckOpt()
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
                        MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        buttonSyncDiff_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("操作失败！\r\n" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (checkoptform.sOptTyp == "报废" || checkoptform.sOptTyp == "丢失")
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
                        //List<string> listAssId = new List<string>();
                        string sPid = listView2.Items[nIndex].SubItems[ChkIndex.pid].Text;
                        //string sAssId = listView2.Items[nIndex].SubItems[ChkIndex.assid].Text;
                        //listAssId.Add(sAssId);
                        bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, sPid);

                        if (bOK)
                        {
                            bOK = checkAss(nIndex, checkoptform.sOptTyp, out sErr);
                        }

                        if (bOK)
                        {
                            MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                            buttonSyncDiff_Click(null, null);
                        }
                        else
                        {
                            MessageBox.Show("操作失败！\r\n" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        }
                    }//GetReasonForm
                }//f.sOptTyp
            }//f.ShowDialog()
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (buttonSyncIni.Enabled && buttonSyncDiff.Enabled)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("同步进行中，请稍后！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
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

            nCurTab = tabControl1.SelectedIndex;
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
            else if (tabControl1.SelectedIndex == (int)Page.wrtag)//盘点
            {
                buttonRead.Text = "发卡(F1)";
                buttonClear.Text = "清除(Ent)";
            }
        }

        private void Read2PcForm_Closing(object sender, CancelEventArgs e)
        {
            if (bInving)
            {
                InvFun();//停止寻卡线程
            }
            Scanner.closeUHF();
            Scanner.close1D();
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
            MessageBox.Show("contextMenuShowDetail_Popup", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
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
                buttonDetail_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                buttonClear_Click(null, null);
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (listView1.FocusedItem != null && tabControl1.SelectedIndex == (int)Page.inv)
                {
                    int nIndex = listView1.Items.IndexOf(listView1.FocusedItem);
                    listView1.Items.RemoveAt(nIndex);
                    int nHasStat = 0;
                    for (int k = 0; k < listView1.Items.Count; k++)
                    {
                        string sTagStat = listView1.Items[k].SubItems[(int)MngIndex.stat].Text;
                        if (sTagStat.Length != 0)
                        {
                            nHasStat++;
                        }
                        labelHit.Text = string.Format(@"{0}/{1}", nHasStat, listView1.Items.Count);
                    }
                }
            }

            if (tabControl1.SelectedIndex == (int)Page.inv) //循环读取
            {
                //if (e.KeyCode == Keys.NumPad1)
                //{
                //    buttonApply_Click(null, null);
                //}
                //else if (e.KeyCode == Keys.NumPad2)
                //{
                //    buttonBorrow_Click(null, null);
                //}
                //else if (e.KeyCode == Keys.NumPad3)
                //{
                //    buttonRepair_Click(null, null);
                //}
                //else if (e.KeyCode == Keys.NumPad4)
                //{
                //    buttonRent_Click(null, null);
                //}
                //else if (e.KeyCode == Keys.NumPad5)
                //{
                //    buttonOut_Click(null, null);
                //}
                //else if (e.KeyCode == Keys.NumPad6)
                //{
                //    buttonReject_Click(null, null);
                //}
                //else if (e.KeyCode == Keys.NumPad7)
                //{
                //    buttonLose_Click(null, null);
                //}
                //else if (e.KeyCode == Keys.NumPad8)
                //{
                //    buttonDiscard_Click(null, null);
                //}
                //else if (e.KeyCode == Keys.NumPad9)
                //{
                //    buttonTransfer_Click(null, null);
                //}
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            label6.Visible = false;
        }
        static public bool CheckStat(string sNewOpt, string sStat, string sStatSub, out string sErr)
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
        private bool AssChange(string sTyp, string sDept, string sMan, string sAddr, string sReason, out string sErr, string sFixPid)
        {
            sErr = "";
            List<string> listSql = new List<string>();
            List<string> listSqlLog = new List<string>();
            List<string> listAssId = new List<string>();
            string sUpd;
            if (sTyp == "领用")// )
            {
                sUpd = string.Format("update ass_list set stat = '领用',use_man = '{0}',duty_man = '{1}',dept = '{2}',mod_man = '{3}',mod_tm = '{4}'", sMan, sMan, sDept, LoginForm.sUserName, LoginForm.getDateTime());
                if (sAddr.Length != 0)
                {
                    sUpd += ",addr = '" + sAddr + "' ";
                }
            }
            else if (sTyp == "退领")
            {
                sUpd = string.Format("update ass_list set stat = '库存',use_man = '',duty_man = '',dept = '',mod_man = '{0}',mod_tm = '{1}'", LoginForm.sUserName, LoginForm.getDateTime());
            }
            else if (sTyp == "租还" || sTyp == "退返" || sTyp == "丢失" || sTyp == "报废" || sTyp == "转出")
            {
                sUpd = string.Format("update ass_list set stat = '{0}',mod_man = '{1}',mod_tm = '{2}'", sTyp, sMan, LoginForm.getDateTime());
            }
            else//借用，归还，送修，修返，外出，返回
            {
                sUpd = string.Format("update ass_list set stat_sub = '{0}',duty_man = '{1}',dept = '{2}',mod_tm = '{3}'", sTyp, sMan, sDept, LoginForm.getDateTime());
                if (sTyp == "送修") sUpd += ",ynrepair = 'Y'";
            }

            string sSql = "";
            int nCount;
            if (sFixPid.Length == 0)
            {
                nCount = listView1.Items.Count;
            }
            else //盘点页面报废、丢失动作调用
            {
                nCount = 1;//只执行一次
            }
            bool bOK = true;
            for (int i = 0; i < nCount; i++)
            {
                string sPid,sAssId, sStat, sStatSub;
                if (sFixPid.Length == 0)
                {
                    sPid = listView1.Items[i].SubItems[(int)MngIndex.pid].Text;
                    sAssId = listView1.Items[i].SubItems[(int)MngIndex.assid].Text;
                    sStat = listView1.Items[i].SubItems[(int)MngIndex.stat].Text;
                    sStatSub = listView1.Items[i].SubItems[(int)MngIndex.statsub].Text;
                }
                else
                {
                    string sYnWrite;
                    sPid = sFixPid;
                    GetAssInfo(sFixPid, out sAssId, out sStat, out sStatSub, out sYnWrite);
                }

                if (sAssId.Length == 0) continue;

                //验证逻辑
                if (!CheckStat(sTyp, sStat, sStatSub, out sErr))
                {
                    sErr = sErr + ",序号：" + (i + 1) + "\r\n资产编码：" + sAssId;
                    bOK = false;
                    break;
                }

                //更新
                sSql = sUpd + " where ass_id = '" + sAssId + "' and ynenable = 'Y'";
                listSql.Add(sSql);
                listAssId.Add(sAssId);
                //插入
                sSql = string.Format("insert into ass_log(ass_id,opt_typ,opt_man,opt_date,cre_man,cre_tm,company,dept,reason,addr,pid) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                sAssId, sTyp, sMan, LoginForm.getDate(), LoginForm.sUserName, LoginForm.getDateTime(), SettingForm.sCompany, sDept, sReason, sAddr,sPid);
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
                sTyp, "0", sSql.Replace("'", "''"), SettingForm.sClientId, sAssId, LoginForm.getDateTime());
                listSql.Add(sSqlLog);
            }

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
                //List<string> listAssId = new List<string>();
                //for (int j = 0; j < listView1.Items.Count; j++)
                //{
                //    string sAssId = listView1.Items[j].SubItems[(int)MngIndex.assid].Text;
                //    listAssId.Add(sAssId);
                //}
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, "");
                if (bOK)
                {
                    MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    buttonClear_Click(null, null);
                    buttonSyncDiff_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
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
                //List<string> listAssId = new List<string>();
                //for (int j = 0; j < listView1.Items.Count; j++)
                //{
                //    string sAssId = listView1.Items[j].SubItems[(int)MngIndex.assid].Text;
                //    listAssId.Add(sAssId);
                //}
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, "");
                if (bOK)
                {
                    MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    buttonClear_Click(null, null);
                    buttonSyncDiff_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void buttonRepair_Click(object sender, EventArgs e)
        {
            GetReasonForm f = new GetReasonForm("维修");
            f.ShowDialog();
            if (f.ret != 0)
            {
                string sTyp = f.ret == 1 ? "送修" : "修返";
                string sDept = f.sDept;
                string sMan = f.sEmpNam;
                string sAddr = f.sAddr;
                string sReason = f.sReason;
                //List<string> listAssId = new List<string>();
                //for (int j = 0; j < listView1.Items.Count; j++)
                //{
                //    string sAssId = listView1.Items[j].SubItems[(int)MngIndex.assid].Text;
                //    listAssId.Add(sAssId);
                //}
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, "");
                if (bOK)
                {
                    MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    buttonClear_Click(null, null);
                    buttonSyncDiff_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
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
                //List<string> listAssId = new List<string>();
                //for (int j = 0; j < listView1.Items.Count; j++)
                //{
                //    string sAssId = listView1.Items[j].SubItems[(int)MngIndex.assid].Text;
                //    listAssId.Add(sAssId);
                //}
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, "");
                if (bOK)
                {
                    MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    buttonClear_Click(null, null);
                    buttonSyncDiff_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
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
                //List<string> listAssId = new List<string>();
                //for (int j = 0; j < listView1.Items.Count; j++)
                //{
                //    string sAssId = listView1.Items[j].SubItems[(int)MngIndex.assid].Text;
                //    listAssId.Add(sAssId);
                //}
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, "");
                if (bOK)
                {
                    MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    buttonClear_Click(null, null);
                    buttonSyncDiff_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
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
                //List<string> listAssId = new List<string>();
                //for (int j = 0; j < listView1.Items.Count; j++)
                //{
                //    string sAssId = listView1.Items[j].SubItems[(int)MngIndex.assid].Text;
                //    listAssId.Add(sAssId);
                //}
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, "");
                if (bOK)
                {
                    MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    buttonClear_Click(null, null);
                    buttonSyncDiff_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
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
                //List<string> listAssId = new List<string>();
                //for (int j = 0; j < listView1.Items.Count; j++)
                //{
                //    string sAssId = listView1.Items[j].SubItems[(int)MngIndex.assid].Text;
                //    listAssId.Add(sAssId);
                //}
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, "");
                if (bOK)
                {
                    MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    buttonClear_Click(null, null);
                    buttonSyncDiff_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
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
                //List<string> listAssId = new List<string>();
                //for (int j = 0; j < listView1.Items.Count; j++)
                //{
                //    string sAssId = listView1.Items[j].SubItems[(int)MngIndex.assid].Text;
                //    listAssId.Add(sAssId);
                //}
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, "");
                if (bOK)
                {
                    MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    buttonClear_Click(null, null);
                    buttonSyncDiff_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
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
                //List<string> listAssId = new List<string>();
                //for (int j = 0; j < listView1.Items.Count; j++)
                //{
                //    string sAssId = listView1.Items[j].SubItems[(int)MngIndex.assid].Text;
                //    listAssId.Add(sAssId);
                //}
                string sErr;
                bool bOK = AssChange(sTyp, sDept, sMan, sAddr, sReason, out sErr, "");
                if (bOK)
                {
                    MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    buttonClear_Click(null, null);
                    buttonSyncDiff_Click(null, null);
                }
                else
                {
                    MessageBox.Show("操作失败！\r\n" + sErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            if (buttonHide.Text == "︽")
            {
                buttonHide.Text = "︾";
                listView1.Height = 106;
                buttonHide.Top = 106;
            }
            else
            {
                buttonHide.Text = "︽";
                listView1.Height = 193;
                buttonHide.Top = 193;
            }
            listView1.Focus();
        }
        private void buttonHideCheck_Click(object sender, EventArgs e)
        {
            //if (buttonHideCheck.Text == "︽")
            if (!bIsCheckHide)
            {
                // buttonHideCheck.Text = "︾";
                // listView2.Top = 15;
                listView2.Height = 191;
                buttonHideCheck.Top = 191;
                bIsCheckHide = true;
                sumCheckText();
            }
            else
            {
                //buttonHideCheck.Text = "︽";
                // listView2.Top = 90;
                listView2.Height = 113;
                buttonHideCheck.Top = 113;
                bIsCheckHide = false;
                sumCheckText();
            }
            listView2.Focus();
        }

        private void buttonSyncDiff_Click(object sender, EventArgs e)
        {
            //获取待同步数据
            string sSql = "select count(*) cnt from sync_log where stat= '0' order by id asc";
            SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql);
            if (reader.Read())
            {
                this.Text = string.Format("Asset System(待同步：{0})", reader["cnt"].ToString());
            }
            reader.Close();

            if (buttonSyncDiff.Enabled)
            {
                listBox1.Items.Clear();
                ShowStat(listBox1, "正在同步，请勿关闭电源!");
                //启动同步线程
                buttonSyncDiff.Enabled = false;
                thrSyncDiff = new Thread(new ThreadStart(syncDiffThread));
                thrSyncDiff.Start();
            }
            else
            {
                if (!timer2.Enabled)
                {
                    timer2.Enabled = true;
                    ShowStat(listBox1, "同步中 ...");
                }
            }
        }
        ///   <summary>     
        ///   判断webservice是否可用     
        ///   </summary>     
        ///   <returns>true：可用；false：不可用</returns>     
        static public bool getWSStatus(string url, out string sErr)
        {
            sErr = "";
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                using (HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse())
                {
                    return true;
                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    sErr = string.Format("code:{0}\r\nDesc:{1}", ((HttpWebResponse)e.Response).StatusCode, ((HttpWebResponse)e.Response).StatusDescription);
                }
            }
            catch (Exception e)
            {
                sErr = e.Message;
            }
            return false;
        }
        //增量同步
        private void syncDiffThread()
        {
            string sErr;
            bHasSrv = getWSStatus(SettingForm.sWebSrvUrl, out sErr);
            if (!bHasSrv)
            {
                ShowStat(listBox1, "当前服务器不可用!");
                ShowStat(listBox1, "finish-diff");
                return;
            }
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
                string sIdSql = "";
                while (reader.Read())
                {
                    sIdSql += "'" + reader["id"].ToString() + "',";
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
                    sSql = "update sync_log set stat = '1' where stat = '0' and id in(" + sIdSql.Trim(',') + ")";
                    int nReslt = SQLiteHelper.ExecuteNonQuery(sSql, null);
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
                    //   Console.Out.WriteLine(sSqlContent);
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
                    //  Console.Out.WriteLine(SQLiteHelper.sLastErr);
                    MessageBox.Show(SQLiteHelper.sLastErr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
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
            string sErr;
            bHasSrv = getWSStatus(SettingForm.sWebSrvUrl, out sErr);
            if (!bHasSrv)
            {
                ShowStat(listBox1, "当前服务器不可用!");
                ShowStat(listBox1, "finish-all");
                return;
            }

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
                if (sSyncMax.Length == 0)
                {
                    sSyncMax = "0";
                }
            }

            ShowStat(listBox1, "同步号更新：" + sSyncMax);
            ShowStat(listBox1, "本地同步队列删除");
            ShowStat(listBox1, "客户端ID恢复：" + SettingForm.sWebSrvIp);
            ShowStat(listBox1, "Web Service恢复：" + SettingForm.sWebSrvIp);
            ShowStat(listBox1, "UDP IP恢复：" + SettingForm.sUdpIp);
            ShowStat(listBox1, "UDP 端口恢复：" + SettingForm.sUdpPort);


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
                SettingForm.LoadSetting();
            }
            ShowStat(listBox1, bOK ? "参数已恢复！" : "参数恢复失败，请重试！");
            ShowStat(listBox1, "--------------");



            if (sSyncMax.Length == 0)
            {
                ShowStat(listBox1, "获取同步号失败，请重试！");
            }
            else
            {
                ShowStat(listBox1, "全部同步完成！");
            }

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
                    // int nIdex = lstBox.Items.Add(str);
                    bSyncing = false;
                    iniData();
                    if (bIsFirstIni)
                    {
                        MessageBox.Show("初始化完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        this.Close();
                    }
                }
                else if (str.Equals("finish-diff"))
                {
                    buttonSyncDiff.Enabled = true;
                    //  int nIdex = lstBox.Items.Add(str);
                    iniData();
                    this.Text = string.Format("Asset System(待同步：{0})", 0);
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
            string sSql = "select distinct dept from inv_list where inv_no = \'" + comboBoxInvListNo.Text + "\'  order by dept";
            SQLiteDataReader reader = SQLiteHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxDept.Items.Add(reader["dept"].ToString());
            }
            reader.Close();
            comboBoxDept.Items.Add("");

            comboBoxMan.Items.Clear();
            sSql = "select distinct duty_man from inv_list where inv_no = \'" + comboBoxInvListNo.Text + "\' order by duty_man";
            reader = SQLiteHelper.ExecuteReader(sSql);
            while (reader.Read())
            {
                comboBoxMan.Items.Add(reader["duty_man"].ToString());
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
                sSql += " and dept = \'" + comboBoxDept.Text + "\'";
                blist = false;
            }
            if (comboBoxMan.Text.Length != 0)
            {
                sSql += " and duty_man = \'" + comboBoxMan.Text + "\'";
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
                    lvi.BackColor = Color.FromArgb(0x99CC66);
                    nCheck++;
                }
                else if (sResult == "手动盘点")
                {
                    lvi.BackColor = Color.FromArgb(0x0099CC);
                    nCheck++;
                }
                else if (sResult == "丢失")
                {
                    lvi.BackColor = Color.FromArgb(0xFFFF66);
                    nExp++;
                }
                else if (sResult == "报废")
                {
                    lvi.BackColor = Color.FromArgb(0xFF6666);
                    nExp++;
                }
                lvi.SubItems.Add(reader["pid"].ToString());
                lvi.SubItems.Add(reader["ass_id"].ToString());
                lvi.SubItems.Add(reader["ass_nam"].ToString());
                lvi.SubItems.Add(reader["stat"].ToString());
                lvi.SubItems.Add(reader["duty_man"].ToString());
                lvi.SubItems.Add(reader["dept"].ToString());
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
            //label4.Text = string.Format("总计 {0};已盘 {1};异常 {2};未盘点 {3}.", nAll, nCheck, nExp, nAll - nCheck);
            sumCheckText();
        }
        void sumCheckText()
        {
            string sText = string.Format("总计{0};已盘{1};异常{2};待盘{3}. --{4}--", nAll, nCheck, nExp, nAll - nCheck - nExp, bIsCheckHide ? "︽" : "︾");
            buttonHideCheck.Text = sText;
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            buttonSyncDiff_Click(null, null);
            timer2.Enabled = false;
        }

        private void buttonReadBarcode_Click(object sender, EventArgs e)
        {
            if (buttonReadBarcode.Text == "读条码")
            {
                scanCall = new HTApi.PWSCAN_CALLBACK(ScanCallBack);
                HTApi.WScanSetCallBack(scanCall);
                Scanner.open1D();
                HTApi.W1DScanStart(true);//true 单次扫描 false 多次扫描
                buttonReadBarcode.Text = "停止";
            }
            else
            {
                buttonReadBarcode.Text = "读条码";
                HTApi.W1DScanStop();
               // Scanner.close1D();
            }
        }

        private void ScanCallBack(IntPtr readBuf, int dwLen)
        {
            byte[] newm = new byte[dwLen];
            Marshal.Copy(readBuf, newm, 0, dwLen);
            sBarcode = Encoding.Default.GetString(newm, 0, dwLen);
            textBoxPid.BeginInvoke(new InvokeDelegate(ShowBarcode));
        }
        public void ShowBarcode()
        {
            textBoxPid.Text = sBarcode;
            buttonReadBarcode.Text = "读条码";
        }

        private void buttonSelectAss_Click(object sender, EventArgs e)
        {
            SelectAssForm dlg = new SelectAssForm();
            dlg.ShowDialog();
            textBoxPid.Text = dlg.sPid;
            textBoxYnWrite.Text = dlg.sYnWrite;
        }

        private void textBoxPid_TextChanged(object sender, EventArgs e)
        {
            string sPid = textBoxPid.Text;
            string sAssId, sStat, sStatSub, sYnWrite;
            if (sPid.Length == 12)
            {
                GetAssInfo(sPid, out sAssId, out sStat, out sStatSub, out sYnWrite);
                textBoxAssId.Text = sAssId;
                textBoxYnWrite.Text = sYnWrite;
                if (sAssId.Length != 0)
                {
                    label1WriteMsg.Text = "*准备OK,发卡请对准标签！";
                }
                else
                {
                    label1WriteMsg.Text = "*无该标签信息！";
                }

            }
            else if (sPid.Length != 0)
            {
                label1WriteMsg.Text = "*数据有误(应为12字节)，请重新获取！";
                textBoxAssId.Text = "";
                textBoxYnWrite.Text = "";
            }
        }

        private void buttonDetail_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == (int)Page.inv)
            {
                if (listView1.FocusedItem != null)
                {
                    int nIndex = listView1.Items.IndexOf(listView1.FocusedItem);
                    string sPid = listView1.Items[nIndex].SubItems[(int)MngIndex.pid].Text;
                    AssDetailForm fr = new AssDetailForm(sPid);
                    fr.ShowDialog();  
                }
            }
            else if (tabControl1.SelectedIndex == (int)Page.onepcs)
            {
                AssDetailForm fr = new AssDetailForm(textBoxEpc.Text);
                fr.ShowDialog();
            }
            else if (tabControl1.SelectedIndex == (int)Page.check)
            {
                if (listView2.FocusedItem != null)
                {
                    int nIndex = listView2.Items.IndexOf(listView2.FocusedItem);
                    string sPid = listView2.Items[nIndex].SubItems[(int)ChkIndex.pid].Text;
                    AssDetailForm fr = new AssDetailForm(sPid);
                    fr.ShowDialog();
                }
            }
        }
    }
}