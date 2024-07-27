using ExtensionPlatform;

namespace XmlSearcherExtension
{
    [SearchExtension]
    public class XmlSearcher : IExtension
    {
        public string Name { get; set; } = "XML Searcher";
        public string Description { get; set; } = "An extension to search the contents of  .xml files";
        public string FileType { get; set; } = ".xml";
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
                .AsSequential()
                .ToList();

            return result;
        }
    }
}
