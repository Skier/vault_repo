using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Controls
{
    public interface IComboboxTableCellEditorModel
    {
        int GetRowCount();

        Object GetValueAt(int rowIndex);

        void SetValueAt(int rowIndex,object oValue);

        bool IsEditable();
    }


    public class ComboboxTableCellEditor : ComboBox, ITableCellEditor
    {
        IComboboxTableCellEditorModel m_model;

        public ComboboxTableCellEditor(IComboboxTableCellEditorModel model)
        {
            m_model = model;

            for(int i = 0; i < model.GetRowCount(); i++)
            {
                Items.Add(model.GetValueAt(i));
            }

            if (m_model.IsEditable())
                this.DropDownStyle = ComboBoxStyle.DropDown;
            else
                this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        #region ITableCellEditor Members

        public Control getTableCellEditorComponent(Table table, object value, bool isSelected, int row, int column)
        {
            foreach (Object itemObject in Items)
            {
                if (value == itemObject)
                {
                    this.SelectedItem = itemObject;
                    break;
                }
            }

            return this;
        }

        public object ExtractControlValue(Table table, int row, int column, Control editorControl)
        {
            return SelectedItem;
        }

        public bool TableControl
        {
            get { return true; }
        }

        #endregion
    }
}
