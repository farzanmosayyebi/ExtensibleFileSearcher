using Microsoft.EntityFrameworkCore;

namespace FileSearcher
{
    public class HistoryEntry
    {
        public int Id { get; set; }
        public string Root { get; set; }
        public string[] FileTypes { get; set; }
        public string Query { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<string> Results { get; set; }
        public SearchMode SearchMode { get; set; }

        public HistoryEntry(string root, string[] fileTypes, string query, DateTime timeStamp, List<string> results, SearchMode searchMode)
        {
            Root = root;
            FileTypes = fileTypes;
            Query = query;
            TimeStamp = timeStamp;
            Results = results;
            SearchMode = searchMode;
        }
    }
}
