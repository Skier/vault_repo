using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Windows.ServiceVisit.ServiceVisit;

namespace Dalworth.Windows.ServiceVisit.VisitReceipt
{
    public class VisitReceiptModel
    {
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
