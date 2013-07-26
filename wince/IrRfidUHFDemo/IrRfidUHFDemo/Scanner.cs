using System;
using System.Collections.Generic;
using System.Text;
using NameApi;
namespace IrRfidUHFDemo
{
    class Scanner
    {
        static bool bUHF = false;
        static bool b1D = false;
  //    static bool b2D = false;
        public static bool openUHF()
        {
            if (!bUHF)
            {
                if (b1D)
                    close1D();

                int nRet = HTApi.WIrUHFOpenPower();
                if (nRet == 1)
                {
                    nRet = HTApi.WIrUHFConnect(2);
                    if (nRet == 1)
                    {
                        bUHF = true;
                    }
                    else
                    { 
                        System.Diagnostics.Debug.WriteLine("WIrUHFConnect:" + nRet);
                    }
                }
                else
                {
                    HTApi.WIrUHFClosePower();
                    System.Diagnostics.Debug.WriteLine("WIrUHFOpenPower:" + nRet);
                }
            }

            return bUHF;
        }

        public static bool closeUHF()
        {
            if (bUHF)
            {
                HTApi.WIrUHFDisconnect();
                HTApi.WIrUHFClosePower();
                bUHF = false;
            }
            return true;
        }

        public static bool open1D()
        {
            if (!b1D)
            {
                if (bUHF)
                    closeUHF();
                int nRet = HTApi.WScanOpenPower();
                if (nRet == 1)
                {
                    nRet = HTApi.W1DScanConnect(2);
                    if (nRet == 1)
                    {
                        HTApi.WScanSetSound(true);//…Ë÷√…˘“Ù
                        b1D = true;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("W1DScanConnect:" + nRet);
                    }
                }
                else
                {
                    HTApi.WScanClosePower();
                    System.Diagnostics.Debug.WriteLine("WScanOpenPower:" + nRet);
                }
            }
            return b1D;

        }
        public static bool close1D()
        {
            if (b1D)
            {
                HTApi.W1DScanDisconnect();
                HTApi.WScanClosePower();
                b1D = false;
            }
            return true;
        }
    }
}
