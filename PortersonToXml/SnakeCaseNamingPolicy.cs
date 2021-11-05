using System.Text.Json;
using Humanizer;

namespace PortersonToXml
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy, IPortersonXmlNamingPolicy
    {
        public static SnakeCaseNamingPolicy Instance { get; } = new();

        public override string ConvertName(string name)
        {
            return name.Underscore();
        }
    }
}
