namespace AutomatedTestFramework.Common.DTOs.Controls
{
    public class ControlDescriptor
    {
        #region Fields

        private readonly string m_controlId;
        private readonly string m_controlName;
        private readonly string m_controlType;

        #endregion

        #region Properties

        public string ControlId
        {
            get { return m_controlId; }
        }

        public string ControlName
        {
            get { return m_controlName; }
        }

        public string ControlType
        {
            get { return m_controlType; }
        }

        #endregion

        #region Constructors

        public ControlDescriptor(string controlType, string controlId, string controlName)
        {
            m_controlType = controlType;
            m_controlId = controlId;
            m_controlName = controlName;
        }

        #endregion
    }
}