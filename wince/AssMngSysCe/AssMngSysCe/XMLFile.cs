using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;
using Microsoft.Win32;

namespace AssMngSysCe
{
    //public class Reg
    //{
    //    /*  Reg reg = new Reg();
    //        string mac = reg.ReadValue(Reg.HKEY.HKEY_LOCAL_MACHINE, @"Comm\DM9CE1\Parms", "NetWorkAddress").ToUpper();
    //        if (mac.Length == 12)
    //        {
    //            mac = mac.Substring(0, 2) + "-" + mac.Substring(2, 2) + "-" + mac.Substring(4, 2) + "-" +
    //                  mac.Substring(6, 2) + "-" + mac.Substring(8, 2) + "-" + mac.Substring(10, 2);
    //        }
    //        textBoxTid.Text = mac;*/
    //    public enum HKEY { HKEY_LOCAL_MACHINE = 0, HKEY_CLASSES_ROOT = 1, HKEY_CURRENT_USER = 2, HKEY_USERS = 3 };
    //    private RegistryKey[] reg = new RegistryKey[4];

    //    public Reg()
    //    {
    //        reg[(int)HKEY.HKEY_LOCAL_MACHINE] = Registry.LocalMachine;
    //        reg[(int)HKEY.HKEY_CLASSES_ROOT] = Registry.ClassesRoot;
    //        reg[(int)HKEY.HKEY_CURRENT_USER] = Registry.CurrentUser;
    //        reg[(int)HKEY.HKEY_USERS] = Registry.Users;
    //    }

    //    //��ָ������ֵ
    //    public string ReadValue(HKEY Root, string SubKey, string ValueName)
    //    {
    //        RegistryKey subKey = reg[(int)Root];
    //        if (ValueName.Length == 0) return "[ERROR]";
    //        try
    //        {
    //            if (SubKey.Length > 0)
    //            {
    //                string[] strSubKey = SubKey.Split('\\');
    //                foreach (string strKeyName in strSubKey)
    //                {
    //                    subKey = subKey.OpenSubKey(strKeyName);
    //                }
    //            }
    //            string strKey = subKey.GetValue(ValueName).ToString();
    //            subKey.Close();
    //            return strKey;
    //        }
    //        catch
    //        {
    //            return "[ERROR]";
    //        }
    //    }
    //}
    public partial class IniFile
    {
        public string sIP = "10.1.1.52";
        public string sPort = "6035";
        public string sClientId = "";
        public string sFilename = "";
        public IniFile()
        {
        }
        public void Load(string sFilePath)
        {
            sFilename = sFilePath;
            if (File.Exists(sFilename))   //�ж��ļ��Ƿ����
            {
                Read();
            }
            else
            {
                Save();
            }
        }
        public void Save()
        {
            /*
             <?xml version="1.0"?>
             <!--ϵͳ����-->
             <Setting Name="UPD">
               <ip>10.1.1.52</ip>
               <port>6035</port>
             </Setting>
            */
            if (File.Exists(sFilename))   //�ж��ļ��Ƿ����
            {
                try
                {
                    File.Delete(sFilename);
                }
                catch (IOException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            //����һ���յ�XML�ĵ�
            XmlDocument xmldoc = new XmlDocument();
            //��XML�ĵ������������������
            XmlNode xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmldoc.AppendChild(xmlnode);
            //����ע��
            XmlComment xmlcomm = xmldoc.CreateComment("ϵͳ����");
            xmldoc.AppendChild(xmlcomm);
            //ΪXML�ĵ�����Ԫ��/����һ����Ԫ��
            XmlElement xmlelem = xmldoc.CreateElement("", "setting", "");
            XmlAttribute xmlattr = xmldoc.CreateAttribute("Name");
            xmlattr.Value = "UDP";
            xmlelem.Attributes.Append(xmlattr);
            xmldoc.AppendChild(xmlelem);
            //����һ����Ԫ��
            XmlElement xmlelem2 = xmldoc.CreateElement("", "ip", "");
            XmlText xmltext = xmldoc.CreateTextNode(sIP);
            xmlelem2.AppendChild(xmltext);
            xmlelem.AppendChild(xmlelem2);
            //����һ����Ԫ��
            XmlElement xmlelem3 = xmldoc.CreateElement("", "port", "");
            xmltext = xmldoc.CreateTextNode(sPort);
            xmlelem3.AppendChild(xmltext);
            xmlelem.AppendChild(xmlelem3);
            //����һ����Ԫ��
            if (sClientId.Length == 0)
            {
                sClientId = string.Format("CE{0}{1:00}{2:00}{3:00}{4:00}{5:00}{6:000}",
                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second,DateTime.Now.Millisecond);
            }
            XmlElement xmlelem4 = xmldoc.CreateElement("", "clientid", "");
            xmltext = xmldoc.CreateTextNode(sClientId);
            xmlelem4.AppendChild(xmltext);
            xmlelem.AppendChild(xmlelem4);
            System.Diagnostics.Debug.WriteLine(sClientId);

            try
            {
                xmldoc.Save(sFilename);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            // try
            //{
            //    FileStream fs = new FileStream(sFilename, FileMode.Create, FileAccess.Write);
            //    StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));//ͨ��ָ���ַ����뷽ʽ����ʵ�ֶԺ��ֵ�֧�֣��������ü��±���
            //    sw.WriteLine(sIP);
            //    sw.WriteLine(sPort);
            //    sw.Flush();
            //    sw.Close();
            //}
            //catch (IOException ex)
            //{
            //    Debug.WriteLine("Save() Exp:");
            //    Debug.WriteLine(ex.ToString());
            //    return;
            //}
        }
        public void Read()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(sFilename);
                XmlNodeList xnl = xmlDoc.SelectSingleNode("setting").ChildNodes;
                foreach (XmlNode xnf in xnl)
                {
                    Debug.WriteLine(xnf.Name);
                    if (xnf.Name == "ip")
                    {
                        sIP = xnf.InnerText;
                        Debug.WriteLine(sIP);
                    }
                    else if (xnf.Name == "port")
                    {
                        sPort = xnf.InnerText;
                        Debug.WriteLine(sPort);
                    }
                    else if (xnf.Name == "clientid")
                    {
                        sClientId = xnf.InnerText;
                        Debug.WriteLine(sClientId);
                    }
                    //XmlElement xe = (XmlElement)xnf;
                    //XmlNodeList xnf1 = xe.ChildNodes;
                    //foreach (XmlNode xn2 in xnf1)
                    //{
                    //    Debug.WriteLine(xn2.Name);
                    //    if (xn2.Name == "ip")
                    //    {
                    //        sIP = xn2.InnerText;
                    //        Debug.WriteLine(sIP);
                    //    }
                    //    else if (xn2.Name == "port")
                    //    {
                    //        sPort = xn2.InnerText;
                    //        Debug.WriteLine(sPort);
                    //    }
                    //}//xn2
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            //try
            //{
            //    FileStream aFile = new FileStream(sFilename, FileMode.Open);
            //    StreamReader sr = new StreamReader(aFile);
            //    sIP = sr.ReadLine();
            //    sPort = sr.ReadLine();
            //    //strLine = sr.ReadLine();
            //    ////Read data in line by line
            //    //while (strLine != null)
            //    //{
            //    //    Console.WriteLine(strLine);
            //    //    Line = sr.ReadLine();
            //    //}
            //    sr.Close();
            //}
            //catch (IOException ex)
            //{
            //    Debug.WriteLine("Read() Exp:");
            //    Debug.WriteLine(ex.ToString());
            //}
            //if (sIP.Length == 0)
            //{
            //    sIP = "10.1.1.50";
            //}
            //if (sPort.Length == 0)
            //{
            //    sPort = "6035";
            //}
        }
    }
}
