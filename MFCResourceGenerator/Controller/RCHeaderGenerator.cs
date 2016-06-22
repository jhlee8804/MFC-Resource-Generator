using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using MFCResourceGenerator.Configuration;
using MFCResourceGenerator.Model;

namespace MFCResourceGenerator.Controller
{
    public class RCHeaderGenerator
    {
        private const string HEADER_FILE = "resource.h.temp";
        private const string PLACE_HOLDER = "$RESOURCE_ID_LIST_HOLDER$";

        public RCHeaderGenerator(List<ResourceItem> items, Options options)
        {
            Debug.Assert(items.Count != 0);
            Debug.Assert(options != null);
            Items = items;
            Options = options;
        }

        public bool Create()
        {
            if (Options.Verbose)
                Console.WriteLine(Environment.NewLine + "# Read resource.h template.");

            string contents = string.Empty;

            for (int i = 0; i < Items.Count; ++i)
            {
                if (i > 0)
                    contents += Environment.NewLine;

                var file = Items[i];
                string idText = file.DefinedResourceIdString;
                idText += "\t" + (i + 100);

                if (Options.Verbose)
                    Console.WriteLine("\t" + idText);

                contents += idText;
            }

            string newContents = File.ReadAllText(HEADER_FILE);
            newContents = newContents.Replace(PLACE_HOLDER, contents);
            
            string newPath = HEADER_FILE.Replace(".temp", string.Empty);
            newPath = Path.Combine(Options.OutputPath, newPath);

            bool overwrite = true;

            if (File.Exists(newPath))
            {
                string oldContents = File.ReadAllText(newPath);
                overwrite = oldContents != newContents;
            }

            if (overwrite)
            {
                File.WriteAllText(newPath, newContents);

                if (Options.Verbose)
                    Console.WriteLine("# Succeed to generate header file: " + newPath);
            }
            else
            {
                Console.WriteLine("# Nothing changed header file.");
            }

            return true;
        }

        public List<ResourceItem> Items { get; set; }

        public Options Options { get; set; }
    }
}
