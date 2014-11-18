using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Dalworth.Server.Controls
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

        #region Functions
        [DllImport("coredll.dll")]
        public static extern Boolean SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("coredll.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private void DropDown(bool value)
        {
            SendMessage(this.Handle, 0x14f, value ? -1 : 0, 0);
        }

        private bool IsDropedDown()
        {
            return SendMessage(this.Handle, 0x157, 0, 0);
        }
        #endregion

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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && IsDroped)
            {
                if (this.Items.Count - 1 > this.SelectedIndex)
                    this.SelectedIndex += 1;

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up && IsDroped)
            {        
                if (this.SelectedIndex > 0)                
                    this.SelectedIndex -= 1;

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter && !IsDroped)
            {        
                DropDown(true);

                e.Handled = true;
            }
            else
                base.OnKeyDown(e);
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && IsDroped)
            {
                e.Handled = true;
            }
            else
                base.OnKeyPress(e); 
        }

        protected override void OnLostFocus(EventArgs e)
        {
            DropDown(false);
        }
        public object ExtractControlValue(Table table, int row, int column, Control editorControl)
        {
            return SelectedItem;
        }

        public bool TableControl
        {
            get { return true; }
        }

        public bool IsDroped
        {
            get {
                return false;
            }
        }

        #endregion
    }
}
