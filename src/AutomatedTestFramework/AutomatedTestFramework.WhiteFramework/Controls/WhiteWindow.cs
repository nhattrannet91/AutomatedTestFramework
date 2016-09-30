using AutomatedTestFramework.Common.DTOs.Controls;
using White.Core.UIItems;
using White.Core.UIItems.WindowItems;

namespace AutomatedTestFramework.WhiteFramework.Controls
{
    internal class WhiteWindow : BaseWindow, IWhiteControl
    {
        #region Fields

        private Window m_window;

        #endregion Fields

        #region Properties

        public UIItem WhiteItem
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

        public static WhiteWindow Wrap(UIItem item)
        {
            return new WhiteWindow((Window)item);
        }

        #endregion Methods
    }
}