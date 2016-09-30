using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutomatedTestFramework.Common.DTOs {
    public class AutomaticTest {
        public List<Step> CommonSteps { get; set; }

        public List<TestCase> TestCases { get; set; }

        [XmlAttribute]
        public string ApplicationPath { get; set; }

        [XmlAttribute]
        public string WindowTitle { get; set; }
    }
}