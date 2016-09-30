namespace AutomatedTestFramework.Common.DTOs.Controls
{
    public abstract class CheckBox
    {
        #region Properties

        public bool Checked { get; set; }
        public bool IsSelected { get; set; }
        public string Name { get; set; }

        #endregion Properties

        #region Methods

        public abstract void Click();

        public abstract void Select();

        public abstract void SetValue();

        public abstract void UnSelected();

        #endregion Methods
    }
}