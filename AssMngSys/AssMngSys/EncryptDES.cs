using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace AssMngSys
{
    class EncryptDES
    {
        //Ĭ����Կ����
        private static byte[] m_KeysIV = { 0x00, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };//��ʼ��������IV��Initialization Vector��
        private static string m_sKeyDefualt = "88888888";
        /// <summary>
        /// DES�����ַ���
        /// </summary>
        /// <param name="encryptString">�����ܵ��ַ���</param>
        /// <param name="encryptKey">������Կ,Ҫ��Ϊ8λ</param>
        /// <returns>���ܳɹ����ؼ��ܺ���ַ�����ʧ�ܷ���Դ�� </returns>
        public static string Encrypt(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));//ת��Ϊ�ֽ�
                byte[] rgbIV = m_KeysIV;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();//ʵ�������ݼ��ܱ�׼
                DCSP.Padding = PaddingMode.Zeros;//���������粻��8�ı�������0����
                MemoryStream mStream = new MemoryStream();//ʵ�����ڴ���
                //�����������ӵ�����ת������
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
        /// DES�����ַ���
        /// </summary>
        /// <param name="decryptString">�����ܵ��ַ���</param>
        /// <param name="decryptKey">������Կ,Ҫ��Ϊ8λ,�ͼ�����Կ��ͬ</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ�����ʧ�ܷ�Դ��</returns>
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
        //string EncryptStr = EncryptDESString.Encrypt("aaaaaaaaaa", "ssssssss");  //���ؼ��ܺ���ַ���
        //string DecryptStr = EncryptDESString.Decrypt(EncryptStr, "ssssssss");//�����ַ���

        /// <summary>
        /// DES�����ַ�����16���ƣ�
        /// </summary>
        /// <param name="encryptString">�����ܵ��ַ���</param>
        /// <param name="encryptKey">������Կ,Ҫ��Ϊ8λ</param>
        /// <returns>���ܳɹ����ؼ��ܺ���ַ�����ʧ�ܷ���Դ�� </returns>
        /// 
        public static string EncryptHex(string sHex)
        {
            return EncryptHex(sHex, m_sKeyDefualt);
        }
        public static string EncryptHex(string sHex, string encryptKey)
        {
            try
            {
                //16����ת��
                int len = sHex.Length;
                byte[] inputByteArray = new byte[len / 2];
                for (int i = 0; i < len; i += 2)
                {
                    string str = sHex.Substring(i, 2);
                    inputByteArray[i / 2] = Convert.ToByte(str, 16);
                }
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));//ת��Ϊ�ֽ�
                byte[] rgbIV = m_KeysIV;
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();//ʵ�������ݼ��ܱ�׼
                DCSP.Padding = PaddingMode.None;//�������ݱ�����8�ı���������ᱣ��
                MemoryStream mStream = new MemoryStream();//ʵ�����ڴ���
                //�����������ӵ�����ת������
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                //return Convert.ToBase64String();
                //ת��Ϊ16����
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
        /// DES�����ַ�����16���ƣ�
        /// </summary>
        /// <param name="decryptString">�����ܵ��ַ���</param>
        /// <param name="decryptKey">������Կ,Ҫ��Ϊ8λ,�ͼ�����Կ��ͬ</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ�����ʧ�ܷ�Դ��</returns>
        /// 
        public static string DecryptHex(string sHex)
        {
            return DecryptHex(sHex, m_sKeyDefualt);
        }
        public static string DecryptHex(string sHex, string decryptKey)
        {
            try
            {
                //16����ת��
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
                //ת��Ϊ16����
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
