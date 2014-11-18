using System;

namespace Weborb.Samples.Email.Entities
{
    public class AccountInfo
    {
        [NonSerialized] public int Id;
        [NonSerialized] public int Pop3SettingsId;
        [NonSerialized] public int SmtpSettingsId;

        public string Email;
        
        public AccountInfo() {
        }

        public AccountInfo(int id, string email, int pop3SettingsId, int smtpSettingsId) {
            Id = id;
            Email = email;
            Pop3SettingsId = pop3SettingsId;
            SmtpSettingsId = smtpSettingsId;
        }

    }
}