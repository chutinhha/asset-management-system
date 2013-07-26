using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AssMngSys
{
    public partial class PrintDlg : Form
    {
        public string sVal1 = "0";
        public string sVal2 = "0";
        public string sVal3 = "0";
        public string sVal4 = "0";
        public string sValBar = "0";
        int nCurIndex = 0;
        public DataGridView dataGridView1 = null;
        public PrintDlg(DataGridView dv)
        {
            InitializeComponent();
            dataGridView1 = dv;
        }

        private void PrintDlg_Load(object sender, EventArgs e)
        {
            //button1_Click(null,null);
            if (dataGridView1.CurrentRow != null)
            {
                nCurIndex = dataGridView1.CurrentRow.Index;
            }
            else
            {
                nCurIndex = 0;
            }
            ShowTag(nCurIndex);

            if (nCurIndex == 0)
            {
                buttonBack.Enabled = false;
            }
            if (nCurIndex == dataGridView1.RowCount - 1)
            {
                buttonForward.Enabled = false;
            }
        }
        private void ShowTag(int nIndex)
        {
            if (nIndex < 0 || nIndex > dataGridView1.RowCount - 1) return;

            sValBar = dataGridView1.Rows[nIndex].Cells["标签喷码"].Value.ToString();
            sVal1 = dataGridView1.Rows[nIndex].Cells["标签喷码"].Value.ToString();
            sVal2 = dataGridView1.Rows[nIndex].Cells["资产编号"].Value.ToString();
            sVal3 = dataGridView1.Rows[nIndex].Cells["资产名称"].Value.ToString();
            sVal4 = dataGridView1.Rows[nIndex].Cells["保管人员"].Value.ToString();

            int nW1 = 80;
            int nW2 = 120;
            int nH = 30;
            int nWith = (nW1 + nW2) * 2;
            int nHeight = nH * 4;

            Bitmap bm1 = new Bitmap((nW1 + nW2) * 2, nH / 2, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            BarCode bc = new BarCode();
            Font drawFont = new Font("Arial", 9);
            bc.ValueFont = drawFont;
            Bitmap bm2 = bc.GetCodeImage(sValBar, BarCode.Encode.Code128A);

            //创建要显示的图片对象
            Bitmap backgroudImg = new Bitmap(nWith+1, nHeight+1);
            Graphics grp = Graphics.FromImage(backgroudImg);

            //清除画布,背景设置为白色
            grp.Clear(System.Drawing.Color.White);
            grp.DrawImageUnscaled(bm1, 0, 0);
            grp.DrawImageUnscaled(bm2, nWith / 2 - bm2.Width / 2, nH * 2 + nH - bm2.Height/2 + 3);


            grp.DrawLine(Pens.Black, new Point(0, 0), new Point(nWith, 0));
            grp.DrawLine(Pens.Black, new Point(0, nH), new Point(nWith, nH));
            grp.DrawLine(Pens.Black, new Point(0, 2 * nH), new Point(nWith, 2 * nH));
            grp.DrawLine(Pens.Black, new Point(0, 4 * nH), new Point(nWith, 4 * nH));

            grp.DrawLine(Pens.Black, new Point(0, 0), new Point(0, nHeight));
            grp.DrawLine(Pens.Black, new Point(nW1, 0), new Point(nW1, 2 * nH));
            grp.DrawLine(Pens.Black, new Point(nW1 + nW2, 0), new Point(nW1 + nW2, 2 * nH));
            grp.DrawLine(Pens.Black, new Point(nW1 + nW2 + nW1, 0), new Point(nW1 + nW2 + nW1, 2 * nH));
            grp.DrawLine(Pens.Black, new Point(nW1 + nW2 + nW1 + nW2, 0), new Point(nW1 + nW2 + nW1 + nW2, nHeight));


            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            //RFID标签
            RectangleF drawRect = new RectangleF(0, 0, nW1, nH);
            grp.DrawString("RFID标签", SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);
            drawRect = new RectangleF(nW1, 0, nW2, nH);
            grp.DrawString(sVal1, SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);

            //资产编码
            drawRect = new RectangleF(nW1 + nW2, 0, nW1, nH);
            grp.DrawString("资产编码", SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);
            drawRect = new RectangleF(nW1 + nW2 + nW1, 0, nW2, nH);
            grp.DrawString(sVal2, SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);

            //资产名称
            drawRect = new RectangleF(0, nH, nW1, nH);
            grp.DrawString("资产名称", SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);
            drawRect = new RectangleF(nW1, nH, nW2, nH);
            grp.DrawString(sVal3, SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);

            //领用人员
            drawRect = new RectangleF(nW1 + nW2, nH, nW1, nH);
            grp.DrawString("保管人员", SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);
            drawRect = new RectangleF(nW1 + nW2 + nW1, nH, nW2, nH);
            grp.DrawString(sVal4, SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);

            grp.Dispose();

            pictureBox1.Image = backgroudImg;
            pictureBox1.Invalidate();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (nCurIndex == dataGridView1.RowCount - 1)
            {
                buttonForward.Enabled = true;
            }
            ShowTag(--nCurIndex);  
            if (nCurIndex == 0)
            {
                buttonBack.Enabled = false;
            }
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            if (nCurIndex == 0)
            {
                buttonBack.Enabled = true;
            }
            ShowTag(++nCurIndex);     
            if (nCurIndex == dataGridView1.RowCount - 1)
            {
                buttonForward.Enabled = false;
            }
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            buttonBack.Visible = checkBoxAll.Checked;
            buttonForward.Visible = checkBoxAll.Checked;
        }
    }
}