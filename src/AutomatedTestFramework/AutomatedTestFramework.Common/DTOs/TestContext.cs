using System;
using System.Collections.Generic;
using System.Linq;
using AutomatedTestFramework.Common.Services;

namespace AutomatedTestFramework.Common.DTOs
{
    public class TestContext
    {
        #region Fields

        private readonly string m_applicationPath;

        #endregion

        #region Properties

        public IControlService ControlService {
            get;
        }

        public Stack<BaseControl> ControlStack {
            get;
        }

        public Dictionary<string, Step> CommonSteps {
            get;
        }

        public string ApplicationPath => m_applicationPath;

        public string MainWindowTitle {
            get;
        }

        public string ProcessName {
            get;
        }

        #endregion

        #region Constructors

        public TestContext(IControlService controlService, AutomaticTest test)
        {
            ControlService = controlService;
            m_applicationPath = test.ApplicationPath;
            var index = m_applicationPath.LastIndexOf(@"\", StringComparison.Ordinal) + 1;
            ProcessName = m_applicationPath.Substring(index, m_applicationPath.Length - index - 4);
            MainWindowTitle = test.WindowTitle;
            CommonSteps = test.CommonSteps.ToDictionary(step => step.Id);
            ControlStack = new Stack<BaseControl>();
        }

        #endregion
    }
}
