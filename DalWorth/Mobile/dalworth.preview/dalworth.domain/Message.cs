using System;
using System.Collections.Generic;
using System.Text;

namespace dalworth.domain
{
    public class Message
    {
        public Message(string subject, string body)
        {
            m_subject = subject;
            m_body = body;
        }

        private string m_subject;
        private string m_body;                

        public string Subject
        {
            get { return m_subject; }
            set { m_subject = value; }
        }

        public string Body
        {
            get { return m_body; }
            set { m_body = value; }
        }
    }
}
