using System;
using AutomatedTestFramework.Common.DTOs.Controls;
using White.Core.UIItems;
using Action = AutomatedTestFramework.Common.DTOs.Actions.Action;

namespace AutomatedTestFramework.WhiteFramework.Controls
{
    internal class WhiteButton : BaseButton, IWhiteControl
    {
        #region Fields

        private readonly White.Core.UIItems.Button m_whiteButton;

        #endregion

        #region Properties

        public override bool Enabled { get; set; }
        public override string Location { get; set; }
        public override string Name { get; set; }
        public override bool Isfocused { get; set; }
        public override bool Visible { get; set; }

        public UIItem WhiteItem
        {
            get { return m_whiteButton; }
        }

        #endregion

        #region Constructors

        private WhiteButton(Button button)
        {
            m_whiteButton = button;
        }

        #endregion

        #region Methods

        public static WhiteButton Wrap(UIItem whiteItem)
        {
            return new WhiteButton((Button)whiteItem);
        }

        public override void Click(Action action)
        {
            m_whiteButton.Click();
        }

        public override void DoubleClick()
        {
            throw new NotImplementedException();
        }

        public override void Enter()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}