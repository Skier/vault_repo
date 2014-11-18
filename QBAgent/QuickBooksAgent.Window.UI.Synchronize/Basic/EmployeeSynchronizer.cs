using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;
using QuickBooksAgent.QBSDK;
using QuickBooksAgent.QBSDK.Domain;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Windows.UI.Synchronize.Basic
{
    public class EmployeeSynchronizer : Synchronizer<QBResponseEmployeeReader,
                                        QBRequestEmployeeWriter, Employee>
    {

        public EmployeeSynchronizer()
        {
            m_name = "Employee";
        }

        #region OnProcessRequest

        protected override void OnProcessRequest(XmlWriter xmlWriter)
        {
            base.OnProcessRequest(xmlWriter);

            xmlWriter.WriteStartElement("EmployeeQueryRq");
            xmlWriter.WriteAttributeString("requestID", "0");

            if (Configuration.QuickBooks.EntityLastSyncDate.HasValue)
                xmlWriter.WriteElementString("FromModifiedDate", Configuration.QuickBooks.EntityLastSyncDate.Value.ToString("yyyy-MM-ddThh:mm:ss"));            
            
            xmlWriter.WriteEndElement();

            List<Customer> c = new List<Customer>();

            int currentIndex = c.FindIndex(

                delegate(Customer customer) 
                { 
                    return customer == null; 
                });
        }

        #endregion        
    }
}
