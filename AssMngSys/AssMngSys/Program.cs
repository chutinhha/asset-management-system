using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AssMngSys
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login f = new Login();
            f.ShowDialog();
            if (f.nRet == 1)
            {
                Application.Run(new MainForm());
            }
        }
    }
}