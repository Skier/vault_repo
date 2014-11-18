using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Dalworth.Controls
{
    [DesignTimeVisible(false)]
    public class MaskedTableCellEditor : MaskedEdit, ITableCellEditor
    {        
        bool m_tableControl;

        #region Constructors

        public MaskedTableCellEditor() { m_tableControl = true; }
        public MaskedTableCellEditor(bool tableControl)
        {
            m_tableControl = tableControl;
        }

        public MaskedTableCellEditor(string inputMask, char inputChar) : this()
        {
            this.InputMask = inputMask;
            this.InputChar = inputChar;
        }

        #endregion        

        #region Editor Properties

        public string InputMask
        {
            get { return this.yInputMask; }
            set { this.yInputMask = value; }
        }

        public char InputChar
        {
            get { return this.wInputChar; }
            set { this.wInputChar = value; }
        }

        #endregion

        #region ITableCellEditor Members

        public System.Windows.Forms.Control getTableCellEditorComponent(Table table,
            object value,
            bool isSelected,
            int row,
            int column)
        {
            try
            {
                this.zValue = value.ToString();
                string temp = this.zValue; //Without that can't catch exception
            }
            catch (Exception)
            {
                Debug.Assert(false, "Cell value doesn't match to cell mask");
                this.zValue = "0";
            }

            return this;
        }


        public Object ExtractControlValue(Table table, int row, int column, Control editorControl)
        {
            Object rv = null;

            if (table.Model.GetColumnClass(column).Equals(int.MaxValue.GetType()))
            {
                int iValue = 0;

                iValue = int.Parse(((MaskedEdit)editorControl).zValue);

                rv = iValue;
            }
            else
                rv = ((MaskedEdit)editorControl).zValue;


            return rv;
        }

        public bool TableControl
        {
            get { return m_tableControl; }
        }

        #endregion

        #region MaskedEdit overriden methods

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up || e.KeyData == Keys.Down)
            {
                OnKeyDownBase(e);
                return;
            }
            
            base.OnKeyDown(e);
        }

        #endregion
        
    }
}
