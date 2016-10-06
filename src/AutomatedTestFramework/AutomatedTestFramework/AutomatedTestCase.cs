using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using AutomatedTestFramework.WhiteFramework;
using NUnit.Framework;
using System.IO;
using System.Xml.Serialization;
using AutomatedTestFramework.Common.Constants;
using AutomatedTestFramework.Common.DTOs;
using AutomatedTestFramework.Common.DTOs.Actions;
using AutomatedTestFramework.Common.Services;

namespace AutomatedTestFramework.Test {
    [TestFixture]
    public class AutomatedTestCase
    {

        private static XmlAttributeOverrides s_xmlOverrides;

        static AutomatedTestCase()
        {
            s_xmlOverrides = new XmlAttributeOverrides();
            var childActionAttributes = new XmlAttributes();
            childActionAttributes.XmlArrayItems.Add(new XmlArrayItemAttribute("Window", typeof(CatchWindow)));
            childActionAttributes.XmlArrayItems.Add(new XmlArrayItemAttribute("Action", typeof(Action)));
            s_xmlOverrides.Add(typeof(Action), "Actions", childActionAttributes);
            s_xmlOverrides.Add(typeof(Step), "Actions", childActionAttributes);
        }

        private const string TestCaseDirectory = "D:/TestCases";
        [SetUp]
        public void TestSetup() {
        }

        [TearDown]
        public void TestTearDown() {
        }

        [Test()]
        [TestCaseSource("ImportTestData")]
        public void TestCase(AutomaticTest test)
        {
            ITestService service = new WhiteTestService();
            var context = service.PrepareTestContext(test);
            test.TestCases.ForEach(testCase => testCase.Execute(context));
        }

        public static List<AutomaticTest> ImportTestData() {

            var dir = ConfigurationManager.AppSettings[FrameWorkConstants.TEST_CASE_DIRECTORY_KEY];
            var paths = Directory.GetFiles(dir, "*.xml");
            var testCases = new List<AutomaticTest>();
            // TODO: return ImportTestDataService.ParseTestCases(paths);
            //       using Ninject to instantiate ImportTestDataService
            foreach (var path in paths)
            {
                var serializer = new XmlSerializer(typeof(AutomaticTest), s_xmlOverrides);
                using (var fs = File.Open(path, FileMode.Open))
                {
                    testCases.Add((AutomaticTest)serializer.Deserialize(fs));
                }
            }
            return testCases;
        }
    }
}