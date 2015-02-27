using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ch.jaxx.WindowsServiceInformation
{
    public interface IOutput
    {
        void WriteOutput(List<OutputModel> OutputContent);
    }
}
