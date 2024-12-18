using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace HearthHelper;

public static class UtilsCopy
{
    public static bool FileIsSame(string str1, string str2)
    {
        try
        {
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create();
            FileStream fileStream = new FileStream(str1, FileMode.Open);
            byte[] array = hashAlgorithm.ComputeHash(fileStream);
            fileStream.Close();
            FileStream fileStream2 = new FileStream(str2, FileMode.Open);
            byte[] array2 = hashAlgorithm.ComputeHash(fileStream2);
            fileStream2.Close();
            return BitConverter.ToString(array) == BitConverter.ToString(array2);
        }
        catch
        {
            return true;
        }
    }

    public static void CopyFileAndDir(string srcDir, string desDir)
    {
        if (!Directory.Exists(desDir))
        {
            Directory.CreateDirectory(desDir);
        }
        IEnumerable<string> enumerable = Directory.EnumerateFileSystemEntries(srcDir);
        if (enumerable == null || enumerable.Count() <= 0)
        {
            return;
        }
        foreach (string item in enumerable)
        {
            string text = Path.Combine(desDir, Path.GetFileName(item));
            if (File.Exists(item))
            {
                if (!File.Exists(text) || !FileIsSame(item, text))
                {
                    File.Copy(item, text, overwrite: true);
                }
            }
            else
            {
                CopyFileAndDir(item, text);
            }
        }
    }

    public static void CopyMercFileToHearthPath(string HearthstonePath)
    {
        try
        {
            CopyFileAndDir(Path.Combine(Directory.GetCurrentDirectory(), "HsMod"), HearthstonePath);
        }
        catch
        {
        }
    }

    public static void DeleteDirectory(string dir)
    {
        if (Directory.GetDirectories(dir).Length == 0 && Directory.GetFiles(dir).Length == 0)
        {
            Directory.Delete(dir);
            return;
        }
        string[] directories = Directory.GetDirectories(dir);
        for (int i = 0; i < directories.Length; i++)
        {
            DeleteDirectory(directories[i]);
        }
        directories = Directory.GetFiles(dir);
        for (int i = 0; i < directories.Length; i++)
        {
            File.Delete(directories[i]);
        }
        Directory.Delete(dir);
    }
}
