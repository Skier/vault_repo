using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class InvoiceItemDataObject
    {

        public int InvoiceItemId;

        public int InvoiceItemTypeId;

        public int InvoiceId;

        public int BillItemId;

        public int AssetAssignmentId;

        public string InvoiceDate;

        public int Qty;

        public decimal InvoiceRate;

        public string Status;

        public bool IsSelected;

        public bool Deleted;

        public int Dummy;

        public int AssetId;

    }

}
