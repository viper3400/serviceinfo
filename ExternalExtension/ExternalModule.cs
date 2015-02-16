using ch.jaxx.WindowsServiceInformation;
using Ninject.Modules;

namespace ExternalExtension
{
    public class ExternalModule : NinjectModule
    {

        public override void Load()
        {
            Bind<IOutput>().To<ExternalOutput>();
            Bind<IServiceInformationCollector>().To<WmiServiceInformationCollector>();
        }
    }
}
