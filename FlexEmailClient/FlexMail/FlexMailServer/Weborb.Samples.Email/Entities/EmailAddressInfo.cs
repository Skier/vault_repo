using System.Net.Mail;

namespace Weborb.Samples.Email.Entities
{
    public class EmailAddressInfo
    {
        #region Fields

        private string m_name;
        private string m_address;

        #endregion

        #region Constructors

        public EmailAddressInfo() {
        }

        public EmailAddressInfo(string address) {
            m_address = address;
        }
        
        public EmailAddressInfo(string name, string address) {
            m_name = name;
            m_address = address;
        }

        public EmailAddressInfo(MailAddress address) {
            m_name = address.DisplayName;
            m_address = address.Address;
        }

        #endregion

        #region Methods

        public MailAddress ToMailAddress() {
            return new MailAddress(m_address, m_name);
        }

        #endregion

        #region Operators

        public static implicit operator EmailAddressInfo(MailAddress address) {
            return new EmailAddressInfo(address);
        }

        public static implicit operator MailAddress(EmailAddressInfo addressInfo) {
            return addressInfo.ToMailAddress();
        }

        #endregion

        #region Properties

        public string Name {
            get { return m_name; }
            set { m_name = value; }
        }

        public string Address {
            get { return m_address; }
            set { m_address = value; }
        }

        public string DisplayValue {
            get {
                string result = string.Empty;
                
                if (null != m_name && m_name.Length > 0) {
                    result = m_name.Trim() + " ";
                }
                
                if (null != m_address && m_address.Length > 0) {
                    if (result.Length > 0) {
                        result += string.Format("<{0}>", m_address.TrimStart());
                    } else {
                        result = m_address.TrimStart();
                    }
                }
                
                return result.TrimEnd();
            }
        }

        #endregion
  
    }
}