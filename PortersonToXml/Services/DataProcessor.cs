using System.IO;
using System.Xml.Serialization;

namespace PortersonToXml.Services
{
    public static class DataProcessor
    {
        public static Entities.Input.Catalog Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(Entities.Input.Catalog));
            using var stringReader = new StringReader(xml);

            var value = serializer.Deserialize(stringReader);

            return (Entities.Input.Catalog) (value!);
        }

        public static Entities.Output.Catalog Transform(Entities.Input.Catalog catalog)
        {
            return new();
        }

        public static string Serialize(Entities.Output.Catalog catalog)
        {
            return "";
        }
    }
}
