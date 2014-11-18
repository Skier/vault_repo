using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain.SyncService;
using Dalworth.Windows.ServiceVisit.PayByCheck;

namespace Dalworth.Windows.ServiceVisit.PayByCheckDone
{
    public class PayByCheckDoneModel
    {
        #region PayByCheckModel

        private PayByCheckModel m_payByCheckModel;
        public PayByCheckModel PayByCheckModel
        {
            get { return m_payByCheckModel; }
            set { m_payByCheckModel = value; }
        }

        #endregion        
    }
}
