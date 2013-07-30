using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using NameApi;
using System.Runtime.InteropServices;
//using System.Net;
//using System.Net.Sockets;


namespace AssMngSysCe
{
    public partial class FindForm : Form
    {
        Thread findThread;
        public delegate void InvokeDelegate();
        String onetagInfo;
        public int m_btnStop;

     //   private System.Net.Sockets.UdpClient sendUdpClient;

        public FindForm()
        {
            InitializeComponent();
        }

        private void FindForm_Load(object sender, EventArgs e)
        {
            listViewControl.FullRowSelect = true;
            listViewControl.Columns.Add("Tag ID", 190, HorizontalAlignment.Left);
            listViewControl.Columns.Add("Count", 45, HorizontalAlignment.Left);

            timer1.Interval = 1000;
            timer1.Enabled = true;

            m_btnStop = 0;

        }
        
        private void myThread()
        {
            // 匿名模式(套接字绑定的端口由系统随机分配)
            //sendUdpClient = new System.Net.Sockets.UdpClient(0);

            byte nTagCount = 0;
            byte[] uReadData = new byte[512];
            String[] tagInfo = new String[50];
            //string strLastDat = "NULL";
            while (m_btnStop == 1)
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
                                tagInfo[i] += "-" + strTmp;
                            }
                        }
                    }

                    //把数据放到列表
                    for (i = 0; i < nRealTagCount; i++)
                    {
                        onetagInfo = tagInfo[i];
                        listViewControl.BeginInvoke(new InvokeDelegate(Display));

                        //if (!strLastDat.Equals(onetagInfo))
                        //{
                        //    strLastDat = onetagInfo;
                        //    string message = onetagInfo;
                        //    byte[] sendbytes = Encoding.Unicode.GetBytes(message);
                        //    IPAddress remoteIp = IPAddress.Parse("10.1.1.50");
                        //    IPEndPoint remoteIpEndPoint = new IPEndPoint(remoteIp, int.Parse("21883"));
                        //    sendUdpClient.Send(sendbytes, sendbytes.Length, remoteIpEndPoint);
                        //}
                    }
                }
            } 
            //sendUdpClient.Close();
        }

        private void Display()
        {
            //listViewControl.Items.Add(DateTime.Now.ToString());
            int nfind = -1;
            int count = listViewControl.Items.Count;
            for (int j = 0; j < count; j++)
            {
                String findItem = listViewControl.Items[j].SubItems[0].Text;
                if (onetagInfo == findItem)
                {
                    nfind = j;
                    break;
                }
            }

            if (nfind >= 0)
            {
                String findItem = listViewControl.Items[nfind].SubItems[1].Text;
                int intFind = Convert.ToInt32(findItem);
                intFind = intFind + 1;
                String sumFind = "" + intFind;
                listViewControl.Items[nfind].SubItems[1].Text = sumFind;
            }
            else
            {
                ListViewItem Item = new ListViewItem();
                Item.SubItems[0].Text = onetagInfo;
                Item.SubItems.Add("1");
                listViewControl.Items.Add(Item);
            }
        }       

        private void timer1_Tick(object sender, EventArgs e)
        {
            int tagCount = listViewControl.Items.Count;
            String strTagCount;
            strTagCount = "数目:" + tagCount;
            label1.Text = strTagCount;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startThread();
        }

        public void startThread()
        {
            if (m_btnStop == 0)
            {
                m_btnStop = 1;
                findThread = new Thread(new ThreadStart(myThread));
                findThread.Start();

                button1.Text = "停止";
            }
            else
            {
                button1.Text = "识别";
                m_btnStop = 0;

                findThread.Abort();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listViewControl.Items.Clear();
        }

        private void FindForm_Closing(object sender, CancelEventArgs e)
        {
            if (m_btnStop == 1)
            {
                button1.Text = "识别";
                m_btnStop = 0;

                findThread.Abort();
                findThread.Join(100);
            }
        }

        [DllImport("CoreDll.DLL")]
        private extern static int PlaySound(string szSound, IntPtr hMod, int flags);

        private void FindForm_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}