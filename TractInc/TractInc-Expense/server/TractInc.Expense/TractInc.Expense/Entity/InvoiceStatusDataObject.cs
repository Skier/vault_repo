using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class InvoiceStatusDataObject
    {

        public const string INVOICE_STATUS_NEW = "NEW";
        public const string INVOICE_STATUS_PAID = "PAID";
        public const string INVOICE_STATUS_SUBMITTED = "SUBMITTED";
        public const string INVOICE_STATUS_VOID = "VOID";

        public string Status;

    }

}
