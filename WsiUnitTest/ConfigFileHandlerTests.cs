using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ch.jaxx.WindowsServiceInformation;
using System.IO;

namespace WsiUnitTest
{
    [TestFixture]
    class ConfigFileHandlerTests
    {
        private const string CATEGORY1 = "ConfigFileHandlerTests";
        private string _tempTestPath;
        private string _tempFileOne;
        private string _tempFileTwo;

        [TestFixtureSetUp]
        public void Init()
        {
            _tempTestPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\WsiUnitTest";
            Directory.CreateDirectory(_tempTestPath);
            _tempFileOne = _tempTestPath + @"\testfile.one";
            _tempFileTwo = _tempTestPath + @"\testfile.two";

            string[] dummycontent = new string[] { "DummyLine1", "DummyLine2", "DummyLine3" };

            File.WriteAllLines(_tempFileOne, dummycontent);
            File.WriteAllLines(_tempFileTwo, dummycontent);
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            Directory.Delete(_tempTestPath, true);
        }

        [TestCase]
        [Category(CATEGORY1)]
        public void ConfigSaverTest()
        {
            WmiServiceInformationCollector collector = new WmiServiceInformationCollector();            
            List<WindowsServiceInfo> result = collector.GetServiceInformation("Anwendungserfahrung");

            if (result.FirstOrDefault().ServiceName != "AeLookupSvc")
            {
                Assert.Inconclusive("Missing service on system to execute testcase");
            }

            result.FirstOrDefault().ServiceConfigurationFiles = new List<string>()
            {
                _tempFileOne, _tempFileTwo
            };

            ConfigFileSaver saver = new ConfigFileSaver(_tempTestPath);
            saver.HandleConfigurationFiles(result);

            bool expected = true;
            bool actual = File.Exists(_tempTestPath + @"\AeLookupSvc\" + Path.GetFileName(_tempFileOne));

            Assert.AreEqual(expected, actual, "Testfile 1 not found.");

            actual = File.Exists(_tempTestPath + @"\AeLookupSvc\" + Path.GetFileName(_tempFileTwo));
            Assert.AreEqual(expected, actual, "Testfile 1 not found.");


        }
    }
}
