using System;
using System.Collections.Generic;
using System.Text;

namespace QuickBooksAgent.QBSDK
{
    public class QBConnectionTicket
    {
        String m_ticked;

        public QBConnectionTicket(String appCode,String ticket)
        {
            m_ticked = ticket;
            m_appCode = appCode;
        }

        public String Ticket
        {
            get
            {
                return m_ticked;
            }
            set
            {
                m_ticked = value;
            }
        }

        String m_appCode;

        public String AppCode
        {
            get { return m_appCode; }
        }
    }
}
