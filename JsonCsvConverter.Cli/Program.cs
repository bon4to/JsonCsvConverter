using System;
using System.IO;

namespace JsonCsvConverter.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check the arguments
            if (!IsValidArgs(args)) { return; }

            // Proceed with the conversion
            string JsonPath = ConvertCsvToJson(args);
            if (JsonPath != "")
            {
                Console.WriteLine($"Conversion successful! JSON file created at: {JsonPath}");
            }
        }

        static bool IsValidArgs(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("No input file specified.");
                AskHelp();
                return false;
            }
            else if (args.Length > 1)
            {
                Console.WriteLine("Too many arguments provided.");
                AskHelp();
                return false;
            }

            if (args[0] == "--help" || args[0] == "-h")
            {
                ShowHelp();
                return false;
            }
            return true;
        }

        static string ConvertCsvToJson(string[] args)
        {
            string csvFilePath = args[0];
            string jsonFilePath = Path.ChangeExtension(csvFilePath, ".json");

            if (!File.Exists(csvFilePath))
            {
                Console.WriteLine($"Error: CSV file '{csvFilePath}' not found. Please provide a valid path.");
                return "";
            }

            Console.WriteLine($"Converting '{csvFilePath}' to '{jsonFilePath}'...");

            // TODO: Implement conversion logic

            return jsonFilePath;
        }

        static void AskHelp()
        {
            Console.WriteLine("Type 'dotnet run --help' for help.");
        }

        static void ShowHelp()
        {
            Console.WriteLine("dotnet run <path_to_csv_file>");
            Console.WriteLine("Example: dotnet run data.csv");
        }
    }
}