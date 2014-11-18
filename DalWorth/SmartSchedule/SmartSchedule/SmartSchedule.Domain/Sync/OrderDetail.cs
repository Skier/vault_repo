using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSchedule.Domain.Sync
{
    [DataContract]
    public class OrderDetail
    {
        #region Constructor

        public OrderDetail(int servType, int itemNumber, string note, decimal amount)
        {
            m_servType = servType;
            m_itemNumber = itemNumber;
            m_note = note.Trim();
            m_amount = amount;
        }

        #endregion


        #region ServType

        private int m_servType;
        [DataMember]
        public int ServType
        {
            get { return m_servType; }
            set { m_servType = value; }
        }

        #endregion

        #region ItemNumber

        private int m_itemNumber;
        [DataMember]
        public int ItemNumber
        {
            get { return m_itemNumber; }
            set { m_itemNumber = value; }
        }

        #endregion

        #region Note

        private string m_note;
        [DataMember]
        public string Note
        {
            get { return m_note; }
            set { m_note = value; }
        }

        #endregion

        #region Amount

        private decimal m_amount;
        [DataMember]
        public decimal Amount
        {
            get { return m_amount; }
            set { m_amount = value; }
        }

        #endregion

        #region AreOrderDetailsChanged

        public static bool AreOrderDetailsChanged(List<OrderDetail> orderDetails, 
            List<VisitDetail> visitDetails)
        {
            if (visitDetails.Count != orderDetails.Count)
                return true;

            List<OrderDetail> localOrderDetails = new List<OrderDetail>(orderDetails);
            List<VisitDetail> localVisitDetails = new List<VisitDetail>(visitDetails);

            localOrderDetails.Sort(Comparison);
            localVisitDetails.Sort(Comparison);
            
            for (int i = 0; i < localOrderDetails.Count; i++)
            {
                if (!localOrderDetails[i].IsEqual(localVisitDetails[i]))
                    return true;
            }

            return false;
        }

        private static int Comparison(VisitDetail x, VisitDetail y)
        {
            if (x.ServiceId.CompareTo(y.ServiceId) != 0)
                return x.ServiceId.CompareTo(y.ServiceId);
            if (x.ItemSequence.CompareTo(y.ItemSequence) != 0)
                return x.ItemSequence.CompareTo(y.ItemSequence);
            if (x.Note.Trim().CompareTo(y.Note.Trim()) != 0)
                return x.Note.Trim().CompareTo(y.Note.Trim());
            return x.Amount.CompareTo(y.Amount);            
        }

        private static int Comparison(OrderDetail x, OrderDetail y)
        {
            if (x.ServType.CompareTo(y.ServType) != 0)
                return x.ServType.CompareTo(y.ServType);
            if (x.ItemNumber.CompareTo(y.ItemNumber) != 0)
                return x.ItemNumber.CompareTo(y.ItemNumber);
            if (x.Note.Trim().CompareTo(y.Note.Trim()) != 0)
                return x.Note.Trim().CompareTo(y.Note.Trim());
            return x.Amount.CompareTo(y.Amount);
        }

        public bool IsEqual(VisitDetail detail)
        {
            if (ServType != detail.ServiceId)
                return false;
            if (ItemNumber != detail.ItemSequence)
                return false;
            if (Note.Trim() != detail.Note.Trim())
                return false;
            if (Amount != detail.Amount)
                return false;
            return true;            
        }

        #endregion

        #region Convert

        public static List<VisitDetail> Convert(List<OrderDetail> orderDetails)
        {
            List<VisitDetail> result = new List<VisitDetail>();
            foreach (OrderDetail detail in orderDetails)
            {
                result.Add(new VisitDetail(0, 0, detail.ServType, detail.ItemNumber, 
                                           detail.Note.Trim(), detail.Amount));
            }

            return result;
        }

        #endregion

    }
}
