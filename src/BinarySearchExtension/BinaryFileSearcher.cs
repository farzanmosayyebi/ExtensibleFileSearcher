using ExtensionPlatform;
using System.IO;

namespace BinarySearchExtension
{
    [SearchExtension]
    public class BinaryFileSearcher : IExtension
    {
        public string Name { get; set; } = "Binary File Searcher";
        public string Description { get; set; } = "To search the contents of .bin files for a specified pattern";
        public string FileType { get; set; } = ".bin";
        public string Author { get; set; } = "Farzan Mosayyebi";

        public bool SearchByContent(string filePath, string query)
        {
            byte[] queryBytes = Array.ConvertAll(query.Split(' '), q =>  byte.Parse(q, System.Globalization.NumberStyles.HexNumber));
            bool match = false;
            using (StreamReader reader = new StreamReader(filePath))
            {
                byte[] buffer = Array.ConvertAll(reader.ReadToEnd().Split(' '), n => byte.Parse(n, System.Globalization.NumberStyles.HexNumber));
                for (int i = 0; i < buffer.Length - queryBytes.Length; i++)
                {
                    match = true;

                    for (int j = 0; j < queryBytes.Length; j++)
                    {
                        if (buffer[i + j] != queryBytes[j])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        break;
                    }
                }
            }
                return match;
        }

        public List<string> StartSearchByContent(List<string> fileList, string query)
        {
            List<string> result = new List<string>();
            try
            {
                result = fileList
                    .Where(f => Path.GetExtension(f) == FileType)
                    .AsParallel()
                    .Where(f => SearchByContent(f, query))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
