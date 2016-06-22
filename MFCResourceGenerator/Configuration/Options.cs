using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using CommandLine.Text;

namespace MFCResourceGenerator.Configuration
{
    public class Options
    {
        [Option('p', "path", Required = true, HelpText = "Working directory to images.")]
        public string WorkingDir { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output directory to be generated.")]
        public string OutputDir { get; set; }

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
