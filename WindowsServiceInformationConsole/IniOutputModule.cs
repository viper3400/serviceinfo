using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ch.jaxx.WindowsServiceInformation;

namespace WindowsServiceInformationConsole
{
    class IniOutputModule : NinjectModule
    {
        public override void Load()
        {
            
            Bind<IServiceInformationCollector>().To<WmiServiceInformationCollector>();            
            
            Bind<IOutputNormalizer>().To<IniStructureNormalizer>();
          
            Bind<IOutput>().To<ConsoleOutput>();
            
            if (!Net35Support.IsNullOrWhiteSpace(RuntimeConstants.OutputFilePath))
            {
                Bind<IConfigFileHandler>().To<ConfigFileSaver>().WithConstructorArgument("ConfigurationOutputPath", RuntimeConstants.OutputFilePath);
                Bind<IOutput>().To<SimpleFileOutput>().WithConstructorArgument("FilePath", RuntimeConstants.OutputFilePath);
            }
        }
    }
}
