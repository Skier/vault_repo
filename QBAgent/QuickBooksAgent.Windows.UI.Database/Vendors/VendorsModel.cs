using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Vendors
{
    public class VendorsModel : ListTableModel<Vendor>, IModel
    {
        #region IModel Members

        public void Init()
        {
            m_list = Vendor.Find();
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
                return "Balance";
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
                if (!GetObject(rowIndex).Balance.HasValue)
                    return string.Empty;
                else
                    return GetObject(rowIndex).Balance.Value.ToString("C");
            }
                
        }

        #endregion        
    }
}
