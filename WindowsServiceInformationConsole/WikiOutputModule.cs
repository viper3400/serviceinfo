using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ch.jaxx.WindowsServiceInformation;

namespace WindowsServiceInformationConsole
{
    class WikiOutputModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOutput>().To<ConsoleOutput>();
            if (!Net35Support.IsNullOrWhiteSpace(RuntimeConstants.OutputFilePath))
            {
                Bind<IOutput>().To<SimpleFileOutput>().WithConstructorArgument("FilePath", RuntimeConstants.OutputFilePath);
            }
            Bind<IServiceInformationCollector>().To<WmiServiceInformationCollector>();            
            Bind<IOutputNormalizer>().To<WikiNormalizer>();

        }
    }
}
