using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AssMngSys
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void Welcome_Resize(object sender, EventArgs e)
        {
            label1.Left = (this.Width - label1.Width) / 2;
            label2.Left = (this.Width - label2.Width) / 2;
            label1.Top = (this.Height - label1.Height - label2.Height) / 2;
            label2.Top = (this.Height + label2.Height + 20) / 2;
        }

        private void Welcome_Load(object sender, EventArgs e)
        {

        }
    }
}