using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    /// <summary>
    /// Model for unmanaged information only structured into a key and a value.
    /// </summary>
    public class WindowsServiceExtraInfo
    {
        /// <summary>
        /// A key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// A value
        /// </summary>
        public string Value { get; set; }

    }
}
