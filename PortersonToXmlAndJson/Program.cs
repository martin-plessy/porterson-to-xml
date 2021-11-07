using System;
using System.IO;
using System.Text;

namespace PortersonToXmlAndJson
{
    internal static class Program
    {
        internal static int Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.Error.WriteLine("Usage: PortersonToJson <input:FILE_PATH> <xml_output:FILE_PATH> <json_output:FILE_PATH>");
                return 1;
            }

            var inputPath = args[0];
            var xmlOutputPath = args[1];
            var jsonOutputPath = args[2];

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine("The input file does not exist...");
                return 2;
            }

            if (Directory.Exists(xmlOutputPath) || Directory.Exists(jsonOutputPath))
            {
                Console.Error.WriteLine("An output path points to an existing directory...");
                return 3;
            }

            Console.WriteLine("To Both");

            TransformAndSaveToXml(inputPath, xmlOutputPath);
            TransformAndSaveToJson(inputPath, jsonOutputPath);

            return 0;
        }

        private static void TransformAndSaveToXml(string inputPath, string outputPath)
        {
            var input = File.ReadAllText(inputPath);

            var _1 = PortersonToXml.Services.DataProcessor.Deserialize(input);
            var _2 = PortersonToXml.Services.DataProcessor.Transform(_1);
            var output = PortersonToXml.Services.DataProcessor.Serialize(_2);

            File.WriteAllText(outputPath, output, Encoding.UTF8);
        }

        private static void TransformAndSaveToJson(string inputPath, string outputPath)
        {
            var input = File.ReadAllText(inputPath);

            var _1 = PortersonToJson.Services.DataProcessor.Deserialize(input);
            var _2 = PortersonToJson.Services.DataProcessor.Transform(_1);
            var output = PortersonToJson.Services.DataProcessor.Serialize(_2);

            File.WriteAllText(outputPath, output, Encoding.UTF8);
        }
    }
}
