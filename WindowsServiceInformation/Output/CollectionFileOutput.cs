using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace ch.jaxx.WindowsServiceInformation
{
    /// <summary>
    /// Class for writing all content into one single file.
    /// </summary>
    public class CollectionFileOutput : IOutput
    {
        
        private string _outputFile;

        public CollectionFileOutput(string OutputFile)
        {
            _outputFile = OutputFile;
        }

        public void WriteOutput(List<OutputModel> OutputContent)
        {
            var outputList = new List<string>();
            foreach (var outputModel in OutputContent)
            {
                outputList.AddRange(outputModel.Content);
            }

            File.WriteAllLines(_outputFile, outputList.ToArray());
            
        }
    }
}
