using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    /// <summary>
    /// Exposes a method to normalize output format to a string[].
    /// </summary>
    public interface IOutputNormalizer
    {
        /// <summary>
        /// Normalizes a given List with WindowsServiceInfo object to a string array in preparation for a normalized data output.
        /// </summary>
        /// <param name="ServiceInformationList">The unnormalized list of WindowsServiceInfo objects.</param>
        /// <returns>A normalized string[] ready for data output.</returns>
        List<OutputModel> Normalize(List<WindowsServiceInfo> ServiceInformationList);
    }
}
