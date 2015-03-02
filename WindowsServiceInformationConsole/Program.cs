using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ch.jaxx.WindowsServiceInformation;
using System.ServiceProcess;
using Ninject;


namespace WindowsServiceInformationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
           
            var wssOptions = new WssOptions();
            if (CommandLine.Parser.Default.ParseArguments(args,wssOptions))
            {
                if (!Net35Support.IsNullOrWhiteSpace(wssOptions.OutputFile)) RuntimeConstants.OutputFilePath = wssOptions.OutputFile;
                DoWork(wssOptions.ServiceFilter, wssOptions.ModuleType);
            }


            

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }

        private static void DoWork(string filter, ModuleType mType)
        {
            Ninject.Modules.INinjectModule module;

            switch (mType)
            {
                default:
                case ModuleType.WIKI:
                    module = new WikiOutputModule();
                    break;
                case ModuleType.INI:
                    module = new IniOutputModule();
                    break;
                case ModuleType.TEST:
                    module = new TestOutputModule();
                    break;
            }
           
            string externalExtension = Properties.Settings.Default.ExtensionDLL;
           
            IKernel extensionKernel;
            if (Net35Support.IsNullOrWhiteSpace(externalExtension))
            {

                extensionKernel = new StandardKernel(module);
            }
            else
            {
                extensionKernel = new StandardKernel();
                extensionKernel.Load(externalExtension);
                //TODO: handle exceptions
            }


            // Collect all service information
            Console.WriteLine("Acquiring service information ...");
            IKernel mainKernel = new StandardKernel(module);
            var collector = mainKernel.Get<IServiceInformationCollector>();          
            List<WindowsServiceInfo> services = collector.GetServiceInformation(filter);

            // Try to extend the service information
            Console.WriteLine("Try to extend service information ...");
            try
            {
                var extender = extensionKernel.Get<IExtension>();
                extender.Extend(services);

                // Handle possible config files
                var configerHandler = mainKernel.Get<IConfigFileHandler>();
                configerHandler.HandleConfigurationFiles(services);
            }
            catch (ActivationException ex) 
            {                
                /* probably not bound */ 
                string source = "Ninject";
                string message = "Error activating IExtension\r\nNo matching bindings are available, and the type is not self-bindable.";
                if (ex.Source != source || !ex.Message.Contains(message))
                {
                    // throw any other excpetion
                    throw ex;
                }
            }

            // Normalize the service information
            Console.WriteLine("Normalize output ...");
            var normalizer = mainKernel.Get<IOutputNormalizer>();            
            var outputArray = normalizer.Normalize(services);

            // Output the service information
            Console.WriteLine("Output ...");
            foreach (var output in mainKernel.GetAll<IOutput>())
            {
                output.WriteOutput(outputArray);
            }
        }
    }
}
