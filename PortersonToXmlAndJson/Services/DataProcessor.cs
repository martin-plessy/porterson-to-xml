using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Humanizer;

namespace PortersonToXmlAndJson.Services
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

        public static string SerializeToXml(Entities.Output.Catalog catalog)
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

        public static string SerializeToJson(Entities.Output.Catalog catalog)
        {
            if (catalog == null)
            {
                throw new ArgumentNullException(nameof(catalog));
            }

            return JsonSerializer.Serialize(catalog, DefaultJsonSerializerOptions);
        }

        private static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new()
        {
            PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,
            Converters =
            {
                new WrapperJsonConverter<Entities.Output.Catalog>(),
                new WrapperJsonConverter<Entities.Output.Author>(),
                new WrapperJsonConverter<Entities.Output.Book>(),
                new NumberToStringJsonConverter(),
                new DateJsonConverter()
            },
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // The encoder refuses do allow '\'' and '+' without the unsafe mode.
        };

        private class SnakeCaseNamingPolicy : JsonNamingPolicy
        {
            public static readonly SnakeCaseNamingPolicy Instance = new();

            public override string ConvertName(string name)
            {
                return name.Underscore();
            }
        }

        private class WrapperJsonConverter<T> : JsonConverter<T>
        {
            public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                options = new(options);
                options.Converters.Remove(options.Converters.OfType<WrapperJsonConverter<T>>().FirstOrDefault()!);

                writer.WriteStartObject();
                writer.WritePropertyName(typeof(T).Name.Underscore());
                JsonSerializer.Serialize(writer, value, options);
                writer.WriteEndObject();
            }
        }

        private class NumberToStringJsonConverter : JsonConverter<decimal>
        {
            public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }

        private class DateJsonConverter : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
            }
        }
    }
}
