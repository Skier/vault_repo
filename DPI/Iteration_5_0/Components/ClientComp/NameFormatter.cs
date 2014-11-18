using System;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ClientComp
{
    public sealed class NameFormatter
    {
        private const string NAME_FORMAT = "{0} {1}";

        private NameFormatter()
        {
        }

        public static string Format(IAcctInfo acctInfo)
        {
            if (acctInfo == null) {
                throw new ArgumentNullException("acctInfo");
            }

            return Format(acctInfo.FirstName, acctInfo.LastName);
        }

        public static string Format(ICustInfo custInfo)
        {
            if (custInfo == null) {
                throw new ArgumentNullException("custInfo");
            }

            return Format(custInfo.FirstName, custInfo.LastName);
        }

        public static string Format(BankCheck check)
        {
            if (check == null) {
                throw new ArgumentNullException("check");
            }

            return Format(check.FirstName, check.LastName);
        }

        public static string Format(CreditCard creditCard)
        {
            if (creditCard == null) {
                throw new ArgumentNullException("creditCard");
            }

            return Format(creditCard.FirstName, creditCard.LastName);
        }

        public static string Format(string firstName, string lastName)
        {
            string cfname = string.Empty, clname = string.Empty;

            if (firstName != null) {
                cfname = Capitalize(firstName);
            }

            if (lastName != null) {
                clname = Capitalize(lastName);
            }

            return string.Format(NAME_FORMAT, cfname, clname);
        }

        public static string Capitalize(string name)
        {
            string cname = string.Empty;

            if (name != null) {
                cname = name.Trim();
            }

            if (cname.Length < 2) {
                return cname.ToUpper();
            }

            return cname[0].ToString().ToUpper() + cname.Substring(1).ToLower();
        }
    }
}