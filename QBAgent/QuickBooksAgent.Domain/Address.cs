using System;
using System.Collections.Generic;
using System.Text;

namespace QuickBooksAgent.Domain
{
    public class Address
    {
        public const int MAX_CHARS_PER_LINE = 500;

        #region GetStreetText

        public static string GetStreetText(string addr1, string addr2, string addr3, string addr4)
        {
            string result = string.Empty;

            if (addr1 != null && addr1 != string.Empty)
                result += addr1;

            if (addr2 != null && addr2 != string.Empty)
            {
                if (result == string.Empty)
                    result += addr2;
                else
                    result += "\r\n" + addr2;
            }

            if (addr3 != null && addr3 != string.Empty)
            {
                if (result == string.Empty)
                    result += addr3;
                else
                    result += "\r\n" + addr3;
            }

            if (addr4 != null && addr4 != string.Empty)
            {
                if (result == string.Empty)
                    result += addr4;
                else
                    result += "\r\n" + addr4;
            }


            return result;
        }

        #endregion

        #region GetStreetText

        public static string[] GetStreetText(string street)
        {

            string[] result = new string[4];

            if (street == null || street == string.Empty)
                return result;

            if (street.IndexOf("\r\n\r\n") != -1)
            {
                while (street.IndexOf("\r\n\r\n") != -1)
                    street = street.Replace("\r\n\r\n", "\r\n");
            }

            for (int i = 0; i <= 3; i++)
            {
                if (street == string.Empty)
                    break;

                if (street.IndexOf("\r\n") != -1)
                {
                    result[i] = street.Substring(0, street.IndexOf("\r\n"));
                    street = street.Replace(result[i] + "\r\n", string.Empty);
                }
                else
                {
                    result[i] = street;
                    street = street.Replace(result[i], string.Empty);
                }
            }

            if (street != string.Empty)
                result[3] += " " + street.Replace("\r\n", " ");

            for (int i = 0; i <= 3; i++)
            {
                if (result[i] != null && result[i].Length > MAX_CHARS_PER_LINE)
                    throw new ArgumentOutOfRangeException(i.ToString(), string.Format("Each line cannot have more than {0} chars", MAX_CHARS_PER_LINE));
            }

            return result;
        }

        #endregion
    }
}
