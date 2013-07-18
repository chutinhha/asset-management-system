using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NameApi;

namespace IrRfidUHFDemo
{
    public partial class SetQForm : Form
    {
        public SetQForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte nCount;

            nCount = Convert.ToByte(textBox1.Text.Trim());
            if (1 == HTApi.WIrUHFSetQ(nCount))
            {
                MessageBox.Show("设置成功");
            }
            else
            {
                MessageBox.Show("设置失败");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] nCount = new byte[1];
            if (1 == HTApi.WIrUHFGetQ(ref nCount[0]))
            {
                textBox1.Text = nCount[0].ToString();
                MessageBox.Show("获取成功");
            }
            else
            {
                MessageBox.Show("获取失败");
            }
        }

        private void SetQForm_Load(object sender, EventArgs e)
        {
        }
    }
}