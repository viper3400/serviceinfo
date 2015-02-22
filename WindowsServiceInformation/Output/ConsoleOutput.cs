using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    public class ConsoleOutput : IOutput
    {
        public void WriteOutput(List<OutputModel> OutputContent)
        {
            foreach (var o in OutputContent) 
            {
                foreach (var s in o.Content)
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}
