using System.Text.Json;

namespace PortersonToXml
{
    public class IdenticalNamingPolicy : JsonNamingPolicy, IPortersonXmlNamingPolicy
    {
        public static IdenticalNamingPolicy Instance { get; } = new();

        public override string ConvertName(string name)
        {
            return name;
        }
    }
}
