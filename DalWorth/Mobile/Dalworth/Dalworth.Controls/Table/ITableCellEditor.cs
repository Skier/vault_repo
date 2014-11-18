using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Controls
{
    public interface ITableCellEditor
    {
        Control getTableCellEditorComponent(Table table, Object value,
                          bool isSelected,
                          int row, int column);




        Object ExtractControlValue(Table table, int row, int column, Control editorControl);

        bool TableControl
        {
            get;
        }
    }
}
