using ch.jaxx.WindowsServiceInformation;
using System;

namespace ExternalExtension
{
    class ExternalOutput : IOutput
    {
        public void WriteOutput(string[] Content)
        {
            foreach (var s in Content)
            {
                Console.WriteLine("Ex: " + s);
            }
        }
    }
}
