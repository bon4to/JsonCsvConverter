namespace JsonCsvConverter.Cli
{
    public class Converter
    {
        /// <summary>
        /// ConvertCsvToJson takes the arguments and 
        /// returns the path of the created json file
        /// </summary>
        /// <param name="args"></param>
        /// <returns>json file path</returns>
        public static string CsvToJson(string[] args)
        {
            string csvFilePath = args[1];

            if (!File.Exists(csvFilePath))
            {
                Console.WriteLine($"Error: CSV file '{csvFilePath}' not found. Please provide a valid path.");
                return "";
            }

            Console.WriteLine($"Converting '{csvFilePath}'...");
            string csvContent = File.ReadAllText(csvFilePath);

            // Creates the json content
            var (jsonContent, error) = JsonBuilder(csvContent, hasHeader: true);
            if (error != null)
            {
                // I know it's not the best thing to use a 'golang-way' error handling here... 
                // but who knows, maybe it'll stay like that :D
                System.Console.WriteLine($"Error: {error}");
                return "";
            }

            // Changes the file extension
            string jsonFilePath = Path.ChangeExtension(csvFilePath, ".json");
            // Saves the file
            File.WriteAllText(jsonFilePath, jsonContent);

            return jsonFilePath;
        }

        private static (string jsonContent, Exception? error) JsonBuilder(string csvContent, bool hasHeader)
        {
            // Not possible to convert (i guess)
            if (!hasHeader) { return ("", new Exception("No header found in CSV file.")); }

            // Separate lines and headers
            string[] lines = csvContent.Split("\n\n")[0] // "\n\n" separates the ghost lines
                .Split('\n');

            string[] headerCells = lines[0].Split(';');
            // Starts the file content
            string jsonContent = "[";
            try
            {
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
            }
            catch (Exception error)
            {
                return ("", error);
            }

            return (jsonContent, null);
        }
    }
}