using System;
using System.Collections.ObjectModel;
using System.IO;
using HearthHelper;
using Newtonsoft.Json;

internal class HearthBuddyExecutionScheduler
{
    /// <summary> 账号运行时间检查 </summary>
    public static bool UserRunningTimeChecker(AccountItemSingle accountItemSingle_0)
    {
        int num = accountItemSingle_0.StartTimeHour;
        int num2 = accountItemSingle_0.StartTimeMin;
        int num3 = accountItemSingle_0.EndTimeHour;
        int num4 = accountItemSingle_0.EndTimeMin;
        int hour = DateTime.Now.Hour;
        int minute = DateTime.Now.Minute;
        bool flag = false;
        if (num > num3 || (num == num3 && num2 > num4))
        {
            flag = true;
            int num5 = num;
            num = num3;
            num3 = num5;
            int num6 = num2;
            num2 = num4;
            num4 = num6;
        }
        bool flag2 = (hour > num || (hour == num && minute >= num2)) && (hour < num3 || (hour == num3 && minute <= num4));
        if (!accountItemSingle_0.Enable)
        {
            return false;
        }
        if (!flag)
        {
            return flag2;
        }
        return !flag2;
    }

    /// <summary> 添加战网账号 </summary>
    public static bool AddBattleNetAccount(ObservableCollection<AccountItemWhole> observableCollection_0, string string_0)
    {
        string path = ApplicationPathManager.GetBattleNetConfigPath();
        try
        {
            string text = "";
            text += string_0;
            foreach (AccountItemWhole item in observableCollection_0)
            {
                text = text + "," + item.Email;
            }
            dynamic val = JsonConvert.DeserializeObject(File.ReadAllText(path));
            val["Client"]["SavedAccountNames"] = text;
            string contents = JsonConvert.SerializeObject(val, Formatting.Indented);
            File.WriteAllText(path, contents);
            UtilsCom.Log("添加战网新用户" + UtilsCom.ReplaceWithSpecialChar(string_0));
            return true;
        }
        catch (Exception ex)
        {
            UtilsCom.Log(ex.Message);
            UtilsCom.Log("无法加战战网用户数据");
            return false;
        }
    }

    /// <summary> 删除战网账号 </summary>
    public static bool DeleteBattleNetAccount(ObservableCollection<AccountItemWhole> observableCollection_0, string string_0)
    {
        string path = ApplicationPathManager.GetBattleNetConfigPath();
        try
        {
            string text = "";
            foreach (AccountItemWhole item in observableCollection_0)
            {
                if (item.Email != string_0)
                {
                    text += (string.IsNullOrEmpty(text) ? item.Email : ("," + item.Email));
                }
            }
            dynamic val = JsonConvert.DeserializeObject(File.ReadAllText(path));
            val["Client"]["SavedAccountNames"] = text;
            string contents = JsonConvert.SerializeObject(val, Formatting.Indented);
            File.WriteAllText(path, contents);
            UtilsCom.Log("删除战网用户" + UtilsCom.ReplaceWithSpecialChar(string_0) + "成功");
            return true;
        }
        catch (Exception ex)
        {
            UtilsCom.Log(ex.Message);
            UtilsCom.Log("无法删除战网用户数据");
            return false;
        }
    }

    /// <summary> 修改战网账号 </summary>
    public static bool EditBattleNetAccount(ObservableCollection<AccountItemWhole> observableCollection_0, string string_0)
    {
        string path = ApplicationPathManager.GetBattleNetConfigPath();
        try
        {
            string text = "";
            text += string_0;
            foreach (AccountItemWhole item in observableCollection_0)
            {
                if (item.Email != string_0)
                {
                    text = text + "," + item.Email;
                }
            }
            dynamic val = JsonConvert.DeserializeObject(File.ReadAllText(path));
            val["Client"]["SavedAccountNames"] = text;
            string contents = JsonConvert.SerializeObject(val, Formatting.Indented);
            File.WriteAllText(path, contents);
            UtilsCom.Log("修改当前战网用户为" + UtilsCom.ReplaceWithSpecialChar(string_0) + "成功");
            return true;
        }
        catch (Exception ex)
        {
            UtilsCom.Log(ex.Message);
            UtilsCom.Log("无法修改战网用户数据");
            return false;
        }
    }

    /// <summary> 满足条件的账号进入执行队列 </summary>
    public static bool HearthBuddyProgramExecuter(bool bool_0, ref bool bool_1, bool bool_2, string string_0, int int_0, int int_1, ObservableCollection<AccountItemWhole> observableCollection_0, object object_0, out AccountItemWhole accountItemWhole_0)
    {
        foreach (AccountItemWhole item in observableCollection_0)
        {
            foreach (AccountItemSingle item2 in item.itemList)
            {
                if (!(item.Selected && bool_0) || !UserRunningTimeChecker(item2))
                {
                    continue;
                }
                if ((((AccountItemWhole)object_0).Email.Length > 0 && !string.Equals(((AccountItemWhole)object_0).Email, item.Email)) || (((AccountItemWhole)object_0).currItem != null && (((AccountItemWhole)object_0).currItem.Mode != item2.Mode || ((AccountItemWhole)object_0).currItem.NormalRule != item2.NormalRule || ((AccountItemWhole)object_0).currItem.MercRule != item2.MercRule)))
                {
                    bool_1 = true;
                    if (bool_2)
                    {
                        UtilsPush.PostMessage(string_0, ((AccountItemWhole)object_0).Email, item.Email, (item2.Mode == 0) ? StringConst.rule0[item2.NormalRule] : StringConst.rule1[item2.MercRule], int_0, int_1, UtilsPush.MSG_TYPE.MSG_CHANGE, out var result);
                        UtilsCom.Log(result);
                    }
                }
                accountItemWhole_0 = item;
                accountItemWhole_0.currItem = item2;
                UtilsCom.Log("账号" + accountItemWhole_0.EmailShow + "满足挂机时间");
                if (item2.Mode == 0)
                {
                    UtilsCom.Log("天梯时间(" + item2.StartTimeHour.ToString("D2") + ":" + item2.StartTimeMin.ToString("D2") + "-" + item2.EndTimeHour.ToString("D2") + ":" + item2.EndTimeMin.ToString("D2") + ")");
                }
                else if (item2.Mode == 1)
                {
                    UtilsCom.Log("佣兵时间(" + item2.StartTimeHour.ToString("D2") + ":" + item2.StartTimeMin.ToString("D2") + "-" + item2.EndTimeHour.ToString("D2") + ":" + item2.EndTimeMin.ToString("D2") + ")");
                }
                return true;
            }
        }
        accountItemWhole_0 = new AccountItemWhole(isSelected: false, "");
        accountItemWhole_0.currItem = null;
        return false;
    }
}
