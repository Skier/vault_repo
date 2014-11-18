
using System;
  

namespace Dalworth.Server.Domain
{
    public partial class QbPayment
    {
        public QbPayment()
        {

        }

        #region CustomerRefListId

        private string m_customerRefListId;
        public string CustomerRefListId
        {
            get { return m_customerRefListId; }
            set { m_customerRefListId = value; }
        }

        #endregion
    }
}
      