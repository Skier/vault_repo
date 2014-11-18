using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Controls
{
    [DesignTimeVisible(false)]
    public class DigitsTableCellEditor : DigitsEdit, ITableCellEditor
    {
        bool m_tableControl;

        public DigitsTableCellEditor() { m_tableControl = true; }
        public DigitsTableCellEditor(bool tableControl) 
        {
            m_tableControl = tableControl;
        }



        #region ITableCellEditor Members

        public System.Windows.Forms.Control getTableCellEditorComponent(Table table, 
            object value, 
            bool isSelected, 
            int row, 
            int column)
        {
            Text = value.ToString();
            Multiline = true;

            return this;
        }


        public Object ExtractControlValue(Table table, int row, int column, Control editorControl)
        {
            Object rv = null;

            Type type = table.Model.GetColumnClass(column);

            if (type.Equals(int.MaxValue.GetType()))
            {
                int iValue = 0;

                if(editorControl.Text != String.Empty)
                iValue = int.Parse(editorControl.Text);

                rv = iValue;
            }
            else if (type.Equals(decimal.MaxValue.GetType()))
            {
                decimal dValue = 0;

                if (editorControl.Text != String.Empty)
                dValue = decimal.Parse(editorControl.Text);

                rv = dValue;
            }
            else
                rv = editorControl.Text;


            return rv;
        }

        public bool TableControl
        {
            get { return m_tableControl; }
        }
        #endregion
    }
}
