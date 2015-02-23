using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    public class WmiServiceInformationCollector : IServiceInformationCollector
    {
        public List<WindowsServiceInfo> GetServiceInformation(string NameFilter = null)
        {
            List<WindowsServiceInfo> services = new List<WindowsServiceInfo>();
            foreach (var service in ServiceController.GetServices())
            {
                try
                {

                    WindowsServiceInfo serviceInformation = new WindowsServiceInfo();
                    if (NameFilter == null || (NameFilter != null && service.DisplayName.ToUpper().Contains(NameFilter.ToUpper())))
                    {
                        //serviceInformation.RegistryServiceDirectory = service.ServiceName;
                        //serviceInformation.ServiceDisplayName = service.DisplayName;
                        //serviceInformation.ServiceState = service.Status.ToString();


                        //serviceInformation.ServiceStartupType = GetServiceStartMode(service.ServiceName);
                        //serviceInformation.ServiceUser = GetServiceUser(service.ServiceName);
                        services.Add(GetWmiServiceInformation(service.ServiceName));
                    }

                }
                catch (InvalidCastException)
                {
                    // Do nothing
                }
            }
            return services;

        }

        private WindowsServiceInfo GetWmiServiceInformation(string serviceName)
        {
            
            WindowsServiceInfo serviceInformation = new WindowsServiceInfo(); 
            string filter = String.Format("SELECT * FROM Win32_Service WHERE Name = '{0}'", serviceName);

            ManagementObjectSearcher query = new ManagementObjectSearcher(filter);

            if (query != null)
            {

                try
                {
                    ManagementObjectCollection services = query.Get();

                    foreach (ManagementObject service in services)
                    {
                        //inParams["Name"] = svcName;
                        //inParams["DisplayName"] = svcDispName;
                        //inParams["PathName"] = svcPath;
                        //inParams["ServiceType"] = svcType;
                        //inParams["ErrorControl"] = errHandle;
                        //inParams["StartMode"] = svcStartMode.ToString();
                        //inParams["DesktopInteract"] = interactWithDesktop;
                        //inParams["StartName"] = svcStartName;
                        //inParams["StartPassword"] = svcPassword;
                        //inParams["LoadOrderGroup"] = loadOrderGroup;
                        //inParams["LoadOrderGroupDependencies"] = loadOrderGroupDependencies;
                        //inParams["ServiceDependencies"] = svcDependencies;
                        serviceInformation.ServiceName = service.GetPropertyValue("Name").ToString();
                        serviceInformation.ServiceDisplayName = service.GetPropertyValue("DisplayName").ToString();
                        serviceInformation.ExecutablePath = service.GetPropertyValue("PathName").ToString();
                        serviceInformation.ServiceStartupType = service.GetPropertyValue("StartMode").ToString();
                        serviceInformation.ServiceState = service.GetPropertyValue("State").ToString();
                        serviceInformation.ServiceUser = service.GetPropertyValue("StartName").ToString();

                        return serviceInformation;
                    }
                }
                catch (Exception)
                {
                    return serviceInformation;
                }
            }

            return serviceInformation;
        }

    }
}
