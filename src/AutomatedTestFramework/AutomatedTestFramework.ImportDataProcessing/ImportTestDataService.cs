using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using AutomatedTestFramework.Common.DTOs;
using AutomatedTestFramework.Common.Services;

namespace AutomatedTestFramework.ImportDataProcessing {
    public class ImportTestDataService : IImportTestDataService {

        public IList<AutomaticTest> ParseTestCases(IList<string> paths) {
            
            var result = new List<AutomaticTest>();

            foreach (var path in paths) {
                var serializer = new XmlSerializer(typeof(AutomaticTest));

                var fs = new FileStream(path, FileMode.Open);

                result.Add((AutomaticTest)serializer.Deserialize(fs)); 
            }

            return result;
        }
    }
}