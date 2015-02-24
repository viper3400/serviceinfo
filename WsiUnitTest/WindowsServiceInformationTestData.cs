using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ch.jaxx.WindowsServiceInformation;

namespace WsiUnitTest
{
    internal class WindowsServiceInformationTestData
    {
        internal static List<WindowsServiceInfo> GetStaticTestData()
        {
            throw new NotImplementedException();
            for (int i = 1; i < 6; i++)
            {
                var wsi = new WindowsServiceInfo();
                wsi.ServiceName = "Service#" + i;
                wsi.ServiceDisplayName = "ServiceDisplayName#" + i;
                
            }
        }
    }
}
