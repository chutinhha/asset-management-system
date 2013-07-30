using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IrRfidUHFDemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //LoginForm f = new LoginForm();
            //f.ShowDialog();
            //if (f.nRet == 1)
            //{
            //    Application.Run(new MainForm(f));
            //}
            Application.Run(new LoginForm());
        }
    }
}