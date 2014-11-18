using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using QuickBooksAgent.Domain;
using QuickBooksAgent.QBSDK.Domain;

namespace QuickBooksAgent.Windows.UI.Synchronize.Basic
{
    public class CompanySynchronizer : Synchronizer<QBResponseCompanyReader, Company>
    {
        #region CompanySynchronizer

        public CompanySynchronizer()
        {
            m_name = "Company";
        }

        #endregion

        #region OnProcessRequest

        protected override void OnProcessRequest(XmlWriter xmlWriter)
        {
            base.OnProcessRequest(xmlWriter);

            xmlWriter.WriteStartElement("CompanyQueryRq");
            xmlWriter.WriteAttributeString("requestID", "0");
            xmlWriter.WriteEndElement();
        }

        #endregion        
    }
}
