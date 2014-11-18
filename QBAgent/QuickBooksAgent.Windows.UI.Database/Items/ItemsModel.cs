using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Items
{
    public class ItemsModel:ListTableModel<Item>, IModel
    {
        #region IModel Members

        public void Init()
        {
            m_list = Item.Find();
        }

        #endregion

        public override int GetColumnCount()
        {
            return 2;
        }

        public override string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return "Name";

            return "Price";
        }

        public override Type GetColumnClass(int columnIndex)
        {
            return String.Empty.GetType();
        }

        public override object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return GetObject(rowIndex).Name;

            return GetObject(rowIndex).SalesPrice.ToString("C");
        }
    }
}
