using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommandLine;
using CommandLine.Text;

namespace WindowsServiceInformationConsole
{
   
    class WssOptions
    {
        public WssOptions()
        {
            XmlVerb = new XmlModuleOptions();
        }

        [VerbOption("xml", HelpText = "Run WSI in xml output mode.")]
        public XmlModuleOptions XmlVerb { get; set; }

        [VerbOption("ini", HelpText = "Run WSI in ini output mode.")]
        public XmlModuleOptions IniVerb { get; set; }

        [VerbOption("wiki", HelpText = "Run WSI in wiki output mode.")]
        public XmlModuleOptions WikiVerb { get; set; }

        [VerbOption("test", HelpText = "Run WSI in test output mode.")]
        public XmlModuleOptions TestVerb { get; set; }
     

        [HelpOption]
        public string GetUsage()
        {           
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }

        // Remainder omitted
        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }
    }
}
