using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using QuickBooksAgent.Domain;
using QuickBooksAgent.QBSDK.Domain;
using System.Xml;
using QuickBooksAgent.QBSDK;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Windows.UI.Synchronize.Basic
{
    public class InvoiceSynchronizer : 
        Synchronizer <QBResponseInvoiceReader, QBRequestInvoiceWriter, Invoice>
    {
        #region InvoiceSynchronizer

        public InvoiceSynchronizer()
        {
            m_name = "Invoice";
        }

        #endregion

        #region OnProcessRequest

        protected override void OnProcessRequest(XmlWriter xmlWriter)
        {
            base.OnProcessRequest(xmlWriter);
            
            xmlWriter.WriteStartElement("InvoiceQueryRq");
            xmlWriter.WriteAttributeString("requestID", "0");

            if (Configuration.QuickBooks.TransactionLastSyncDate.HasValue)
            {
                xmlWriter.WriteStartElement("ModifiedDateRangeFilter");
                xmlWriter.WriteElementString("FromModifiedDate", Configuration.QuickBooks.TransactionLastSyncDate.Value.ToString("yyyy-MM-dd"));
                xmlWriter.WriteElementString("ToModifiedDate", Configuration.QuickBooks.CurrentSyncTime.ToString("yyyy-MM-dd"));
                xmlWriter.WriteEndElement();
            }
            
            xmlWriter.WriteEndElement();            
        }

        #endregion
    }
}
