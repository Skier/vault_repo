using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using QuickBooksAgent.Data;
using QuickBooksAgent.QBSDK.Domain;
using QuickBooksAgent.Domain;
using QuickBooksAgent.QBSDK;

namespace QuickBooksAgent.Windows.UI.Synchronize.Basic
{
    public class TermsSynchronizer:Synchronizer<QBResponseTermsReader,Terms>
    {
        #region Constructor

        public TermsSynchronizer()
        {
            m_name = "Terms";
        }

        #endregion

        #region OnProcessRequest

        protected override void OnProcessRequest(XmlWriter xmlWriter)
        {
            base.OnProcessRequest(xmlWriter);

            xmlWriter.WriteStartElement("StandardTermsQueryRq");
            xmlWriter.WriteAttributeString("requestID", "0");

            if (Configuration.QuickBooks.EntityLastSyncDate.HasValue)
                xmlWriter.WriteElementString("FromModifiedDate", Configuration.QuickBooks.EntityLastSyncDate.Value.ToString("yyyy-MM-ddThh:mm:ss"));            
            
            xmlWriter.WriteEndElement();
        }

        #endregion        
    }
}
