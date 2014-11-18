using System.Collections.Generic;

namespace TractInc.Expense.Domain
{
    public partial class InvoiceDataMapper :_InvoiceDataMapper
    {
              public InvoiceDataMapper()
              {}
              public InvoiceDataMapper(TractIncRAIDDb database):base(database)
              {}

        public Invoice createInvoice(int year, int month, bool isFirstPart, int clientId)
        {
            return createInvoice(year, month, isFirstPart, clientId, 0);
        }

        public Invoice createInvoice(int year, int month, bool isFirstPart, int clientId, int assetId)
        {
            ClientDataMapper clientDM = new ClientDataMapper();
            Client client = clientDM.findByPrimaryKey(clientId);
            
            Invoice invoice = new Invoice();
            invoice.ClientId = client.ClientId;
            invoice.ClientName = client.ClientName;
            invoice.ClientAddress = client.ClientAddress;
            invoice.Status = "NEW";
            invoice.StartDate = "";
            invoice.TotalDailyAmt = 0;
            invoice.DailyInvoiceAmt = 0;
            invoice.OtherInvoiceAmt = 0;
            invoice.TotalInvoiceAmt = 0;
            invoice.relatedInvoiceItem = new List<InvoiceItem>();

            invoice = create(invoice);
            
            BillItemDataMapper billItemDM = new BillItemDataMapper();
            List<BillItem> billItems = billItemDM.getToCreateInvoice(year, month, isFirstPart, clientId, assetId);

            foreach(BillItem billItem in billItems)
            {
                InvoiceItem invoiceItem = new InvoiceItem(billItem);
                invoiceItem.InvoiceId = invoice.InvoiceId;

                invoice.relatedInvoiceItem.Add(invoiceItem);

                decimal amount = invoiceItem.Qty.Value * invoiceItem.InvoiceRate.Value;
                
                if (invoiceItem.InvoiceItemTypeId == 1) {
                    invoice.TotalDailyAmt += (int)invoiceItem.Qty;
                    invoice.DailyInvoiceAmt += amount;
                } else {
                    invoice.OtherInvoiceAmt += amount;
                }

                invoice.TotalInvoiceAmt += amount;
            }

            return save(invoice);
        }
        
    }
}
        