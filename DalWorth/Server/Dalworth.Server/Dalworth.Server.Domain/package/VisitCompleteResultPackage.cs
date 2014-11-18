using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Server.Domain.package
{
    public enum GeneratedVisitActionEnum
    {
        Created,
        Updated,
        Unknown
    }

    public class VisitCompleteResultPackage
    {
        public VisitCompleteResultPackage() {}

        #region Constructor

        public VisitCompleteResultPackage(PaymentResult paymentResult, Visit newVisit1, Visit newVisit2, 
            GeneratedVisitActionEnum newVisit1Action, GeneratedVisitActionEnum newVisit2Action)
        {
            m_paymentResult = paymentResult;
            m_newVisit1 = newVisit1;
            m_newVisit2 = newVisit2;
            m_newVisit1Action = newVisit1Action;
            m_newVisit2Action = newVisit2Action;
        }

        #endregion

        #region PaymentResult

        private PaymentResult m_paymentResult;
        public PaymentResult PaymentResult
        {
            get { return m_paymentResult; }
            set { m_paymentResult = value; }
        }

        #endregion

        #region NewVisit1

        private Visit m_newVisit1;
        public Visit NewVisit1
        {
            get { return m_newVisit1; }
            set { m_newVisit1 = value; }
        }

        #endregion

        #region NewVisit2

        private Visit m_newVisit2;
        public Visit NewVisit2
        {
            get { return m_newVisit2; }
            set { m_newVisit2 = value; }
        }

        #endregion

        #region NewVisit1Action

        private GeneratedVisitActionEnum m_newVisit1Action;       
        public GeneratedVisitActionEnum NewVisit1Action
        {
            get { return m_newVisit1Action; }
            set { m_newVisit1Action = value; }
        }

        #endregion

        #region NewVisit2Action

        private GeneratedVisitActionEnum m_newVisit2Action;
        public GeneratedVisitActionEnum NewVisit2Action
        {
            get { return m_newVisit2Action; }
            set { m_newVisit2Action = value; }
        }

        #endregion
    }
}
