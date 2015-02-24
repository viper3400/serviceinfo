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
        public List<OutputModel> Normalize(List<WindowsServiceInfo> ServiceInformationList)
        {

            List<OutputModel> outputList = new List<OutputModel>();
            foreach (var s in ServiceInformationList)
            {
                List<string> outputArray = new List<string>();

                outputArray.Add(String.Format("[{0}]", s.ServiceName));
                outputArray.Add(String.Format("ServiceName={0}", s.ServiceName));
                outputArray.Add(String.Format("ServiceState={0}", s.ServiceState));
                outputArray.Add(String.Format("ServiceDisplayName={0}", s.ServiceDisplayName));
                outputArray.Add(String.Format("ServiceStartupType={0}", s.ServiceStartupType));
                outputArray.Add(String.Format("ExecutablePath={0}", s.ExecutablePath));
                outputArray.Add(String.Format("FileVersion={0}", s.ExecutableFileVersion));

                // check if there is any addional information at all
                if (s.AdditionalInformation != null)
                {
                    foreach (var extraInfo in s.AdditionalInformation)
                    {
                        outputArray.Add(String.Format("{0}={1}", extraInfo.Key, extraInfo.Value));
                    }
                }
                outputArray.Add(Environment.NewLine);

                var outputModel = new OutputModel();
                outputModel.FileName = s.ServiceName;
                outputModel.Content = outputArray.ToArray();
                outputList.Add(outputModel);

            }
            return outputList;
        }
    }
}
