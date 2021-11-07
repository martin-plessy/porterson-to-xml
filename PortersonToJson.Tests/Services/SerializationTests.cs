using System;
using System.Text.RegularExpressions;
using PortersonToJson.Services;
using NUnit.Framework;
using PortersonToJson.Entities.Output;

namespace PortersonToJson.Tests.Services
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
            AssertXml(@"{ ""catalog"": { ""authors"": [] } }", DataProcessor.Serialize(new Catalog()));
        }

        [Test]
        public void Serialize_catalog_with_one_empty_author()
        {
            AssertXml(@"{ ""catalog"": {
                ""authors"": [
                    { ""author"": {
                        ""name"": """",
                        ""books"": []
                    } }
                ]
            } }", DataProcessor.Serialize(new Catalog
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
            AssertXml(@"{ ""catalog"": {
                ""authors"": [
                    { ""author"": {
                        ""name"": ""Corets, Eva"",
                        ""books"": [
                            { ""book"": {
                                ""id"": """",
                                ""title"": """",
                                ""genre"": """",
                                ""price"": ""0"",
                                ""publish_date"": ""0001-01-01"",
                                ""description"": """"
                            } }
                        ]
                    } }
                ]
            } }", DataProcessor.Serialize(new Catalog
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
            AssertXml(@"{ ""catalog"": {
                ""authors"": [
                    { ""author"": {
                        ""name"": ""Corets, Eva"",
                        ""books"": [
                            { ""book"": {
                                ""id"": ""bk103"",
                                ""title"": ""Maeve Ascendant"",
                                ""genre"": ""Fantasy"",
                                ""price"": ""5.95"",
                                ""publish_date"": ""2000-11-17"",
                                ""description"": ""\n After the collapse of a nanotechnology \n society in England, the young survivors lay the \n foundation for a new society. \n""
                            } }
                        ]
                    } }
                ]
            } }", DataProcessor.Serialize(new Catalog
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
            AssertXml(@"{ ""catalog"": {
                ""authors"": [
                    { ""author"": {
                        ""name"": ""Corets, Eva"",
                        ""books"": [
                            { ""book"": {
                                ""id"": ""bk103"",
                                ""title"": ""Maeve Ascendant"",
                                ""genre"": ""Fantasy"",
                                ""price"": ""5.95"",
                                ""publish_date"": ""2000-11-17"",
                                ""description"": ""\n After the collapse of a nanotechnology \n society in England, the young survivors lay the \n foundation for a new society. \n""
                            } },
                            { ""book"": {
                                ""id"": ""bk104"",
                                ""title"": ""Oberon's Legacy"",
                                ""genre"": ""Fantasy"",
                                ""price"": ""5.95"",
                                ""publish_date"": ""2001-03-10"",
                                ""description"": ""\n In post-apocalypse England, the mysterious \n agent known only as Oberon helps to create a new life \n for the inhabitants of London. Sequel to Maeve \n Ascendant. \n""
                            } }
                        ]
                    } },
                    { ""author"": {
                        ""name"": ""Galos, Mike"",
                        ""books"": [
                            { ""book"": {
                                ""id"": ""bk112"",
                                ""title"": ""Visual Studio 7: A Comprehensive Guide"",
                                ""genre"": ""Computer"",
                                ""price"": ""49.95"",
                                ""publish_date"": ""2001-04-16"",
                                ""description"": ""\n Microsoft Visual Studio 7 is explored in depth, \n looking at how Visual Basic, Visual C++, C#, and ASP+ are \n integrated into a comprehensive development \n environment. \n""
                            } }
                        ]
                    } }
                ]
            } }", DataProcessor.Serialize(new Catalog
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
