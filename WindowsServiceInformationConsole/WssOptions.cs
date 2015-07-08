using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommandLine;
using CommandLine.Text;

namespace WindowsServiceInformationConsole
{
    internal enum ModuleType { INI, WIKI, XML, TEST}

    class WssOptions
    {       

        [Option('s',"servicefilter",Required=false, DefaultValue=null, HelpText="filter for service name or name part")]
        public string ServiceFilter { get; set; }

        [Option('o',"outputfile",Required=false,HelpText="output file path")]
        public string OutputFile { get; set; }

        [Option("type", Required=true, HelpText="output type")]
        public ModuleType ModuleType { get; set; }

        [HelpOption]
        public string GetUsage()
        {           
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
