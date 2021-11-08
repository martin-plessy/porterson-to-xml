using System.Collections.Generic;

namespace PortersonToXml2.Entities.Output
{
    public class Author
    {
        public string Name { get; set; } = string.Empty;
        public List<Book> Books { get; init; } = new();
    }
}
