using System;

namespace PortersonToXml.Entities.Output
{
    public class Book
    {
        public string Id { get; set; } = string.Empty; // ATTR
        public string Title { get; set; } = string.Empty; // ATTR
        public string Genre { get; set; } = string.Empty; // ATTR
        public decimal Price { get; set; } // ATTR
        public DateTime PublishDate { get; set; } // ATTR, NO TIME
        public string Description { get; set; } = string.Empty; // ATTR
    }
}
