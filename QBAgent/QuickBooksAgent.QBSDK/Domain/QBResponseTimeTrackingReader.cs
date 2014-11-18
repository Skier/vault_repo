using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBResponseTimeTrackingReader : QBResponseReader<TimeTracking>
    {
        #region Convert

        protected override TimeTracking Convert(object item)
        {
            TimeTrackingRet timeTrackingRet = (TimeTrackingRet)item;

            TimeTracking timeTracking =
                new TimeTracking(
                    null,                    
                    null,
                    null,
                    new QBEntity(timeTrackingRet.EntityRef.ListId),
                    0,
                    timeTrackingRet.QuickBooksTxnId,
                    timeTrackingRet.EditSequence,
                    timeTrackingRet.TimeCreated,
                    timeTrackingRet.TimeModified,
                    timeTrackingRet.TxnNumber,
                    timeTrackingRet.TxnDate,
                    timeTrackingRet.Rate,
                    timeTrackingRet.Duration,
                    timeTrackingRet.Notes,
                    timeTrackingRet.IsBillable,
                    timeTrackingRet.IsBilled
                    );

            if (timeTrackingRet.CustomerRef != null)
            {
                timeTracking.Customer = new Customer();
                timeTracking.Customer.QuickBooksListId = timeTrackingRet.CustomerRef.ListId;
            }
                

            if (timeTrackingRet.ItemServiceRef != null)
            {
                timeTracking.Item = new Item();
                timeTracking.Item.QuickBooksListId = timeTrackingRet.ItemServiceRef.ListId;
            }
                

            return timeTracking;
        }

        #endregion

        #region ProcessResponse

        protected override void ProcessResponse(QBAffectedObject<TimeTracking> item)
        {
            QBAffectedObject<TimeTracking> expectedTimeTracking = null;
            if (item.CommandType == QBCommandTypeEnum.Add || item.CommandType == QBCommandTypeEnum.Delete)
            {
                try
                {
                    expectedTimeTracking = new QBAffectedObject<TimeTracking>(TimeTracking.FindByPrimaryKey(item.RequestId), item.RequestId);
                }
                catch (DataNotFoundException)
                {
                    throw new QuickBooksAgentException("Expected DB object not found");
                }
            }       
            
            #region Process added TimeTracking

            if (item.CommandType == QBCommandTypeEnum.Add)
            {
                TimeTracking timeTracking = item.DomainObject;

                timeTracking.TimeTrackingId = expectedTimeTracking.DomainObject.TimeTrackingId;
                timeTracking.EntityState = EntityState.Synchronized;
                timeTracking.Customer = expectedTimeTracking.DomainObject.Customer;
                timeTracking.Item = expectedTimeTracking.DomainObject.Item;
                timeTracking.QBEntity = expectedTimeTracking.DomainObject.QBEntity;

                TimeTracking.Update(timeTracking);
            }
            #endregion

            #region Process deleted TimeTracking

            if (item.CommandType == QBCommandTypeEnum.Delete)
            {
                TimeTracking.Delete(expectedTimeTracking.DomainObject);
            }
            #endregion

            #region Process queried TimeTracking

            if (item.CommandType == QBCommandTypeEnum.Query)
            {
                TimeTracking timeTracking = item.DomainObject;
                
                try
                {
                    TimeTracking existingTimeTracking
                        = TimeTracking.FindBy((int)timeTracking.QuickBooksTxnId);

                    if (timeTracking.Customer != null)
                    {
                        timeTracking.Customer
                            = Customer.FindByQuickBooksId((int)timeTracking.Customer.QuickBooksListId);
                    }

                    if (timeTracking.Item != null)
                    {
                        timeTracking.Item =
                            Item.FindByQuickBooksId(timeTracking.Item.QuickBooksListId);
                    }

                    timeTracking.TimeTrackingId = existingTimeTracking.TimeTrackingId;
                    timeTracking.EntityState = EntityState.Synchronized;
                    TimeTracking.Update(timeTracking);
                }
                catch (DataNotFoundException)
                {
                    try
                    {
                        Counter.Assign(timeTracking);

                        timeTracking.EntityState = EntityState.Synchronized;

                        try
                        {
                            QBEntity.FindByPrimaryKey(timeTracking.QBEntity.QBEntityId);
                        }
                        catch (DataNotFoundException)
                        {
                            return;
                        }

                        if (timeTracking.Customer != null)
                        {
                            try
                            {
                                timeTracking.Customer
                                    = Customer.FindByQuickBooksId((int)timeTracking.Customer.QuickBooksListId);
                            }
                            catch (DataNotFoundException)
                            {
                                return;
                            }
                        }

                        if (timeTracking.Item != null)
                        {
                            try
                            {
                                timeTracking.Item
                                    = Item.FindByQuickBooksId(timeTracking.Item.QuickBooksListId);
                            }
                            catch (DataNotFoundException)
                            {
                                return;
                            }
                        }

                        TimeTracking.Insert(timeTracking);
                    }
                    catch (DataNotFoundException ex)
                    {
                        throw ex;
                    }
                }
            }

            #endregion
            
        }

        #endregion

        #region TargetNodeName
        protected override string TargetNodeName
        {
            get { return "TimeTrackingRet"; }
        }
        #endregion

        #region TargetClassType
        protected override Type TargetClassType
        {
            get { return typeof(TimeTrackingRet); }
        }
        #endregion

        #region IsRootNode

        public override bool IsRootNode(string nodeName)
        {
            return "TimeTrackingQueryRs".Equals(nodeName)
                   || "TimeTrackingAddRs".Equals(nodeName);
        }

        #endregion                        
        
        #region TimeTrackingRet

        [XmlRoot("TimeTrackingRet")]
        public class TimeTrackingRet : QBResponseItem
        {            
            #region QuickBooksTxnId

            int m_quickBooksTxnId;
            [XmlElement("TxnID")]
            public int QuickBooksTxnId
            {
                get { return m_quickBooksTxnId; }
                set { m_quickBooksTxnId = value; }
            }

            #endregion            

            #region TxnNumber

            int m_txnNumber;
            [XmlElement("TxnNumber")]
            public int TxnNumber
            {
                get { return m_txnNumber; }
                set { m_txnNumber = value; }
            }

            #endregion

            #region TxnDate

            DateTime m_txnDate;
            [XmlElement("TxnDate")]
            public DateTime TxnDate
            {
                get { return m_txnDate; }
                set { m_txnDate = value; }
            }

            #endregion

            #region EntityRef

            QBResponseItem m_entityRef;
            [XmlElement("EntityRef")]
            public QBResponseItem EntityRef
            {
                get { return m_entityRef; }
                set { m_entityRef = value; }
            }
            
            #endregion

            #region CustomerRef

            QBResponseItem m_customerRef;
            [XmlElement("CustomerRef")]
            public QBResponseItem CustomerRef
            {
                get { return m_customerRef; }
                set { m_customerRef = value; }
            }

            #endregion

            #region ItemServiceRef

            QBResponseItem m_itemServiceRef;
            [XmlElement("ItemServiceRef")]
            public QBResponseItem ItemServiceRef
            {
                get { return m_itemServiceRef; }
                set { m_itemServiceRef = value; }
            }

            #endregion

            #region Rate

            decimal m_rate;
            [XmlElement("Rate")]
            public decimal Rate
            {
                get { return m_rate; }
                set { m_rate = value; }
            }

            #endregion

            #region Duration

            string m_duration;
            [XmlElement("Duration")]
            public string Duration
            {
                get { return m_duration; }
                set { m_duration = value; }
            }

            #endregion

            #region ClassRef

            QBResponseItem m_classRef;
            [XmlElement("ClassRef")]
            public QBResponseItem ClassRef
            {
                get { return m_classRef; }
                set { m_classRef = value; }
            }

            #endregion

            #region Notes

            string m_notes;
            [XmlElement("Notes")]
            public string Notes
            {
                get { return m_notes; }
                set { m_notes = value; }
            }

            #endregion

            #region IsBillable

            bool m_isBillable;
            [XmlElement("IsBillable")]
            public bool IsBillable
            {
                get { return m_isBillable; }
                set { m_isBillable = value; }
            }

            #endregion

            #region IsBilled

            bool m_isBilled;
            [XmlElement("IsBilled")]
            public bool IsBilled
            {
                get { return m_isBilled; }
                set { m_isBilled = value; }
            }

            #endregion
        }

        #endregion
    }
}
