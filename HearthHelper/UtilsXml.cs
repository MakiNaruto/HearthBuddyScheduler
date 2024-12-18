using System.IO;
using System.Linq;
using System.Xml;

namespace HearthHelper;

public class UtilsXml
{
    private string string_0;

    public UtilsXml(string xmlPath)
    {
        string_0 = Path.GetFullPath(xmlPath);
    }

    public void Write(string value, params string[] nodes)
    {
        XmlDocument xmlDocument = new XmlDocument();
        if (!File.Exists(string_0))
        {
            xmlDocument.LoadXml("<XmlConfig />");
        }
        else
        {
            xmlDocument.Load(string_0);
        }
        XmlNode xmlNode_ = xmlDocument.ChildNodes[0];
        string text = string.Join("/", nodes);
        XmlNode xmlNode = xmlDocument.SelectSingleNode(text);
        if (xmlNode == null)
        {
            xmlNode = smethod_0(xmlDocument, xmlNode_, text);
        }
        xmlNode.InnerText = value;
        xmlDocument.Save(string_0);
    }

    public string Read(params string[] nodes)
    {
        XmlDocument xmlDocument = new XmlDocument();
        if (File.Exists(string_0))
        {
            xmlDocument.Load(string_0);
            string text = string.Join("/", nodes);
            return xmlDocument.SelectSingleNode("/XmlConfig/" + text)?.InnerText;
        }
        return null;
    }

    private static XmlNode smethod_0(XmlDocument xmlDocument_0, XmlNode xmlNode_0, string string_1)
    {
        string[] source = string_1.Trim('/').Split('/');
        string text = source.First();
        if (!string.IsNullOrEmpty(text))
        {
            XmlNode xmlNode = xmlNode_0.SelectSingleNode(text);
            if (xmlNode == null)
            {
                xmlNode = xmlNode_0.AppendChild(xmlDocument_0.CreateElement(text));
            }
            string string_2 = string.Join("/", source.Skip(1).ToArray());
            return smethod_0(xmlDocument_0, xmlNode, string_2);
        }
        return xmlNode_0;
    }
}
