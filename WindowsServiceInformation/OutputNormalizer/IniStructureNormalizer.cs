using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.jaxx.WindowsServiceInformation
{
    /// <summary>
    /// An output normalizer with INI style output
    /// </summary>
    public class IniStructureNormalizer : IOutputNormalizer
    {
        //<inherit/>
        public string[] Normalize(List<WindowsServiceInformation> ServiceInformationList)
        {
            
            List<string> outputList = new List<string>();
            foreach (var s in ServiceInformationList)
            {
                outputList.Add(String.Format("[{0}]", s.ServiceName));
                outputList.Add(String.Format("ServiceName={0}", s.ServiceName));
                outputList.Add(String.Format("ServiceState={0}", s.ServiceState));
                outputList.Add(String.Format("ServiceDisplayName={0}", s.ServiceDisplayName));
                outputList.Add(String.Format("ServiceStartupType={0}", s.ServiceStartupType));
                outputList.Add(String.Format("ExecutablePath={0}", s.ExecutablePath));

                foreach (var extraInfo in s.AdditionalInformation)
                {
                    outputList.Add(String.Format("{0}={1}", extraInfo.Key, extraInfo.Value));
                }
                outputList.Add(Environment.NewLine);

            }
            return outputList.ToArray();
        }
    }
}
