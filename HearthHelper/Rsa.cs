using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace HearthHelper;

public static class Rsa
{
    public static string RsaSign(string str)
    {
        byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(str);
        byte[] rgbHash = new SHA256CryptoServiceProvider().ComputeHash(bytes);
        RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
        rSACryptoServiceProvider.FromXmlString(File.ReadAllText("privateKey.xml"));
        RSAPKCS1SignatureFormatter rSAPKCS1SignatureFormatter = new RSAPKCS1SignatureFormatter(rSACryptoServiceProvider);
        rSAPKCS1SignatureFormatter.SetHashAlgorithm("SHA256");
        return Convert.ToBase64String(rSAPKCS1SignatureFormatter.CreateSignature(rgbHash));
    }

    public static void AddUser()
    {
        File.Delete("Users.dat");
        XmlDocument xmlDocument = new XmlDocument();
        XmlDeclaration newChild = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
        xmlDocument.AppendChild(newChild);
        XmlElement xmlElement = xmlDocument.CreateElement("", "Users", "");
        xmlDocument.AppendChild(xmlElement);
        using (StreamReader streamReader = new StreamReader("Users.txt"))
        {
            string str;
            while ((str = streamReader.ReadLine()) != null)
            {
                XmlElement xmlElement2 = xmlDocument.CreateElement("", "Key", "");
                xmlElement2.InnerText = RsaSign(str);
                xmlElement.AppendChild(xmlElement2);
            }
        }
        File.WriteAllText("Users.dat", AesEncrypt(xmlDocument.OuterXml));
        File.Delete("Ability.dat");
        XDocument xDocument = XDocument.Load("Ability.xml");
        File.WriteAllText("Ability.dat", AesEncrypt(xDocument.ToString()));
    }

    public static bool RsaSignCheck(string str, string sign)
    {
        try
        {
            string xmlString = "<RSAKeyValue><Modulus>l9YgeWOlHPRczcPwccVAJQcyB6lFcNIPiQUR41ZpEV2AUePTboMisPOqQFfik91Vbi5ooFTEoFIi2yUQiIOcAEGyka4o0m4JsJg+ZWruBdTqZY+7fckKQb61hM9Cf1f88yM/4DB+u8iQFtg1fYZ45o5ldN62Kd9ku4pTxbLB4iOptXQxyzDyYrA59wKDFC4XMW8LKFqXtv3oW39UWdLU/nXYqezDlhki9W3RRYctF3knWNF4xRTAqQEz6AzUJR4icrp2WGl0t3cHFiK8kEMmCenRr7LfkuMoUpiudOVLzrPdYhkroaZ/Zje+uDRB7QgFlaIYhAfKrzqLfTasiczdQQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(str);
            byte[] rgbHash = new SHA256CryptoServiceProvider().ComputeHash(bytes);
            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
            rSACryptoServiceProvider.FromXmlString(xmlString);
            RSAPKCS1SignatureDeformatter rSAPKCS1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rSACryptoServiceProvider);
            rSAPKCS1SignatureDeformatter.SetHashAlgorithm("SHA256");
            byte[] rgbSignature = Convert.FromBase64String(sign);
            if (rSAPKCS1SignatureDeformatter.VerifySignature(rgbHash, rgbSignature))
            {
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public static string AesEncrypt(string data)
    {
        try
        {
            string s = "nM2SaGfvwlqiJ4I7D3mFQ7AFtDnzNCM/ptOi299ISaU=";
            string s2 = "yp2+XGZ9J4K4dnyfgF4kGg==";
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            using Aes aes = Aes.Create();
            aes.Key = Convert.FromBase64String(s);
            aes.IV = Convert.FromBase64String(s2);
            ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(memoryStream.ToArray());
        }
        catch
        {
            return "";
        }
    }

    public static string AesDecrypt(string string_0)
    {
        try
        {
            string s = "nM2SaGfvwlqiJ4I7D3mFQ7AFtDnzNCM/ptOi299ISaU=";
            string s2 = "yp2+XGZ9J4K4dnyfgF4kGg==";
            byte[] buffer = Convert.FromBase64String(string_0);
            using Aes aes = Aes.Create();
            aes.Key = Convert.FromBase64String(s);
            aes.IV = Convert.FromBase64String(s2);
            ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream stream = new MemoryStream(buffer);
            using CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            using StreamReader streamReader = new StreamReader(stream2);
            return streamReader.ReadToEnd();
        }
        catch
        {
            return "";
        }
    }
}
