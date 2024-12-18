using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using HearthHelper;
using Newtonsoft.Json;

internal class ProgramOperationManagement
{
    private static int int_0;

    /// <summary> 炉石程序运行状态检查器 </summary>
    public static bool HearthStoneProcessStatusChecker(int int_1)
    {
        Process[] processesByName = Process.GetProcessesByName("Hearthstone");
        if (processesByName != null && processesByName.Length != 0)
        {
            Process[] array = processesByName;
            foreach (Process process in array)
            {
                if (int_1 == process.Id)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary> 炉石程序运行状态检查器 </summary>
    public static void StopBattleNetProcess(AccountItemWhole accountItemWhole_0, bool bool_0, bool bool_1, bool bool_2)
    {
        UtilsCom.Log("准备停止战网、炉石、战网更新进程");
        try
        {
            UtilsCom.Log("停止炉石中...");
            Process[] processesByName = Process.GetProcessesByName("Hearthstone");
            if (processesByName != null && processesByName.Length != 0)
            {
                Process[] array = processesByName;
                foreach (Process process in array)
                {
                    if (!bool_0)
                    {
                        process.Kill();
                        UtilsCom.Delay(1000);
                        UtilsCom.Log($"检测到炉石(Pid={process.Id})已停止");
                    }
                    else if (accountItemWhole_0.Running && accountItemWhole_0.StonePid == process.Id)
                    {
                        process.Kill();
                        UtilsCom.Delay(1000);
                        UtilsCom.Log($"检测到炉石(Pid={process.Id})已停止");
                        break;
                    }
                }
            }
            if (bool_1 || bool_2)
            {
                UtilsCom.Log("停止战网中...");
                processesByName = Process.GetProcessesByName("Battle.net");
                if (processesByName != null && processesByName.Length != 0)
                {
                    Process[] array = processesByName;
                    foreach (Process process2 in array)
                    {
                        process2.Kill();
                        UtilsCom.Delay(1000);
                        UtilsCom.Log($"检测到战网(Pid={process2.Id})已停止");
                    }
                }
            }
            UtilsCom.Log("停止战网更新进程中...");
            processesByName = Process.GetProcessesByName("Agent");
            if (processesByName != null && processesByName.Length != 0)
            {
                Process[] array = processesByName;
                foreach (Process process3 in array)
                {
                    process3.Kill();
                    UtilsCom.Delay(1000);
                    UtilsCom.Log($"检测到战网更新进程(Pid={process3.Id})已停止");
                }
            }
        }
        catch
        {
        }
    }

    [DllImport("kernel32.dll")]
    private static extern int WinExec(string string_0, int int_1);

    /// <summary> 运行战网 </summary>
    public static void LaunchBattleNet(string string_0)
    {
        while (Process.GetProcessesByName("Battle.net").Length < 1)
        {
            WinExec(string_0, 2);
            UtilsCom.Delay(5000);
        }
    }

    /// <summary> 运行炉石 </summary>
    public static bool LaunchHearthStone(object object_0, string string_0, ref bool bool_0, bool bool_1, bool bool_2, bool bool_3, bool bool_4, string string_1, int int_1, int int_2, int int_3, bool bool_5, int int_4, bool bool_6, int int_5, int int_6, int int_7)
    {
        if (bool_0)
        {
            try
            {
                UtilsIniFile utilsIniFile = new UtilsIniFile(Path.Combine(string_0, "doorstop_config.ini"));
                string text = "";
                string path = ApplicationPathManager.GetBattleNetConfigPath();
                dynamic val = JsonConvert.DeserializeObject(File.ReadAllText(path));
                if (bool_5)
                {
                    utilsIniFile.Write("enabled", "true", "UnityDoorstop");
                    if (((AccountItemWhole)object_0).currItem.Mode == 1)
                    {
                        text += "--autostart";
                        if (bool_4)
                        {
                            text += " --needHang";
                        }
                        if (((AccountItemWhole)object_0).currItem.MercConcede)
                        {
                            text += " --autoconcede";
                        }
                        if (((AccountItemWhole)object_0).currItem.MercCraft)
                        {
                            text += " --autocraft";
                        }
                        if (((AccountItemWhole)object_0).currItem.MercUpdate)
                        {
                            text += " --autoupdate";
                        }
                        if (((AccountItemWhole)object_0).StonePid != 0)
                        {
                            text = text + " --pid:" + ((AccountItemWhole)object_0).StonePid;
                        }
                        text = text + " --rule:" + ((AccountItemWhole)object_0).currItem.MercRule;
                        text = text + " --behavior:" + ((AccountItemWhole)object_0).currItem.MercBehavior;
                        if (!string.IsNullOrEmpty(((AccountItemWhole)object_0).currItem.MercTeam))
                        {
                            text = text + " --team:" + ((AccountItemWhole)object_0).currItem.MercTeam;
                        }
                        if (!string.IsNullOrEmpty(((AccountItemWhole)object_0).currItem.MercMap))
                        {
                            text = text + " --map:" + ((AccountItemWhole)object_0).currItem.MercMap;
                        }
                        text = text + " --interval:" + ((AccountItemWhole)object_0).currItem.MercInterval;
                        text = text + " --path:" + AppDomain.CurrentDomain.BaseDirectory;
                        text = text + " --hash:" + ((AccountItemWhole)object_0).Email.GetHashCode();
                    }
                    if (bool_6)
                    {
                        text = text + " --timeGear:" + int_5 + "," + int_6 + "," + int_7;
                    }
                    text = text + " --port:" + int_4;
                    text = text + " --matchPath:" + Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MercInfo"), "mercInfo" + ((AccountItemWhole)object_0).Email.GetHashCode() + ".txt");
                    text = text + " --width:" + int_2;
                    text = text + " --height:" + int_3;
                    text += (bool_5 ? " --afk:1" : " --afk:0");
                    val["Games"]["hs_beta"]["AdditionalLaunchArguments"] = text;
                    string contents = JsonConvert.SerializeObject(val, Formatting.Indented);
                    File.WriteAllText(path, contents);
                }
                UtilsCom.Log("准备启动炉石");
                while ((Process.GetProcessesByName("Battle.net").Length < 1) & bool_0)
                {
                    UtilsCom.Log("未检测到战网，启动战网中...，5秒后再次检测");
                    WinExec(string_1, 2);
                    UtilsCom.Delay(5000);
                }
                if (bool_0)
                {
                    Process[] processesByName = Process.GetProcessesByName("Battle.net");
                    foreach (Process process in processesByName)
                    {
                        UtilsCom.Log($"已检测到战网(Pid={process.Id})运行中");
                    }
                    int_0 = 0;
                    UtilsCom.Log($"{int_1}秒后启动炉石");
                    UtilsCom.Delay(1000 * int_1);
                    Process[] processesByName2 = Process.GetProcessesByName("Hearthstone");
                    while (true)
                    {
                        if (!((Process.GetProcessesByName("Hearthstone").Length <= processesByName2.Length) & bool_0))
                        {
                            if (!bool_0)
                            {
                                UtilsCom.Log("用户主动停止运行，终止后续启动");
                                return false;
                            }
                            processesByName = Process.GetProcessesByName("Hearthstone");
                            foreach (Process process2 in processesByName)
                            {
                                bool flag = true;
                                Process[] array = processesByName2;
                                foreach (Process process3 in array)
                                {
                                    if (process2.Id == process3.Id)
                                    {
                                        flag = false;
                                    }
                                }
                                if (flag)
                                {
                                    ((AccountItemWhole)object_0).StonePid = process2.Id;
                                    break;
                                }
                            }
                            UtilsCom.Log($"已检测到炉石(Pid={((AccountItemWhole)object_0).StonePid})运行中");
                            if (bool_2 || bool_3)
                            {
                                UtilsCom.Log("停止战网中...");
                                while ((Process.GetProcessesByName("Battle.net").Length != 0) & bool_0)
                                {
                                    processesByName = Process.GetProcessesByName("Battle.net");
                                    foreach (Process process4 in processesByName)
                                    {
                                        try
                                        {
                                            process4.Kill();
                                            UtilsCom.Delay(1000);
                                            UtilsCom.Log($"检测到战网(Pid={process4.Id})已停止");
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                if (!bool_0)
                                {
                                    UtilsCom.Log("用户主动停止运行，终止后续启动");
                                    return false;
                                }
                            }
                            UtilsCom.Log("停止战网更新进程中...");
                            Process[] processesByName3 = Process.GetProcessesByName("Agent");
                            if (processesByName3 != null && processesByName3.Length != 0)
                            {
                                processesByName = processesByName3;
                                foreach (Process process5 in processesByName)
                                {
                                    process5.Kill();
                                    UtilsCom.Delay(1000);
                                    UtilsCom.Log($"检测到战网更新进程(Pid={process5.Id})已停止");
                                }
                            }
                            if (bool_5)
                            {
                                val = JsonConvert.DeserializeObject(File.ReadAllText(path));
                                val["Games"]["hs_beta"]["AdditionalLaunchArguments"] = "";
                                string contents = JsonConvert.SerializeObject(val, Formatting.Indented);
                                File.WriteAllText(path, contents);
                                utilsIniFile.Write("enabled", "false", "UnityDoorstop");
                            }
                            return true;
                        }
                        UtilsCom.Log("未检测到炉石，启动炉石中...");
                        if (bool_1 && processesByName2.Length != 0)
                        {
                            UtilsCom.Log("先停止战网更新进程...");
                            Process[] processesByName4 = Process.GetProcessesByName("Agent");
                            if (processesByName4 != null && processesByName4.Length != 0)
                            {
                                processesByName = processesByName4;
                                foreach (Process process6 in processesByName)
                                {
                                    process6.Kill();
                                    UtilsCom.Delay(1000);
                                    UtilsCom.Log($"检测到战网更新进程(Pid={process6.Id})已停止");
                                }
                            }
                            UtilsCom.Log("再次检测战网更新进程...");
                            while ((Process.GetProcessesByName("Agent").Length < 1) & bool_0)
                            {
                                int_0++;
                                UtilsCom.Log("未检测到战网更新进程...，1秒后再次检测");
                                UtilsCom.Delay(1000);
                                if (int_0 > 200)
                                {
                                    UtilsCom.Log("200秒内一直检测不到战网更新进程，停止后续启动");
                                    return false;
                                }
                            }
                            if (!bool_0)
                            {
                                break;
                            }
                            UtilsCom.Log("检测到战网更新进程已运行，4秒后启动炉石...");
                            UtilsCom.Delay(4000);
                        }
                        Process[] processesByName5 = Process.GetProcessesByName("Battle.net");
                        int num = 0;
                        if (0 < processesByName5.Length)
                        {
                            Process.Start(processesByName5[num].MainModule.FileName, "--exec=\"launch WTCG\"");
                        }
                        UtilsCom.Delay(5000);
                    }
                    UtilsCom.Log("用户主动停止运行，终止后续启动");
                    return false;
                }
                UtilsCom.Log("用户主动停止运行，终止后续启动");
                return false;
            }
            catch (Exception ex)
            {
                UtilsCom.Log(ex.ToString());
                return false;
            }
        }
        UtilsCom.Log("用户主动停止运行，终止后续启动");
        return false;
    }

    /// <summary> 运行炉石兄弟 </summary>
    public static void LaunchHearthBuddy(object object_0, ref bool bool_0, string string_0, int int_1, int int_2, int int_3)
    {
        if (bool_0)
        {
            UtilsCom.Log("准备启动炉石兄弟");
            try
            {
                UtilsCom.Log("未检测到炉石兄弟，启动炉石兄弟中...");
                Process process = new Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = string_0;
                process.StartInfo.Arguments = "--autostart --config:Default";
                if (((AccountItemWhole)object_0).StonePid != 0)
                {
                    ProcessStartInfo startInfo = process.StartInfo;
                    startInfo.Arguments = startInfo.Arguments + " --pid:" + ((AccountItemWhole)object_0).StonePid;
                }
                if (!string.IsNullOrEmpty(((AccountItemWhole)object_0).currItem.NormalDeck))
                {
                    ProcessStartInfo startInfo2 = process.StartInfo;
                    startInfo2.Arguments = startInfo2.Arguments + " --deck:" + ((AccountItemWhole)object_0).currItem.NormalDeck;
                }
                ProcessStartInfo startInfo3 = process.StartInfo;
                startInfo3.Arguments = startInfo3.Arguments + " --behavior:" + ((AccountItemWhole)object_0).currItem.NormalBehavior;
                ProcessStartInfo startInfo4 = process.StartInfo;
                startInfo4.Arguments = startInfo4.Arguments + " --rule:" + ((AccountItemWhole)object_0).currItem.NormalRule;
                ProcessStartInfo startInfo5 = process.StartInfo;
                startInfo5.Arguments = startInfo5.Arguments + " --width:" + int_2;
                ProcessStartInfo startInfo6 = process.StartInfo;
                startInfo6.Arguments = startInfo6.Arguments + " --height:" + int_3;
                ProcessStartInfo startInfo7 = process.StartInfo;
                startInfo7.Arguments = startInfo7.Arguments + " --os:" + ((int_1 == 0) ? "10" : "7");
                process.Start();
                UtilsCom.Log("已经启动炉石兄弟，是否成功，听天由命~");
                return;
            }
            catch (Exception ex)
            {
                UtilsCom.Log(ex.ToString());
                return;
            }
        }
        UtilsCom.Log("用户主动停止运行，终止后续启动");
    }
}
