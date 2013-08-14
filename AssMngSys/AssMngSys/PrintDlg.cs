using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
namespace AssMngSys
{
    public partial class PrintDlg : Form
    {
        public string sVal1 = "0";
        public string sVal2 = "0";
        public string sVal3 = "0";
        public string sVal4 = "0";
        public string sVal5 = "0";
        public string sVal6 = "0";
        public string sVal7 = "0";
        public string sValBar = "0";
        int nCurIndex = 0;
        public DataGridView dataGridView1 = null;

        private Image image = null;

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

            sValBar = dataGridView1.Rows[nIndex].Cells["��ǩ����"].Value.ToString();
            sVal1 = dataGridView1.Rows[nIndex].Cells["�ʲ�����"].Value.ToString();
            sVal2 = dataGridView1.Rows[nIndex].Cells["��������"].Value.ToString();
            sVal3 = dataGridView1.Rows[nIndex].Cells["�ʲ�����"].Value.ToString();
            sVal4 = dataGridView1.Rows[nIndex].Cells["�豸�ͺ�"].Value.ToString();
            sVal5 = dataGridView1.Rows[nIndex].Cells["������Ա"].Value.ToString();
            sVal6 = dataGridView1.Rows[nIndex].Cells["�ʲ�����"].Value.ToString();
            sVal7 = dataGridView1.Rows[nIndex].Cells["��������"].Value.ToString();

            int nW1 = 72;
            int nW2 = 115;
            int nH = 28;
            int nWith = (nW1 + nW2) * 2;
            int nHeight = nH * 6;

            //     Bitmap bm1 = new Bitmap((nW1 + nW2) * 2, nH * 5,System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            BarCode bc = new BarCode();
            Font drawFont = new Font("Arial", 9);
            bc.ValueFont = drawFont;
            Bitmap bm2 = bc.GetCodeImage(sValBar, BarCode.Encode.Code128A);

            //����Ҫ��ʾ��ͼƬ����
            Bitmap backgroudImg = new Bitmap(nWith + 1, nHeight + 1);
            Graphics grp = Graphics.FromImage(backgroudImg);

            //�������,��������Ϊ��ɫ
            grp.Clear(System.Drawing.Color.White);
            //     grp.DrawImageUnscaled(bm1, 0, 0);
            grp.DrawImageUnscaled(bm2, nW1 + (nW2 + nW1 + nW2) / 2 - bm2.Width / 2, nH * 5 - bm2.Height / 2 + 6);


            grp.DrawLine(Pens.Black, new Point(0, 0), new Point(nWith, 0));
            grp.DrawLine(Pens.Black, new Point(0, nH), new Point(nWith, nH));
            grp.DrawLine(Pens.Black, new Point(0, 2 * nH), new Point(nWith, 2 * nH));
            grp.DrawLine(Pens.Black, new Point(0, 3 * nH), new Point(nWith, 3 * nH)); 
            int diff = 8;
            grp.DrawLine(Pens.Black, new Point(0, 4 * nH + diff), new Point(nWith, 4 * nH + diff));
            grp.DrawLine(Pens.Black, new Point(0, 6 * nH), new Point(nWith, 6 * nH));
           // grp.DrawLine(Pens.Black, new Point(0, 7 * nH), new Point(nWith, 7 * nH));

            grp.DrawLine(Pens.Black, new Point(0, 0), new Point(0, nHeight));
            grp.DrawLine(Pens.Black, new Point(nW1, 0), new Point(nW1, nHeight));
            grp.DrawLine(Pens.Black, new Point(nW1 + nW2,0), new Point(nW1 + nW2, 3 * nH));
            grp.DrawLine(Pens.Black, new Point(nW1 + nW2 + nW1,0), new Point(nW1 + nW2 + nW1, 3 * nH));
            grp.DrawLine(Pens.Black, new Point(nWith, 0), new Point(nWith, nHeight));


            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            //����̶��ʲ���ǩ
            //RectangleF drawRect = new RectangleF(0, 0, nWith, nH);
            //grp.DrawString("����̶��ʲ���ǩ", SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);

            //�ʲ�����
            RectangleF drawRect = new RectangleF(0, 0, nW1, nH);
            grp.DrawString("�ʲ�����", SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);
            drawRect = new RectangleF(nW1, 0, nW2, nH);
            grp.DrawString(sVal1, SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);

            //��������
            drawRect = new RectangleF(nW1 + nW2, 0, nW1, nH);
            grp.DrawString("��������", SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);
            drawRect = new RectangleF(nW1 + nW2 + nW1, 0, nW2, nH);
            grp.DrawString(sVal2, SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);

            //�ʲ�����
            drawRect = new RectangleF(0, 1 * nH, nW1, nH);
            grp.DrawString("�ʲ�����", SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);
            drawRect = new RectangleF(nW1, 1 * nH, nW2, nH);
            grp.DrawString(sVal3, SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);

            //�豸�ͺ�
            drawRect = new RectangleF(nW1 + nW2, 1 * nH, nW1, nH);
            grp.DrawString("�豸�ͺ�", SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);
            drawRect = new RectangleF(nW1 + nW2 + nW1, 1 * nH, nW2, nH);
            grp.DrawString(sVal4, SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);

            //������Ա
            drawRect = new RectangleF(0, 2 * nH, nW1, nH);
            grp.DrawString("������Ա", SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);
            drawRect = new RectangleF(nW1, 2 * nH, nW2, nH);
            grp.DrawString(sVal5, SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);

            //��ע
            drawRect = new RectangleF(nW1 + nW2, 2 * nH, nW1, nH);
            grp.DrawString("�ʲ�����", SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);
            drawRect = new RectangleF(nW1 + nW2 + nW1, 2 * nH, nW2, nH);
            grp.DrawString(sVal6, SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);

            //��������
            drawRect = new RectangleF(0, 3 * nH, nW1, nH + diff);
            grp.DrawString("��������", SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);
            drawRect = new RectangleF(nW1, 3 * nH, nW2 + nW1 + nW2, nH + diff);
            grp.DrawString(sVal7, SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);

            //RFID����
            drawRect = new RectangleF(0, 4 * nH + diff, nW1, 2 * nH - diff);
            grp.DrawString("RFID����", SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);

            grp.Dispose();

            pictureBox1.Image = backgroudImg;
            pictureBox1.Invalidate();
        }

        //��ӡ��ǩ
        private void buttonSave_Click(object sender, EventArgs e)
        {
            StartPrint();
        }
        //2������Print��ӡ����
        public void StartPrint()//Stream streamToPrint, string streamType
        {
            //����ֵ��PageSettings A4\A5
            PageSettings ps = new PageSettings();
            //��ʾ���ô�ӡҳ�Ի���
            PageSetupDialog Psdl = new PageSetupDialog();
            //��ӡ������ã�ע�⣬�÷��������printpage�������档
            PrintDialog pt = new PrintDialog();
            pt.AllowCurrentPage = true;
            pt.AllowSomePages = true;
            pt.AllowPrintToFile = true;
            //       StreamToPrint = streamToPrint;//��ӡ���ֽ���
            //       StreamType = streamType; //��ӡ������
            //       printDocument1.DocumentName = Filename; //��ӡ���ļ���
            Psdl.Document = printDocument1;
            //       PrintPreview.Document = printDocument1;
            pt.Document = printDocument1;
            Psdl.PageSettings = printDocument1.DefaultPageSettings;
            try
            {
                //ҳ�����öԻ���
                if (Psdl.ShowDialog() == DialogResult.OK)
                {
                    ps = Psdl.PageSettings;
                    printDocument1.DefaultPageSettings = Psdl.PageSettings;
                }
                //ѡ���ӡ���Ի���
                if (pt.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings.Copies = pt.PrinterSettings.Copies;
                    //printDocument1.Print();
                    if (!checkBoxAll.Checked)
                    {
                        printDocument1.Print();
                    }
                    else
                    {
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            ShowTag(i);
                            printDocument1.Print();
                        }
                    }

                }
                ////��ӡԤ���Ի���
                //if (PrintPreview.ShowDialog() == DialogResult.OK)
                //{
                //    //���ô�ӡ
                //    printDocument1.Print();
                //}

                //PrintDocument�����Print()������PrintController����ִ��PrintPage�¼���
            }
            catch (InvalidPrinterException ex)
            {
                MessageBox.Show(ex.Message, "Simple Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            image = pictureBox1.Image;
        }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int index = 1;
            switch (index)//�ؼ���ӡ��ҳ��     
            {
                case 1:
                    index++;//һҳ��ӡ��֮��              
                    e.HasMorePages = true;
                    break;
                case 2:
                    index++; //һҳ��ӡ��֮��     
                    e.HasMorePages = true;
                    break;
                case 3:
                    index++; //һҳ��ӡ��֮��                 
                    e.HasMorePages = true;
                    break;
                case 4:
                    index++;
                    e.HasMorePages = false;
                    break;

            }
            e.Graphics.DrawImage(pictureBox1.Image, e.MarginBounds.Left, e.MarginBounds.Top);
            //if (!checkBoxAll.Checked)
            //{
            //    e.Graphics.DrawImage(pictureBox1.Image, e.MarginBounds.Left, e.MarginBounds.Top);
            //}
            //else
            //{
            //    for (int i = 0; i < dataGridView1.RowCount; i++)
            //    {
            //        ShowTag(i);
            //        e.Graphics.DrawImage(pictureBox1.Image, e.MarginBounds.Left, e.MarginBounds.Top);
            //        if (i != dataGridView1.RowCount - 1)
            //        {
            //            e.HasMorePages = true;
            //        }
            //        else
            //        {
            //            e.HasMorePages = false;
            //        }
            //    }
            //}

            //������ɺ󣬹رն�ҳ��ӡ����
            e.HasMorePages = false;
        }
        void printDocument1_EndPrint(object sender, PrintEventArgs e)
        {
            // lines = null;
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