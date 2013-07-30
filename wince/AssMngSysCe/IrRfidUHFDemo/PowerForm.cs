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
    public partial class PowerForm : Form
    {
        public PowerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte uPower;

            uPower = Convert.ToByte(textBox1.Text.Trim());
            if (1 == HTApi.WIrUHFSetPower(uPower))
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
            byte[] uPower = new byte[1];
            if (1 == HTApi.WIrUHFGetPower(ref uPower[0]))
            {
                textBox1.Text = uPower[0].ToString();
                MessageBox.Show("获取成功");
            }
            else
            {
                MessageBox.Show("获取失败");
            }
        }
    }
}