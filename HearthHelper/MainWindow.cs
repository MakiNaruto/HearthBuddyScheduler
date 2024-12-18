using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;

namespace HearthHelper;

public partial class MainWindow : Window, IComponentConnector, IStyleConnector
{
    #region 成员变量
    public ObservableCollection<AccountItemWhole> AccountList = new ObservableCollection<AccountItemWhole>();

    private readonly System.Windows.Forms.Timer LogTextBoxScrollerTimer = new System.Windows.Forms.Timer();

    private readonly System.Windows.Forms.Timer ProgramStatusCheckerTimer = new System.Windows.Forms.Timer();

    private readonly System.Windows.Forms.Timer ProgramControlResetTimer = new System.Windows.Forms.Timer();

    private DispatcherTimer dispatcherTimer_0;

    private DispatcherTimer dispatcherTimer_1;

    private TimeSpan timeSpan_0 = TimeSpan.Zero;

    private AccountItemWhole accountItemWhole_0 = new AccountItemWhole(isSelected: false, "");

    private bool bool_0;

    private bool bool_1;

    private int int_0;

    private DateTime dateTime_0;

    private long long_0 = DateTime.Now.ToBinary() / 1000L / 10000L - 86400L;

    private int int_1 = 3600000;

    private int int_2 = 60000;

    private static int int_3;

    private static bool bool_2;

    // Token: 0x0400007F RID: 127
    private bool bool_3;

    #endregion

    #region settter getter 方法
    private string BattleNetPath
    {
        get
        {
            return BattleNetPathTextBox.Text;
        }
        set
        {
            BattleNetPathTextBox.Text = value;
        }
    }

    private string HearthstonePath
    {
        get
        {
            return HearthstonePathTextBox.Text;
        }
        set
        {
            HearthstonePathTextBox.Text = value;
        }
    }

    private string HearthbuddyPath
    {
        get
        {
            return HearthbuddyPathTextBox.Text;
        }
        set
        {
            HearthbuddyPathTextBox.Text = value;
        }
    }

    private int BNHSInterval
    {
        get
        {
            if (!int.TryParse(BNHSIntervalTextBox.Text, out var result))
            {
                return 20;
            }
            return result;
        }
        set
        {
            BNHSIntervalTextBox.Text = value.ToString();
        }
    }

    private int HSHBInterval
    {
        get
        {
            if (int.TryParse(HSHBIntervalTextBox.Text, out var result))
            {
                return result;
            }
            return 30;
        }
        set
        {
            HSHBIntervalTextBox.Text = value.ToString();
        }
    }

    private int CheckInterval
    {
        get
        {
            if (int.TryParse(CheckIntervalTextBox.Text, out var result))
            {
                return result;
            }
            return 5;
        }
        set
        {
            CheckIntervalTextBox.Text = value.ToString();
        }
    }

    private bool NeedCloseBattle
    {
        get
        {
            return NeedCloseBattleCheckBox.IsChecked.Value;
        }
        set
        {
            NeedCloseBattleCheckBox.IsChecked = value;
        }
    }

    private bool NeedMultStone
    {
        get
        {
            return NeedMultStoneCheckBox.IsChecked.Value;
        }
        set
        {
            NeedMultStoneCheckBox.IsChecked = value;
        }
    }

    private int RebootCntMax
    {
        get
        {
            if (int.TryParse(RebootMaxCntTextBox.Text, out var result))
            {
                return result;
            }
            return 5;
        }
        set
        {
            RebootMaxCntTextBox.Text = value.ToString();
        }
    }

    private int SystemVersion
    {
        get
        {
            return SystemVersionComboBox.SelectedIndex;
        }
        set
        {
            SystemVersionComboBox.SelectedIndex = value;
        }
    }

    private int WindowWidth
    {
        get
        {
            if (!int.TryParse(WindowWidthTextBox.Text, out var result))
            {
                return 144;
            }
            return result;
        }
        set
        {
            WindowWidthTextBox.Text = value.ToString();
        }
    }

    private int WindowHeight
    {
        get
        {
            if (!int.TryParse(WindowHeightTextBox.Text, out var result))
            {
                return 108;
            }
            return result;
        }
        set
        {
            WindowHeightTextBox.Text = value.ToString();
        }
    }

    private string PushPlusToken
    {
        get
        {
            return PushPlusTokenTextBox.Text;
        }
        set
        {
            PushPlusTokenTextBox.Text = value;
        }
    }

    private bool NeedPushMessage
    {
        get
        {
            return NeedPushMessageCheckBox.IsChecked.Value;
        }
        set
        {
            NeedPushMessageCheckBox.IsChecked = value;
        }
    }

    private bool NeedPushNormal
    {
        get
        {
            return NeedPushNormalCheckBox.IsChecked.Value;
        }
        set
        {
            NeedPushNormalCheckBox.IsChecked = value;
        }
    }

    private int PushNormalInterval
    {
        get
        {
            if (int.TryParse(PushNormalIntervalTextBox.Text, out var result))
            {
                return result;
            }
            return 2;
        }
        set
        {
            PushNormalIntervalTextBox.Text = value.ToString();
        }
    }

    public bool EnableHsMod
    {
        get
        {
            return EnableHsModCheckBox.IsChecked.Value;
        }
        set
        {
            EnableHsModCheckBox.IsChecked = value;
        }
    }

    public int HsModPort
    {
        get
        {
            if (!int.TryParse(HsModPortTextBox.Text, out var result))
            {
                return 58744;
            }
            return result;
        }
        set
        {
            HsModPortTextBox.Text = value.ToString();
        }
    }

    public bool EnableTimeGear
    {
        get
        {
            return EnableTimeGearCheckBox.IsChecked.Value;
        }
        set
        {
            EnableTimeGearCheckBox.IsChecked = value;
        }
    }

    public int NoFightTime
    {
        get
        {
            if (!int.TryParse(NoFightTimeTextBox.Text, out var result))
            {
                return 2;
            }
            return result;
        }
        set
        {
            NoFightTimeTextBox.Text = value.ToString();
        }
    }

    public int PveFightTime
    {
        get
        {
            if (int.TryParse(PveFightTimeTextBox.Text, out var result))
            {
                return result;
            }
            return 4;
        }
        set
        {
            PveFightTimeTextBox.Text = value.ToString();
        }
    }

    public int PvpFightTime
    {
        get
        {
            if (int.TryParse(PvpFightTimeTextBox.Text, out var result))
            {
                return result;
            }
            return 1;
        }
        set
        {
            PvpFightTimeTextBox.Text = value.ToString();
        }
    }

    #endregion

    #region 按钮点击触发的方法

    private void AddAccountButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Rsa.AddUser();
            UtilsCom.Log("批量注册学习账号成功");
        }
        catch
        {
            UtilsCom.Log("请检查学习配置文件");
        }
    }

    private void ConfigAccountButtonView_Click(object sender, RoutedEventArgs e)
    {
        if (accountItemWhole_0.Running && EnableHsMod)
        {
            Process.Start(string.Format($"http://localhost:{HsModPort}"));
        }
        else
        {
            UtilsCom.Log("请先安装和启用HsMod并且运行程序");
        }
    }

    private void ConfigAccountButtonAdd_Click(object sender, RoutedEventArgs e)
    {
        if (AccountList.Count >= 5)
        {
            System.Windows.MessageBox.Show("当前账号已达5个，无法再添加", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        else if (sender is System.Windows.Controls.Button)
        {
            string text = "";
            AccountPopupAdd accountPopupAdd = new AccountPopupAdd();
            accountPopupAdd.Left = base.Left + (base.Width - accountPopupAdd.Width) / 2.0;
            accountPopupAdd.Top = base.Top + (base.Height - accountPopupAdd.Height) / 2.0;
            if (accountPopupAdd.ShowDialog().Value)
            {
                text = accountPopupAdd.GetAccount();
            }
            accountPopupAdd.Close();
            if (!string.IsNullOrEmpty(text) && HearthBuddyExecutionScheduler.AddBattleNetAccount(AccountList, text))
            {
                AccountList.Insert(0, new AccountItemWhole(isSelected: false, text));
                ProgramOperationManagement.StopBattleNetProcess(accountItemWhole_0, bool_0: false, bool_1: true, bool_2: true);
                UtilsCom.Delay(2000);
                ProgramOperationManagement.LaunchBattleNet(BattleNetPath);
            }
        }
    }

    private void SelectBattleNetFileButton_Click(object sender, RoutedEventArgs e)
    {
        string text = ApplicationPathManager.GetSpecificTypeLauncherChoosePath("Battle.net Launcher.exe|*.exe");
        if (!string.IsNullOrEmpty(text))
        {
            BattleNetPath = text;
            UtilsCom.Log("战网路径配置成功：");
            UtilsCom.Log(text);
        }
    }

    private void SelectHearthstoneFileButton_Click(object sender, RoutedEventArgs e)
    {
        string text = ApplicationPathManager.GetHearthBuddyLauncherChoosePath();
        if (!string.IsNullOrEmpty(text))
        {
            HearthstonePath = text;
            UtilsCom.Log("炉石路径配置成功：");
            UtilsCom.Log(text);
        }
    }

    private void SelectHearthbuddyFileButton_Click(object sender, RoutedEventArgs e)
    {
        string text = ApplicationPathManager.GetSpecificTypeLauncherChoosePath("Hearthbuddy.exe|*.exe");
        if (!string.IsNullOrEmpty(text))
        {
            HearthbuddyPath = text;
            UtilsCom.Log("兄弟路径配置成功：");
            UtilsCom.Log(text);
        }
    }

    private void GetPushTokenButton_Click(object sender, RoutedEventArgs e)
    {
        Process.Start("https://www.pushplus.plus/push1.html");
        UtilsCom.Log("IE浏览器扫码后无法跳页面时，请更换其他浏览器重新扫描二维码");
    }

    private void TestPushMessageButton_Click(object sender, RoutedEventArgs e)
    {
        UtilsPush.PostMessage(PushPlusToken, "123456789@qq.com", "987654321@qq.com", "", 5, 30, UtilsPush.MSG_TYPE.MSG_TEST, out var result);
        UtilsCom.Log(result);
        System.Windows.MessageBox.Show(result.ToString(), "", MessageBoxButton.OK);
    }

    private void DeleteBepButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is System.Windows.Controls.Button && System.Windows.MessageBox.Show(this, "确定卸载HsMod？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
        {
            try
            {
                string path = ApplicationPathManager.GetInstallSoftWarePath("Hearthstone");
                File.Delete(Path.Combine(path, "doorstop_config.ini"));
                File.Delete(Path.Combine(path, "winhttp.dll"));
                UtilsCopy.DeleteDirectory(Path.Combine(path, "BepInEx"));
                UtilsCopy.DeleteDirectory(Path.Combine(path, "unstripped_corlib"));
                UtilsCom.Log("HsMod卸载成功，如需继续使用，请点击“安装HsMod”");
            }
            catch
            {
                UtilsCom.Log("HsMod卸载失败，请检查权限");
            }
        }
    }

    private void CopyBepButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is System.Windows.Controls.Button)
        {
            //new Thread((ParameterizedThreadStart)UtilsCopy.CopyMercFileToHearthPath).Start(HearthstonePath);
            UtilsCom.Log("HsMod安装成功");
        }
    }

    /// <summary> 备份文件按钮 </summary>
    private void PatchStoneButton0_Click(object sender, RoutedEventArgs e)
    {
        if (UtilsPatch.PatchHearthStone(HearthstonePath, 0))
        {
            UtilsCom.Log("备份Assembly-CSharp.dll和libacsdk_x86.dll成功");
        }
        else
        {
            UtilsCom.Log("备份Assembly-CSharp.dll和libacsdk_x86.dll失败，请先关闭炉石客户端");
        }
    }

    /// <summary> 最小化按钮 </summary>
    private void PatchStoneButton1_Click(object sender, RoutedEventArgs e)
    {
        if (UtilsPatch.PatchHearthStone(HearthstonePath, 1))
        {
            UtilsCom.Log("打窗口最小化补丁成功");
        }
        else
        {
            UtilsCom.Log("打窗口最小化补丁失败，请先关闭炉石客户端");
        }
    }

    /// <summary> 反作弊按钮 </summary>
    private void PatchStoneButton2_Click(object sender, RoutedEventArgs e)
    {
        if (!UtilsPatch.PatchHearthStone(HearthstonePath, 2))
        {
            UtilsCom.Log("打反作弊补丁失败，请先关闭炉石客户端");
        }
        else
        {
            UtilsCom.Log("打反作弊补丁成功");
        }
    }

    /// <summary> 反作弊按钮 </summary>
    private void PatchStoneButton3_Click(object sender, RoutedEventArgs e)
    {
        if (!UtilsPatch.PatchHearthStone(HearthstonePath, 3))
        {
            UtilsCom.Log("打去除广告补丁失败，请先关闭炉石客户端");
        }
        else
        {
            UtilsCom.Log("打去除广告补丁成功");
        }
    }

    /// <summary> 去广告按钮 </summary>
    private void PatchStoneButton4_Click(object sender, RoutedEventArgs e)
    {
        if (!UtilsPatch.PatchHearthStone(HearthstonePath, 4))
        {
            UtilsCom.Log("打去除特有开门补丁失败，请先关闭炉石客户端");
        }
        else
        {
            UtilsCom.Log("打去除特有开门补丁成功");
        }
    }

    /// <summary> 去特效按钮 </summary>
    private void PatchStoneButton5_Click(object sender, RoutedEventArgs e)
    {
        if (!UtilsPatch.PatchHearthStone(HearthstonePath, 5))
        {
            UtilsCom.Log("打去除金卡特效补丁失败，请先关闭炉石客户端");
        }
        else
        {
            UtilsCom.Log("打去除金卡特效补丁成功");
        }
    }

    /// <summary> 还原文件按钮 </summary>
    private void PatchStoneButton6_Click(object sender, RoutedEventArgs e)
    {
        if (!UtilsPatch.PatchHearthStone(HearthstonePath, 6))
        {
            UtilsCom.Log("还原Assembly-CSharp.dll和libacsdk_x86.dll失败，请先关闭炉石客户端");
        }
        else
        {
            UtilsCom.Log("还原Assembly-CSharp.dll和libacsdk_x86.dll成功");
        }
    }

    /// <summary> 开始和停止按钮的状态切换 </summary>
    private void StartOrStopButton_Click(object sender, RoutedEventArgs e)
    {
        if (!bool_1)
        {
            StartButtonClick();
        }
        else
        {
            StopButtonClick();
        }
    }

    /// <summary> 按下开始按钮 </summary>
    private void StartButtonClick()
    {
        //PatchStoneButton6_Click
        if (bool_1)
        {
            return;
        }
        bool flag = false;
        if (!File.Exists(BattleNetPath))
        {
            flag = true;
            UtilsCom.Log("战网路径配置错误");
        }
        if (flag)
        {
            return;
        }
        if (!File.Exists(HearthbuddyPath))
        {
            flag = true;
            UtilsCom.Log("兄弟路径配置错误，请勿将本程序单独放置");
        }
        if (flag)
        {
            return;
        }
        if (!flag && AppDomain.CurrentDomain.BaseDirectory != Directory.GetParent(HearthbuddyPath).FullName + "\\")
        {
            flag = true;
            UtilsCom.Log("本程序未放置在兄弟根目录");
            UtilsCom.Log("当前目录：" + AppDomain.CurrentDomain.BaseDirectory);
            UtilsCom.Log("所配置兄弟目录：" + Directory.GetParent(HearthbuddyPath).FullName + "\\");
        }
        if (flag)
        {
            return;
        }
        if (UtilsCom.smethod_0(HearthbuddyPath))
        {
            flag = true;
            UtilsCom.Log("本程序禁止解压到中文目录下");
        }
        if (flag)
        {
            return;
        }
        if (AppDomain.CurrentDomain.BaseDirectory.Contains(HearthstonePath))
        {
            flag = true;
            UtilsCom.Log("本程序禁止解压到炉石目录或其子目录");
            UtilsCom.Log("请解压到桌面纯英文目录下");
        }
        if (!flag)
        {
            SaveConfigurationFile();
            bool_1 = true;
            ProgramStatusCheckerTimer.Interval = 1000;
            ProgramStatusCheckerTimer.Start();
            StartOrStopButton.Content = "停止运行";
            DisableAllForms();
            UtilsCom.Log("配置成功，兄弟中控开始运行...");
            if (NeedMultStone)
            {
                UtilsCom.Log("多开模式会一直在等待其他中控操作完成，请稍等...");
            }
        }
    }

    /// <summary> 按下停止按钮 </summary>
    private void StopButtonClick()
    {
        if (bool_1)
        {
            bool_1 = false;
            ProgramStatusCheckerTimer.Interval = 1000;
            ProgramStatusCheckerTimer.Stop();
            accountItemWhole_0.Running = false;
            StartOrStopButton.Content = "开始运行";
            EnableAllForms();
            UtilsCom.Log("兄弟中控已停止");
        }
    }

    /// <summary> 按下账号配置按钮 </summary>
    private void AccountConfigurationButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is System.Windows.Controls.Button button)
        {
            AccountItemWhole whole = button.Tag as AccountItemWhole;
            int index = AccountList.IndexOf(whole);
            AccountPopupList accountPopupList = new AccountPopupList(ref whole);
            accountPopupList.Left = base.Left + (base.Width - accountPopupList.Width) / 2.0;
            accountPopupList.Top = base.Top + (base.Height - accountPopupList.Height) / 2.0;
            accountPopupList.ShowDialog();
            AccountList[index] = whole;
        }
    }

    /// <summary> 按下删除账号按钮 </summary>
    private void AccountConfigurationDeleteClick(object sender, RoutedEventArgs e)
    {
        if (sender is System.Windows.Controls.Button button && System.Windows.MessageBox.Show(this, "确定删除此账号？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
        {
            AccountItemWhole accountItemWhole = button.Tag as AccountItemWhole;
            if (HearthBuddyExecutionScheduler.DeleteBattleNetAccount(AccountList, accountItemWhole.Email))
            {
                AccountList.Remove(accountItemWhole);
            }
        }
    }

    #endregion

    #region 按钮触发的方法的具体实现
    /// <summary> 禁用窗体所有功能 </summary>
    private void DisableAllForms()
    {
        SelectBattleNetFileButton.IsEnabled = false;
        SelectHearthstoneFileButton.IsEnabled = false;
        SelectHearthbuddyFileButton.IsEnabled = false;
        GetPushTokenButton.IsEnabled = false;
        TestPushMessageButton.IsEnabled = false;
        AccountListBox.IsEnabled = false;
        ConfigAccountButtonAdd.IsEnabled = false;
        BattleNetPathTextBox.IsEnabled = false;
        HearthstonePathTextBox.IsEnabled = false;
        HearthbuddyPathTextBox.IsEnabled = false;
        PushPlusTokenTextBox.IsEnabled = false;
        timeTextBlock1.IsEnabled = false;
        timeTextBlock2.IsEnabled = false;
        CheckIntervalTextBox.IsEnabled = false;
        RebootMaxCntTextBox.IsEnabled = false;
        SystemVersionComboBox.IsEnabled = false;
        WindowWidthTextBox.IsEnabled = false;
        WindowHeightTextBox.IsEnabled = false;
        PushNormalIntervalTextBox.IsEnabled = false;
        BNHSIntervalTextBox.IsEnabled = false;
        NeedCloseBattleCheckBox.IsEnabled = false;
        NeedMultStoneCheckBox.IsEnabled = false;
        NeedPushMessageCheckBox.IsEnabled = false;
        NeedPushNormalCheckBox.IsEnabled = false;
        PatchStoneButton0.IsEnabled = false;
        PatchStoneButton1.IsEnabled = false;
        PatchStoneButton2.IsEnabled = false;
        PatchStoneButton3.IsEnabled = false;
        PatchStoneButton4.IsEnabled = false;
        PatchStoneButton5.IsEnabled = false;
        PatchStoneButton6.IsEnabled = false;
        AddAccountButton.IsEnabled = false;
    }

    /// <summary> 启用窗体所有功能 </summary>
    private void EnableAllForms()
    {
        BNHSIntervalTextBox.IsEnabled = true;
        SelectBattleNetFileButton.IsEnabled = true;
        SelectHearthstoneFileButton.IsEnabled = true;
        GetPushTokenButton.IsEnabled = true;
        TestPushMessageButton.IsEnabled = true;
        AccountListBox.IsEnabled = true;
        ConfigAccountButtonAdd.IsEnabled = true;
        BattleNetPathTextBox.IsEnabled = true;
        HearthstonePathTextBox.IsEnabled = true;
        PushPlusTokenTextBox.IsEnabled = true;
        timeTextBlock1.IsEnabled = true;
        timeTextBlock2.IsEnabled = true;
        CheckIntervalTextBox.IsEnabled = true;
        RebootMaxCntTextBox.IsEnabled = true;
        SystemVersionComboBox.IsEnabled = true;
        WindowWidthTextBox.IsEnabled = true;
        WindowHeightTextBox.IsEnabled = true;
        PushNormalIntervalTextBox.IsEnabled = true;
        NeedCloseBattleCheckBox.IsEnabled = true;
        NeedMultStoneCheckBox.IsEnabled = true;
        NeedPushMessageCheckBox.IsEnabled = true;
        NeedPushNormalCheckBox.IsEnabled = true;
        PatchStoneButton0.IsEnabled = true;
        PatchStoneButton1.IsEnabled = true;
        PatchStoneButton2.IsEnabled = true;
        PatchStoneButton3.IsEnabled = true;
        PatchStoneButton4.IsEnabled = true;
        PatchStoneButton5.IsEnabled = true;
        PatchStoneButton6.IsEnabled = true;
        AddAccountButton.IsEnabled = true;
    }

    [SpecialName]
    /// <summary> 重启计数器 </summary>
    private void TodayRebootCnt(string string_0)
    {
        TodayRebootCntLabel.Content = string_0;
    }

    /// <summary> 日志显示滚动 </summary>
    private void LogTextBoxScroller(object sender, EventArgs e)
    {
        if (UtilsCom.logQueue.Count > 0)
        {
            string text = (string)UtilsCom.logQueue.Dequeue();
            if (int_3++ > 500)
            {
                int_3 = 0;
                LogTextBox.Clear();
            }
            LogTextBox.AppendText(DateTime.Now.ToLongTimeString() + "：" + ((!NeedMultStone) ? "[单]" : "[多]") + text + "\n");
            if (int_3 > 60)
            {
                LogTextBox.ScrollToEnd();
            }
        }
    }

    /// <summary> 软件更新检查 </summary>
    private void ProgramUpdate()
    {
        try
        {
            string newVersion = "";
            string date = "";
            string downloadurl = "";
            string downloadname = "";
            string describe = "";
            string must = "";
            string auth = "";
            string enable = "";
            string reason = "";
            int num = SoftUpdate.bNeedUpdate("1.0.2", ref newVersion, ref date, ref downloadurl, ref downloadname, ref describe, ref must, ref auth, ref enable, ref reason);
            if (-1 == num)
            {
                UtilsCom.Log(describe);
                UtilsCom.Log($"{int_2 / 1000}秒后本程序将关闭！");
                UtilsCom.Delay(int_2);
                SaveConfigurationFileAndExit();
            }
            if (enable != "1")
            {
                UtilsCom.Log(reason);
                UtilsCom.Log($"{int_2 / 1000}秒后本程序将关闭！");
                UtilsCom.Delay(int_2);
                SaveConfigurationFileAndExit();
            }
            //if (num == 0)
            //{
            //	if (System.Windows.MessageBox.Show("有新版本：V" + newVersion + "\n更新日期：" + date + "\n\n更新内容如下：\n" + describe + "\n\n是否立即更新？", "更新", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //	{
            //		new Thread((ParameterizedThreadStart)SoftUpdate.DownloadFile).Start(downloadurl + "@" + downloadname);
            //		while (true)
            //		{
            //			if (!SoftUpdate.bDownloadFileOk())
            //			{
            //				UtilsCom.Delay(1000);
            //				continue;
            //			}
            //			ProgramOperationManagement.StopBattleNetProcess(accountItemWhole_0, NeedMultStone, NeedCloseBattle, bool_0);
            //			Process process = new Process();
            //			process.StartInfo.FileName = downloadname;
            //			process.Start();
            //			method_7();
            //		}
            //	}
            //	if (must == "1")
            //	{
            //		UtilsCom.Log("新版本V" + newVersion + "必须更新，否则无法运行！");
            //		UtilsCom.Log($"{int_2 / 1000}秒后本程序将关闭！");
            //		UtilsCom.Delay(int_2);
            //		method_7();
            //	}
            //}
            else
            {
                UtilsCom.Log("当前版本已是最新，无需更新！");
                if (!(auth == "1"))
                {
                    UtilsCom.Log("当前用户认证成功，正常使用！");
                }
                else if (Security.bAuthOK() != 0)
                {
                    UtilsCom.Log($"{int_2 / 1000}秒后本程序将关闭！");
                    UtilsCom.Delay(int_2);
                    SaveConfigurationFileAndExit();
                }
            }
        }
        catch (Exception)
        {
            UtilsCom.Log("启动错误，请检查网络是否正常！");
            UtilsCom.Log($"{int_2 / 1000}秒后本程序将关闭！");
            UtilsCom.Delay(int_2);
            SaveConfigurationFileAndExit();
        }
    }

    /// <summary> 程序运行状态重置 </summary>
    private void ProgramControlReset(object sender, EventArgs e)
    {
        TimeSpan timeOfDay = DateTime.Now.TimeOfDay;
        TimeSpan timeOfDay2 = DateTime.Parse("00:00:00").TimeOfDay;
        TimeSpan timeOfDay3 = DateTime.Parse("00:00:05").TimeOfDay;
        if (timeOfDay > timeOfDay2 && timeOfDay < timeOfDay3)
        {
            UtilsCom.Log("当前时间0点0分，重启次数重置为0");
            int_0 = -1;
            ProgramControlResetTimer.Stop();
            UtilsCom.Delay(6000);
            ProgramControlResetTimer.Interval = 1000;
            ProgramControlResetTimer.Start();
        }
        if (int_0 == -1)
        {
            int_0 = 0;
            TodayRebootCnt(int_0 + "/");
            if (!bool_1)
            {
                if (NeedPushMessage)
                {
                    UtilsPush.PostMessage(PushPlusToken, "", accountItemWhole_0.Email, "", int_0, RebootCntMax, UtilsPush.MSG_TYPE.MSG_START, out var result);
                    UtilsCom.Log(result);
                }
                UtilsCom.Log("一日之计在于晨，兄弟中控自动开始运行");
                StartButtonClick();
            }
        }
        if (bool_1)
        {
            LogTextBox.ScrollToEnd();
        }
    }

    /// <summary> 程序运行状态检查器 </summary>
    private void ProgramStatusChecker(object sender, EventArgs e)
    {
        if (!bool_2)
        {
            return;
        }
        bool_2 = false;
        ProgramStatusCheckerTimer.Interval = 10000;
        try
        {
            if (!accountItemWhole_0.Running)
            {
                if (HearthBuddyExecutionScheduler.HearthBuddyProgramExecuter(bool_1, ref bool_0, NeedPushMessage, PushPlusToken, int_0, RebootCntMax, AccountList, accountItemWhole_0, out accountItemWhole_0))
                {
                    UtilsCom.Log("未检测到目标账号挂机中，准备启动挂机流程...");
                    HearthBuddyStart(bool_4: true);
                }
                else
                {
                    UtilsCom.Log("当前时间没有账号需要挂机");
                }
                bool_2 = true;
            }
            else if (HearthBuddyExecutionScheduler.UserRunningTimeChecker(accountItemWhole_0.currItem))
            {
                if (accountItemWhole_0.currItem.Mode == 1 && accountItemWhole_0.currItem.MercRule == 7)
                {
                    UtilsXml utilsXml = new UtilsXml("Settings/Default/HearthHelper.xml");
                    int num = int.Parse(utilsXml.Read("BattleNetAccount", "HASH" + accountItemWhole_0.Email.GetHashCode(), "Awake"));
                    bool flag = bool.Parse(utilsXml.Read("BattleNetAccount", "HASH" + accountItemWhole_0.Email.GetHashCode(), "AwakeStatus"));
                    int num2 = (int)DateTime.Now.Subtract(Convert.ToDateTime("2010-1-1 00:00:00")).TotalSeconds;
                    if (num2 < num)
                    {
                        if (!flag)
                        {
                            UtilsCom.Log("佣兵正在种菜，" + $"{(num - num2) / 60}分{(num - num2) % 60}秒后检测是否种完");
                        }
                        else
                        {
                            UtilsCom.Log("佣兵种菜完成，" + $"{(num - num2) / 60}分{(num - num2) % 60}秒后准备佣兵收菜");
                        }
                        bool_2 = true;
                    }
                    else
                    {
                        UtilsCom.Log("佣兵大白菜已成熟，开始启动炉石准备收割");
                        HearthBuddyStart(bool_4: true);
                        bool_2 = true;
                    }
                    return;
                }
                bool flag2 = false;
                DateTime dateTime = default(DateTime);
                if (!ProgramOperationManagement.HearthStoneProcessStatusChecker(accountItemWhole_0.StonePid))
                {
                    UtilsCom.Log($"炉石进程[{accountItemWhole_0.StonePid}]掉线，准备使用重启大法...");
                }
                else
                {
                    if (accountItemWhole_0.currItem.Mode != 0)
                    {
                        if (accountItemWhole_0.currItem.Mode == 1)
                        {
                            dateTime = Logger.GetMercStraLogsLastTime();
                        }
                    }
                    else
                    {
                        dateTime = Logger.GetRoutinesLogsLastTime();
                    }
                    int num3 = (int)DateTime.Now.Subtract((!(dateTime_0 > dateTime)) ? dateTime : dateTime_0).TotalSeconds;
                    if (num3 < CheckInterval * 60)
                    {
                        if (accountItemWhole_0.currItem.Mode != 0)
                        {
                            if (accountItemWhole_0.currItem.Mode == 1)
                            {
                                UtilsCom.Log($"佣兵插件{num3 / 60}分钟{num3 % 60}秒内运行正常");
                            }
                        }
                        else
                        {
                            UtilsCom.Log($"炉石兄弟{num3 / 60}分钟{num3 % 60}秒内运行正常");
                        }
                        if (NeedPushMessage && NeedPushNormal && DateTime.Now.ToBinary() / 1000L / 10000L - long_0 > PushNormalInterval * 3600)
                        {
                            long_0 = DateTime.Now.ToBinary() / 1000L / 10000L - 60L;
                            UtilsPush.PostMessage(PushPlusToken, "", accountItemWhole_0.Email, "", int_0, RebootCntMax, UtilsPush.MSG_TYPE.MSG_NORMAL, out var result);
                            UtilsCom.Log(result);
                        }
                        bool_2 = true;
                        return;
                    }
                    if (accountItemWhole_0.currItem.Mode == 0)
                    {
                        UtilsCom.Log($"炉石兄弟{CheckInterval}分钟内无任何操作");
                        UtilsCom.Log("天梯日志最新时间=" + dateTime.ToLongTimeString() + "，准备使用重启大法...");
                    }
                    else if (accountItemWhole_0.currItem.Mode == 1)
                    {
                        UtilsCom.Log($"佣兵插件{CheckInterval}分钟内无任何操作");
                        UtilsCom.Log("佣兵日志最新时间=" + dateTime.ToLongTimeString() + "，准备使用重启大法...");
                    }
                }
                if (flag2)
                {
                    return;
                }
                int_0++;
                TodayRebootCnt(int_0 + "/");
                if (int_0 > RebootCntMax)
                {
                    UtilsCom.Log($"今日重启次数已达最大值{RebootCntMax}，中控自动停止运行");
                    if (NeedPushMessage)
                    {
                        UtilsPush.PostMessage(PushPlusToken, "", accountItemWhole_0.Email, "", RebootCntMax, RebootCntMax, UtilsPush.MSG_TYPE.MSG_STOP, out var result2);
                        UtilsCom.Log(result2);
                    }
                    ProgramOperationManagement.StopBattleNetProcess(accountItemWhole_0, NeedMultStone, NeedCloseBattle, bool_0);
                    StopButtonClick();
                    bool_2 = true;
                }
                else
                {
                    if (NeedPushMessage)
                    {
                        UtilsPush.PostMessage(PushPlusToken, "", accountItemWhole_0.Email, "", int_0, RebootCntMax, UtilsPush.MSG_TYPE.MSG_REBOOT, out var result3);
                        UtilsCom.Log(result3);
                    }
                    HearthBuddyStart(bool_4: true);
                    bool_2 = true;
                }
            }
            else
            {
                if (accountItemWhole_0.currItem.Mode == 1 && accountItemWhole_0.currItem.MercRule == 7)
                {
                    UtilsCom.Log("佣兵大白菜未成熟，天降大雨，提前收割");
                    HearthBuddyStart(bool_4: false);
                }
                else
                {
                    UtilsCom.Log("准备停止账号" + accountItemWhole_0.EmailShow + "挂机(" + accountItemWhole_0.currItem.StartTimeHour.ToString("D2") + ":" + accountItemWhole_0.currItem.StartTimeMin.ToString("D2") + "-" + accountItemWhole_0.currItem.EndTimeHour.ToString("D2") + ":" + accountItemWhole_0.currItem.EndTimeMin.ToString("D2") + ")");
                    ProgramOperationManagement.StopBattleNetProcess(accountItemWhole_0, NeedMultStone, NeedCloseBattle, bool_0);
                }
                accountItemWhole_0.Running = false;
                bool_2 = true;
            }
        }
        catch
        {
            bool_2 = true;
        }
    }

    /// <summary> 炉石兄弟启动处理 </summary>
    private void HearthBuddyStart(bool bool_4)
    {
        try
        {
            using Mutex mutex = new Mutex(initiallyOwned: false, "HearthHelp");
            try
            {
                mutex.WaitOne();
                dateTime_0 = DateTime.Now;
                ProgramOperationManagement.StopBattleNetProcess(accountItemWhole_0, NeedMultStone, NeedCloseBattle, bool_0);
                if (!bool_1)
                {
                    UtilsCom.Log("用户主动停止运行，终止后续启动");
                    return;
                }
                UtilsCom.Delay(5000);
                HearthBuddyExecutionScheduler.EditBattleNetAccount(AccountList, accountItemWhole_0.Email);
                UtilsCom.Delay(500);
                bool_0 = false;
                bool flag = ProgramOperationManagement.LaunchHearthStone(accountItemWhole_0, HearthstonePath, ref bool_1, NeedMultStone, NeedCloseBattle, bool_0, bool_4, BattleNetPath, BNHSInterval, WindowWidth, WindowHeight, EnableHsMod, HsModPort, EnableTimeGear, NoFightTime, PveFightTime, PvpFightTime);
                if (bool_1)
                {
                    if (flag && accountItemWhole_0.currItem.Mode == 0)
                    {
                        UtilsCom.Log($"{HSHBInterval}秒后启动炉石兄弟");
                        UtilsCom.Delay(1000 * HSHBInterval);
                        ProgramOperationManagement.LaunchHearthBuddy(accountItemWhole_0, ref bool_1, HearthbuddyPath, SystemVersion, WindowWidth, WindowHeight);
                        if (!bool_1)
                        {
                            UtilsCom.Log("用户主动停止运行，终止后续启动");
                            return;
                        }
                        UtilsCom.Log("炉石兄弟已启动，30秒后开始循环检测");
                        UtilsCom.Delay(30000);
                    }
                    if (flag && accountItemWhole_0.currItem.MercRule == 7 && accountItemWhole_0.currItem.Mode == 1)
                    {
                        UtilsXml utilsXml = new UtilsXml("Settings/Default/HearthHelper.xml");
                        utilsXml.Write(((int)DateTime.Now.AddMinutes(CheckInterval).Subtract(Convert.ToDateTime("2010-1-1 00:00:00")).TotalSeconds).ToString(), "BattleNetAccount", "HASH" + accountItemWhole_0.Email.GetHashCode(), "Awake");
                        utilsXml.Write(false.ToString(), "BattleNetAccount", "HASH" + accountItemWhole_0.Email.GetHashCode(), "AwakeStatus");
                    }
                    if (flag && bool_1)
                    {
                        accountItemWhole_0.Running = true;
                        StartOrStopButton.Content = "停止运行（当前挂机账号" + accountItemWhole_0.EmailShow + "）";
                        UtilsCom.Log("预计已全部启动成功，开始循环检测");
                    }
                }
                else
                {
                    UtilsCom.Log("用户主动停止运行，终止后续启动");
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
        catch (Exception ex)
        {
            UtilsCom.Log(ex.ToString());
        }
    }

    /// <summary> 字符处理 </summary>
    private void PushNormalIntervalTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    /// <summary> 时分秒 </summary>
    private void formatTimeToHMS(object sender, EventArgs e)
    {
        timeSpan_0 += TimeSpan.FromSeconds(1.0);
        timeTextBlock1.Text = string.Format("运行时间：{0}", timeSpan_0.ToString("d'天 'h'小时 'm'分 's'秒'"));
    }

    /// <summary> 日期 </summary>
    private void formatDateToYMD(object sender, EventArgs e)
    {
        timeTextBlock2.Text = string.Format("{0}", DateTime.Now.ToString("g"));
    }

    /// <summary> 保存运行配置文件信息 </summary>
    private void SaveConfigurationFile()
    {
        XmlConfigureManager.CondfigWriter(AccountList, BattleNetPath, HearthstonePath, HearthbuddyPath, PushPlusToken, BNHSInterval, HSHBInterval, CheckInterval, RebootCntMax, PushNormalInterval, SystemVersion, WindowWidth, WindowHeight, NeedCloseBattle, NeedMultStone, NeedPushMessage, NeedPushNormal, EnableHsMod, HsModPort, EnableTimeGear, NoFightTime, PveFightTime, PvpFightTime);
    }

    /// <summary> 保存运行配置文件信息并退出程序 </summary>
    private void SaveConfigurationFileAndExit()
    {
        SaveConfigurationFile();
        Environment.Exit(0);
    }

    /// <summary> 主界面处理 </summary>
    private void ProgramInterfaceEventHandler(object sender, RoutedEventArgs e)
    {
        base.Title = "中控V1.0.2--[2024-11-10]--星辰大海";
        UtilsCom.Log("请先完整配置一次炉石兄弟运行成功后，再使用中控");
        UtilsCom.Log("同时必须先在中控里面配置账号的模式、卡组、策略");
        DisableAllForms();
        StartOrStopButton.IsEnabled = false;
        string path = Directory.GetCurrentDirectory() + "\\";
        string text = "Update_HearthScript";
        string[] files = Directory.GetFiles(path, text + "*");
        foreach (string text2 in files)
        {
            try
            {
                UtilsCom.Log("删除升级包[" + text2.Substring(text2.LastIndexOf(text)) + "]");
                File.Delete(text2);
            }
            catch
            {
            }
        }
        ObservableCollection<AccountItemWhole> observableCollection_ = new ObservableCollection<AccountItemWhole>();
        string string_ = "";
        string string_2 = "";
        string string_3 = "";
        string string_4 = "";
        int bNHSInterval = 0;
        int hSHBInterval = 0;
        int checkInterval = 0;
        int rebootCntMax = 0;
        int int_ = 0;
        int int_2 = 0;
        int int_3 = 144;
        int int_4 = 108;
        bool needCloseBattle = false;
        bool needMultStone = false;
        bool needPushMessage = false;
        bool needPushNormal = false;
        bool bool_ = false;
        int int_5 = 58744;
        bool bool_2 = false;
        int int_6 = 2;
        int int_7 = 4;
        int int_8 = 1;
        XmlConfigureManager.CondfigReader(ref observableCollection_, ref string_, ref string_2, ref string_3, ref string_4, ref bNHSInterval, ref hSHBInterval, ref checkInterval, ref rebootCntMax, ref int_, ref int_2, ref int_3, ref int_4, ref needCloseBattle, ref needMultStone, ref needPushMessage, ref needPushNormal, ref bool_, ref int_5, ref bool_2, ref int_6, ref int_7, ref int_8);
        AccountList = observableCollection_;
        BattleNetPath = string_;
        HearthstonePath = string_2;
        HearthbuddyPath = string_3;
        PushPlusToken = string_4;
        BNHSInterval = bNHSInterval;
        HSHBInterval = hSHBInterval;
        CheckInterval = checkInterval;
        RebootCntMax = rebootCntMax;
        PushNormalInterval = int_;
        SystemVersion = int_2;
        WindowWidth = int_3;
        WindowHeight = int_4;
        NeedCloseBattle = needCloseBattle;
        NeedMultStone = needMultStone;
        NeedPushMessage = needPushMessage;
        NeedPushNormal = needPushNormal;
        EnableHsMod = bool_;
        HsModPort = int_5;
        EnableTimeGear = bool_2;
        NoFightTime = int_6;
        PveFightTime = int_7;
        PvpFightTime = int_8;
        AccountListBox.ItemsSource = AccountList;
        Logger.InitializationDirectory();
        LogTextBoxScrollerTimer.Interval = 100;
        LogTextBoxScrollerTimer.Tick += LogTextBoxScroller;
        LogTextBoxScrollerTimer.Start();

        // 设置定时器的间隔时间为1000ms (1秒)
        ProgramControlResetTimer.Interval = 1000;
        // 绑定定时器触发事件
        ProgramControlResetTimer.Tick += ProgramControlReset;
        // 启动定时器
        ProgramControlResetTimer.Start();

        ProgramStatusCheckerTimer.Interval = 1000;
        ProgramStatusCheckerTimer.Tick += ProgramStatusChecker;

        EnableAllForms();
        StartOrStopButton.IsEnabled = true;
        dispatcherTimer_0 = new DispatcherTimer();
        dispatcherTimer_0.Interval = TimeSpan.FromSeconds(1.0);
        dispatcherTimer_0.Tick += formatTimeToHMS;
        dispatcherTimer_0.Start();
        dispatcherTimer_1 = new DispatcherTimer();
        dispatcherTimer_1.Interval = TimeSpan.FromSeconds(1.0);
        dispatcherTimer_1.Tick += formatDateToYMD;
        dispatcherTimer_1.Start();

    }

    /// <summary> 文本框处理 </summary>
    private void FrameTextEventHandler(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = false;
        e.Handled = true;
    }

    /// <summary> 保存运行配置处理 </summary>
    private void SaveConfigurationFileEventHandler(object sender, EventArgs e)
    {
        SaveConfigurationFile();
    }

    #endregion

    public MainWindow()
    {
        base.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        InitializeComponent();
    }

    static MainWindow()
    {
        int_3 = 0;
        bool_2 = true;
    }

}
