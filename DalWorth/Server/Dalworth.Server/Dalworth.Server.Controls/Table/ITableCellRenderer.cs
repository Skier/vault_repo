using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Dalworth.Server.Controls
{
     /// <summary>
    /// This interface defines the method required by any object that
    /// would like to be a renderer for cells in a <code>ITable</code>.
    /// </summary>
    public interface ITableCellRenderer
    {
        DrawControl getTableCellRendererComponent(Table table, Object value,
                            bool isSelected, bool hasFocus,
                            int row, int column);
    }
}
