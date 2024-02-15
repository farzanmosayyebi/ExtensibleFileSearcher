using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionPlatform
{
    public interface ISearcher
    {
        public string FileType { get; set; }
        public string Description { get; set; }
        public bool SearchByContent(string filePath, string query);
        public List<string> StartSearchByContent(List<string> fileList, string query);
    }
}
