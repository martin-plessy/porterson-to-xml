using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PortersonToXml
{
    public static class PortersonXmlSerializer
    {
        // https://stackoverflow.com/a/258974
        private static readonly XmlSerializerNamespaces XmlNamespaces = new();

        static PortersonXmlSerializer()
        {
            XmlNamespaces.Add("", "");
        }

        // https://stackoverflow.com/a/3862106
        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

        public static string Serialize<T>(T value, PortersonXmlSerializerOptions? options = null)
        {
            options ??= new();

            var overrides = BuildAttributeOverrides(typeof(T), options);
            var serializer = new XmlSerializer(typeof(T), overrides);
            using var stringWriter = new Utf8StringWriter();

            serializer.Serialize(stringWriter, value, XmlNamespaces);

            return stringWriter.ToString();
        }

        public static T Deserialize<T>(string xml, PortersonXmlSerializerOptions? options = null)
        {
            options ??= new();

            var overrides = BuildAttributeOverrides(typeof(T), options);
            var serializer = new XmlSerializer(typeof(T), overrides);
            using var stringReader = new StringReader(xml);

            var value = serializer.Deserialize(stringReader);

            return (T) (value!);
        }

        private static XmlAttributeOverrides BuildAttributeOverrides(Type serializedType, PortersonXmlSerializerOptions options)
        {
            var overrides = new XmlAttributeOverrides();

            overrides.Add(serializedType, new XmlAttributes
            {
                XmlType = new XmlTypeAttribute(options.NamingPolicy.ConvertName(serializedType.Name))
            });

            foreach (var property in serializedType.GetProperties())
            {
                var attributes = new XmlAttributes();
                var serializedPropertyName = options.NamingPolicy.ConvertName(property.Name);

                if (property.PropertyType != typeof(string) && property.PropertyType.IsAssignableTo(typeof(IEnumerable)))
                {
                    var arrayItemElementType = property.PropertyType
                        .GetInterfaces()
                        .First(@interface => @interface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                        .GetGenericArguments()
                        .Single()
                        .Name;

                    var arrayItemElementName = options.NamingPolicy.ConvertName(arrayItemElementType);

                    attributes.XmlArray = new XmlArrayAttribute(serializedPropertyName);
                    attributes.XmlArrayItems.Add(new XmlArrayItemAttribute(arrayItemElementName));
                }
                else if (options.UseAttributesWhenPossible)
                {
                    attributes.XmlAttribute = new XmlAttributeAttribute(serializedPropertyName);
                }
                else
                {
                    attributes.XmlElements.Add(new XmlElementAttribute(serializedPropertyName));
                }

                overrides.Add(serializedType, property.Name, attributes);
            }

            return overrides;
        }
    }
}
