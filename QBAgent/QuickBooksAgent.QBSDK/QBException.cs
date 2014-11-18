using System;
using System.Collections.Generic;
using System.Text;

namespace QuickBooksAgent.QBSDK
{
    public class QBException:QuickBooksAgentException
    {
        public QBException(String message):base(message)
        { }
    }

    public class QBSessionKeyException : QBException
    {
        string m_qbErrorCode;

        public string QBErrorCode
        {
            get { return m_qbErrorCode; }
            set { m_qbErrorCode = value; }
        }

        public QBSessionKeyException(String quickBooksMessage)
            : base(quickBooksMessage.Substring(6))
        {
            m_qbErrorCode = quickBooksMessage.Substring(0, 5);
        }
    }

}
