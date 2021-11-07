using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PortersonToXml.Services
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
                            .ToList()
                    })
                    .ToList()
            };
        }

        public static string Serialize(Entities.Output.Catalog catalog)
        {
            if (catalog == null)
            {
                throw new ArgumentNullException(nameof(catalog));
            }

            var serializer = new XmlSerializer(typeof(Entities.Output.Catalog));
            using var stringWriter = new Utf8StringWriter();
            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add("", ""); // https://stackoverflow.com/a/258974: Remove `xmlns:*` definitions on the root XML element.
            serializer.Serialize(stringWriter, catalog, namespaces);

            return stringWriter.ToString();
        }

        // https://stackoverflow.com/a/3862106: use UTF8 encoding instead of the default UTF16.
        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
    }
}
