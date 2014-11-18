using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Windows.UI.Controls;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Accounts
{
    public class AccountsModel : ListTableModel<Account>, IModel
    {
        #region IModel Members

        public void Init()
        {
            m_list = Account.Find();
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

            return "Balance";
        }

        public override Type GetColumnClass(int columnIndex)
        {
            return typeof(string);
        }

        public override object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return GetObject(rowIndex).Name;

            return Account.GetBalance(GetObject(rowIndex)).ToString("C");
        }

        #endregion
    }
}
