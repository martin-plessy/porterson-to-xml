using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Humanizer;
using NUnit.Framework;

namespace PortersonToJson.Tests.Learning
{
    internal class Potato
    {
        public string SkinColor { get; set; } = string.Empty;
    }

    public class WrapperJsonConverter<T> : JsonConverter<T>
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

    public class JsonSerializationLearningTests
    {
        [Test]
        public void Test()
        {
            var json = JsonSerializer.Serialize(new Potato { SkinColor = "gold" }, new()
            {
                PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,
                Converters =
                {
                    new WrapperJsonConverter<Potato>()
                }
            });

            Assert.AreEqual(@"{""potato"":{""skin_color"":""gold""}}", json);
        }
    }

    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public static readonly SnakeCaseNamingPolicy Instance = new();

        public override string ConvertName(string name)
        {
            return name.Underscore();
        }
    }
}
