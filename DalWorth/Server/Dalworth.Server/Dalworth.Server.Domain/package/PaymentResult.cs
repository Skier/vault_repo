using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Server.Domain.package
{
    public class PaymentResult : DomainObject
    {
        #region IsAccepted

        private bool m_isAccepted;
        public bool IsAccepted
        {
            get { return m_isAccepted; }
            set { m_isAccepted = value; }
        }

        #endregion

        #region CreditCardNumber

        private string m_creditCardNumber;
        public string CreditCardNumber
        {
            get { return m_creditCardNumber; }
            set { m_creditCardNumber = value; }
        }

        #endregion

        #region BankCheckNumber

        private string m_bankCheckNumber;
        public string BankCheckNumber
        {
            get { return m_bankCheckNumber; }
            set { m_bankCheckNumber = value; }
        }

        #endregion

        #region Amount

        private decimal m_amount;
        public decimal Amount
        {
            get { return m_amount; }
            set { m_amount = value; }
        }

        #endregion

        #region ServerResponse

        private string m_serverResponse;
        public string ServerResponse
        {
            get { return m_serverResponse; }
            set { m_serverResponse = value; }
        }

        #endregion
    }
}
