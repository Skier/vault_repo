using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBRequestTimeTrackingWriter : QBRequestWriter<TimeTracking>
    {
        #region OnProcess

        protected override void OnProcess(XmlWriter xmlWriter, QBCommandTypeEnum commandType,
                                          List<QBAffectedObject<TimeTracking>> returnList)
        {
            List<TimeTracking> timeTrackingList;

            if (commandType == QBCommandTypeEnum.Add)
                timeTrackingList = TimeTracking.FindBy(EntityState.Created);
            else if (commandType == QBCommandTypeEnum.Delete)
                timeTrackingList = TimeTracking.FindBy(EntityState.Deleted);
            else return;

            foreach (TimeTracking timeTracking in timeTrackingList)
            {
                int requestId = timeTracking.TimeTrackingId;
                    
                if (commandType == QBCommandTypeEnum.Delete)
                {
                    xmlWriter.WriteStartElement("TxnDelRq");
                    xmlWriter.WriteAttributeString("requestID", requestId.ToString());
                    WriteElement(xmlWriter, "TxnDelType", "TimeTracking");
                    WriteElement(xmlWriter, "TxnID", timeTracking.QuickBooksTxnId.Value.ToString());
                    xmlWriter.WriteEndElement();                                        
                } else
                {
                    xmlWriter.WriteStartElement("TimeTrackingAddRq");
                    xmlWriter.WriteAttributeString("requestID", requestId.ToString());
                    xmlWriter.WriteStartElement("TimeTrackingAdd");

                    if (timeTracking.TxnDate != null)
                        WriteElement(xmlWriter, "TxnDate",
                                     timeTracking.TxnDate.Value.ToString("yyyy-MM-dd"));

                    xmlWriter.WriteStartElement("EntityRef");
                    WriteElement(xmlWriter, "ListID", timeTracking.QBEntity.QBEntityId.ToString());
                    xmlWriter.WriteEndElement();

                    if (timeTracking.Customer != null)
                    {
                        xmlWriter.WriteStartElement("CustomerRef");
                        timeTracking.Customer = Customer.FindByPrimaryKey(timeTracking.Customer.CustomerId);
                        WriteElement(xmlWriter, "ListID", timeTracking.Customer.QuickBooksListId.ToString());
                        xmlWriter.WriteEndElement();
                    }

                    if (timeTracking.Item != null)
                    {
                        xmlWriter.WriteStartElement("ItemServiceRef");
                        timeTracking.Item = Item.FindByPrimaryKey(timeTracking.Item.ItemId);
                        WriteElement(xmlWriter, "ListID", timeTracking.Item.QuickBooksListId.ToString());
                        xmlWriter.WriteEndElement();
                    }

                    if (timeTracking.Rate.HasValue)
                        WriteElement(xmlWriter, "Rate", timeTracking.Rate.Value.ToString("0.00", QBDataType.USCulture));
                    WriteElement(xmlWriter, "Duration", timeTracking.Duration);

                    WriteElement(xmlWriter, "Notes", timeTracking.Notes);
                    WriteElement(xmlWriter, "IsBillable", timeTracking.IsBillable.ToString().ToLower());

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();                    
                }                
            }
            
        }

        #endregion
    }
}
