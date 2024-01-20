using Microsoft.EntityFrameworkCore;

namespace FileSearcher
{
    class HistoryManager
    {
        private DbContextOptions<HistoryDbContext> _contextOptions;
        public HistoryManager(DbContextOptions<HistoryDbContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }
        public void AddEntry(HistoryEntry entry)
        {
            using var _dbContext = new HistoryDbContext(_contextOptions);

            _dbContext.Database.Migrate();
            _dbContext.HistoryEntries.Add(entry);
            _dbContext.SaveChanges();
        }

        public void RetrieveRecords()
        {
            Console.Clear();
            using var _dbContext = new HistoryDbContext(_contextOptions);

            foreach (var entry in _dbContext.HistoryEntries)
            {
                Console.WriteLine("--------------------------------------------\n");
                Console.WriteLine("Search Id: " + entry.Id);
                Console.WriteLine("Date: " + entry.TimeStamp.ToString());
                Console.WriteLine("\nRoot: " + entry.Root);
                Console.WriteLine("Query: " + entry.Query);
                Console.WriteLine("File types: " + string.Join(", ", entry.FileTypes));
                Console.WriteLine("Search Mode: " + entry.SearchMode);

                Console.WriteLine("\nResults: ");
                entry.Results.ForEach(Console.WriteLine);

                Console.WriteLine("\n--------------------------------------------");

            }
        }
    }
}
