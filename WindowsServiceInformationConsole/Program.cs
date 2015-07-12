using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ch.jaxx.WindowsServiceInformation;
using System.ServiceProcess;
using Ninject;

using libjfunx.logging;
using libjfunx.operating;

namespace WindowsServiceInformationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.SetLogger(new ConsoleLogger());


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
                RuntimeConstants.ConfigOutputPath = FileOperation.EnsureTrailingBackslash(commonOptions.ConfigOutputPath);
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
            Logger.Log(LogEintragTyp.Status,"Acquiring service information ...");
            IKernel mainKernel = new StandardKernel(module);
            var collector = mainKernel.Get<IServiceInformationCollector>();          
            List<WindowsServiceInfo> services = collector.GetServiceInformation(RuntimeConstants.ServiceFilter);

            // Try to extend the service information
            Logger.Log(LogEintragTyp.Status,"Try to extend service information ...");
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
                Logger.Log(LogEintragTyp.Hinweis, "Not possible to extend service information: Extension not bound.");
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
                Logger.Log(LogEintragTyp.Status, "Try to save config files ...");
                try
                {
                    // Handle possible config files
                    var configerHandler = mainKernel.Get<IConfigFileHandler>();
                    configerHandler.HandleConfigurationFiles(services);
                }
                catch (ActivationException ex)
                {
                    Logger.Log(LogEintragTyp.Hinweis, "Not possible to save configuration: ConfigHandler not bound.");
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
            Logger.Log(LogEintragTyp.Hinweis, "Normalize output ...");
            var normalizer = mainKernel.Get<IOutputNormalizer>();            
            var outputArray = normalizer.Normalize(services);

            // Output the service information
            Logger.Log(LogEintragTyp.Hinweis, "Output ...");
            foreach (var output in mainKernel.GetAll<IOutput>())
            {
                output.WriteOutput(outputArray);
            }
        }
    }
}
