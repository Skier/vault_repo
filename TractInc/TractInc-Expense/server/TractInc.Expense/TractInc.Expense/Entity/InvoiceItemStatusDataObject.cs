using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class InvoiceItemStatusDataObject
    {

        public const string INVOICE_ITEM_STATUS_NEW = "NEW";
        public const string INVOICE_ITEM_STATUS_PAID = "PAID";
        public const string INVOICE_ITEM_STATUS_SUBMITTED = "SUBMITTED";
        public const string INVOICE_ITEM_STATUS_VOID = "VOID";

        public string Status;

    }

}
