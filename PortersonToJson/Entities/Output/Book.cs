using System;

namespace PortersonToJson.Entities.Output
{
    public class Book
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
