using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Server
{
    public class ServmanConversionUtil
    {
        #region GetArea

        public static byte? GetArea(string servmanArea)
        {
            string trimmedArea = servmanArea.Trim();
            if (trimmedArea == "D/FW")
                return 3;
            if (trimmedArea == "HOUST")
                return 4;
            if (trimmedArea == "AUSTN")
                return 1;
            if (trimmedArea == "GRAYS")
                return 2;
            return null;
        }

        #endregion

        #region Servman Map conversion

        public static string GetMapPage(string servmanGrid)
        {
            string trimmedGrid = servmanGrid.Trim();

            if (trimmedGrid == string.Empty)
                return string.Empty;

            if (char.IsLetter(trimmedGrid[trimmedGrid.Length - 1]))
                trimmedGrid = trimmedGrid.Substring(0, trimmedGrid.Length - 1);

            return trimmedGrid.Replace("-", string.Empty).Trim();
        }

        public static string GetMapLetter(string servmanGrid)
        {
            string trimmedGrid = servmanGrid.Trim();

            if (trimmedGrid == string.Empty)
                return string.Empty;

            if (char.IsLetter(trimmedGrid[trimmedGrid.Length - 1]))
                return trimmedGrid[trimmedGrid.Length - 1].ToString();
            return string.Empty;
        }

        #endregion

        #region Servman Name Conversion

        public static string GetLastName(string servmanName)
        {
            if (servmanName.IndexOf(',') >= 0)
            {
                string separator;
                
                if ((servmanName.IndexOf(',') < servmanName.Length - 1) && (servmanName[servmanName.IndexOf(',') + 1] == ' '))
                    separator = ", ";
                else
                    separator = ",";

                return servmanName.Substring(0, servmanName.IndexOf(separator)).Trim();
            }
            else
            {
                return servmanName.Trim();
            }
        }

        public static string GetFirstName(string servmanName)
        {
            if (servmanName.IndexOf(',') >= 0)
            {
                string separator;

                if ((servmanName.IndexOf(',') < servmanName.Length - 1) && (servmanName[servmanName.IndexOf(',') + 1] == ' '))
                    separator = ", ";
                else
                    separator = ",";

                return servmanName.Substring(servmanName.IndexOf(separator) + separator.Length).Trim();
            }

            return string.Empty;
        }

        #endregion

        #region GetPreferredTime

        public static void GetPreferredTime(string servmanTimeSpan, out int? startHour24, out int? endHour24)
        {
            startHour24 = null;
            endHour24 = null;

            string timeSpan = servmanTimeSpan.Trim();
            if (timeSpan.Contains("-"))
            {
                string startHourText = timeSpan.Remove(timeSpan.IndexOf("-")).Trim();
                string endHourText = timeSpan.Replace(timeSpan.Remove(timeSpan.IndexOf("-")) + "-", string.Empty).Trim();
               
                try
                {
                    int startHour = int.Parse(startHourText);
                    if (startHour < 7)
                        startHour24 = Pm(startHour);
                    else
                        startHour24 = Am(startHour);
                }
                catch (Exception){}


                try
                {
                    int endHour = int.Parse(endHourText);

                    if (startHour24 == null)
                    {
                        if (endHour <= 8)
                            endHour24 = Pm(endHour);
                        else
                            endHour24 = Am(endHour);
                    } else
                    {
                        if (Am(endHour) > startHour24.Value)
                            endHour24 = Am(endHour);
                        else
                            endHour24 = Pm(endHour);
                    }
                }
                catch (Exception){}

            } else if (timeSpan.ToUpper().Contains("AFTER"))
            {
                string hourText = timeSpan.ToUpper().Replace("AFTER", string.Empty).Trim();

                try
                {
                    int startHour = int.Parse(hourText);
                    startHour24 = Pm(startHour);
                }
                catch (Exception) { }

            } else if (timeSpan.ToUpper().Contains("AM"))
            {
                endHour24 = 12;
            } else if (timeSpan.ToUpper().Contains("PM"))
            {
                startHour24 = 12;
            }
        }

        private static int Am(int hour)
        {
            return hour;
        }

        private static int Pm(int hour)
        {
            return hour + 12;
        }

        #endregion

        #region GetServmanTimeFrame

        public static string GetServmanTimeFrame(DateTime? from, DateTime? to)
        {
            if (from.HasValue && !to.HasValue && from.Value.Hour >= 17)
                return "AFTER 5";
            if (!from.HasValue || !to.HasValue)
                return "SECTOR";

            int fromAmPm = from.Value.Hour > 12 ? from.Value.Hour - 12 : from.Value.Hour;
            int toAmPm = to.Value.Hour > 12 ? to.Value.Hour - 12 : to.Value.Hour;
            return fromAmPm + "-" + toAmPm;
        }

        #endregion

    }
}
