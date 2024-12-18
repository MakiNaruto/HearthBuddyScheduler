using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HearthHelper;

public class UtilsPush
{
    public enum MSG_TYPE
    {
        MSG_NORMAL,
        MSG_REBOOT,
        MSG_START,
        MSG_STOP,
        MSG_CHANGE,
        MSG_TEST
    }

    private static string smethod_0(string string_0)
    {
        string result = "";
        if (!File.Exists(string_0))
        {
            return result;
        }
        using StreamReader streamReader = File.OpenText(string_0);
        using JsonTextReader reader = new JsonTextReader(streamReader);
        result = (string?)((JObject)JToken.ReadFrom(reader))["CurrAccountHashCode"];
        streamReader.Close();
        return result;
    }

    private static JObject smethod_1(string string_0, object object_0, int int_0, int int_1)
    {
        JObject jObject = new JObject();
        if (!File.Exists(string_0))
        {
            jObject.Add("文件状态", "读取监控配置文件失败");
            return jObject;
        }
        using StreamReader streamReader = File.OpenText(string_0);
        using JsonTextReader reader = new JsonTextReader(streamReader);
        JObject obj = (JObject)JToken.ReadFrom(reader);
        _ = (int)obj["Wins"];
        _ = (int)obj["Losses"];
        _ = (int)obj["Concedes"];
        int num = (int)obj["Level"];
        int num2 = (int)obj["Xp"];
        int num3 = (int)obj["XpNeeded"];
        int num4 = (int)obj["AllXp"];
        int num5 = (int)obj["AllXpNeeded"];
        string arg = (string?)obj["AllRunTimeText"];
        long num6 = (long)obj["AllGetXp"];
        int num7 = (int)obj["PerHourXp"];
        _ = (string?)obj["PerHourXpStr"];
        int num8 = (int)obj["FullXpNeeded"];
        string arg2 = (string?)obj["FullTimeNeeded"];
        string arg3 = (string?)obj["Collection"];
        string arg4 = (string?)obj["PassportEnd"];
        string arg5 = (string?)obj["TwistInfo"];
        string arg6 = (string?)obj["StandardInfo"];
        string arg7 = (string?)obj["WildInfo"];
        jObject.Add("0", $"战令等级={num}级 账号={object_0}");
        jObject.Add("1", $"升级经验={num2}/{num3} 重启次数={int_0}/{int_1}");
        jObject.Add("2", $"总体经验={num4}/{num5} 时长={arg}小时");
        jObject.Add("3", $"满级还差={num8}经验 {arg2}");
        jObject.Add("4", $"获取经验={num6} 挂机效率={num7}/小时");
        jObject.Add("5", $"账号资产={arg3}");
        jObject.Add("6", $"战令结束={arg4}");
        jObject.Add("7", $"幻变天梯={arg5}");
        jObject.Add("8", $"标准天梯={arg6}");
        jObject.Add("9", $"狂野天梯={arg7}");
        streamReader.Close();
        return jObject;
    }

    private static string smethod_2(string string_0, string string_1, JToken jtoken_0)
    {
        return new JObject
        {
            { "token", string_0 },
            { "title", string_1 },
            { "content", jtoken_0 },
            { "template", "json" }
        }.ToString();
    }

    public static bool PostMessage(string token, string oldAccount, string account, string rule, int todayRebootCnt, int rebootMaxCnt, MSG_TYPE type, out string result)
    {
        string s = "";
        if (token.Length > 0)
        {
            switch (type)
            {
                case MSG_TYPE.MSG_TEST:
                    {
                        JObject jObject3 = new JObject();
                        jObject3.Add("当前状态", "微信推送功能设置成功");
                        s = smethod_2(token, "恭喜恭喜", jObject3);
                        break;
                    }
                case MSG_TYPE.MSG_REBOOT:
                    {
                        JObject jtoken_3 = smethod_1("Settings/Default/Monitor" + smethod_0("Settings/Default/Dev.json") + ".json", UtilsCom.ReplaceWithSpecialChar(account), todayRebootCnt, rebootMaxCnt);
                        s = smethod_2(token, "运行异常--账号(" + UtilsCom.ReplaceWithSpecialChar(account) + ")", jtoken_3);
                        break;
                    }
                case MSG_TYPE.MSG_START:
                    {
                        JObject jObject2 = new JObject();
                        jObject2.Add("当前状态", "兄弟中控自动开始运行");
                        jObject2.Add("重启次数", todayRebootCnt + "/" + rebootMaxCnt);
                        s = smethod_2(token, "兄弟中控已开始运行", jObject2);
                        break;
                    }
                case MSG_TYPE.MSG_STOP:
                    {
                        JObject jObject = new JObject();
                        jObject.Add("当前状态", "兄弟中控自动停止运行");
                        jObject.Add("重启次数", todayRebootCnt + "/" + rebootMaxCnt);
                        s = smethod_2(token, "兄弟中控已停止运行", jObject);
                        break;
                    }
                case MSG_TYPE.MSG_CHANGE:
                    {
                        JObject jtoken_2 = smethod_1("Settings/Default/Monitor" + smethod_0("Settings/Default/Dev.json") + ".json", UtilsCom.ReplaceWithSpecialChar(oldAccount), todayRebootCnt, rebootMaxCnt);
                        s = smethod_2(token, "账号切换到(" + UtilsCom.ReplaceWithSpecialChar(account) + ")[" + rule + "]", jtoken_2);
                        break;
                    }
                case MSG_TYPE.MSG_NORMAL:
                    {
                        JObject jtoken_ = smethod_1("Settings/Default/Monitor" + smethod_0("Settings/Default/Dev.json") + ".json", UtilsCom.ReplaceWithSpecialChar(account), todayRebootCnt, rebootMaxCnt);
                        s = smethod_2(token, "运行正常--账号(" + UtilsCom.ReplaceWithSpecialChar(account) + ")", jtoken_);
                        break;
                    }
            }
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.pushplus.plus/send");
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                httpWebRequest.ContentLength = bytes.Length;
                using (Stream stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
                using StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream(), Encoding.UTF8);
                result = streamReader.ReadToEnd();
                if (!result.Contains("200"))
                {
                    result = "微信消息推送异常";
                    return false;
                }
                result = "微信消息推送成功";
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = "网络异常";
                return false;
            }
        }
        result = "请先获取Token后重试";
        return false;
    }
}
