using System.Xml;

namespace SparkEjs
{
    public class Document : IDocument
    {
        public XmlDocument Read(string file)
        {
            return new XmlDocument();
        }
    }
}