using AutomatedTestFramework.Common.DTOs.Controls;

namespace AutomatedTestFramework.Common.DTOs.Actions
{
    public class CatchWindow : Action
    {
        #region Properties

        public string Title { get; set; }

        #endregion Properties

        #region Methods

        protected override ControlDescriptor ExtractControlInfo()
        {
            return new ControlDescriptor(BaseWindow.ControlType, ControlId, Title);
        }

        #endregion Methods
    }
}