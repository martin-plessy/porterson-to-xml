using System;
using System.Xml.Serialization;

namespace PortersonToXmlAndJson.Entities.Input
{
    [XmlType("book")]
    public class Book
    {
        [XmlAttribute("id")]
        public string Id { get; set; } = string.Empty;

        [XmlElement("author")]
        public string Author { get; set; } = string.Empty;

        [XmlElement("title")]
        public string Title { get; set; } = string.Empty;

        [XmlElement("genre")]
        public string Genre { get; set; } = string.Empty;

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("publish_date")]
        public DateTime PublishDate { get; set; }

        [XmlElement("description")]
        public string Description { get; set; } = string.Empty;
    }
}
