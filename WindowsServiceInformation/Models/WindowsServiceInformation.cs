using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ch.jaxx.WindowsServiceInformation
{
    /// <summary>
    /// Class provides information about a windows service.
    /// </summary>
    public class WindowsServiceInfo
    {
        /// <summary>
        /// Service name as displayed in windows service manager.
        /// </summary>
        public string ServiceDisplayName { get; set; }
        /// <summary>
        /// Service name.
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// Running state of the service.
        /// </summary>
        public string ServiceState { get; set; }
        /// <summary>
        /// Service startup type (manual, automatic)
        /// </summary>
        public string ServiceStartupType { get; set; }
        /// <summary>
        /// Service user as set in service control manager
        /// </summary>
        public string ServiceUser { get; set; }
        /// <summary>
        /// Path to service executable.
        /// </summary>
        public string ExecutablePath { get; set; }
        /// <summary>
        /// A list of valid configuration files for this service instance
        /// </summary>
        public List<string> ServiceConfigurationFiles { get; set; }
        /// <summary>
        /// FileVersion of the service executable.
        /// </summary>
        public string ExecutableFileVersion { get; set; }
        /// <summary>
        /// The name of service host machine
        /// </summary>
        public string ServiceHostName { get; set; }
        /// <summary>
        /// The os version of service host machines
        /// </summary>
        public string ServiceHostOsVersion { get; set; }
        /// <summary>
        /// The timestamp of the service information object
        /// </summary>
        public string ServiceInfoTimeStamp { get; set; }
        /// <summary>
        /// An unmanaged string list with additional information.
        /// </summary>
        public List<WindowsServiceExtraInfo> AdditionalInformation { get; set; }
       
    }
}
