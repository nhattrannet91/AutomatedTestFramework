using System.Collections.Generic;

namespace AutomatedTestFramework.Common.DTOs.Controls
{
    // Methods:     Cell, MultiSelect, Row, Select, TryUnselectAll
    // Properties:  ListView (Rows, Header, SelectedRows, ScrollBars)
    public abstract class ListView : BaseControl
    {
        #region Properties

        public List<Row> Rows { get; set; }
        public List<Row> SelectedRows { get; set; }
        public ScrollBars ScrollBars { get; set; }

        public Row Header { get; set; }

        #endregion Properties

        #region Methods

        public abstract void Select();

        public abstract void MultiSelect();

        public abstract void TryUnselectAll();

        #endregion Methods
    }
}