using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ch.jaxx.WindowsServiceInformation
{
    public class XmlOutputNormalizer : IOutputNormalizer
    {
        public List<OutputModel> Normalize(List<WindowsServiceInfo> ServiceInformationList)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(List<WindowsServiceInfo>));
            var subReq = ServiceInformationList;
            StringWriter sww = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);
            xsSubmit.Serialize(writer, subReq);

            var normalizedOutput = new OutputModel();
            normalizedOutput.FileName = "x";
            normalizedOutput.Content = new string[] { sww.ToString() };
            return new List<OutputModel>() { normalizedOutput };
            
        }
    }
}
