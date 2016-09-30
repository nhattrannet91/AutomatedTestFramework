using AutomatedTestFramework.Common.DTOs;
using AutomatedTestFramework.Common.Services;
using AutomatedTestFramework.WhiteFramework.Services;
using System;
using White.Core.Configuration;

namespace AutomatedTestFramework.WhiteFramework
{
    public class WhiteTestService : ITestService
    {
        #region Properties

        //public void PerformTest(AutomaticTest test)
        //{
        //    var context = PrepareTestContext(test);
        //    foreach (var testCase in test.TestCases)
        //    {
        //        testCase.Execute(context);
        //    }
        //    //foreach (var step in test.Steps) {
        //    //    //step.
        //    //}
        //}

        //private WhiteContext PrepareTestContext(AutomaticTest test)
        //{
        //    Window mainWindow = null;
        //    var app = Application.Launch(test.ApplicationPath);
        //    TestUtils.WaitUntil(() => (mainWindow = app.GetWindow(
        //        test.WindowTitle, InitializeOption.NoCache)) != null);

        //    return new WhiteContext(app, mainWindow, test.CommonSteps);
        //}

        public IControlService ControlService
        {
            get
            {
                return new WhiteControlService();
            }
        }

        #endregion Properties

        #region Constructors

        static WhiteTestService()
        {
        }

        #endregion Constructors

        #region Methods

        public TestContext PrepareTestContext(AutomaticTest test)
        {
            return new TestContext(new WhiteControlService(), test);
        }
        
        #endregion Methods
    }
}