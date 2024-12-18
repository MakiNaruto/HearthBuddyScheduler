using System;
using System.IO;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace HearthHelper;

public static class Security
{
    public static int bAuthOK()
    {
        try
        {
            string string_ = "";
            string text = "";
            WebClient webClient = new WebClient();
            using (StreamReader streamReader = new StreamReader(webClient.OpenRead("https://gitee.com/UniverseString/Hearthstone-myAccount/raw/master/myAccount/Users.dat")))
            {
                string_ = streamReader.ReadToEnd();
            }
            text = Rsa.AesDecrypt(string_);
            Stream inStream = new MemoryStream(Encoding.UTF8.GetBytes(text));
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(inStream);
            XmlNode xmlNode = xmlDocument.SelectSingleNode("Users");
            webClient.Dispose();
            bool flag = false;
            string text2 = Token();
            if (text2 == "c82ad3d391d1b75cf2b85aef8a1a5ce0")
            {
                text2 = Token1();
            }
            if (text2 != null)
            {
                foreach (XmlNode item in xmlNode)
                {
                    if (item.Name == "Key" && Rsa.RsaSignCheck(text2, item.InnerText))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    UtilsCom.Log("当前用户认证成功，正常使用！");
                    return 0;
                }
                if (!flag)
                {
                    UtilsCom.Log("当前用户认证失败，无法使用！");
                    UtilsCom.Log("请将下面Key复制后发送给管理员进行注册！");
                    UtilsCom.Log("Key=" + text2);
                }
            }
            return 1;
        }
        catch (Exception)
        {
            string text3 = Token();
            if (text3 == "c82ad3d391d1b75cf2b85aef8a1a5ce0")
            {
                text3 = Token1();
            }
            if (text3 != null)
            {
                UtilsCom.Log("当前用户认证失败，无法使用！");
                UtilsCom.Log("请将下面Key复制后发送给管理员进行注册！");
                UtilsCom.Log("Key=" + text3);
            }
            return -1;
        }
    }

    public static string Token()
    {
        string hD = GetHD();
        string moAddress = GetMoAddress();
        string cpuID = GetCpuID();
        if (hD == null && moAddress == null && cpuID == null)
        {
            return "";
        }
        return GetMD5String(hD + moAddress + cpuID);
    }

    public static string Token1()
    {
        string hD = GetHD();
        string moAddress = GetMoAddress1();
        string cpuID = GetCpuID();
        if (hD == null && moAddress == null && cpuID == null)
        {
            return "";
        }
        return GetMD5String(hD + moAddress + cpuID);
    }

    public static string GetCpuID()
    {
        try
        {
            string result = "";
            foreach (ManagementObject instance in new ManagementClass("Win32_Processor").GetInstances())
            {
                result = instance.Properties["ProcessorId"].Value.ToString();
            }
            return result;
        }
        catch
        {
            return "unknow";
        }
    }

    public static string GetHD()
    {
        try
        {
            foreach (ManagementObject item in new ManagementObjectSearcher("SELECT DiskIndex FROM Win32_DiskPartition WHERE Bootable = TRUE").Get())
            {
                using ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator2 = new ManagementObjectSearcher("SELECT Model FROM Win32_DiskDrive WHERE Index = " + Convert.ToInt32(item.Properties["DiskIndex"].Value)).Get().GetEnumerator();
                if (!managementObjectEnumerator2.MoveNext())
                {
                    continue;
                }
                return (string)((ManagementObject)managementObjectEnumerator2.Current).Properties["Model"].Value;
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static string GetMoAddress()
    {
        try
        {
            using (ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                foreach (ManagementObject instance in managementClass.GetInstances())
                {
                    if ((bool)instance["IPEnabled"])
                    {
                        instance["MacAddress"].ToString();
                    }
                    instance.Dispose();
                }
            }
            return "MoAddress";
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static string GetMoAddress1()
    {
        try
        {
            string result = null;
            using (ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                foreach (ManagementObject instance in managementClass.GetInstances())
                {
                    if ((bool)instance["IPEnabled"])
                    {
                        result = instance["MacAddress"].ToString();
                    }
                    instance.Dispose();
                }
            }
            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static string GetSystemTime()
    {
        try
        {
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectCollection managementObjectCollection = new ManagementObjectSearcher(new ManagementScope(), query).Get();
            string text = "";
            foreach (ManagementObject item in managementObjectCollection)
            {
                text = item.GetText(TextFormat.Mof);
            }
            string text2 = text.Substring(text.LastIndexOf("InstallDate") + 15, 14);
            UtilsCom.Log("SystemTime=" + text2);
            return null;
        }
        catch (Exception)
        {
            UtilsCom.Log("SystemTime=111");
            return null;
        }
    }

    public static string GetMD5String(string text)
    {
        byte[] array = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(text));
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            stringBuilder.Append(array[i].ToString("x2"));
        }
        return stringBuilder.ToString().ToLower();
    }
}
