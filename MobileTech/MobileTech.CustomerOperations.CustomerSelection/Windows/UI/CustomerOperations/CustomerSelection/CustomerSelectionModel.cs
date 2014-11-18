using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Windows.UI.Controls;
using MobileTech.Domain;

namespace MobileTech.Windows.UI.CustomerOperations.CustomerSelection
{
    internal class CustomerSelectionModel:ListTableModel<RouteScheduleQueue>, IModel
    {
        #region IModel

        public void Init()
        {
            m_list = RouteScheduleQueue.FindCurrent();
        }

        #endregion

        #region ITable

        public override int GetColumnCount()
        {
            return 2;
        }

        public override string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return Resources.Status;

            return Resources.Customer;
        }

        public override Type GetColumnClass(int columnIndex)
        {
            return String.Empty.GetType();
        }

        public override object GetValueAt(int rowIndex, int columnIndex)
        {
            switch (columnIndex)
            {
                case 0:
                    return m_list[rowIndex].Status.ToString();
                case 1:
                    return m_list[rowIndex].Customer.Name;
            }

            return GetValueAtError(rowIndex, columnIndex);
        }

        #endregion
    }
}
