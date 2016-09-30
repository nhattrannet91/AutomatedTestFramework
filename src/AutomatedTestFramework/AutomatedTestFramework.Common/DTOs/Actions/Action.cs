using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using AutomatedTestFramework.Common.DTOs.Controls;

namespace AutomatedTestFramework.Common.DTOs.Actions
{
    public class Action
    {
        #region Properties

        [XmlAttribute]
        public string Type { get; set; }

        [XmlAttribute]
        public string ControlId { get; set; }

        [XmlAttribute]
        public string ControlName { get; set; }

        [XmlAttribute]
        public string ControlType { get; set; }

        [XmlAttribute]
        public string Params { get; set; }

        [XmlIgnore]
        public Step ParentStep { get; set; }

        public List<Action> Actions { get; set; }

        #endregion Properties

        #region Methods

        public void Execute(TestContext context)
        {
            if (context.ControlStack.IsNullOrEmpty())
            {
                context.ControlStack.Push(context.ControlService
                    .GetMainWindow(context.ApplicationPath, context.MainWindowTitle));
            }
            var mainControl = context.ControlService.CatchControl(context, ExtractControlInfo());
            context.ControlStack.Push(mainControl);
            PerfomAction(context);
            Actions.ForEach(action => action.Execute(context));
            // Use reflecttion get method info based on 
            context.ControlStack.Pop();
        }

        protected virtual ControlDescriptor ExtractControlInfo()
        {
            return new ControlDescriptor(ControlType, ControlId, ControlName);
        }

        private void PerfomAction(TestContext context)
        {
            // TODO Replace condition with ignored list
            if(string.IsNullOrEmpty(Type))
            {
                return;
            }
            var mainControl = context.ControlStack.Peek();
            var methodInfo = mainControl.GetType().GetMethod(Type, BindingFlags.Instance | BindingFlags.Public);
            methodInfo.Invoke(mainControl, new object[] {this});
        }

        #endregion Methods
    }
}