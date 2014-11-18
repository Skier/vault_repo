using System;
using System.Collections.Generic;
using System.Text;

namespace MobileTech.Windows.UI.Controls
{
    public class DefaultTableHeaderRenderer:DefaultTableCellRenderer
    {
        public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected, bool hasFocus, int row, int column)
        {


            this.BackColor = table.ColumnBackColor;
            this.ForeColor = table.ColumnForeColor;
            this.BorderColor = table.BorderColor;
            this.Font = table.ColumnFont;
            this.StringFormat = table.DefaultStringFormat;

            this.m_value = value;

            return this;
        }
    }
}
