using System;
using PortersonToXml.Services;
using PortersonToXml.Entities.Input;
using NUnit.Framework;
using System.Collections;

namespace PortersonToXml.Tests.Services
{
    public class DeserializationTests
    {
        [Test]
        public void Deserialize_null_string()
        {
            // I would generally not test for NULL,
            // and rather rely on the type system to catch errors at compile-time,
            // although C# is far from being perfect on that matter.
            Assert.Throws<ArgumentNullException>(() => DataProcessor.Deserialize(null!));
        }

        [Test]
        public void Deserialize_empty_string()
        {
            Assert.Throws<InvalidOperationException>(() => DataProcessor.Deserialize(string.Empty));
        }

        [Test]
        public void Deserialize_json_string()
        {
            Assert.Throws<InvalidOperationException>(() => DataProcessor.Deserialize(@"{ ""Books"": [] }"));
        }

        [Test]
        public void Deserialize_XML_string_without_root()
        {
            Assert.Throws<InvalidOperationException>(() => DataProcessor.Deserialize(@"<?xml version=""1.0""?>"));
        }

        [Test]
        public void Deserialize_XML_string_with_wrong_root()
        {
            Assert.Throws<InvalidOperationException>(() => DataProcessor.Deserialize(@"<?xml version=""1.0""?> <Potato/>"));
        }

        [Test]
        public void Deserialize_PascalCase_XML_string()
        {
            Assert.Throws<InvalidOperationException>(() => DataProcessor.Deserialize(@"<?xml version=""1.0""?> <Catalog/>"));
        }

        [Test]
        public void Deserialize_empty_catalog()
        {
            AssertCatalog(new Catalog(), DataProcessor.Deserialize(@"<?xml version=""1.0""?> <catalog/>"));
        }

        [Test]
        public void Deserialize_PascalCase_catalog_books()
        {
            // Actually, the serializer ignores extra XML elements it doesn't recognize,
            // which is fine... I guess...
            // Assert.Throws<InvalidOperationException>(() =>
            AssertCatalog(new Catalog(), DataProcessor.Deserialize(@"<?xml version=""1.0""?>
            <catalog>
                <Books>
                    <Book/>
                </Books>
            </catalog>"));
        }

        [Test]
        public void Deserialize_catalog_with_one_empty_book()
        {
            AssertCatalog(new Catalog
            {
                Books =
                {
                    new Book()
                }
            }, DataProcessor.Deserialize(@"<?xml version=""1.0""?>
            <catalog>
                <book/>
            </catalog>"));
        }

        [Test]
        public void Deserialize_catalog_with_one_book()
        {
            AssertCatalog(new Catalog
            {
                Books =
                {
                    new Book
                    {
                        Id = "bk101",
                        Author = "Gambardella, Matthew",
                        Title = "XML Developer's Guide",
                        Genre = "Computer",
                        Price = 44.95m,
                        PublishDate = new DateTime(2000, 10, 01),
                        Description = @"
                          An in-depth look at creating applications
                          with XML.
                        "
                    }
                }
            }, DataProcessor.Deserialize(@"<?xml version=""1.0""?>
            <catalog>
                <book id=""bk101"">
                    <author>Gambardella, Matthew</author>
                    <title>XML Developer's Guide</title>
                    <genre>Computer</genre>
                    <price>44.95</price>
                    <publish_date>2000-10-01</publish_date>
                    <description>
                        An in-depth look at creating applications
                        with XML.
                    </description>
                </book>
            </catalog>"));
        }
        
        [Test]
        public void Deserialize_catalog_with_many_books()
        {
            AssertCatalog(new Catalog
            {
                Books =
                {
                    new Book
                    {
                        Id = "bk101",
                        Author = "Gambardella, Matthew",
                        Title = "XML Developer's Guide",
                        Genre = "Computer",
                        Price = 44.95m,
                        PublishDate = new DateTime(2000, 10, 01),
                        Description = @"
                            An in-depth look at creating applications
                            with XML.
                        "
                    },
                    new Book
                    {
                        Id = "bk102",
                        Author = "Ralls, Kim",
                        Title = "Midnight Rain",
                        Genre = "Fantasy",
                        Price = 5.95m,
                        PublishDate = new DateTime(2000, 12, 16),
                        Description = @"
                            A former architect battles corporate zombies,
                            an evil sorceress, and her own childhood to become queen
                            of the world.
                        "
                    }
                }
            }, DataProcessor.Deserialize(@"<?xml version=""1.0""?>
            <catalog>
                <book id=""bk101"">
                    <author>Gambardella, Matthew</author>
                    <title>XML Developer's Guide</title>
                    <genre>Computer</genre>
                    <price>44.95</price>
                    <publish_date>2000-10-01</publish_date>
                    <description>
                        An in-depth look at creating applications
                        with XML.
                    </description>
                </book>
                <book id=""bk102"">
                    <author>Ralls, Kim</author>
                    <title>Midnight Rain</title>
                    <genre>Fantasy</genre>
                    <price>5.95</price>
                    <publish_date>2000-12-16</publish_date>
                    <description>
                        A former architect battles corporate zombies,
                        an evil sorceress, and her own childhood to become queen
                        of the world.
                    </description>
                </book>
            </catalog>"));
        }

        private static void AssertCatalog(Catalog expected, Catalog actual)
        {
            CollectionAssert.AreEqual(expected.Books, actual.Books, BookComparer);
        }

        private static readonly IComparer BookComparer = new TestBookComparer();
    }
}
