using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ch.jaxx.WindowsServiceInformation
{
    public static class Net35Support
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value == null) return true;
            return string.IsNullOrEmpty(value.Trim());
        }
    }
}
