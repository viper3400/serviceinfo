using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ch.jaxx.WindowsServiceInformation;

namespace WindowsServiceInformationConsole
{
    class XmlOutputModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<IOutput>().To<ConsoleOutput>();
            if (!Net35Support.IsNullOrWhiteSpace(RuntimeConstants.OutputFilePath))
            {
                Bind<IConfigFileHandler>().To<ConfigFileSaver>().WithConstructorArgument("ConfigurationOutputPath", RuntimeConstants.ConfigOutputPath);
                Bind<IOutput>().To<CollectionFileOutput>().WithConstructorArgument("OutputFile", RuntimeConstants.OutputFilePath);
            }
            Bind<IServiceInformationCollector>().To<WmiServiceInformationCollector>();
            Bind<IOutputNormalizer>().To<XmlOutputNormalizer>();

        }
    }
}
