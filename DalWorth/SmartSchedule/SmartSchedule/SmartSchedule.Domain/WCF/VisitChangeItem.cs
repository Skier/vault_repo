using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSchedule.Domain.WCF
{
    [DataContract]
    public class VisitChangeItem
    {
        private Visit m_oldVisit;
        private Visit m_newVisit;

        [DataMember]
        public string TicketNumber { get; set; }
        [DataMember]
        public string ChangeDescription { get; set; }

        #region VisitChangeItem

        public VisitChangeItem(Visit oldVisit, Visit newVisit)
        {
            m_oldVisit = oldVisit;
            m_newVisit = newVisit;

            TicketNumber = GetTicketNumber();
            ChangeDescription = GetChangeDescription();
        }

        #endregion

        #region GetTicketNumber

        private string GetTicketNumber()
        {
            if (m_oldVisit == null)
                return m_newVisit.TicketNumber;
            return m_oldVisit.TicketNumber;            
        }

        #endregion

        #region GetChangeDescription

        private string GetChangeDescription()
        {
            if (m_oldVisit == null)
                return "Ticket added";
            if (m_newVisit == null)
                return "Ticket cancelled or rescheduled";

            return "Ticket changes: " + m_newVisit.GetPrintoutChange(m_oldVisit);
        }

        #endregion

        #region Print

        public void Print()
        {
            if (m_newVisit != null)
                m_newVisit.Print();
        }

        #endregion
    }
}
