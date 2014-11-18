using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Domain;
using QuickBooksAgent.QBSDK;
using QuickBooksAgent.QBSDK.Domain;
using System.Xml;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Windows.UI.Synchronize.Basic
{
    public class TimeTrackingSynchronizer : Synchronizer<QBResponseTimeTrackingReader,
                                            QBRequestTimeTrackingWriter, TimeTracking>
    {
        public TimeTrackingSynchronizer()
        {
            m_name = "Time Tracking";
        }

        #region OnProcessRequest

        protected override void OnProcessRequest(XmlWriter xmlWriter)
        {
            base.OnProcessRequest(xmlWriter);

            xmlWriter.WriteStartElement("TimeTrackingQueryRq");
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
