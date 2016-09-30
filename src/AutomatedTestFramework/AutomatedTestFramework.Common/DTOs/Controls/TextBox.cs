namespace AutomatedTestFramework.Common.DTOs.Controls
{
    public abstract class TextBox : BaseControl
    {
        #region Properties

        public abstract string Text { get; set; }

        public abstract bool IsReadOnly { get; set; }
        public abstract bool BulkText { get; set; }

        #endregion Properties

        #region Methods

        public abstract void Click();

        public abstract void ClickAtCenter();

        public abstract void ClickAtRightEdge();

        public abstract void SetValue(string text);
        public abstract void Enter(string text);

        #endregion Methods
    }
}