using ch.jaxx.WindowsServiceInformation;
using System;
using System.Collections.Generic;

namespace ExternalExtension
{
    class ExternalOutput : IOutput
    {
        public void WriteOutput(List<OutputModel> OutputContent)
        {
            foreach (var o in OutputContent)
            {
                foreach (var s in o.Content)
                {
                    Console.WriteLine("External: " + s);
                }
            }
        }
    }
}
