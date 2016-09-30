using System.Collections.Generic;
using System.Linq;
using AutomatedTestFramework.Common.Services;

namespace AutomatedTestFramework.Common.DTOs
{
    public class TestContext
    {
        #region Fields

        private readonly Stack<BaseControl> m_controlStack;
        private readonly IControlService m_controlService;
        private readonly string m_applicationPath;
        private readonly string m_mainWindowTitle;
        private readonly Dictionary<string, Step> m_commonSteps;

        #endregion

        #region Properties

        public IControlService ControlService
        {
            get { return m_controlService; }
        }

        public Stack<BaseControl> ControlStack
        {
            get { return m_controlStack; }
        }

        public Dictionary<string, Step> CommonSteps
        {
            get { return m_commonSteps; }
        }

        public string ApplicationPath
        {
            get { return m_applicationPath; }
        }

        public string MainWindowTitle
        {
            get { return m_mainWindowTitle; }
        }

        #endregion

        #region Constructors

        public TestContext(IControlService controlService, AutomaticTest test)
        {
            m_controlService = controlService;
            m_applicationPath = test.ApplicationPath;
            m_mainWindowTitle = test.WindowTitle;
            m_commonSteps = test.CommonSteps.ToDictionary(step => step.Id);
            m_controlStack = new Stack<BaseControl>();
        }

        #endregion
    }
}
