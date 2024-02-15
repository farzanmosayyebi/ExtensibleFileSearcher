using System;
using ExtensionPlatform;
using Microsoft.EntityFrameworkCore.Update;
using System.Security.AccessControl;
using System.IO;
using System.Security.Permissions;

namespace FileSearcher
{
    public enum SearchMode
    {
        SearchByName = 1,
        SearchByContent = 2,
        SearchByNameAndContent = 3
    }

    class SearchManager
    {
        private readonly HistoryManager _historyManager;
        private readonly List<ISearcher> _searchers;

        public SearchManager(HistoryManager historyManager, List<IExtension> extensions, TxtSearcher txtSearcher)
        {
            _historyManager = historyManager;
            _searchers = [txtSearcher, .. extensions];
        }

        public void PerformSearch()
        {

            List<string> AvailableTypes = _searchers.Select(s => s.FileType).ToList();

            string root = InputManager.GetRootPath();
            
            SearchMode searchMode = InputManager.GetSearchMode();
            
            int[] selectedTypes = InputManager.GetTypesToSearch(AvailableTypes);

            string query = InputManager.GetSearchQuery();
            
            List<string> selectedTypesNames = selectedTypes.Select(t => AvailableTypes[t - 1]).ToList();

            string dir = new string(root);

            Console.WriteLine("Searching ...\n");

            List<string> results = StartSearch(dir, query, selectedTypesNames, searchMode);

            DisplayResults(results);

            List<string> resultsFullNames = results.Select(r => r).ToList();
            AddResultsToHistory(new HistoryEntry(root, selectedTypesNames.ToArray(), query, DateTime.Now, resultsFullNames, searchMode));
        }

        private void DisplayResults(List<string> results)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------\n");

            if (results.Count == 0)
                Console.WriteLine("No results were found!");
            else
            {
                Console.WriteLine($"{results.Count} result(s) were found:\n");
                results.ForEach(Console.WriteLine);
            }
         
            Console.WriteLine("\n---------------------------------------------------");

        }

        private void AddResultsToHistory(HistoryEntry entry)
        {
            _historyManager.AddEntry(entry);
        }

        public List<string> StartSearch(string rootDir, string query, List<string> types, SearchMode searchMode)
        {
            List<string> subDirectories = new List<string>();
            List<string> fileList = new List<string>();
            List<string> results = new List<string>();
            
            try
            {
                subDirectories = Directory.EnumerateDirectories(rootDir).ToList();
                fileList = Directory.EnumerateFiles(rootDir).ToList();
            }
            catch(UnauthorizedAccessException ex)
            {
            }

            switch (searchMode)
            {
                case (SearchMode.SearchByName):
                    {
                        results.AddRange(StartSearchByName(rootDir, query, types));
                        break;
                    }

                case (SearchMode.SearchByContent):
                    {
                        results.AddRange(StartSearchByContent(fileList, query, types));
                        break;
                    }

                case (SearchMode.SearchByNameAndContent):
                    {
                        results.AddRange(StartSearchByName(rootDir, query, types));
                        results.AddRange(StartSearchByContent(fileList, query, types));
                        break;
                    }

                default: break;
            }

            List<Thread> threads = new List<Thread>();
            foreach (string dir in subDirectories)
            {
                Thread thread = new Thread(() => { results.AddRange(StartSearch(dir, query, types, searchMode)); });
                threads.Add(thread);
                thread.Start();
            }
            threads.ForEach(thread => thread.Join());
            
            return results.Where(r => r != null).ToList();
        }
        public List<string> StartSearchByName(string directoryPath, string query, List<string> types)
        {
            List<string> fileList = new List<string>();
            try
            {
                fileList = Directory.EnumerateFiles(directoryPath)
                    .Where(f =>
                        Path.GetFileName(f).Contains(query)
                        && types.Any(k => k == Path.GetExtension(f)))
                    .ToList();
            }
            catch (UnauthorizedAccessException ex)
            {
            }
            return fileList;
        }

        public List<string> StartSearchByContent(List<string> fileList, string query, List<string> types)
        {
            List<string> results = new List<string>();
            List<ISearcher> selectedSearchers = new List<ISearcher>();

            foreach (string type in types)
            {
                selectedSearchers.AddRange(_searchers.Where(s => s.FileType == type));
            }

            List<Thread> threads = new List<Thread>();

            foreach (ISearcher searcher in selectedSearchers)
            {
                Thread thread = new Thread(() => { results.AddRange(searcher.StartSearchByContent(fileList, query)); });
                threads.Add(thread);
                thread.Start();
            }

            threads.ForEach(t => t.Join());            

            return results;
        }

    }
}
