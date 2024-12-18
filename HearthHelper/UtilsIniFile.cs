using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace HearthHelper;

public class UtilsIniFile
{
    private string Path;

    private string string_0 = Assembly.GetExecutingAssembly().GetName().Name;

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern long WritePrivateProfileString(string string_1, string string_2, string string_3, string string_4);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern int GetPrivateProfileString(string string_1, string string_2, string string_3, StringBuilder stringBuilder_0, int int_0, string string_4);

    public UtilsIniFile(string IniPath = null)
    {
        Path = new FileInfo(IniPath ?? (string_0 + ".ini")).FullName;
    }

    public string Read(string Key, string Section = null)
    {
        StringBuilder stringBuilder = new StringBuilder(255);
        GetPrivateProfileString(Section ?? string_0, Key, "", stringBuilder, 255, Path);
        return stringBuilder.ToString();
    }

    public void Write(string Key, string Value, string Section = null)
    {
        WritePrivateProfileString(Section ?? string_0, Key, Value, Path);
    }

    public void DeleteKey(string Key, string Section = null)
    {
        Write(Key, null, Section ?? string_0);
    }

    public void DeleteSection(string Section = null)
    {
        Write(null, null, Section ?? string_0);
    }

    public bool KeyExists(string Key, string Section = null)
    {
        return Read(Key, Section).Length > 0;
    }
}
