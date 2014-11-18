
namespace Weborb.Samples.Email.Entities
{
    public class MailBoxStatus
    {
        private int m_messagesOnServer;
        private int m_newMessages;

        public MailBoxStatus() {
        }

        public MailBoxStatus(int m_messagesOnServer, int m_newMessages) {
            this.m_messagesOnServer = m_messagesOnServer;
            this.m_newMessages = m_newMessages;
        }

        public int MessagesOnServer {
            get { return m_messagesOnServer; }
            set { m_messagesOnServer = value; }
        }

        public int NewMessages {
            get { return m_newMessages; }
            set { m_newMessages = value; }
        }
    }
}
