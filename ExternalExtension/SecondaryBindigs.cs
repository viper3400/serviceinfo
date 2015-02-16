using ch.jaxx.WindowsServiceInformation;
using Ninject.Modules;

namespace ExternalExtension
{
    class SecondaryBindigs : NinjectModule
    {
        public override void Load()
        {
            Bind<IOutput>().To<ConsoleOutput>();
            Bind<IServiceInformationCollector>().To<WmiServiceInformationCollector>();
        }
   
    }
}
