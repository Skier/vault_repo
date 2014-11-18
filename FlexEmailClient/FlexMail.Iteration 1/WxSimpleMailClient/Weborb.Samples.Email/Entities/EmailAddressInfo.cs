using System.Net.Mail;

namespace Weborb.Samples.Email.Entities
{
    public class EmailAddressInfo
    {
        private string _name;
        private string _address;

        public EmailAddressInfo() {
        }

        public EmailAddressInfo(string address) {
            _address = address;
        }
        
        public EmailAddressInfo(string name, string address) {
            _name = name;
            _address = address;
        }

        public EmailAddressInfo(MailAddress address) {
            _name = address.DisplayName;
            _address = address.Address;
        }

        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        public string Address {
            get { return _address; }
            set { _address = value; }
        }

        public string DisplayValue {
            get {
                string result = string.Empty;
                
                if (null != _name && _name.Length > 0) {
                    result = _name.Trim() + " ";
                }
                
                if (null != _address && _address.Length > 0) {
                    if (result.Length > 0) {
                        result += string.Format("<{0}>", _address.TrimStart());
                    } else {
                        result = _address.TrimStart();
                    }
                }
                
                return result.TrimEnd();
            }
        }
        
        public MailAddress ToMailAddress() {
            return new MailAddress(_address, _name);
        }

        public static implicit operator EmailAddressInfo(MailAddress address) {
            return new EmailAddressInfo(address);
        }

        public static implicit operator MailAddress(EmailAddressInfo addressInfo) {
            return addressInfo.ToMailAddress();
        }
    }
}