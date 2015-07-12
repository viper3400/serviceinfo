﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using libjfunx.logging;

namespace ch.jaxx.WindowsServiceInformation
{
    /// <summary>
    /// A class to save config files from a given service information list to file system.
    /// </summary>
    public class ConfigFileSaver : IConfigFileHandler
    {
        /// <summary>
        /// Constructor for this class. The output path had to be provided.
        /// </summary>
        /// <param name="ConfigurationOutputPath"></param>
        public ConfigFileSaver(string ConfigurationOutputPath)
        {
            _configOutputPath = ConfigurationOutputPath;
            Logger.Log(LogEintragTyp.Hinweis, "Initialized ConfigFileSaver with target directory: " + _configOutputPath);
            
        }


        private string _configOutputPath;

        /// <summary>
        /// Create a file copy for each configuration file which is passed in the service list. The output path set in the class
        /// constructor is used. For each service a subdirectory will be created, containing the configuration files of 
        /// this service.
        /// </summary>
        /// <param name="Services"></param>
        public void HandleConfigurationFiles(List<WindowsServiceInfo> Services)
        {           
            foreach (var service in Services)
            {
                Logger.Log(LogEintragTyp.Debug, "Handle configuration files for service: " + service.ServiceName);
                // check if there are any configuration files
                if (service.ServiceConfigurationFiles != null)
                {                   
                    foreach (var configFile in service.ServiceConfigurationFiles)
                    {
                        Logger.Log(LogEintragTyp.Debug, "Handle configuration file: " + configFile);
                        try
                        {
                            Directory.CreateDirectory(_configOutputPath + @"\" + service.ServiceName);
                            File.Copy(configFile, _configOutputPath + @"\" + service.ServiceName + @"\" + Path.GetFileName(configFile));
                        }
                        catch (IOException ex)
                        {
                            Logger.Log(LogEintragTyp.Fehler, "Not able to save config file.");
                            Logger.Log(LogEintragTyp.Fehler, ex.Message);
                        }
                    }
                }
            }
        }
    }
}
