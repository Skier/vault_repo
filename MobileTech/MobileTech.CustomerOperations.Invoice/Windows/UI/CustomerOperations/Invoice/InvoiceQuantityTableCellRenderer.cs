using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Windows.UI.Controls;
using MobileTech.Domain;
using System.Drawing;

namespace MobileTech.Windows.UI.CustomerOperations.Invoice
{
    public class InvoiceQuantityTableCellRenderer:DefaultTableCellRenderer
    {
        public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected, bool hasFocus, int row, int column)
        {
            base.getTableCellRendererComponent(
                table,
                value,
                isSelected,
                hasFocus,
                row,
                column);


            CustomerTransactionDetail detail = (CustomerTransactionDetail)
                table.Model.GetObjectAt(row, column);


            if (detail.Quantity > detail.InventoryQuantity)
            {
                ForeColor = Color.Red;
            }


            return this;
        }
    }
}
