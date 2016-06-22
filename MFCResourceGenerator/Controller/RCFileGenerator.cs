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
    public class RCFileGenerator
    {
        private const string RC_FILE = "resource.rc.temp";
        private const string PLACE_HOLDER = "$RESOURCE_PNG_ID_LIST_HOLDER$";

        public RCFileGenerator(List<ResourceItem> items, Options options)
        {
            Debug.Assert(items.Count != 0);
            Debug.Assert(options != null);
            Items = items;
            Options = options;
        }

        public bool Create()
        {
            if (Options.Verbose)
                Trace.WriteLine("# Read resource.rc template.");

            string rcFile = File.ReadAllText(RC_FILE);
            string contents = string.Empty;

            for (int i = 0; i < Items.Count; ++i)
            {
                if (i > 0)
                    contents += Environment.NewLine;

                var file = Items[i];
                string idText = file.DefinedRCPathString;

                if (Options.Verbose)
                    Trace.WriteLine("\t" + idText);

                contents += idText;
            }

            rcFile = rcFile.Replace(PLACE_HOLDER, contents);

            string newRCPath = RC_FILE.Replace(".temp", string.Empty);
            File.WriteAllText(newRCPath, rcFile);

            if (Options.Verbose)
                Trace.WriteLine("# Succeed to generate rc file: " + newRCPath);

            return true;
        }

        public List<ResourceItem> Items { get; set; }

        public Options Options { get; set; }
    }
}
