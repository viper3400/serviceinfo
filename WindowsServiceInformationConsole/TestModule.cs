using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ch.jaxx.WindowsServiceInformation;

namespace WindowsServiceInformationConsole
{
    class TestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOutput>().To<ConsoleOutput>();
            if (!String.IsNullOrWhiteSpace(RuntimeConstants.OutputFilePath))
            {
                Bind<IOutput>().To<SimpleFileOutput>().WithConstructorArgument("FilePath", RuntimeConstants.OutputFilePath);
            }
            Bind<IServiceInformationCollector>().To<WmiServiceInformationCollector>();
            Bind<IExtension>().To<ExampleExtension>();
            Bind<IOutputNormalizer>().To<WikiNormalizer>();

        }
    }
}
