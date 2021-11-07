using System.Collections.Generic;
using System.Xml.Serialization;

namespace PortersonToXml.Entities.Output
{
    [XmlType("catalog")]
    public class Catalog
    {
        [XmlArray("authors")]
        public List<Author> Authors { get; } = new();
    }
}
