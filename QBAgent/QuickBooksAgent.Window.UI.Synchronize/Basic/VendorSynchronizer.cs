using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.QBSDK;
using QuickBooksAgent.QBSDK.Domain;
using QuickBooksAgent.Domain;
using System.Xml;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Windows.UI.Synchronize.Basic
{
    public class VendorSynchronizer : Synchronizer<QBResponseVendorReader,
                                      QBRequestVendorWriter, Vendor>
    {
        public VendorSynchronizer()
        {
            m_name = "Vendor";
        }

        #region OnProcessRequest

        protected override void OnProcessRequest(XmlWriter xmlWriter)
        {
            base.OnProcessRequest(xmlWriter);

            xmlWriter.WriteStartElement("VendorQueryRq");
            xmlWriter.WriteAttributeString("requestID", "0");

            if (Configuration.QuickBooks.EntityLastSyncDate.HasValue)
                xmlWriter.WriteElementString("FromModifiedDate", Configuration.QuickBooks.EntityLastSyncDate.Value.ToString("yyyy-MM-ddThh:mm:ss"));            
            
            xmlWriter.WriteEndElement();
        }

        #endregion
                
    }
}
