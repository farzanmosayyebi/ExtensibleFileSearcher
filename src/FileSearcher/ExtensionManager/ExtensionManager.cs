using System.Reflection;
using System.Xml.Linq;
using ExtensionPlatform;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FileSearcher
{
    class ExtensionManager
    {
        string _pluginsDirectoryPath;
        List<IExtension> _extensions;
        public List<IExtension> ExtensionList
        {
            get { return _extensions; }
        }

        public ExtensionManager(string pluginsDirectoryPath)
        {
            _pluginsDirectoryPath = pluginsDirectoryPath;
            _extensions = new List<IExtension>();

            ValidatePluginsDirectory();
        }

        void ValidatePluginsDirectory()
        {
            if (!Directory.Exists(_pluginsDirectoryPath))
            {
                Directory.CreateDirectory(_pluginsDirectoryPath);
            }
        }

        public void LoadExtensions()
        {
            if (!Directory.Exists(_pluginsDirectoryPath))
            {
                throw new Exception("There was a problem when loading the extensions.\nNo \"plugins\" directory was found!");
            }

            string[] dllFiles = Directory.GetFiles(_pluginsDirectoryPath, "*.dll");
            
            foreach (string dll in dllFiles)
            {
                try
                {
                    Assembly extension = Assembly.LoadFrom(dll);

                    List<Type> extensionTypes = extension
                        .GetTypes()
                        .Where(t =>
                            typeof(IExtension).IsAssignableFrom(t)
                            && !t.IsInterface
                            && t.GetCustomAttribute<SearchExtensionAttribute>() != null)
                        .ToList();


                    foreach (var extensionType in extensionTypes)
                    {
                        IExtension? ext = Activator.CreateInstance(extensionType) as IExtension;
                        if (ext != null)
                            _extensions.Add(ext);
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("an error occured while trying to load an extension");
                }
            }

        }

        public void DisplayDetailedExtensionsList()
        {
            Console.Clear();

            Console.WriteLine($"\n{_extensions.Count} Extension(s) were found:\n");
            Console.WriteLine("----------------------------------------------------------\n");

            for (int i = 0; i < _extensions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_extensions[i].Name}");
                Console.WriteLine($" --> Description; {_extensions[i].Description}");
                Console.WriteLine($" --> Author: {_extensions[i].Author}");
                Console.WriteLine();
            }

            Console.WriteLine("----------------------------------------------------------\n");
        }

    }
}
