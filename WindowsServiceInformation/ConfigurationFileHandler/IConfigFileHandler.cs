using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    /// <summary>
    /// Class for doing some magic with service configuration files
    /// </summary>
    public interface IConfigFileHandler
    {
        void HandleConfigurationFiles(List<WindowsServiceInfo> Services);
    }
}
