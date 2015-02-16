using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    public class ExampleNormalizer : IOutputNormalizer
    {
        public string[] Normalize(List<WindowsServiceInformation> ServiceInformationList)
        {
            List<string> outputList = new List<string>();
            foreach (var s in ServiceInformationList)
            {
                foreach (var p in s.GetType().GetProperties())
                {
                    try
                    {
                        switch (p.GetValue(s).GetType().Name)
                        {
                            case "List`1":
                                List<string> temporaryList = (List<string>)p.GetValue(s);
                                foreach (var line in temporaryList)
                                {
                                    outputList.Add(p.Name  + ": " + line);
                                }
                                break;
                            default:
                                outputList.Add(p.Name + ": " + p.GetValue(s).ToString());
                                break;
                        }
                        
                    }
                    catch (NullReferenceException)
                    {
                        outputList.Add(p.Name + ": No value assigned.");
                    }
                }
                outputList.Add("=====================================");
                outputList.Add("| Normalized with ExampleNormalizer |");
                outputList.Add("=====================================");
            }
            return outputList.ToArray();
        }
    }
}
