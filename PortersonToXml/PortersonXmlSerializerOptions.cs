namespace PortersonToXml
{
    public class PortersonXmlSerializerOptions
    {
        public bool UseAttributesWhenPossible { get; set; }
        public IPortersonXmlNamingPolicy NamingPolicy { get; set; } = new IdenticalNamingPolicy();
    }
}
