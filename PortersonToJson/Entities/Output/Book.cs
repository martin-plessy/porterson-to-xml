using System;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace PortersonToJson.Entities.Output
{
    public class Book
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }

        [JsonIgnore]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description_normalizedEOL
        {
            get => Regex.Replace(Description, @"\r\n?", "\n");
            set => Description = value;
        }
    }
}
