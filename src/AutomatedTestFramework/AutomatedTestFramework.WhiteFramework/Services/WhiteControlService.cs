using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Instrumentation;
using AutomatedTestFramework.Common.DTOs;
using AutomatedTestFramework.Common.DTOs.Controls;
using AutomatedTestFramework.Common.Services;
using AutomatedTestFramework.WhiteFramework.Controls;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.UIA3;

namespace AutomatedTestFramework.WhiteFramework.Services
{
    public class WhiteControlService : IControlService
    {
        #region  Static Fields

        private Dictionary<string, Func<TestContext, ControlDescriptor, BaseControl>> s_getControlFunctions;

        #endregion

        #region Constructors

        WhiteControlService()
        {
            s_getControlFunctions = new Dictionary<string, Func<TestContext, ControlDescriptor, BaseControl>>
            {
                {BaseWindow.ControlType, (context, controlDescriptor) => GetAndWrapControl<Window>(context, controlDescriptor, WhiteWindow.Wrap)},
                {BaseButton.ControlType, (context, controlDescriptor) => GetAndWrapControl<Button>(context, controlDescriptor, WhiteButton.Wrap)} 
            };

        }

        #endregion

        #region Methods


        private BaseControl GetAndWrapControl<T>(TestContext context, ControlDescriptor controlDescriptor, Func<T, BaseControl> wrap) where T : UIItem
        {
            if (controlDescriptor.ControlType == BaseWindow.ControlType) {

                App.

                //var application = Application.Attach(Process.GetProcessesByName(context.ProcessName).First());
                //return new WhiteWindow(application.GetWindows().First(w => w.Title == context.MainWindowTitle));

                //var process = Process.GetProcessesByName(context.ProcessName);
                //TestUtils.WaitUntil(() => (process = Process.GetProcessesByName(context.ProcessName)).Length > 0);
                //var application = Application.Launch(context.ApplicationPath);
                //Window window = null;
                //TestUtils.WaitUntil(() => (window = application.GetWindow(context.MainWindowTitle, InitializeOption.NoCache)) != null);
                //return new WhiteWindow(window);
            }

            var whiteControl = context.ControlStack.Peek() as IWhiteControl;
            if (whiteControl == null)
            {
                throw new ArgumentException("The control doesn't have type of WhiteControl");
            }

            var uiItem = GetControl<T>(whiteControl.AutomationElement, controlDescriptor);
            if (uiItem == null)
            {
                throw new InstanceNotFoundException("The control couldn't be found");
            }

            return wrap(uiItem);
        }

        //private static T GetControl<T>(UIItem container, ControlDescriptor controlDescriptor) where T: UIItem
        //{
        //    if (!string.IsNullOrEmpty(controlDescriptor.ControlId))
        //    {
        //        return container.Get<T>(SearchCriteria.ByAutomationId(controlDescriptor.ControlId));
        //    }

        //    if (!string.IsNullOrEmpty(controlDescriptor.ControlName))
        //    {
        //        return container.Get<T>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, controlDescriptor.ControlName));
        //    }

        //    return container.Get<T>(SearchCriteria.ByControlType(typeof(T)));
        //}

        protected Application App { get; private set; }

        protected AutomationBase Automation;

        public BaseWindow GetMainWindow(string applicationPath, string mainWindowTitle)
        {
            ConfigureFramework();

            App = Application.Launch(applicationPath);
            

            Automation = new UIA3Automation();
            return new WhiteWindow(App.GetMainWindow(Automation));
        }

        //public static Application GetMiloApplication()
        //{
        //    Process p = null;

        //    // Dont know why when we run previous testcase success and the mainTitle of the
        //    // MiloApplication is string.Empty --> But if we get it again it ok

        //    var allWordProcess = Process.GetProcessesByName(CommonConstants.PROCESS_NAME);
        //    foreach (var item in allWordProcess)
        //    {
        //        if (item.MainWindowTitle.Contains() ||
        //            item.MainWindowTitle == string.Empty)
        //        {
        //            p = item;
        //            break;
        //        }
        //    }

        //    return p == null ? null : Application.Attach(p);
        //}

        public BaseControl CatchControl(TestContext context, ControlDescriptor controlDescriptor)
        {
            return s_getControlFunctions[controlDescriptor.ControlType].Invoke(context, controlDescriptor);
        }

        private static void ConfigureFramework()
        {
            //CoreAppXmlConfiguration.Instance.BusyTimeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;
            //CoreAppXmlConfiguration.Instance.WaitBasedOnHourGlass = true;
            //TestUtils.DefaultTimeout = (uint)TimeSpan.FromMinutes(2).TotalMilliseconds;
            //TestUtils.SetCultureInfo("en-US");
        }

        #endregion
    }
}