using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using MFCResourceGenerator.Configuration;

namespace MFCResourceGenerator.Controller
{
    public class RCHeaderGenerator
    {
        public RCHeaderGenerator(Options options)
        {
            Debug.Assert(options != null);
            Options = options;
        }

        public bool Create()
        {
            // enumerate all files
            var extensions = new List<string> { ".png" };
            var resourceFiles = Directory.GetFiles(Options.WorkingDir, "*.*", SearchOption.AllDirectories)
                .Where(f => extensions.Any(o => f.EndsWith(o)));

            return true;
        }

        public Options Options { get; set; }
    }
}
