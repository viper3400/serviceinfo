using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ch.jaxx.WindowsServiceInformation
{
    public interface ILogger
    {
        void Debug(string Message);
        void Info(string Message);
        void Warn(string Message);
        void Error(string Message);
        void Fatal(string Message);
    }
}
