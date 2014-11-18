using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace QuickBooksAgent.Data
{
    public class QBDataType
    {
        /// <summary>
        /// Returns true if value belongs to specified format, otherwise false.
        /// IsInFormat(2.3456, 10, 2) returns false
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="maxIntPart">Max integer part length (digits count)</param>
        /// <param name="maxDecimalPart">Max decimal part length (digits count)</param>
        /// <returns></returns>
        public static bool IsInFormat(decimal value, int maxIntPart, int maxDecimalPart)
        {
            string s = value.ToString(QBDataType.USCulture);

            int intDigits = 0;
            int decimalDigits = 0;
            bool isCalculatingInt = true;

            foreach (char c in s)
            {
                if (isCalculatingInt && char.IsDigit(c))
                    intDigits++;
                else if (c == '.')
                    isCalculatingInt = false;
                else if (!isCalculatingInt && char.IsDigit(c))
                    decimalDigits++;
            }

            if (intDigits > maxIntPart)
                return false;

            if (decimalDigits > maxDecimalPart)
                return false;

            return true;
        }

        public static bool IsInFormat(decimal? value, int maxIntPart, int maxDecimalPart)
        {
            if (value == null)
                return true;
            else
                return IsInFormat(value.Value, maxIntPart, maxDecimalPart);
        }
        
        /// <summary>
        /// Formats specified decimal value to string, removing unused decimal zeros from the right
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RoundTripFormat(decimal value, CultureInfo culture)
        {
            string s = value.ToString(culture);

            if (value.ToString(QBDataType.USCulture).IndexOf(".") != -1) //Contains decimal part
            {
                s = s.TrimEnd("0".ToCharArray());
                if (s.EndsWith("."))
                    s = s.TrimEnd(".".ToCharArray());
            }

            return s;            
        }

        public static string RoundTripFormat(decimal value)
        {
            return RoundTripFormat(value, CultureInfo.CurrentCulture);
        }
        
        public static string RoundTripFormat(decimal? value)
        {
            if (value == null)
                return string.Empty;
            else
                return RoundTripFormat(value.Value);
        }

        public static string RoundTripFormat(decimal? value, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            else
                return RoundTripFormat(value.Value, culture);
        }

        private static CultureInfo m_usCulture;
        public static CultureInfo USCulture
        {
            get
            {
                if (m_usCulture == null)
                    m_usCulture = new CultureInfo("en-US");
                return m_usCulture;
            }
        }
        
    }
}
