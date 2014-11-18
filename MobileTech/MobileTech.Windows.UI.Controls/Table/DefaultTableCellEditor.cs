using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace MobileTech.Windows.UI.Controls
{
    [DesignTimeVisible(false)]
    public class DefaultTableCellEditor:TextBox,ITableCellEditor
    {

        bool m_tableControl;

        public DefaultTableCellEditor() { m_tableControl = true; }
        public DefaultTableCellEditor(bool tableControl) 
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

            return this;
        }


        public Object ExtractControlValue(Table table, int row, int column, Control editorControl)
        {
            Object rv = null;

            if (table.Model.GetColumnClass(column).Equals(int.MaxValue.GetType()))
            {
                int iValue = 0;

                iValue = int.Parse(editorControl.Text);

                rv = iValue;
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
