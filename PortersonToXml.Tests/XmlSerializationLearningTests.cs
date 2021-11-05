using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace PortersonToXml.Tests
{
    public class Potato
    {
        public string SkinColor { get; set; } = string.Empty;
        public List<double> SizeHistory { get; } = new();
    }

    public class XmlSerializationLearningTests
    {
        [Test]
        public void Json_invalid_deserialization()
        {
            var json = @"[ ""gold"" ]";

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Potato>(json));
        }

        [Test]
        public void Json_serialization_and_deserialization()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance
            };
            var json = JsonSerializer.Serialize(new Potato { SkinColor = "gold" }, options);
            var deserialized = JsonSerializer.Deserialize<Potato>(json, options);

            AssertSerialization(@"{ ""skin_color"": ""gold"", ""size_history"": [] }", json);
            Assert.AreEqual("gold", deserialized!.SkinColor);
            Assert.IsEmpty(deserialized.SizeHistory);
        }

        [Test]
        public void Xml_invalid_deserialization()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?> <skin_color>gold</skin_color>";

            Assert.Throws<InvalidOperationException>(() => PortersonXmlSerializer.Deserialize<Potato>(xml));
        }

        [Test]
        public void Xml_serialization_and_deserialization()
        {
            var options = new PortersonXmlSerializerOptions
            {
                NamingPolicy = SnakeCaseNamingPolicy.Instance
            };
            var xml = PortersonXmlSerializer.Serialize(new Potato { SkinColor = "gold" }, options);
            var deserialized = PortersonXmlSerializer.Deserialize<Potato>(xml, options);

            AssertSerialization(@"<?xml version=""1.0"" encoding=""utf-8""?> <potato> <skin_color>gold</skin_color> <size_history/> </potato>", xml);
            Assert.AreEqual("gold", deserialized.SkinColor);
            Assert.IsEmpty(deserialized.SizeHistory);
        }

        [Test]
        public void Xml_serialization_and_deserialization_of_collection()
        {
            var options = new PortersonXmlSerializerOptions
            {
                NamingPolicy = SnakeCaseNamingPolicy.Instance
            };
            var xml = PortersonXmlSerializer.Serialize(new Potato
            {
                SkinColor = "gold",
                SizeHistory = { 1.2, 1.8, 2.3 }
            }, options);
            var deserialized = PortersonXmlSerializer.Deserialize<Potato>(xml, options);

            AssertSerialization(@"<?xml version=""1.0"" encoding=""utf-8""?>
            <potato>
                <skin_color>gold</skin_color>
                <size_history>
                    <double>1.2</double>
                    <double>1.8</double>
                    <double>2.3</double>
                </size_history>
            </potato>", xml);
            Assert.AreEqual("gold", deserialized.SkinColor);
            Assert.AreEqual(new[] { 1.2, 1.8, 2.3 }, deserialized.SizeHistory);
        }

        [Test]
        public void Xml_serialization_and_deserialization_with_attributes()
        {
            var options = new PortersonXmlSerializerOptions
            {
                NamingPolicy = SnakeCaseNamingPolicy.Instance,
                UseAttributesWhenPossible = true
            };
            var xml = PortersonXmlSerializer.Serialize(new Potato { SkinColor = "gold" }, options);
            var deserialized = PortersonXmlSerializer.Deserialize<Potato>(xml, options);

            AssertSerialization(@"<?xml version=""1.0"" encoding=""utf-8""?> <potato skin_color=""gold""> <size_history/> </potato>", xml);
            Assert.AreEqual("gold", deserialized.SkinColor);
            Assert.IsEmpty(deserialized.SizeHistory);
        }
        
        [Test]
        public void Xml_serialization_and_deserialization_of_collection_with_attributes()
        {
            var options = new PortersonXmlSerializerOptions
            {
                NamingPolicy = SnakeCaseNamingPolicy.Instance,
                UseAttributesWhenPossible = true
            };
            var xml = PortersonXmlSerializer.Serialize(new Potato
            {
                SkinColor = "gold",
                SizeHistory = { 1.2, 1.8, 2.3 }
            }, options);
            var deserialized = PortersonXmlSerializer.Deserialize<Potato>(xml, options);

            AssertSerialization(@"<?xml version=""1.0"" encoding=""utf-8""?> 
            <potato skin_color=""gold"">
                <size_history>
                    <double>1.2</double>
                    <double>1.8</double>
                    <double>2.3</double>
                </size_history>
            </potato>", xml);
            Assert.AreEqual("gold", deserialized.SkinColor);
            Assert.AreEqual(new[] { 1.2, 1.8, 2.3 }, deserialized.SizeHistory);
        }

        private static void AssertSerialization(string expected, string actual)
        {
            Assert.AreEqual(
                Regex.Replace(expected, @"\s+", string.Empty),
                Regex.Replace(actual, @"\s+", string.Empty));
        }
    }
}
