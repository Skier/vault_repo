using System.Configuration;
using Dalworth.LeadCentral.SDK;

namespace Dalworth.LeadCentral.Cryptographer
{
    public class Cryptographer
    {
        private const string PasswordKey = "CryptoPassword";

        private static string GetPassphrase()
        {
            var password = ConfigurationManager.AppSettings[PasswordKey];
            
            if (string.IsNullOrEmpty(password))
                password = "unique_pa$$w0rd";
            
            return password;
        }

        public static string Encrypt(string value)
        {
            return Crypto.Encrypt(GetPassphrase(), value);
        }

        public static string Decrypt(string value)
        {
            return Crypto.Decrypt(GetPassphrase(), value);
        }
    }
}
