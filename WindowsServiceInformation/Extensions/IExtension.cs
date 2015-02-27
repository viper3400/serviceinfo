using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ch.jaxx.WindowsServiceInformation
{
    public interface IExtension
    {
        List<WindowsServiceInfo> Extend(List<WindowsServiceInfo> ServiceList);
    }
}
