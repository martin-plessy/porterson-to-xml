using System.Collections.Generic;

namespace PortersonToJson.Entities.Output
{
    public class Catalog
    {
        public List<Author> Authors { get; init; } = new();
    }
}
