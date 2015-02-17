using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    /// <summary>
    /// Class provides information about a windows service.
    /// </summary>
    public class WindowsServiceInformation
    {
        /// <summary>
        /// Service name as displayed in windows service manager.
        /// </summary>
        public string ServiceDisplayName { get; set; }
        /// <summary>
        /// RegistryPath of service under the main service path in registry.
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
        /// An unmanaged string list with additional information.
        /// </summary>
        public List<string> AdditionalInformation { get; set; }
       
    }
}
