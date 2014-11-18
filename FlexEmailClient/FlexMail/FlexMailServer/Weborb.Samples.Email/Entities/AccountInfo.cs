
namespace Weborb.Samples.Email.Entities
{
    public class AccountInfo
    {
        #region Fields

        private int m_id;
        private int m_pop3SettingsId;
        private int m_smtpSettingsId;

        private string m_email;
        private ServerSettingsInfo m_pop3Settings;
        private ServerSettingsInfo m_smtpSettings;

        #endregion

        #region Constructors

        public AccountInfo() {
        }

        public AccountInfo(int id, string email, int pop3SettingsId, int smtpSettingsId) {
            m_id = id;
            m_email = email;
            m_pop3SettingsId = pop3SettingsId;
            m_smtpSettingsId = smtpSettingsId;
        }

        #endregion

        #region Fields

        internal int Id {
            get { return m_id; }
            set { m_id = value; }
        }

        internal int Pop3SettingsId {
            get { return m_pop3SettingsId; }
            set { m_pop3SettingsId = value; }
        }

        internal int SmtpSettingsId {
            get { return m_smtpSettingsId; }
            set { m_smtpSettingsId = value; }
        }

        public string Email {
            get { return m_email; }
            set { m_email = value; }
        }

        public ServerSettingsInfo Pop3Settings {
            get { return m_pop3Settings; }
            set { m_pop3Settings = value; }
        }

        public ServerSettingsInfo SmtpSettings {
            get { return m_smtpSettings; }
            set { m_smtpSettings = value; }
        }
    }

    #endregion

}