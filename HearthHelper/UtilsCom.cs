using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HearthHelper;

public class UtilsCom
{
    public static Queue logQueue;

    public static void Log(string log)
    {
        logQueue.Enqueue(log);
    }

    public static void Delay(int mm)
    {
        DateTime now = DateTime.Now;
        while (now.AddMilliseconds(mm) > DateTime.Now)
        {
            Application.DoEvents();
        }
    }

    public static string ReplaceWithSpecialChar(string src, char specialChar = '*')
    {
        if (src.Length <= 0)
        {
            return "";
        }
        string text = src;
        try
        {
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            if (!text.Contains("@"))
            {
                num = 1;
                num3 = 1;
                if (text.Length < 2)
                {
                    num3 = 0;
                }
                if (text.Length >= 7)
                {
                    num = 3;
                    num3 = 2;
                }
                num2 = text.Length - num - num3;
            }
            else
            {
                int length = Regex.Split(text, "@", RegexOptions.IgnoreCase)[0].Length;
                num = 1;
                num3 = 1;
                if (length < 2)
                {
                    num3 = 0;
                }
                if (length >= 7)
                {
                    num = 2;
                    num3 = 2;
                }
                num2 = length - num - num3;
            }
            if (num2 > 0)
            {
                string text2 = text.Substring(num, num2);
                string text3 = string.Empty;
                for (int i = 0; i < text2.Length; i++)
                {
                    text3 += specialChar;
                }
                text = text.Replace(text2, text3);
            }
        }
        catch (Exception)
        {
            throw;
        }
        return text;
    }

    public static bool smethod_0(string inputData)
    {
        return new Regex("[一-龥]").Match(inputData).Success;
    }

    static UtilsCom()
    {
        logQueue = new Queue();
    }
}
