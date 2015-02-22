using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    public class SimpleFileOutput : IOutput
    {
        private string _filePath;
        public SimpleFileOutput(string FilePath)
        {
            _filePath = FilePath;
        }

        public void WriteOutput(List<OutputModel> OutputContent)
        {
            foreach (var o in OutputContent)
            {
                System.IO.File.WriteAllLines(_filePath + o.FileName + ".txt", o.Content);
            }
        }
    }
}
