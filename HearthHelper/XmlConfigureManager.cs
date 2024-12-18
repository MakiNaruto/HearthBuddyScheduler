using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using HearthHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

internal class XmlConfigureManager
{
    public static void CondfigReader(ref ObservableCollection<AccountItemWhole> observableCollection_0, ref string string_0, ref string string_1, ref string string_2, ref string string_3, ref int int_0, ref int int_1, ref int int_2, ref int int_3, ref int int_4, ref int int_5, ref int int_6, ref int int_7, ref bool bool_0, ref bool bool_1, ref bool bool_2, ref bool bool_3, ref bool bool_4, ref int int_8, ref bool bool_5, ref int int_9, ref int int_10, ref int int_11)
    {
        UtilsXml utilsXml = new UtilsXml("Settings/Default/HearthHelper.xml");
        string_0 = utilsXml.Read("BattleNetPath");
        string_1 = utilsXml.Read("HearthstonePath");
        string_2 = utilsXml.Read("HearthbuddyPath");
        string_3 = utilsXml.Read("PushPlusToken");
        string_0 = ApplicationPathManager.GetBattleNetLauncherPath();
        if (string.IsNullOrEmpty(string_0) || !string_0.Contains("Battle.net"))
        {
            string_0 = ApplicationPathManager.GetBattleNetLauncherPath();
        }
        if (string.IsNullOrEmpty(string_1) || !string_1.Contains("Hearthstone") || string_1.Contains("Assembly-CSharp.dll"))
        {
            string_1 = ApplicationPathManager.GetHearthStoneLauncherPath();
        }
        if (string.IsNullOrEmpty(string_2) || !string_2.Contains(AppDomain.CurrentDomain.BaseDirectory))
        {
            string_2 = ApplicationPathManager.GetHearthBuddyLauncherPath();
        }
        try
        {
            int_0 = int.Parse(utilsXml.Read("BNHSInterval"));
        }
        catch
        {
            int_0 = 20;
            UtilsCom.Log($"读取数据错误，恢复默认值={20}");
        }
        try
        {
            int_1 = int.Parse(utilsXml.Read("HSHBInterval"));
        }
        catch
        {
            int_1 = 30;
            UtilsCom.Log($"读取数据错误，恢复默认值={30}");
        }
        try
        {
            int_2 = int.Parse(utilsXml.Read("CheckInterval"));
        }
        catch
        {
            int_2 = 5;
            UtilsCom.Log($"读取数据错误，恢复默认值={5}");
        }
        try
        {
            int_3 = int.Parse(utilsXml.Read("RebootCntMax"));
        }
        catch
        {
            int_3 = 30;
            UtilsCom.Log($"读取数据错误，恢复默认值={30}");
        }
        try
        {
            int_4 = int.Parse(utilsXml.Read("PushNormalInterval"));
        }
        catch
        {
            int_4 = 2;
            UtilsCom.Log($"读取数据错误，恢复默认值={2}");
        }
        try
        {
            bool_0 = bool.Parse(utilsXml.Read("NeedCloseBattle"));
        }
        catch
        {
            bool_0 = true;
            UtilsCom.Log($"读取数据错误，恢复默认值={true}");
        }
        try
        {
            bool_1 = bool.Parse(utilsXml.Read("NeedMultStone"));
        }
        catch
        {
            bool_1 = false;
            UtilsCom.Log($"读取数据错误，恢复默认值={false}");
        }
        try
        {
            bool_2 = bool.Parse(utilsXml.Read("NeedPushMessage"));
        }
        catch
        {
            bool_2 = true;
            UtilsCom.Log($"读取数据错误，恢复默认值={true}");
        }
        try
        {
            bool_3 = bool.Parse(utilsXml.Read("NeedPushNormal"));
        }
        catch
        {
            bool_3 = false;
            UtilsCom.Log($"读取数据错误，恢复默认值={false}");
        }
        try
        {
            int_5 = int.Parse(utilsXml.Read("SystemVersion"));
        }
        catch
        {
            int_5 = 0;
            UtilsCom.Log($"读取数据错误，恢复默认值={0}");
        }
        try
        {
            int_6 = int.Parse(utilsXml.Read("WindowWidth"));
        }
        catch
        {
            int_6 = 144;
            UtilsCom.Log($"读取数据错误，恢复默认值={144}");
        }
        try
        {
            int_7 = int.Parse(utilsXml.Read("WindowHeight"));
        }
        catch
        {
            int_7 = 108;
            UtilsCom.Log($"读取数据错误，恢复默认值={108}");
        }
        try
        {
            bool_4 = bool.Parse(utilsXml.Read("EnableHsMod"));
        }
        catch
        {
            bool_4 = false;
            UtilsCom.Log($"读取数据错误，恢复默认值={false}");
        }
        try
        {
            int_8 = int.Parse(utilsXml.Read("HsModPort"));
        }
        catch
        {
            int_8 = 58744;
            UtilsCom.Log($"读取数据错误，恢复默认值={58744}");
        }
        try
        {
            bool_5 = bool.Parse(utilsXml.Read("EnableTimeGear"));
        }
        catch
        {
            bool_5 = false;
            UtilsCom.Log($"读取数据错误，恢复默认值={false}");
        }
        try
        {
            int_9 = int.Parse(utilsXml.Read("NoFightTime"));
        }
        catch
        {
            int_9 = 2;
            UtilsCom.Log($"读取数据错误，恢复默认值={2}");
        }
        try
        {
            int_10 = int.Parse(utilsXml.Read("PveFightTime"));
        }
        catch
        {
            int_10 = 4;
            UtilsCom.Log($"读取数据错误，恢复默认值={4}");
        }
        try
        {
            int_11 = int.Parse(utilsXml.Read("PvpFightTime"));
        }
        catch
        {
            int_11 = 1;
            UtilsCom.Log($"读取数据错误，恢复默认值={1}");
        }
        string path = ApplicationPathManager.GetBattleNetConfigPath();
        try
        {
            using StreamReader streamReader = File.OpenText(path);
            using JsonTextReader reader = new JsonTextReader(streamReader);
            string[] array = Regex.Split((string?)((JObject)JToken.ReadFrom(reader))["Client"]["SavedAccountNames"], ",", RegexOptions.IgnoreCase);
            foreach (string whichEmail in array)
            {
                observableCollection_0.Add(new AccountItemWhole(isSelected: false, whichEmail));
            }
            streamReader.Close();
        }
        catch (Exception ex)
        {
            UtilsCom.Log(ex.Message);
            UtilsCom.Log("无法读取战网用户数据");
        }
        foreach (AccountItemWhole item in observableCollection_0)
        {
            try
            {
                item.Selected = bool.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), "Selected"));
                int num = 0;
                while (true)
                {
                    string text = $"AccountItem{num++}";
                    if (utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text) != null)
                    {
                        AccountItemSingle accountItemSingle = new AccountItemSingle(0);
                        accountItemSingle.Mode = int.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "Mode"));
                        accountItemSingle.Enable = bool.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "Enable"));
                        accountItemSingle.StartTimeHour = int.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "StartTimeHour"));
                        accountItemSingle.StartTimeMin = int.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "StartTimeMin"));
                        accountItemSingle.EndTimeHour = int.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "EndTimeHour"));
                        accountItemSingle.EndTimeMin = int.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "EndTimeMin"));
                        accountItemSingle.NormalRule = int.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "NormalRule"));
                        accountItemSingle.NormalBehavior = int.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "NormalBehavior"));
                        accountItemSingle.NormalDeck = utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "NormalDeck");
                        accountItemSingle.MercRule = int.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercRule"));
                        accountItemSingle.MercBehavior = int.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercBehavior"));
                        accountItemSingle.MercTeam = utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercTeam");
                        accountItemSingle.MercMap = utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercMap");
                        try
                        {
                            accountItemSingle.MercInterval = int.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercInterval"));
                        }
                        catch
                        {
                            accountItemSingle.MercInterval = 22;
                        }
                        try
                        {
                            accountItemSingle.MercConcede = bool.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercConcede"));
                        }
                        catch
                        {
                            accountItemSingle.MercConcede = false;
                        }
                        try
                        {
                            accountItemSingle.MercCraft = bool.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercCraft"));
                        }
                        catch
                        {
                            accountItemSingle.MercCraft = true;
                        }
                        try
                        {
                            accountItemSingle.MercUpdate = bool.Parse(utilsXml.Read("BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercUpdate"));
                        }
                        catch
                        {
                            accountItemSingle.MercUpdate = false;
                        }
                        item.itemList.Add(accountItemSingle);
                        continue;
                    }
                    break;
                }
            }
            catch
            {
                UtilsCom.Log("读取账号" + item.EmailShow + "数据错误");
            }
        }
    }

    public static void CondfigWriter(ObservableCollection<AccountItemWhole> observableCollection_0, string string_0, string string_1, string string_2, string string_3, int int_0, int int_1, int int_2, int int_3, int int_4, int int_5, int int_6, int int_7, bool bool_0, bool bool_1, bool bool_2, bool bool_3, bool bool_4, int int_8, bool bool_5, int int_9, int int_10, int int_11)
    {
        try
        {
            File.Delete("Settings/Default/HearthHelper.xml");
            UtilsXml utilsXml = new UtilsXml("Settings/Default/HearthHelper.xml");
            utilsXml.Write(string_0, "BattleNetPath");
            utilsXml.Write(string_1, "HearthstonePath");
            utilsXml.Write(string_2, "HearthbuddyPath");
            utilsXml.Write(string_3, "PushPlusToken");
            utilsXml.Write(int_0.ToString(), "BNHSInterval");
            utilsXml.Write(int_1.ToString(), "HSHBInterval");
            utilsXml.Write(int_2.ToString(), "CheckInterval");
            utilsXml.Write(int_3.ToString(), "RebootCntMax");
            utilsXml.Write(int_5.ToString(), "SystemVersion");
            utilsXml.Write(int_6.ToString(), "WindowWidth");
            utilsXml.Write(int_7.ToString(), "WindowHeight");
            utilsXml.Write(int_4.ToString(), "PushNormalInterval");
            utilsXml.Write(bool_0.ToString(), "NeedCloseBattle");
            utilsXml.Write(bool_1.ToString(), "NeedMultStone");
            utilsXml.Write(bool_2.ToString(), "NeedPushMessage");
            utilsXml.Write(bool_3.ToString(), "NeedPushNormal");
            utilsXml.Write(bool_4.ToString(), "EnableHsMod");
            utilsXml.Write(int_8.ToString(), "HsModPort");
            utilsXml.Write(bool_5.ToString(), "EnableTimeGear");
            utilsXml.Write(int_9.ToString(), "NoFightTime");
            utilsXml.Write(int_10.ToString(), "PveFightTime");
            utilsXml.Write(int_11.ToString(), "PvpFightTime");
            foreach (AccountItemWhole item in observableCollection_0)
            {
                utilsXml.Write(item.Selected.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), "Selected");
                int num = 0;
                foreach (AccountItemSingle item2 in item.itemList)
                {
                    string text = $"AccountItem{num++}";
                    utilsXml.Write(item2.Mode.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "Mode");
                    utilsXml.Write(item2.Enable.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "Enable");
                    utilsXml.Write(item2.StartTimeHour.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "StartTimeHour");
                    utilsXml.Write(item2.StartTimeMin.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "StartTimeMin");
                    utilsXml.Write(item2.EndTimeHour.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "EndTimeHour");
                    utilsXml.Write(item2.EndTimeMin.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "EndTimeMin");
                    utilsXml.Write(item2.NormalRule.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "NormalRule");
                    utilsXml.Write(item2.NormalBehavior.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "NormalBehavior");
                    utilsXml.Write(item2.NormalDeck, "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "NormalDeck");
                    utilsXml.Write(item2.MercRule.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercRule");
                    utilsXml.Write(item2.MercBehavior.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercBehavior");
                    utilsXml.Write(item2.MercTeam, "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercTeam");
                    utilsXml.Write(item2.MercMap, "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercMap");
                    utilsXml.Write(item2.MercInterval.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercInterval");
                    utilsXml.Write(item2.MercConcede.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercConcede");
                    utilsXml.Write(item2.MercCraft.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercCraft");
                    utilsXml.Write(item2.MercUpdate.ToString(), "BattleNetAccount", "HASH" + item.Email.GetHashCode(), text, "MercUpdate");
                }
            }
        }
        catch
        {
        }
    }
}
