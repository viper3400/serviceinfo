using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace WindowsServiceInformationConsole
{
    class WssOptions
    {
        [Option('s',"servicefilter",Required=false, DefaultValue=null, HelpText="filter services to this name or name part")]
        public string ServiceFilter { get; set; }

        [Option('o',"outputfile",Required=false,HelpText="output file path")]
        public string OutoutFile { get; set; }

        [HelpOption]
        public string GetUsage()
        {           
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
