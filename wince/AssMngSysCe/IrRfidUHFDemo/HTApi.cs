using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NameApi
{
    class HTApi
    {
        //扫描
        public delegate void PWSCAN_CALLBACK(IntPtr readBuf, int dwLen);

        [DllImport("HTDll.dll")]
        public static extern int WScanOpenPower();

        [DllImport("HTDll.dll")]
        public static extern int WScanClosePower();

        [DllImport("HTDll.dll")]
        public static extern int WScanSetCallBack(PWSCAN_CALLBACK scanCall);

        [DllImport("HTDll.dll")]
        public static extern int WScanSetSound(Boolean bOpen);

        //一维
        [DllImport("HTDll.dll")]
        public static extern int W1DScanConnect(byte portNo);

        [DllImport("HTDll.dll")]
        public static extern int W1DScanDisconnect();

        [DllImport("HTDll.dll")]
        public static extern int W1DScanStart(Boolean bOnce);

        [DllImport("HTDll.dll")]
        public static extern int W1DScanStop();

        //二维
        [DllImport("HTDll.dll")]
        public static extern int W2DScanConnect(byte portNo);

        [DllImport("HTDll.dll")]
        public static extern int W2DScanDisconnect();

        [DllImport("HTDll.dll")]
        public static extern int W2DScanStart();

        //超高频 

        //Ir模块
        [DllImport("HTDll.dll")]
        public static extern int WIrUHFOpenPower();

        [DllImport("HTDll.dll")]
        public static extern int WIrUHFClosePower();

        [DllImport("HTDll.dll")]
        public static extern int WIrUHFConnect(byte portNo);

        [DllImport("HTDll.dll")]
        public static extern int WIrUHFDisconnect();

        [DllImport("HTDll.dll")]
        public static extern int WIrUHFInventoryOnce(ref byte nTagCount, ref byte uReadData);

        [DllImport("HTDll.dll")]
        public static extern int WIrUHFSetInventoryMode(byte uMode);

        [DllImport("HTDll.dll")]
        public static extern int WIrUHFGetInventoryMode(ref byte uMode);

        [DllImport("HTDll.dll")]
        public static extern int WIrUHFGetPower(ref byte uPower);

        [DllImport("HTDll.dll")]
        public static extern int WIrUHFSetPower(byte uPower);

        [DllImport("HTDll.dll")]
        public static extern int WIrUHFGetQ(ref byte nQ);

        [DllImport("HTDll.dll")]
        public static extern int WIrUHFSetQ(byte nQ);

        [DllImport("HTDll.dll")]
        public static extern int WIrGetFrequency(ref byte uIndex);

        [DllImport("HTDll.dll")]
        public static extern int WIrSetFrequency(byte uIndex);

        [DllImport("HTDll.dll")]
        public static extern int WIrUHFReadData(byte uBank, byte uOff, byte uLen, ref byte nTagCount, ref byte uReadData);

        [DllImport("HTDll.dll")]
        public static extern int WIrUHFWriteData(byte uBank, byte uOff, byte uLen, ref byte uWriteData);

        [DllImport("HTDll.dll")]
        public static extern int WIrUHFReadDataByUID(byte uBank, byte uOff, byte uLen, ref byte uUii, byte uUiiLen, ref byte uReadData);

        [DllImport("HTDll.dll")]
        public static extern int WIrUHFWriteDataByUID(byte uBank, byte uOff, byte uLen, ref byte uUii, byte uUiiLen, ref byte uWriteData);
        
        [DllImport("HTDll.dll")]
        public static extern int WIrUHFLockTag(byte uSelect, byte uAction, ref byte uPassword, byte uLen, ref byte uUii);

        //RMU模块
        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFOpenPower();

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFClosePower();

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFConnect(byte portNo);

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFDisconnect();

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFGetPaStatus(ref byte uStatus);

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFGetPower(ref byte uPower);

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFSetPower(byte uPower);

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFGetFrequency(ref byte uFreMode, ref byte uFreBase, ref byte uBaseFre, ref byte uChannNum, ref byte uChannSpc, ref byte FreHop);

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFSetFrequency(byte uFreMode, byte uFreBase, ref byte uBaseFre, byte uChannNum, byte uChannSpc, byte FreHop);

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFInventory(byte flagAnti, byte initQ);

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFGetReceived(ref byte uUii, ref byte uLenUii);

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFStopGet();

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFReadDataByUID(ref byte uAccessPwd, byte uBank, ref byte uPtr, byte uCnt, ref byte uUii, ref byte uReadData, ref byte uErrorCode);

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFReadData(ref byte uAccessPwd, byte uBank, ref byte uPtr, byte uCnt, ref byte uReadData, ref byte uUii, ref byte uLenUii, ref byte uErrorCode);

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFWriteDataByUID(ref byte uAccessPwd, byte uBank, ref byte uPtr, byte uCnt, ref byte uUii, ref byte uWriteData, ref byte uErrorCode);

        [DllImport("HTDll.dll")]
        public static extern int WRmuUHFWriteData(ref byte uAccessPwd, byte uBank, ref byte uPtr, byte uCnt, ref byte uWriteData, ref byte uUii, ref byte uLenUii, ref byte uErrorCode);

        //Rw模块
        [DllImport("HTDll.dll")]
        public static extern int WRwUHFOpenPower();

        [DllImport("HTDll.dll")]
        public static extern int WRwUHFClosePower();

        [DllImport("HTDll.dll")]
        public static extern int WRwUHFConnect(byte portNo);

        [DllImport("HTDll.dll")]
        public static extern int WRwUHFDisconnect();

        [DllImport("HTDll.dll")]
        public static extern int WRwUHFInventoryOnce6B(ref byte nTagCount, ref byte uReadData);

        [DllImport("HTDll.dll")]
        public static extern int WRwUHFInventoryOnce6C(ref byte nTagCount, ref byte uReadData);

        [DllImport("HTDll.dll")]
        public static extern int WRwUHFGetPower(ref byte uPower);

        [DllImport("HTDll.dll")]
        public static extern int WRwUHFSetPower(byte uPower);

        [DllImport("HTDll.dll")]
        public static extern int WRwUHFReadData6B(ref byte uid, byte uOff, byte uLen, ref byte uReadData);

        [DllImport("HTDll.dll")]
        public static extern int WRwUHFWriteData6B(ref byte uid, byte uOff, ref byte uWriteData, byte uLen);

        [DllImport("HTDll.dll")]
        public static extern int WRwUHFSelectTag6C(ref byte uid, byte uidLen);

        [DllImport("HTDll.dll")]
        public static extern int WRwUHFReadData6C(byte uBank, byte uOff, ref byte uLen, ref byte uReadData);

        [DllImport("HTDll.dll")]
        public static extern int WRwUHFWriteData6C(ref byte acces_pw, byte uBank, byte uOff, ref byte uWriteData, byte uLen);

        //GPS
        [DllImport("HTDll.dll")]
        public static extern int WGpsOpenPower();

        [DllImport("HTDll.dll")]
        public static extern int WGpsClosePower();

        //GPRS
        [DllImport("HTDll.dll")]
        public static extern int WGsmOpenPower(int gprs, bool bCloseBlue);

        [DllImport("HTDll.dll")]
        public static extern int WGsmClosePower(int gprs, bool bOpenBlue);

        [DllImport("HTDll.dll")]
        public static extern int WGprsConnect(IntPtr hWnd);

        [DllImport("HTDll.dll")]
        public static extern int WGprsDisConnect();

        [DllImport("HTDll.dll")]
        public static extern int WGprsIsConnect();

        //摄像头
        [DllImport("HTDll.dll")]
        public static extern int WCameraOpen(IntPtr hWnd);

        [DllImport("HTDll.dll")]
        public static extern int WCameraClose();

        [DllImport("HTDll.dll")]
        public static extern int WCameraSetFocus();

        [DllImport("HTDll.dll")]
        public static extern int WCameraEndSetFocus();

        [DllImport("HTDll.dll")]
        public static extern int WCameraCancelFocus();

        [DllImport("HTDll.dll")]
        public static extern int WCameraGetJpeg(String lpOutFileName);

        [DllImport("HTDll.dll")]
        public static extern int WCameraStartRecord(String lpOutFileName);

        [DllImport("HTDll.dll")]
        public static extern int WCameraStopRecord();

        //系统
        [DllImport("HTDll.dll")]
        public static extern int WGetSystemVersion(String Ver);

        [DllImport("HTDll.dll")]
        public static extern int WGetHardwareVersion(String Ver);

        [DllImport("HTDll.dll")]
        public static extern int WVolumeKey(int dwVol);

        [DllImport("HTDll.dll")]
        public static extern int WVolumeScreen(int dwVol);
    }
}


