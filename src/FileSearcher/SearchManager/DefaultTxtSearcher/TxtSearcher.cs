using ExtensionPlatform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearcher
{
    public class TxtSearcher : ISearcher
    {
        public string FileType { get; set; } = ".txt";
        public string Description { get; set; } = "Default .txt searcher.";

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
