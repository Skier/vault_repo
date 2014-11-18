using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace TractInc.Expense
{

    public class BaseService
    {

        public String GetStorageUrl()
        {
            return Uploader.StorageUrl;
        }

        public String GetUploaderUrl()
        {
            return Uploader.UploaderUrl;
        }

        public String GetGUID()
        {
            return Guid.NewGuid().ToString();
        }

        public String GetInvoicePDFUrl(int invoiceId)
        {
            InvoiceProcessor processor = new InvoiceProcessor();
            return processor.ProcessInvoice(invoiceId);
        }

        public String GetCoverPDFUrl(int invoiceId)
        {
            InvoiceProcessor processor = new InvoiceProcessor();
            return processor.ProcessCover(invoiceId);
        }

        public String GetWorkLogPDFUrl(int invoiceId)
        {
            InvoiceProcessor processor = new InvoiceProcessor();
            return processor.ProcessWorkLog(invoiceId);
        }

        public String GetAttachmentsPDFUrl(int invoiceId)
        {
            InvoiceProcessor processor = new InvoiceProcessor();
            return processor.ProcessAttachments(invoiceId);
        }

    }

}
