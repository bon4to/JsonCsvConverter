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
            return;
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

            Console.WriteLine($"Converting '{csvFilePath}'...");

            // TODO: Implement code abstraction 
            // to make the code more readable**

            string csvContent = File.ReadAllText(csvFilePath);

            // Separate lines and headers
            string[] lines = csvContent.Split("\n\n")[0] // "\n\n" separates the ghost lines
                .Split('\n');

            string[] headerCells = lines[0].Split(';');

            string jsonContent = "[";

            for (int i = 1; i < lines.Length; i++)
            {
                string[] lineCells = lines[i].Split(';');

                for (int j = 0; j < headerCells.Length; j++)
                {
                    if (j == 0) { jsonContent += "{"; }
                    jsonContent += $"\"{headerCells[j]}\": \"{lineCells[j]}\"";
                    if (j == headerCells.Length - 1) { jsonContent += "}"; } else { jsonContent += ", "; }
                }

                if (i != lines.Length - 1) { jsonContent += ", "; } else { jsonContent += "]"; }
            }

            File.WriteAllText(jsonFilePath, jsonContent);
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