using System.Collections.Generic;
using System.Xml.Serialization;

namespace PortersonToXmlAndJson.Entities.Output
{
    [XmlType("catalog")]
    public class Catalog
    {
        [XmlArray("authors")]
        public List<Author> Authors { get; init; } = new();
    }
}
