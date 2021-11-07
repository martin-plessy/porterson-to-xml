using System.Text.RegularExpressions;
using PortersonToJson.Services;
using NUnit.Framework;

namespace PortersonToJson.Tests.Services
{
    public class IntegrationTests
    {
        [Test]
        public void Example()
        {
            var input = @"<?xml version=""1.0""?>
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
                <book id=""bk103"">
                    <author>Corets, Eva</author>
                    <title>Maeve Ascendant</title>
                    <genre>Fantasy</genre>
                    <price>5.95</price>
                    <publish_date>2000-11-17</publish_date>
                    <description>
                        After the collapse of a nanotechnology
                        society in England, the young survivors lay the
                        foundation for a new society.
                    </description>
                </book>
                <book id=""bk104"">
                    <author>Corets, Eva</author>
                    <title>Oberon's Legacy</title>
                    <genre>Fantasy</genre>
                    <price>5.95</price>
                    <publish_date>2001-03-10</publish_date>
                    <description>
                        In post-apocalypse England, the mysterious
                        agent known only as Oberon helps to create a new life
                        for the inhabitants of London. Sequel to Maeve
                        Ascendant.
                    </description>
                </book>
                <book id=""bk105"">
                    <author>Corets, Eva</author>
                    <title>The Sundered Grail</title>
                    <genre>Fantasy</genre>
                    <price>5.95</price>
                    <publish_date>2001-09-10</publish_date>
                    <description>
                        The two daughters of Maeve, half-sisters,
                        battle one another for control of England. Sequel to
                        Oberon's Legacy.
                    </description>
                </book>
                <book id=""bk106"">
                    <author>Randall, Cynthia</author>
                    <title>Lover Birds</title>
                    <genre>Romance</genre>
                    <price>4.95</price>
                    <publish_date>2000-09-02</publish_date>
                    <description>
                        When Carla meets Paul at an ornithology
                        conference, tempers fly as feathers get ruffled.
                    </description>
                </book>
                <book id=""bk107"">
                    <author>Thurman, Paula</author>
                    <title>Splish Splash</title>
                    <genre>Romance</genre>
                    <price>4.95</price>
                    <publish_date>2000-11-02</publish_date>
                    <description>
                        A deep sea diver finds true love twenty
                        thousand leagues beneath the sea.
                    </description>
                </book>
                <book id=""bk108"">
                    <author>Knorr, Stefan</author>
                    <title>Creepy Crawlies</title>
                    <genre>Horror</genre>
                    <price>4.95</price>
                    <publish_date>2000-12-06</publish_date>
                    <description>
                        An anthology of horror stories about roaches,
                        centipedes, scorpions  and other insects.
                    </description>
                </book>
                <book id=""bk109"">
                    <author>Kress, Peter</author>
                    <title>Paradox Lost</title>
                    <genre>Science Fiction</genre>
                    <price>6.95</price>
                    <publish_date>2000-11-02</publish_date>
                    <description>
                        After an inadvertant trip through a Heisenberg
                        Uncertainty Device, James Salway discovers the problems
                        of being quantum.
                    </description>
                </book>
                <book id=""bk110"">
                    <author>O'Brien, Tim</author>
                    <title>Microsoft .NET: The Programming Bible</title>
                    <genre>Computer</genre>
                    <price>36.95</price>
                    <publish_date>2000-12-09</publish_date>
                    <description>
                        Microsoft's .NET initiative is explored in
                        detail in this deep programmer's reference.
                    </description>
                </book>
                <book id=""bk111"">
                    <author>O'Brien, Tim</author>
                    <title>MSXML3: A Comprehensive Guide</title>
                    <genre>Computer</genre>
                    <price>36.95</price>
                    <publish_date>2000-12-01</publish_date>
                    <description>
                        The Microsoft MSXML3 parser is covered in
                        detail, with attention to XML DOM interfaces, XSLT processing,
                        SAX and more.
                    </description>
                </book>
                <book id=""bk112"">
                    <author>Galos, Mike</author>
                    <title>Visual Studio 7: A Comprehensive Guide</title>
                    <genre>Computer</genre>
                    <price>49.95</price>
                    <publish_date>2001-04-16</publish_date>
                    <description>
                        Microsoft Visual Studio 7 is explored in depth,
                        looking at how Visual Basic, Visual C++, C#, and ASP+ are
                        integrated into a comprehensive development
                        environment.
                    </description>
                </book>
            </catalog>";
            var expected = @"{ ""catalog"": {
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
                                ""description"": ""\n      After the collapse of a nanotechnology\n      society in England, the young survivors lay the\n      foundation for a new society.\n    ""
                            } },
                            { ""book"": {
                                ""id"": ""bk104"",
                                ""title"": ""Oberon's Legacy"",
                                ""genre"": ""Fantasy"",
                                ""price"": ""5.95"",
                                ""publish_date"": ""2001-03-10"",
                                ""description"": ""\n      In post-apocalypse England, the mysterious\n      agent known only as Oberon helps to create a new life\n      for the inhabitants of London. Sequel to Maeve\n      Ascendant.\n    ""
                            } },
                            { ""book"": {
                                ""id"": ""bk105"",
                                ""title"": ""The Sundered Grail"",
                                ""genre"": ""Fantasy"",
                                ""price"": ""5.95"",
                                ""publish_date"": ""2001-09-10"",
                                ""description"": ""\n      The two daughters of Maeve, half-sisters,\n      battle one another for control of England. Sequel to\n      Oberon's Legacy.\n    ""
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
                                ""description"": ""\n      Microsoft Visual Studio 7 is explored in depth,\n      looking at how Visual Basic, Visual C++, C#, and ASP+ are\n      integrated into a comprehensive development\n      environment.\n    ""
                            } }
                        ]
                    } },
                    { ""author"": {
                        ""name"": ""Gambardella, Matthew"",
                        ""books"": [
                            { ""book"": {
                                ""id"": ""bk101"",
                                ""title"": ""XML Developer's Guide"",
                                ""genre"": ""Computer"",
                                ""price"": ""44.95"",
                                ""publish_date"": ""2000-10-01"",
                                ""description"": ""\n      An in-depth look at creating applications\n      with XML.\n    ""
                            } }
                        ]
                    } },
                    { ""author"": {
                        ""name"": ""Knorr, Stefan"",
                        ""books"": [
                            { ""book"": {
                                ""id"": ""bk108"",
                                ""title"": ""Creepy Crawlies"",
                                ""genre"": ""Horror"",
                                ""price"": ""4.95"",
                                ""publish_date"": ""2000-12-06"",
                                ""description"": ""\n      An anthology of horror stories about roaches,\n      centipedes, scorpions  and other insects.\n    ""
                            } }
                        ]
                    } },
                    { ""author"": {
                        ""name"": ""Kress, Peter"",
                        ""books"": [
                            { ""book"": {
                                ""id"": ""bk109"",
                                ""title"": ""Paradox Lost"",
                                ""genre"": ""Science Fiction"",
                                ""price"": ""6.95"",
                                ""publish_date"": ""2000-11-02"",
                                ""description"": ""\n      After an inadvertant trip through a Heisenberg\n      Uncertainty Device, James Salway discovers the problems\n      of being quantum.\n    ""
                            } }
                        ]
                    } },
                    { ""author"": {
                        ""name"": ""O'Brien, Tim"",
                        ""books"": [
                            { ""book"": {
                                ""id"": ""bk110"",
                                ""title"": ""Microsoft .NET: The Programming Bible"",
                                ""genre"": ""Computer"",
                                ""price"": ""36.95"",
                                ""publish_date"": ""2000-12-09"",
                                ""description"": ""\n      Microsoft's .NET initiative is explored in\n      detail in this deep programmer's reference.\n    ""
                            } },
                            { ""book"": {
                                ""id"": ""bk111"",
                                ""title"": ""MSXML3: A Comprehensive Guide"",
                                ""genre"": ""Computer"",
                                ""price"": ""36.95"",
                                ""publish_date"": ""2000-12-01"",
                                ""description"": ""\n      The Microsoft MSXML3 parser is covered in\n      detail, with attention to XML DOM interfaces, XSLT processing,\n      SAX and more.\n    ""
                            } }
                        ]
                    } },
                    { ""author"": {
                        ""name"": ""Ralls, Kim"",
                        ""books"": [
                            { ""book"": {
                                ""id"": ""bk102"",
                                ""title"": ""Midnight Rain"",
                                ""genre"": ""Fantasy"",
                                ""price"": ""5.95"",
                                ""publish_date"": ""2000-12-16"",
                                ""description"": ""\n      A former architect battles corporate zombies,\n      an evil sorceress, and her own childhood to become queen\n      of the world.\n    ""
                            } }
                        ]
                    } },
                    { ""author"": {
                        ""name"": ""Randall, Cynthia"",
                        ""books"": [
                            { ""book"": {
                                ""id"": ""bk106"",
                                ""title"": ""Lover Birds"",
                                ""genre"": ""Romance"",
                                ""price"": ""4.95"",
                                ""publish_date"": ""2000-09-02"",
                                ""description"": ""\n      When Carla meets Paul at an ornithology\n      conference, tempers fly as feathers get ruffled.\n    ""
                            } }
                        ]
                    } },
                    { ""author"": {
                        ""name"": ""Thurman, Paula"",
                        ""books"": [
                            { ""book"": {
                                ""id"": ""bk107"",
                                ""title"": ""Splish Splash"",
                                ""genre"": ""Romance"",
                                ""price"": ""4.95"",
                                ""publish_date"": ""2000-11-02"",
                                ""description"": ""\n      A deep sea diver finds true love twenty\n      thousand leagues beneath the sea.\n    ""
                            } }
                        ]
                    } }
                ]
            } }";

            var _1 = DataProcessor.Deserialize(input);
            var _2 = DataProcessor.Transform(_1);
            var actual = DataProcessor.Serialize(_2);

            AssertXml(expected, actual);
        }

        private static void AssertXml(string expected, string actual)
        {
            Assert.AreEqual(
                Regex.Replace(expected, @"\s+", string.Empty),
                Regex.Replace(actual, @"\s+", string.Empty));
        }
    }
}
