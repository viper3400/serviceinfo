using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ch.jaxx.WindowsServiceInformation
{
    public class ConsoleLogger : ILogger
    {
        public void Debug(string Message)
        {
            Console.WriteLine(Message);
        }

        public void Info(string Message)
        {
            Console.WriteLine(Message);
        }

        public void Warn(string Message)
        {
            Console.WriteLine(Message);
        }

        public void Error(string Message)
        {
            Console.WriteLine(Message);
        }

        public void Fatal(string Message)
        {
            Console.WriteLine(Message);
        }
    }
}
