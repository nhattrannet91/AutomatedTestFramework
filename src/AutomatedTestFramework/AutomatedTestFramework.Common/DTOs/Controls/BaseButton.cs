using AutomatedTestFramework.Common.DTOs.Actions;

namespace AutomatedTestFramework.Common.DTOs.Controls
{
    public abstract class BaseButton : BaseControl
    {

        public const string ControlType = "Button";
        #region Properties

        public abstract bool Enabled { get; set; }
        public abstract string Location { get; set; }
        public abstract string Name { get; set; }
        public abstract bool Isfocused { get; set; }
        public abstract bool Visible { get; set; }

        #endregion Properties

        #region Methods

        public abstract void Click(Action action);

        public abstract void DoubleClick();

        public abstract void Enter();

        #endregion Methods
    }
}