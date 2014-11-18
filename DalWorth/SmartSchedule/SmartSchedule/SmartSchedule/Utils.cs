using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchedule
{
    public class Utils
    {
        public static readonly DateTime SERVMAN_NULL_DATE = new DateTime(1899, 12, 30);
        public static double PENALTY_THRESHOLD = 10;
        public static double PENALTY_MULTIPLIER = 1;
        private static Dictionary<string, int> servmanTimeFrameToIdMap = new Dictionary<string, int>();

        #region Constructor

        static Utils()
        {
            servmanTimeFrameToIdMap.Add("7:30-8", 1);
            servmanTimeFrameToIdMap.Add("8-8:30", 2);
            servmanTimeFrameToIdMap.Add("8-10", 2);
            servmanTimeFrameToIdMap.Add("8-9", 2);
            servmanTimeFrameToIdMap.Add("9-11", 3);
            servmanTimeFrameToIdMap.Add("9-12", 3);
            servmanTimeFrameToIdMap.Add("10-11", 4);
            servmanTimeFrameToIdMap.Add("10-12", 4);
            servmanTimeFrameToIdMap.Add("10-1", 4);
            servmanTimeFrameToIdMap.Add("11-1", 5);
            servmanTimeFrameToIdMap.Add("11-2", 5);
            servmanTimeFrameToIdMap.Add("12-1", 6);
            servmanTimeFrameToIdMap.Add("12-2", 6);
            servmanTimeFrameToIdMap.Add("12-3", 6);
            servmanTimeFrameToIdMap.Add("1-3", 7);
            servmanTimeFrameToIdMap.Add("1-4", 7);
            servmanTimeFrameToIdMap.Add("2-3", 8);
            servmanTimeFrameToIdMap.Add("2-4", 8);
            servmanTimeFrameToIdMap.Add("2-5", 8);
            servmanTimeFrameToIdMap.Add("3-5", 9);
            servmanTimeFrameToIdMap.Add("3-6", 9);
            servmanTimeFrameToIdMap.Add("4-6", 10);
            servmanTimeFrameToIdMap.Add("4-7", 10);
            servmanTimeFrameToIdMap.Add("5-7", 11);
            servmanTimeFrameToIdMap.Add("AFTER 5", 11);
            servmanTimeFrameToIdMap.Add("6-8", 12);
            servmanTimeFrameToIdMap.Add("7-9", 12);
            servmanTimeFrameToIdMap.Add("ANY", 18);
            servmanTimeFrameToIdMap.Add("", 18);
            servmanTimeFrameToIdMap.Add("OTHER", 18);
            servmanTimeFrameToIdMap.Add("SECTOR", 18);

        }

        #endregion


        #region BalanceToMileMultiplier

        private static double m_balanceToMileMultiplier;
        public static double BalanceToMileMultiplier
        {
            get { return m_balanceToMileMultiplier; }
            set { m_balanceToMileMultiplier = value; }
        }

        #endregion

        #region SecondaryAreaToMileMultiplier

        private static double m_secondaryAreaToMileMultiplier;
        public static double SecondaryAreaToMileMultiplier
        {
            get { return m_secondaryAreaToMileMultiplier; }
            set { m_secondaryAreaToMileMultiplier = value; }
        }

        #endregion

        #region TempAssignmentToMileMultiplier

        private static double m_tempAssignmentToMileMultiplier;
        public static double TempAssignmentToMileMultiplier
        {
            get { return m_tempAssignmentToMileMultiplier; }
            set { m_tempAssignmentToMileMultiplier = value; }
        }

        #endregion


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
            if (string.IsNullOrEmpty(phone) || phone.Trim() == string.Empty)
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
                result += " ext. " + part4;
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
            int addMinute = 0;

            if (time.Minute >= 0 && time.Minute <= 7)
                addMinute = 0;
            else if (time.Minute >= 8 && time.Minute <= 22)
                addMinute = 15;
            else if (time.Minute >= 23 && time.Minute <= 37)
                addMinute = 30;
            else if (time.Minute >= 38 && time.Minute <= 52)
                addMinute = 45;
            else if (time.Minute >= 53)
                addMinute = 60;

            return time.Date.AddHours(time.Hour).AddMinutes(addMinute);
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

        #region Distance

        public static double Distance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            if ((latitude1 == 0 && longitude1 == 0)
                || (latitude2 == 0 && longitude2 == 0))
            {
                return 50; //Average distance to unknown addresses
            }

            double dLatitude = DegreeToRadian(latitude1 - latitude2);
            double dLongitude = DegreeToRadian(longitude1 - longitude2);

            double a = Math.Sin(dLatitude / 2) * Math.Sin(dLatitude / 2) +
                       Math.Cos(DegreeToRadian(latitude2)) * Math.Cos(DegreeToRadian(latitude2)) *
                       Math.Sin(dLongitude / 2) * Math.Sin(dLongitude / 2);

            return 7917.511728464 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        }

        private static double DegreeToRadian(double degree)
        {
            return Math.PI * degree / 180;
        }

        #endregion

        #region GetTimeFrameId

        public static int GetTimeFrameId(string servmanTimeFrame)
        {
            return servmanTimeFrameToIdMap[servmanTimeFrame];
        }

        #endregion

        #region FormatTime

        public static string FormatTime(DateTime time)
        {
            string result;

            if (time.Hour > 12)
                result = (time.Hour - 12).ToString();
            else
                result = time.Hour.ToString();

            if (time.Minute != 0)
                result += ":" + time.Minute.ToString("00");

            return result;
        }

        #endregion


    }
}
