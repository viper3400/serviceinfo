using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    public class ConsoleOutput : IOutput
    {
        public void WriteOutput(string[] Content)
        {
            foreach (var s in Content) 
            {
                Console.WriteLine(s);
            }
        }
    }
}
