using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    public interface IExtension
    {
        List<WindowsServiceInformation> Extend(List<WindowsServiceInformation> ServiceList);
    }
}
