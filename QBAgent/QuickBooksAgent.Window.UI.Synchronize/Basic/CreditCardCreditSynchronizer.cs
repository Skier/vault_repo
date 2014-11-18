using System;
using System.Xml;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using QuickBooksAgent.QBSDK.Domain;

namespace QuickBooksAgent.Windows.UI.Synchronize.Basic
{
    public class CreditCardCreditSynchronizer : Synchronizer<QBResponseCreditCardCreditReader, 
                                                    QBRequestCreditCardCreditWriter,
                                                    CreditCard>
    {
        #region CreditCardCreditSynchronizer

        public CreditCardCreditSynchronizer()
        {
            m_name = "CC Credit";
        }

        #endregion

        #region OnProcessRequest

        protected override void OnProcessRequest(XmlWriter xmlWriter)
        {
            base.OnProcessRequest(xmlWriter);

            xmlWriter.WriteStartElement("CreditCardCreditQueryRq");
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
