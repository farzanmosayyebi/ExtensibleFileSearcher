using ExtensionPlatform;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace JsonSearchExtension
{
    [SearchExtension]
    public class JsonSearcher : IExtension
    {
        public string Name { get; set; } = "JSON Searcher";
        public string Description { get; set; } = "To search the contents of .json files for a specified object";
        public string FileType { get; set; } = ".json";
        public string Author { get; set; } = "Farzan Mosayyebi";

        public bool SearchByContent(string filePath, string query)
        {
            string fileText = File.ReadAllText(filePath);
            return fileText.Contains(query);
        }

        public List<string> StartSearchByContent(List<string> fileList, string query)
        {
            List<string> result = fileList
                .Where(f => Path.GetExtension(f) == FileType)
                .AsParallel()
                .Where(f => SearchByContent(f, query))
                .ToList();

            return result;
        }

    }
}
