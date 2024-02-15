using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace FileSearcher
{
    class Program
    {
        static void Main(string[] args)
        {
            const int SearchFiles = 1, ManageExtensions = 2, ViewHistory = 3, Exit = 4;

            Console.WriteLine();
            Console.WriteLine("=== Welcome to the FileSearcher Console App ===");

            int command;

            DbContextOptions<HistoryDbContext> contextOptions = new DbContextOptionsBuilder<HistoryDbContext>()
                .UseSqlite()
                .Options;

            //HistoryDbContext dbContext = new HistoryDbContext(contextOptions);
            //dbContext.Database.EnsureCreated();
            //dbContext.Dispose();

            TxtSearcher txtSearcher = new TxtSearcher();
            ExtensionManager extensionManager = new ExtensionManager(Path.Combine(Directory.GetCurrentDirectory(), "plugins"));
            HistoryManager historyManager = new HistoryManager(contextOptions);

            try
            {
                extensionManager.LoadExtensions();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            SearchManager searcher = new SearchManager(historyManager, extensionManager.ExtensionList, txtSearcher);

            while (true)
            {
                Console.WriteLine($"\n[{SearchFiles}]. Search for files");
                Console.WriteLine($"[{ManageExtensions}]. Manage Extensions");
                Console.WriteLine($"[{ViewHistory}]. View History");
                Console.WriteLine($"[{Exit}]. Exit\n");
                Console.Write("Select an operation to perform: ");

                try
                {
                    command = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("\nInput must be an integer!");
                    continue;
                }

                if (command == Exit)
                {
                    break;
                }

                switch (command)
                {
                    case SearchFiles:
                        searcher.PerformSearch();
                        break;

                    case ManageExtensions:
                        extensionManager.DisplayDetailedExtensionsList();
                        break;

                    case ViewHistory:
                        historyManager.RetrieveRecords();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("\nInput must be one of the operations below!");
                        break;
                }
                while (true)
                {
                    Console.WriteLine("Press any key to Continue...");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
            }

        }
    }
}
