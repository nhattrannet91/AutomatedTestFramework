namespace AutomatedTestFramework.Common.DTOs.Controls
{
    public abstract class ComboBox : BaseControl
    {
        #region Properties

        public abstract string EditableText { get; set; }

        public abstract ScrollBars ScrollBars { get; set; }
        public abstract bool IsEditable { get; set; }
        public abstract object SelectedItem { get; set; }
        public abstract string SelectedItemText { get; set; }
        public abstract bool Enabled { get; set; }

        #endregion Properties

        #region Methods

        public abstract void Click();

        public abstract void Select();

        #endregion Methods
    }
}