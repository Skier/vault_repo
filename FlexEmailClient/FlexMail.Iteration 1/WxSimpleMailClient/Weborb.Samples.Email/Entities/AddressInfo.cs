namespace Weborb.Samples.Email.Entities
{
    public class AddressInfo
    {
        private int _id;
        private int _accountId;
        private string _email;

        public AddressInfo() {
        }

        public AddressInfo(int _id, int _accountId, string _email) {
            this._id = _id;
            this._accountId = _accountId;
            this._email = _email;
        }

        public int Id {
            get { return _id; }
            set { _id = value; }
        }

        public int AccountId {
            get { return _accountId; }
            set { _accountId = value; }
        }

        public string Email {
            get { return _email; }
            set { _email = value; }
        }
    }
}