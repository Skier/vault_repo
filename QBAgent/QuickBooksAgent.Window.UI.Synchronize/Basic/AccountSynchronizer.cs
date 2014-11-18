using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using QuickBooksAgent.QBSDK;
using QuickBooksAgent.QBSDK.Domain;

namespace QuickBooksAgent.Windows.UI.Synchronize.Basic
{
    public class AccountSynchronizer : Synchronizer<QBResponseAccountReader, Account>
    {
        #region AccountSynchronizer

        public AccountSynchronizer()
        {
            m_name = "Account";
        }

        #endregion

        #region OnProcessRequest

        protected override void OnProcessRequest(XmlWriter xmlWriter)
        {
            base.OnProcessRequest(xmlWriter);

            xmlWriter.WriteStartElement("AccountQueryRq");
            xmlWriter.WriteAttributeString("requestID", "0");

            if (Configuration.QuickBooks.EntityLastSyncDate.HasValue)
                xmlWriter.WriteElementString("FromModifiedDate", Configuration.QuickBooks.EntityLastSyncDate.Value.ToString("yyyy-MM-ddThh:mm:ss"));            
            
            xmlWriter.WriteEndElement();
        }

        #endregion        
    }
}
