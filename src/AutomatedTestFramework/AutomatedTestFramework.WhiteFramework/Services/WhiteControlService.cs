using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Instrumentation;
using System.Windows.Automation;
using AutomatedTestFramework.Common.DTOs;
using AutomatedTestFramework.Common.DTOs.Controls;
using AutomatedTestFramework.Common.Services;
using AutomatedTestFramework.WhiteFramework.Controls;
using White.Core;
using White.Core.Configuration;
using White.Core.Factory;
using White.Core.UIItems;
using White.Core.UIItems.Finders;
using White.Core.UIItems.WindowItems;
using White.Core.UIItems.WPFUIItems;

namespace AutomatedTestFramework.WhiteFramework.Services
{
    public class WhiteControlService : IControlService
    {
        #region  Static Fields

        private static Dictionary<string, Func<TestContext, ControlDescriptor, BaseControl>> s_getControlFunctions;

        #endregion

        #region Constructors

        static WhiteControlService()
        {
            s_getControlFunctions = new Dictionary<string, Func<TestContext, ControlDescriptor, BaseControl>>
            {
                {BaseWindow.ControlType, (context, controlDescriptor) => GetAndWrapControl<Window>(context, controlDescriptor, WhiteWindow.Wrap)},
                {BaseButton.ControlType, (context, controlDescriptor) => GetAndWrapControl<Button>(context, controlDescriptor, WhiteButton.Wrap)} 
            };

        }

        #endregion

        #region Methods


        private static BaseControl GetAndWrapControl<T>(TestContext context, ControlDescriptor controlDescriptor, Func<T, BaseControl> wrap) where T : UIItem
        {
            if (controlDescriptor.ControlType == BaseWindow.ControlType)
            {
                // TODO Adapter later
                var application = Application.Attach(Process.GetProcessesByName("ProcessName").First());
                return new WhiteWindow(application.GetWindows().First(w => w.Title == "WindowTitle"));
            }

            var whiteControl = context.ControlStack.Peek() as IWhiteControl;
            if (whiteControl == null)
            {
                throw new ArgumentException("The control doesn't have type of WhiteControl");
            }

            var uiItem = GetControl<T>(whiteControl.WhiteItem, controlDescriptor);
            if (uiItem == null)
            {
                throw new InstanceNotFoundException("The control couldn't be found");
            }

            return wrap(uiItem);
        }

        private static T GetControl<T>(UIItem container, ControlDescriptor controlDescriptor) where T: UIItem
        {
            if (!string.IsNullOrEmpty(controlDescriptor.ControlId))
            {
                return container.Get<T>(SearchCriteria.ByAutomationId(controlDescriptor.ControlId));
            }

            if (!string.IsNullOrEmpty(controlDescriptor.ControlName))
            {
                return container.Get<T>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, controlDescriptor.ControlName));
            }

            return container.Get<T>(SearchCriteria.ByControlType(typeof(T)));
        }

        public BaseWindow GetMainWindow(string applicationPath, string mainWindowTitle)
        {
            ConfigureFramework();

            var application = Application.Launch(applicationPath);
            Window mainWindow = null;
            TestUtils.WaitUntil(() => (mainWindow = application.GetWindow(
               mainWindowTitle, InitializeOption.NoCache)) != null);

            return new WhiteWindow(mainWindow);
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
            CoreAppXmlConfiguration.Instance.BusyTimeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;
            CoreAppXmlConfiguration.Instance.WaitBasedOnHourGlass = true;
            TestUtils.DefaultTimeout = (uint)TimeSpan.FromMinutes(2).TotalMilliseconds;
            TestUtils.SetCultureInfo("en-US");
        }

        #endregion
    }
}