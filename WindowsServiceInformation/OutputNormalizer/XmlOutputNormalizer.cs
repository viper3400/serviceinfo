using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ch.jaxx.WindowsServiceInformation
{
    /// <summary>
    /// Normalization class for xml output
    /// </summary>
    public class XmlOutputNormalizer : IOutputNormalizer
    {
        public List<OutputModel> Normalize(List<WindowsServiceInfo> ServiceInformationList)
        {            
            XmlSerializer serializer = new XmlSerializer(typeof(List<WindowsServiceInfo>));

            // create a MemoryStream here, we are just working
            // exclusively in memory
            System.IO.Stream stream = new System.IO.MemoryStream();

            // The XmlTextWriter takes a stream and encoding
            // as one of its constructors
            System.Xml.XmlTextWriter xtWriter = new System.Xml.XmlTextWriter(stream, Encoding.UTF8);

            serializer.Serialize(xtWriter, ServiceInformationList);

            xtWriter.Flush();

            // go back to the beginning of the Stream to read its contents
            stream.Seek(0, System.IO.SeekOrigin.Begin);

            // read back the contents of the stream and supply the encoding
            System.IO.StreamReader reader = new System.IO.StreamReader(stream, Encoding.UTF8);

            string result = reader.ReadToEnd();
            
            var normalizedOutput = new OutputModel();            
            normalizedOutput.Content = new string[] { result.ToString() };
            return new List<OutputModel>() { normalizedOutput };
            
        }
    }
}
