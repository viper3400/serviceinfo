using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using ch.jaxx.WindowsServiceInformation;
using System.ServiceProcess;
using Ninject;



using log4net;
using log4net.Config;

namespace WindowsServiceInformationConsole
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);        

        static void Main(string[] args)
        {            
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.xml"));

            Log.Info("Application is starting");
            string invokedVerb = null;
            object invokedVerbInstance = null;

            var wssOptions = new WssOptions();

            if (!CommandLine.Parser.Default.ParseArguments(args, wssOptions,
                (verb, subOptions) =>               
                {
                    // if parsing succeeds the verb name and correct instance
                    // will be passed to onVerbCommand delegate (string,object)
                    invokedVerb = verb;
                    invokedVerbInstance = subOptions;                                        
                    
                }))
             {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
             }

            var commonOptions = (CommonOptions)invokedVerbInstance;

            if (!Net35Support.IsNullOrWhiteSpace(commonOptions.OutputFile)) RuntimeConstants.OutputFilePath = commonOptions.OutputFile;
           
            // Check if config save has been trigger from command line 
            if (!Net35Support.IsNullOrWhiteSpace(commonOptions.ConfigOutputPath))
            {
                //EnsureTrailingBackslash
                RuntimeConstants.ConfigOutputPath = commonOptions.ConfigOutputPath.TrimEnd('\\') + @"\"; ;
                RuntimeConstants.IsConfigOutputEnabled = true;
            }
            else RuntimeConstants.IsConfigOutputEnabled = false;

            RuntimeConstants.ServiceFilter = commonOptions.ServiceFilter;
            DoWork(wssOptions, invokedVerb);

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }

        private static void DoWork(WssOptions wssOptions, string InvokedVerb)
        {
            Ninject.Modules.INinjectModule module;

            switch (InvokedVerb)
            {
                default:
                case "wiki":
                    module = new WikiOutputModule();
                    break;
                case "ini":
                    module = new IniOutputModule();
                    break;
                case "test":
                    module = new TestOutputModule();
                    break;
                case "xml":
                    module = new XmlOutputModule();
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
            Log.Info("Acquiring service information ...");
            IKernel mainKernel = new StandardKernel(module);
            var collector = mainKernel.Get<IServiceInformationCollector>();          
            List<WindowsServiceInfo> services = collector.GetServiceInformation(RuntimeConstants.ServiceFilter);

            // Try to extend the service information
            Log.Info("Try to extend service information ...");
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
                Log.Info("Not possible to extend service information: Extension not bound.");
                /* probably not bound */ 
                string source = "Ninject";
                string messageIExtension = "Error activating IExtension\r\nNo matching bindings are available, and the type is not self-bindable.";
                string messageIConfigFileHandler = "Error activating IConfigFileHandler\r\nNo matching bindings are available, and the type is not self-bindable.";
                if (ex.Source != source && (!ex.Message.Contains(messageIExtension) || !ex.Message.Contains(messageIConfigFileHandler)))
                {
                    // throw any other excpetion
                    throw ex;
                }
            }

            if (RuntimeConstants.IsConfigOutputEnabled)
            {
                // Try to save config files
                Log.Info("Try to save config files ...");
                try
                {
                    // Handle possible config files
                    var configerHandler = mainKernel.Get<IConfigFileHandler>();
                    configerHandler.HandleConfigurationFiles(services);
                }
                catch (ActivationException ex)
                {
                    Log.Info("Not possible to save configuration: ConfigHandler not bound.");
                    /* probably not bound */
                    string source = "Ninject";
                    string messageIExtension = "Error activating IExtension\r\nNo matching bindings are available, and the type is not self-bindable.";
                    string messageIConfigFileHandler = "Error activating IConfigFileHandler\r\nNo matching bindings are available, and the type is not self-bindable.";
                    if (ex.Source != source && (!ex.Message.Contains(messageIExtension) || !ex.Message.Contains(messageIConfigFileHandler)))
                    {
                        // throw any other excpetion
                        throw ex;
                    }
                }
            }



            // Normalize the service information
            Log.Info("Normalize output ...");
            var normalizer = mainKernel.Get<IOutputNormalizer>();            
            var outputArray = normalizer.Normalize(services);

            // Output the service information
            Log.Info("Output ...");
            foreach (var output in mainKernel.GetAll<IOutput>())
            {
                output.WriteOutput(outputArray);
            }
        }
    }
}
