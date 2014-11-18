using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Explorer
{
    public class ExplorerModel:ITableModel
    {
        List<Object> m_list;


        public void Init(List<Object> list)
        {
            m_list = list;

            if (Change != null)
                Change.Invoke();
        }

        #region ITableModel Members

        public event TableModelChangeHandler Change;

        public Type GetColumnClass(int columnIndex)
        {
            return String.Empty.GetType();
        }

        public int GetColumnCount()
        {
            return 1;
        }

        public string GetColumnName(int columnIndex)
        {
            return "Name";
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_list[rowIndex];
        }

        public int GetRowCount()
        {
            return m_list.Count;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            return m_list[rowIndex].ToString();
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
