using System.Collections.Generic;
using System.Xml.Serialization;
using AutomatedTestFramework.Common.DTOs.Actions;

namespace AutomatedTestFramework.Common.DTOs
{
    public class Step
    {
        #region Properties

        public List<Action> Actions { get; set; }

        public List<Parameter> Parameters { get; set; }

        [XmlAttribute]
        public string Id { get; set; }

        #endregion Properties

        #region Methods

        public void Execute(TestContext context)
        {
            Actions.ForEach(action => action.Execute(context));
        }

        #endregion Methods
    }
}