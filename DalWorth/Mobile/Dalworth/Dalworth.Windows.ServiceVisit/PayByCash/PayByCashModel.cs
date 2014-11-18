using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Windows.ServiceVisit.ServiceVisit;

namespace Dalworth.Windows.ServiceVisit.PayByCash
{
    public class PayByCashModel
    {
        #region Amount

        private decimal m_amount;
        public decimal Amount
        {
            get { return m_amount; }
            set { m_amount = value; }
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
