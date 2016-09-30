using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutomatedTestFramework.Common.DTOs {
    public class TestCase {
        [XmlAttribute]
        public string Id { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        public List<Step> Steps { get; set; }

        public void Execute(TestContext context)
        {
            foreach (var step in Steps)
            {
                if (!string.IsNullOrEmpty(step.Id) && context.CommonSteps.ContainsKey(step.Id))
                {
                    // TODO: Pass paramters to common step before we execute it
                    context.CommonSteps[step.Id].Execute(context);
                    continue;
                }

                step.Execute(context);
            }
        }
    }
}