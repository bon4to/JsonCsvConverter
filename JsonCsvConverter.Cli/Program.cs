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
            string JsonPath = Converter.CsvToJson(args);
            if (JsonPath != "")
            {
                Console.WriteLine($"Conversion successful! JSON file created at: {JsonPath}");
            }
            return;
        }

        static bool IsValidArgs(string[] args)
        {
            if (args[0] != "conv")
            {
                Console.WriteLine("Invalid arguments provided.");
                AskHelp();
                return false;
            }

            if (args.Length < 2)
            {
                Console.WriteLine("No input file specified.");
                AskHelp();
                return false;
            }
            else if (args.Length > 2)
            {
                Console.WriteLine("Too many arguments provided.");
                AskHelp();
                return false;
            }

            if (args[1] == "--help" || args[1] == "-h")
            {
                ShowHelp();
                return false;
            }
            return true;
        }

        static void AskHelp()
        {
            // TODO: fix
            // not working yet, '--help' flag trigger .net help
            Console.WriteLine("Type 'dotnet run conv --help' for help.");
        }

        static void ShowHelp()
        {
            Console.WriteLine("dotnet run conv <path_to_csv_file>");
            Console.WriteLine("Example: dotnet run conv data.csv");
        }
    }
}