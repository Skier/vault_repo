using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchedule
{
    public class Utils
    {
        public static readonly DateTime SERVMAN_NULL_DATE = new DateTime(1899, 12, 30);

        #region GetDateTimeFromServman

        public static DateTime? GetDateTimeFromServman(DateTime date, string time)
        {
            if (date.Date == SERVMAN_NULL_DATE)
                return null;

            int hour = 0;
            int minute = 0;

            time = time.Trim();

            if (time != string.Empty && time.Length >= 2)
            {
                try
                {
                    hour = int.Parse(time.Substring(0, 2));
                }
                catch (Exception){}
            }

            if (time != string.Empty && time.Length >= 4)
            {
                try
                {
                    minute = int.Parse(time.Substring(2, 2));
                }
                catch (Exception){}
            }

            return new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
        }

        public static DateTime? GetDateTimeFromServman(DateTime date)
        {
            return GetDateTimeFromServman(date, string.Empty);
        }

        #endregion

        #region JoinStrings

        public static string JoinStrings(string separator, params string[] str)
        {
            string result = string.Empty;

            foreach (string s in str)
            {
                if (s != null && s != string.Empty)
                    result += s + separator;
            }

            if (result != string.Empty)
                return result.Remove(result.Length - separator.Length, separator.Length);
            return result;
        }

        #endregion

        #region FormatPhone

        public static string FormatPhone(string phone)
        {
            if (phone == null || phone == string.Empty)
                return string.Empty;

            string part1;
            string part2 = string.Empty;
            string part3 = string.Empty;
            string part4 = string.Empty;

            if (phone.Substring(0).Length < 3)
                part1 = phone.Substring(0);
            else
                part1 = phone.Substring(0, 3);

            if (phone.Length > 3)
            {
                if (phone.Substring(3).Length < 3)
                    part2 = phone.Substring(3);
                else
                    part2 = phone.Substring(3, 3);

                if (phone.Length > 6)
                {
                    if (phone.Substring(6).Length < 4)
                        part3 = phone.Substring(6);
                    else
                        part3 = phone.Substring(6, 4);
                    
                    if (phone.Length > 10)
                        part4 = phone.Substring(10);
                }
            }

            string result = string.Format("({0}) {1}-{2}", part1, part2, part3);
            if (part4 != string.Empty)
                result += " " + part4;
            return result;
        }

        #endregion

        #region RoundTo30Min

        public static DateTime RoundTo30Min(DateTime time)
        {            
            if (time.Minute >= 0 && time.Minute < 15)
                return new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0);
            if (time.Minute >= 15 && time.Minute < 45)
                return new DateTime(time.Year, time.Month, time.Day, time.Hour, 30, 0);
            if (time.Minute >= 45)
                return new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0).AddHours(1);
            return time;
        }

        #endregion

        #region RoundTo15Min

        public static DateTime RoundTo15Min(DateTime time)
        {
            int minute = 45;

            if (time.Minute <= 14)
                minute = 0;
            if (time.Minute >= 15 && time.Minute <= 29)
                minute = 15;
            if (time.Minute >= 30 && time.Minute <= 44)
                minute = 30;
            
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, minute, 0);
        }

        #endregion

        #region ReformatText

        //maxLineLengths specifies how many chars each line should contain. If result contains more lines 
        //than this parameter specifies - all unspecified lines will have length equal last item value in list

        public static List<string> DivideText(string text, List<int> maxBlockLengths)
        {
            List<string> result = new List<string>();
            if (text == string.Empty)
            {
                result.Add(string.Empty);
                return result;
            }

            while (text != string.Empty)
            {
                int maxLineLength;

                if (result.Count > maxBlockLengths.Count - 1)
                    maxLineLength = maxBlockLengths[maxBlockLengths.Count - 1];
                else
                    maxLineLength = maxBlockLengths[result.Count];

                string currentLine;
                if (text.Length > maxLineLength)
                    currentLine = text.Substring(0, maxLineLength);
                else
                    currentLine = text;

                if (currentLine.Contains("\r\n"))
                {
                    currentLine = currentLine.Substring(0, currentLine.IndexOf("\r\n"));
                }
                else if (text.Length <= maxLineLength)
                {
                    //do nothing
                }
                else if (currentLine.Contains(" "))
                {
                    currentLine = currentLine.Substring(0, currentLine.LastIndexOf(' '));
                }

                result.Add(currentLine);
                text = text.Remove(0, currentLine.Length).Trim("\r\n ".ToCharArray());
            }

            return result;
        }

        public static List<string> DivideText(string text, int maxBlockLength)
        {
            List<int> blockLengths = new List<int>();
            blockLengths.Add(maxBlockLength);

            return DivideText(text, blockLengths);
        }

        #endregion

        #region RemoveTrailingZeros

        public static string RemoveTrailingZeros(decimal d)
        {
            if (d.ToString().Contains("."))
                return d.ToString().TrimEnd('0').TrimEnd('.');
            else
                return d.ToString();
        }

        #endregion

        #region ExtractDigits

        public static string ExtractDigits(string s)
        {
            string result = string.Empty;

            foreach (char c in s)
            {
                if (char.IsDigit(c))
                    result += c;
            }

            return result;
        }

        #endregion
    }
}
