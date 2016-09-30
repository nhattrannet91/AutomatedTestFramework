using AutomatedTestFramework.Common.DTOs;
using AutomatedTestFramework.Common.DTOs.Controls;

namespace AutomatedTestFramework.Common.Services
{
    public interface IControlService
    {
        #region Methods
        
        BaseWindow GetMainWindow(string applicationPath, string mainWindowTitle);

        BaseControl CatchControl(TestContext context, ControlDescriptor extractControlInfo);

        #endregion Methods
    }
}