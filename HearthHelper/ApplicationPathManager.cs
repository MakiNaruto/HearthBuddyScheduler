using System;
using System.IO;
using System.Windows.Forms;
using HearthHelper;
using Microsoft.Win32;

internal class ApplicationPathManager
{
    public static string GetInstallSoftWarePath(string string_0)
    {
        try
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\" + string_0);
            if (registryKey == null)
            {
                return null;
            }
            object value = registryKey.GetValue("InstallLocation");
            registryKey.Close();
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                return value.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return null;
    }

    public static string GetSpecificTypeLauncherChoosePath(string string_0)
    {
        Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
        {
            Filter = string_0,
            DereferenceLinks = false
        };
        if (openFileDialog.ShowDialog() == true)
        {
            return openFileDialog.FileName;
        }
        return string.Empty;
    }

    public static string GetHearthBuddyLauncherChoosePath()
    {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
            return folderBrowserDialog.SelectedPath;
        }
        return string.Empty;
    }

    public static string GetBattleNetLauncherPath()
    {
        string text = GetInstallSoftWarePath("Battle.net");
        if (!string.IsNullOrEmpty(text) && Directory.Exists(text) && File.Exists(Path.Combine(text, "Battle.net Launcher.exe")))
        {
            string text2 = Path.Combine(text, "Battle.net Launcher.exe");
            UtilsCom.Log("自动获取战网路径成功：");
            UtilsCom.Log(text2);
            return text2;
        }
        UtilsCom.Log("自动获取战网路径失败，请手动选择");
        return string.Empty;
    }

    public static string GetHearthStoneLauncherPath()
    {
        string text = GetInstallSoftWarePath("Hearthstone");
        if (!string.IsNullOrEmpty(text) && Directory.Exists(text) && File.Exists(Path.Combine(text, "Hearthstone.exe")))
        {
            UtilsCom.Log("自动获取炉石路径成功：");
            UtilsCom.Log(text);
            return text;
        }
        UtilsCom.Log("自动获取炉石路径失败，请手动选择");
        return string.Empty;
    }

    public static string GetHearthBuddyLauncherPath()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        if (!string.IsNullOrEmpty(currentDirectory) && Directory.Exists(currentDirectory) && File.Exists(Path.Combine(currentDirectory, "Hearthbuddy.exe")))
        {
            string text = Path.Combine(currentDirectory, "Hearthbuddy.exe");
            UtilsCom.Log("自动获取兄弟路径成功：");
            UtilsCom.Log(text);
            return text;
        }
        UtilsCom.Log("自动获取兄弟路径失败，请检测本程序是否在兄弟目录下");
        return string.Empty;
    }

    public static string GetBattleNetConfigPath()
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        if (!string.IsNullOrEmpty(folderPath) && Directory.Exists(folderPath) && Directory.Exists(Path.Combine(folderPath, "Battle.net")) && File.Exists(Path.Combine(folderPath, "Battle.net", "Battle.net.config")))
        {
            return Path.Combine(folderPath, "Battle.net", "Battle.net.config");
        }
        return string.Empty;
    }
}
