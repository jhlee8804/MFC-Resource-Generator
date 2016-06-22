using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MFCResourceGenerator.Configuration;
using MFCResourceGenerator.Controller;

namespace MFCResourceGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
                return;

            RCHeaderGenerator headerGen = new RCHeaderGenerator(options);
            headerGen.Create();

        }
    }
}