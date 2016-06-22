using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MFCResourceGenerator.Configuration;

namespace MFCResourceGenerator.Controller
{
    public class RCFileGenerator
    {
        private Options _options;

        public RCFileGenerator(Options options)
        {
            Debug.Assert(options != null);
            _options = options;
        }
    }
}
