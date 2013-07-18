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
    public partial class ModeForm : Form
    {
        public ModeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte uMode = 0;
            if (radioButton2.Checked)
            {
                uMode = 1;
            }

            if (1 == HTApi.WIrUHFSetInventoryMode(uMode))
            {
                MessageBox.Show("���óɹ�");
            }
            else
            {
                MessageBox.Show("����ʧ��");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            byte[] uMode = new byte[1];
            if (1 == HTApi.WIrUHFGetInventoryMode(ref uMode[0]))
            {
                radioButton1.Checked = (uMode[0] == 0);
                radioButton2.Checked = (uMode[0] == 1);

                MessageBox.Show("��ȡ�ɹ�");

            }
            else
            {
                MessageBox.Show("��ȡʧ��");

            }
        }

        private void ModeForm_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
        }
    }
}