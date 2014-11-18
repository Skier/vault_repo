using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Server.Controls
{
    public class DefaultTableHeaderRenderer:DefaultTableCellRenderer
    {
        public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected, bool hasFocus, int row, int column)
        {
            if (table.LeftHeader && row == -1 && column == 0)
            {
                this.BackColor = table.BackColor;
                this.ForeColor = table.BackColor;
                this.BorderColor = table.BackColor;
                this.Font = table.ColumnFont;
                this.StringFormat = table.DefaultStringFormat;
                this.m_value = String.Empty;
            }
            else
            {
                this.BackColor = table.ColumnBackColor;
                this.ForeColor = table.ColumnForeColor;
                this.BorderColor = table.BorderColor;
                this.Font = table.ColumnFont;
                this.StringFormat = table.DefaultStringFormat;

                this.m_value = value;
            }

            return this;
        }
    }
}
