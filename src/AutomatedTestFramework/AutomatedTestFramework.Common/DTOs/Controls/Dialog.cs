namespace AutomatedTestFramework.Common.DTOs.Controls
{
    public class Dialog
    {
        #region Fields

        private readonly string m_title;

        #endregion Fields

        #region Properties

        public string Title
        {
            get { return m_title; }
        }

        #endregion Properties

        #region Constructors

        public Dialog(string title)
        {
            m_title = title;
        }

        #endregion Constructors
    }
}