using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MFCResourceGenerator.Configuration;
using MFCResourceGenerator.Controller;
using System.Diagnostics;
using MFCResourceGenerator.Model;
using MFCResourceGenerator.Utility;

namespace MFCResourceGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
                return;

            var items = ParseResourceItems(options);
            if (items.Count == 0)
            {
                Trace.WriteLine("# Resource files empty.");
                return;
            }

            RCHeaderGenerator headerGen = new RCHeaderGenerator(items, options);
            if (!headerGen.Create())
            {
                Trace.WriteLine("# Failed to generate header file.");
                return;
            }

            RCFileGenerator rcGen = new RCFileGenerator(items, options);
            if (!rcGen.Create())
            {
                Trace.WriteLine("# Failed to generate rc file.");
                return;
            }
        }

        public static List<ResourceItem> ParseResourceItems(Options options)
        {
            if (options.Verbose)
            {
                Trace.WriteLine("# Working Path: " + options.WorkingPath);
                Trace.WriteLine("# Resource Path: " + options.ResourcePath);
                Trace.WriteLine("# Output Path: " + options.OutputPath);
            }

            string[] resExtensions = { ".png" };

            if (options.Verbose)
                Trace.WriteLine("# Target Resource Type: " + string.Join(", ", resExtensions));

            var items = Directory.GetFiles(options.ResourcePath, "*.*", SearchOption.AllDirectories)
                .Where(p => resExtensions.Any(o => p.EndsWith(o, StringComparison.OrdinalIgnoreCase)))
                .Select(p =>
                {
                    string relativePath = PathUtils.MakeRelative(p, options.WorkingPath);
                    return new ResourceItem(relativePath);
                })
                .OrderBy(f => f.DefinedResourceIdString)
                .ToList();

            if (options.Verbose)
            {
                Trace.WriteLine("# Target Resource Files: " + items.Count);

                foreach (var item in items)
                    Trace.WriteLine("\t" + item.ToString());
            }

            return items;
        }
    }
}