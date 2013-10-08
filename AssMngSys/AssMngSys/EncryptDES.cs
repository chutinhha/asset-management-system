using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace AssMngSys
{
    class EncryptDES
    {
        //默认密钥向量
        private static byte[] m_KeysIV = { 0x00, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };//初始化向量（IV，Initialization Vector）
        private static string m_sKeyDefualt = "88888888";
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串 </returns>
        public static string Encrypt(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));//转换为字节
                byte[] rgbIV = m_KeysIV;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();//实例化数据加密标准
                DCSP.Padding = PaddingMode.Zeros;//加密数据如不是8的倍数，用0补充
                MemoryStream mStream = new MemoryStream();//实例化内存流
                //将数据流链接到加密转换的流
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
               //return Convert.ToBase64String(mStream.ToArray());
                return Encoding.UTF8.GetString(mStream.ToArray(), 0, mStream.ToArray().Length);
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string Decrypt(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = m_KeysIV;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                DCSP.Padding = PaddingMode.Zeros;
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray(), 0, mStream.ToArray().Length);
            }
            catch
            {
                return decryptString;
            }
        }
        //string EncryptStr = EncryptDESString.Encrypt("aaaaaaaaaa", "ssssssss");  //返回加密后的字符串
        //string DecryptStr = EncryptDESString.Decrypt(EncryptStr, "ssssssss");//解密字符串

        /// <summary>
        /// DES加密字符串（16进制）
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串 </returns>
        /// 
        public static string EncryptHex(string sHex)
        {
            return EncryptHex(sHex, m_sKeyDefualt);
        }
        public static string EncryptHex(string sHex, string encryptKey)
        {
            try
            {
                //16进制转换
                int len = sHex.Length;
                byte[] inputByteArray = new byte[len / 2];
                for (int i = 0; i < len; i += 2)
                {
                    string str = sHex.Substring(i, 2);
                    inputByteArray[i / 2] = Convert.ToByte(str, 16);
                }
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));//转换为字节
                byte[] rgbIV = m_KeysIV;
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();//实例化数据加密标准
                DCSP.Padding = PaddingMode.None;//加密数据必须是8的倍数，否则会保存
                MemoryStream mStream = new MemoryStream();//实例化内存流
                //将数据流链接到加密转换的流
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                //return Convert.ToBase64String();
                //转换为16进制
                string sOutHex = "";
                byte[] outputByteArray = mStream.ToArray();
                foreach (byte bt in outputByteArray)
                {
                    sOutHex += string.Format("{0:X2}", bt);
                }
                return sOutHex;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message.ToString());
                return sHex;
            }
        }

        /// <summary>
        /// DES解密字符串（16进制）
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        /// 
        public static string DecryptHex(string sHex)
        {
            return DecryptHex(sHex, m_sKeyDefualt);
        }
        public static string DecryptHex(string sHex, string decryptKey)
        {
            try
            {
                //16进制转换
                int len = sHex.Length;
                byte[] inputByteArray = new byte[len / 2];
                for (int i = 0; i < len; i += 2)
                {
                    string str = sHex.Substring(i, 2);
                    inputByteArray[i / 2] = Convert.ToByte(str, 16);
                }
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = m_KeysIV;

                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                DCSP.Padding = PaddingMode.None;
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                // return Encoding.UTF8.GetString(mStream.ToArray());
                //转换为16进制
                string sOutHex = "";
                byte[] outputByteArray = mStream.ToArray();
                foreach (byte bt in outputByteArray)
                {
                    sOutHex += string.Format("{0:X2}", bt);
                }
                return sOutHex;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message.ToString());
                return sHex;
            }
        }
    }
}
