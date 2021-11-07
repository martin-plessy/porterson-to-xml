using System;
using System.Text.RegularExpressions;
using PortersonToXml.Services;
using NUnit.Framework;
using PortersonToXml.Entities.Output;

namespace PortersonToXml.Tests.Services
{
    public class SerializationTests
    {
        [Test]
        public void Serialize_null_catalog()
        {
            Assert.Throws<ArgumentNullException>(() => DataProcessor.Serialize(null!));
        }

        [Test]
        public void Serialize_empty_catalog()
        {
            // In case we want to omit attributes and elements when the values are empty strings or collection:
            // https://stackoverflow.com/questions/5818513/xml-serialization-hide-null-values/5818571.
            // It didn't seem of high importance here.
            AssertXml(@"<?xml version=""1.0"" encoding=""utf-8""?> <catalog> <authors/> </catalog>", DataProcessor.Serialize(new Catalog()));
        }

        [Test]
        public void Serialize_catalog_with_one_empty_author()
        {
            AssertXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
            <catalog>
                <authors>
                    <author name="""">
                        <books/>
                    </author>
                </authors>
            </catalog>", DataProcessor.Serialize(new Catalog
            {
                Authors =
                {
                    new Author()
                }
            }));
        }

        [Test]
        public void Serialize_catalog_with_one_author_with_one_empty_book()
        {
            AssertXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
            <catalog>
                <authors>
                    <author name=""Corets, Eva"">
                        <books>
                            <book id="""" title="""" genre="""" price=""0"" publish_date=""0001-01-01"" description="""" />
                        </books>
                    </author>
                </authors>
            </catalog>", DataProcessor.Serialize(new Catalog
            {
                Authors =
                {
                    new Author
                    {
                        Name = "Corets, Eva",
                        Books =
                        {
                            new Book()
                        }
                    }
                }
            }));
        }

        [Test]
        public void Serialize_catalog_with_one_author_with_one_book()
        {
            AssertXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
            <catalog>
                <authors>
                    <author name=""Corets, Eva"">
                        <books>
                            <book id=""bk103"" title=""Maeve Ascendant"" genre=""Fantasy"" price=""5.95"" publish_date=""2000-11-17"" description=""&#xA; After the collapse of a nanotechnology &#xA; society in England, the young survivors lay the &#xA; foundation for a new society. &#xA;"" />
                        </books>
                    </author>
                </authors>
            </catalog>", DataProcessor.Serialize(new Catalog
            {
                Authors =
                {
                    new Author
                    {
                        Name = "Corets, Eva",
                        Books =
                        {
                            new Book
                            {
                                Id = "bk103",
                                Title = "Maeve Ascendant",
                                Genre = "Fantasy",
                                Price = 5.95m,
                                PublishDate = new DateTime(2000, 11, 17),
                                Description = @"
                                    After the collapse of a nanotechnology
                                    society in England, the young survivors lay the
                                    foundation for a new society.
                                "
                            }
                        }
                    }
                }
            }));
        }

        [Test]
        public void Serialize_catalog_with_many_authors_with_many_books()
        {
            AssertXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
            <catalog>
                <authors>
                    <author name=""Corets, Eva"">
                        <books>
                            <book id=""bk103"" title=""Maeve Ascendant"" genre=""Fantasy"" price=""5.95"" publish_date=""2000-11-17"" description=""&#xA; After the collapse of a nanotechnology&#xA; society in England, the young survivors lay the &#xA; foundation for a new society. &#xA;"" />
                            <book id=""bk104"" title=""Oberon's Legacy"" genre=""Fantasy"" price=""5.95"" publish_date=""2001-03-10"" description=""&#xA; In post-apocalypse England, the mysterious &#xA; agent known only as Oberon helps to create a new life &#xA; for the inhabitants of London. Sequel to Maeve &#xA; Ascendant. &#xA;"" />
                        </books>
                    </author>
                    <author name=""Galos, Mike"">
                        <books>
                            <book id=""bk112"" title=""Visual Studio 7: A Comprehensive Guide"" genre=""Computer"" price=""49.95"" publish_date=""2001-04-16"" description=""&#xA; Microsoft Visual Studio 7 is explored in depth, &#xA; looking at how Visual Basic, Visual C++, C#, and ASP+ are &#xA; integrated into a comprehensive development &#xA; environment. &#xA;"" />
                        </books>
                    </author>
                </authors>
            </catalog>", DataProcessor.Serialize(new Catalog
            {
                Authors =
                {
                    new Author
                    {
                        Name = "Corets, Eva",
                        Books =
                        {
                            new Book
                            {
                                Id = "bk103",
                                Title = "Maeve Ascendant",
                                Genre = "Fantasy",
                                Price = 5.95m,
                                PublishDate = new DateTime(2000, 11, 17),
                                Description = @"
                                    After the collapse of a nanotechnology
                                    society in England, the young survivors lay the
                                    foundation for a new society.
                                "
                            },
                            new Book
                            {
                                Id = "bk104",
                                Title = "Oberon's Legacy",
                                Genre = "Fantasy",
                                Price = 5.95m,
                                PublishDate = new DateTime(2001, 03, 10),
                                Description = @"
                                    In post-apocalypse England, the mysterious
                                    agent known only as Oberon helps to create a new life
                                    for the inhabitants of London. Sequel to Maeve
                                    Ascendant.
                                "
                            }
                       }
                    },
                    new Author
                    {
                        Name = "Galos, Mike",
                        Books =
                        {
                            new Book
                            {
                                Id = "bk112",
                                Title = "Visual Studio 7: A Comprehensive Guide",
                                Genre = "Computer",
                                Price = 49.95m,
                                PublishDate = new DateTime(2001, 04, 16),
                                Description = @"
                                    Microsoft Visual Studio 7 is explored in depth,
                                    looking at how Visual Basic, Visual C++, C#, and ASP+ are
                                    integrated into a comprehensive development
                                    environment.
                                "
                            }
                        }
                    }
                }
            }));
        }

        private static void AssertXml(string expected, string actual)
        {
            Assert.AreEqual(
                Regex.Replace(expected, @"\s+", string.Empty),
                Regex.Replace(actual, @"\s+", string.Empty));
        }
    }
}
