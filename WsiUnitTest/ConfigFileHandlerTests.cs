using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        /// <summary>
        /// Test case for ConfigFileSaver class
        /// </summary>
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

            // test the case if there is no configuration available, this should be possible every time.

            ConfigFileSaver saver = new ConfigFileSaver(_tempTestPath);
            saver.HandleConfigurationFiles(result);

            // now check with available config information (the dedicated use case)
            result.FirstOrDefault().ServiceConfigurationFiles = new List<string>()
            {
                _tempFileOne, _tempFileTwo
            };


            saver.HandleConfigurationFiles(result);

            bool expected = true;
            bool actual = File.Exists(_tempTestPath + @"\AeLookupSvc\" + Path.GetFileName(_tempFileOne));

            Assert.AreEqual(expected, actual, "Testfile 1 not found.");

            actual = File.Exists(_tempTestPath + @"\AeLookupSvc\" + Path.GetFileName(_tempFileTwo));
            Assert.AreEqual(expected, actual, "Testfile 1 not found.");


        }
    }
}
