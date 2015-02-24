using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ch.jaxx.WindowsServiceInformation;

namespace WindowsServiceInformationConsole
{
    class TestOutputModule : NinjectModule
    {
        public override void Load()
        {
            
            Bind<IServiceInformationCollector>().To<WmiServiceInformationCollector>();
            Bind<IExtension>().To<ExampleExtension>();
            
            Bind<IOutputNormalizer>().To<IniStructureNormalizer>();
          
            Bind<IOutput>().To<ConsoleOutput>();
            
            if (!String.IsNullOrWhiteSpace(RuntimeConstants.OutputFilePath))
            {
                Bind<IConfigFileHandler>().To<ConfigFileSaver>().WithConstructorArgument("ConfigurationOutputPath", RuntimeConstants.OutputFilePath);
                Bind<IOutput>().To<SimpleFileOutput>().WithConstructorArgument("FilePath", RuntimeConstants.OutputFilePath);
            }
        }
    }
}
