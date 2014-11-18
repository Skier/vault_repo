using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Windows.UI.Controls;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Employees
{
    public class EmployeesModel : ListTableModel<Employee>, IModel
    {
        #region IModel Members

        public void Init()
        {
            m_list = Employee.Find();
        }

        #endregion

        #region ListTableModel Members

        public override int GetColumnCount()
        {
            return 2;
        }

        public override string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return "Name";
            else
                return "Hired Date";
        }

        public override Type GetColumnClass(int columnIndex)
        {
            return string.Empty.GetType();
        }

        public override object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return GetObject(rowIndex).Name ?? string.Empty;
            else
            {
                if (!GetObject(rowIndex).HiredDate.HasValue)
                    return string.Empty;
                else
                    return GetObject(rowIndex).HiredDate.Value.ToString("yyyy-MM-dd");
            }                
        }

        #endregion
    }
}
