using System;
using System.Collections.Generic;
using System.Text;

namespace QuickBooksAgent.QBSDK
{
    public class QBLoginInfo
    {

        public QBLoginInfo() { }
        public QBLoginInfo(String login, String password)
        {
            m_login = login;
            m_password = password;
        }

        String m_password = String.Empty;

        public String Password
        {
            get { return m_password; }
            set { m_password = value; }
        }

        String m_login = String.Empty;

        public String Login
        {
            get { return m_login; }
            set { m_login = value; }
        }

        public bool IsCorrect
        {
            get
            {
                return m_login != String.Empty
                && m_password != String.Empty;
            }
        }
    }
}
