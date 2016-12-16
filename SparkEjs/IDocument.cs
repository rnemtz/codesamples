using System.Xml;

namespace SparkEjs
{
    public interface IDocument
    {
        XmlDocument Read(string file);
    }
}