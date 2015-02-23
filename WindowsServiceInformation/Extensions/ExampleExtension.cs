using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    public class ExampleExtension : IExtension
    {
        public List<WindowsServiceInfo> Extend(List<WindowsServiceInfo> ServiceList)
        {
           foreach (var s in ServiceList)
           {
               s.AdditionalInformation = new List<WindowsServiceExtraInfo>()
               { 
                   new WindowsServiceExtraInfo() { Key = "1st Line", Value = "Information added with ExampleExtension"},
                   new WindowsServiceExtraInfo() { Key = "2nd Line", Value = "Information added with ExampleExtension"}
                   
               };
           }

           return ServiceList;
        }
    }
}
