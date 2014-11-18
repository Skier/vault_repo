using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.QBSDK;

namespace QuickBooksAgent.Windows.UI
{
    public class SessionModel
    {
        private SessionModel() { }

        static SessionModel s_instance;
        public static SessionModel Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new SessionModel();

                return s_instance;
            }
        }


        QBSessionTicket m_sessionTicket;

        public QBSessionTicket SessionTicket
        {
            get { return m_sessionTicket; }
            set { m_sessionTicket = value; }
        }

        QBLoginInfo m_loginInfo = new QBLoginInfo();

        public QBLoginInfo LoginInfo
        {
            get { return m_loginInfo; }
        }

        bool m_skipPasswordRequest;

        public bool SkipPasswordRequest
        {
            get { return m_skipPasswordRequest; }
            set { m_skipPasswordRequest = value; }
        }
    }
}
