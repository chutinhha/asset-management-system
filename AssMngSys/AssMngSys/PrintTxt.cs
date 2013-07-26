using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace AssMngSys
{
    public partial class PrintTxt
    {
        private PrintPreviewDialog PrintPreview = new PrintPreviewDialog();
        private string StreamType;
        private Image image = null;
        private Stream StreamToPrint = null;
        Font mainFont = new Font("����", 12);//��ӡ������
        public string Filename = null;

        //1��ʵ������ӡ�ĵ�
        PrintDocument pdDocument = new PrintDocument();
        private string[] lines;
        private int linesPrinted;

        public PrintTxt(string filepath, string filetype)
        {
            Filename = Path.GetFileNameWithoutExtension(filepath);
            //����BeginPrint�¼�
            pdDocument.BeginPrint += new PrintEventHandler(pdDocument_BeginPrint);
            //ӆ�EndPrint�¼����ͷ���Դ
            pdDocument.PrintPage += new PrintPageEventHandler(OnPrintPage);
            //����Print��ӡ�¼�,�÷���������ڶ��Ĵ�ӡ�¼������
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            StartPrint(fs, filetype);

            //��ӡ����
            pdDocument.EndPrint += new PrintEventHandler(pdDocument_EndPrint);

        }
        //2������Print��ӡ����
        public void StartPrint(Stream streamToPrint, string streamType)
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
            StreamToPrint = streamToPrint;//��ӡ���ֽ���
            StreamType = streamType; //��ӡ������
            pdDocument.DocumentName = Filename; //��ӡ���ļ���
            Psdl.Document = pdDocument;
            PrintPreview.Document = pdDocument;
            pt.Document = pdDocument;
            Psdl.PageSettings = pdDocument.DefaultPageSettings;
            try
            {
                //ҳ�����öԻ���
                if (Psdl.ShowDialog() == DialogResult.OK)
                {
                    ps = Psdl.PageSettings;
                    pdDocument.DefaultPageSettings = Psdl.PageSettings;
                }
                //ѡ���ӡ���Ի���
                if (pt.ShowDialog() == DialogResult.OK)
                {
                    pdDocument.PrinterSettings.Copies = pt.PrinterSettings.Copies;
                    pdDocument.Print();
                }
                //��ӡԤ���Ի���
                if (PrintPreview.ShowDialog() == DialogResult.OK)
                {
                    //���ô�ӡ
                    pdDocument.Print();
                }

                //PrintDocument�����Print()������PrintController����ִ��PrintPage�¼���
            }
            catch (InvalidPrinterException ex)
            {
                MessageBox.Show(ex.Message, "Simple Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        /// <summary>
        /// 3���õ���ӡ����
        /// ÿ����ӡ����ֻ����OnBeginPrint()һ�Ρ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pdDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            char[] param = { '\n' };
            char[] trimParam = { '\r' };//�س�
            switch (StreamType)
            {
                case "txt":
                    StringBuilder text = new StringBuilder();
                    System.IO.StreamReader streamReader = new StreamReader(StreamToPrint, Encoding.Default);
                    while (streamReader.Peek() >= 0)
                    {
                        lines = streamReader.ReadToEnd().Split(param);
                        for (int i = 0; i < lines.Length; i++)
                        {
                            lines[i] = lines[i].TrimEnd(trimParam);
                        }
                    }
                    break;
                case "image":
                    image = System.Drawing.Image.FromStream(StreamToPrint);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 4�����ƶ����ӡ����
        /// printDocument��PrintPage�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            int leftMargin = Convert.ToInt32((e.MarginBounds.Left) * 3 / 4);�� //��߾�
            int topMargin = Convert.ToInt32(e.MarginBounds.Top * 2 / 3);������ //���߾�
            switch (StreamType)
            {
                case "txt":
                    while (linesPrinted < lines.Length)
                    {
                        //�򻭲�����д����
                        e.Graphics.DrawString(lines[linesPrinted++], new Font("Arial", 10), Brushes.Black, leftMargin, topMargin, new StringFormat());
                        topMargin += 55;//�и�Ϊ55���ɵ���
                        //��ֽ��ҳ
                        if (topMargin >= e.PageBounds.Height - 60)//ҳ���ۼӵĸ߶ȴ���ҳ��߶ȡ������Լ���Ҫ�������ʵ�����
                        {
                            //��������趨�ĸ�
                            e.HasMorePages = true;
                            /*
                            * PrintPageEventArgs���HaeMorePages����ΪTrueʱ��֪ͨ�ؼ����������ٴ��{��OnPrintPage()��������ӡһ��ҳ�档
                            * PrintLoopI()��һ�����ÿ��Ҫ��ӡ��ҳ������������HasMorePages��False��PrintLoop()�ͻ�ֹͣ��
                            */
                            return;
                        }
                    }
                    break;
                case "image"://һ���漰����ͼƬ,
                    int width = image.Width;
                    int height = image.Height;
                    if ((width / e.MarginBounds.Width) > (height / e.MarginBounds.Height))
                    {
                        width = e.MarginBounds.Width;
                        height = image.Height * e.MarginBounds.Width / image.Width;
                    }
                    else
                    {
                        height = e.MarginBounds.Height;
                        width = image.Width * e.MarginBounds.Height / image.Height;
                    }
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(topMargin, leftMargin, width, height);
                    //�򻭲�д��ͼƬ
                    for (int i = 0; i < Convert.ToInt32(Math.Floor((double)image.Height / 820)) + 1; i++)
                    {
                        e.Graphics.DrawImage(image, destRect, i * 820, i * 1170, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
                        //��ֽ��ҳ
                        if (i * 1170 >= e.PageBounds.Height - 60)//ҳ���ۼӵĸ߶ȴ���ҳ��߶ȡ������Լ���Ҫ�������ʵ�����
                        {
                            //��������趨�ĸ�
                            e.HasMorePages = true;
                            /*
                            * PrintPageEventArgs���HaeMorePages����ΪTrueʱ��֪ͨ�ؼ����������ٴ��{��OnPrintPage()��������ӡһ��ҳ�档
                            * PrintLoopI()��һ�����ÿ��Ҫ��ӡ��ҳ������������HasMorePages��False��PrintLoop()�ͻ�ֹͣ��
                            */
                            return;
                        }
                    }
                    break;
            }
            //��ӡ��Ϻ󣬻���������ע����ӡ����
            e.Graphics.DrawLine(new Pen(Color.Black), leftMargin, topMargin, e.MarginBounds.Right, topMargin);
            string strdatetime = DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString();
            e.Graphics.DrawString(string.Format("��ӡʱ�䣺{0}", strdatetime), mainFont, Brushes.Black, e.MarginBounds.Right - 240, topMargin + 40, new StringFormat());
            linesPrinted = 0;
            //������ɺ󣬹رն�ҳ��ӡ����
            e.HasMorePages = false;
        }

        /// <summary>? 
        ///5��EndPrint�¼�,�ͷ���Դ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pdDocument_EndPrint(object sender, PrintEventArgs e)
        {
            //����Linesռ�ú����õ��ַ������飬�����ͷ�
            lines = null;
        }
    }
    //PrintTxt simple = new PrintTxt("D:\\Mainsoft\\12.txt", "txt"); 
}
