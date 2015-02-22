using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    /// <summary>
    /// An example implementation of IOutputNormalizer 
    /// </summary>
    public class ExampleNormalizer : IOutputNormalizer
    {
        //<inherit />
        public List<OutputModel> Normalize(List<WindowsServiceInformation> ServiceInformationList)
        {
            List<OutputModel> outputList = new List<OutputModel>();
            foreach (var s in ServiceInformationList)
            {
                List<string> outputArray = new List<string>();
                foreach (var p in s.GetType().GetProperties())
                {
                    try
                    {
                        switch (p.GetValue(s).GetType().Name)
                        {
                            case "List`1":
                                var temporaryList = (List<WindowsServiceExtraInfo>)p.GetValue(s);
                                foreach (var line in temporaryList)
                                {
                                    outputArray.Add(line.Key + ": " + line.Value);
                                }
                                break;
                            default:
                                outputArray.Add(p.Name + ": " + p.GetValue(s).ToString());
                                break;
                        }
                        
                    }
                    catch (NullReferenceException)
                    {
                        outputArray.Add(p.Name + ": No value assigned.");
                    }
                }
                outputArray.Add("=====================================");
                outputArray.Add("| Normalized with ExampleNormalizer |");
                outputArray.Add("=====================================");

                var outputModel = new OutputModel();
                outputModel.FileName = s.ServiceName;
                outputModel.Content = outputArray.ToArray();
                outputList.Add(outputModel);
            }
            return outputList;
        }
    }
}
