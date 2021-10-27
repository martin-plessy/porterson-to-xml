using System;

namespace PortersonToXml
{
    class Program
    {
        static void TransformAndSaveToJson(string input, string output)
        {
            throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("To JSon");

            var input = @"PortersonToXml\ExampleData\ExampleInput.xml";
            var output = @"PortersonToXml\ExampleData\ExampleOutput.json";
            TransformAndSaveToJson(input,output);
        }
    }
}
