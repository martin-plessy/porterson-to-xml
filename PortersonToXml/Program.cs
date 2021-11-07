using System;
using System.IO;
using System.Text;
using PortersonToXml.Services;

namespace PortersonToXml
{
    internal static class Program
    {
        internal static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.Error.WriteLine("Usage: PortersonToXml <input:FILE_PATH> <output:FILE_PATH>");
                return 1;
            }

            var inputPath = args[0];
            var outputPath = args[1];

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine("The input file does not exist...");
                return 2;
            }

            if (Directory.Exists(outputPath))
            {
                Console.Error.WriteLine("The output path points to an existing directory...");
                return 3;
            }

            Console.WriteLine("To Xml");

            TransformAndSaveToXml(inputPath, outputPath);

            return 0;
        }

        private static void TransformAndSaveToXml(string inputPath, string outputPath)
        {
            var input = File.ReadAllText(inputPath);

            var _1 = DataProcessor.Deserialize(input);
            var _2 = DataProcessor.Transform(_1);
            var output = DataProcessor.Serialize(_2);

            File.WriteAllText(outputPath, output, Encoding.UTF8);
        }
    }
}
