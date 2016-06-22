using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MFCResourceGenerator.Configuration;
using MFCResourceGenerator.Controller;
using MFCResourceGenerator.Model;

namespace MFCResourceGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
                return;

            if (string.IsNullOrEmpty(options.WorkingPath))
            {
                options.WorkingPath = Directory.GetCurrentDirectory();
                Console.WriteLine("Working Path is empty. Default Path: " + options.OutputPath);
            }

            if (string.IsNullOrEmpty(options.OutputPath))
            {
                options.OutputPath = Directory.GetCurrentDirectory();
                Console.WriteLine("Output Path is empty. Default Path: " + options.OutputPath);
            }

            var items = ParseResourceItems(options);
            if (items.Count == 0)
            {
                Console.WriteLine("# Resource files empty.");
                return;
            }

            RCHeaderGenerator headerGen = new RCHeaderGenerator(items, options);
            if (!headerGen.Create())
            {
                Console.WriteLine("# Failed to generate header file.");
                return;
            }

            RCFileGenerator rcGen = new RCFileGenerator(items, options);
            if (!rcGen.Create())
            {
                Console.WriteLine("# Failed to generate rc file.");
                return;
            }
        }

        public static List<ResourceItem> ParseResourceItems(Options options)
        {
            if (options.Verbose)
            {
                Console.WriteLine("# Working Path: " + options.WorkingPath);
                Console.WriteLine("# Resource Path: " + options.ResourcePath);
                Console.WriteLine("# Output Path: " + options.OutputPath);
            }

            string[] resExtensions = { ".png" };

            if (options.Verbose)
                Console.WriteLine("# Target Resource Type: " + string.Join(", ", resExtensions));

            var items = Directory.GetFiles(options.ResourcePath, "*.*", SearchOption.AllDirectories)
                .Where(p => resExtensions.Any(o => p.EndsWith(o, StringComparison.OrdinalIgnoreCase)))
                .Select(p => new ResourceItem(p, options))
                .OrderBy(f => f.DefinedResourceIdString)
                .ToList();

            if (options.Verbose)
            {
                Console.WriteLine("# Target Resource Files: " + items.Count);

                foreach (var item in items)
                    Console.WriteLine("\t" + item.ToString());
            }

            return items;
        }
    }
}