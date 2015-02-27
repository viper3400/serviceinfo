using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ch.jaxx.WindowsServiceInformation
{
    public interface IServiceInformationCollector
    {
        List<WindowsServiceInfo> GetServiceInformation(string NameFilter = null);
    }
}
