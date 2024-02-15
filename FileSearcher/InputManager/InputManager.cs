using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearcher
{
    public class InputManager
    {
        public static SearchMode GetSearchMode()
        {
            SearchMode searchMode;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"[{(int)SearchMode.SearchByName}]. Search files by name");
                Console.WriteLine($"[{(int)SearchMode.SearchByContent}]. Search files by content");
                Console.WriteLine($"[{(int)SearchMode.SearchByNameAndContent}]. Both");
                Console.Write("\nPick an option: ");

                try
                {
                    searchMode = (SearchMode)int.Parse(Console.ReadLine().Trim());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                break;
            } // select search mode

            return searchMode;
        }

        public static int[] GetTypesToSearch(List<string> availableTypes)
        {
            int[] selectedTypes;

            Console.WriteLine("\nSelect the file types to search: (separated by space (e.g. 1 2 3))\n");// add options for txt & any type

            for (int i = 0; i < availableTypes.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {availableTypes[i]}");
            }

            while (true)
            {
                Console.Write("\nYour choice: ");
                try
                {
                    selectedTypes = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), int.Parse);
                    if (selectedTypes.Any(st => st < 1 || st > availableTypes.Count))
                        throw new Exception();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Input");
                    continue;
                }
                break;
            }
            return selectedTypes;
        }

        public static string GetRootPath()
        {
            string root = string.Empty;
            while (true)
            {
                Console.Write("\nEnter the root path: ");
                root = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(root))
                {
                    Console.WriteLine("\nroot cannot be empty.\n");
                    continue;
                }
                if (!Path.IsPathFullyQualified(root))
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                if (!Directory.Exists(root))
                {
                    Console.WriteLine("\nDirectory does not exist. enter a valid path.\n");
                    continue;
                }

                break;
            } // enter the root path

            return root;
        }

        public static string GetSearchQuery()
        {
            string query = string.Empty;

            while (true)
            {
                Console.Write("\nQuery: ");
                query = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(query))
                {
                    Console.WriteLine("\nQuery cannot be empty.\n");
                    continue;
                }
                break;
            } // enter the query

            return query;
        }
    }
}
