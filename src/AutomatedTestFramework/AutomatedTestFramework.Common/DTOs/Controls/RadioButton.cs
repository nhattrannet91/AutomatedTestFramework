namespace AutomatedTestFramework.Common.DTOs.Controls
{
    public abstract class RadioButton : BaseControl
    {
        #region Properties

        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public bool Enabled { get; set; }

        #endregion Properties

        #region Methods

        public abstract void Click();

        public abstract void SetValue(bool value);

        #endregion Methods
    }
}