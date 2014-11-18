
namespace Weborb.Samples.Email.Entities
{
    public class MailBoxStatus
    {
        #region Fields

        private int m_totalMessages;
        private int m_newMessages;

        #endregion

        #region Constructors

        public MailBoxStatus() {
        }

        public MailBoxStatus(int totalMessages, int newMessages) {
            m_totalMessages = totalMessages;
            m_newMessages = newMessages;
        }

        #endregion

        #region Properties

        public int TotalMessages {
            get { return m_totalMessages; }
            set { m_totalMessages = value; }
        }

        public int NewMessages {
            get { return m_newMessages; }
            set { m_newMessages = value; }
        }

        #endregion
    }
}
