using System;
using System.IO;
using System.Text;

namespace PortersonToXmlAndJsonAndJson
{
    internal static class Program
    {
        internal static int Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.Error.WriteLine("Usage: PortersonToXmlAndJson <input:FILE_PATH> <xml_output:FILE_PATH> <json_output:FILE_PATH>");
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

            TransformAndSaveToXmlAndJson(inputPath, xmlOutputPath, jsonOutputPath);

            return 0;
        }

        private static void TransformAndSaveToXmlAndJson(string inputPath, string xmlOutputPath, string jsonOutputPath)
        {
            var input = File.ReadAllText(inputPath);

            var _1 = PortersonToXmlAndJson.Services.DataProcessor.Deserialize(input);
            var _2 = PortersonToXmlAndJson.Services.DataProcessor.Transform(_1);
            var xmlOutput = PortersonToXmlAndJson.Services.DataProcessor.SerializeToXml(_2);
            var jsonOutput = PortersonToXmlAndJson.Services.DataProcessor.SerializeToJson(_2);

            File.WriteAllText(xmlOutputPath, xmlOutput, Encoding.UTF8);
            File.WriteAllText(jsonOutputPath, jsonOutput, Encoding.UTF8);
        }
    }
}
