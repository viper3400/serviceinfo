using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    public class ExampleExtension : IExtension
    {
        public List<WindowsServiceInformation> Extend(List<WindowsServiceInformation> ServiceList)
        {
           foreach (var s in ServiceList)
           {
               s.AdditionalInformation = new List<string>() { "1st Line: Information added with ExampleExtension", "2nd Line: Information added with ExampleExtension" };
           }

           return ServiceList;
        }
    }
}
