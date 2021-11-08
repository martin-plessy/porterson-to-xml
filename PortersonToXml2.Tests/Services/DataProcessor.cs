using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PortersonToXml2.Services
{
    public static class DataProcessor
    {
        public static Entities.Input.Catalog Deserialize(string xml)
        {
            using var reader = new StringReader(xml);

            return (Entities.Input.Catalog) new XmlSerializerBuilder()
                .Type<Entities.Input.Catalog>(catalog => catalog
                    .Name("catalog")
                    .ContentsMember(catalog => catalog.Books, "book"))

                .Type<Entities.Input.Book>(book => book
                    .Name("book")
                    .AttributeMember(book => book.Id, "id")
                    .ElementMember(book => book.Author, "author")
                    .ElementMember(book => book.Title, "title")
                    .ElementMember(book => book.Genre, "genre")
                    .ElementMember(book => book.Price, "price")
                    .ElementMember(book => book.PublishDate, "publish_date", "date")
                    .ElementMember(book => book.Description, "description"))

                .BuildFor<Entities.Input.Catalog>()
                .Deserialize(reader)!;
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

        public static string SerializeToXml1(Entities.Output.Catalog catalog) => SerializeToXmlN(1, catalog);
        public static string SerializeToXml2(Entities.Output.Catalog catalog) => SerializeToXmlN(2, catalog);
        public static string SerializeToXmlN(int N, Entities.Output.Catalog catalog)
        {
            if (catalog == null)
            {
                throw new ArgumentNullException(nameof(catalog));
            }

            using var stringWriter = new Utf8StringWriter();

            new XmlSerializerBuilder()
                .Type<Entities.Output.Catalog>(catalog => catalog
                    .Name("catalog")
                    .ElementMember(catalog => catalog.Authors, "authors"))

                .Type<Entities.Output.Author>(author =>
                {
                    author
                        .Name("author")
                        .ElementMember(author => author.Books, "books");

                    if (N == 1)
                    {
                        author.AttributeMember(author => author.Name, "name");
                    }
                    else
                    {
                        author.ElementMember(author => author.Name, "name");
                    }
                })

                .Type<Entities.Output.Book>(book =>
                {
                    book
                        .Name("book");

                    if (N == 1)
                    {
                        book
                            .AttributeMember(book => book.Id, "id")
                            .AttributeMember(book => book.Title, "title")
                            .AttributeMember(book => book.Genre, "genre")
                            .AttributeMember(book => book.Price, "price")
                            .AttributeMember(book => book.PublishDate, "publish_date", "date")
                            .AttributeMember(book => book.Description, "description");
                    }
                    else
                    {
                        book
                            .ElementMember(book => book.Id, "id")
                            .ElementMember(book => book.Title, "title")
                            .ElementMember(book => book.Genre, "genre")
                            .ElementMember(book => book.Price, "price")
                            .ElementMember(book => book.PublishDate, "publish_date", "date")
                            .ElementMember(book => book.Description, "description");
                    }

                })

                .BuildFor<Entities.Output.Catalog>()
                .Serialize(stringWriter, catalog, new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") }));

            return stringWriter.ToString();
        }

        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
    }
}
