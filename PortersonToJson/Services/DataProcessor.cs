using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace PortersonToJson.Services
{
    public static class DataProcessor
    {
        public static Entities.Input.Catalog Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(Entities.Input.Catalog));
            using var stringReader = new StringReader(xml);

            var value = serializer.Deserialize(stringReader);

            return (Entities.Input.Catalog) (value!);
        }

        public static Entities.Output.Catalog Transform(Entities.Input.Catalog input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return new Entities.Output.Catalog
            {
                Authors = input.Books
                    .GroupBy(book => book.Author)
                    .OrderBy(group => group.Key)
                    .Select(group => new Entities.Output.Author
                    {
                        Name = group.Key,
                        Books = group
                            .Select(book => new Entities.Output.Book
                            {
                                Id = book.Id,
                                Title = book.Title,
                                Genre = book.Genre,
                                Price = book.Price,
                                PublishDate = book.PublishDate,
                                Description = book.Description
                            })
                            .OrderBy(book => book.Id)
                            .ToList()
                    })
                    .ToList()
            };
        }

        public static string Serialize(Entities.Output.Catalog catalog)
        {
            return "";
        }
    }
}
