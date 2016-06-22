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
                Trace.WriteLine("# Read resource.h template.");

            string header = File.ReadAllText(HEADER_FILE);
            string contents = string.Empty;

            for (int i = 0; i < Items.Count; ++i)
            {
                if (i > 0)
                    contents += Environment.NewLine;

                var file = Items[i];
                string idText = file.DefinedResourceIdString;
                idText += "\t" + (i + 100);

                if (Options.Verbose)
                    Trace.WriteLine("\t" + idText);

                contents += idText;
            }

            header = header.Replace(PLACE_HOLDER, contents);
            
            string newHeaderPath = HEADER_FILE.Replace(".temp", string.Empty);
            File.WriteAllText(newHeaderPath, header);

            if (Options.Verbose)
                Trace.WriteLine("# Succeed to generate header file: " + newHeaderPath);

            return true;
        }

        public List<ResourceItem> Items { get; set; }

        public Options Options { get; set; }
    }
}
