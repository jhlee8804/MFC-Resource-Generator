using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using CommandLine.Text;
using System.IO;

namespace MFCResourceGenerator.Configuration
{
    public class Options
    {
        private string _workingPath;
        private string _resPath;
        private string _outPath;

        [Option("wpath", Required = false, HelpText = "Working directory for resource.h, resource.rc files.")]
        public string WorkingPath 
        {
            get { return _workingPath; }
            set { _workingPath = Path.GetFullPath(value); }
        }

        [Option("rpath", Required = true, HelpText = "Resource directory.")]
        public string ResourcePath
        {
            get { return _resPath; }
            set { _resPath = Path.GetFullPath(value); }
        }

        [Option("opath", Required = false, HelpText = "Output directory to be generated.")]
        public string OutputPath 
        {
            get { return _outPath; }
            set { _outPath = Path.GetFullPath(value); }
        }

        [Option('v', "verbose", DefaultValue = true, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
