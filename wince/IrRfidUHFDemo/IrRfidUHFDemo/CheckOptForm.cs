using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace IrRfidUHFDemo
{
    public partial class CheckOptForm : Form
    {
        public string sInvListNo = "";
        public CheckOptForm()
        {
            InitializeComponent();
        }

        private void SelectInvList_Load(object sender, EventArgs e)
        {
            comboBoxOptTyp.Items.Add("ÒÑÅÌµã");
            comboBoxOptTyp.Items.Add("¶ªÊ§");
            comboBoxOptTyp.Items.Add("±¨·Ï");
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            
        }

    }
}