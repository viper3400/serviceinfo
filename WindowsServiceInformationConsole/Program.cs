using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (!String.IsNullOrWhiteSpace(wssOptions.OutoutFile)) RuntimeConstants.OutputFilePath = wssOptions.OutoutFile;
                DoWork(wssOptions.ServiceFilter);
            }


            

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }

        private static void DoWork(string filter)
        {

            var module = new TestModule();

            string externalExtension = Properties.Settings.Default.ExtensionDLL;
           
            IKernel extensionKernel;
            if (String.IsNullOrWhiteSpace(externalExtension))
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
            IKernel mainKernel = new StandardKernel(module);
            var collector = mainKernel.Get<IServiceInformationCollector>();          
            List<WindowsServiceInfo> services = collector.GetServiceInformation(filter);

            // Try to extend the service information
            try
            {
                var extender = extensionKernel.Get<IExtension>();
                extender.Extend(services);
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
            var normalizer = mainKernel.Get<IOutputNormalizer>();            
            var outputArray = normalizer.Normalize(services);

            // Output the service information
            foreach (var output in mainKernel.GetAll<IOutput>())
            {
                output.WriteOutput(outputArray);
            }
        }
    }
}
