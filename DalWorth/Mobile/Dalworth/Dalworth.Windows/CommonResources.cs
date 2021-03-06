using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Dalworth.Windows
{
    public static class CommonResources
    {
        #region ChangeCulture

        public static void ChangeCulture(CultureInfo cultureInfo)
        {
            CommonUIResources.Culture = cultureInfo;
        }

        #endregion

        #region Buttons

        #region Done

        public static String BtnDone
        {
            get
            {
                return CommonUIResources.BtnDone;
            }
        }

        #endregion

        #region Detail

        public static String BtnDetail
        {
            get
            {
                return CommonUIResources.BtnDetail;
            }
        }

        #endregion

        #endregion

        #region Dialog caption
        public static String DialogCaption
        {
            get
            {
                return CommonUIResources.DialogCaption;
            }
        }
        #endregion

        #region Information Title
        public static String InformationTitle
        {
            get
            {
                return CommonUIResources.InformationTitle;
            }
        }
        #endregion

        #region Question Title
        public static String QuestionTitle
        {
            get
            {
                return CommonUIResources.QuestionTitle;
            }
        }
        #endregion

        #region Warning Title
        public static String WarningTitle
        {
            get
            {
                return CommonUIResources.WarningTitle;
            }
        }
        #endregion

        #region Error Title
        public static String ErrorTitle
        {
            get
            {
                return CommonUIResources.ErrorTitle;
            }
        }
        #endregion

        #region Date

        public static String GetDayOfWeek(System.DayOfWeek key)
        {           
            switch(key)
            {
                case DayOfWeek.Sunday:
                    return CommonUIResources.DaySunday;
                case DayOfWeek.Monday:
                    return CommonUIResources.DayMonday;
                case DayOfWeek.Tuesday:
                    return CommonUIResources.DayTuesday;
                case DayOfWeek.Wednesday:
                    return CommonUIResources.DayWednesday;
                case DayOfWeek.Thursday:
                    return CommonUIResources.DayThursday;
                case DayOfWeek.Friday:
                    return CommonUIResources.DayFriday;
                case DayOfWeek.Saturday:
                    return CommonUIResources.DaySaturday;                                                                              
            }
            
            return null;
        }
                
        #endregion     
    }
}
