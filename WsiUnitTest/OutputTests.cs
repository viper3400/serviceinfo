using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using ch.jaxx.WindowsServiceInformation;
using NUnit.Framework;

namespace WsiUnitTest
{
    [TestFixture]
    public class OutputTests
    {
        private string _tempTestPath;
        private const string CATEGORY1 = "OutputTests";

        [TestFixtureSetUp]
        public void Init()
        {
            _tempTestPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\WsiUnitTest";
            Directory.CreateDirectory(_tempTestPath);
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            Directory.Delete(_tempTestPath, true);
        }

        /// <summary>
        /// Simple test for the CollectionFileOutput Class
        /// </summary>
        [TestCase]        
        [Category(CATEGORY1)]
        public void CollectionFileOutputTest()
        {
            string outputTestFile = _tempTestPath + @"\collectionfileoutput.txt";
            var SUT = new CollectionFileOutput(outputTestFile);

            var testInput = new List<OutputModel>();

            testInput.Add(new OutputModel() { FileName = "Doesnt matter", Content = new string[] { "Object One: Line 1", "Object One: Line 2" } });
            testInput.Add(new OutputModel() { FileName = "Doesnt matter", Content = new string[] { "Object Two: Line 1", "Object Two: Line 2" } });

            string[] expected = new string[] 
            {
                "Object One: Line 1", "Object One: Line 2", "Object Two: Line 1", "Object Two: Line 2"
            };
            
            SUT.WriteOutput(testInput);

            string[] actual = File.ReadAllLines(outputTestFile);
            
            Assert.AreEqual(expected, actual);

        }

    }
}
