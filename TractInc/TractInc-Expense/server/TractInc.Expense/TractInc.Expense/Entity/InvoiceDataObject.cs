using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class InvoiceDataObject
    {

        public int InvoiceId;

        public string InvoiceNumber;

        public int ClientId;

        public string ClientName;

        public string ClientAddress;

        public bool ClientActive;

        public string Status;

        public string StartDate;

        public int TotalDailyAmt;

        public decimal DailyInvoiceAmt;

        public decimal OtherInvoiceAmt;

        public decimal TotalInvoiceAmt;

        public List<InvoiceItemDataObject> InvoiceItems;

        public List<BillItemDataObject> BillItems;

        public List<AssetAssignmentDataObject> Assignments;

        public string Landman;

    }

}
