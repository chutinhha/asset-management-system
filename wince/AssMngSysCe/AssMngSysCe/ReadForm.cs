using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NameApi;
using System.Runtime.InteropServices;

namespace AssMngSysCe
{
    public partial class ReadForm : Form
    {
        public ReadForm()
        {
            InitializeComponent();
        }

        private void ReadForm_Load(object sender, EventArgs e)
        {
            epcButton.Checked = true;
        }

        private void reservedButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            labelMsg.Text = "";

            byte _offset = Convert.ToByte(textBoxOff.Text.Trim()); 
            byte _length = Convert.ToByte(textBoxlen.Text.Trim()); 


            byte[] nTagCount = new byte[1];
            byte[] uReadData = new byte[512];

            byte uBank = 0;


            if (epcButton.Checked)
            {
                uBank = 1;
            }
            else if (userButton.Checked)
            {
                uBank = 3;
            }
            else if (tidButton.Checked)
            {
                uBank = 2;
            }

            if (1 == HTApi.WIrUHFReadData(uBank, _offset, _length, ref nTagCount[0], ref uReadData[0]))
            {
                //显示一张标签
                int _recLength = uReadData[0];
                String tagdata = "";
                int i = 0;
                
                for (i = 0; i < _recLength; i++)
                {
                    String strTmp = string.Format("{0:X2}", uReadData[i+1]);
                    tagdata += strTmp;                    
                }

                textBoxRead.Text = tagdata;
                labelMsg.Text = "读卡成功";
                PlaySound("\\Windows\\critical.wav", IntPtr.Zero, 0x0001);
            }
            else
            {
                labelMsg.Text = "读卡失败";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            labelMsg.Text = "";

            byte _offset = Convert.ToByte(textBoxOff.Text.Trim()); ;
            byte _length = Convert.ToByte(textBoxlen.Text.Trim()); ;

            byte[] writedata = new byte[100];
            //转换为16进制
            StringToHexByte(textBoxWrite.Text.Trim(), writedata);

            byte uBank = 0;

            if (reservedButton.Checked)
            {
                MessageBox.Show("Reserved区不可以写");
                return;
            }
            else if (epcButton.Checked)
            {
                if (_offset < 2)
                {
                    MessageBox.Show("EPC OFFSET 从2开始");
                    return;
                }

                uBank = 1;
            }
            else if (userButton.Checked)
            {
                uBank = 3;
            }
            else if (tidButton.Checked)
            {
                MessageBox.Show("TID 区不可以写");
                return;
            }

            //写入
            if (1 == HTApi.WIrUHFWriteData(uBank, _offset, _length, ref writedata[0]))
            {
                labelMsg.Text = "写卡成功";
                PlaySound("\\Windows\\critical.wav", IntPtr.Zero, 0x0001);
            }
            else
            {
                labelMsg.Text = "写卡失败";
            }
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

    }    
}