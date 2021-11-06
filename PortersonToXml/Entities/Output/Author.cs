using System.Collections.Generic;

namespace PortersonToXml.Entities.Output
{
    public class Author
    {
        public string Name { get; set; } = string.Empty; // ATTR
        public List<Book> Books { get; } = new(); // ELEMENT
    }
}
