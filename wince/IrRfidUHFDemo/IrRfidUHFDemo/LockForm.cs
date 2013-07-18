using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NameApi;
using System.Runtime.InteropServices;

namespace IrRfidUHFDemo
{
    public partial class LockForm : Form
    {
        public LockForm()
        {
            InitializeComponent();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            byte nTagCount = 0;
            byte[] uReadData = new byte[512];

            int nResult = 1;
            int nTime = 20;
            while (nTime-- > 0)
            {
                if (1 == (nResult = HTApi.WIrUHFInventoryOnce(ref nTagCount, ref uReadData[0])))
                {
                    int uiiLen = uReadData[0];
                    string tagdata = "";
                    int i = 0;
                    for (i = 0; i < uiiLen; i++)
                    {
                        String strTmp = string.Format("{0:X2}", uReadData[i + 1]);
                        tagdata += strTmp;
                    }

                    textBox1.Text = tagdata;

                    break;
                }
            }

            if (nResult == 1)
            {
                PlaySound("\\Windows\\critical.wav", IntPtr.Zero, 0x0001);
                MessageBox.Show("获取标签号成功");
            }
            else
            {

                MessageBox.Show("获取标签号失败");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            byte uSelect = (byte)comboBox1.SelectedIndex;
            byte uAction = (byte)comboBox2.SelectedIndex;

            byte epcLen = (byte)(textBox1.Text.Length / 2);
            if (epcLen == 0)
            {
                MessageBox.Show("请先获取EPC");
                return;
            }

            byte[] epc = new byte[100];
            //转换为16进制
            StringToHexByte(textBox1.Text.Trim(), epc);

            byte passLen = (byte)(textBox2.Text.Length);
            if (passLen != 8)
            {
                MessageBox.Show("请输入4个字节密码(8个字符)");
                return;

            }

            byte[] password = new byte[100];
            //转换为16进制
            StringToHexByte(textBox2.Text.Trim(), password);

            if (1 == HTApi.WIrUHFLockTag(uSelect, uAction, ref password[0], epcLen, ref epc[0]))
            {
                MessageBox.Show("锁定成功");
            }
            else
            {
                MessageBox.Show("锁定失败");
            }
        }

        private void LockForm_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("EPC");
            comboBox1.Items.Add("USER");
            comboBox1.Items.Add("TID");
            comboBox1.Items.Add("Access Password");
            comboBox1.Items.Add("Kill Password");
            comboBox1.SelectedIndex = 1;

            comboBox2.Items.Add("非锁");
            comboBox2.Items.Add("永久非锁");
            comboBox2.Items.Add("写锁");
            comboBox2.Items.Add("永久锁");
            comboBox2.SelectedIndex = 2;


        }

    }    
}