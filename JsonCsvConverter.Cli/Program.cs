using System;
using System.IO;

namespace JsonCsvConverter.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("No input file specified.");
                AskHelp();
                return;
            }
            else if (args.Length > 1)
            {
                Console.WriteLine("Too many arguments provided.");
                AskHelp();
                return;
            }

            if (args[0] == "--help" || args[0] == "-h")
            {
                ShowHelp();
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