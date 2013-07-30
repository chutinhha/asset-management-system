using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
namespace AssMngSys
{
    public class BarCode
    {
        private DataTable m_Code128 = new DataTable();
        private uint m_Height = 40;
        /// <summary>
        /// 高度
        /// </summary>
        public uint Height { get { return m_Height; } set { m_Height = value; } }
        private Font m_ValueFont = null;
        /// <summary>
        /// 是否显示可见号码  如果为NULL不显示号码
        /// </summary>
        public Font ValueFont { get { return m_ValueFont; } set { m_ValueFont = value; } }
        private byte m_Magnify = 0;
        /// <summary>
        /// 放大倍数
        /// </summary>
        public byte Magnify { get { return m_Magnify; } set { m_Magnify = value; } }
        /// <summary>
        /// 条码类别
        /// </summary>
        public enum Encode
        {
            Code128A,
            Code128B,
            Code128C,
            EAN128
        }
        public BarCode()
        {
            m_Code128.Columns.Add("ID");
            m_Code128.Columns.Add("Code128A");
            m_Code128.Columns.Add("Code128B");
            m_Code128.Columns.Add("Code128C");
            m_Code128.Columns.Add("BandCode");
            m_Code128.CaseSensitive = true;
            #region 数据表
            m_Code128.Rows.Add("0", " ", " ", "00", "212222");
            m_Code128.Rows.Add("1", "!", "!", "01", "222122");
            m_Code128.Rows.Add("2", "\"", "\"", "02", "222221");
            m_Code128.Rows.Add("3", "#", "#", "03", "121223");
            m_Code128.Rows.Add("4", "$", "$", "04", "121322");
            m_Code128.Rows.Add("5", "%", "%", "05", "131222");
            m_Code128.Rows.Add("6", "&", "&", "06", "122213");
            m_Code128.Rows.Add("7", "'", "'", "07", "122312");
            m_Code128.Rows.Add("8", "(", "(", "08", "132212");
            m_Code128.Rows.Add("9", ")", ")", "09", "221213");
            m_Code128.Rows.Add("10", "*", "*", "10", "221312");
            m_Code128.Rows.Add("11", "+", "+", "11", "231212");
            m_Code128.Rows.Add("12", ",", ",", "12", "112232");
            m_Code128.Rows.Add("13", "-", "-", "13", "122132");
            m_Code128.Rows.Add("14", ".", ".", "14", "122231");
            m_Code128.Rows.Add("15", "/", "/", "15", "113222");
            m_Code128.Rows.Add("16", "0", "0", "16", "123122");
            m_Code128.Rows.Add("17", "1", "1", "17", "123221");
            m_Code128.Rows.Add("18", "2", "2", "18", "223211");
            m_Code128.Rows.Add("19", "3", "3", "19", "221132");
            m_Code128.Rows.Add("20", "4", "4", "20", "221231");
            m_Code128.Rows.Add("21", "5", "5", "21", "213212");
            m_Code128.Rows.Add("22", "6", "6", "22", "223112");
            m_Code128.Rows.Add("23", "7", "7", "23", "312131");
            m_Code128.Rows.Add("24", "8", "8", "24", "311222");
            m_Code128.Rows.Add("25", "9", "9", "25", "321122");
            m_Code128.Rows.Add("26", ":", ":", "26", "321221");
            m_Code128.Rows.Add("27", ";", ";", "27", "312212");
            m_Code128.Rows.Add("28", "<", "<", "28", "322112");
            m_Code128.Rows.Add("29", "=", "=", "29", "322211");
            m_Code128.Rows.Add("30", ">", ">", "30", "212123");
            m_Code128.Rows.Add("31", "?", "?", "31", "212321");
            m_Code128.Rows.Add("32", "@", "@", "32", "232121");
            m_Code128.Rows.Add("33", "A", "A", "33", "111323");
            m_Code128.Rows.Add("34", "B", "B", "34", "131123");
            m_Code128.Rows.Add("35", "C", "C", "35", "131321");
            m_Code128.Rows.Add("36", "D", "D", "36", "112313");
            m_Code128.Rows.Add("37", "E", "E", "37", "132113");
            m_Code128.Rows.Add("38", "F", "F", "38", "132311");
            m_Code128.Rows.Add("39", "G", "G", "39", "211313");
            m_Code128.Rows.Add("40", "H", "H", "40", "231113");
            m_Code128.Rows.Add("41", "I", "I", "41", "231311");
            m_Code128.Rows.Add("42", "J", "J", "42", "112133");
            m_Code128.Rows.Add("43", "K", "K", "43", "112331");
            m_Code128.Rows.Add("44", "L", "L", "44", "132131");
            m_Code128.Rows.Add("45", "M", "M", "45", "113123");
            m_Code128.Rows.Add("46", "N", "N", "46", "113321");
            m_Code128.Rows.Add("47", "O", "O", "47", "133121");
            m_Code128.Rows.Add("48", "P", "P", "48", "313121");
            m_Code128.Rows.Add("49", "Q", "Q", "49", "211331");
            m_Code128.Rows.Add("50", "R", "R", "50", "231131");
            m_Code128.Rows.Add("51", "S", "S", "51", "213113");
            m_Code128.Rows.Add("52", "T", "T", "52", "213311");
            m_Code128.Rows.Add("53", "U", "U", "53", "213131");
            m_Code128.Rows.Add("54", "V", "V", "54", "311123");
            m_Code128.Rows.Add("55", "W", "W", "55", "311321");
            m_Code128.Rows.Add("56", "X", "X", "56", "331121");
            m_Code128.Rows.Add("57", "Y", "Y", "57", "312113");
            m_Code128.Rows.Add("58", "Z", "Z", "58", "312311");
            m_Code128.Rows.Add("59", "[", "[", "59", "332111");
            m_Code128.Rows.Add("60", "\\", "\\", "60", "314111");
            m_Code128.Rows.Add("61", "]", "]", "61", "221411");
            m_Code128.Rows.Add("62", "^", "^", "62", "431111");
            m_Code128.Rows.Add("63", "_", "_", "63", "111224");
            m_Code128.Rows.Add("64", "NUL", "`", "64", "111422");
            m_Code128.Rows.Add("65", "SOH", "a", "65", "121124");
            m_Code128.Rows.Add("66", "STX", "b", "66", "121421");
            m_Code128.Rows.Add("67", "ETX", "c", "67", "141122");
            m_Code128.Rows.Add("68", "EOT", "d", "68", "141221");
            m_Code128.Rows.Add("69", "ENQ", "e", "69", "112214");
            m_Code128.Rows.Add("70", "ACK", "f", "70", "112412");
            m_Code128.Rows.Add("71", "BEL", "g", "71", "122114");
            m_Code128.Rows.Add("72", "BS", "h", "72", "122411");
            m_Code128.Rows.Add("73", "HT", "i", "73", "142112");
            m_Code128.Rows.Add("74", "LF", "j", "74", "142211");
            m_Code128.Rows.Add("75", "VT", "k", "75", "241211");
            m_Code128.Rows.Add("76", "FF", "I", "76", "221114");
            m_Code128.Rows.Add("77", "CR", "m", "77", "413111");
            m_Code128.Rows.Add("78", "SO", "n", "78", "241112");
            m_Code128.Rows.Add("79", "SI", "o", "79", "134111");
            m_Code128.Rows.Add("80", "DLE", "p", "80", "111242");
            m_Code128.Rows.Add("81", "DC1", "q", "81", "121142");
            m_Code128.Rows.Add("82", "DC2", "r", "82", "121241");
            m_Code128.Rows.Add("83", "DC3", "s", "83", "114212");
            m_Code128.Rows.Add("84", "DC4", "t", "84", "124112");
            m_Code128.Rows.Add("85", "NAK", "u", "85", "124211");
            m_Code128.Rows.Add("86", "SYN", "v", "86", "411212");
            m_Code128.Rows.Add("87", "ETB", "w", "87", "421112");
            m_Code128.Rows.Add("88", "CAN", "x", "88", "421211");
            m_Code128.Rows.Add("89", "EM", "y", "89", "212141");
            m_Code128.Rows.Add("90", "SUB", "z", "90", "214121");
            m_Code128.Rows.Add("91", "ESC", "{", "91", "412121");
            m_Code128.Rows.Add("92", "FS", "|", "92", "111143");
            m_Code128.Rows.Add("93", "GS", "}", "93", "111341");
            m_Code128.Rows.Add("94", "RS", "~", "94", "131141");
            m_Code128.Rows.Add("95", "US", "DEL", "95", "114113");
            m_Code128.Rows.Add("96", "FNC3", "FNC3", "96", "114311");
            m_Code128.Rows.Add("97", "FNC2", "FNC2", "97", "411113");
            m_Code128.Rows.Add("98", "SHIFT", "SHIFT", "98", "411311");
            m_Code128.Rows.Add("99", "CODEC", "CODEC", "99", "113141");
            m_Code128.Rows.Add("100", "CODEB", "FNC4", "CODEB", "114131");
            m_Code128.Rows.Add("101", "FNC4", "CODEA", "CODEA", "311141");
            m_Code128.Rows.Add("102", "FNC1", "FNC1", "FNC1", "411131");
            m_Code128.Rows.Add("103", "StartA", "StartA", "StartA", "211412");
            m_Code128.Rows.Add("104", "StartB", "StartB", "StartB", "211214");
            m_Code128.Rows.Add("105", "StartC", "StartC", "StartC", "211232");
            m_Code128.Rows.Add("106", "Stop", "Stop", "Stop", "2331112");
            #endregion
        }
        /// <summary>
        /// 获取128图形
        /// </summary>
        /// <param name="p_Text">文字</param>
        /// <param name="p_Code">编码</param>
        /// <returns>图形</returns>
        public Bitmap GetCodeImage(string p_Text, Encode p_Code)
        {
            string _ViewText = p_Text;
            string _Text = "";
            IList<int> _TextNumb = new List<int>();
            int _Examine = 0;  //首位
            switch (p_Code)
            {
                case Encode.Code128C:
                    _Examine = 105;
                    if (!((p_Text.Length & 1) == 0)) throw new Exception("128C长度必须是偶数");
                    while (p_Text.Length != 0)
                    {
                        int _Temp = 0;
                        try
                        {
                            int _CodeNumb128 = Int32.Parse(p_Text.Substring(0, 2));
                        }
                        catch
                        {
                            throw new Exception("128C必须是数字！");
                        }
                        _Text += GetValue(p_Code, p_Text.Substring(0, 2), ref _Temp);
                        _TextNumb.Add(_Temp);
                        p_Text = p_Text.Remove(0, 2);
                    }
                    break;
                case Encode.EAN128:
                    _Examine = 105;
                    if (!((p_Text.Length & 1) == 0)) throw new Exception("EAN128长度必须是偶数");
                    _TextNumb.Add(102);
                    _Text += "411131";
                    while (p_Text.Length != 0)
                    {
                        int _Temp = 0;
                        try
                        {
                            int _CodeNumb128 = Int32.Parse(p_Text.Substring(0, 2));
                        }
                        catch
                        {
                            throw new Exception("128C必须是数字！");
                        }
                        _Text += GetValue(Encode.Code128C, p_Text.Substring(0, 2), ref _Temp);
                        _TextNumb.Add(_Temp);
                        p_Text = p_Text.Remove(0, 2);
                    }
                    break;
                default:
                    if (p_Code == Encode.Code128A)
                    {
                        _Examine = 103;
                    }
                    else
                    {
                        _Examine = 104;
                    }
                    while (p_Text.Length != 0)
                    {
                        int _Temp = 0;
                        string _ValueCode = GetValue(p_Code, p_Text.Substring(0, 1), ref _Temp);
                        if (_ValueCode.Length == 0) throw new Exception("无效的字符集!" + p_Text.Substring(0, 1).ToString());
                        _Text += _ValueCode;
                        _TextNumb.Add(_Temp);
                        p_Text = p_Text.Remove(0, 1);
                    }
                    break;
            }
            if (_TextNumb.Count == 0) throw new Exception("错误的编码,无数据");
            _Text = _Text.Insert(0, GetValue(_Examine)); //获取开始位
            for (int i = 0; i != _TextNumb.Count; i++)
            {
                _Examine += _TextNumb[i] * (i + 1);
            }
            _Examine = _Examine % 103;
            //获得严效位
            _Text += GetValue(_Examine);  //获取严效位
            _Text += "2331112"; //结束位
            Bitmap _CodeImage = GetImage(_Text);
            GetViewText(_CodeImage, _ViewText);
            return _CodeImage;
        }
        /// <summary>
        /// 获取目标对应的数据
        /// </summary>
        /// <param name="p_Code">编码</param>
        /// <param name="p_Value">数值 A b  30</param>
        /// <param name="p_SetID">返回编号</param>
        /// <returns>编码</returns>
        private string GetValue(Encode p_Code, string p_Value, ref int p_SetID)
        {
            if (m_Code128 == null) return "";
            DataRow[] _Row = m_Code128.Select(p_Code.ToString() + "='" + p_Value + "'");
            if (_Row.Length != 1) throw new Exception("错误的编码" + p_Value.ToString());
            p_SetID = Int32.Parse(_Row[0]["ID"].ToString());
            return _Row[0]["BandCode"].ToString();
        }
        /// <summary>
        /// 根据编号获得条纹
        /// </summary>
        /// <param name="p_CodeId"></param>
        /// <returns></returns>
        private string GetValue(int p_CodeId)
        {
            DataRow[] _Row = m_Code128.Select("ID='" + p_CodeId.ToString() + "'");
            if (_Row.Length != 1) throw new Exception("验效位的编码错误" + p_CodeId.ToString());
            return _Row[0]["BandCode"].ToString();
        }
        /// <summary>
        /// 获得条码图形
        /// </summary>
        /// <param name="p_Text">文字</param>
        /// <returns>图形</returns>
        private Bitmap GetImage(string p_Text)
        {
            char[] _Value = p_Text.ToCharArray();
            int _Width = 0;
            for (int i = 0; i != _Value.Length; i++)
            {
                _Width += Int32.Parse(_Value[i].ToString()) * (m_Magnify + 1);
            }
            Bitmap _CodeImage = new Bitmap(_Width, (int)m_Height);
            Graphics _Garphics = Graphics.FromImage(_CodeImage);
            //Pen _Pen;
            int _LenEx = 0;
            for (int i = 0; i != _Value.Length; i++)
            {
                int _ValueNumb = Int32.Parse(_Value[i].ToString()) * (m_Magnify + 1);  //获取宽和放大系数
                if (!((i & 1) == 0))
                {
                    //_Pen = new Pen(Brushes.White, _ValueNumb);
                    _Garphics.FillRectangle(Brushes.White, new Rectangle(_LenEx, 0, _ValueNumb, (int)m_Height));
                }
                else
                {
                    //_Pen = new Pen(Brushes.Black, _ValueNumb);
                    _Garphics.FillRectangle(Brushes.Black, new Rectangle(_LenEx, 0, _ValueNumb, (int)m_Height));
                }
                //_Garphics.(_Pen, new Point(_LenEx, 0), new Point(_LenEx, m_Height));
                _LenEx += _ValueNumb;
            }
            _Garphics.Dispose();
            return _CodeImage;
        }
        /// <summary>
        /// 显示可见条码文字 如果小于40 不显示文字
        /// </summary>
        /// <param name="p_Bitmap">图形</param>
        private void GetViewText(Bitmap p_Bitmap, string p_ViewText)
        {
            if (m_ValueFont == null) return;
            Graphics _Graphics = Graphics.FromImage(p_Bitmap);
            SizeF _DrawSize = _Graphics.MeasureString(p_ViewText, m_ValueFont);
            if (_DrawSize.Height > p_Bitmap.Height - 10 || _DrawSize.Width > p_Bitmap.Width)
            {
                _Graphics.Dispose();
                return;
            }
            int _StarY = p_Bitmap.Height - (int)_DrawSize.Height;
            _Graphics.FillRectangle(Brushes.White, new Rectangle(0, _StarY, p_Bitmap.Width, (int)_DrawSize.Height));

            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            RectangleF drawRect = new RectangleF(0, _StarY, p_Bitmap.Width, _DrawSize.Height);
            //_Graphics.DrawString(p_ViewText, m_ValueFont, Brushes.Black, 0, _StarY);
            _Graphics.DrawString(p_ViewText, m_ValueFont, Brushes.Black, drawRect, drawFormat);
        }
        //12345678
        //(105 + (1 * 12 + 2 * 34 + 3 * 56 + 4 *78)) % 103 = 47
        //结果为starc +12 +34 +56 +78 +47 +end
        internal Image GetCodeImage(string p)
        {
            throw new NotImplementedException();
        }
    }
}

/*using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AssMngSys
{
    /// <summary>
    /// 生成条形码( 128条码,标准参考:GB/T 18347-2001 )
    /// BY JUNSON 20090508
    /// </summary>
    public class BarCode
    {
        /// <summary>
        /// 条形码生成函数
        /// </summary>
        /// <param name="text">条型码字串</param>
        /// <returns></returns>
        public static Bitmap BuildBarCode(string text)
        {
            //查检是否合条件TEXT
            bool ck = CheckErrerCode(text);
            if (!ck)
                throw new Exception("条形码字符不合要求，不能是汉字或全角字符");
           // string barstring = BuildBarString(text);
           // return KiCode128C(barstring, 120);
            return KiCode128C(text, 30);
        }
        /// <summary>
        /// 建立条码字符串
        /// </summary>
        /// <param name="tex">条码内容</param>
        /// <returns></returns>
        private static string BuildBarString(string tex)
        {
            string barstart = "bbsbssbssss";    //码头
            string barbody = "";                //码身
            string barcheck = "";               //码检
            string barend = "bbsssbbbsbsbb";    //码尾
            int checkNum = 104;
            //循环添加码身,计算码检
            for (int i = 1; i <= tex.Length; i++)
            {
                int index = (int)tex[i - 1] - 32;
                checkNum += (index * i);
                barbody += AddSimpleTag(index);//加入字符值的条码标记
            }
            //码检值计算
            barcheck = AddSimpleTag(int.Parse(Convert.ToDouble(checkNum % 103).ToString("0")));
            string barstring = barstart + barbody + barcheck + barend;
            return barstring;
        }

        //增加一个条码标记
        private static string AddSimpleTag(int CodeIndex)
        {
            string res = "";
            /// <summary>1-4的条的字符标识 </summary>
            string[] TagB ={ "", "b", "bb", "bbb", "bbbb" };
            /// <summary>1-4的空的字符标识 </summary>
            string[] TagS ={ "", "s", "ss", "sss", "ssss" };
            string[] Code128List = new string[] {
 
                "212222","222122","222221","121223","121322","131222","122213","122312","132212","221213",
 
                "221312","231212","112232","122132","122231","113222","123122","123221","223211","221132",
 
                "221231","213212","223112","312131","311222","321122","321221","312212","322112","322211",
 
                "212123","212321","232121","111323","131123","131321","112313","132113","132311","211313",
 
                "231113","231311","112133","112331","132131","113123","113321","133121","313121","211331",
 
                "231131","213113","213311","213131","311123","311321","331121","312113","312311","332111",
 
                "314111","221411","431111","111224","111422","121124","121421","141122","141221","112214",
 
                "112412","122114","122411","142112","142211","241211","221114","413111","241112","134111",
 
                "111242","121142","121241","114212","124112","124211","411212","421112","421211","212141",
 
                "214121","412121","111143","111341","131141","114113","114311","411113","411311","113141",
 
                "114131","311141","411131","211412","211214","211232" };

            string tag = Code128List[CodeIndex];
            for (int i = 0; i < tag.Length; i++)
            {
                string temp = "";
                int num = int.Parse(tag[i].ToString());
                if (i % 2 == 0)
                {
                    temp = TagB[num];
                }
                else
                {
                    temp = TagS[num];
                }
                res += temp;
            }
            return res;
        }

        /// <summary>
        /// 检查条形码文字是否合条件(不能是汉字或全角字符)
        /// </summary>
        /// <param name="cktext"></param>
        /// <returns></returns>
        private static bool CheckErrerCode(string cktext)
        {
            foreach (char c in cktext)
            {
                byte[] tmp = System.Text.UnicodeEncoding.Default.GetBytes(c.ToString());
                if (tmp.Length > 1)

                    return false;
            }
            return true;
        }

        /// <summary>生成条码 </summary>
        /// <param name="BarString">条码模式字符串</param> //Format32bppArgb
        /// <param name="Height">生成的条码高度</param>
        /// <returns>条码图形</returns>
        private static Bitmap KiCode128C(string text, int _Height)
        {
            string BarString = BuildBarString(text);
            Bitmap b = new Bitmap(BarString.Length, _Height + 15, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //using (Graphics grp = Graphics.FromImage(b))
            //{
            try
            {
                char[] cs = BarString.ToCharArray();
                for (int i = 0; i < cs.Length; i++)
                {
                    for (int j = 0; j < _Height; j++)
                    {

                        if (cs[i] == 'b')
                        {
                            b.SetPixel(i, j, Color.Black);
                        }

                        else
                        {
                            b.SetPixel(i, j, Color.White);
                        }
                    }
                }
                Graphics grp = Graphics.FromImage(b);
                // Create rectangle for drawing.
                RectangleF drawRect = new RectangleF(0, _Height, b.Width, 15);

                //Font drawFont = new Font("Arial", 16);
                //SolidBrush drawBrush = new SolidBrush(Color.Black);
                //// Draw rectangle to screen.
                //Pen blackPen = new Pen(Color.Black);
                //e.Graphics.DrawRectangle(blackPen, x, y, width, height);

                // Set format of string.
                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Center;
                grp.DrawString(text, SystemFonts.CaptionFont, Brushes.Black, drawRect, drawFormat);
                return b;
            }
            catch
            {
                return null;
            }
            //}
        }
    }
}
*/