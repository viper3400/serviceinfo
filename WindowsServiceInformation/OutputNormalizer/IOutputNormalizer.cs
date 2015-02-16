using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    public interface IOutputNormalizer
    {
        string[] Normalize(List<WindowsServiceInformation> ServiceInformationList);
    }
}
