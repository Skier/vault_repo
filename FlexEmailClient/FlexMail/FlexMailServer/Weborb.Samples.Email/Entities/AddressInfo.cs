namespace Weborb.Samples.Email.Entities
{
    public class AddressInfo
    {
        private int m_id;
        private int m_accountId;
        private string m_email;

        public AddressInfo() {
        }

        public AddressInfo(int id, int accountId, string email) {
            m_id = id;
            m_accountId = accountId;
            m_email = email;
        }

        public int Id {
            get { return m_id; }
            set { m_id = value; }
        }

        public int AccountId {
            get { return m_accountId; }
            set { m_accountId = value; }
        }

        public string Email {
            get { return m_email; }
            set { m_email = value; }
        }
    }
}