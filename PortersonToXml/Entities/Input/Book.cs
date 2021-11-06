using System;

namespace PortersonToXml.Entities.Input
{
    public class Book
    {
        public string Id { get; set; } = string.Empty; // ATTR
        public string Author { get; set; } = string.Empty; // ELEMENT
        public string Title { get; set; } = string.Empty; // ELEMENT
        public string Genre { get; set; } = string.Empty; // ELEMENT
        public decimal Price { get; set; } // ELEMENT
        public DateTime PublishDate { get; set; } // ELEMENT, NO TIME
        public string Description { get; set; } = string.Empty; // ELEMENT
    }
}
