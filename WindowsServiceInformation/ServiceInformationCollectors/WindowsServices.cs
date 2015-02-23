using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.ServiceProcess;
using System.Management;

namespace ch.jaxx.WindowsServiceInformation
{
    public class WindowsServices : IServiceInformationCollector
    {

        private RegistryKey serviceRootKey;

        public WindowsServices()
        {

            serviceRootKey = Registry.LocalMachine;
            serviceRootKey = serviceRootKey.OpenSubKey("SYSTEM");
            serviceRootKey = serviceRootKey.OpenSubKey("CurrentControlSet");
            serviceRootKey = serviceRootKey.OpenSubKey("services");
        }

        public  List<WindowsServiceInfo> GetServiceInformation (string DisplayNameFilter = null)
        {
            List<WindowsServiceInfo> services = new List<WindowsServiceInfo>();
            foreach (var service in ServiceController.GetServices())
            {
                try
                {
 
                    WindowsServiceInfo serviceInformation = new WindowsServiceInfo();
                    if (DisplayNameFilter == null || (DisplayNameFilter != null && service.DisplayName.ToUpper().Contains(DisplayNameFilter.ToUpper())))
                    {
                        serviceInformation.ServiceName = service.ServiceName;
                        serviceInformation.ServiceDisplayName = service.DisplayName;
                        serviceInformation.ServiceState = service.Status.ToString();
                        serviceInformation.ServiceStartupType =GetServiceStartMode(service.ServiceName);
                        serviceInformation.ServiceUser = GetServiceUser(service.ServiceName);
                        services.Add(serviceInformation);
                    }

                }
                catch (InvalidCastException)
                {
                    // Do nothing
                }
            }
            return services;
        }

        public static string GetServiceStartMode(string serviceName)
        {

            string filter = String.Format("SELECT * FROM Win32_Service WHERE Name = '{0}'", serviceName);

            ManagementObjectSearcher query = new ManagementObjectSearcher(filter);

            // No match = failed condition
            if (query == null) return "<null>";

            try
            {
                ManagementObjectCollection services = query.Get();

                foreach (ManagementObject service in services)
                {
                    return service.GetPropertyValue("StartMode").ToString() == "Auto" ? "Automatic" : "Manual";
                }
            }
            catch (Exception)
            {
                return "<null>";
            }

            return "<null>";
        }

        public static string GetServiceUser(string serviceName)
        {

            string filter = String.Format("SELECT * FROM Win32_Service WHERE Name = '{0}'", serviceName);

            ManagementObjectSearcher query = new ManagementObjectSearcher(filter);

            // No match = failed condition
            if (query == null) return "<null>";

            try
            {
                ManagementObjectCollection services = query.Get();

                foreach (ManagementObject service in services)
                {
                    return service.GetPropertyValue("StartPassword").ToString();
                    //return service.GetPropertyValue("StartName").ToString();
                }
            }
            catch (Exception)
            {
                return "<null>";
            }

            return "<null>";
        }
    }
}
