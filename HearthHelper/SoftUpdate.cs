using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace HearthHelper;

public static class SoftUpdate
{
    public static bool bDownloadOk;

    public static string filename;

    private static int int_0;

    public static void DownloadFile(string downloadurl_loadname)
    {
        try
        {
            string uriString = downloadurl_loadname.Substring(0, downloadurl_loadname.IndexOf("@"));
            string text = downloadurl_loadname.Substring(downloadurl_loadname.LastIndexOf("@") + 1);
            filename = text.Substring(text.LastIndexOf("HearthStudy"));
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += smethod_0;
            webClient.DownloadProgressChanged += smethod_1;
            webClient.DownloadFileAsync(new Uri(uriString), text);
        }
        catch
        {
            UtilsCom.Log("升级包下载失败，网络连接异常，请稍后再试！");
        }
    }

    public static bool bDownloadFileOk()
    {
        return bDownloadOk;
    }

    private static void smethod_0(object object_0, object object_1)
    {
        UtilsCom.Log("升级包下载完成，准备安装...");
        bDownloadOk = true;
    }

    private static void smethod_1(object sender, ProgressChangedEventArgs e)
    {
        if (int_0 != e.ProgressPercentage)
        {
            int_0 = e.ProgressPercentage;
            if (int_0 > 0)
            {
                UtilsCom.Log($"升级包[{filename}]下载进度={int_0}%");
            }
        }
    }

    public static int bNeedUpdate(string oldVersion, ref string newVersion, ref string date, ref string downloadurl, ref string downloadname, ref string describe, ref string must, ref string auth, ref string enable, ref string reason)
    {
        try
        {
            string string_ = "";
            string text = "";
            WebClient webClient = new WebClient();
            using (StreamReader streamReader = new StreamReader(webClient.OpenRead("https://gitee.com/UniverseString/Hearthstone-myAccount/raw/master/myAccount/Ability.dat")))
            {
                string_ = streamReader.ReadToEnd();
            }
            text = Rsa.AesDecrypt(string_);
            Stream inStream = new MemoryStream(Encoding.UTF8.GetBytes(text));
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(inStream);
            foreach (XmlNode item in xmlDocument.SelectSingleNode("Ability"))
            {
                if (!(item.Name == "Version"))
                {
                    if (item.Name == "Date")
                    {
                        date = item.InnerText;
                    }
                    else if (!(item.Name == "DownLoad"))
                    {
                        if (item.Name == "Describe")
                        {
                            describe = item.InnerText;
                        }
                        else if (!(item.Name == "Must"))
                        {
                            if (!(item.Name == "Auth"))
                            {
                                if (item.Name == "Enable")
                                {
                                    enable = item.InnerText;
                                }
                                else if (item.Name == "Reason")
                                {
                                    reason = item.InnerText;
                                }
                            }
                            else
                            {
                                auth = item.InnerText;
                            }
                        }
                        else
                        {
                            must = item.InnerText;
                        }
                    }
                    else
                    {
                        downloadurl = item.InnerText;
                    }
                }
                else
                {
                    newVersion = item.InnerText;
                }
            }
            string path = Directory.GetCurrentDirectory() + "\\";
            string text2 = downloadurl.Substring(downloadurl.LastIndexOf("/") + 1);
            downloadname = Path.Combine(Path.GetDirectoryName(path), "Update_" + text2);
            Version value = new Version(newVersion);
            Version version = new Version(oldVersion);
            webClient.Dispose();
            if (version.CompareTo(value) >= 0)
            {
                return 1;
            }
            return 0;
        }
        catch (Exception)
        {
            describe = "检查更新失败，网络连接异常，请稍后再试！";
            return -1;
        }
    }

    static SoftUpdate()
    {
        bDownloadOk = false;
        filename = "";
        int_0 = 0;
    }
}
