using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.QBSDK.Domain;
using QuickBooksAgent.Domain;
using QuickBooksAgent.QBSDK;

namespace QuickBooksAgent.Windows.UI.Synchronize.Basic
{
    public class ItemSynchronizer:Synchronizer<QBResponseItemReader,Item>
    {
        public ItemSynchronizer()
        {
            m_name = "Items";
        }

        protected override void OnProcessRequest(System.Xml.XmlWriter xmlWriter)
        {
            base.OnProcessRequest(xmlWriter);

            xmlWriter.WriteStartElement("ItemQueryRq");
            xmlWriter.WriteAttributeString("requestID", "0");

            if (Configuration.QuickBooks.EntityLastSyncDate.HasValue)
                xmlWriter.WriteElementString("FromModifiedDate", Configuration.QuickBooks.EntityLastSyncDate.Value.ToString("yyyy-MM-ddThh:mm:ss"));            
            
            xmlWriter.WriteEndElement();
        }
    }
}
