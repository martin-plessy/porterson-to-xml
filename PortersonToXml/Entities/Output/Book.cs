using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace PortersonToXml.Entities.Output
{
    [XmlType("book")]
    public class Book
    {
        [XmlAttribute("id")]
        public string Id { get; set; } = string.Empty;

        [XmlAttribute("title")]
        public string Title { get; set; } = string.Empty;

        [XmlAttribute("genre")]
        public string Genre { get; set; } = string.Empty;

        [XmlAttribute("price")]
        public decimal Price { get; set; }

        [XmlAttribute("publish_date", DataType = "date")]
        public DateTime PublishDate { get; set; }

        [XmlIgnore]
        public string Description { get; set; } = string.Empty;

        [XmlAttribute("description")]
        public string Description_normalizedEOL
        {
            get => Regex.Replace(Description, @"\r\n?", "\n");
            set => Description = value;
        }
    }
}
