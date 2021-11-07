using System.Collections.Generic;
using System.Xml.Serialization;

namespace PortersonToXmlAndJson.Entities.Output
{
    [XmlType("author")]
    public class Author
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlArray("books")]
        public List<Book> Books { get; init; } = new();
    }
}
