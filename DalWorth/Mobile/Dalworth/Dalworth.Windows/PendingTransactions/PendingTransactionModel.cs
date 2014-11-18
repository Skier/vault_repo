using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Controls;
using Dalworth.Domain;

namespace Dalworth.Windows.PendingTransactions
{
    public class PendingTransactionModel : IModel, ITableModel
    {
        private List<WorkTransaction> m_workTransactions;

        #region Init

        public void Init()
        {
            m_workTransactions = WorkTransaction.FindNotSentTransactions();
        }

        #endregion

        #region ITableModel

        public int GetRowCount()
        {
            return m_workTransactions.Count;
        }

        public int GetColumnCount()
        {
            return 3;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return "Visit";
            else if (columnIndex == 1)
                return "Type";
            else
                return "Time";
        }

        public Type GetColumnClass(int columnIndex)
        {
            return string.Empty.GetType();
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
            {
                if (m_workTransactions[rowIndex].VisitId != null)
                    return m_workTransactions[rowIndex].VisitId.Value;
                else
                    return string.Empty;
            }
            else if (columnIndex == 1)
                return WorkTransactionType.GetText(m_workTransactions[rowIndex].WorkTransactionType);
            else
                return m_workTransactions[rowIndex].TransactionDate.Value.ToShortTimeString();
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            throw new NotImplementedException();
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_workTransactions[rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion
    }
}
