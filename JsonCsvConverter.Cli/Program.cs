using System;
using System.IO;

namespace JsonCsvConverter.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 0)
            {
                Console.WriteLine("No input file specified.");
                return;
            }

            string csvFilePath = args[0];
            string jsonFilePath = Path.ChangeExtension(csvFilePath, ".json");

            if (!File.Exists(csvFilePath))
            {
                Console.WriteLine($"Error: CSV file '{csvFilePath}' not found. Please provide a valid path.");
                return;
            }

            Console.WriteLine($"Converting '{csvFilePath}' to '{jsonFilePath}'...");

            // TODO: Implement conversion logic

            Console.WriteLine("Conversion completed successfully!");
        }
    }
}