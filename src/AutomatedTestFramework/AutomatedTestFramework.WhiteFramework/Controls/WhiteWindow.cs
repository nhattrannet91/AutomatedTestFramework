using AutomatedTestFramework.Common.DTOs.Controls;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Infrastructure;

namespace AutomatedTestFramework.WhiteFramework.Controls
{
    internal class WhiteWindow : BaseWindow, IWhiteControl
    {
        #region Fields

        private Window m_window;

        #endregion Fields

        #region Properties

        public AutomationElement AutomationElement
        {
            get
            {
                return m_window;
            }
        }

        #endregion Properties

        #region Constructors

        public WhiteWindow(Window window)
        {
            m_window = window;
        }

        #endregion Constructors

        #region Methods

        //public static WhiteWindow Wrap(UIItem item)
        //{
        //    return new WhiteWindow((Window)item);
        //}

        #endregion Methods
    }
}