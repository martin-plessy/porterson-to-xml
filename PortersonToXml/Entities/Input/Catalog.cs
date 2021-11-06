using System.Collections.Generic;
using System.Xml.Serialization;

namespace PortersonToXml.Entities.Input
{
    [XmlType("catalog")]
    public class Catalog
    {
        [XmlElement("book")]
        public List<Book> Books { get; } = new();
    }
}
