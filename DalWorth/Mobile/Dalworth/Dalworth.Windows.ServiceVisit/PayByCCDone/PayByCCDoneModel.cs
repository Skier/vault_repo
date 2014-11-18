using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain.SyncService;
using Dalworth.Windows.ServiceVisit.PayByCC;

namespace Dalworth.Windows.ServiceVisit.PayByCCDone
{
    public class PayByCCDoneModel
    {
        #region PayByCCModel

        private PayByCCModel m_payByCCModel;
        public PayByCCModel PayByCCModel
        {
            get { return m_payByCCModel; }
            set { m_payByCCModel = value; }
        }

        #endregion        
    }
}
