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
        public string sOptTyp = "";
        public CheckOptForm()
        {
            InitializeComponent();
        }

        private void SelectInvList_Load(object sender, EventArgs e)
        {
            comboBoxOptTyp.Items.Add("手动盘点");
            comboBoxOptTyp.Items.Add("丢失");
            comboBoxOptTyp.Items.Add("报废");
            comboBoxOptTyp.SelectedIndex = 0;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            sOptTyp = comboBoxOptTyp.Text;
        }

        private void CheckOptForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                buttonCancel_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(null, null);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}