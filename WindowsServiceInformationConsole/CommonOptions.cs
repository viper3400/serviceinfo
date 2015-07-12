using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommandLine;

namespace WindowsServiceInformationConsole
{
    abstract class CommonOptions
    {
        [Option('s', "servicefilter", Required = false, DefaultValue = null, HelpText = "filter for service name or name part")]
        public string ServiceFilter { get; set; }

        [Option('o', "outputfile", Required = true, HelpText = "output file")]
        public string OutputFile { get; set; }

        [Option('c', "configsaver", Required= false, HelpText = "enables save of config information and sets the output path")]
        public string ConfigOutputPath {get; set;}
    }
   
}
