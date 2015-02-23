using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    public interface IServiceInformationCollector
    {
        List<WindowsServiceInfo> GetServiceInformation(string NameFilter = null);
    }
}
