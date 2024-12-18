using System;
using System.IO;

internal class Logger
{
    public static string base_logs_directory = "";

    public static string default_settings_directory = "";

    public static string routines_logs_directory = "";

    public static string mercstra_logs_directory = "";

    public static void InitializationDirectory()
    {
        base_logs_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        if (!Directory.Exists(base_logs_directory))
        {
            Directory.CreateDirectory(base_logs_directory);
        }
        default_settings_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings/Default");
        if (!Directory.Exists(default_settings_directory))
        {
            Directory.CreateDirectory(default_settings_directory);
        }
        routines_logs_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Routines/DefaultRoutine/Silverfish/UltimateLogs");
        if (!Directory.Exists(routines_logs_directory))
        {
            Directory.CreateDirectory(routines_logs_directory);
        }
        mercstra_logs_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MercStraLogs");
        if (!Directory.Exists(mercstra_logs_directory))
        {
            Directory.CreateDirectory(mercstra_logs_directory);
        }
    }

    public static DateTime GetRoutinesLogsLastTime()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(routines_logs_directory);
        DateTime dateTime = default(DateTime);
        try
        {
            FileInfo[] files = directoryInfo.GetFiles("*.txt");
            foreach (FileInfo fileInfo in files)
            {
                if (DateTime.Compare(fileInfo.LastWriteTime, dateTime) > 0)
                {
                    dateTime = fileInfo.LastWriteTime;
                }
            }
        }
        catch
        {
        }
        return dateTime;
    }

    public static DateTime GetMercStraLogsLastTime()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(mercstra_logs_directory);
        DateTime dateTime = default(DateTime);
        try
        {
            FileInfo[] files = directoryInfo.GetFiles("*.txt");
            foreach (FileInfo fileInfo in files)
            {
                if (DateTime.Compare(fileInfo.LastWriteTime, dateTime) > 0)
                {
                    dateTime = fileInfo.LastWriteTime;
                }
            }
        }
        catch
        {
        }
        return dateTime;
    }

    //static Class2()
    //{
    //    string_0 = "";
    //    string_1 = "";
    //    string_2 = "";
    //    string_3 = "";
    //}
}
