using System;

namespace PortersonToXmlAndJson
{
    class Program
    {
        private static void TransformAndSaveToXml(string input, string output)
        {
            throw new NotImplementedException();
        }
        static void TransformAndSaveToJson(string input, string output)
        {
            throw new NotImplementedException();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("To Both");

            var input = @"PortersonToXml\ExampleData\ExampleInput.xml";
            var xmlOutput = @"PortersonToXml\ExampleData\ExampleOutputBoth.xml";
            var jsonOutput = @"PortersonToXml\ExampleData\ExampleOutputBoth.json";
            TransformAndSaveToXml(input, xmlOutput);
            TransformAndSaveToJson(input, jsonOutput);
        }
    }
}
