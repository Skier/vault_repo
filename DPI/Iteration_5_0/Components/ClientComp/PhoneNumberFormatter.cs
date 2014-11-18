using System.Text;

namespace DPI.ClientComp
{
    public sealed class PhoneNumberFormatter
    {
        private PhoneNumberFormatter()
        {
        }

        public static string Format(string phoneNumber)
        {
            if (phoneNumber == null || phoneNumber.Length < 10) {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("(");
            sb.Append(phoneNumber.Substring(0, 3));
            sb.Append(") ");
            sb.Append(phoneNumber.Substring(3, 3));
            sb.Append("-");
            sb.Append(phoneNumber.Substring(6, 4));

            return sb.ToString();
        }
    }
}