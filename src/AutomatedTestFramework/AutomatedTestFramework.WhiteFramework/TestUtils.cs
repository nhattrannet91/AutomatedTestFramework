using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using White.Core;
using White.Core.Factory;
using White.Core.UIItems.WindowItems;

namespace AutomatedTestFramework.WhiteFramework {

    public static class TestUtils {
        #region Types

        /// <summary>
        /// A simpler version of WaitConditionDelegate if no context is needed
        /// </summary>
        public delegate bool SimpleWaitConditionDelegate();

        /// <summary>
        /// Wait condition delegate. The method WaitUntil will wait as long as this delegate returns false. (or the timeout is hit)
        /// </summary>
        /// <typeparam name="T">The generic parameter for context</typeparam>
        /// <param name="context">the context</param>
        public delegate bool WaitConditionDelegate<in T>(T context);

        #endregion Types

        #region Constants

        /// <summary>
        /// The default refresh rate which is used
        /// The unit is milliseconds
        /// </summary>
        public const uint DEFAULT_REFRESH_RATE = 300;

        /// <summary>
        /// Defaut time out
        /// </summary>
        public const uint DEFAULT_TIMEOUT = 120000;

        #endregion Constants

        #region IConstructors

        static TestUtils() {
            DefaultTimeout = DEFAULT_TIMEOUT;
            RefreshRate = DEFAULT_REFRESH_RATE;
        }

        #endregion IConstructors

        #region SMethods

        public static IList<Window> GetOpeningWindowsOfProcess(string processName) {
            Process process = null;
             WaitUntil(
                 () =>
                     (process =
                         Process.GetProcessesByName(processName)
                             .FirstOrDefault(p => !string.IsNullOrEmpty(p.MainWindowTitle))) != null);
            Thread.Sleep(TimeSpan.FromSeconds(CommonConstants.DEFAULT_WAITING_TIME));
            var application = Application.Attach(process);
            return application.GetWindows();
        }

        /// <summary>
        /// Gets the days count in interval.
        /// </summary>
        /// <param name="dt1">The DT1.</param>
        /// <param name="dt2">The DT2.</param>
        /// <returns></returns>
        public static int GetDaysCountInInterval(DateTime dt1, DateTime dt2) {
            var count = 1;
            if (dt1.Date != dt2.Date) {
                var start = dt1.Date;
                var end = dt2.Date;
                if (end < start) {
                    start = dt2.Date;
                    end = dt1.Date;
                }
                while (start < end) {
                    start = start.AddDays(1);
                    count++;
                }
            }
            return count;
        }

        public static Window GetWindowFromApplication(Application app, string windowTitle) {
            Window result = null;
            var start = DateTime.Now;

            while (result == null) {
                if ((DateTime.Now - start).TotalMilliseconds > TimeSpan.FromMinutes(1).TotalMilliseconds) {
                    break;
                }
                try {
                    result = app.GetWindow(windowTitle, InitializeOption.NoCache);
                } catch (Exception) {
                    continue;
                }
            }
            return result;
        }

        public static void KillAllProcessesStartWith(string name) {
            while (true) {
                var processes = Process.GetProcesses();
                var expectedProcess = processes.FirstOrDefault(process => process.ProcessName.StartsWith(name));
                if (expectedProcess == null) {
                    break;
                }

                try {
                    expectedProcess.Kill();
                    expectedProcess.WaitForExit();
                } catch (Exception ex) {

                    // MNT 21.10.2014: Some time method Process.Kill throws exception "Access is denied"
                }
            }
        }

        /// <summary>
        /// Waits the until.
        /// </summary>
        /// <param name="waitCondition">The wait condition.</param>
        public static void WaitUntil(SimpleWaitConditionDelegate waitCondition) {

            // Default Time out is 2'
            WaitUntil(waitCondition, Convert.ToInt32(TimeSpan.FromMinutes(1).TotalMilliseconds));
        }

        /// <summary>
        /// Waits the until.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="waitcondition">The waitcondition.</param>
        /// <param name="context">The context.</param>
        public static void WaitUntil<T>(WaitConditionDelegate<T> waitcondition, T context) {
            WaitUntil(waitcondition, context, -1);
        }

        /// <summary>
        /// Waits the until.
        /// </summary>
        /// <param name="waitcondition">The waitcondition.</param>
        /// <param name="timeout">The timeout.</param>
        public static void WaitUntil(SimpleWaitConditionDelegate waitcondition, int timeout) {
            var realTimeout = timeout < 0 ? DefaultTimeout : (uint)timeout;
            var start = DateTime.Now;

            while ((DateTime.Now - start).TotalMilliseconds < realTimeout) {
                if (waitcondition.Invoke()) {
                    return;
                }
                Thread.Sleep((int)RefreshRate);
            }

            throw new TimeoutException(String.Format("Operation '{0}' Has timed out. Timeout {1}. Operation took: {2}",
                waitcondition.Method.Name,
                realTimeout,
                DateTime.Now - start));
        }

        /// <summary>
        /// Waits the until.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="waitcondition">The waitcondition.</param>
        /// <param name="context">The context.</param>
        /// <param name="timeout">The timeout.</param>
        public static void WaitUntil<T>(WaitConditionDelegate<T> waitcondition, T context, int timeout) {
            var realTimeout = timeout < 0 ? DefaultTimeout : (uint)timeout;
            var start = DateTime.Now;

            while ((DateTime.Now - start).TotalMilliseconds < realTimeout) {
                if (waitcondition.Invoke(context)) {
                    return;
                }
                Thread.Sleep((int)RefreshRate);
            }

            throw new TimeoutException(String.Format("Operation '{0}' Has timed out. Timeout {1}. Operation took: {2}",
                waitcondition.Method.Name,
                realTimeout,
                DateTime.Now - start));
        }

        public static void SetCultureInfo(string culture) {
            var myCltr = new System.Globalization.CultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = myCltr;
            Thread.CurrentThread.CurrentUICulture = myCltr;
        }

        public static void DeleteAllFilesInFolder(string folderPath, params string[] fileExtensions) {
            if (!Directory.Exists(folderPath)) {
                Directory.CreateDirectory(folderPath);
                return;
            }

            var files = Directory.GetFiles(folderPath);
            foreach (var file in files) {
                var fileInfo = new FileInfo(file);
                if (fileExtensions.IsNullOrEmpty() || fileExtensions.Any(ext => ext.ToUpper() == fileInfo.Extension.ToUpper())) {
                    File.Delete(file);
                }
            }
        }

        #endregion SMethods

        #region SProperties

        /// <summary>
        /// Gets or sets the default timeout.
        /// </summary>
        /// <value>
        /// The default timeout.
        /// </value>
        public static uint DefaultTimeout { get; set; }

        /// <summary>
        /// Gets or sets the refresh rate.
        /// </summary>
        /// <value>
        /// The refresh rate.
        /// </value>
        public static uint RefreshRate { get; set; }

        #endregion SProperties
    }
}