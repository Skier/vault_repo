using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain.SyncService;
using Dalworth.Windows.ServiceVisit.ServiceVisit;

namespace Dalworth.Windows.ServiceVisit.PayByCheck
{
    public class PayByCheckModel
    {
        #region Amount

        private decimal m_amount;
        public decimal Amount
        {
            get { return m_amount; }
            set { m_amount = value; }
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

        #region ServiceVisitModel

        private ServiceVisitModel m_serviceVisitModel;
        public ServiceVisitModel ServiceVisitModel
        {
            get { return m_serviceVisitModel; }
            set { m_serviceVisitModel = value; }
        }

        #endregion
    }
}
