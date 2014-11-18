namespace Weborb.Samples.Email.Entities
{
    public class AccountInfo
    {
        private int _id;
        private string _email;
        private int _pop3SettingsId;
        private int _smtpSettingsId;

        public AccountInfo()
        {
        }

        public AccountInfo(int id, string email, int pop3SettingsId, int smtpSettingsId)
        {
            _id = id;
            _email = email;
            _pop3SettingsId = pop3SettingsId;
            _smtpSettingsId = smtpSettingsId;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public int Pop3SettingsId
        {
            get { return _pop3SettingsId; }
            set { _pop3SettingsId = value; }
        }

        public int SmtpSettingsId
        {
            get { return _smtpSettingsId; }
            set { _smtpSettingsId = value; }
        }
    }
}