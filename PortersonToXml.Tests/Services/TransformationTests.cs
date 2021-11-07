using System;
using System.Collections;
using PortersonToXml.Services;
using PortersonToXml.Tests.Utilities;
using NUnit.Framework;

namespace PortersonToXml.Tests.Services
{
    public class TransformationTests
    {
        [Test]
        public void Transform_null_catalog()
        {
            Assert.Throws<ArgumentNullException>(() => DataProcessor.Transform(null!));
        }

        [Test]
        public void Transform_empty_catalog()
        {
            AssertOutput(new(), DataProcessor.Transform(new()));
        }

        [Test]
        public void Transform_catalog_with_one_empty_book()
        {
            AssertOutput(new()
            {
                Authors =
                {
                    new()
                    {
                        Books =
                        {
                            new()
                        }
                    }
                }
            }, DataProcessor.Transform(new()
            {
                Books =
                {
                    new()
                }
            }));
        }

        [Test]
        public void Transform_catalog_with_one_book()
        {
            AssertOutput(new()
            {
                Authors =
                {
                    new()
                    {
                        Name = "Gambardella, Matthew",
                        Books =
                        {
                            new()
                            {
                                Id = "bk101",
                                Title = "XML Developer's Guide",
                                Genre = "Computer",
                                Price = 4.95m,
                                PublishDate = new DateTime(2000, 10, 01),
                                Description = @"
                                    An in-depth look at creating applications
                                    with XML.
                                "
                            }
                        }
                    }
                }
            }, DataProcessor.Transform(new()
            {
                Books =
                {
                    new()
                    {
                        Id = "bk101",
                        Author = "Gambardella, Matthew",
                        Title = "XML Developer's Guide",
                        Genre = "Computer",
                        Price = 4.95m,
                        PublishDate = new DateTime(2000, 10, 01),
                        Description = @"
                            An in-depth look at creating applications
                            with XML.
                        "
                    }
                }
            }));
        }

        [Test]
        public void Transform_catalog_with_ordered_authors_and_books()
        {
            AssertOutput(new()
            {
                Authors =
                {
                    new()
                    {
                        Name = "A",
                        Books =
                        {
                            new()
                            {
                                Id = "1"
                            },
                            new()
                            {
                                Id = "4"
                            }
                        }
                    },
                    new()
                    {
                        Name = "B",
                        Books =
                        {
                            new()
                            {
                                Id = "2"
                            }
                        }
                    },
                    new()
                    {
                        Name = "C",
                        Books =
                        {
                            new()
                            {
                                Id = "3"
                            }
                        }
                    }
                }
            }, DataProcessor.Transform(new()
            {
                Books =
                {
                    new()
                    {
                        Id = "2",
                        Author = "B"
                    },
                    new()
                    {
                        Id = "4",
                        Author = "A"
                    },
                    new()
                    {
                        Id = "3",
                        Author = "C"
                    },
                    new()
                    {
                        Id = "1",
                        Author = "A"
                    }
                }
            }));
        }

        [Test]
        public void Transform_catalog_with_many_authors_with_many_books()
        {
            AssertOutput(new()
            {
                Authors =
                {
                    new()
                    {
                        Name = "Corets, Eva",
                        Books =
                        {
                            new()
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
                            new()
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
                    new()
                    {
                        Name = "Gambardella, Matthew",
                        Books =
                        {
                            new()
                            {
                                Id = "bk101",
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
                    }
                }
            }, DataProcessor.Transform(new()
            {
                Books =
                {
                    new()
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
                    new()
                    {
                        Id = "bk103",
                        Author = "Corets, Eva",
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
                    new()
                    {
                        Id = "bk104",
                        Author = "Corets, Eva",
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
            }));
        }

        private static void AssertOutput(Entities.Output.Catalog expected, Entities.Output.Catalog actual)
        {
            CollectionAssert.AreEqual(expected.Authors, actual.Authors, AuthorComparer);
        }

        private static readonly IComparer AuthorComparer = new TestOutputAuthorComparer();
    }
}
